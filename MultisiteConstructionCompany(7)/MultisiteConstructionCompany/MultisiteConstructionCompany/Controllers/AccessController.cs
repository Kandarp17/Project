using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authenticate(FormCollection collection)
        {
            Staff s = new Staff();
            s.Username = collection["username"];
            s.Password = collection["password"];
            if (s.AuthenticateStaff())
            {
                Session["Staff"] = s;
                if (((Staff)Session["Staff"]).StaffType == "Admin" || ((Staff)Session["Staff"]).StaffType == "Main Office")
                {
                    return RedirectToAction("Dashboard", "Site");
                }
                else
                {
                    return RedirectToAction("Login", "Access");
                }
            }
            else
            {
                TempData["Message"] = "Enter Valid Credentials";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Forget(FormCollection collection)
        {
            try
            {
                Staff s = new Staff();
                s.Username = collection["username"];
                DataTable dt = s.ForgetPassword();
                if (dt.Rows.Count > 0)
                {
                    string sendID = "mygtuproject@gmail.com";//EmailID
                    string password = "myproject@gtu";//Password
                    SmtpClient sc = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sendID, password)
                    };
                    string email = dt.Rows[0]["Email"].ToString();
                    string pwd = dt.Rows[0]["Password"].ToString();
                    string body = "Your Username for Login is " + email + " And Password is " + pwd + "";
                    string subject = "Welcome to My GTU Project";
                    MailMessage mail = new MailMessage(sendID, email, subject, body);
                    sc.Send(mail);
                }
                TempData["Message"] = "Email sent. Please Login again.";
                return RedirectToAction("Login","Access");
            }
            catch
            {
                TempData["Message"] = "Some Error in sending the mail.";
                return RedirectToAction("Login", "Access");
            }
        }
        
        public string SignIn()
        {
            try
            {
                Staff s = new Staff();
                s.Username = Request.QueryString["Username"];
                s.Password = Request.QueryString["Password"];
                if (s.AuthenticateStaffMobile())
                {
                    return "Success";
                }
                else
                {
                    return "Invalid Credentials";
                }
            }
            catch
            {
                return "Fail";
            }
        }

        public string ForgetMobile()
        {
            try
            {
                Staff s = new Staff();
                s.Username = Request.QueryString["Username"];
                DataTable dt = s.ForgetPassword();
                if (dt.Rows.Count > 0)
                {
                    string sendID = "mygtuproject@gmail.com";//EmailID
                    string password = "myproject@gtu";//Password
                    SmtpClient sc = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sendID, password)
                    };
                    string email = dt.Rows[0]["Email"].ToString();
                    string pwd = dt.Rows[0]["Password"].ToString();
                    string body = "Your Username for Login is " + email + " And Password is " + pwd + "";
                    string subject = "Welcome to My GTU Project";
                    MailMessage mail = new MailMessage(sendID, email, subject, body);
                    sc.Send(mail);
                }
                return "Success";
            }
            catch
            {
                return "Fail";
            }
        }
    }
}