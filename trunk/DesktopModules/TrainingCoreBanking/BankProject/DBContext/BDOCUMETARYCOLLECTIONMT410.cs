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
    
    public partial class BDOCUMETARYCOLLECTIONMT410
    {
        public long Id { get; set; }
        public string DocCollectCode { get; set; }
        public string GeneralMT410_1 { get; set; }
        public string GeneralMT410_2 { get; set; }
        public string SendingBankTRN { get; set; }
        public string RelatedReference { get; set; }
        public string Currency { get; set; }
        public Nullable<double> Amount { get; set; }
        public string SenderToReceiverInfo1 { get; set; }
        public string SenderToReceiverInfo2 { get; set; }
        public string SenderToReceiverInfo3 { get; set; }
    }
}
