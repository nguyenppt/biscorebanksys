using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace BankProject
{
    public class LCDataProvider
    {
        string _connectionString = WebConfigurationManager.ConnectionStrings["VietVictoryCoreBanking"].ConnectionString;

        public DataSet ExecuteDataset(string sql, params object[] sqlParams)
        {
            return (DataSet)SqlHelper.ExecuteDataset(_connectionString, sql, sqlParams);
        }

        public void ExecuteNonQuery(string sql, params object[] sqlParams)
        {
            SqlHelper.ExecuteNonQuery(_connectionString, sql, sqlParams);
        }

        public void ExecuteInsert(DataSet dataSet, string tableName, SqlCommand command)
        {
            SqlHelper.UpdateDataset(command, null, null, dataSet, tableName);
        }

        public void InsertChargeDetail(
            string NormalLCCode
           ,string WaiveCharges
           ,string Chargecode
           ,string ChargePeriod
           ,string ChargeAcct
           ,string ChargeCcy
           ,string ExchRate
           ,string ChargeAmt
           ,string PartyCharged
           ,string OmortCharges
           ,string AmtInLocalCCY
           ,string AmtDRfromAcct
           ,string ChargeStatus)
        {
            this.ExecuteNonQuery(
                "sp_LC_ChargeDetail_Insert",
                NormalLCCode
               ,WaiveCharges
               ,Chargecode
               ,ChargePeriod
               ,ChargeAcct
               ,ChargeCcy
               ,ExchRate
               ,ChargeAmt
               ,PartyCharged
               ,OmortCharges
               ,AmtInLocalCCY
               ,AmtDRfromAcct
               ,ChargeStatus);
        }

        public void UpdateChargeMaster(
	        string NormalLCCode,
            string ChargeRemarks,
            string ChargeVATNo,
            string ChargeTaxCode,
            string ChargeTaxCcy,
            string ChargeTaxAmt,
            string ChargeTaxinLCCYAmt,
            string ChargeTaxDate
        )
        {
            this.ExecuteNonQuery(
                "sp_LC_ChargeMaster_Update",
	            NormalLCCode,
                ChargeRemarks,
                ChargeVATNo,
                ChargeTaxCode,
                ChargeTaxCcy,
                ChargeTaxAmt,
                ChargeTaxinLCCYAmt,
                ChargeTaxDate
            );
        }

        public DataSet GetChargeMaster(string lcCode)
        {
            return this.ExecuteDataset("select * from V_LC_ChargeMaster where NormalLCCode=@NormalLCCode", lcCode);
        }

        public DataSet GetChargeDetail(string lcCode)
        {
            return this.ExecuteDataset("select * from V_LC_ChargeDetail where NormalLCCode=@NormalLCCode", lcCode);
        }
    }
}

