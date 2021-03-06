﻿using BankProject.DBContext;
using BankProject.Helper;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace BankProject.Model.Reports
{
    public class MauBiaHsLc
    {
        public string Ref { get; set; }
        public string LCCode { get; set; }
        public string DateOfIssue { get; set; }
        public string Beneficiary { get; set; }
        public string Applicant { get; set; }
        public string IssuingBank { get; set; }
        public string Tenor { get; set; }
        public string AdvisingBank { get; set; }
        public string Amount { get; set; }//Amount and Currency
        public string LatestDateOfShipment { get; set; }
        public string DateOfExpiry { get; set; }
        public string Transhipment { get; set; }
        public string PartialShipment { get; set; }
        public string Commodity { get; set; }
        public string PortOfLoading { get; set; }
        public string PeriodForPresentation { get; set; }
        public string PortOfDischarge { get; set; }         
    }
    public class MauThongBaoVaTuChinhLc
    {
        public string DateCreate { get; set; }
        public string Ref { get; set; }
        public string Beneficiary { get; set; }//Name & Address
        public string LCCode { get; set; }
        public string DateOfIssue { get; set; }
        public string DateOfExpiry { get; set; }
        public string IssuingBank { get; set; }
        public string Amount { get; set; }//Amount and Currency
        public string Applicant { get; set; }        
        public string NumberOfAmendment { get; set; }
    }
    public class VAT
    {
        public string UserName { get; set; }
        public string VATNo { get; set; }
        public string TransCode { get; set; }
        //
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string IdentityNo { get; set; }
        //
        public string DebitAccount { get; set; }        
        public string ChargeRemarks { get; set; }
        //
        public string ChargeType1 { get; set; }
        public string ChargeAmount1 { get; set; }

        public string ChargeType2 { get; set; }
        public string ChargeAmount2 { get; set; }

        public string ChargeType3 { get; set; }
        public string ChargeAmount3 { get; set; }
        //
        public string TotalTaxText { get; set; }
        public string TotalTaxAmount { get; set; }
        //
        public string TotalChargeAmount { get; set; }
        public string TotalChargeAmountWord { get; set; }
    }
    //
    public class PhieuXuatNgoaiBang
    {
        public int Day
        {
            get {
                return (DateTime.Now).Day;
            }
        }
        public int Month
        {
            get {
                return (DateTime.Now).Month;
            }
        }
        public int Year
        {
            get {
                return (DateTime.Now).Year;
            }
        }
        public string NormalLCCode { get; set; }
        public string DocCode { get; set; }
        public string CustomerName { get; set; }
        public string CurrentUserLogin { get; set; }
        public string ApplicantName { get; set; }
        public string IdentityNo { get; set; }
        public string ApplicantAddr1 { get; set; }
        public string ApplicantAddr2 { get; set; }
        public string ApplicantAddr3 { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        //public string NormalLCCode { get; set; }
        public string S_Amount
        {
            get {
                return Utils.CurrencyFormat(Amount, Currency);
            }
        }
        public string SoTienVietBangChu
        {
            get {
                return Utils.ReadNumber(Currency, Amount);
            }
        }
    }
    public class CoverNhoThu
    {
        public string CurrentDate { get; set; }
        public string CollectionNo { get; set; }
        public string BeneficiaryNo { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string ApplicantNo { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public string LCCode { get; set; }
        public string DraweeNo { get; set; }
        public string DraweeName { get; set; }
        public string DateExpirity { get; set; }
        public double DocAmount1 { get; set; }
        public string DocCurrency1 { get; set; }
        public string S_DocAmount1
        {
            get {
                return Utils.CurrencyFormat(DocAmount1, DocCurrency1);
            }
        }
        public string AdCurrency { get; set; }
        public double TotalAmount { get; set; }
        public string S_TotalAmount {
            get {
                return Utils.CurrencyFormat(TotalAmount, AdCurrency);
            }
        }
        public string DocsCode1 { get; set; }
        public string DocsCode2 { get; set; }
        public string DocsCode3 { get; set; }
        public string OtherDocs1 { get; set; }
        public string OtherDocs2{ get; set; }
        public string OtherDocs3 { get; set; }
    }
    public class ThuThongBao
    {
        public string Date { get; set; }
        public string BeneficiaryNo { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
        public string NormalLCCode { get; set; }
        public string ReceivingBank { get; set; }
        public string DateIssue { get; set; }
        public string DateExpiry { get; set; }
        public double Amount { get; set; }
        public string ApplicantNo { get; set; }
        public string Currency { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public string S_Amount {
            get {
                return Utils.CurrencyFormat(Amount, Currency);
            }
        }
    }
    public class PhieuThu
    {
        public string VATNo { get; set; }
        public string CustomerName { get; set; }
        public string DocCollectCode { get; set; }
        public string CustomerAddress { get; set; }
        public string UserNameLogin { get; set; }
        public string IdentityNo { get; set; }
        public string ChargeAcct { get; set; }
        public string CustomerID { get; set; }
        public string Remarks { get; set; }
        public string Cot9_1Name { get; set; }
        public string Cot9_2Name { get; set; }
        public string Cot9_3Name { get; set; }
        public double Amount1 { get; set; }//so tien cho tab charge 1
        public double Amount2 { get; set; }// so tien cho tab charge 2
        public double Amount3 { get; set; }//so tien cho tab charge 3
        public string Currency1 { get; set; }//loai tien cho tab charge 1
        public string Currency2 { get; set; }//loai tien cho tab charge 2
        public string Currency3 { get; set; }//loai tien cho tab charge 3
        public string MCurrency { get; set; }//loai tien cho so TF
        public string S_Amount1 {
            get {
                return Utils.CurrencyFormat(Amount1, Currency1);
            }
        }
        public string S_Amount2
        {
            get
            {
                return Utils.CurrencyFormat(Amount2, Currency2);
            }
        }
        public string S_Amount3
        {
            get
            {
                return Utils.CurrencyFormat(Amount3, Currency3);
            }
        }
        public string PL1 { get; set; }//PL cho tabcharge 1
        public string PL2 { get; set; }//PL cho tabcharge 2
        public string PL3 { get; set; }//PL cho tabcharge 3
        public string Cot9_1
        {
            get {
                return S_Amount1 + " " + Currency1 + " " + PL1 + " C";
            }
        }
        public string Cot9_2
        {
            get
            {
                return S_Amount2 + " " + Currency2 + " " + PL2 + " C";
            }
        }
        public string Cot9_3
        {
            get
            {
                return S_Amount3 + " " + Currency3 + " " + PL3 + " C";
            }
        }
        public double VAT
        {
            get {
                return (Amount1 + Amount2 + Amount3) * 10 / 100;
            }
        }
        public double TienThanhToan
        {
            get {
                return Amount1 + Amount2 + Amount3+VAT;
            }
        }
        public string TongSoTienThanhToan
        {
            get {
                return Utils.CurrencyFormat(TienThanhToan, MCurrency);
            }
        }
        public string SoTienVietBangChu
        {
            get {
                return Utils.ReadNumber(MCurrency, TienThanhToan);
            }
        }
    }

    public class CoverProcessing
    {
        public string DocCode { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddr1 { get; set; }
        public string BeneficiaryAddr2 { get; set; }
        public string BeneficiaryAddr3 { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddr1 { get; set; }
        public string ApplicantAddr2 { get; set; }
        public string ApplicantAddr3 { get; set; }
        public string IssuingBankNo { get; set; }
        public string IssuingBankName { get; set; }
        public string IssuingBankAddr1 { get; set; }
        public string IssuingBankAddr2 { get; set; }
        public string IssuingBankAddr3 { get; set; }
        public string NostroAgentBankNo { get; set; }
        public string NostroAgentBankName { get; set; }
        public string NostroAgentBankAddr1 { get; set; }
        public string NostroAgentBankAddr2 { get; set; }
        public string NostroAgentBankAddr3 { get; set; }
        public string ReceivingBankName { get; set; }
        public string ReceivingBankAddr1 { get; set; }
        public string ReceivingBankAddr2 { get; set; }
        public string ReceivingBankAddr3 { get; set; }
        public string DocumentaryCreditNo { get; set; }
        public string Commodity { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string DocumentReceivedDate { get; set; }
        public string ProccessingDate { get; set; }
        public string Tenor { get; set; }
        public string InvoiceNo { get; set; }
        public string DocsCode1 { get; set; }
        public string NoOfOriginals1 { get; set; }
        public string NoOfCopies1 { get; set; }
        public string DocsCode2 { get; set; }
        public string NoOfOriginals2 { get; set; }
        public string NoOfCopies2 { get; set; }
        public string DocsCode3 { get; set; }
        public string NoOfOriginals3 { get; set; }
        public string NoOfCopies3 { get; set; }
        public string Remark { get; set; }
        public string SettlementInstruction { get; set; }
        public string WaiveCharges { get; set; }
        public string ChargeRemarks { get; set; }
        public string VATNo { get; set; }
        public string PaymentFull { get; set; }
        public string Status { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdateDate { get; set; }
        public string AuthorizedBy { get; set; }
        public string AuthorizedDate { get; set; }
        public string AmendStatus { get; set; }
        public string AmendBy { get; set; }
        public string AmendDate { get; set; }
        public string AmendNo { get; set; }
        public string AmendNoOriginal { get; set; }
        public string ActiveRecordFlag { get; set; }
        public string RejectStatus { get; set; }
        public string RejectDate { get; set; }
        public string AcceptStatus { get; set; }
        public string AcceptDate { get; set; }
        public string PaymentAmount { get; set; }
        public string OtherDocs1 { get; set; }
        public string OtherDocs2 { get; set; }
        public string OtherDocs3 { get; set; }
        public string BeneficiaryNo { get; set; }
        public string OtherDocs4 { get; set; }
        public string OtherDocs5 { get; set; }
        public string OtherDocs6 { get; set; }
        public string OtherDocs7 { get; set; }
        public string OtherDocs8 { get; set; }
        public string OtherDocs9 { get; set; }
        public string OtherDocs10 { get; set; }
        public string AcceptanceDate { get; set; }
        public string MaturityDate { get; set; }
        public string AcceptRemarks { get; set; }
    }
}