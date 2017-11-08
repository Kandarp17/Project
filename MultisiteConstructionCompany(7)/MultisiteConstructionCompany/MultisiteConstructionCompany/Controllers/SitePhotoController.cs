using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultisiteConstructionCompany.Controllers
{
    public class SitePhotoController : Controller
    {
        // GET: SitePhoto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SiteProgress(int ID)
        {
            SitePhoto sp = new SitePhoto();
            sp.SiteID = ID;
            ViewBag.dt = sp.SelectBySiteID();
            SiteUpdate su = new SiteUpdate();
            su.SiteID = ID;
            ViewBag.dt1 = su.SelectBySiteID();
            Site s = new Site();
            s.SiteID = ID;
            s.SelectByID();
            return View(s);

        }
    }
}