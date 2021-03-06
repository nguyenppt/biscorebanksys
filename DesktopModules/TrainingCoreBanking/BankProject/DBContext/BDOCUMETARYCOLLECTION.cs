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
    
    public partial class BDOCUMETARYCOLLECTION
    {
        public long Id { get; set; }
        public string DocCollectCode { get; set; }
        public string CollectionType { get; set; }
        public string RemittingBankNo { get; set; }
        public string RemittingBankAddr { get; set; }
        public string RemittingBankAcct { get; set; }
        public string RemittingBankRef { get; set; }
        public string DraweeType { get; set; }
        public string DraweeCusNo { get; set; }
        public string DraweeAddr1 { get; set; }
        public string DraweeAddr2 { get; set; }
        public string DraweeAddr3 { get; set; }
        public string ReimbDraweeAcct { get; set; }
        public string DrawerType { get; set; }
        public string DrawerCusNo { get; set; }
        public string DrawerAddr { get; set; }
        public string Currency { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<double> Amount_Old { get; set; }
        public Nullable<System.DateTime> DocsReceivedDate { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public string Tenor { get; set; }
        public string Tenor_New { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<System.DateTime> TracerDate { get; set; }
        public Nullable<System.DateTime> TracerDate_New { get; set; }
        public Nullable<int> ReminderDays { get; set; }
        public string Commodity { get; set; }
        public string DocsCode1 { get; set; }
        public Nullable<int> NoOfOriginals1 { get; set; }
        public Nullable<int> NoOfCopies1 { get; set; }
        public string DocsCode2 { get; set; }
        public Nullable<int> NoOfOriginals2 { get; set; }
        public Nullable<int> NoOfCopies2 { get; set; }
        public string OtherDocs { get; set; }
        public string InstructionToCus { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string AuthorizedBy { get; set; }
        public Nullable<System.DateTime> AuthorizedDate { get; set; }
        public string DrawerAddr1 { get; set; }
        public string DrawerAddr2 { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public Nullable<System.DateTime> ContingentExpiryDate { get; set; }
        public string Amend_Status { get; set; }
        public string DrawerCusName { get; set; }
        public string DraweeCusName { get; set; }
        public string AccountOfficer { get; set; }
        public string ExpressNo { get; set; }
        public string InvoiceNo { get; set; }
        public string CancelRemark { get; set; }
        public string RemittingBankAddr2 { get; set; }
        public string RemittingBankAddr3 { get; set; }
        public string Cancel_Status { get; set; }
        public string CancelBy { get; set; }
        public Nullable<System.DateTime> AcceptedDate { get; set; }
        public string AcceptRemarks { get; set; }
        public string Accept_Status { get; set; }
        public string AcceptBy { get; set; }
        public Nullable<System.DateTime> AcceptByDate { get; set; }
        public Nullable<int> PaymentFullFlag { get; set; }
        public Nullable<double> B4_AUT_Amount { get; set; }
        public Nullable<long> PaymentNo { get; set; }
        public string PaymentId { get; set; }
        public string DraftNo { get; set; }
        public string AmendNo { get; set; }
        public string ActiveRecordFlag { get; set; }
        public Nullable<double> OldAmount { get; set; }
        public string RefAmendNo { get; set; }
    }
}
