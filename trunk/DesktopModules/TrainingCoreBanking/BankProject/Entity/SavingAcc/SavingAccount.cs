using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity.SavingAcc
{
    public class SavingAccount
    {       
        public string RefId { get; set; }
        public string Status { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AccCategory { get; set; }
        public string AccTitle { get; set; }
        public string  ShortTitle { get; set; }
        public string Currency { get; set; }
        public string ProductLineId { get; set; }
        public string JointACHolderId { get; set; }
        public string JointACHolderName { get; set; }
        public string RelationshipId { get; set; }
        public string RelationshipName { get; set; }
        public string Note { get; set; }
        public string AccountOfferCode { get; set; }
        public string AZProductCode { get; set; }
        public decimal AZPrincipal { get; set; }
        public DateTime? AZValueDate { get; set; }
        public string AZTerm { get; set; }
        public DateTime? AZOriginalMaturityDate { get; set; }
        public DateTime? AZMaturityDate { get; set; }
        public DateTime? AZPreMaturityDate { get; set; }
        public decimal AZInterestRate { get; set; }
        public string AZWorkingAccount { get; set; }
        public string AZMaturityInstr { get; set; }        
        public string TTNo { get; set; }
        public string TTAccNo { get; set; }
        public string TTCurrency { get; set; }
        public string TTForTeller { get; set; }
        public string TTDebitAccount { get; set; }
        public string TTNarative { get; set; }
        public decimal? TTDealRate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CloseStatus { get; set; }
        public DateTime? CloseValueDate { get; set; }
        public decimal? CloseAmoutLCY { get; set; }
        public decimal? CloseAmountFCY { get; set; }
        public string CloseNarative { get; set; }
        public string CloseForTeller { get; set; }
        public string CloseCreditAccount { get; set; }
    }
}