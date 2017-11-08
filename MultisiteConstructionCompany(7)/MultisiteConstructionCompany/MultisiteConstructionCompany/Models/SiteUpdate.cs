using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class SiteUpdate
    {
        public int SiteUpdateID { get; set; }
        public int SiteID { get; set; }
        public int StaffID { get; set; }
        public DateTime CreateDate { get; set; }
        public float Percentage { get; set; }
        public string Feedback { get; set; }
        public int Insert()
        {
            String query = "Insert into SiteUpdate values(@SiteID,@StaffID,@CreateDate,@Percentage,@Feedback)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Percentage", this.Percentage));
            lstprms.Add(new SqlParameter("@Feedback", this.Feedback));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update SiteUpdate set SiteID=@SiteID,StaffID=@StaffID,CreateDate=@CreateDate,Percentage=@Percentage,Feedback=@Feedback where SiteUpdateID=@SiteUpdateID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteUpdateID", this.SiteUpdateID));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Percentage", this.Percentage));
            lstprms.Add(new SqlParameter("@Feedback", this.Feedback));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from SiteUpdate where SiteUpdateID=@SiteUpdateID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteUpdateID", this.SiteUpdateID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from SiteUpdate where SiteUpdateID=@SiteUpdateID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteUpdateID", this.SiteUpdateID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.SiteUpdateID = Convert.ToInt32(dt.Rows[0]["SiteUpdateID"]);
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.Percentage = (float)Convert.ToDouble(dt.Rows[0]["Percentage"]);
                this.Feedback = dt.Rows[0]["Feedback"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select * from SiteUpdate";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectBySiteID()
        {
            String query = "Select *,Site.Name from SiteUpdate INNER JOIN Site ON SiteUpdate.SiteID=Site.SiteID where SiteUpdate.SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}