﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BankProject.DataProvider
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PhatEntities : DbContext
    {
        public PhatEntities()
            : base("name=PhatEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BPAYMENTMETHOD> BPAYMENTMETHODs { get; set; }
        public DbSet<BCUSTOMER> BCUSTOMERS { get; set; }
        public DbSet<BSWIFTCODE> BSWIFTCODEs { get; set; }
        public DbSet<BOUTGOINGCOLLECTIONPAYMENT> BOUTGOINGCOLLECTIONPAYMENTs { get; set; }
        public DbSet<BCHARGECODE> BCHARGECODEs { get; set; }
        public DbSet<BOUTGOINGCOLLECTIONPAYMENTCHARGE> BOUTGOINGCOLLECTIONPAYMENTCHARGES { get; set; }
        public DbSet<BOUTGOINGCOLLECTIONPAYMENTMT910> BOUTGOINGCOLLECTIONPAYMENTMT910 { get; set; }
        public DbSet<BDRFROMACCOUNT> BDRFROMACCOUNTs { get; set; }
        public DbSet<BEXPORT_DOCUMETARYCOLLECTION> BEXPORT_DOCUMETARYCOLLECTION { get; set; }
    }
}