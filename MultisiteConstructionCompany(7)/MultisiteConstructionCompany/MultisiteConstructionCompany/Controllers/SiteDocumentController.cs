using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class SiteDocumentController : Controller
    {
        // GET: SiteDocument
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
        public ActionResult SiteDocumentDetail()
        {
            return View();
        }
        public ActionResult Insert(FormCollection collection)
        {
            SiteDocument s = new SiteDocument();
            s.DocumentName = collection["DocumentName"];
            if (Request.Files["DocumentPath"] != null)
            {
                string path = "~/sitedocuments/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["DocumentPath"].FileName;
                Request.Files["DocumentPath"].SaveAs(Server.MapPath(path));
                s.DocumentPath = path;
            }

            s.Status = collection["Status"];
            s.SiteID = Convert.ToInt32(collection["SiteID"]);
            s.Insert();
            return RedirectToAction("SearchDocument","SiteDocument");
        }
        public ActionResult RemoveDocument(int id)
        {
            SiteDocument s = new SiteDocument();
            s.SiteDocumentID = id;
            s.Delete();
            return RedirectToAction("SiteInformation", "Site");
        }
        public ActionResult DeleteDocument(int id)
        {
            SiteDocument s = new SiteDocument();
            s.SiteDocumentID = id;
            s.Delete();
            return RedirectToAction("SearchDocument", "SiteDocument");
        }
        public ActionResult SearchDocument(FormCollection collection)
        {
            SiteDocument s = new SiteDocument();
            try
            {
                if (Convert.ToInt32(collection["SiteID"]) != '\0')
                {
                    s.SiteID = Convert.ToInt32(collection["SiteID"]);
                    DataTable dt = s.getDocumentByID();
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
            catch(Exception)
            {
                TempData["Message"] = "Select the Site first";
                DataTable dt = s.SelectAll();
                ViewBag.Add = dt;
                return View();
            }
        }
    }
}