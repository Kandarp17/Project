using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class SiteStatus
    {
        public int SiteID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CreateDate { get; set; }

        public DataTable SelectStartAndCreate()
        {
            string query = "select DATEDIFF(day,@StartDate,@CreateDate) AS SCDiff,SiteID from Site INNER JOIN SiteUpdate ON Site.SiteID = SiteUpdate.SiteID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StartDate",this.StartDate));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}