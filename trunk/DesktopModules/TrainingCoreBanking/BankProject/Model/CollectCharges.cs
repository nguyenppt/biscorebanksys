using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Model.CollectCharges.Reports
{
    public class PhieuChuyenKhoan : BankProject.DBContext.B_CollectCharges
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        //
        /*public string TransCode { get; set; }
        public string CreditAccount { get; set; }
        public string CreditCurrency { get; set; }
        public Nullable<double> CreditAmount { get; set; }*/
        public string DebitAcctName { get; set; }
        public string DebitAmountWord { get; set; }
        //
        public string CreditAccountName { get; set; }
        public string CreditAmountWord { get; set; }
    }
    //
    public class MT103 : BankProject.DBContext.B_CollectCharges_MT103
    {
        public string CreditAccount { get; set; }
        public string CreditAccountName { get; set; }
        public string CreditAccountAddr1 { get; set; }
        public string CreditAccountAddr2 { get; set; }
        //
        public string IntermediaryInstitutionCity { get; set; }
        public string IntermediaryInstitutionCountry { get; set; }
        //
        public string AccountWithInstitutionCity { get; set; }
        public string AccountWithInstitutionCountry { get; set; }
    }
    //
    public class VAT : BankProject.DBContext.B_CollectCharges_ChargeCommission
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string IdentityNo { get; set; }
        public string UserName { get; set; }
        public string ChargeRemarks { get; set; }
        //
        public string TotalChargeAmountWord { get; set; }
    }
}