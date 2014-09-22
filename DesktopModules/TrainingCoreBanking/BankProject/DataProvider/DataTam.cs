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
            return WebConfigurationManager.ConnectionStrings["VietVictoryCoreBanking"].ConnectionString;
        }
        public static DataSet B_BMENUTOP_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BMENUTOP_GetAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
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
        public static DataSet BCUSTOMERS_INDIVIDUAL_GetbyID(string CustomerID)
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BCUSTOMERS_INDIVIDUAL_GetbyID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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
        public static DataSet B_BCUSTOMERS_GetbyID(string CustomerID)
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BCUSTOMERS_GetbyID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
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
        public static DataSet B_BMACODE_Update(string macode)
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BMACODE_Update", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaCode", macode);
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
        public static DataSet ProvisionTransfer_GetNewID()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("ProvisionTransfer_GetNewID", conn);
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
        public static DataSet B_BCOMMODITY_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BCOMMODITY_GetAll", conn);
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

        public static DataSet BRELATIONCODE_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BRELATIONCODE_GetAll", conn);
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

        public static DataSet BCATEGORY_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BCATEGORY_GetAll", conn);
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
        public static DataSet BCATEGORY_GetAll_2()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BCATEGORY_GetAll_2", conn);
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

        public static DataSet B_BDEPOSITACCTS_GetArrearID()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BDEPOSITACCTS_GetArrearID", conn);
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

        //Create : ThanhLP 05/07/2014
        public static DataSet B_BPRODUCTLINE_GetByType(string type)
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BPRODUCTLINE_GetByType", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Type", type);
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

        public static DataSet B_BPRODUCTLINE_GetAll()
        {
            DataSet dsInfo = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("B_BPRODUCTLINE_GetAll", conn);
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