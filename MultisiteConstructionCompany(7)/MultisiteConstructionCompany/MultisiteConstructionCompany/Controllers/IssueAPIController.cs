using MultisiteConstructionCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultisiteConstructionCompany.Controllers
{
    public class IssueAPIController : ApiController
    {
        public string GetMessage(String Username, String Password, String Title, String Details)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                Issue i = new Issue();
                i.StaffID = s.StaffID;
                i.Title = Title;
                i.Details = Details;
                i.SiteID = s.SiteID;
                i.Status = "Pending";
                i.ReplyMsg ="";
                i.ReportingTime = DateTime.Today;
                i.ReplyTime =DateTime.Today;
                i.Insert();
                return "Issue Reported";
            }
            else
            {
                return "Issue not reported";
            }
        }
        [HttpGet]
        public List<Issue> AllIssue(String Username,String Password)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                Issue i = new Issue();
                i.StaffID = s.StaffID;
                List<Issue> list = i.SelectIssuesByStaffID();
                return list;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        public List<Issue> ReplyIssue(String Username, String Password,int IssueID)
        {
            Staff s = new Staff();
            s.Username = Username;
            s.Password = Password;
            if (s.AuthenticateStaff())
            {
                Issue i = new Issue();
                i.IssueID = IssueID;
                List<Issue> list = i.SelectIssuesByIssueID();
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
