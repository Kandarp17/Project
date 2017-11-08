using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class ResCategoryController : Controller
    {
        // GET: ResCategory
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
        public ActionResult InsertResCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(FormCollection collection)
        {
            ResCategory res = new ResCategory();
            res.Name = collection["Name"];
            res.Insert();
            return RedirectToAction("GetAll","ResCategory");
        }
        public ActionResult GetAll()
        {
            ResCategory res = new ResCategory();
            DataTable dt = res.SelectAll();
            return View(dt);
        }
        public ActionResult Delete(int ID)
        {
            ResCategory res = new ResCategory();
            res.ResCategoryID = ID;
            res.Delete();
            return RedirectToAction("GetAll", "ResCategory");
        }
    }
}