using BankProject.DBContext;
using BankProject.Helper;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

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
            public const string Courier = "ELC.COURIER";
            public const string Other = "ELC.OTHER";
        }
        //public DbSet<BEXPORT_DOCUMETARYCOLLECTIONCHARGES> BEXPORT_DOCUMETARYCOLLECTIONCHARGES { get; set; }
        //public DbSet<BAdvisingAndNegotiationLC> BAdvisingAndNegotiationLCs { get; set; }
        //public DbSet<BAdvisingAndNegotiationLCCharge> BAdvisingAndNegotiationLCCharges { get; set; }
        //public DbSet<BEXPORT_DOCUMENTPROCESSING> BEXPORT_DOCUMENTPROCESSINGs { get; set; }
        //public DbSet<BEXPORT_DOCUMENTPROCESSINGCHARGE> BEXPORT_DOCUMENTPROCESSINGCHARGEs { get; set; }
        //public DbSet<B_AddConfirmInfo> B_AddConfirmInfos { get; set; }
        //public DbSet<B_ExportLCPayment> B_ExportLCPayments { get; set; }
        //public DbSet<B_ExportLCPaymentCharge> B_ExportLCPaymentCharges { get; set; }
        //public DbSet<B_ExportLCPaymentMT202> B_ExportLCPaymentMT202s { get; set; }
       // public DbSet<B_ExportLCPaymentMT756> B_ExportLCPaymentMT756s { get; set; }
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
            //Lấy thông tin chi tiết của amend theo code hoặc lấy amend cuối cùng theo code
            Code = Code.Trim().ToUpper();
            if (Code.IndexOf(".") > 0)
                return BEXPORT_LC_AMEND.Where(p => p.AmendNo.Trim().Equals(Code)).FirstOrDefault();
            
            return BEXPORT_LC_AMEND.Where(p => p.AmendNo.StartsWith(Code)).OrderByDescending(p => p.NumberOfAmendment).FirstOrDefault();
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
            public const string Service = "EC.AMEND";
            public const string Commission = "EC.CABLE";
            public const string Courier = "EC.COURIER";
            public const string Other = "EC.OTHER";
        }
        public BEXPORT_LC_DOCS_PROCESSING findExportLCDoc(string Code)
        {
            //Code có dạng TFxxx hoặc TFxxx.No hoặc TFxxx.No.AmendNo
            Code = Code.Trim().ToUpper();
            int i = Code.IndexOf(".");
            if (i < 0)//TFxxx
                return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.DocCode.StartsWith(Code)).OrderByDescending(p => p.DocCode).FirstOrDefault();

            if (Code.IndexOf(".", i + 1) > 0)//TFxxx.No.AmendNo
                return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.AmendNo.Equals(Code)).FirstOrDefault();

            //TFxxx.No
            return BEXPORT_LC_DOCS_PROCESSING.Where(p => (p.DocCode.Equals(Code) && p.ActiveRecordFlag.Equals("Yes"))).FirstOrDefault();
        }
        public BEXPORT_LC_DOCS_PROCESSING findExportLCDocLastestAmend(string Code)
        {
            return BEXPORT_LC_DOCS_PROCESSING.Where(p => p.DocCode.Equals(Code)).OrderByDescending(p => p.AmendNo).FirstOrDefault();
        }
    }
}