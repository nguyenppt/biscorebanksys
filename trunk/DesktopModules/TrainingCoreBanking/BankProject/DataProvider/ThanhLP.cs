using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace BankProject.DataProvider
{
    public static class ThanhLP
    {
        private static SqlDataProvider sqldata = new SqlDataProvider();

        public static DataTable B_BPRECLOSURE_MAIN()
        {
            return sqldata.ndkExecuteDataset("B_BPRECLOSURE_MAIN").Tables[0];
        }
        public static DataTable B_BPRECLOSURE_TELLER_TRANS()
        {
            return sqldata.ndkExecuteDataset("B_BPRECLOSURE_TELLER_TRANS").Tables[0];
        }

        public static DataTable B_BPRECLOSURE_AUTHORISE()
        {
            return sqldata.ndkExecuteDataset("B_BPRECLOSURE_AUTHORISE").Tables[0];
        }
        
    }
}