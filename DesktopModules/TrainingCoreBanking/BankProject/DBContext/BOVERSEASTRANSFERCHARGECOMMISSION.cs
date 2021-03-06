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
    
    public partial class BOVERSEASTRANSFERCHARGECOMMISSION
    {
        public long Id { get; set; }
        public string OverseasTransferCode { get; set; }
        public string ChargeAcct { get; set; }
        public string DisplayChargesCom { get; set; }
        public string CommissionCode { get; set; }
        public string CommissionType { get; set; }
        public Nullable<double> CommissionAmount { get; set; }
        public string CommissionFor { get; set; }
        public string ChargeCode { get; set; }
        public string ChargeType { get; set; }
        public Nullable<double> ChargeAmount { get; set; }
        public string ChargeFor { get; set; }
        public string DetailOfCharges { get; set; }
        public string VATNo { get; set; }
        public string AddRemarks1 { get; set; }
        public string AddRemarks2 { get; set; }
        public string ProfitCenteCust { get; set; }
        public Nullable<double> TotalChargeAmount { get; set; }
        public Nullable<double> TotalTaxAmount { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthorizedDate { get; set; }
        public string CommissionCurrency { get; set; }
        public string ChargeCurrency { get; set; }
    }
}
