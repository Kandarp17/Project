using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public String Dept { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String PhotoPath { get; set; }
        public String City { get; set; }
        public String StaffType { get; set; }
        public int SiteID { get; set; }

        public int Insert()
        {
            String query = "Insert into Staff values(@Name,@Email,@Mobile,@Dept,@Username,@Password,@PhotoPath,@City,@StaffType,@SiteID)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Email", this.Email));
            lstprms.Add(new SqlParameter("@Mobile", this.Mobile));
            lstprms.Add(new SqlParameter("@Dept", this.Dept));
            lstprms.Add(new SqlParameter("@Username", this.Username));
            lstprms.Add(new SqlParameter("@Password", this.Password));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@City", this.City));
            lstprms.Add(new SqlParameter("@StaffType", this.StaffType));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Staff set Name=@Name,Email=@Email,Mobile=@Mobile,Dept=@Dept,Username=@Username,Password=@Password,PhotoPath=@PhotoPath,City=@City,StaffType=@StaffType,SiteID=@SiteID where StaffID=@StaffID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Email", this.Email));
            lstprms.Add(new SqlParameter("@Mobile", this.Mobile));
            lstprms.Add(new SqlParameter("@Dept", this.Dept));
            lstprms.Add(new SqlParameter("@Username", this.Username));
            lstprms.Add(new SqlParameter("@Password", this.Password));
            lstprms.Add(new SqlParameter("@PhotoPath", this.PhotoPath));
            lstprms.Add(new SqlParameter("@City", this.City));
            lstprms.Add(new SqlParameter("@StaffType", this.StaffType));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Staff where StaffID=@StaffID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from Staff where StaffID=@StaffID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Dept = dt.Rows[0]["Dept"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.StaffType = dt.Rows[0]["StaffType"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select *,Site.Name AS SName from Staff,Site where Staff.SiteID=Site.SiteID ";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public bool AuthenticateStaff()
        {
            String query = "Select * from Staff where Username=@Username and Password=@Password";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Username", this.Username));
            lstprms.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Dept = dt.Rows[0]["Dept"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.StaffType = dt.Rows[0]["StaffType"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateStaffMobile()
        {
            String query = "Select * from Staff where Username=@Username and Password=@Password and (StaffType='Site Manager' OR StaffType='Site Staff')";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Username", this.Username));
            lstprms.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Dept = dt.Rows[0]["Dept"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.PhotoPath = dt.Rows[0]["PhotoPath"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.StaffType = dt.Rows[0]["StaffType"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ForgetPassword()
        {
            String query = "Select Password,Email from Staff where Username=@Username";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Username", this.Username));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectStaffBySiteID()
        {
            String query = "Select Staff.*,Site.Name AS SName from Staff,Site where Staff.SiteID=@SiteID and Staff.SiteID=Site.SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public List<Staff> AllStaffBySiteID()
        {
            String query = "Select * from Staff where SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<Staff> list = new List<Staff>();
            for (int i = 0; i < dt.Rows.Count; i++) 
            {
                Staff s = new Staff();
                s.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"]);
                s.Name = dt.Rows[i]["Name"].ToString();
                s.Email = dt.Rows[i]["Email"].ToString();
                s.Mobile = dt.Rows[i]["Mobile"].ToString();
                s.Dept = dt.Rows[i]["Dept"].ToString();
                s.Username = dt.Rows[i]["Username"].ToString();
                s.Password = dt.Rows[i]["Password"].ToString();
                s.PhotoPath = dt.Rows[i]["PhotoPath"].ToString();
                s.City = dt.Rows[i]["City"].ToString();
                s.StaffType = dt.Rows[i]["StaffType"].ToString();
                s.SiteID = Convert.ToInt32(dt.Rows[i]["SiteID"]);
                list.Add(s);
            }
            return list;
        }
        public DataTable SelectRestOfStaff() //Selecting Staff not assigned to any site
        {
            String query = "Select * from Staff where SiteID!=@SiteID and SiteID!=0 Order by StaffID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;

        }
        public bool UpdateSiteID()//Replacing Staff to site
        {
            String query = "Update Staff set SiteID=@SiteID where StaffID=@StaffID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            int x = DataAccess.ModifyData(query, lstprms);
            if (x == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataTable SelectMainOfficeStaff()
        {
            string query = "select * from Staff where StaffType='Main Office'";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }

    }
}