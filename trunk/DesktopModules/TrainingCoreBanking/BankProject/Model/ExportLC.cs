﻿using BankProject.DBContext;
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
        public DbSet<BEXPORT_DOCUMETARYCOLLECTIONCHARGES> BEXPORT_DOCUMETARYCOLLECTIONCHARGES { get; set; }
        public DbSet<BAdvisingAndNegotiationLC> BAdvisingAndNegotiationLCs { get; set; }
        public DbSet<BAdvisingAndNegotiationLCCharge> BAdvisingAndNegotiationLCCharges { get; set; }
        public DbSet<BEXPORT_DOCUMENTPROCESSING> BEXPORT_DOCUMENTPROCESSINGs { get; set; }
        public DbSet<BEXPORT_DOCUMENTPROCESSINGCHARGE> BEXPORT_DOCUMENTPROCESSINGCHARGEs { get; set; }
        public DbSet<B_AddConfirmInfo> B_AddConfirmInfos { get; set; }
        public DbSet<B_ExportLCPayment> B_ExportLCPayments { get; set; }
        public DbSet<B_ExportLCPaymentCharge> B_ExportLCPaymentCharges { get; set; }
        public DbSet<B_ExportLCPaymentMT202> B_ExportLCPaymentMT202s { get; set; }
        public DbSet<B_ExportLCPaymentMT756> B_ExportLCPaymentMT756s { get; set; }
    }
}