using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BankProject.DataProvider
{
    public class KhanhND
    {
        static SqlDataProvider sqldata = new SqlDataProvider();
        public static DataSet B_BDYNAMICCONTROLS_Update(string DataControlID, string ModuleID, string ControlID, string SubControlID, string Datavalue, string DataKey)
        {
            return sqldata.ndkExecuteDataset("B_BDYNAMICCONTROLS_Update", DataControlID, ModuleID, ControlID, SubControlID, Datavalue, DataKey);
        }
        public static DataSet B_BDYNAMICCONTROLS_GetControls(string ModuleID, string ControlID, string DataKey)
        {
            return sqldata.ndkExecuteDataset("B_BDYNAMICCONTROLS_GetControls", ModuleID, ControlID, DataKey);
        }
        public static void B_BDYNAMICCONTROLS_Del(string DataControlID)
        {
            sqldata.ndkExecuteNonQuery("B_BDYNAMICCONTROLS_Del", DataControlID);
        }
        public static DataSet B_BCUSTOMERS_GetAllForVVC(string SourceTable)
        {
            return sqldata.ndkExecuteDataset("B_RADCOMBOBOX_GetDataSource", SourceTable);
        }
        public static void B_BDYNAMICCONTROLS_DelByTab(string ModuleID, string ControlID, string DataKey)
        {
            sqldata.ndkExecuteNonQuery("B_BDYNAMICCONTROLS_DelByTab", ModuleID, ControlID,DataKey);
        }
        public static void B_BDYNAMICCONTROLS_UpdateByTab(string ModuleID, string ControlID, string DataKey, string DataKeyCu)
        {
            sqldata.ndkExecuteNonQuery("B_BDYNAMICCONTROLS_UpdateByTab", ModuleID, ControlID, DataKey, DataKeyCu);
        }
        public static void B_BNORMAILLC_UpdateStatus(string Status, string NormalLCCode)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLC_UpdateStatus", Status, NormalLCCode);
        }
        public static void B_BNORMAILLCPROVITIONTRANSFER_UpdateStatus(string Status, string NormalLCCode)
        {
            sqldata.ndkExecuteNonQuery("B_BNORMAILLCPROVITIONTRANSFER_UpdateStatus", Status, NormalLCCode);
        }
        public static DataSet B_BNORMAILLC_Print(string NormalLCCode)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLC_Print", NormalLCCode);
        }
        public static DataSet B_BNORMAILLC_Search(string TbLCCode, string AplicantID)
        {
            return sqldata.ndkExecuteDataset("B_BNORMAILLC_Search", TbLCCode, AplicantID);
        }
        public static DataSet B_BDEBITACCOUNTS_GetByCurrency(string Currency)
        {
            return sqldata.ndkExecuteDataset("B_BDEBITACCOUNTS_GetByCurrency", Currency);
        }
        public static DataSet B_BCUSTOMERS_GetByDepositCode(string DepositCode)
        {
            return sqldata.ndkExecuteDataset("B_BCUSTOMERS_GetByDepositCode", DepositCode);
        }
    }
}