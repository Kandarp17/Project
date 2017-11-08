using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultisiteConstructionCompany.Controllers
{
    public class SiteDocumentAPIController : ApiController
    {
        public List<SiteDocument> GetSiteDocument(String Username, String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                SiteDocument sd = new SiteDocument();
                sd.SiteID = s.SiteID;
                List<SiteDocument> list = sd.getDocumentBySiteID();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
