using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class NewPhotoController : Controller
    {
        // GET: NewPhoto
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPhoto()
        {
            Staff s = new Staff();
            s.Username = Request.Params["Username"];
            s.Password = Request.Params["Password"];
            if (s.AuthenticateStaff())
            {
               SitePhoto sp = new SitePhoto();
               /* var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files["file"] != null)
                {
                    string path = "~/updateimages/" + DateTime.Now.Ticks.ToString() + "_" + httpRequest.Files["file"].FileName; ;
                    httpRequest.Files["file"].SaveAs(HttpContext.Current.Server.MapPath(path));
                    sp.PhotoPath = path;
                }
                else
                {
                    sp.PhotoPath = "";
                }*/
                if (Request.Params["PhotoPath"] != null)
                {
                    
                    String photoString = Request.Params["PhotoPath"];

                    string path = "~/updateimages/" + DateTime.Now.Ticks.ToString() + ".jpg";


                    byte[] arr = Convert.FromBase64String(photoString);
                    System.IO.File.WriteAllBytes(Server.MapPath(path), arr);
                    sp.PhotoPath = path;
                }
                sp.StaffID = s.StaffID;
                sp.SiteID = s.SiteID;
                sp.CreateDate = DateTime.Today;
                sp.Latitude = Convert.ToInt32(Convert.ToDouble(Request.Params["Latitude"]));
                sp.Longitude = Convert.ToInt32(Convert.ToDouble(Request.Params["Longitude"]));
                sp.Feedback = Request.Params["Feedback"];
                sp.Insert();
                return Content("Image Uploaded");

            }
            else
            {
                return Content("Image not Uploaded");
            }
        }
    }
}