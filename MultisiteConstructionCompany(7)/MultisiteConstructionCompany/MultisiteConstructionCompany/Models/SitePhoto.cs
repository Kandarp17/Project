using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class SitePhoto
    {
        public int SitePhotoID { get; set; }
        public string PhotoPath { get; set; }
        public int SiteID { get; set; }
        public int StaffID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Feedback { get; set; }
        public int Insert()
        {
            String query = "Insert into SitePhoto values(@PhotoPath,@SiteID,@StaffID,@CreateDate,@Latitude,@Longitude,@Feedback)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Latitude", this.Latitude));
            lstprms.Add(new SqlParameter("@Longitude", this.Longitude));
            lstprms.Add(new SqlParameter("@Feedback", this.Feedback));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update SitePhoto set PhotoPath=@PhotoPath,SiteID=@SiteID,StaffID=@StaffID,CreateDate=@CreateDate,Latitude=@Latitude,Longitude=@Longitude,Feedback=@Feedback where SitePhotoID=@SitePhotoID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SitePhotoID", this.SitePhotoID));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Latitude", this.Latitude));
            lstprms.Add(new SqlParameter("@Longitude", this.Longitude));
            lstprms.Add(new SqlParameter("@Feedback", this.Feedback));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from SitePhoto where SitePhotoID=@SitePhotoID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SitePhotoID", this.SitePhotoID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from SitePhoto where SitePhotoID=@SitePhotoID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SitePhotoID", this.SitePhotoID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.SitePhotoID = Convert.ToInt32(dt.Rows[0]["SitePhotoID"]);
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.Latitude = Convert.ToInt32(dt.Rows[0]["Latitude"]);
                this.Longitude = Convert.ToInt32(dt.Rows[0]["Longitude"]);
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
            String query = "Select * from SitePhoto";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectBySiteID()
        {
            String query = "Select * from SitePhoto where SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}