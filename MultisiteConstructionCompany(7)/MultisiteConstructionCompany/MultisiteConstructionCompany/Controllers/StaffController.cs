using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StaffDetail(int ID)
        {
            Staff s = new Staff();
            s.StaffID = ID;
            s.SelectByID();
            return View(s);
        }
        public ActionResult Insert(FormCollection collection)
        {
            Staff s = new Staff();
            s.StaffID = Convert.ToInt32(collection["StaffID"]);
            s.SelectByID();
            if (s.StaffID > 0)
            {
                s.StaffID = Convert.ToInt32(collection["StaffID"]);
                s.Name = collection["Name"];
                s.Email = collection["Email"];
                s.Mobile = collection["Mobile"];
                s.Dept = collection["Dept"];
                s.Username = collection["Email"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/staffphotos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    s.PhotoPath = path;
                }
                s.City = collection["City"];
                s.StaffType = collection["StaffType"];
                s.SiteID = 0;
                s.Update();
                return RedirectToAction("GetAll", "Staff");
            }
            else
            {
                s.Name = collection["Name"];
                s.Email = collection["Email"];
                s.Mobile = collection["Mobile"];
                s.Dept = collection["Dept"];
                s.Username = collection["Email"];
                
                    StringBuilder st = new StringBuilder();
                    Random r = new Random((int)DateTime.Now.Ticks);
                    char ch;
                    for (int i = 0; i < 8; i++)
                    {
                        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * r.NextDouble() + 65)));
                        st.Append(ch);
                    }
                    s.Password = st.ToString();
               
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/staffphotos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    s.PhotoPath = path;
                }
                s.City = collection["City"];
                s.StaffType = collection["StaffType"];
                s.SiteID = 0;
                try
                {
                    string sendID = "mygtuproject@gmail.com";//EmailID
                    string password = "myproject@gtu";//Password
                    SmtpClient sc = new SmtpClient
                    {
                        Host = "smtp.gmail.com" ,
                        Port = 587 ,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(sendID,password)
                    };
                    string body = "Your Username for MSCC Login is "+ s.Email + " And Password is " + s.Password + "";
                    string subject = "Welcome to MSCC";
                    MailMessage mail = new MailMessage(sendID,s.Email,subject,body);
                    sc.Send(mail);
                    s.Insert();
                    return RedirectToAction("GetAll","Staff");      
                 }
                catch (Exception e)
                {
                    TempData["Message"] = e.Message;
                    return RedirectToAction("StaffDetail/"+"0");
                }
            }
        }
        public ActionResult GetAll(FormCollection collection)
        {
            Staff s = new Staff();
            try
            {
                if (Convert.ToInt32(collection["SiteID"]) != '\0')
                {
                    s.SiteID = Convert.ToInt32(collection["SiteID"]);
                    DataTable dt = s.SelectStaffBySiteID();
                    ViewBag.Add = dt;
                    return View();
                }
                else
                {
                    DataTable dt = s.SelectAll();
                    ViewBag.Add = dt;
                    return View();
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Select the Site first";
                DataTable dt = s.SelectAll();
                ViewBag.Add = dt;
                return View();
            }

        }
        public ActionResult UpdateSiteID(FormCollection collection)
        {
            Staff s = new Staff();
            s.StaffID = Convert.ToInt32(collection["StaffID"]);
            s.SiteID = Convert.ToInt32(collection["SiteID"]);
            s.UpdateSiteID();
            return RedirectToAction("SiteInformation/"+s.SiteID, "Site");
        }
        public ActionResult MyProfile()
        {
            if(Session["Staff"] == null)
            {
                return RedirectToAction("Login","Access");
            }
            else
            { 
                return View();
            }
        }
        public ActionResult AllStaff()
        {
            Staff s = new Staff();
            DataTable dt = s.SelectMainOfficeStaff();
            return View(dt);
        }
    }
}