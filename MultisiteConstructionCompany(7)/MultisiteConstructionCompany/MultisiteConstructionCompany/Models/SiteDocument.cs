using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class SiteDocument
    {
        public int SiteDocumentID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public string Status { get; set; }
        public int SiteID { get; set; }

        public int Insert()
        {
            String query = "Insert into SiteDocument values(@DocumentName,@DocumentPath,@Status,@SiteID)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@DocumentName", this.DocumentName));
            lstprms.Add(new SqlParameter("@DocumentPath", this.DocumentPath));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update SiteDocument set DocumentName=@DocumentName,DocumentPath=@DocumentPath,Status=@Status,SiteID=@SiteID where SiteDocumentID=@SiteDocumentID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteDocumentID", this.SiteDocumentID));
            lstprms.Add(new SqlParameter("@DocumentName", this.DocumentName));
            lstprms.Add(new SqlParameter("@DocumentPath", this.DocumentPath));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from SiteDocument where SiteDocumentID=@SiteDocumentID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteDocumentID", this.SiteDocumentID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            String query = "Select * from SiteDocument where SiteDocumentID=@SiteDocumentID";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteDocumentID", this.SiteDocumentID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.SiteDocumentID = Convert.ToInt32(dt.Rows[0]["SiteDocumentID"]);
                this.DocumentName = dt.Rows[0]["DocumentName"].ToString();
                this.DocumentPath = dt.Rows[0]["DocumentPath"].ToString();
                this.Status = dt.Rows[0]["Status"].ToString();
                this.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);

                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectAll()
        {
            String query = "Select *,Site.Name AS SName from SiteDocument,Site where SiteDocument.SiteID=Site.SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public DataTable getDocumentByID()
        {
            String query = "Select SiteDocument.*,Site.Name AS SName from SiteDocument INNER JOIN Site ON SiteDocument.SiteID=Site.SiteID WHERE SiteDocument.SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public List<SiteDocument> getDocumentBySiteID()
        {
            String query = "Select * from SiteDocument where SiteID=@SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SiteID", this.SiteID));
            List<SiteDocument> list = new List<SiteDocument>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                SiteDocument s = new SiteDocument();
                s.SiteDocumentID = Convert.ToInt32(dt.Rows[i]["SiteDocumentID"]);
                s.DocumentName = dt.Rows[i]["DocumentName"].ToString();
                s.DocumentPath = dt.Rows[i]["DocumentPath"].ToString();
                s.Status = dt.Rows[i]["Status"].ToString();
                s.SiteID = Convert.ToInt32(dt.Rows[i]["SiteID"]);
                list.Add(s);
            }
            return list;
        }
    }
}