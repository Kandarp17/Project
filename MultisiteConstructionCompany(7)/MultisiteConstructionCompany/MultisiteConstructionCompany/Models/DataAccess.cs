using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MultisiteConstructionCompany.Models
{
    public class DataAccess
    {
        public static int ModifyData(String query,List<SqlParameter> lstprms)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\patel_000\\Desktop\\Project\\MultisiteConstructionCompany(7)\\MultisiteConstructionCompany\\MultisiteConstructionCompany\\App_Data\\CompanyDB.mdf;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            for(int i = 0; i < lstprms.Count; i++)
            {
                cmd.Parameters.Add(lstprms[i]);
            }
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();

            return x;
        }
        public static DataTable SelectData(String query,List<SqlParameter> lstprms)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\patel_000\\Desktop\\Project\\MultisiteConstructionCompany(7)\\MultisiteConstructionCompany\\MultisiteConstructionCompany\\App_Data\\CompanyDB.mdf;Integrated Security=True";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            for(int i = 0; i < lstprms.Count; i++)
            {
                cmd.Parameters.Add(lstprms[i]);
            }
            SqlDataAdapter ad = new SqlDataAdapter();
            ad.SelectCommand = cmd;
            DataTable dt = new DataTable();
            con.Open();
            ad.Fill(dt);
            con.Close();
            return dt;
        }
    }
}