using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public string PhotoPath { get; set; }
        public int ContractorID { get; set; }

        public int Insert()
        {
            String query = "Insert into Site values(@Name,@Address,@Area,@City,@Phone,@StartDate,@EndDate,@Status,@Details,@PhotoPath,@ContractorID)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Address", this.Address));
            lstprms.Add(new SqlParameter("@Area", this.Area));
            lstprms.Add(new SqlParameter("@City", this.City));
            lstprms.Add(new SqlParameter("@Phone", this.Phone));
            lstprms.Add(new SqlParameter("@StartDate", this.StartDate));
            lstprms.Add(new SqlParameter("@EndDate", this.EndDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@Details", this.Details));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@ContractorID", this.ContractorID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Site set Name=@Name,Address=@Address,Area=@Area,City=@City,Phone=@Phone,Status=@Status,Details=@Details,PhotoPath=@PhotoPath where SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Address", this.Address));
            lstprms.Add(new SqlParameter("@Area", this.Area));
            lstprms.Add(new SqlParameter("@City", this.City));
            lstprms.Add(new SqlParameter("@Phone", this.Phone));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@Details", this.Details));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Site where SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from Site where SiteID=@SiteID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {               
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.Area = dt.Rows[0]["Area"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.Phone = dt.Rows[0]["Phone"].ToString();
                this.StartDate =Convert.ToDateTime(dt.Rows[0]["StartDate"]);
                this.EndDate = Convert.ToDateTime(dt.Rows[0]["EndDate"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.Details = dt.Rows[0]["Details"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.ContractorID = Convert.ToInt32(dt.Rows[0]["ContractorID"]);

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select * from Site";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectActiveSites()
        {
            String query = "Select Site.*,Staff.Name AS SName from Site INNER JOIN Staff ON Site.ContractorID=Staff.StaffID where Site.Status='Active' and Staff.StaffType='Site Manager'";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectInactiveSites()
        {
            String query = "Select Site.*,Staff.Name AS SName from Site INNER JOIN Staff ON Site.ContractorID=Staff.StaffID where Site.Status='Inactive' and Staff.StaffType='Site Manager'";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable NumberOfDaysBetweenStartAndEndDate()//Get Total number of days from start date to end date
        {
            string query = "Select DATEDIFF(day,@StartDate,@EndDate) As Diff,SiteID from Site";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StartDate", this.StartDate));
            lstprms.Add(new SqlParameter("@EndDate", this.EndDate));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}