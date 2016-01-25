using BankProject.DBContext;
using BankProject.Helper;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using bd = BankProject.DataProvider;

namespace BankProject.Model
{
    public class ExportLC : VietVictoryCoreBankingEntities
    {
        public struct Actions
        {
            public const int Register = 242;
            public const int Amend = 235;
            public const int Confirm = 236;
            public const int Cancel = 237;
            public const int Close = 265;
        }
        public struct Charges
        {
            public const string Amendment = "ELC.ADAMEND";
            public const string Advising = "ELC.ADVISE";
            public const string Cancel = "ELC.CANCEL";
            public const string Courier = "ELC.COURIER";
            public const string Other = "ELC.OTHER";
        }
        public DbSet<BEXPORT_DOCUMETARYCOLLECTIONCHARGES> BEXPORT_DOCUMETARYCOLLECTIONCHARGES { get; set; }
        public DbSet<BAdvisingAndNegotiationLC> BAdvisingAndNegotiationLCs { get; set; }
        public DbSet<BAdvisingAndNegotiationLCCharge> BAdvisingAndNegotiationLCCharges { get; set; }
        public DbSet<BEXPORT_DOCUMENTPROCESSING> BEXPORT_DOCUMENTPROCESSINGs { get; set; }
        public DbSet<BEXPORT_DOCUMENTPROCESSINGCHARGE> BEXPORT_DOCUMENTPROCESSINGCHARGEs { get; set; }
        public DbSet<B_AddConfirmInfo> B_AddConfirmInfos { get; set; }
        public DbSet<B_ExportLCPayment> B_ExportLCPayments { get; set; }
        public DbSet<B_ExportLCPaymentCharge> B_ExportLCPaymentCharges { get; set; }
        public DbSet<B_ExportLCPaymentMT202> B_ExportLCPaymentMT202s { get; set; }
        public DbSet<B_ExportLCPaymentMT756> B_ExportLCPaymentMT756s { get; set; }
        //
        public BEXPORT_LC findExportLC(string Code)
        {
            Code = Code.Trim().ToUpper();
            return BEXPORT_LC.Where(p => p.ExportLCCode.Equals(Code)).FirstOrDefault();
        }
        public BIMPORT_NORMAILLC findImportLC(string Code)
        {
            Code = Code.Trim().ToUpper();
            return BIMPORT_NORMAILLC.Where(p => (p.NormalLCCode.Equals(Code) && (p.ActiveRecordFlag == null || p.ActiveRecordFlag.Equals("Yes")))).FirstOrDefault();
        }
        public BEXPORT_LC_AMEND findExportLCAmend(string Code)
        {
            Code = Code.Trim().ToUpper();
            return BEXPORT_LC_AMEND.Where(p => p.AmendNo.Trim().Equals(Code)).FirstOrDefault();
        }
        //
        public string getChargeTypeInfo(string ChargeType, int infoType)
        {
            var cc = BCHARGECODEs.Where(p => p.Code.Equals(ChargeType)).FirstOrDefault();
            if (cc == null) return "";
            switch (infoType)
            {
                case 1:
                    return cc.Name_VN;
                case 2:
                    return cc.PLAccount;
            }

            return "";
        }
        //
        public string getVATNo()
        {
            var dataVAT = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            if (dataVAT != null && dataVAT.Tables.Count > 0)
                return dataVAT.Tables[0].Rows[0]["SoTT"].ToString();

            return null;
        }
        //
        public string getNewId()
        {
            var dataIDs = bd.DataTam.B_ISSURLC_GetNewID();
            if (dataIDs != null && dataIDs.Tables.Count > 0)
                return dataIDs.Tables[0].Rows[0]["Code"].ToString();

            return null;
        }
    }
    //
    public class ExportLCDocProcessing : ExportLC
    {
        public new struct Actions
        {
            public const int Register = 239;
            public const int Register1 = 240;
            public const int Amend = 376;
            public const int Reject = 241;
            public const int Accept = 244;
        }
        public new struct Charges
        {
            public const string Service = "ELC.AMEND";
            public const string Commission = "ELC.HANDLE";
            public const string Courier = "ELC.COURIER";
            public const string Other = "ELC.OTHER";
        }
        //
        public BEXPORT_LC_DOCS_PROCESSING findExportLCDoc(string Code)
        {
            return findExportLCDoc(Code, null);
        }
        public BEXPORT_LC_DOCS_PROCESSING findExportLCDoc(string Code, bool? isAmendNo)
        {
            int i = Code.IndexOf(".");
            if (Code.LastIndexOf(".") != i || (isAmendNo.HasValue && isAmendNo.Value))
                return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.AmendNo.Equals(Code)).FirstOrDefault();

            return BEXPORT_LC_DOCS_PROCESSING.Where(p => (p.DocCode.Contains(Code) && p.ActiveRecordFlag.Equals("Yes"))).FirstOrDefault();
        }
        public BEXPORT_LC_DOCS_PROCESSING findExportLCDocLastestAmend(string DocCode)
        {
            return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.DocCode.StartsWith(DocCode)).OrderByDescending(p => p.AmendNo).FirstOrDefault();
        }
        public BEXPORT_LC_DOCS_PROCESSING findExportLCLastestDoc(string LCCode)
        {
            return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.DocCode.StartsWith(LCCode)).OrderByDescending(p => p.DocCode).FirstOrDefault();
        }
    }
    //
    public class ExportLCDocSettlement : ExportLCDocProcessing
    {
        //
        public BEXPORT_LC_DOCS_SETTLEMENT findExportLCDocSettlement(string PaymentCode)
        {
            return BEXPORT_LC_DOCS_SETTLEMENT.Where(p => p.PaymentCode.Equals(PaymentCode)).FirstOrDefault();
        }
        //
        public BEXPORT_LC_DOCS_SETTLEMENT findExportLCDocSettlementUNA(string DocCode)
        {
            return BEXPORT_LC_DOCS_SETTLEMENT.Where(p => (p.DocsCode.Equals(DocCode) && p.Status.Equals(bd.TransactionStatus.UNA))).FirstOrDefault();
        }
        //
        public BEXPORT_LC_DOCS_SETTLEMENT findExportLCDocSettlementLastest(string DocCode)
        {
            return BEXPORT_LC_DOCS_SETTLEMENT.Where(p => p.DocsCode.Equals(DocCode)).OrderByDescending(p => p.PaymentCode).FirstOrDefault();
        }
    }
}