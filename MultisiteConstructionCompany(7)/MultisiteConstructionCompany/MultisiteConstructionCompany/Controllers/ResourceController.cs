using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class ResourceController : Controller
    {
        // GET: Resource
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
        public ActionResult InsertResource()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(FormCollection collection)
        {
            Resource r = new Resource();
            r.Name = collection["Name"];
            r.ResCategoryID = Convert.ToInt32(collection["ResCategoryID"]);
            if (Request.Files["PhotoPath"] != null)
            {
                string path = "/resourcephoto/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                r.PhotoPath = path;
            }
            r.Specs = collection["Specs"];

            r.Insert();
            return RedirectToAction("GetAll", "Resource");
        }
        public ActionResult Delete(int ID)
        {
            Resource r = new Resource();
            r.ResourceID = ID;
            r.Delete();
            return RedirectToAction("GetAll", "Resource");
        }
        public ActionResult GetAll(FormCollection collection)
        {
            Resource res = new Resource();
            try
            {
                if (Convert.ToInt32(collection["ResCategoryID"]) != '\0')
                {
                    res.ResCategoryID = Convert.ToInt32(collection["ResCategoryID"]);
                    DataTable dt = res.SelectResourceByCategoryID();
                    return View(dt);
                }
                else
                {
                    DataTable dt = res.SelectAll();
                    return View(dt);
                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Select Category";
                DataTable dt = res.SelectAll();
                return View(dt);
            }
        }
        public ActionResult ResourceDetails(int ID)
        {
            Resource r = new Resource();
            r.ResourceID = ID;
            DataTable dt = r.SelectByResourceID();
            return View(dt);
        }
    }
}