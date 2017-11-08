using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MultisiteConstructionCompany.Controllers
{
    public class SitePhotoAPIController : ApiController
    {
        [HttpPost]
        public string UploadImage(string Username, string Password, string Feedback,string Latitude, string Longitude)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                SitePhoto sp = new SitePhoto();
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files["file"] != null)
                { 
                    string path = "~/updateimages/" + DateTime.Now.Ticks.ToString() + "_" + httpRequest.Files["file"].FileName; ;
                    httpRequest.Files["file"].SaveAs(HttpContext.Current.Server.MapPath(path));
                    sp.PhotoPath = path;
                }
                else
                {
                    sp.PhotoPath = "";
                }
                sp.StaffID = s.StaffID;
                sp.SiteID = s.SiteID;
                sp.CreateDate = DateTime.Today;
                sp.Latitude =  Convert.ToInt32((Latitude)); 
                sp.Longitude = Convert.ToInt32(Convert.ToDouble( Longitude));
                sp.Feedback = Feedback;
                sp.Insert();
                return "Image Uploaded";
            }
            else
            {
                return "Image not Uploaded";
            }
        }
        [HttpGet]
        public string GetUpdate(string Username, string Password, string Feedback, int Percentage)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                SiteUpdate su = new SiteUpdate();
                su.SiteID = s.SiteID;
                su.StaffID = s.StaffID;
                su.CreateDate = DateTime.Today;
                su.Percentage = Percentage;
                su.Feedback = Feedback;
                su.Insert();
                return "Updated";
            }
            else
            {
                return "Not Updated";
            }
        }
    }
}