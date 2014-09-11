using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.DataProvider
{
    public static class Customer
    {
        public const string SignaturePath = "upload/customer/signature";
        private static SqlDataProvider sqldata = new SqlDataProvider();
        //
        #region Signature
        public static void InsertSignature(string CustomerId, string Signatures, string UserCreate)
        {
            sqldata.ndkExecuteNonQuery("P_CustomerSignatureUpdate", CustomerId, Signatures, UserCreate);
        }

        public static DataTable SignatureList(string CustomerId, string CustomerName)
        {
            return sqldata.ndkExecuteDataset("P_CustomerSignatureList", CustomerId, CustomerName).Tables[0];
        }

        public static DataTable SignatureDetail(string CustomerId)
        {
            DataSet ds = sqldata.ndkExecuteDataset("P_CustomerSignatureDetail", CustomerId);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static void UpdateSignatureStatus(string CustomerId, string Status, string UserUpdate)
        {
            sqldata.ndkExecuteNonQuery("P_CustomerSignatureUpdateStatus", CustomerId, Status, UserUpdate);
        }
        #endregion
        public static DataTable CustomerDetail(string CustomerId)
        {
            DataSet ds = sqldata.ndkExecuteDataset("P_CustomerDetail", CustomerId);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }
    }
}