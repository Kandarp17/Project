using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultisiteConstructionCompany.Controllers
{
    public class ResCategoryAPIController : ApiController
    {
        [HttpGet]
        public List<ResCategory> SelectAllCategory(String Username,String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                ResCategory r = new ResCategory();
                List<ResCategory> list = r.SelectAllCategory();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
