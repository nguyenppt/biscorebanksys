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
    
    public partial class B_ImportLCPaymentMT756
    {
        public long PaymentId { get; set; }
        public string PaymentCode { get; set; }
        public string General { get; set; }
        public string SendingBankTRN { get; set; }
        public string RelatedReference { get; set; }
        public Nullable<double> AmountCollected { get; set; }
        public Nullable<System.DateTime> ValueDate { get; set; }
        public string Currency { get; set; }
        public Nullable<double> Amount { get; set; }
        public string SenderCorrespondent1 { get; set; }
        public string SenderCorrespondent2 { get; set; }
        public string ReceiverCorrespondent1 { get; set; }
        public string ReceiverCorrespondent2 { get; set; }
        public string DetailOfCharges1 { get; set; }
        public string DetailOfCharges2 { get; set; }
        public string ReceiverCorrespondentType { get; set; }
        public string ReceiverCorrespondentNo { get; set; }
        public string ReceiverCorrespondentName { get; set; }
        public string ReceiverCorrespondentAddr1 { get; set; }
        public string ReceiverCorrespondentAddr2 { get; set; }
        public string ReceiverCorrespondentAddr3 { get; set; }
        public string SenderCorrespondentType { get; set; }
        public string SenderCorrespondentNo { get; set; }
        public string SenderCorrespondentName { get; set; }
        public string SenderCorrespondentAddr1 { get; set; }
        public string SenderCorrespondentAddr2 { get; set; }
        public string SenderCorrespondentAddr3 { get; set; }
        public string SenderToReceiverInformation1 { get; set; }
        public string SenderToReceiverInformation2 { get; set; }
        public string SenderToReceiverInformation3 { get; set; }
        public string DetailOfCharges3 { get; set; }
    }
}
