using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Dapper;
using BankProject.Entity.LCExport;

namespace BankProject.DataProvider
{
    public class ExportLCDao
    {
        #region "SQL statement"

        private readonly string QUERY_GET_ADV_NEGO_MAIN_BY_ID = @"SELECT * FROM [BEXPORT_LC_ADV_NEGO] WHERE [ExportLCId] = @Id";

        #endregion
        private SqlDataProvider DataProvider
        {
            get { return new SqlDataProvider(); }
        }

        public AdvAndNegoMain GetAdvAndNegoMainById(int Id)
        {
            using (var conn = new SqlConnection(DataProvider.ConnectionString))
            {
                return conn.Query<AdvAndNegoMain>(QUERY_GET_ADV_NEGO_MAIN_BY_ID, new { Id = Id }).FirstOrDefault();
            }
        }
    }
}