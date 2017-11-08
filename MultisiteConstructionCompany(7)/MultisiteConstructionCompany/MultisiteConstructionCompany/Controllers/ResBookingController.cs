using MultisiteConstructionCompany.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class ResBookingController : Controller
    {
        // GET: ResBooking
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Staff"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Access");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }
        public ActionResult ResBookingNew()
        {
            ResCategory r = new ResCategory();
            ViewBag.cat = r.SelectAll();
            Site s = new Site();
            ViewBag.site = s.SelectAll();
            return View();
        }
        [HttpPost]
        public ActionResult BookResource(FormCollection collection)
        {
            int Quantity = Convert.ToInt32(collection["Quantity"]);
            Resource res = new Resource();
            res.ResCategoryID = Convert.ToInt32(collection["ResCategoryID"]);
            DataTable dt = res.GetResourceByCategory();
            ResBooking rb = new ResBooking();
            rb.ResCategoryID = res.ResCategoryID;
            rb.RequiredDate = Convert.ToDateTime(collection["RequiredDate"]);
            DataTable dt1 = rb.GetTotalAvailableResource();
            int qty = dt.Rows.Count - dt1.Rows.Count;
            ResBooking[] r = new ResBooking[Quantity];
            Stack s = new Stack();

            if (dt1.Rows.Count == 0 && qty >= Quantity)
            {
                for (int j = 0; j < Quantity; j++)
                {
                    r[j] = new ResBooking();
                    r[j].BookingDate = DateTime.Today;
                    r[j].RequiredDate = Convert.ToDateTime(collection["RequiredDate"]);
                    r[j].SiteID = Convert.ToInt16(collection["SiteID"]);
                    r[j].ResourceID = Convert.ToInt32(dt.Rows[j]["ResourceID"]);
                    r[j].StaffID = Convert.ToInt32(collection["StaffID"]);
                    r[j].Comments = collection["Comments"];
                    r[j].Status = "Booked";
                    r[j].ResCategoryID = Convert.ToInt32(collection["ResCategoryID"]);
                    r[j].Insert();
                }
                TempData["Success"] = "Resource Booked Successfully";
                return RedirectToAction("GetBooking");
            }
            else if (dt1.Rows.Count != 0 && qty >= Quantity) 
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
                    r[j].RequiredDate = Convert.ToDateTime(collection["RequiredDate"]);
                    r[j].SiteID = Convert.ToInt16(collection["SiteID"]);
                    r[j].ResourceID = Convert.ToInt32(s.Pop());
                    r[j].StaffID = Convert.ToInt32(collection["StaffID"]);
                    r[j].Comments = collection["Comments"];
                    r[j].Status = "Booked";
                    r[j].ResCategoryID = Convert.ToInt32(collection["ResCategoryID"]);
                    r[j].Insert();
                }
                TempData["Success"] = "Resource Booked Successfully";
                return RedirectToAction("GetBooking");
            }
            else
            {
                TempData["Error"] = "No available resource for the given category";
                return RedirectToAction("ResBookingNew");
            }
        }
        public ActionResult GetBooking(FormCollection collection)
        {
            ResBooking r = new ResBooking();
            try
            {
                if (Convert.ToInt32(collection["SiteID"]) != '\0')
                {
                    r.SiteID = Convert.ToInt32(collection["SiteID"]);
                    DataTable dt = r.GetBookingBySiteID();
                    return View(dt);
                }
                else
                {
                    DataTable dt = r.SelectAll();
                    return View(dt);
                }
            }catch(Exception)
            {
                TempData["Message"] = "Please Select the search credentials ";
                DataTable dt = r.SelectAll();
                return View(dt);
            }  
        }
        public ActionResult CancelBooking(int ID)
        {
            ResBooking r = new ResBooking();
            r.ResBookingID = ID;
            r.Delete();
            return RedirectToAction("GetBooking");
        }
    }
}