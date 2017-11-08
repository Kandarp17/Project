using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class Issue
    {
        public int IssueID { get; set; }
        public int StaffID { get; set; }
        public String Title { get; set;}
        public String Details { get; set;}
        public int SiteID { get; set;}
        public String Status { get; set; }
        public string ReplyMsg { get; set; }
        public DateTime ReportingTime { get; set; }
        public DateTime ReplyTime { get; set; }

        public int Insert()
        {
            String query = "Insert into Issue values(@StaffID,@Title,@Details,@SiteID,@Status,@ReplyMsg,@ReportingTime,@ReplyTime)";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID", this.StaffID));
            lstprms.Add(new SqlParameter("@Title", this.Title));
            lstprms.Add(new SqlParameter("@Details", this.Details));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@ReplyMsg", this.ReplyMsg));
            lstprms.Add(new SqlParameter("@ReportingTime", this.ReportingTime));
            lstprms.Add(new SqlParameter("@ReplyTime", this.ReplyTime));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Issue set Status=@Status,ReplyMsg=@ReplyMsg,ReplyTime=@ReplyTime where IssueID=@IssueID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@IssueID", this.IssueID));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@ReplyMsg", this.ReplyMsg));           
            lstprms.Add(new SqlParameter("@ReplyTime", this.ReplyTime));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Issue where IssueID=@IssueID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@IssueID", this.IssueID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            string query = "Select * from Issue where IssueID=@IssueID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@IssueID", this.IssueID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.IssueID = Convert.ToInt32(dt.Rows[0]["IssueID"]);
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"]);
                this.Title = dt.Rows[0]["Title"].ToString();
                this.Details = dt.Rows[0]["Details"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.ReplyMsg = dt.Rows[0]["ReplyMsg"].ToString();
                this.ReportingTime = Convert.ToDateTime(dt.Rows[0]["ReportingTime"]);
                return true;
            }else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select Issue.*,Staff.Name AS SName,Staff.StaffType,Site.Name AS SiteName from Issue INNER JOIN Staff ON Issue.StaffID=Staff.StaffID INNER JOIN Site ON Issue.SiteID=Site.SiteID ORDER BY IssueID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public List<Issue> SelectIssuesByStaffID()
        {
            String query = "select * from Issue where StaffID=@StaffID ORDER BY IssueID DESC";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StaffID",this.StaffID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<Issue> list = new List<Issue>();
            for(int j = 0; j < dt.Rows.Count; j++)
            {
                Issue i = new Issue();
                i.IssueID = Convert.ToInt32(dt.Rows[j]["IssueID"]);
                i.StaffID = Convert.ToInt32(dt.Rows[j]["StaffID"]);
                i.Title = dt.Rows[j]["Title"].ToString();
                i.Details = dt.Rows[j]["Details"].ToString();
                i.SiteID = Convert.ToInt32(dt.Rows[j]["SiteID"]);
                i.Status = dt.Rows[j]["Status"].ToString();
                i.ReplyMsg = dt.Rows[j]["ReplyMsg"].ToString();
                i.ReportingTime = Convert.ToDateTime(dt.Rows[j]["ReportingTime"]);
                
                list.Add(i);
            }
            return list;
        }

        public List<Issue> SelectIssuesByIssueID()
        {
            String query = "select * from Issue where IssueID=@IssueID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@IssueID", this.IssueID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<Issue> list = new List<Issue>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                Issue i = new Issue();
                i.IssueID = Convert.ToInt32(dt.Rows[j]["IssueID"]);
                i.StaffID = Convert.ToInt32(dt.Rows[j]["StaffID"]);
                i.Title = dt.Rows[j]["Title"].ToString();
                i.Details = dt.Rows[j]["Details"].ToString();
                i.SiteID = Convert.ToInt32(dt.Rows[j]["SiteID"]);
                i.Status = dt.Rows[j]["Status"].ToString();
                i.ReplyMsg = dt.Rows[j]["ReplyMsg"].ToString();
                i.ReportingTime = Convert.ToDateTime(dt.Rows[j]["ReportingTime"]);
                list.Add(i);
            }
            return list;
        }
    }
}