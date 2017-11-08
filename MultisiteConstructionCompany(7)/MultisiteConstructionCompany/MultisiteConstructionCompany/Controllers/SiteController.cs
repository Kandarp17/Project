using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
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
        public ActionResult Dashboard()
        {
            SiteUpdate sd = new SiteUpdate();
            Site s = new Site();
            DataTable dt = s.SelectActiveSites();
            DataTable[] dt1 = new DataTable[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt1[i] = new DataTable();
                sd.SiteID = Convert.ToInt32(dt.Rows[i]["SiteID"]);
                dt1[i] = sd.SelectBySiteID();
            }
            ViewBag.Tables = dt1;
            return View();
        }
        public ActionResult SiteDetail(int ID)
        {
           
            Site s = new Site();
            s.SiteID = ID;
            s.SelectByID();
            Staff st = new Staff();
            ViewBag.dt = st.SelectAll();
            return View(s);
        }
        [HttpPost]
        public ActionResult Insert(FormCollection collection)
        {
            Site s = new Site();
            s.SiteID = Convert.ToInt32(collection["SiteID"]);

            if (s.SiteID > 0)
            {
                s.SelectByID();
                s.Name = collection["Name"];
                s.Address = collection["Address"];
                s.Area = collection["Area"];
                s.City = collection["City"];
                s.Phone = collection["Phone"];
                s.StartDate =Convert.ToDateTime(collection["StartDate"]);
                s.EndDate =Convert.ToDateTime(collection["EndDate"]);
                s.Status = collection["Status"];
                s.Details = collection["Details"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/site/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    s.PhotoPath = path;
                }
                s.ContractorID =Convert.ToInt32(collection["ContractorID"]);
                s.Update();
            }
            else
            {
              
                s.Name = collection["Name"];
                s.Address = collection["Address"];
                s.Area = collection["Area"];
                s.City = collection["City"];
                s.Phone = collection["Phone"];
                s.StartDate = Convert.ToDateTime(collection["StartDate"]);
                s.EndDate = Convert.ToDateTime(collection["EndDate"]);
                s.Status = collection["Status"];
                s.Details = collection["Details"];
                if (Request.Files["PhotoPath"] != null)
                {
                    string path = "/site/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["PhotoPath"].FileName;
                    Request.Files["PhotoPath"].SaveAs(Server.MapPath(path));
                    s.PhotoPath = path;
                }
                s.ContractorID = Convert.ToInt32(collection["ContractorID"]);
                s.Insert();
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult GetAll()
        {
            Site s = new Site();
            DataTable dt = s.SelectAll();
            return View(dt);

        }
        public ActionResult SiteInformation(int ID)
        {
            Site s = new Site();
            s.SiteID = ID;
            s.SelectByID();
            Staff staff = new Staff();
            staff.SiteID = ID;
            DataTable dt = staff.SelectStaffBySiteID();
            ViewBag.staff = dt;
            Staff moreStaff = new Staff();
            moreStaff.SiteID = ID;
            DataTable dt1 = moreStaff.SelectRestOfStaff();
            ViewBag.moreStaff = dt1;
            SiteDocument sd = new SiteDocument();
            sd.SiteID = ID;
            DataTable dt2 = sd.getDocumentByID();
            ViewBag.getDocument = dt2;
            return View(s);
        }
        public ActionResult ActiveSites()
        {
            Site s = new Site();
            DataTable dt = s.SelectActiveSites();
            return View(dt);
        }

        public ActionResult InactiveSites()
        {
            Site s = new Site();
            DataTable dt = s.SelectInactiveSites();
            return View(dt);
        }
        public ActionResult SiteGallery()
        {
            Site s = new Site();
            DataTable dt = s.SelectAll();
            return View(dt);
        }
    }
}