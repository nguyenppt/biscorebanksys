using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace BankProject.DataProvider
{
    public class DataTam
    {
        private static string ConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString;
        }
        public static DataSet B_ISSURLC_GetNewID()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_ISSURLC_GetNewID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CVID", cvid);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.Fill(dsInfo);

                    cmd.Dispose();
                    conn.Close();

                    return dsInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }
        public static DataSet B_BDEPOSITACCTS_GetNewID()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BDEPOSITACCTS_GetNewID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CVID", cvid);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.Fill(dsInfo);

                    cmd.Dispose();
                    conn.Close();

                    return dsInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }
        public static DataSet B_BCUSTOMERS_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BCUSTOMERS_GetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CVID", cvid);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.Fill(dsInfo);

                    cmd.Dispose();
                    conn.Close();

                    return dsInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }
        public static DataSet B_BLCTYPES_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BLCTYPES_GetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@CVID", cvid);
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.Fill(dsInfo);

                    cmd.Dispose();
                    conn.Close();

                    return dsInfo;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }
    }
}