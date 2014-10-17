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
        protected const int TabIssueLCConfirm = 236;
        protected const int TabIssueLCCancel = 237;
        protected const int TabIssueLCClose = 265;
        public enum AdvisingAndNegotiationScreenType
        {
            Register,
            Amend,
            Cancel,
            Close,
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
                    case TabIssueLCAmend:
                        return AdvisingAndNegotiationScreenType.Amend;
                    case TabIssueLCCancel:
                        return AdvisingAndNegotiationScreenType.Cancel;
                    case TabIssueLCClose:
                        return AdvisingAndNegotiationScreenType.Close;
                    case TabIssueLCConfirm:
                        return AdvisingAndNegotiationScreenType.Acception;
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
                    tbEssurLCCode.Enabled = true;
                    break;
                case AdvisingAndNegotiationScreenType.Amend:
                    InitToolBarForAmend();
                    //tabCharges.Visible = false;
                    //Charges.Visible = false;
                    tbEssurLCCode.Enabled = true;
                    break;
                case AdvisingAndNegotiationScreenType.Cancel:
                    InitToolBarForCancel();
                    tbEssurLCCode.Enabled = true;
                    break;
                case AdvisingAndNegotiationScreenType.Close:
                    InitToolBarForClose();
                    tbEssurLCCode.Enabled = true;
                    break;
                case AdvisingAndNegotiationScreenType.Acception:
                    InitToolBarForAccept();
                    RadComboBoxGD.Enabled = true;
                    txtExternalReference.Enabled = true;
                    ComboConfirmInstr.Enabled = true;
                    txtLimitRef.Enabled = true;
                    tbEssurLCCode.Enabled = true;
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
        protected void InitToolBarForAccept()
        {
            
            divCancelLC.Visible = false;
            divAcceptLC.Visible = true;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            
            
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable)//Authorizing
                {
                    if (_exportDoc.Status != "AUT")
                    {
                        lblError.Text = "This LC has not authorized yet.";
                    }
                    else if (_exportDoc.CancelStatus != null && _exportDoc.CancelStatus!="REV")
                    {
                        if (_exportDoc.CancelStatus == "UNA")
                        {
                            lblError.Text = "This LC is processing at Cancel step";
                        }
                        else if(_exportDoc.CancelStatus == "AUT")
                        {
                            lblError.Text = "This LC was cancelled";
                        }
                    }
                    else if (_exportDoc.CloseStatus != null &&_exportDoc.CloseStatus!="REV")
                    {
                        if (_exportDoc.CloseStatus == "UNA")
                        {
                            lblError.Text = "This LC is processing at Close step";
                        }
                        else if (_exportDoc.CloseStatus == "AUT")
                        {
                            lblError.Text = "This LC was closed";
                        }
                    }
                    else if (_exportDoc.AmendStatus == "UNA")
                    {
                        lblError.Text = "This LC is processing at Amend step";
                    }
                    else if (_exportDoc.AcceptStatus != null)
                    {
                        if (_exportDoc.AcceptStatus == "UNA")
                        {
                            lblError.Text = "This LC is processing at Accept step";
                        }
                        else if (_exportDoc.AcceptStatus == "AUT")
                        {
                            lblError.Text = "This LC was accepted.";
                        }
                    }
                }
                else {
                    if (_exportDoc.Status != "AUT")
                    {
                        lblError.Text = "This LC has not authorized yet.";
                    }
                    else if (_exportDoc.CancelStatus != null && _exportDoc.CancelStatus!="REV" )
                    {
                        if (_exportDoc.CancelStatus == "UNA")
                        {
                            lblError.Text = "This LC is processing at Cancel step";
                        }
                        else if(_exportDoc.CancelStatus=="AUT")
                        {
                            lblError.Text = "This LC was cancelled";
                        }
                    }
                    else if (_exportDoc.CloseStatus != null && _exportDoc.CloseStatus!="REV")
                    {
                        if (_exportDoc.CloseStatus == "UNA")
                        {
                            lblError.Text = "This LC is processing at Close step";
                        }
                        else if(_exportDoc.CloseStatus=="AUT")
                        {
                            lblError.Text = "This LC was closed";
                        }
                    }
                    else if (_exportDoc.AmendStatus == "UNA")
                    {
                        lblError.Text = "This LC is processing at Amend step";
                    }
                    else if (_exportDoc.AcceptStatus != null)
                    {
                        if(_exportDoc.AcceptStatus=="AUT")
                        {
                            lblError.Text = "This LC was accepted.";
                        }
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
            }
            LoadToolBar(true);
        }
        protected void InitToolBarForClose()
        {
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable)//Authorizing
                {
                    if (_exportDoc.Status != "AUT")//Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportDoc.AmendStatus != "AUT" && _exportDoc.AmendStatus != null)
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc.CancelStatus !=null)
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was cancelled or waiting for cancelling";
                    }
                    //else if (!string.IsNullOrEmpty(_exportDoc.CloseStatus) && _exportDoc.CloseStatus == "UNA")
                    //{
                    //    lblError.Text = "This Documentary was closed and waited for approve";
                    //}
                    else if (_exportDoc.CloseStatus == "AUT")
                    {
                        lblError.Text = "This Documentary was closed";
                    }
                    else if (_exportDoc.CloseStatus == "UNA")
                    {
                        lblError.Text = "This Documentary was waiting for approving closed";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else //Edit
                {
                    if (_exportDoc.Status != "AUT")//Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportDoc.AmendStatus != "AUT" &&_exportDoc.AmendStatus!=null)
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc.CancelStatus !=null)
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportDoc.CloseStatus == "UNA")
                    {
                        lblError.Text = "This Documentary was closed and waited for approve";
                    }
                    else if (_exportDoc.CloseStatus == "AUT")
                    {
                        lblError.Text = "This LC was closed";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
            }
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
                    if (Disable) // Authorizing
                    {
                        if (_exportDoc.Status != "AUT")
                        {
                            lblError.Text = "This Documentary was not authorized";
                        }
                        else if (_exportDoc.AmendStatus != "AUT"&&_exportDoc.AmendStatus!=null)
                        {
                            lblError.Text = "This Amend Documentary was not authorized";
                        }
                        else if (_exportDoc.CancelStatus == "AUT")
                        {
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            lblError.Text = "This LC was authorized for cancel";
                        }
                        else if (_exportDoc.CancelStatus == "UNA")
                        {
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            lblError.Text = "This LC was waiting for authorized cancel";
                        }
                        else if (_exportDoc.CloseStatus!=null)
                        {
                            lblError.Text = "This Documentary was closed or waiting for approving closing";
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
                        }
                        else if (_exportDoc.AmendStatus != "AUT"&&_exportDoc.AmendStatus!=null)
                        {
                            lblError.Text = "This Amend Documentary was not authorized";
                        }
                        else if (_exportDoc.CancelStatus == "UNA")
                        {
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            lblError.Text = "This Cancel Documentary was waiting for authorized cancel";
                        }
                        else if (_exportDoc.CancelStatus == "AUT")
                        {
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            lblError.Text = "This Cancel Documentary was authorized";
                        }
                        else if (_exportDoc.CloseStatus!=null)
                        {
                            lblError.Text = "This Documentary was closed or waiting for approving closing";
                        }
                        else // Not yet authorize
                        {
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                        }
                        SetDisableByReview(false);
                        if (_exportDoc.CancelStatus != "AUT")
                        {
                            dteCancelDate.Enabled = true;
                            dteContingentExpiryDate.Enabled = true;
                            txtCancelRemark.Enabled = true;
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
                    if (_exportDoc.Status != "AUT")
                    {
                        lblError.Text = "This LC was not authorized";
                    }
                    else if (_exportDoc.CancelStatus != null)
                    {
                        lblError.Text = "This LC was canceled or waited for approve cancel";
                    }
                    else if (_exportDoc.CloseStatus != null)
                    {
                        lblError.Text = "This LC was closed or waited for approve closed";
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
                        lblError.Text = "This LC was not authorized";
                        SetDisableByReview(false);
                    }
                    else if (_exportDoc.CancelStatus != null)
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This LC was canceled or waited for approve cancel";
                        SetDisableByReview(false);
                    }
                    else if (_exportDoc.CloseStatus!=null)
                    {
                        lblError.Text = "This LC was closed or waited for approve closed";
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
                    else if (_exportDoc.CloseStatus == "AUT" || _exportDoc.CloseStatus == "UNA")
                    {
                        lblError.Text = "This Documentary was closed or waiting for approving closing";
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
                    if (_exportDoc.Status== "AUT") // Authorized
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was authorized";
                        SetDisableByReview(false);
                    }
                    else if (_exportDoc.CloseStatus == "AUT" || _exportDoc.CloseStatus == "UNA")
                    {
                        lblError.Text = "This Documentary was closed or waiting for approving closing";
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
                if (dt != null)
                {
                    //
                    _exportDoc = dt;
                    txtRevivingBank700.Text = dt.ReceivingBank;
                    tbBaquenceOfTotal.Text = dt.SequenceOfTotal;
                    comboFormOfDocumentaryCredit.SelectedValue = dt.FormDocumentaryCredit??"";
                    lblDocumentaryCreditNumber.Text = dt.DocumentaryCreditNumber;
                    tbPlaceOfExpiry.Text = dt.PlaceOfExpiry;
                    comboAvailableRule.SelectedValue = dt.ApplicationRule??"";

                    rcbApplicantBankType700.SelectedValue = dt.ApplicantType??"";
                    tbApplicantNo700.Text = dt.ApplicantNo;
                    tbApplicantName700.Text = dt.ApplicantName;
                    tbApplicantAddr700_1.Text = dt.ApplicantAddr1;
                    tbApplicantAddr700_2.Text = dt.ApplicantAddr2;
                    tbApplicantAddr700_3.Text = dt.ApplicantAddr3;

                    //draw
                    comboDraweeCusType.SelectedValue = dt.DraweeType??"";
                    comboDraweeCusNo700.SelectedValue = dt.DraweeNo??"";
                    //txtDraweeCusNo.SelectedValue = dt.DraweeNo.ToString();
                    txtDraweeCusName.Text = dt.DraweeName;
                    txtDraweeAddr1.Text = dt.DraweeAddr1;
                    txtDraweeAddr2.Text = dt.DraweeAddr2;
                    txtDraweeAddr3.Text = dt.DraweeAddr3;


                    //
                    

                    //
                    if (!String.IsNullOrEmpty(tbEssurLCCode.Text))
                    {
                        var dsbeneficiary = entContext.BIMPORT_NORMAILLC.Where(x => x.NormalLCCode == tbEssurLCCode.Text).FirstOrDefault();
                        if (dsbeneficiary != null)
                        {
                            txtBeneficiaryNo700.Text = dsbeneficiary.ApplicantId;
                            txtBeneficiaryName700.Text = dsbeneficiary.ApplicantName;
                            txtBeneficiaryAddr700_1.Text = dsbeneficiary.ApplicantAddr1;
                            txtBeneficiaryAddr700_2.Text = dsbeneficiary.ApplicantAddr2;
                            txtBeneficiaryAddr700_3.Text = dsbeneficiary.ApplicantAddr3;
                            
                        }
                    }
                    comboBeneficiaryType700.SelectedValue = dt.BeneficiaryType ?? "";
                    //
                    comboCurrency700.SelectedValue = dt.Currency??"";
                    numAmount700.Value = (double?)dt.Amount;
                    numPercentCreditAmount1.Value = (double?)dt.PercentageCredit;
                    numPercentCreditAmount2.Value = (double?)dt.AmountTolerance;
                    comboMaximumCreditAmount700.SelectedValue = dt.MaximumCreditAmount??"";

                    rcbAvailableWithType.SelectedValue = dt.AvailableWithType??"";
                    comboAvailableWithNo.SelectedValue = dt.AvailableWithNo??"";
                    tbAvailableWithName.Text = dt.AvailableWithName;
                    tbAvailableWithAddr1.Text = dt.AvailableWithAddr1;
                    tbAvailableWithAddr2.Text = dt.AvailableWithAddr2;
                    tbAvailableWithAddr3.Text = dt.AvailableWithAddr3;

                    comboAvailableWithBy.SelectedValue = dt.Available_By??"";

                    rcbPatialShipment.SelectedValue = dt.PatialShipment??"";
                    rcbTranshipment.SelectedValue = dt.Transhipment??"";
                    tbPlaceoftakingincharge.Text = dt.PlaceOfTakingInCharge;
                    tbPortofDischarge.Text = dt.PortOfDischarge;
                    tbPlaceoffinalindistination.Text = dt.PlaceOfFinalInDistination;

                    txtEdittor_DescrpofGoods.Content = dt.DescrpGoodsBervices;
                    txtEdittor_OrderDocs700.Content = dt.DocsRequired;
                    txtEdittor_AdditionalConditions700.Content = dt.AdditionalConditions;
                    txtEdittor_Charges700.Content = dt.Charges;
                    txtEdittor_PeriodforPresentation700.Content = dt.PeriodForPresentation;
                    rcbConfimationInstructions.SelectedValue = dt.ConfimationInstructions??"";
                    txtEdittor_NegotgBank700.Content = dt.InstrToPaygAccptgNegotgBank;
                    txtEdittor_SendertoReceiverInfomation700.Content = dt.SenderReceiverInfomation;

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
                    comboAdviseThroughBankType700.SelectedValue = dt.AdviseThroughBankType??"";
                    comboAdviseThroughBankNo700.SelectedValue = dt.AdviseThroughBankNo??"";
                    txtAdviseThroughBankName700.Text = dt.AdviseThroughBankName;
                    txtAdviseThroughBankAddr700_1.Text = dt.AdviseThroughBankAddr1;
                    txtAdviseThroughBankAddr700_2.Text = dt.AdviseThroughBankAddr2;
                    txtAdviseThroughBankAddr700_3.Text = dt.AdviseThroughBankAddr3;

                    comboReimbBankType700.SelectedValue = dt.ReimbBankType??"";
                    rcbReimbBankNo700.SelectedValue = dt.ReimbBankNo??"";
                    tbReimbBankName700.Text = dt.ReimbBankName;
                    tbReimbBankAddr700_1.Text = dt.ReimbBankAddr1;
                    tbReimbBankAddr700_2.Text = dt.ReimbBankAddr2;
                    tbReimbBankAddr700_3.Text = dt.ReimbBankAddr3;


                    txtAdditionalAmountsCovered700_1.Text = dt.AdditionalAmountsCovered1;
                    txtAdditionalAmountsCovered700_2.Text = dt.AdditionalAmountsCovered2;
                    txtDraftsAt700_1.Text = dt.DraftsAt1;
                    txtDraftsAt700_2.Text = dt.DraftsAt2;


                    txtMixedPaymentDetails700_1.Text = dt.MixedPaymentDetails1;
                    txtMixedPaymentDetails700_2.Text = dt.MixedPaymentDetails2;
                    txtMixedPaymentDetails700_3.Text = dt.MixedPaymentDetails3;
                    txtMixedPaymentDetails700_4.Text = dt.MixedPaymentDetails4;

                    txtDeferredPaymentDetails700_1.Text = dt.DeferredPaymentDetails1;
                    txtDeferredPaymentDetails700_2.Text = dt.DeferredPaymentDetails2;
                    txtDeferredPaymentDetails700_3.Text = dt.DeferredPaymentDetails3;
                    txtDeferredPaymentDetails700_4.Text = dt.DeferredPaymentDetails4;

                    txtShipmentPeriod700_1.Text = dt.ShipmentPeriod1;
                    txtShipmentPeriod700_2.Text = dt.ShipmentPeriod2;
                    txtShipmentPeriod700_3.Text = dt.ShipmentPeriod3;
                    txtShipmentPeriod700_4.Text = dt.ShipmentPeriod4;
                    txtShipmentPeriod700_5.Text = dt.ShipmentPeriod5;
                    txtShipmentPeriod700_6.Text = dt.ShipmentPeriod6;
                    if (!string.IsNullOrEmpty(dt.CancelDate.ToString()) && dt.CancelDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteCancelDate.SelectedDate = DateTime.Parse(dt.CancelDate.ToString());
                    }

                    if (!string.IsNullOrEmpty(dt.ContingentExpiryDate.ToString()) && dt.ContingentExpiryDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteContingentExpiryDate.SelectedDate = DateTime.Parse(dt.ContingentExpiryDate.ToString());
                    }
                    txtCancelRemark.Text = dt.CancelRemark;
                    //
                    if (TabId == TabIssueLCConfirm)
                    {
                        

                        RadComboBoxGD.SelectedValue = dt.GenerateDelivery;
                        //RadComboBoxItem item = RadComboBoxGD.FindItemByValue(dt.GenerateDelivery);
                        //item.Selected = true;
                        if (!String.IsNullOrEmpty(dt.Date.ToString()) && dt.Date.ToString().IndexOf("1/1/1900") == -1)
                        {
                            DateConfirm.SelectedDate = DateTime.Parse(dt.Date.ToString());
                        }
                        txtExternalReference.Text = dt.ExternalReference;
                        ComboConfirmInstr.SelectedValue = dt.ConfirmationInstr;
                        txtLimitRef.Text = dt.LimitRef;
                    }
                    
                    //
                }
                else
                {
                    if (TabId != TabIssueLCAmend)
                    {
                        //find in BIMPORT_NORMAILLC_MT700 if cannot find, find in BAdvisingAndNegotiationLCs
                        var ds = from M in entContext.BIMPORT_NORMAILLC_MT700
                                 join N in entContext.BIMPORT_NORMAILLC on M.NormalLCCode equals N.NormalLCCode
                                 where (M.NormalLCCode == tbEssurLCCode.Text)
                                 select new { M, N };
                        //

                        //
                        if (ds == null || ds.Count() == 0)
                        {
                            lblError.Text = "This LC can not find";
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
                            return false;
                        }
                        else
                        {
                            //filter status="UNA" from ds
                            var BMT700 = new BIMPORT_NORMAILLC_MT700();
                            var BM = new BIMPORT_NORMAILLC();
                            foreach (var item in ds)
                            {
                                BMT700 = item.M;
                                BM = item.N;
                            }
                            if (BMT700 != null && BM != null)
                            {
                                //
                                if (BM.Status != "AUT")
                                {
                                    lblError.Text = "This LC was not authorized";
                                    SetDisableByReview(true);
                                    return false;

                                }
                                else if (BM.Amend_Status != null && BM.Amend_Status != "AUT" && BM.Amend_Status != "REV")
                                {
                                    lblError.Text = "This LC is processing at Amend step";
                                    SetDisableByReview(true);
                                    return false;
                                }
                                else if (BM.Cancel_Status != null)
                                {
                                    lblError.Text = "This LC was cancelled or waiting for approving Cancelled";
                                    SetDisableByReview(true);
                                    return false;
                                }
                                else if (BM.CloseStatus != null)
                                {
                                    lblError.Text = "This LC was closed or waiting for approving Closing";
                                    SetDisableByReview(true);
                                    return false;
                                }
                                //
                                txtRevivingBank700.Text = BMT700.ReceivingBank;
                                tbBaquenceOfTotal.Text = BMT700.SequenceOfTotal;
                                comboFormOfDocumentaryCredit.SelectedValue = BMT700.FormDocumentaryCredit ?? "";
                                lblDocumentaryCreditNumber.Text = BMT700.DocumentaryCreditNumber;
                                tbPlaceOfExpiry.Text = BMT700.PlaceOfExpiry;
                                comboAvailableRule.SelectedValue = BMT700.ApplicationRule ?? "";

                                //change two value
                                    
                                 
                                rcbApplicantBankType700.SelectedValue = BMT700.BeneficiaryType ?? "";
                                tbApplicantNo700.Text = BMT700.BeneficiaryNo;
                                tbApplicantName700.Text = BMT700.BeneficiaryName;
                                tbApplicantAddr700_1.Text = BMT700.BeneficiaryAddr1;
                                tbApplicantAddr700_2.Text = BMT700.BeneficiaryAddr2;
                                tbApplicantAddr700_3.Text = BMT700.BeneficiaryAddr3;

                                //comboBeneficiaryType700.SelectedValue = BMT700.ApplicantType ?? "";
                                //txtBeneficiaryNo700.Text = BMT700.ApplicantNo;
                                //txtBeneficiaryName700.Text = BMT700.ApplicantName;
                                //txtBeneficiaryAddr700_1.Text = BMT700.ApplicantAddr1;
                                //txtBeneficiaryAddr700_2.Text = BMT700.ApplicantAddr2;
                                //txtBeneficiaryAddr700_3.Text = BMT700.ApplicantAddr3;
                                //
                                if (BM != null)
                                {
                                    txtBeneficiaryNo700.Text = BM.ApplicantId;
                                    txtBeneficiaryName700.Text = BM.ApplicantName;
                                    txtBeneficiaryAddr700_1.Text = BM.ApplicantAddr1;
                                    txtBeneficiaryAddr700_2.Text = BM.ApplicantAddr2;
                                    txtBeneficiaryAddr700_3.Text = BM.ApplicantAddr3;
                                    
                                }
                                //
                                comboBeneficiaryType700.SelectedValue = BMT700.BeneficiaryType ?? "";    
                                
                                //drawee type load nguoc
                                comboDraweeCusType.SelectedValue = BMT700.DraweeType ?? "";
                                //txtDraweeCusNo.Text = ds.ApplicantNo;
                                txtDraweeCusName.Text = BMT700.DraweeName;
                                txtDraweeAddr1.Text = BMT700.DraweeAddr1;
                                txtDraweeAddr2.Text = BMT700.DraweeAddr2;
                                txtDraweeAddr3.Text = BMT700.DraweeAddr3;
                                comboDraweeCusNo700.SelectedValue = BMT700.DraweeNo ?? "";
                                //
                                rcbAvailableWithType.SelectedValue = BMT700.AvailableWithType ?? "";
                                comboAvailableWithNo.SelectedValue = BMT700.AvailableWithNo ?? "";
                                tbAvailableWithName.Text = BMT700.AvailableWithName;
                                tbAvailableWithAddr1.Text = BMT700.AvailableWithAddr1;
                                tbAvailableWithAddr2.Text = BMT700.AvailableWithAddr2;
                                tbAvailableWithAddr3.Text = BMT700.AvailableWithAddr3;


                                comboCurrency700.SelectedValue = BMT700.Currency ?? "";
                                numAmount700.Value = (double?)BMT700.Amount;
                                numPercentCreditAmount1.Value = (double?)BMT700.PercentageCredit;
                                numPercentCreditAmount2.Value = (double?)BMT700.AmountTolerance;
                                comboMaximumCreditAmount700.SelectedValue = BMT700.MaximumCreditAmount ?? "";

                                
                                comboAvailableWithBy.SelectedValue = BMT700.Available_By ?? "";

                                rcbPatialShipment.SelectedValue = BMT700.PatialShipment ?? "";
                                rcbTranshipment.SelectedValue = BMT700.Transhipment ?? "";
                                tbPlaceoftakingincharge.Text = BMT700.PlaceOfTakingInCharge;
                                tbPortofDischarge.Text = BMT700.PortOfDischarge;
                                tbPlaceoffinalindistination.Text = BMT700.PlaceOfFinalInDistination;

                                txtEdittor_DescrpofGoods.Content = BMT700.DescrpGoodsBervices;
                                txtEdittor_OrderDocs700.Content = BMT700.DocsRequired;
                                txtEdittor_AdditionalConditions700.Content = BMT700.AdditionalConditions;
                                txtEdittor_Charges700.Content = BMT700.Charges;
                                txtEdittor_PeriodforPresentation700.Content = BMT700.PeriodForPresentation ?? "";
                                rcbConfimationInstructions.SelectedValue = BMT700.ConfimationInstructions ?? "";
                                txtEdittor_NegotgBank700.Content = BMT700.InstrToPaygAccptgNegotgBank;
                                txtEdittor_SendertoReceiverInfomation700.Content = BMT700.SenderReceiverInfomation;

                                if ((!String.IsNullOrEmpty(BMT700.LatesDateOfShipment.ToString())) && (BMT700.LatesDateOfShipment.ToString().IndexOf("1/1/1900") != -1))
                                {
                                    tbLatesDateofShipment.SelectedDate = DateTime.Parse(BMT700.LatesDateOfShipment.ToString());
                                }
                                if ((!String.IsNullOrEmpty(BMT700.DateExpiry.ToString())) && (BMT700.DateExpiry.ToString().IndexOf("1/1/1900") != -1))
                                {
                                    dteMT700DateAndPlaceOfExpiry.SelectedDate = DateTime.Parse(BMT700.DateExpiry.ToString());
                                }
                                if ((!String.IsNullOrEmpty(BMT700.DateOfIssue.ToString())) && (BMT700.DateOfIssue.ToString().IndexOf("1/1/1900") != -1))
                                {
                                    dteDateOfIssue.SelectedDate = DateTime.Parse(BMT700.DateOfIssue.ToString());
                                }
                                comboAdviseThroughBankType700.SelectedValue = BMT700.AdviseThroughBankType ?? "";
                                comboAdviseThroughBankNo700.SelectedValue = BMT700.AdviseThroughBankNo ?? "";
                                txtAdviseThroughBankName700.Text = BMT700.AdviseThroughBankName;
                                txtAdviseThroughBankAddr700_1.Text = BMT700.AdviseThroughBankAddr1;
                                txtAdviseThroughBankAddr700_2.Text = BMT700.AdviseThroughBankAddr2;
                                txtAdviseThroughBankAddr700_3.Text = BMT700.AdviseThroughBankAddr3;

                                comboReimbBankType700.SelectedValue = BMT700.ReimbBankType ?? "";
                                rcbReimbBankNo700.SelectedValue = BMT700.ReimbBankNo ?? "";
                                tbReimbBankName700.Text = BMT700.ReimbBankName;
                                tbReimbBankAddr700_1.Text = BMT700.ReimbBankAddr1;
                                tbReimbBankAddr700_2.Text = BMT700.ReimbBankAddr2;
                                tbReimbBankAddr700_3.Text = BMT700.ReimbBankAddr3;


                                txtAdditionalAmountsCovered700_1.Text = BMT700.AdditionalAmountsCovered1;
                                txtAdditionalAmountsCovered700_2.Text = BMT700.AdditionalAmountsCovered2;
                                txtDraftsAt700_1.Text = BMT700.DraftsAt1;
                                txtDraftsAt700_2.Text = BMT700.DraftsAt2;


                                txtMixedPaymentDetails700_1.Text = BMT700.MixedPaymentDetails1;
                                txtMixedPaymentDetails700_2.Text = BMT700.MixedPaymentDetails2;
                                txtMixedPaymentDetails700_3.Text = BMT700.MixedPaymentDetails3;
                                txtMixedPaymentDetails700_4.Text = BMT700.MixedPaymentDetails4;

                                txtDeferredPaymentDetails700_1.Text = BMT700.DeferredPaymentDetails1;
                                txtDeferredPaymentDetails700_2.Text = BMT700.DeferredPaymentDetails2;
                                txtDeferredPaymentDetails700_3.Text = BMT700.DeferredPaymentDetails3;
                                txtDeferredPaymentDetails700_4.Text = BMT700.DeferredPaymentDetails4;

                                txtShipmentPeriod700_1.Text = BMT700.ShipmentPeriod1;
                                txtShipmentPeriod700_2.Text = BMT700.ShipmentPeriod2;
                                txtShipmentPeriod700_3.Text = BMT700.ShipmentPeriod3;
                                txtShipmentPeriod700_4.Text = BMT700.ShipmentPeriod4;
                                txtShipmentPeriod700_5.Text = BMT700.ShipmentPeriod5;
                                txtShipmentPeriod700_6.Text = BMT700.ShipmentPeriod6;
                            }
                        }
                    }
                    else {
                        lblError.Text = "This LC can not find.";
                    }
                }
            }
            //load tab charge
            var dsCharge = entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault();
            if (dsCharge != null)
            {
                tbChargeRemarks.Text = dsCharge.ChargeRemarks;
                tbVatNo.Text = dsCharge.VATNo;
                //tbChargeCode.SelectedValue = dsCharge.Chargecode??"";
                rcbChargeCcy.SelectedValue = dsCharge.ChargeCcy??"";
                if (dsCharge.ChargeCcy != null && dsCharge.ChargeCcy.Count() > 0)
                {
                    LoadChargeAcct(ref rcbChargeAcct);
                    LoadChargeAcct(ref rcbChargeAcct2);
                    LoadChargeAcct(ref rcbChargeAcct3);
                }
                comboWaiveCharges.SelectedValue = dsCharge.WaiveCharges??"";
                rcbChargeAcct.SelectedValue = dsCharge.ChargeAcct??"";
                tbChargeAmt.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged.SelectedValue = dsCharge.PartyCharged??"";
                rcbOmortCharge.SelectedValue = dsCharge.OmortCharges??"";
                rcbChargeStatus.SelectedValue = dsCharge.ChargeStatus??"";
                lblChargeStatus.Text = dsCharge.ChargeStatus;
                lblTaxCode.Text = dsCharge.TaxCode;
                lblTaxAmt.Text = dsCharge.TaxAmt;
                //
                //tbChargeCode2.SelectedValue = dsCharge.Chargecode??"";
                rcbChargeCcy2.SelectedValue = dsCharge.ChargeCcy??"";
                rcbChargeAcct2.SelectedValue = dsCharge.ChargeAcct;
                tbChargeAmt2.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged2.SelectedValue = dsCharge.PartyCharged??"";
                rcbOmortCharge2.SelectedValue = dsCharge.OmortCharges??"";
                rcbChargeStatus2.SelectedValue = dsCharge.ChargeStatus??"";
                lblTaxCode2.Text = dsCharge.TaxCode;
                lblTaxAmt2.Text = dsCharge.TaxAmt;
                //
                //tbChargeCode3.SelectedValue = dsCharge.Chargecode??"";
                rcbChargeCcy3.SelectedValue = dsCharge.ChargeCcy??"";
                rcbChargeAcct3.SelectedValue = dsCharge.ChargeAcct??"";
                tbChargeAmt3.Text = dsCharge.ChargeAmt.ToString();
                rcbPartyCharged3.SelectedValue = dsCharge.PartyCharged??"";
                rcbOmortCharge3.SelectedValue = dsCharge.OmortCharges??"";
                rcbChargeStatus3.SelectedValue = dsCharge.ChargeStatus??"";
                lblTaxCode3.Text = dsCharge.TaxCode;
                lblTaxAmt3.Text = dsCharge.TaxAmt;
            }
            else
            {
                tbChargeRemarks.Text = string.Empty;
                tbVatNo.Text = string.Empty;
                //tbChargeCode.SelectedValue = string.Empty;
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
                //tbChargeCode2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                rcbChargeAcct2.SelectedValue = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = string.Empty;
                rcbOmortCharge2.SelectedValue = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;
                //
                //tbChargeCode3.SelectedValue = string.Empty;
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
            divAcceptLC.Visible = false;
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
            var dsChargeCode = new List<BCHARGECODE>();
            if (TabId == TabIssueLCAmend)
            {
                dsChargeCode = entContext.BCHARGECODEs.Where(x => x.AmendELC == "X").ToList();
            }
            else if (TabId == TabIssueLCCancel)
            {
                dsChargeCode = entContext.BCHARGECODEs.Where(x => x.CancelELC == "X").ToList();
            }
            else
            {
                dsChargeCode = entContext.BCHARGECODEs.Where(x => x.AdviseELC == "X").ToList();
            }
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("ID");
            tbl1.Columns.Add("Code");
            foreach (var item in dsChargeCode)
            {
                tbl1.Rows.Add(item.Code, item.Name_EN);
            }

            DataSet datasource = new DataSet();//Tab1
            datasource.Tables.Add(tbl1);

            var lstChargeCode = new List<string>();
            if (dsChargeCode != null)
            {
                tbChargeCode.Items.Clear();
                tbChargeCode.Items.Add(new RadComboBoxItem(""));
                tbChargeCode.DataValueField = "ID";
                tbChargeCode.DataTextField = "ID";
                tbChargeCode.DataSource = datasource;
                tbChargeCode.DataBind();


                tbChargeCode2.Items.Clear();
                tbChargeCode2.Items.Add(new RadComboBoxItem(""));
                tbChargeCode2.DataValueField = "ID";
                tbChargeCode2.DataTextField = "ID";
                tbChargeCode2.DataSource = datasource;
                tbChargeCode2.DataBind();

                tbChargeCode3.Items.Clear();
                tbChargeCode3.Items.Add(new RadComboBoxItem(""));
                tbChargeCode3.DataValueField = "ID";
                tbChargeCode3.DataTextField = "ID";
                tbChargeCode3.DataSource = datasource;
                tbChargeCode3.DataBind();

                if (TabId == TabIssueLCAmend)
                {
                    tbChargeCode.SelectedValue = "ELC.ADAMEND";
                    tbChargeCode.Enabled = false;
                    tbChargeCode2.SelectedValue = "ELC.CONFIRM";
                    tbChargeCode2.Enabled = false;
                    tbChargeCode3.SelectedValue = "ELC.OTHER";
                    tbChargeCode3.Enabled = false;
                }
                else if (TabId == TabIssueLCCancel)
                {
                    tbChargeCode.SelectedValue = "ELC.CANCEL";
                    tbChargeCode.Enabled = false;
                    tbChargeCode2.SelectedValue = "ELC.COURIER";
                    tbChargeCode2.Enabled = false;
                    tbChargeCode3.SelectedValue = "ELC.OTHER";
                    tbChargeCode3.Enabled = false;

                }
                else
                {
                    tbChargeCode.SelectedValue = "ELC.ADVISE";
                    tbChargeCode.Enabled = false;
                    tbChargeCode2.SelectedValue = "ELC.CONFIRM";
                    tbChargeCode2.Enabled = false;
                    tbChargeCode3.SelectedValue = "ELC.OTHER";
                    tbChargeCode3.Enabled = false;

                    
                }
                //set currency
                rcbChargeCcy.Items.Clear();
                rcbChargeCcy.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy.DataValueField = "Code";
                rcbChargeCcy.DataTextField = "Code";
                rcbChargeCcy.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy.DataBind();
                rcbChargeCcy.Items.Insert(0, new RadComboBoxItem { Text = string.Empty, Value = "-1" });
                rcbChargeCcy.SelectedIndex = 0;

                rcbChargeCcy2.Items.Clear();
                rcbChargeCcy2.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy2.DataValueField = "Code";
                rcbChargeCcy2.DataTextField = "Code";
                rcbChargeCcy2.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy2.DataBind();
                rcbChargeCcy2.Items.Insert(0, new RadComboBoxItem { Text = string.Empty, Value = "-1" });
                rcbChargeCcy2.SelectedIndex = 0;

                rcbChargeCcy3.Items.Clear();
                rcbChargeCcy3.Items.Add(new RadComboBoxItem(""));
                rcbChargeCcy3.DataValueField = "Code";
                rcbChargeCcy3.DataTextField = "Code";
                rcbChargeCcy3.DataSource = entContext.BCURRENCies.ToList();
                rcbChargeCcy3.DataBind();
                rcbChargeCcy3.Items.Insert(0, new RadComboBoxItem { Text = string.Empty, Value = "-1" });
                rcbChargeCcy3.SelectedIndex = 0;
                //LOAD PARTYCHARGED
                LoadDataSourceComboPartyCharged();
                //load ComboBox Confirm
                var lstconfirm = entContext.B_AddConfirmInfos.ToList();
                if (lstconfirm != null)
                {
                    foreach (var item in lstconfirm)
                    {
                        RadComboBoxItem raditem = new RadComboBoxItem();
                        raditem.Text = item.ConfirmName;
                        raditem.Value = item.ConfirmID;
                        ComboConfirmInstr.Items.Add(raditem);
                    }
                }
                
            }
        }
        
        protected void LoadChargeAcct2(ref RadComboBox cboChargeAcct)
        {
            
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtDraweeCusName.Text ?? "XXXXX", comboDraweeCusNo700.SelectedValue));
        }

        protected void LoadChargeAcct3(ref RadComboBox cboChargeAcct)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtDraweeCusName.Text ?? "XXXXX", comboDraweeCusNo700.SelectedValue));
        }
        protected void LoadDataChargeAcct()
        {
            var dataCharge = entContext.BAdvisingAndNegotiationLCCharges.Where(dr => dr.DocCollectCode == tbEssurLCCode.Text).FirstOrDefault();
            if (dataCharge != null)
            {
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                comboWaiveCharges.SelectedValue = dataCharge.WaiveCharges;
                rcbChargeAcct.SelectedValue = dataCharge.ChargeAcct;

                    //tbChargePeriod.Text = drow1["ChargePeriod"];
                rcbChargeCcy.SelectedValue = dataCharge.ChargeCcy;
                tbChargeAmt.Text = dataCharge.ChargeAmt.ToString();
                    rcbPartyCharged.SelectedValue = dataCharge.PartyCharged;
                    lblPartyCharged.Text = dataCharge.PartyCharged;
                    rcbOmortCharge.SelectedValue = dataCharge.OmortCharges;
                    rcbChargeStatus.SelectedValue = dataCharge.ChargeStatus;
                    lblChargeStatus.Text = dataCharge.ChargeStatus;

                    tbChargeRemarks.Text = dataCharge.ChargeRemarks;
                    tbVatNo.Text = dataCharge.VATNo;
                    lblTaxCode.Text = dataCharge.TaxCode;
                    //lblTaxCcy.Text = drow1["TaxCcy"];
                    lblTaxAmt.Text = dataCharge.TaxAmt;

                    //tbChargeCode.SelectedValue = dataCharge.Chargecode;

                    //ChargeAmount += ConvertStringToFloat(drow1["ChargeAmt"]);
                

            }
        }
        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct);
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
        //
        protected void LoadDataSourceComboPartyCharged()
        {
            var dtSource = SQLData.CreateGenerateDatas("PartyCharged");

            rcbPartyCharged.Items.Clear();
            rcbPartyCharged.DataValueField = "Id";
            rcbPartyCharged.DataTextField = "Id";
            rcbPartyCharged.DataSource = dtSource;
            rcbPartyCharged.DataBind();
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];

            rcbPartyCharged2.Items.Clear();
            rcbPartyCharged2.DataValueField = "Id";
            rcbPartyCharged2.DataTextField = "Id";
            rcbPartyCharged2.DataSource = dtSource;
            rcbPartyCharged2.DataBind();
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];

            rcbPartyCharged3.Items.Clear();
            rcbPartyCharged3.DataValueField = "Id";
            rcbPartyCharged3.DataTextField = "Id";
            rcbPartyCharged3.DataSource = dtSource;
            rcbPartyCharged3.DataBind();
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];

            
        }
        //
        protected void rcbChargeAcct3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct3);
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
            LoadChargeAcct(ref rcbChargeAcct2);
        }
        protected void LoadChargeAcct(ref RadComboBox cboChargeAcct)
        {
            var obj = entContext.BDRFROMACCOUNTs.Where(x => x.CustomerID == txtBeneficiaryNo700.Text && x.Currency == "USD").FirstOrDefault();
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("Id");
            tbl1.Columns.Add("Name");
            tbl1.Rows.Add(obj.Id, obj.Name);

            cboChargeAcct.Items.Clear();

            DataSet datasource = new DataSet();//Tab1
            datasource.Tables.Add(tbl1);
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id",datasource);
            

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
                            ori.CancelStatus = "UNA";
                        }
                        else if (TabId == TabIssueLCAmend)
                        {
                            ori.AmendStatus = "UNA";
                        }
                        else if (TabId == TabIssueLCClose)
                        {
                            ori.CloseStatus = "UNA";
                        }
                        else if (TabId == TabIssueLCConfirm)
                        {
                            ori.GenerateDelivery = RadComboBoxGD.SelectedValue;
                            ori.Date = DateConfirm.SelectedDate;
                            ori.ExternalReference = txtExternalReference.Text;
                            ori.ConfirmationInstr = ComboConfirmInstr.SelectedValue;
                            ori.LimitRef = txtLimitRef.Text;
                            ori.AcceptStatus = "UNA";
                        }
                        else
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
                        if (status == "REV")
                        {
                            obj.CancelStatus = status;
                        }
                        else
                        {
                            obj.CancelStatus = status;
                            obj.CancelBy = UserId.ToString();
                        }
                        break;
                    case AdvisingAndNegotiationScreenType.Close:
                        if (status == "REV")
                        {
                            obj.CloseStatus = status;
                        }
                        else
                        {
                            obj.CloseStatus = status;
                            obj.CloseBy = UserId.ToString();
                        }
                        break;
                    case AdvisingAndNegotiationScreenType.Acception:
                        if (status == "REV")
                        {
                            obj.AcceptStatus = status;
                        }
                        else
                        {
                            obj.AcceptStatus = status;
                            obj.AcceptBy = UserId.ToString();
                        }
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