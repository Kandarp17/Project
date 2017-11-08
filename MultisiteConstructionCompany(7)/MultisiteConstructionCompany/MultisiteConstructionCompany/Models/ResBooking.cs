using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class ResBooking
    {
        public int ResBookingID { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public int SiteID { get; set; }
        public int ResourceID { get; set; }
        public int StaffID { get; set; }
        public String Comments { get; set; }
        public String Status { get; set; }
        public int ResCategoryID { get; set; }

        public int Insert()
        {
            String query = "Insert into ResBooking values(@BookingDate,@RequiredDate,@SiteID,@ResourceID,@StaffID,@Comments,@Status,@ResCategoryID)";
            List<SqlParameter> lstprms = new List<SqlParameter>();
          
            lstprms.Add(new SqlParameter("@BookingDate", this.BookingDate));
            lstprms.Add(new SqlParameter("@RequiredDate", this.RequiredDate));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@ResourceID", this.ResourceID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@Comments", this.Comments));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));


            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }

        public int Update()
        {
            String query = "Update ResBooking set BookingDate=@BookingDate,RequiredDate=@RequiredDate,SiteID=@SiteID,ResourceID=@ResourceID,StaffID=@StaffID,Comments=@Comments,Status=@Status,ResCategoryID=@ResCategoryID where ResBookingID=@ResBookingID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResBookingID", this.ResBookingID));
            lstprms.Add(new SqlParameter("@BookingDate", this.BookingDate));
            lstprms.Add(new SqlParameter("@RequiredDate", this.RequiredDate));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@ResourceID", this.ResourceID));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@Comments", this.Comments));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from ResBooking where ResBookingID=@ResBookingID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResBookingID", this.ResBookingID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from ResBooking where ResBookingID=@ResBookingID";
           
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ResBookingID", this.ResBookingID));
            DataTable dt= DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.ResBookingID = Convert.ToInt32(dt.Rows[0]["ResBookingID"]);
                this.BookingDate = Convert.ToDateTime(dt.Rows[0]["BookingDate"]);
                this.RequiredDate = Convert.ToDateTime(dt.Rows[0]["RequiredDate"]);
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                this.ResourceID = Convert.ToInt32(dt.Rows[0]["ResourceID"]);
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.Comments = dt.Rows[0]["Comments"].ToString();
                this.Status = dt.Rows[0]["Status"].ToString();
                this.ResCategoryID = Convert.ToInt32(dt.Rows[0]["ResCategoryID"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<BookingInformation> SelectPastBooking()
        {
            String query = "Select ResBooking.*,Resource.Name AS RName,Site.Name AS SName from ResBooking INNER JOIN Resource ON ResBooking.ResourceID=Resource.ResourceID INNER JOIN Site ON ResBooking.SiteID=Site.SiteID where ResBooking.RequiredDate < @RequiredDate and ResBooking.StaffID=@StaffID ORDER BY ResBookingID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();                                                                          
            lstprms.Add(new SqlParameter("@RequiredDate",this.RequiredDate));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<BookingInformation> lst = new List<BookingInformation>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BookingInformation b = new BookingInformation();
                b.ResBookingID = Convert.ToInt32(dt.Rows[i]["ResBookingID"]);
                b.BookingDate = Convert.ToDateTime(dt.Rows[i]["BookingDate"]);
                b.RequiredDate = Convert.ToDateTime(dt.Rows[i]["RequiredDate"]);
                b.SiteID = Convert.ToInt32(dt.Rows[i]["SiteID"]);
                b.ResourceID = Convert.ToInt32(dt.Rows[i]["ResourceID"]);
                b.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"]);
                b.Comments = dt.Rows[i]["Comments"].ToString();
                b.Status = dt.Rows[i]["Status"].ToString();
                b.ResCategoryID = Convert.ToInt32(dt.Rows[i]["ResCategoryID"]);
                b.SName = dt.Rows[i]["SName"].ToString();
                b.RName = dt.Rows[i]["RName"].ToString();
                lst.Add(b);
            }
            return lst;
        }
        public List<BookingInformation> SelectBooking()
        {
            String query = "Select ResBooking.*,Resource.Name AS RName,Site.Name AS SName from ResBooking INNER JOIN Resource ON ResBooking.ResourceID=Resource.ResourceID INNER JOIN Site ON ResBooking.SiteID=Site.SiteID where ResBooking.RequiredDate >= @RequiredDate and ResBooking.StaffID=@StaffID ORDER BY ResBookingID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@RequiredDate", this.RequiredDate));
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<BookingInformation> lst = new List<BookingInformation>();
            for(int i=0;i< dt.Rows.Count; i++)
            {
                BookingInformation b = new BookingInformation();
                b.ResBookingID = Convert.ToInt32(dt.Rows[i]["ResBookingID"]);
                b.BookingDate = Convert.ToDateTime(dt.Rows[i]["BookingDate"]);
                b.RequiredDate = Convert.ToDateTime(dt.Rows[i]["RequiredDate"]);
                b.SiteID = Convert.ToInt32(dt.Rows[i]["SiteID"]);
                b.ResourceID = Convert.ToInt32(dt.Rows[i]["ResourceID"]);
                b.StaffID = Convert.ToInt32(dt.Rows[i]["StaffID"]);
                b.Comments = dt.Rows[i]["Comments"].ToString();
                b.Status = dt.Rows[i]["Status"].ToString();
                b.ResCategoryID = Convert.ToInt32(dt.Rows[i]["ResCategoryID"]);
                b.SName = dt.Rows[i]["SName"].ToString();
                b.RName = dt.Rows[i]["RName"].ToString();
                lst.Add(b);
            }
            return lst;
        }
        public DataTable GetTotalAvailableResource()
        {
            String query = "Select * from ResBooking where RequiredDate=@RequiredDate and ResCategoryID=@ResCategoryID ORDER BY ResourceID ASC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@RequiredDate", this.RequiredDate));
            lstprms.Add(new SqlParameter("@ResCategoryID", this.ResCategoryID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable GetBookingBySiteID()
        {
            String query = "Select ResBooking.*,Resource.Name AS RName,Site.Name AS SName from ResBooking INNER JOIN Resource ON ResBooking.ResourceID=Resource.ResourceID INNER JOIN Site ON ResBooking.SiteID=Site.SiteID where ResBooking.SiteID=@SiteID ORDER BY ResBookingID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable SelectAll()
        {
            String query = "Select ResBooking.*,Resource.Name AS RName,Site.Name AS SName from ResBooking INNER JOIN Resource ON ResBooking.ResourceID=Resource.ResourceID INNER JOIN Site ON ResBooking.SiteID=Site.SiteID ORDER BY ResBookingID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}