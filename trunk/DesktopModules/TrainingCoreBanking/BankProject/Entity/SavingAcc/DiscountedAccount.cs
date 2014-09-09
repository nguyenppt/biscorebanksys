using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.SavingAcc
{
    public class DiscountedAccount
    {       
        public string RefId { get; set; }
        public string LDId { get; set; }
        public string Status { get; set; }
        public string WorkingAccId { get; set; }
        public string CustomerId { get; set; }
        public decimal? AmmountLCY { get; set; }
        public decimal? AmountFCY { get; set; }
        public string Narrative { get; set; }
        public decimal? DealRate { get; set; }
        public string PaymentCCY { get; set; }
        public string ForTeller { get; set; }
        public string DebitAccount { get; set; }
        public string TDCustomerId { get; set; }
        public string TDJoinHolderId { get; set; }
        public string TDProductLineId { get; set; }
        public string TDCurrency { get; set; }
        public decimal? TDAmmount { get; set; }
        public DateTime? TDValueDate { get; set; }
        public string TDBusDayDate { get; set; }
        public string TDTerm { get; set; }
        public DateTime? TDFinalMatDate { get; set; }
        public decimal? TDInterestRate { get; set; }
        public decimal? TDTotalIntamt { get; set; }
        public string TDWorkingAccountId { get; set; }
        public string TDWorkingAccountName { get; set; }
        public string TDAccountOfficerId { get; set; }
        public string DPDrAccountId { get; set; }
        public string DPDrAccountName { get; set; }
        public decimal? DPAmountLCY { get; set; }
        public decimal? DPAmountFCY { get; set; }
        public string DPNarrative { get; set; }
        public string DPPaymentCcy { get; set; }
        public string DPForTeller { get; set; }
        public string DPCreditAccount { get; set; }
        public decimal? DPExchRate { get; set; }
        public string CloseStatus { get; set; }
        public DateTime? CloseDate { get; set; }
        public decimal? CloseInterest { get; set; }
        public DateTime? CloseRateVDate { get; set; }
        public decimal? CloseDealRate { get; set; }
        public string CloseTeller { get; set; }
        public string CloseNarrative { get; set; }
        public decimal? CloseAmountLCY { get; set; }
        public decimal? CloseAmountFCY { get; set; }
        public string CloseCurrency { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}