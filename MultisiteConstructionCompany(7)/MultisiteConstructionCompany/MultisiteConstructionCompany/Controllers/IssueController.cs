using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class IssueController : Controller
    {
        // GET: Issue
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
        public ActionResult GetIssue()
        {
            Issue i = new Issue();
            DataTable dt = i.SelectAll();
            return View(dt);
        }
        public ActionResult ReplyIssue(int id)
        {
            Issue i = new Issue();
            i.IssueID = id;
            i.SelectByID();
            return View(i);
        }
        [HttpPost]
        public ActionResult ReplyIssue(FormCollection collection)
        {
            Issue i = new Issue();
            i.IssueID = Convert.ToInt32(collection["IssueID"]);
            i.ReplyMsg = Request.Form["ReplyMsg"];
            i.Status = "Replied";
            i.ReplyTime = DateTime.Today;
            i.Update();
            return RedirectToAction("GetIssue");
        }
    }
}