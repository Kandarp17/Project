using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MultisiteConstructionCompany.Controllers
{
    public class StaffAPIController : ApiController
    {
        public List<SiteStaff> GetMyDetails(String Username, String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                SiteStaff ss = new SiteStaff();
                ss.StaffID = s.StaffID;
                List<SiteStaff> list = ss.ListOfSiteStaff();
                return list;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("api/StaffAPI/AllSiteStaff")]
        public List<Staff> AllSiteStaff(String Username, String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                Staff ss = new Staff();
                ss.SiteID = s.SiteID;
                List<Staff> list = ss.AllStaffBySiteID();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
