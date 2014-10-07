using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Common;
using Telerik.Web.UI;
using BankProject.DBContext;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.DataProvider;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;
using BankProject.Helper;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class AdvisingAndNegotiationLC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        //Gen tam cac tab chi de lai Main can thi bat len va file note pad +: Advising
        private readonly VietVictoryCoreBankingEntities entContext = new VietVictoryCoreBankingEntities();
        private BAdvisingAndNegotiationLC _exportDoc;
        protected const int TabIssueLCAmend = 235;
        protected const int TabIssueLCCancel = 237;
        public enum AdvisingAndNegotiationScreenType
        {
            Register,
            Amend,
            Cancel,
            RegisterCc, 
            Acception
        }
        private string CodeId
        {
            get { return Request.QueryString["LCCode"]; }
        }

        private bool Disable
        {
            get { return Request.QueryString["disable"] == "1"; }
        }
        private AdvisingAndNegotiationScreenType ScreenType
        {
            get
            {
                switch (TabId)
                {
                    case 235:
                        return AdvisingAndNegotiationScreenType.Amend;
                    case 237:
                        return AdvisingAndNegotiationScreenType.Cancel;
                    //case 227:
                    //    return ExportDocumentaryScreenType.RegisterCc;
                    //case 377:
                    //    return ExportDocumentaryScreenType.Acception;
                    default:
                        return AdvisingAndNegotiationScreenType.Register;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            //BankProject.Controls.Commont.SetTatusFormControls(this.divCharge2.Controls, false);
            if (IsPostBack)
                return;

            LoadToolBar();
            InitDataSource();
            
            if (!string.IsNullOrWhiteSpace(CodeId))
            {
                tbEssurLCCode.Text = CodeId;
                LoadData();
            }
            else if (Request.QueryString["IsAmendment"] != null)
            {
                tbEssurLCCode.Text = Request.QueryString["LCCode"].ToString();
                tbEssurLCCode.Enabled = true;
                LoadData();
            }

            switch (ScreenType)
            {
                case AdvisingAndNegotiationScreenType.Register:

                    InitToolBarForRegister();

                    break;
                case AdvisingAndNegotiationScreenType.Amend:
                    InitToolBarForAmend();
                    //tabCharges.Visible = false;
                    //Charges.Visible = false;
                    break;
                case AdvisingAndNegotiationScreenType.Cancel:
                    InitToolBarForCancel();
                    break;
                //case ExportDocumentaryScreenType.Cancel:
                //    InitToolBarForCancel();

                //    lblAmount_New.Visible = false;
                //    divDocumentaryCollectionCancel.Visible = true;
                //    break;
                //case ExportDocumentaryScreenType.Acception:
                //    InitToolBarForAccept();

                //    lblAmount_New.Visible = false;
                //    divOutgoingCollectionAcception.Visible = true;
                //    break;
            }


            //else
            //{
            //    DataSet ds = DataProvider.DataTam.B_ISSURLC_GetNewID();
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        tbEssurLCCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            //        LoadData();
            //    }
            //}
            //LoadToolBar(false);
            //InitDataSource();
        }
        protected void InitToolBarForCancel()
        {
            if (TabId == TabIssueLCCancel)// Cancel LC
            {
                divCancelLC.Visible = true;
                SetDisableByReview(false);
                LoadToolBar(true);

                tbEssurLCCode.Enabled = true;
                dteCancelDate.Enabled = true;
                dteContingentExpiryDate.Enabled = true;
                txtCancelRemark.Enabled = true;
                //RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                if (_exportDoc != null)
                {
                    if (_exportDoc.AmendStatus!=null)//Authorizing
                    {
                        if (_exportDoc.Status != "AUT")
                        {
                            lblError.Text = "This Documentary was not authorized";
                        }
                    }
                }
            }
        }
        protected void InitToolBarForAmend()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportDoc.Status != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportDoc.AmendStatus == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                    }
                    else if (_exportDoc.CancelRemark == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was canceled";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else // Editing
                {
                    if (_exportDoc.Status != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                        SetDisableByReview(false);
                    }
                    else if (_exportDoc.CancelRemark == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was canceled";
                    }
                    else if (_exportDoc.AmendStatus == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                        SetDisableByReview(false);
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    }

                }
            }
            else
            {

            }
        }
        protected void InitToolBarForRegister()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportDoc.Status == "AUT") // Authorized
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was authorized";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else // Editing
                {
                    if (_exportDoc.Status.ToString() == "AUT") // Authorized
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was authorized";
                        SetDisableByReview(false);
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    }

                }
            }
            else // Creating
            {
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
               // .Text = SQLData.B_BMACODE_GetNewID("EXPORT_DOCUMETARYCOLLECTION", Refix_BMACODE());
            }
        }
        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            if (Request.QueryString["IsDisable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected bool LoadData()
        {
            //load tab main
            if (!String.IsNullOrWhiteSpace(tbEssurLCCode.Text))
            {

                var dt = entContext.BAdvisingAndNegotiationLCs.Where(dr => dr.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault();
                if (TabId == TabIssueLCAmend)
                {
                    if (dt.Status == "UNA")
                    {
                        dt = null;

                        lblError.Text = "This Amend Documentary "+tbEssurLCCode.Text+ " was not authorized ";
                        tbEssurLCCode.Text = "";
                        return false;
                    }
                }
                else if (TabId == TabIssueLCCancel)
                {
                    if (dt.CancelRemark != null)
                    {
                        lblError.Text = "This Documentary was canceled";
                        
                    }
                    else if(dt.AmendStatus!=null)
                    {
                        if (dt.Status != "AUT")
                        {
                            lblError.Text = "This Documentary was not authorized";
                        }
                    }
                }
                if (dt != null)
                {
                    _exportDoc = dt;
                    txtRevivingBank700.Text = dt.ReceivingBank.ToString();
                    tbBaquenceOfTotal.Text = dt.SequenceOfTotal.ToString();
                    comboFormOfDocumentaryCredit.SelectedValue = dt.FormDocumentaryCredit.ToString();
                    lblDocumentaryCreditNumber.Text = dt.DocumentaryCreditNumber.ToString();
                    tbPlaceOfExpiry.Text = dt.PlaceOfExpiry.ToString();
                    comboAvailableRule.SelectedValue = dt.ApplicationRule.ToString();

                    rcbApplicantBankType700.SelectedValue = dt.ApplicantType.ToString();
                    tbApplicantNo700.Text = dt.ApplicantNo.ToString();
                    tbApplicantName700.Text = dt.ApplicantName.ToString();
                    tbApplicantAddr700_1.Text = dt.ApplicantAddr1.ToString();
                    tbApplicantAddr700_2.Text = dt.ApplicantAddr2.ToString();
                    tbApplicantAddr700_3.Text = dt.ApplicantAddr3.ToString();

                    //draw
                    comboDraweeCusType.SelectedValue = dt.DraweeType.ToString();
                    comboDraweeCusNo700.SelectedValue = dt.DraweeNo.ToString();
                    //txtDraweeCusNo.SelectedValue = dt.DraweeNo.ToString();
                    txtDraweeCusName.Text = dt.DraweeName.ToString();
                    txtDraweeAddr1.Text = dt.DraweeAddr1.ToString();
                    txtDraweeAddr2.Text = dt.DraweeAddr2.ToString();
                    txtDraweeAddr3.Text = dt.DraweeAddr3.ToString();


                    //
                    comboBeneficiaryType700.SelectedValue = dt.BeneficiaryType.ToString();
                    txtBeneficiaryNo700.Text = dt.BeneficiaryNo.ToString();
                    txtBeneficiaryName700.Text = dt.BeneficiaryName.ToString();
                    txtBeneficiaryAddr700_1.Text = dt.BeneficiaryAddr1.ToString();
                    txtBeneficiaryAddr700_2.Text = dt.BeneficiaryAddr2.ToString();
                    txtBeneficiaryAddr700_3.Text = dt.BeneficiaryAddr3.ToString();

                    comboCurrency700.SelectedValue = dt.Currency.ToString();
                    numAmount700.Value = (double?)dt.Amount;
                    numPercentCreditAmount1.Value = (double?)dt.PercentageCredit;
                    numPercentCreditAmount2.Value = (double?)dt.AmountTolerance;
                    comboMaximumCreditAmount700.SelectedValue = dt.MaximumCreditAmount.ToString();

                    rcbAvailableWithType.SelectedValue = dt.AvailableWithType.ToString();
                    comboAvailableWithNo.SelectedValue = dt.AvailableWithNo.ToString();
                    tbAvailableWithName.Text = dt.AvailableWithName.ToString();
                    tbAvailableWithAddr1.Text = dt.AvailableWithAddr1.ToString();
                    tbAvailableWithAddr2.Text = dt.AvailableWithAddr2.ToString();
                    tbAvailableWithAddr3.Text = dt.AvailableWithAddr3.ToString();

                    comboAvailableWithBy.SelectedValue = dt.Available_By.ToString();

                    rcbPatialShipment.SelectedValue = dt.PatialShipment.ToString();
                    rcbTranshipment.SelectedValue = dt.Transhipment.ToString();
                    tbPlaceoftakingincharge.Text = dt.PlaceOfTakingInCharge.ToString();
                    tbPortofDischarge.Text = dt.PortOfDischarge.ToString();
                    tbPlaceoffinalindistination.Text = dt.PlaceOfFinalInDistination.ToString();

                    txtEdittor_DescrpofGoods.Content = dt.DescrpGoodsBervices.ToString();
                    txtEdittor_OrderDocs700.Content = dt.DocsRequired.ToString();
                    txtEdittor_AdditionalConditions700.Content = dt.AdditionalConditions.ToString();
                    txtEdittor_Charges700.Content = dt.Charges.ToString();
                    txtEdittor_PeriodforPresentation700.Content = dt.PeriodForPresentation.ToString();
                    rcbConfimationInstructions.SelectedValue = dt.ConfimationInstructions.ToString();
                    txtEdittor_NegotgBank700.Content = dt.InstrToPaygAccptgNegotgBank.ToString();
                    txtEdittor_SendertoReceiverInfomation700.Content = dt.SenderReceiverInfomation.ToString();

                    if ((!String.IsNullOrEmpty(dt.LatesDateOfShipment.ToString())) && (dt.LatesDateOfShipment.ToString().IndexOf("1/1/1900") == -1))
                    {
                        tbLatesDateofShipment.SelectedDate = DateTime.Parse(dt.LatesDateOfShipment.ToString());
                    }
                    if ((!String.IsNullOrEmpty(dt.DateExpiry.ToString())) && (dt.DateExpiry.ToString().IndexOf("1/1/1900") == -1))
                    {
                        dteMT700DateAndPlaceOfExpiry.SelectedDate = DateTime.Parse(dt.DateExpiry.ToString());
                    }
                    if ((!String.IsNullOrEmpty(dt.DateOfIssue.ToString())) && (dt.DateOfIssue.ToString().IndexOf("1/1/1900") == -1))
                    {
                        dteDateOfIssue.SelectedDate = DateTime.Parse(dt.DateOfIssue.ToString());
                    }
                    comboAdviseThroughBankType700.SelectedValue = dt.AdviseThroughBankType.ToString();
                    comboAdviseThroughBankNo700.SelectedValue = dt.AdviseThroughBankNo.ToString();
                    txtAdviseThroughBankName700.Text = dt.AdviseThroughBankName.ToString();
                    txtAdviseThroughBankAddr700_1.Text = dt.AdviseThroughBankAddr1.ToString();
                    txtAdviseThroughBankAddr700_2.Text = dt.AdviseThroughBankAddr2.ToString();
                    txtAdviseThroughBankAddr700_3.Text = dt.AdviseThroughBankAddr3.ToString();

                    comboReimbBankType700.SelectedValue = dt.ReimbBankType.ToString();
                    rcbReimbBankNo700.SelectedValue = dt.ReimbBankNo.ToString();
                    tbReimbBankName700.Text = dt.ReimbBankName.ToString();
                    tbReimbBankAddr700_1.Text = dt.ReimbBankAddr1.ToString();
                    tbReimbBankAddr700_2.Text = dt.ReimbBankAddr2.ToString();
                    tbReimbBankAddr700_3.Text = dt.ReimbBankAddr3.ToString();


                    txtAdditionalAmountsCovered700_1.Text = dt.AdditionalAmountsCovered1.ToString();
                    txtAdditionalAmountsCovered700_2.Text = dt.AdditionalAmountsCovered2.ToString();
                    txtDraftsAt700_1.Text = dt.DraftsAt1.ToString();
                    txtDraftsAt700_2.Text = dt.DraftsAt2.ToString();


                    txtMixedPaymentDetails700_1.Text = dt.MixedPaymentDetails1.ToString();
                    txtMixedPaymentDetails700_2.Text = dt.MixedPaymentDetails2.ToString();
                    txtMixedPaymentDetails700_3.Text = dt.MixedPaymentDetails3.ToString();
                    txtMixedPaymentDetails700_4.Text = dt.MixedPaymentDetails4.ToString();

                    txtDeferredPaymentDetails700_1.Text = dt.DeferredPaymentDetails1.ToString();
                    txtDeferredPaymentDetails700_2.Text = dt.DeferredPaymentDetails2.ToString();
                    txtDeferredPaymentDetails700_3.Text = dt.DeferredPaymentDetails3.ToString();
                    txtDeferredPaymentDetails700_4.Text = dt.DeferredPaymentDetails4.ToString();

                    txtShipmentPeriod700_1.Text = dt.ShipmentPeriod1.ToString();
                    txtShipmentPeriod700_2.Text = dt.ShipmentPeriod2.ToString();
                    txtShipmentPeriod700_3.Text = dt.ShipmentPeriod3.ToString();
                    txtShipmentPeriod700_4.Text = dt.ShipmentPeriod4.ToString();
                    txtShipmentPeriod700_5.Text = dt.ShipmentPeriod3.ToString();
                    txtShipmentPeriod700_6.Text = dt.ShipmentPeriod4.ToString();
                    if (!string.IsNullOrEmpty(dt.CancelDate.ToString()) && dt.CancelDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteCancelDate.SelectedDate = DateTime.Parse(dt.CancelDate.ToString());
                    }

                    if (!string.IsNullOrEmpty(dt.ContingentExpiryDate.ToString()) && dt.ContingentExpiryDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteContingentExpiryDate.SelectedDate = DateTime.Parse(dt.ContingentExpiryDate.ToString());
                    }
                    txtCancelRemark.Text = dt.CancelRemark;
                }
                else
                {
                    if (TabId != TabIssueLCAmend)
                    {
                        //find in BIMPORT_NORMAILLC_MT700 if cannot find, find in BAdvisingAndNegotiationLCs
                        var ds = entContext.BIMPORT_NORMAILLC_MT700.Where(dr => dr.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault();

                        if (ds == null)
                        {
                            txtCancelRemark.Text = string.Empty;
                            txtRevivingBank700.Text = string.Empty;
                            tbBaquenceOfTotal.Text = string.Empty;
                            comboFormOfDocumentaryCredit.SelectedValue = string.Empty;
                            lblDocumentaryCreditNumber.Text = string.Empty;
                            tbPlaceOfExpiry.Text = string.Empty;
                            comboAvailableRule.SelectedValue = "EUCP LASTED VERSION";

                            rcbApplicantBankType700.SelectedValue = string.Empty;
                            tbApplicantNo700.Text = string.Empty;
                            tbApplicantName700.Text = string.Empty;
                            tbApplicantAddr700_1.Text = string.Empty;
                            tbApplicantAddr700_2.Text = string.Empty;
                            tbApplicantAddr700_3.Text = string.Empty;

                            comboBeneficiaryType700.SelectedValue = "D";
                            txtBeneficiaryNo700.Text = string.Empty;
                            txtBeneficiaryName700.Text = string.Empty;
                            txtBeneficiaryAddr700_1.Text = string.Empty;
                            txtBeneficiaryAddr700_2.Text = string.Empty;
                            txtBeneficiaryAddr700_3.Text = string.Empty;

                            comboCurrency700.SelectedValue = string.Empty;
                            numAmount700.Value = 0;
                            numPercentCreditAmount1.Value = 0;
                            numPercentCreditAmount2.Value = 0;
                            comboMaximumCreditAmount700.SelectedValue = string.Empty;

                            rcbAvailableWithType.SelectedValue = string.Empty;
                            rcbAvailableWithType.SelectedValue = string.Empty;
                            tbAvailableWithName.Text = string.Empty;
                            tbAvailableWithAddr1.Text = string.Empty;
                            tbAvailableWithAddr2.Text = string.Empty;
                            tbAvailableWithAddr3.Text = string.Empty;

                            comboAvailableWithBy.SelectedValue = string.Empty;

                            rcbPatialShipment.SelectedValue = string.Empty;
                            rcbTranshipment.SelectedValue = string.Empty;
                            tbPlaceoftakingincharge.Text = string.Empty;
                            tbPortofloading.Text = string.Empty;
                            tbPortofDischarge.Text = string.Empty;
                            tbPlaceoffinalindistination.Text = string.Empty;

                            comboAdviseThroughBankType700.SelectedValue = string.Empty;
                            comboAdviseThroughBankNo700.SelectedValue = string.Empty;
                            txtAdviseThroughBankName700.Text = string.Empty;
                            txtAdviseThroughBankAddr700_1.Text = string.Empty;
                            txtAdviseThroughBankAddr700_2.Text = string.Empty;
                            txtAdviseThroughBankAddr700_3.Text = string.Empty;

                            comboReimbBankType700.SelectedValue = string.Empty;
                            rcbReimbBankNo700.SelectedValue = string.Empty;
                            tbReimbBankName700.Text = string.Empty;
                            tbReimbBankAddr700_1.Text = string.Empty;
                            tbReimbBankAddr700_2.Text = string.Empty;
                            tbReimbBankAddr700_3.Text = string.Empty;

                            txtAdditionalAmountsCovered700_1.Text = string.Empty;
                            txtAdditionalAmountsCovered700_2.Text = string.Empty;
                            txtDraftsAt700_1.Text = string.Empty;
                            txtDraftsAt700_2.Text = string.Empty;

                            txtMixedPaymentDetails700_1.Text = string.Empty;
                            txtMixedPaymentDetails700_2.Text = string.Empty;
                            txtMixedPaymentDetails700_3.Text = string.Empty;
                            txtMixedPaymentDetails700_4.Text = string.Empty;

                            txtDeferredPaymentDetails700_1.Text = string.Empty;
                            txtDeferredPaymentDetails700_2.Text = string.Empty;

                            txtShipmentPeriod700_1.Text = string.Empty;
                            txtShipmentPeriod700_2.Text = string.Empty;
                            txtShipmentPeriod700_3.Text = string.Empty;
                            txtShipmentPeriod700_4.Text = string.Empty;
                            txtShipmentPeriod700_5.Text = string.Empty;
                            txtShipmentPeriod700_6.Text = string.Empty;
                        }
                        else
                        {
                            txtRevivingBank700.Text = ds.ReceivingBank.ToString();
                            tbBaquenceOfTotal.Text = ds.SequenceOfTotal.ToString();
                            comboFormOfDocumentaryCredit.SelectedValue = ds.FormDocumentaryCredit.ToString();
                            lblDocumentaryCreditNumber.Text = ds.DocumentaryCreditNumber.ToString();
                            tbPlaceOfExpiry.Text = ds.PlaceOfExpiry.ToString();
                            comboAvailableRule.SelectedValue = ds.ApplicationRule.ToString();

                            rcbApplicantBankType700.SelectedValue = dt.DraweeType.ToString(); ;
                            tbApplicantNo700.Text = dt.DraweeNo.ToString();
                            tbApplicantName700.Text = dt.DraweeName.ToString();
                            tbApplicantAddr700_1.Text = dt.DraweeAddr1.ToString();
                            tbApplicantAddr700_2.Text = dt.DraweeAddr2.ToString();
                            tbApplicantAddr700_3.Text = dt.DraweeAddr3.ToString();

                            //drawee type load nguoc
                            comboDraweeCusType.SelectedValue = ds.ApplicantType.ToString();
                            //txtDraweeCusNo.Text = ds.ApplicantNo.ToString();
                            txtDraweeCusName.Text = ds.ApplicantName.ToString();
                            txtDraweeAddr1.Text = ds.ApplicantAddr1.ToString();
                            txtDraweeAddr2.Text = ds.ApplicantAddr2.ToString();
                            txtDraweeAddr3.Text = ds.ApplicantAddr3.ToString();
                            //
                            comboBeneficiaryType700.SelectedValue = ds.BeneficiaryType.ToString();
                            txtBeneficiaryNo700.Text = ds.BeneficiaryNo.ToString();
                            txtBeneficiaryName700.Text = ds.BeneficiaryName.ToString();
                            txtBeneficiaryAddr700_1.Text = ds.BeneficiaryAddr1.ToString();
                            txtBeneficiaryAddr700_2.Text = ds.BeneficiaryAddr2.ToString();
                            txtBeneficiaryAddr700_3.Text = ds.BeneficiaryAddr3.ToString();

                            comboCurrency700.SelectedValue = ds.Currency.ToString();
                            numAmount700.Value = (double?)ds.Amount;
                            numPercentCreditAmount1.Value = (double?)ds.PercentageCredit;
                            numPercentCreditAmount2.Value = (double?)ds.AmountTolerance;
                            comboMaximumCreditAmount700.SelectedValue = ds.MaximumCreditAmount.ToString();

                            rcbAvailableWithType.SelectedValue = ds.AvailableWithType.ToString();
                            comboAvailableWithNo.SelectedValue = ds.AvailableWithNo.ToString();
                            tbAvailableWithName.Text = ds.AvailableWithName.ToString();
                            tbAvailableWithAddr1.Text = ds.AvailableWithAddr1.ToString();
                            tbAvailableWithAddr2.Text = ds.AvailableWithAddr2.ToString();
                            tbAvailableWithAddr3.Text = ds.AvailableWithAddr3.ToString();

                            comboAvailableWithBy.SelectedValue = ds.Available_By.ToString();

                            rcbPatialShipment.SelectedValue = ds.PatialShipment.ToString();
                            rcbTranshipment.SelectedValue = ds.Transhipment.ToString();
                            tbPlaceoftakingincharge.Text = ds.PlaceOfTakingInCharge.ToString();
                            tbPortofDischarge.Text = ds.PortOfDischarge.ToString();
                            tbPlaceoffinalindistination.Text = ds.PlaceOfFinalInDistination.ToString();

                            txtEdittor_DescrpofGoods.Content = ds.DescrpGoodsBervices.ToString();
                            txtEdittor_OrderDocs700.Content = ds.DocsRequired.ToString();
                            txtEdittor_AdditionalConditions700.Content = ds.AdditionalConditions.ToString();
                            txtEdittor_Charges700.Content = ds.Charges.ToString();
                            txtEdittor_PeriodforPresentation700.Content = ds.PeriodForPresentation.ToString();
                            rcbConfimationInstructions.SelectedValue = ds.ConfimationInstructions.ToString();
                            txtEdittor_NegotgBank700.Content = ds.InstrToPaygAccptgNegotgBank.ToString();
                            txtEdittor_SendertoReceiverInfomation700.Content = ds.SenderReceiverInfomation.ToString();

                            if ((!String.IsNullOrEmpty(ds.LatesDateOfShipment.ToString())) && (ds.LatesDateOfShipment.ToString().IndexOf("1/1/1900") != -1))
                            {
                                tbLatesDateofShipment.SelectedDate = DateTime.Parse(ds.LatesDateOfShipment.ToString());
                            }
                            if ((!String.IsNullOrEmpty(ds.DateExpiry.ToString())) && (ds.DateExpiry.ToString().IndexOf("1/1/1900") != -1))
                            {
                                dteMT700DateAndPlaceOfExpiry.SelectedDate = DateTime.Parse(ds.DateExpiry.ToString());
                            }
                            if ((!String.IsNullOrEmpty(ds.DateOfIssue.ToString())) && (ds.DateOfIssue.ToString().IndexOf("1/1/1900") != -1))
                            {
                                dteDateOfIssue.SelectedDate = DateTime.Parse(ds.DateOfIssue.ToString());
                            }
                            comboAdviseThroughBankType700.SelectedValue = ds.AdviseThroughBankType.ToString();
                            comboAdviseThroughBankNo700.SelectedValue = ds.AdviseThroughBankNo.ToString();
                            txtAdviseThroughBankName700.Text = ds.AdviseThroughBankName.ToString();
                            txtAdviseThroughBankAddr700_1.Text = ds.AdviseThroughBankAddr1.ToString();
                            txtAdviseThroughBankAddr700_2.Text = ds.AdviseThroughBankAddr2.ToString();
                            txtAdviseThroughBankAddr700_3.Text = ds.AdviseThroughBankAddr3.ToString();

                            comboReimbBankType700.SelectedValue = ds.ReimbBankType.ToString();
                            rcbReimbBankNo700.SelectedValue = ds.ReimbBankNo.ToString();
                            tbReimbBankName700.Text = ds.ReimbBankName.ToString();
                            tbReimbBankAddr700_1.Text = ds.ReimbBankAddr1.ToString();
                            tbReimbBankAddr700_2.Text = ds.ReimbBankAddr2.ToString();
                            tbReimbBankAddr700_3.Text = ds.ReimbBankAddr3.ToString();


                            txtAdditionalAmountsCovered700_1.Text = ds.AdditionalAmountsCovered1.ToString();
                            txtAdditionalAmountsCovered700_2.Text = ds.AdditionalAmountsCovered2.ToString();
                            txtDraftsAt700_1.Text = ds.DraftsAt1.ToString();
                            txtDraftsAt700_2.Text = ds.DraftsAt2.ToString();


                            txtMixedPaymentDetails700_1.Text = ds.MixedPaymentDetails1.ToString();
                            txtMixedPaymentDetails700_2.Text = ds.MixedPaymentDetails2.ToString();
                            txtMixedPaymentDetails700_3.Text = ds.MixedPaymentDetails3.ToString();
                            txtMixedPaymentDetails700_4.Text = ds.MixedPaymentDetails4.ToString();

                            txtDeferredPaymentDetails700_1.Text = ds.DeferredPaymentDetails1.ToString();
                            txtDeferredPaymentDetails700_2.Text = ds.DeferredPaymentDetails2.ToString();
                            txtDeferredPaymentDetails700_3.Text = ds.DeferredPaymentDetails3.ToString();
                            txtDeferredPaymentDetails700_4.Text = ds.DeferredPaymentDetails4.ToString();

                            txtShipmentPeriod700_1.Text = ds.ShipmentPeriod1.ToString();
                            txtShipmentPeriod700_2.Text = ds.ShipmentPeriod2.ToString();
                            txtShipmentPeriod700_3.Text = ds.ShipmentPeriod3.ToString();
                            txtShipmentPeriod700_4.Text = ds.ShipmentPeriod4.ToString();
                            txtShipmentPeriod700_5.Text = ds.ShipmentPeriod3.ToString();
                            txtShipmentPeriod700_6.Text = ds.ShipmentPeriod4.ToString();
                        }
                    }
                }
            }
            //load tab charge
            var dsCharge = entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault();
            if (dsCharge != null)
            {
                tbChargeRemarks.Text = dsCharge.ChargeRemarks.ToString();
                tbVatNo.Text = dsCharge.VATNo.ToString();
                tbChargeCode.SelectedValue = dsCharge.Chargecode.ToString();
                rcbChargeCcy.SelectedValue = dsCharge.ChargeCcy.ToString();
                comboWaiveCharges.SelectedValue = dsCharge.WaiveCharges.ToString();
                rcbChargeAcct.SelectedValue = dsCharge.ChargeAcct.ToString();
                tbChargeAmt.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged.SelectedValue = dsCharge.PartyCharged.ToString();
                rcbOmortCharge.SelectedValue = dsCharge.OmortCharges.ToString();
                rcbChargeStatus.SelectedValue = dsCharge.ChargeStatus.ToString();
                lblChargeStatus.Text = dsCharge.ChargeStatus.ToString();
                lblTaxCode.Text = dsCharge.TaxCode.ToString();
                lblTaxAmt.Text = dsCharge.TaxAmt.ToString();
                //
                tbChargeCode2.SelectedValue = dsCharge.Chargecode.ToString();
                rcbChargeCcy2.SelectedValue = dsCharge.ChargeCcy.ToString();
                rcbChargeAcct2.SelectedValue = dsCharge.ChargeAcct.ToString();
                tbChargeAmt2.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged2.SelectedValue = dsCharge.PartyCharged.ToString();
                rcbOmortCharge2.SelectedValue = dsCharge.OmortCharges.ToString();
                rcbChargeStatus2.SelectedValue = dsCharge.ChargeStatus.ToString();
                lblTaxCode2.Text = dsCharge.TaxCode.ToString();
                lblTaxAmt2.Text = dsCharge.TaxAmt.ToString();
                //
                tbChargeCode3.SelectedValue = dsCharge.Chargecode.ToString();
                rcbChargeCcy3.SelectedValue = dsCharge.ChargeCcy.ToString();
                rcbChargeAcct3.SelectedValue = dsCharge.ChargeAcct.ToString();
                tbChargeAmt3.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged3.SelectedValue = dsCharge.PartyCharged.ToString();
                rcbOmortCharge3.SelectedValue = dsCharge.OmortCharges.ToString();
                rcbChargeStatus3.SelectedValue = dsCharge.ChargeStatus.ToString();
                lblTaxCode3.Text = dsCharge.TaxCode.ToString();
                lblTaxAmt3.Text = dsCharge.TaxAmt.ToString();
            }
            else
            {
                tbChargeRemarks.Text = string.Empty;
                tbVatNo.Text = string.Empty;
                tbChargeCode.SelectedValue = string.Empty;
                rcbChargeCcy.SelectedValue = string.Empty;
                comboWaiveCharges.SelectedValue = string.Empty;
                rcbChargeAcct.SelectedValue = string.Empty;
                tbChargeAmt.Text = string.Empty;
                rcbPartyCharged.SelectedValue = string.Empty;
                rcbOmortCharge.SelectedValue = string.Empty;
                rcbChargeStatus.SelectedValue = string.Empty;
                lblChargeStatus.Text = string.Empty;
                lblTaxCode.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;
                //
                tbChargeCode2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                rcbChargeAcct2.SelectedValue = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = string.Empty;
                rcbOmortCharge2.SelectedValue = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;
                //
                tbChargeCode3.SelectedValue = string.Empty;
                rcbChargeCcy3.SelectedValue = string.Empty;
                rcbChargeAcct3.SelectedValue = string.Empty;
                tbChargeAmt3.Text = string.Empty;
                rcbPartyCharged3.SelectedValue = string.Empty;
                rcbOmortCharge3.SelectedValue = string.Empty;
                rcbChargeStatus3.SelectedValue = string.Empty;
                lblTaxCode3.Text = string.Empty;
                lblTaxAmt3.Text = string.Empty;
            }
            //var dsDoc = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetByDocCollectCode(tbEssurLCCode.Text);
            //if (dsDoc == null || dsDoc.Tables.Count <= 0 || dsDoc.Tables[0].Rows.Count <= 0)
            //{
            //    return;
            //}
            LoadDataChargeAcct();
            return true;
        }
        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender,
                                                               RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (comboWaiveCharges.SelectedValue == "NO")
            {
                divACCPTCHG.Visible = true;
                divCABLECHG.Visible = true;
                divPAYMENTCHG.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                divACCPTCHG.Visible = false;
                divCABLECHG.Visible = false;
                divPAYMENTCHG.Visible = false;
            }
        }
        protected void LoadTabMT700()
        {
            //var dsMT700 = DataProvider.SQLData.B_BNORMAILLCMT700_GetByNormalLCCode(tbEssurLCCode.Text.Trim());

        }
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = Request.QueryString["IsAuthorize"] == null;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = Request.QueryString["IsAuthorize"] != null;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void InitDataSource()
        {
            //disable Cancel
            divCancelLC.Visible = false;
            dteCancelDate.SelectedDate = DateTime.Now;
            dteContingentExpiryDate.SelectedDate = DateTime.Now;
            //
            var dsNostro = bd.SQLData.B_BBANKSWIFTCODE_GetByType("Nostro");
            bc.Commont.initRadComboBox(ref rcbReimbBankNo700, "SwiftCode", "SwiftCode", dsNostro);

            var dsall = bd.SQLData.B_BBANKSWIFTCODE_GetByType("all");
            bc.Commont.initRadComboBox(ref comboAdviseThroughBankNo700, "SwiftCode", "SwiftCode", dsall);
            bc.Commont.initRadComboBox(ref comboAvailableWithNo, "SwiftCode", "SwiftCode", dsall);
            //currency
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref comboCurrency700, "Code", "Code", dsCurrency);
            //
            bc.Commont.initRadComboBox(ref comboDraweeCusNo700, "SwiftCode", "SwiftCode", dsall);
            //load tab Charge
            var dsChargeCode = entContext.BCHARGECODEs.Where(x => x.AdviseELC == "X").ToList();
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("ID");
            tbl1.Columns.Add("Code");
            foreach (var item in dsChargeCode)
            {
                tbl1.Rows.Add(item.Code, item.Name_EN);
            }

            DataSet datasource = new DataSet();
            datasource.Tables.Add(tbl1);

            var lstChargeCode = new List<string>();
            if (dsChargeCode != null)
            {
                tbChargeCode.Items.Clear();
                tbChargeCode.Items.Add(new RadComboBoxItem(""));
                tbChargeCode.DataValueField = "ID";
                tbChargeCode.DataTextField = "Code";
                tbChargeCode.DataSource = datasource;
                tbChargeCode.DataBind();

                tbChargeCode2.Items.Clear();
                tbChargeCode2.Items.Add(new RadComboBoxItem(""));
                tbChargeCode2.DataValueField = "ID";
                tbChargeCode2.DataTextField = "Code";
                tbChargeCode2.DataSource = datasource;
                tbChargeCode2.DataBind();

                tbChargeCode3.Items.Clear();
                tbChargeCode3.Items.Add(new RadComboBoxItem(""));
                tbChargeCode3.DataValueField = "ID";
                tbChargeCode3.DataTextField = "Code";
                tbChargeCode3.DataSource = datasource;
                tbChargeCode3.DataBind();

                //set currency
                rcbChargeCcy.Items.Clear();
                rcbChargeCcy.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy.DataValueField = "Code";
                rcbChargeCcy.DataTextField = "Code";
                rcbChargeCcy.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy.DataBind();

                rcbChargeCcy2.Items.Clear();
                rcbChargeCcy2.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy2.DataValueField = "Code";
                rcbChargeCcy2.DataTextField = "Code";
                rcbChargeCcy2.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy2.DataBind();

                rcbChargeCcy3.Items.Clear();
                rcbChargeCcy3.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy3.DataValueField = "Code";
                rcbChargeCcy3.DataTextField = "Code";
                rcbChargeCcy3.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy3.DataBind();
                //

            }
        }
        protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantBankType700.SelectedItem != null ? rcbApplicantBankType700.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        protected void LoadChargeAcct3()
        {
            rcbChargeAcct3.Items.Clear();
            rcbChargeAcct3.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct3.DataValueField = "Id";
            rcbChargeAcct3.DataTextField = "Id";
            rcbChargeAcct3.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantBankType700.SelectedItem != null ? rcbApplicantBankType700.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy3.SelectedValue);
            rcbChargeAcct3.DataBind();
        }
        protected void LoadDataChargeAcct()
        {
            var dataCharge = entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault();
            if (dataCharge != null)
            {
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                comboWaiveCharges.SelectedValue = dataCharge.WaiveCharges.ToString();
                rcbChargeAcct.SelectedValue = dataCharge.ChargeAcct.ToString();

                    //tbChargePeriod.Text = drow1["ChargePeriod"].ToString();
                rcbChargeCcy.SelectedValue = dataCharge.ChargeCcy.ToString();
                    tbChargeAmt.Text = dataCharge.ChargeAmt.ToString();
                    rcbPartyCharged.SelectedValue = dataCharge.PartyCharged.ToString();
                    lblPartyCharged.Text = dataCharge.PartyCharged.ToString();
                    rcbOmortCharge.SelectedValue = dataCharge.OmortCharges.ToString();
                    rcbChargeStatus.SelectedValue = dataCharge.ChargeStatus.ToString();
                    lblChargeStatus.Text = dataCharge.ChargeStatus.ToString();

                    tbChargeRemarks.Text = dataCharge.ChargeRemarks.ToString();
                    tbVatNo.Text = dataCharge.VATNo.ToString();
                    lblTaxCode.Text = dataCharge.TaxCode.ToString();
                    //lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                    lblTaxAmt.Text = dataCharge.TaxAmt.ToString();

                    tbChargeCode.SelectedValue = dataCharge.Chargecode.ToString();

                    //ChargeAmount += ConvertStringToFloat(drow1["ChargeAmt"].ToString());
                

            }
        }
        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct();
        }
        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            //CalcTax();
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
            //CalcTax3();
        }
        protected void rcbPartyCharged3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            //CalcTax2();
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            //CalcTax3();
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
            //CalcTax2();

        }
        protected void rcbChargeAcct3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct3();
        }
        protected void rcbPartyCharged2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged.Text = rcbPartyCharged.SelectedValue;
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
            //CalcTax();
        }
        protected void rcbChargeAcct2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct2();
        }
        protected void LoadChargeAcct()
        {
            rcbChargeAcct.Items.Clear();
            rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct.DataValueField = "Id";
            rcbChargeAcct.DataTextField = "Id";
            rcbChargeAcct.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(rcbApplicantBankType700.SelectedItem != null ? rcbApplicantBankType700.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy.SelectedValue);
            rcbChargeAcct.DataBind();

        }
        protected void SetRelation_AvailableWithType()
        {
            switch (rcbAvailableWithType.SelectedValue)
            {
                case "A":
                    comboAvailableWithNo.Enabled = true;
                    tbAvailableWithName.Enabled = false;
                    tbAvailableWithAddr1.Enabled = false;
                    tbAvailableWithAddr2.Enabled = false;
                    tbAvailableWithAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    comboAvailableWithNo.Enabled = false;
                    tbAvailableWithName.Enabled = true;
                    tbAvailableWithAddr1.Enabled = true;
                    tbAvailableWithAddr2.Enabled = true;
                    tbAvailableWithAddr3.Enabled = true;
                    break;
            }
        }

        protected void rcbAvailableWithType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AvailableWithType();
        }
        protected void SwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["BankName"] = row["BankName"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
            e.Item.Attributes["Continent"] = row["Continent"].ToString();
            e.Item.Attributes["SwiftCode"] = row["SwiftCode"].ToString();
        }
        protected void comboAvailableWithBy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
        }
        protected void comboAvailableWithNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbAvailableWithName.Text = comboAvailableWithNo.SelectedItem != null ? comboAvailableWithNo.SelectedItem.Attributes["BankName"] : "";
        }
        private void LoadToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected bool SaveData()
        {
            try
            {
                if (tbEssurLCCode.Text == "")
                {
                    lblError.Text = "Please fill in the Code";
                    return false;
                }
                if (entContext.BAdvisingAndNegotiationLCs.Where(dr => dr.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault() == null)
                {
                    //check to press commmit not validation return false for case amend new code
                    if((TabId == TabIssueLCAmend)||(TabId==TabIssueLCCancel))
                    {
                        lblError.Text = "This Amend Documentary "+tbEssurLCCode.Text+ " was not found";
                        return false;
                    }
                    //
                    BAdvisingAndNegotiationLC obj = new BAdvisingAndNegotiationLC
                    {
                        NormalLCCode = tbEssurLCCode.Text.Trim(),
                        ReceivingBank = txtRevivingBank700.Text.Trim(),
                        SequenceOfTotal = tbBaquenceOfTotal.Text.Trim(),
                        FormDocumentaryCredit = comboFormOfDocumentaryCredit.SelectedValue,
                        DocumentaryCreditNumber = lblDocumentaryCreditNumber.Text,
                        DateOfIssue = dteDateOfIssue.SelectedDate,
                        DateExpiry = dteMT700DateAndPlaceOfExpiry.SelectedDate,
                        PlaceOfExpiry = tbPlaceOfExpiry.Text.Trim(),
                        ApplicationRule = comboAvailableRule.SelectedValue,
                        ApplicantType = rcbApplicantBankType700.SelectedValue,
                        ApplicantNo = tbApplicantNo700.Text.Trim(),
                        ApplicantName = tbApplicantName700.Text.Trim(),
                        ApplicantAddr1 = tbApplicantAddr700_1.Text.Trim(),
                        ApplicantAddr2 = tbApplicantAddr700_2.Text.Trim(),
                        ApplicantAddr3 = tbApplicantAddr700_3.Text.Trim(),
                        Currency = comboCurrency700.SelectedValue,
                        Amount = numAmount700.Value,
                        PercentageCredit = numPercentCreditAmount1.Value,
                        AmountTolerance = numPercentCreditAmount2.Value,
                        MaximumCreditAmount = comboMaximumCreditAmount700.SelectedValue,
                        AvailableWithType = rcbAvailableWithType.SelectedValue,
                        AvailableWithNo = comboAvailableWithNo.SelectedValue,
                        AvailableWithName = tbAvailableWithName.Text.Trim(),
                        AvailableWithAddr1 = tbAvailableWithAddr1.Text.Trim(),
                        AvailableWithAddr2 = tbAvailableWithAddr2.Text.Trim(),
                        AvailableWithAddr3 = tbAvailableWithAddr3.Text.Trim(),
                        Available_By = comboAvailableWithBy.SelectedValue,
                        MixedPaymentDetails1 = txtMixedPaymentDetails700_1.Text.Trim(),
                        MixedPaymentDetails2 = txtMixedPaymentDetails700_2.Text.Trim(),
                        MixedPaymentDetails3 = txtMixedPaymentDetails700_3.Text.Trim(),
                        MixedPaymentDetails4 = txtMixedPaymentDetails700_4.Text.Trim(),
                        DeferredPaymentDetails1 = txtDeferredPaymentDetails700_1.Text.Trim(),
                        DeferredPaymentDetails2 = txtMixedPaymentDetails700_2.Text.Trim(),
                        DeferredPaymentDetails3 = txtMixedPaymentDetails700_3.Text.Trim(),
                        DeferredPaymentDetails4 = txtMixedPaymentDetails700_4.Text.Trim(),
                        PatialShipment = rcbPatialShipment.SelectedValue,
                        Transhipment = rcbTranshipment.SelectedValue,
                        PlaceOfTakingInCharge = tbPlaceoftakingincharge.Text,
                        PortOfLoading = tbPortofloading.Text,
                        PortOfDischarge = tbPortofDischarge.Text,
                        PlaceOfFinalInDistination = tbPlaceoffinalindistination.Text,
                        LatesDateOfShipment = tbLatesDateofShipment.SelectedDate,
                        ShipmentPeriod1 = txtShipmentPeriod700_1.Text,
                        ShipmentPeriod2 = txtShipmentPeriod700_2.Text,
                        ShipmentPeriod3 = txtShipmentPeriod700_3.Text,
                        ShipmentPeriod4 = txtShipmentPeriod700_4.Text,
                        DescrpGoodsBervices = txtEdittor_DescrpofGoods.Content,
                        DocsRequired = txtEdittor_OrderDocs700.Content,
                        AdditionalConditions = txtEdittor_AdditionalConditions700.Content,
                        Charges = txtEdittor_Charges700.Content,
                        PeriodForPresentation = txtEdittor_PeriodforPresentation700.Content,
                        ConfimationInstructions = rcbConfimationInstructions.SelectedValue,
                        InstrToPaygAccptgNegotgBank = txtEdittor_NegotgBank700.Content,
                        SenderReceiverInfomation = txtEdittor_SendertoReceiverInfomation700.Content,
                        BeneficiaryType = comboBeneficiaryType700.SelectedValue,
                        BeneficiaryNo = txtBeneficiaryNo700.Text,
                        BeneficiaryName = txtBeneficiaryName700.Text,
                        BeneficiaryAddr1 = txtBeneficiaryAddr700_1.Text,
                        BeneficiaryAddr2 = txtBeneficiaryAddr700_2.Text,
                        BeneficiaryAddr3 = txtBeneficiaryAddr700_3.Text,
                        AdviseThroughBankType = comboAdviseThroughBankType700.SelectedValue,
                        AdviseThroughBankNo = comboAdviseThroughBankNo700.SelectedValue,
                        AdviseThroughBankName = txtAdviseThroughBankName700.Text,
                        AdviseThroughBankAddr1 = txtAdviseThroughBankAddr700_1.Text,
                        AdviseThroughBankAddr2 = txtAdviseThroughBankAddr700_2.Text,
                        AdviseThroughBankAddr3 = txtAdviseThroughBankAddr700_3.Text,
                        ReimbBankType = comboReimbBankType700.SelectedValue,
                        ReimbBankNo = rcbReimbBankNo700.SelectedValue,
                        ReimbBankName = tbReimbBankName700.Text,
                        ReimbBankAddr1 = tbReimbBankAddr700_1.Text,
                        ReimbBankAddr2 = tbReimbBankAddr700_2.Text,
                        ReimbBankAddr3 = tbReimbBankAddr700_3.Text,
                        AdditionalAmountsCovered1 = txtAdditionalAmountsCovered700_1.Text,
                        AdditionalAmountsCovered2 = txtAdditionalAmountsCovered700_2.Text,
                        DraftsAt1 = txtDraftsAt700_1.Text,
                        DraftsAt2 = txtDraftsAt700_2.Text,
                        //
                        DraweeType = comboDraweeCusType.SelectedValue,
                        DraweeNo = comboDraweeCusNo700.SelectedValue,
                        DraweeName = txtDraweeCusName.Text,
                        DraweeAddr1 = txtDraweeAddr1.Text,
                        DraweeAddr2 = txtDraweeAddr2.Text,
                        DraweeAddr3 = txtDraweeAddr3.Text,
                        Status = "UNA"
                        //
                    };
                    entContext.BAdvisingAndNegotiationLCs.Add(obj);
                }
                else
                {
                    //check save/update new or not approve
                    //amend for code had approved
                    
                    var ori = entContext.BAdvisingAndNegotiationLCs.Where(dr => dr.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault();
                    if (TabId == TabIssueLCAmend)//case amend filter AUT
                    {
                        if (ori.Status == "UNA")
                        {
                            lblError.Text = "This Amend Documentary " + tbEssurLCCode.Text + " was not authorized";
                            return false;
                        }
                    }
                    else if (TabId == TabIssueLCCancel)
                    {
                        if (ori.AmendStatus != null)
                        {
                            if (ori.Status != "AUT")
                            {
                                lblError.Text = "This Documentary " + tbEssurLCCode.Text + " was not authorized";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (ori.Status != "UNA")
                        {
                            lblError.Text = "This Amend Documentary " + tbEssurLCCode.Text + " was authorized";
                            return false;
                        }
                    }
                    if (ori != null)
                    {
                        ori.ReceivingBank = txtRevivingBank700.Text.Trim();
                        ori.SequenceOfTotal = tbBaquenceOfTotal.Text.Trim();
                        ori.FormDocumentaryCredit = comboFormOfDocumentaryCredit.SelectedValue;
                        ori.DocumentaryCreditNumber = lblDocumentaryCreditNumber.Text;
                        ori.DateOfIssue = dteDateOfIssue.SelectedDate;
                        ori.DateExpiry = dteMT700DateAndPlaceOfExpiry.SelectedDate;
                        ori.PlaceOfExpiry = tbPlaceOfExpiry.Text.Trim();
                        ori.ApplicationRule = comboAvailableRule.SelectedValue;
                        ori.ApplicantType = rcbApplicantBankType700.SelectedValue;
                        ori.ApplicantNo = tbApplicantNo700.Text.Trim();
                        ori.ApplicantName = tbApplicantName700.Text.Trim();
                        ori.ApplicantAddr1 = tbApplicantAddr700_1.Text.Trim();
                        ori.ApplicantAddr2 = tbApplicantAddr700_2.Text.Trim();
                        ori.ApplicantAddr3 = tbApplicantAddr700_3.Text.Trim();
                        ori.Currency = comboCurrency700.SelectedValue;
                        ori.Amount = numAmount700.Value;
                        ori.PercentageCredit = numPercentCreditAmount1.Value;
                        ori.AmountTolerance = numPercentCreditAmount2.Value;
                        ori.MaximumCreditAmount = comboMaximumCreditAmount700.SelectedValue;
                        ori.AvailableWithType = rcbAvailableWithType.SelectedValue;
                        ori.AvailableWithNo = comboAvailableWithNo.SelectedValue;
                        ori.AvailableWithName = tbAvailableWithName.Text.Trim();
                        ori.AvailableWithAddr1 = tbAvailableWithAddr1.Text.Trim();
                        ori.AvailableWithAddr2 = tbAvailableWithAddr2.Text.Trim();
                        ori.AvailableWithAddr3 = tbAvailableWithAddr3.Text.Trim();
                        ori.Available_By = comboAvailableWithBy.SelectedValue;
                        ori.MixedPaymentDetails1 = txtMixedPaymentDetails700_1.Text.Trim();
                        ori.MixedPaymentDetails2 = txtMixedPaymentDetails700_2.Text.Trim();
                        ori.MixedPaymentDetails3 = txtMixedPaymentDetails700_3.Text.Trim();
                        ori.MixedPaymentDetails4 = txtMixedPaymentDetails700_4.Text.Trim();
                        ori.DeferredPaymentDetails1 = txtDeferredPaymentDetails700_1.Text.Trim();
                        ori.DeferredPaymentDetails2 = txtMixedPaymentDetails700_2.Text.Trim();
                        ori.DeferredPaymentDetails3 = txtMixedPaymentDetails700_3.Text.Trim();
                        ori.DeferredPaymentDetails4 = txtMixedPaymentDetails700_4.Text.Trim();
                        ori.PatialShipment = rcbPatialShipment.SelectedValue;
                        ori.Transhipment = rcbTranshipment.SelectedValue;
                        ori.PlaceOfTakingInCharge = tbPlaceoftakingincharge.Text;
                        ori.PortOfLoading = tbPortofloading.Text;
                        ori.PortOfDischarge = tbPortofDischarge.Text;
                        ori.PlaceOfFinalInDistination = tbPlaceoffinalindistination.Text;
                        ori.LatesDateOfShipment = tbLatesDateofShipment.SelectedDate;
                        ori.ShipmentPeriod1 = txtShipmentPeriod700_1.Text;
                        ori.ShipmentPeriod2 = txtShipmentPeriod700_2.Text;
                        ori.ShipmentPeriod3 = txtShipmentPeriod700_3.Text;
                        ori.ShipmentPeriod4 = txtShipmentPeriod700_4.Text;
                        ori.DescrpGoodsBervices = txtEdittor_DescrpofGoods.Content;
                        ori.DocsRequired = txtEdittor_OrderDocs700.Content;
                        ori.AdditionalConditions = txtEdittor_AdditionalConditions700.Content;
                        ori.Charges = txtEdittor_Charges700.Content;
                        ori.PeriodForPresentation = txtEdittor_PeriodforPresentation700.Content;
                        ori.ConfimationInstructions = rcbConfimationInstructions.SelectedValue;
                        ori.InstrToPaygAccptgNegotgBank = txtEdittor_NegotgBank700.Content;
                        ori.SenderReceiverInfomation = txtEdittor_SendertoReceiverInfomation700.Content;
                        ori.BeneficiaryType = comboBeneficiaryType700.SelectedValue;
                        ori.BeneficiaryNo = txtBeneficiaryNo700.Text;
                        ori.BeneficiaryName = txtBeneficiaryName700.Text;
                        ori.BeneficiaryAddr1 = txtBeneficiaryAddr700_1.Text;
                        ori.BeneficiaryAddr2 = txtBeneficiaryAddr700_2.Text;
                        ori.BeneficiaryAddr3 = txtBeneficiaryAddr700_3.Text;
                        ori.AdviseThroughBankType = comboAdviseThroughBankType700.SelectedValue;
                        ori.AdviseThroughBankNo = comboAdviseThroughBankNo700.SelectedValue;
                        ori.AdviseThroughBankName = txtAdviseThroughBankName700.Text;
                        ori.AdviseThroughBankAddr1 = txtAdviseThroughBankAddr700_1.Text;
                        ori.AdviseThroughBankAddr2 = txtAdviseThroughBankAddr700_2.Text;
                        ori.AdviseThroughBankAddr3 = txtAdviseThroughBankAddr700_3.Text;
                        ori.ReimbBankType = comboReimbBankType700.SelectedValue;
                        ori.ReimbBankNo = rcbReimbBankNo700.SelectedValue;
                        ori.ReimbBankName = tbReimbBankName700.Text;
                        ori.ReimbBankAddr1 = tbReimbBankAddr700_1.Text;
                        ori.ReimbBankAddr2 = tbReimbBankAddr700_2.Text;
                        ori.ReimbBankAddr3 = tbReimbBankAddr700_3.Text;
                        ori.AdditionalAmountsCovered1 = txtAdditionalAmountsCovered700_1.Text;
                        ori.AdditionalAmountsCovered2 = txtAdditionalAmountsCovered700_2.Text;
                        ori.DraftsAt1 = txtDraftsAt700_1.Text;
                        ori.DraftsAt2 = txtDraftsAt700_2.Text;
                        //
                        ori.DraweeType = comboDraweeCusType.SelectedValue;
                        ori.DraweeNo = comboDraweeCusNo700.SelectedValue;
                        ori.DraweeName = txtDraweeCusName.Text;
                        ori.DraweeAddr1 = txtDraweeAddr1.Text;
                        ori.DraweeAddr2 = txtDraweeAddr2.Text;
                        ori.DraweeAddr3 = txtDraweeAddr3.Text;
                        if (TabId == TabIssueLCCancel)
                        {
                            ori.CancelDate = dteCancelDate.SelectedDate;
                            ori.ContingentExpiryDate = dteContingentExpiryDate.SelectedDate;
                            ori.CancelRemark = txtCancelRemark.Text;
                        }
                        else if (TabId != TabIssueLCAmend)
                        {
                            ori.Status = "UNA";    
                        }
                        
                        
                    }
                }
                entContext.SaveChanges();
                //save Charge
                if (entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault() == null)
                {
                    var txtChargeAmt = 0;
                    if (tbChargeAmt.Text != "")
                    {
                        txtChargeAmt = Int32.Parse(tbChargeAmt.Text);
                    }
                    var objCharge = new BAdvisingAndNegotiationLCCharge
                    {
                        DocCollectCode = tbEssurLCCode.Text,
                        WaiveCharges = comboWaiveCharges.SelectedValue,
                        Chargecode = tbChargeCode.SelectedValue,
                        ChargeAcct = rcbChargeAcct.SelectedValue,
                        ChargeCcy = rcbChargeCcy.SelectedValue,
                        ChargeAmt = txtChargeAmt,
                        PartyCharged = rcbPartyCharged.SelectedValue,
                        OmortCharges = rcbOmortCharge.SelectedValue,
                        ChargeStatus = rcbChargeStatus.SelectedValue,
                        ChargeRemarks = tbChargeRemarks.Text,
                        VATNo = tbVatNo.Text,
                        TaxCode = lblTaxCode.Text,
                        TaxAmt = lblTaxAmt.Text
                    };
                    entContext.BAdvisingAndNegotiationLCCharges.Add(objCharge);
                }
                else
                {
                    var txtChargeAmt = 0;
                    if (tbChargeAmt.Text != "")
                    {
                        txtChargeAmt = Int32.Parse(tbChargeAmt.Text);
                    }
                    var itCharge = entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault();
                    itCharge.DocCollectCode = tbEssurLCCode.Text;
                    itCharge.WaiveCharges = comboWaiveCharges.SelectedValue;
                    itCharge.Chargecode = tbChargeCode.SelectedValue;
                    itCharge.ChargeAcct = rcbChargeAcct.SelectedValue;
                    itCharge.ChargeCcy = rcbChargeCcy.SelectedValue;
                    itCharge.ChargeAmt = txtChargeAmt;
                    itCharge.PartyCharged = rcbPartyCharged.SelectedValue;
                    itCharge.OmortCharges = rcbOmortCharge.SelectedValue;
                    itCharge.ChargeStatus = rcbChargeStatus.SelectedValue;
                    itCharge.ChargeRemarks = tbChargeRemarks.Text;
                    itCharge.VATNo = tbVatNo.Text;
                    itCharge.TaxCode = lblTaxCode.Text;
                    itCharge.TaxAmt = lblTaxAmt.Text;

                }
                entContext.SaveChanges();
                //
                return true;       
            }
            catch (Exception ex)
            {
                
                lblError.Text = ex.Message;
                return false;
            }
        }
        protected void comboDraweeCusNo700_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtDraweeCusName.Text = comboDraweeCusNo700.SelectedItem != null
                                        ? comboDraweeCusNo700.SelectedItem.Attributes["BankName"]
                                        : "";
        }
        protected void SetRelation_DraweeCusType700()
        {
            switch (comboDraweeCusType.SelectedValue)
            {
                case "A":
                    comboDraweeCusNo700.Enabled = true;
                    txtDraweeCusName.Enabled = false;
                    txtDraweeAddr1.Enabled = false;
                    txtDraweeAddr2.Enabled = false;
                    txtDraweeAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    comboDraweeCusNo700.Enabled = false;
                    txtDraweeCusName.Enabled = true;
                    txtDraweeAddr1.Enabled = true;
                    txtDraweeAddr2.Enabled = true;
                    txtDraweeAddr3.Enabled = true;
                    break;
            }
        }
        protected void comboDraweeCusType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_DraweeCusType700();
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName.ToLower();
            switch (commandName)
            {
                case bc.Commands.Commit:
                    if (SaveData())
                    {
                        Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    }
                    break;
                case bc.Commands.Preview:
                    Response.Redirect(EditUrl("preview_exportdoc"));
                    break;
                case bc.Commands.Authorize:
                    Authorize();
                    break;
                case bc.Commands.Reverse:
                    Reverse();
                    break;

            }
        }
        protected void Reverse()
        {
            UpdateStatus("REV");
            Response.Redirect(Globals.NavigateURL(TabId, "", "LCCode=" + CodeId));
        }

        protected void UpdateStatus(string status)
        {
            var obj = entContext.BAdvisingAndNegotiationLCs.Where(dr => dr.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault();
            if (obj != null)
            {
                switch (ScreenType)
                {
                    case AdvisingAndNegotiationScreenType.Register:
                        obj.Status = status;
                        obj.AuthorizedBy = UserId.ToString();
                        obj.AuthorizedDate = DateTime.Now;
                        break;
                    case AdvisingAndNegotiationScreenType.Amend:
                        if (status == "REV")
                        {
                            obj.AmendStatus = status;
                        }
                        else
                        {
                            obj.AmendStatus = status;
                            obj.AmendBy = UserId.ToString();
                        }
                        break;
                    case AdvisingAndNegotiationScreenType.Cancel:
                        //obj.Status = status;
                        //obj.AuthorizedBy = UserId.ToString();
                        //obj.AuthorizedDate = DateTime.Now;
                        break;
                }
                entContext.SaveChanges();
            }
        }
        protected void Authorize()
        {
            UpdateStatus("AUT");
            Response.Redirect(Globals.NavigateURL(TabId));
        }
        protected void comboAvailableRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblApplicableRule740.Text = comboAvailableRule.SelectedValue;
        }
        protected void rcbApplicantBankType700_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_ApplicantBankType700();
        }
        protected void SetRelation_ApplicantBankType700()
        {
            //switch (rcbApplicantBankType700.SelectedValue)
            //{
            //    case "A":
            //        rcbApplicant700.Enabled = true;
            //        tbApplicantName700.Enabled = false;
            //        tbApplicantAddr700_1.Enabled = false;
            //        tbApplicantAddr700_2.Enabled = false;
            //        tbApplicantAddr700_3.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        rcbApplicant700.Enabled = false;
            //        tbApplicantName700.Enabled = true;
            //        tbApplicantAddr700_1.Enabled = true;
            //        tbApplicantAddr700_2.Enabled = true;
            //        tbApplicantAddr700_3.Enabled = true;
            //        break;
            //}
        }
        protected void txtBeneficiaryNo700_OnTextChanged(object sender, EventArgs e)
        {
            lblBeneficiaryNo700Error.Text = "";
            txtBeneficiaryName700.Text = "";
            if (!string.IsNullOrEmpty(txtBeneficiaryNo700.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtBeneficiaryNo700.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtBeneficiaryName700.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblBeneficiaryNo700Error.Text = "No found swiftcode";
                }
            }
        }
    }
}