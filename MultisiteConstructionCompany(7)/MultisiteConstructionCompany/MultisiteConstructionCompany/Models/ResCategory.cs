using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class ResCategory
    {
        public int ResCategoryID { get; set; }
        public string Name { get; set; }

        public int Insert()
        {
            String query = "Insert into ResCategory values(@Name)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@Name", this.Name));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update ResCategory set Name=@Name where ResCategoryID=@ResCategoryID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResCategoryID",this.ResCategoryID));
            lstprms.Add(new SqlParameter("@Name", this.Name));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from ResCategory where ResCategoryID=@ResCategoryID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from ResCategory where ResCategoryID=@ResCategoryID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.ResCategoryID = Convert.ToInt32(dt.Rows[0]["ResCategoryID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select * from ResCategory";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }

        public List<ResCategory> SelectAllCategory()
        {
            String query = "Select * from ResCategory";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<ResCategory> list = new List<ResCategory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ResCategory r = new ResCategory();
                r.ResCategoryID = Convert.ToInt32(dt.Rows[i]["ResCategoryID"]);
                r.Name = dt.Rows[i]["Name"].ToString();
                list.Add(r);
            }
            return list;
        }
    }
}