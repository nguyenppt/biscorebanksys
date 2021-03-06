//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankProject.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class BIMPORT_NORMAILLC_MT747
    {
        public long Id { get; set; }
        public string NormalLCCode { get; set; }
        public string GenerateMT747 { get; set; }
        public string ReceivingBank { get; set; }
        public string DocumentaryCreditNumber { get; set; }
        public string ReimbBankType { get; set; }
        public string ReimbBankNo { get; set; }
        public string ReimbBankName { get; set; }
        public string ReimbBankAddr1 { get; set; }
        public string ReimbBankAddr2 { get; set; }
        public string ReimbBankAddr3 { get; set; }
        public Nullable<System.DateTime> DateOriginalAuthorization { get; set; }
        public Nullable<System.DateTime> DateOfExpiry { get; set; }
        public string Currency { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> PercentageCreditAmountTolerance1 { get; set; }
        public Nullable<double> PercentageCreditAmountTolerance2 { get; set; }
        public string MaximumCreditAmount { get; set; }
        public string AdditionalCovered1 { get; set; }
        public string AdditionalCovered2 { get; set; }
        public string AdditionalCovered3 { get; set; }
        public string AdditionalCovered4 { get; set; }
        public string SenderToReceiverInformation1 { get; set; }
        public string SenderToReceiverInformation2 { get; set; }
        public string SenderToReceiverInformation3 { get; set; }
        public string SenderToReceiverInformation4 { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
        public string Narrative3 { get; set; }
        public string Narrative4 { get; set; }
        public string Narrative5 { get; set; }
        public string Narrative6 { get; set; }
    }
}
