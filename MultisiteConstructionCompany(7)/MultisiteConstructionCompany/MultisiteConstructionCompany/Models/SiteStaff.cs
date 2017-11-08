using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace MultisiteConstructionCompany.Models
{
    public class SiteStaff
    {
        public int StaffID { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Mobile { get; set; }
        public String Dept { get; set; }
        public String PhotoPath { get; set; }
        public String City { get; set; }
        public String StaffType { get; set; }
        public int SiteID { get; set; }
        public string SName { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string SCity { get; set; }
        public string Phone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public string SPhotoPath { get; set; }

        public List<SiteStaff> ListOfSiteStaff()
        {
            string query = "Select Staff.*,Site.Name AS SName,Site.Address,Site.Area,Site.City AS SCity,Site.Phone,Site.StartDate,Site.EndDate,Site.Status,Site.Details,Site.PhotoPath AS SPhotoPath from Staff,Site where Staff.StaffID=@StaffID and Site.ContractorID=@StaffID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID",this.StaffID));
            DataTable dt= DataAccess.SelectData(query, lstprms);
            List<SiteStaff> list = new List<SiteStaff>();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                SiteStaff s = new SiteStaff();
                s.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"]);
                s.Name = dt.Rows[i]["Name"].ToString();
                s.Email = dt.Rows[i]["Email"].ToString();
                s.Mobile = dt.Rows[i]["Mobile"].ToString();
                s.Dept = dt.Rows[i]["Dept"].ToString();
                s.PhotoPath = dt.Rows[i]["PhotoPath"].ToString();
                s.City = dt.Rows[i]["City"].ToString();
                s.StaffType = dt.Rows[i]["StaffType"].ToString();
                s.SiteID= Convert.ToInt32(dt.Rows[i]["SiteID"]);
                s.SName = dt.Rows[i]["SName"].ToString();
                s.Address = dt.Rows[i]["Address"].ToString();
                s.Area = dt.Rows[i]["Area"].ToString();
                s.SCity = dt.Rows[i]["SCity"].ToString();
                s.Phone = dt.Rows[i]["Phone"].ToString();
                s.StartDate= Convert.ToDateTime(dt.Rows[i]["StartDate"]);
                s.EndDate = Convert.ToDateTime(dt.Rows[i]["EndDate"]);
                s.Status = dt.Rows[i]["Status"].ToString();
                s.Details = dt.Rows[i]["Details"].ToString();
                s.SPhotoPath = dt.Rows[i]["SPhotoPath"].ToString();
                list.Add(s);
            }
            return list;
        }
    }
}