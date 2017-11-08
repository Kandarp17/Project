using MultisiteConstructionCompany.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultisiteConstructionCompany.Controllers
{
    public class ResBookingAPIController : ApiController
    {
        [HttpGet]
        public String ResourceBooking(String Username,String Password,String Date,int qty,int ResCategoryID,String Comments)
        {
            Staff st = new Staff();
            st.Username = Username;
            st.Password = Password;
            if (st.AuthenticateStaff())
            {
                int Quantity = Convert.ToInt32(qty);
                Resource res = new Resource();
                res.ResCategoryID = Convert.ToInt32(ResCategoryID);
                DataTable dt = res.GetResourceByCategory();
                ResBooking rb = new ResBooking();
                rb.ResCategoryID = res.ResCategoryID;
                rb.RequiredDate = Convert.ToDateTime(Date);
                DataTable dt1 = rb.GetTotalAvailableResource();
                int availqty = dt.Rows.Count - dt1.Rows.Count;
                ResBooking[] r = new ResBooking[Quantity];
                Stack s = new Stack();

                if (dt1.Rows.Count == 0 && availqty >= Quantity)
                {
                    for (int j = 0; j < Quantity; j++)
                    {
                        r[j] = new ResBooking();
                        r[j].BookingDate = DateTime.Today;
                        r[j].RequiredDate = Convert.ToDateTime(Date);
                        r[j].SiteID = Convert.ToInt16(st.SiteID);
                        r[j].ResourceID = Convert.ToInt32(dt.Rows[j]["ResourceID"]);
                        r[j].StaffID = Convert.ToInt32(st.StaffID);
                        r[j].Comments = Comments;
                        r[j].Status = "Booked";
                        r[j].ResCategoryID = Convert.ToInt32(ResCategoryID);
                        r[j].Insert();
                    }
                    return "Resource Booked Successfully";
                }
                else if (dt1.Rows.Count != 0 && availqty >= Quantity)
                {

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            if (Convert.ToInt32(dt.Rows[k]["ResourceID"]) == Convert.ToInt32(dt1.Rows[i]["ResourceID"]))
                            {

                            }
                            else
                            {
                                s.Push(Convert.ToInt32(dt.Rows[k]["ResourceID"]));
                            }
                        }
                    }
                    for (int j = 0; j < Quantity; j++)
                    {
                        r[j] = new ResBooking();
                        r[j].BookingDate = DateTime.Today;
                        r[j].RequiredDate = Convert.ToDateTime(Date);
                        r[j].SiteID = Convert.ToInt16(st.SiteID);
                        r[j].ResourceID = Convert.ToInt32(s.Pop());
                        r[j].StaffID = Convert.ToInt32(st.StaffID);
                        r[j].Comments = Comments;
                        r[j].Status = "Booked";
                        r[j].ResCategoryID = Convert.ToInt32(ResCategoryID);
                        r[j].Insert();
                    }
                    return "Resource Booked Successfully";
                }
                else
                {
                    return "No available resource for the given category";
                }
            }
            else
            {
                return "Booking Failed! Please try again.";
            }
        }
        [HttpGet]
        [Route ("api/ResBookingAPI/PastBookings")]
        public List<BookingInformation> PastBookings(String Username,String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                ResBooking r = new ResBooking();
                r.RequiredDate = DateTime.Now;
                r.StaffID = s.StaffID;
                List<BookingInformation> list = r.SelectPastBooking();
                return list;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("api/ResBookingAPI/MyBookings")]
        public List<BookingInformation> MyBookings(String Username, String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                ResBooking r = new ResBooking();
                r.RequiredDate = DateTime.Now;
                r.StaffID = s.StaffID;
                List<BookingInformation> list = r.SelectBooking();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
