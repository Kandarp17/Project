using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class Resource
    {
        public int ResourceID { get; set; }
        public string Name { get; set; }
        public int ResCategoryID { get; set; }
        public string PhotoPath { get; set; }
        public string Specs { get; set; }
        public int Insert()
        {
            String query = "Insert into Resource values(@Name,@ResCategoryID,@PhotoPath,@Specs)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@Specs", this.Specs));
            
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Resource set Name=@Name,ResCategoryID=@ResCategoryID,PhotoPath=@PhotoPath,Specs=@Specs where ResourceID=@ResourceID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@Specs", this.Specs));
           
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Resource where ResourceID=@ResourceID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResourceID", this.ResourceID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from Resource where ResourceID=@ResourceID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResourceID", this.ResourceID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.ResourceID = Convert.ToInt32(dt.Rows[0]["ResourceID"]);
                this.ResCategoryID = Convert.ToInt32(dt.Rows[0]["ResCategoryID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.Specs = dt.Rows[0]["Specs"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectResourceByCategoryID()
        {
            String query = "Select * from Resource where ResCategoryID=@ResCategoryID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectAll()
        {
            String query = "Select * from Resource";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable GetResourceByCategory()
        {
            String query = "Select * from Resource where ResCategoryID=@ResCategoryID ORDER BY ResourceID ASC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectByResourceID()
        {
            String query = "Select *,ResCategory.Name AS CName from Resource INNER JOIN ResCategory ON Resource.ResCategoryID = ResCategory.ResCategoryID where ResourceID=@ResourceID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResourceID", this.ResourceID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}