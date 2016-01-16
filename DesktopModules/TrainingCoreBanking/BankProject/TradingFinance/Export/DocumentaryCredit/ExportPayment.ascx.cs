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
using BankProject.Model;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ExportPayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
        private const int tabSightPayment = 246;
        private const int tabMatureAcceptance = 247;
        private const double VAT = 0.1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            //
            cboDrawType.Items.Clear();
            switch (this.TabId)
            {
                case tabSightPayment:
                    cboDrawType.Items.Add(new RadComboBoxItem("Sight Payment", "SP"));
                    break;
                case tabMatureAcceptance:
                    cboDrawType.Items.Add(new RadComboBoxItem("Maturity Acceptance", "MA"));
                    break;
            }
            //load datasource from BAccounts
            InitSourceForDepositAccount();
            bc.Commont.initRadComboBox(ref cboPaymentMethod, "Description", "Code", bd.IssueLC.PaymentMethod());

            DataTable tblList;//* = bd.SQLData.B_BCHARGECODE_GetByViewType(this.TabId);

            //ChargeCcy
            tblList = bd.SQLData.B_BCURRENCY_GetAll().Tables[0]; //bd.Database.ExchangeRate();
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref comboCurrency, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref comboCurrency_MT400, "Code", "Code", tblList);

            //remove "GOLD" from list of currency
            bc.Commont.removeCurrencyItem(comboCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(comboCurrency_MT400, "GOLD");

            //Party Charged
            tblList = createTableList();
            addData2TableList(ref tblList, "A");
            addData2TableList(ref tblList, "AC");
            addData2TableList(ref tblList, "B");
            addData2TableList(ref tblList, "BC");
            bc.Commont.initRadComboBox(ref tabCableCharge_cboPartyCharged, "Text", "Value", tblList);
            tabCableCharge_cboPartyCharged.SelectedValue = "BC";
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboPartyCharged, "Text", "Value", tblList);
            tabPaymentCharge_cboPartyCharged.SelectedValue = "BC";
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboPartyCharged, "Text", "Value", tblList);
            tabHandlingCharge_cboPartyCharged.SelectedValue = "BC";
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboPartyCharged, "Text", "Value", tblList);
            tabDiscrepenciesCharge_cboPartyCharged.SelectedValue = "BC";
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboPartyCharged, "Text", "Value", tblList);
            tabOtherCharge_cboPartyCharged.SelectedValue = "BC";
            //Amort Charges
            tblList = createTableList();
            addData2TableList(ref tblList, bd.YesNo.NO);
            addData2TableList(ref tblList, bd.YesNo.YES);
            bc.Commont.initRadComboBox(ref tabCableCharge_cboAmortCharge, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboAmortCharge, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboAmortCharge, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboAmortCharge, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboAmortCharge, "Text", "Value", tblList);
            //Charge Status
            tblList = createTableList();
            addData2TableList(ref tblList, "2 - CHARGE COLECTED", "CHARGE COLECTED");
            addData2TableList(ref tblList, "3 - CHARGE UNCOLECTED", "CHARGE UNCOLECTED");
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeStatus, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeStatus, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeStatus, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeStatus, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeStatus, "Text", "Value", tblList);
            //
            bc.Commont.initRadComboBox(ref cboPaymentMethod, "Description", "Code", bd.IssueLC.PaymentMethod());
            //
            if (!String.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                if (Request.QueryString["tid"] != null)
                {
                    var tid = long.Parse(Request.QueryString["tid"].ToString());
                    var Code = entContext.B_ExportLCPayments.Where(x => x.Id == tid).FirstOrDefault();
                    if (Code != null)
                    {
                        LoadDetail(Code.LCCode);
                    }
                }
                if (!String.IsNullOrEmpty(Request.QueryString["lst"]))
                    setToolbar(2);
            }
        }
        protected void tabOtherCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabOtherCharge_txtChargeAmt, tabOtherCharge_cboPartyCharged, ref tabOtherCharge_txtTaxAmt, ref tabOtherCharge_txtTaxCode);
        }
        protected void tabOtherCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabOtherCharge_txtChargeAmt, tabOtherCharge_cboPartyCharged, ref tabOtherCharge_txtTaxAmt, ref tabOtherCharge_txtTaxCode);
        }
        protected void tabHandlingCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabHandlingCharge_txtChargeAmt, tabHandlingCharge_cboPartyCharged, ref tabHandlingCharge_txtTaxAmt, ref tabHandlingCharge_txtTaxCode);
        }
        protected void tabHandlingCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabHandlingCharge_txtChargeAmt, tabHandlingCharge_cboPartyCharged, ref tabHandlingCharge_txtTaxAmt, ref tabHandlingCharge_txtTaxCode);
        }
        protected void tabDiscrepenciesCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabDiscrepenciesCharge_txtChargeAmt, tabDiscrepenciesCharge_cboPartyCharged, ref tabDiscrepenciesCharge_txtTaxAmt, ref tabDiscrepenciesCharge_txtTaxCode);
        }
        protected void tabDiscrepenciesCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabDiscrepenciesCharge_txtChargeAmt, tabDiscrepenciesCharge_cboPartyCharged, ref tabDiscrepenciesCharge_txtTaxAmt, ref tabDiscrepenciesCharge_txtTaxCode);
        }
        private DataTable createTableList()
        {
            DataTable tblList = new DataTable();
            tblList.Columns.Add(new DataColumn("Value", typeof(string)));
            tblList.Columns.Add(new DataColumn("Text", typeof(string)));

            return tblList;
        }
        private void addData2TableList(ref DataTable tblList, string text)
        {
            addData2TableList(ref tblList, text, text);
        }
        private void addData2TableList(ref DataTable tblList, string text, string value)
        {
            DataRow dr = tblList.NewRow();
            dr["Value"] = text;
            dr["Text"] = value;
            tblList.Rows.Add(dr);
        }
        protected void tabPaymentCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabPaymentCharge_txtChargeAmt, tabPaymentCharge_cboPartyCharged, ref tabPaymentCharge_txtTaxAmt, ref tabPaymentCharge_txtTaxCode);
        }
        protected void tabPaymentCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabPaymentCharge_txtChargeAmt, tabPaymentCharge_cboPartyCharged, ref tabPaymentCharge_txtTaxAmt, ref tabPaymentCharge_txtTaxCode);
        }
        protected void tabCableCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabCableCharge_txtChargeAmt, tabCableCharge_cboPartyCharged, ref tabCableCharge_txtTaxAmt, ref tabCableCharge_txtTaxCode);
        }
        protected void tabCableCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabCableCharge_txtChargeAmt, tabCableCharge_cboPartyCharged, ref tabCableCharge_txtTaxAmt, ref tabCableCharge_txtTaxCode);
        }
        //
        protected void tabCableCharge_cboChargeCcy_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, tabCableCharge_cboChargeCcy.SelectedValue));
        }
        protected void tabPaymentCharge_cboChargeCcy_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, tabPaymentCharge_cboChargeCcy.SelectedValue));
        }
        protected void tabHandlingCharge_cboChargeCcy_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, tabHandlingCharge_cboChargeCcy.SelectedValue));
        }
        protected void tabDiscrepenciesCharge_cboChargeCcy_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, tabDiscrepenciesCharge_cboChargeCcy.SelectedValue));
        }
        protected void tabOtherCharge_cboChargeCcy_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, tabOtherCharge_cboChargeCcy.SelectedValue));
        }
        //
        protected void btnLoadDocsInfo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (txtCode.Text.IndexOf('.') == -1)
            {
                lblError.Text = "This Docs was not found";
            }
            else
            {
                //bind tab charge cbo acc
                var lstAcc = txtCode.Text.Split('.');
                if (lstAcc != null)
                {
                    var name = lstAcc[0].ToString();
                    var bindCharge = entContext.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == name).FirstOrDefault();
                    if (bindCharge != null)
                    {
                        txtCustomerName.Value = bindCharge.BeneficiaryName;
                        bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                        bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                        bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                        bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                        bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                    }
                }
                //
                var dsDetails = entContext.B_ExportLCPayments.Where(x => x.LCCode == txtCode.Text).FirstOrDefault();
                if (dsDetails == null)
                {
                    var lstDetails = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).ToList();
                    if (lstDetails == null)
                    {
                        lblError.Text = "This Docs was not found";
                        return;
                    }
                    else
                    {
                        var drDetails=new BEXPORT_DOCUMENTPROCESSING();
                        if (lstDetails.Count == 1)
                        {
                            drDetails = lstDetails[0];
                        }
                        else
                        {
                            drDetails = lstDetails.Where(x => x.ActiveRecordFlag == YesNo.YES).FirstOrDefault();
                        }
                        //kiem tra lai Status !AUT, RejectStatus 
                        if (drDetails.Status != "AUT")
                        {
                            lblError.Text = "This Docs has wrong Status (" + drDetails.Status + ") !";
                            return;
                        }
                        else {
                            if (drDetails.RejectStatus != null)
                            {
                                if (!drDetails.RejectStatus.ToString().Equals(bd.TransactionStatus.REV) && !drDetails.RejectStatus.ToString().Equals(bd.TransactionStatus.AUT))
                                {
                                    lblError.Text = "This Docs is waiting for reject !";
                                    return;
                                }
                            }
                            if (Convert.ToInt32(drDetails.PaymentFullFlag) != 0)
                            {
                                lblError.Text = "This Doc is already payment completed !";
                                return;
                            }
                        }
                        //
                        lblCurrency.Text = drDetails.Currency;
                        if (drDetails.Amount != null)
                        {
                            txtDrawingAmount.Value = drDetails.Amount;
                        }
                        txtValueDate.SelectedDate = drDetails.BookingDate;
                        //cboDepositAccount.SelectedValue=drDetails.a
                        bc.Commont.initRadComboBox(ref cboNostroAcct, "Description", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(drDetails.Currency));
                        txtAmountCredited.Value = 0;
                        txtFullyUtilised.Text = bd.YesNo.NO;
                        //MT202
                        lblTransactionReferenceNumber.Text = txtCode.Text;
                        txtRelatedReference.Text = drDetails.PresentorNo;
                        dteValueDate_MT202.SelectedDate = DateTime.Now;
                        setCurrency(ref comboCurrency, lblCurrency.Text);
                        numAmount.Value = txtDrawingAmount.Value;
                        //MT756
                        txtSendingBankTRN.Text = txtCode.Text;
                        txtRelatedReferenceMT400.Text = drDetails.PresentorNo;
                        numAmountCollected.Value = txtDrawingAmount.Value;
                        dteValueDate_MT400.SelectedDate = DateTime.Now;
                        setCurrency(ref comboCurrency_MT400, lblCurrency.Text);
                        numAmount_MT400.Value = txtDrawingAmount.Value;
                        txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
                        //Charge
                        txtVatNo.Text = bd.IssueLC.GetVatNo();
                        //
                        setToolbar(1);

                        return;
                    }
                }
                else
                { 
                    //xet Status 
                    if (dsDetails.Status.ToString().Equals(bd.TransactionStatus.AUT) || dsDetails.Status.ToString().Equals(bd.TransactionStatus.REV))
                    {
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        setToolbar(0);
                    }
                    else
                    {
                        setToolbar(1);
                    }

                    //tab Main
                    cboDrawType.SelectedValue = dsDetails.DrawType;
                    lblCurrency.Text = dsDetails.Currency;
                    if (dsDetails.WaiveCharges != null)
                    {
                        cboWaiveCharges.SelectedValue = dsDetails.WaiveCharges;
                    }
                    txtChargeRemarks.Text = dsDetails.ChargeRemarks;
                    txtVatNo.Text = dsDetails.VATNo;
                    if (dsDetails.DrawingAmount != null)
                    {
                        txtDrawingAmount.Value = dsDetails.DrawingAmount;
                    }
                    if (dsDetails.UpdatedDate != null)
                    {
                        txtValueDate.SelectedDate = dsDetails.UpdatedDate;
                    }
                    if (dsDetails.DepositAccount != null)
                    {
                        cboDepositAccount.SelectedValue = dsDetails.DepositAccount;
                    }
                    if (dsDetails.ExchangeRate != null)
                    {
                        txtExchangeRate.Value = dsDetails.ExchangeRate;
                    }
                    txtAmtDRFrAcctCcy.Text = dsDetails.AmtDRFrAcctCcy;
                    if (dsDetails.ProvAmtRelease != null)
                    {
                        txtProvAmtRelease.Value = dsDetails.ProvAmtRelease;
                    }
                    txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
                    if (dsDetails.PaymentMethod != null)
                    {
                        cboPaymentMethod.SelectedValue = dsDetails.PaymentMethod;
                    }
                    if (dsDetails.NostroAcct != null)
                    {
                        cboNostroAcct.SelectedValue = dsDetails.NostroAcct;
                    }
                    if (dsDetails.AmountCredited != null)
                    {
                        txtAmountCredited.Value = dsDetails.AmountCredited;
                    }
                    txtPaymentRemarks.Text = dsDetails.PaymentRemarks;
                    txtFullyUtilised.Text = dsDetails.FullyUtilised;
                    //bind tab MT202
                    var dsMT202 = entContext.B_ExportLCPaymentMT202s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
                    if (dsMT202 != null)
                    {
                        lblTransactionReferenceNumber.Text = dsMT202.TransactionReferenceNumber;
                        txtRelatedReference.Text = dsMT202.RelatedReference;
                        if (dsMT202.ValueDate!=null)
                        {
                            dteValueDate_MT202.SelectedDate = dsMT202.ValueDate;
                        }
                        if (dsMT202.Currency != null)
                        {
                            comboCurrency.SelectedValue = dsMT202.Currency;
                        }
                        if (dsMT202.Amount != null)
                        {
                            numAmount.Value = dsMT202.Amount;
                        }
                        lblOrderingInstitution.Text = dsMT202.OrderingInstitution;
                        lblSenderCorrespondent1.Text = dsMT202.SenderCorrespondent1;
                        lblSenderCorrespondent2.Text = dsMT202.SenderCorrespondent2;
                        lblReceiverCorrespondentName2.Text = dsMT202.ReceiverCorrespondent1;
                        if (dsMT202.IntermediaryBankType != null)
                        {
                            comboIntermediaryBankType.SelectedValue = dsMT202.IntermediaryBankType;
                        }
                        txtIntermediaryBank.Text = dsMT202.IntermediaryBank;
                        txtIntermediaryBankName.Text = dsMT202.IntermediaryBankName;
                        txtIntermediaryBankAddr1.Text = dsMT202.IntermediaryBankAddr1;
                        txtIntermediaryBankAddr2.Text = dsMT202.IntermediaryBankAddr2;
                        txtIntermediaryBankAddr3.Text = dsMT202.IntermediaryBankAddr3;
                        if (dsMT202.AccountWithInstitutionType != null)
                        {
                            comboAccountWithInstitutionType.SelectedValue = dsMT202.AccountWithInstitutionType;
                        }
                        txtAccountWithInstitution.Text = dsMT202.AccountWithInstitution;
                        txtAccountWithInstitutionName.Text = dsMT202.AccountWithInstitutionName;
                        txtAccountWithInstitutionAddr1.Text = dsMT202.AccountWithInstitutionAddr1;
                        txtAccountWithInstitutionAddr2.Text = dsMT202.AccountWithInstitutionAddr2;
                        txtAccountWithInstitutionAddr3.Text = dsMT202.AccountWithInstitutionAddr3;
                        if (dsMT202.BeneficiaryBankType != null)
                        {
                            comboBeneficiaryBankType.SelectedValue = dsMT202.BeneficiaryBankType;
                        }
                        txtBeneficiaryBank.Text = dsMT202.BeneficiaryBank;
                        txtBeneficiaryBankName.Text = dsMT202.BeneficiaryBankName;
                        txtBeneficiaryBankAddr1.Text = dsMT202.BeneficiaryBankAddr1;
                        txtBeneficiaryBankAddr2.Text = dsMT202.BeneficiaryBankAddr2;
                        txtBeneficiaryBankAddr3.Text = dsMT202.BeneficiaryBankAddr3;
                        txtSenderToReceiverInformation.Text = dsMT202.SenderToReceiverInformation;
                        txtSenderToReceiverInformation2.Text = dsMT202.SenderToReceiverInformation2;
                        txtSenderToReceiverInformation3.Text = dsMT202.SenderToReceiverInformation3;
                    }
                    //tab MT756
                    var dsMT756 = entContext.B_ExportLCPaymentMT756s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
                    if (dsMT756 != null)
                    {
                        comboCreateMT756.SelectedValue = bd.YesNo.YES;
                        txtRelatedReferenceMT400.Text = dsMT756.RelatedReference;
                        txtSendingBankTRN.Text = dsMT756.SendingBankTRN;
                        if (dsMT756.AmountCollected != null)
                        {
                            numAmountCollected.Value = dsMT756.AmountCollected;
                        }
                        if (dsMT756.ValueDate != null)
                        {
                            dteValueDate_MT400.SelectedDate = dsMT756.ValueDate;
                        }
                        if (dsMT756.Currency != null)
                        {
                            comboCurrency_MT400.SelectedValue = dsMT756.Currency;
                        }
                        if (dsMT756.Amount != null)
                        {
                            numAmount_MT400.Value = dsMT756.Amount;
                        }
                        if (dsMT756.SenderCorrespondentType != null)
                        {
                            comboSenderCorrespondentType.SelectedValue = dsMT756.SenderCorrespondentType;
                        }
                        txtSenderCorrespondentNo.Text = dsMT756.SenderCorrespondentNo;
                        txtSenderCorrespondentName.Text = dsMT756.SenderCorrespondentName;
                        txtSenderCorrespondentAddress1.Text = dsMT756.SenderCorrespondentAddr1;
                        txtSenderCorrespondentAddress2.Text = dsMT756.SenderCorrespondentAddr2;
                        txtSenderCorrespondentAddress3.Text = dsMT756.SenderCorrespondentAddr3;
                        if (dsMT756.ReceiverCorrespondentType != null)
                        {
                            comboReceiverCorrespondentType.SelectedValue = dsMT756.ReceiverCorrespondentType;
                        }
                        txtReceiverCorrespondentNo.Text = dsMT756.ReceiverCorrespondentNo;
                        txtReceiverCorrespondentName.Text = dsMT756.ReceiverCorrespondentName;
                        txtReceiverCorrespondentAddr1.Text = dsMT756.ReceiverCorrespondentAddr1;
                        txtReceiverCorrespondentAddr2.Text = dsMT756.ReceiverCorrespondentAddr2;
                        txtReceiverCorrespondentAddr3.Text = dsMT756.ReceiverCorrespondentAddr3;
                        txtDetailOfCharges1.Text = dsMT756.DetailOfCharges1;
                        txtDetailOfCharges2.Text = dsMT756.DetailOfCharges2;
                        txtDetailOfCharges3.Text = dsMT756.DetailOfCharges3;
                        txtSenderToReceiverInformation1_400_1.Text = dsMT756.SenderToReceiverInformation1;
                        txtSenderToReceiverInformation1_400_2.Text = dsMT756.SenderToReceiverInformation2;
                        txtSenderToReceiverInformation1_400_3.Text = dsMT756.SenderToReceiverInformation3;
                        //tab charge
                        var dsCharge = entContext.B_ExportLCPaymentCharges.Where(x => x.PaymentId == dsMT756.PaymentId).ToList();
                        foreach (var item in dsCharge)
                        {
                            if (item.ChargeCode == "ILC.CABLE")
                            {
                                tabCableCharge.Style.Add("display", "block");
                                tabCableCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                                tabCableCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                                bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                                
                                if (item.ExchangeRate != null)
                                {
                                    tabCableCharge_txtExchangeRate.Value = item.ExchangeRate;
                                }
                                if (item.ChargeAmt != null)
                                {
                                    tabCableCharge_txtChargeAmt.Value = item.ChargeAmt;
                                }
                                if (item.PartyCharged != null)
                                {
                                    tabCableCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                                }
                                if (item.AmortCharge != null)
                                {
                                    tabCableCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                                }
                                tabCableCharge_txtTaxCode.Text = item.TaxCode;
                                lblTaxCcy.Text = item.ChargeCcy;
                                tabCableCharge_txtTaxAmt.Value = item.TaxAmt;
                            }
                            if (item.ChargeCode == "ILC.PAYMENT")
                            {
                                tabPaymentCharge.Style.Add("display", "block");
                                tabPaymentCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                                tabPaymentCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                                bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                               
                                if (item.ExchangeRate != null)
                                {
                                    tabPaymentCharge_txtExchangeRate.Value = item.ExchangeRate;
                                }
                                if (item.PartyCharged != null)
                                {
                                    tabPaymentCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                                }
                                if (item.AmortCharge != null)
                                {
                                    tabPaymentCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                                }
                                if (item.ChargeStatus != null)
                                {
                                    tabPaymentCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                                }
                                tabPaymentCharge_txtTaxCode.Text = item.TaxCode;
                                if (item.TaxAmt != null)
                                {
                                    tabPaymentCharge_txtTaxAmt.Value = item.TaxAmt;
                                }

                            }
                            if (item.ChargeCode == "ILC.HANDLING")
                            {
                                bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                               
                                tabHandlingCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                                if (item.ChargeCcy != null)
                                {
                                    tabHandlingCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                                }
                                if (item.ExchangeRate != null)
                                {
                                    tabHandlingCharge_txtExchangeRate.Value = item.ExchangeRate;
                                }
                                if(item.ChargeAmt!=null)
                                {
                                    tabHandlingCharge_txtChargeAmt.Value = item.ChargeAmt;
                                }
                                if (item.PartyCharged != null)
                                {
                                    tabHandlingCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                                }
                                if (item.AmortCharge != null)
                                {
                                    tabHandlingCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                                }
                                tabHandlingCharge_cboChargeStatus.Text = item.ChargeStatus;
                                tabHandlingCharge_txtTaxCode.Text = item.TaxCode;
                                if (item.TaxAmt != null)
                                {
                                    tabHandlingCharge_txtTaxAmt.Value = item.TaxAmt;
                                }
                            }
                            if (item.ChargeCode == "ILC.DISCRP")
                            {
                                tabDiscrepenciesCharge.Style.Add("display", "block");
                                tabDiscrepenciesCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                                bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                                
                                if (item.ChargeCcy != null)
                                {
                                    tabDiscrepenciesCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                                }
                                if (item.ExchangeRate != null)
                                {
                                    tabDiscrepenciesCharge_txtExchangeRate.Value = item.ExchangeRate;
                                }
                                if (item.ChargeAmt != null)
                                {
                                    tabDiscrepenciesCharge_txtChargeAmt.Value = item.ChargeAmt;
                                }
                                if(item.PartyCharged!=null)
                                {
                                    tabDiscrepenciesCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                                }
                                if (item.AmortCharge != null)
                                {
                                    tabDiscrepenciesCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                                }
                                if (item.ChargeStatus != null)
                                {
                                    tabDiscrepenciesCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                                }
                                tabDiscrepenciesCharge_txtTaxCode.Text = item.TaxCode;
                                if (item.TaxAmt != null)
                                {
                                    tabDiscrepenciesCharge_txtTaxAmt.Value = item.TaxAmt;
                                }
                            }
                            if (item.ChargeCode == "ILC.OTHER")
                            {
                                tabOtherCharge.Style.Add("display", "block");
                                tabCableCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                                bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                                if (item.ChargeCcy != null)
                                {
                                    tabOtherCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                                }
                                if (item.ExchangeRate != null)
                                {
                                    tabOtherCharge_txtExchangeRate.Value = item.ExchangeRate;
                                }
                                if (item.ChargeAmt != null)
                                {
                                    tabOtherCharge_txtChargeAmt.Value = item.ChargeAmt;
                                }
                                if (item.PartyCharged != null)
                                {
                                    tabOtherCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                                }
                                if (item.AmortCharge != null)
                                {
                                    tabOtherCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                                }
                                if (item.ChargeStatus != null)
                                {
                                    tabOtherCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                                }
                                tabOtherCharge_txtTaxCode.Text = item.TaxCode;
                                if (item.TaxAmt != null)
                                {
                                    tabOtherCharge_txtTaxAmt.Value = item.TaxAmt;
                                }
                            }
                        }
                    }
                }
                setToolbar(1);
                
            }
        }
        private void setToolbar(int commandType)
        {
            RadToolBar1.FindItemByValue("btCommit").Enabled = (commandType == 1);
            //RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = (commandType == 2);
            RadToolBar1.FindItemByValue("btReverse").Enabled = (commandType == 2);
            //RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = (commandType > 1);
        }
        private void setCurrency(ref RadComboBox cboCurrency, string Currency)
        {
            for (int i = 0; i < cboCurrency.Items.Count; i++)
            {
                if (cboCurrency.Items[i].Text.Equals(Currency))
                {
                    cboCurrency.SelectedIndex = i;
                    break;
                }
            }
        }
        private void calculateTaxAmt(RadNumericTextBox txtChargeAmt, RadComboBox cboPartyCharged, ref RadNumericTextBox txtTaxAmt, ref RadTextBox txtTaxCode)
        {
            txtTaxAmt.Text = "";
            txtTaxCode.Text = "";
            if (txtChargeAmt.Value.HasValue)
            {
                //Khong tinh VAT theo y/c nghiep vu !
                //[9/10/2014 10:01:06 PM] Nguyen - Viet Victory: Neu Party Charge la: A hoac B thi Xuat phieu VAT (Charge Phi + 10%VAT)
                //[9/10/2014 10:01:27 PM] Nguyen - Viet Victory: Neu Party Charge la: AC hoac BC thi KHONG Xuat phieu VAT (Charge Phi)
                /*switch (cboPartyCharged.SelectedValue)
                {
                    case "A":
                    case "B":
                        txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value * VAT).Replace("$", "");
                        txtTaxCode.Text = "81      10% VAT on Charge";
                        break;
                    default:
                        //txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value).Replace("$", "");
                        break;
                }*/
            }
            //Tính toán lại Amount Credited
            if (txtDrawingAmount.Value.HasValue)
            {
                double AmountCredited = txtDrawingAmount.Value.Value;
                calculateAmountCredited(tabCableCharge_txtChargeAmt.Value, tabCableCharge_cboPartyCharged.SelectedValue, tabCableCharge_txtTaxAmt.Text, ref AmountCredited);
                calculateAmountCredited(tabPaymentCharge_txtChargeAmt.Value, tabPaymentCharge_cboPartyCharged.SelectedValue, tabPaymentCharge_txtTaxAmt.Text, ref AmountCredited);
                calculateAmountCredited(tabHandlingCharge_txtChargeAmt.Value, tabHandlingCharge_cboPartyCharged.SelectedValue, tabHandlingCharge_txtTaxAmt.Text, ref AmountCredited);
                calculateAmountCredited(tabDiscrepenciesCharge_txtChargeAmt.Value, tabDiscrepenciesCharge_cboPartyCharged.SelectedValue, tabDiscrepenciesCharge_txtTaxAmt.Text, ref AmountCredited);
                calculateAmountCredited(tabOtherCharge_txtChargeAmt.Value, tabOtherCharge_cboPartyCharged.SelectedValue, tabOtherCharge_txtTaxAmt.Text, ref AmountCredited);
                txtAmountCredited.Value = AmountCredited;
                numAmount.Value = AmountCredited;
                numAmount_MT400.Value = AmountCredited;
            }
        }
        private void calculateAmountCredited(double? ChargeAmt, string PartyCharged, string ChargeAmtVat, ref double AmountCredited)
        {
            //[9/10/2014 10:01:06 PM] Nguyen - Viet Victory: Neu Party Charge la: A hoac B thi Xuat phieu VAT (Charge Phi + 10%VAT)
            //[9/10/2014 10:01:27 PM] Nguyen - Viet Victory: Neu Party Charge la: AC hoac BC thi KHONG Xuat phieu VAT (Charge Phi)
            if (ChargeAmt.HasValue)
            {
                if (!string.IsNullOrEmpty(ChargeAmtVat))//VAT
                    ChargeAmt += Convert.ToDouble(ChargeAmtVat);
                switch (PartyCharged)
                {
                    case "B":
                    case "BC":
                        AmountCredited -= ChargeAmt.Value;
                        break;
                    case "A":
                    case "AC":
                        AmountCredited += ChargeAmt.Value;
                        break;
                }
            }
        }
        protected void comboReceiverCorrespondentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isEnable = !comboReceiverCorrespondentType.SelectedValue.Equals("A");
            txtReceiverCorrespondentNo.Enabled = !isEnable;
            txtReceiverCorrespondentName.Enabled = isEnable;
            txtReceiverCorrespondentAddr1.Enabled = isEnable;
            txtReceiverCorrespondentAddr2.Enabled = isEnable;
            txtReceiverCorrespondentAddr3.Enabled = isEnable;
        }

        protected void txtReceiverCorrespondentNo_OnTextChanged(object sender, EventArgs e)
        {
            lblReceiverCorrespondentError.Text = "";
            txtReceiverCorrespondentName.Text = "";
            if (!string.IsNullOrEmpty(txtReceiverCorrespondentNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtReceiverCorrespondentNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtReceiverCorrespondentName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblReceiverCorrespondentError.Text = "No found swiftcode";
                }
            }
        }
        protected void comboSenderCorrespondentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isEnable = !comboSenderCorrespondentType.SelectedValue.Equals("A");
            txtSenderCorrespondentNo.Enabled = !isEnable;
            txtSenderCorrespondentName.Enabled = isEnable;
            txtSenderCorrespondentAddress1.Enabled = isEnable;
            txtSenderCorrespondentAddress2.Enabled = isEnable;
            txtSenderCorrespondentAddress3.Enabled = isEnable;
        }

        protected void txtSenderCorrespondentNo_OnTextChanged(object sender, EventArgs e)
        {
            lblSenderCorrespondentNoError.Text = "";
            txtSenderCorrespondentName.Text = "";
            if (!string.IsNullOrEmpty(txtSenderCorrespondentNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtSenderCorrespondentNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtSenderCorrespondentName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblSenderCorrespondentNoError.Text = "No found swiftcode";
                }
            }
        }
        protected void comboIntermediaryBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isEnable = !comboIntermediaryBankType.SelectedValue.Equals("A");
            //
            txtIntermediaryBank.Enabled = !isEnable;
            txtIntermediaryBankName.Enabled = isEnable;
            txtIntermediaryBankAddr1.Enabled = isEnable;
            txtIntermediaryBankAddr2.Enabled = isEnable;
            txtIntermediaryBankAddr3.Enabled = isEnable;
        }
        protected void comboBeneficiaryBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isEnable = !comboBeneficiaryBankType.SelectedValue.Equals("A");
            txtBeneficiaryBank.Enabled = !isEnable;
            txtBeneficiaryBankName.Enabled = isEnable;
            txtBeneficiaryBankAddr1.Enabled = isEnable;
            txtBeneficiaryBankAddr2.Enabled = isEnable;
            txtBeneficiaryBankAddr3.Enabled = isEnable;
        }

        protected void txtBeneficiaryBank_OnTextChanged(object sender, EventArgs e)
        {
            lblBeneficiaryBankError.Text = "";
            txtBeneficiaryBankName.Text = "";
            if (!string.IsNullOrEmpty(txtBeneficiaryBank.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtBeneficiaryBank.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtBeneficiaryBankName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblBeneficiaryBankError.Text = "No found swiftcode";
                }
            }
        }
        protected void comboAccountWithInstitutionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool isEnable = !comboAccountWithInstitutionType.SelectedValue.Equals("A");
            txtAccountWithInstitution.Enabled = !isEnable;
            txtAccountWithInstitutionName.Enabled = isEnable;
            txtAccountWithInstitutionAddr1.Enabled = isEnable;
            txtAccountWithInstitutionAddr2.Enabled = isEnable;
            txtAccountWithInstitutionAddr3.Enabled = isEnable;
        }

        protected void txtAccountWithInstitution_OnTextChanged(object sender, EventArgs e)
        {
            lblAccountWithInstitutionError.Text = "";
            txtAccountWithInstitutionName.Text = "";
            if (!string.IsNullOrEmpty(txtAccountWithInstitution.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtAccountWithInstitution.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtAccountWithInstitutionName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();

                    //if (comboCreateMT756.SelectedValue == bd.YesNo.YES)
                    //{
                    //    txtReceiverCorrespondentNo.Text = txtAccountWithInstitution.Text;
                    //    txtReceiverCorrespondentName.Text = txtAccountWithInstitutionName.Text;
                    //}
                }
                else
                {
                    lblAccountWithInstitutionError.Text = "No found swiftcode";
                }
            }
        }

        protected void txtIntermediaryBank_OnTextChanged(object sender, EventArgs e)
        {
            lblIntermediaryBankNoError.Text = "";
            txtIntermediaryBankName.Text = "";
            if (!string.IsNullOrEmpty(txtIntermediaryBank.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtIntermediaryBank.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtIntermediaryBankName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblIntermediaryBankNoError.Text = "No found swiftcode";
                }
            }
        }
        protected void cboNostroAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Account"] = row["AccountNo"].ToString();
        }
        protected void InitSourceForDepositAccount()
        {
            var lstDepositAccount = entContext.BACCOUNTS.ToList();
            DataTable tbl1 = new DataTable();
            tbl1.Columns.Add("AccountID");
            tbl1.Columns.Add("AccountName");
            foreach (var item in lstDepositAccount)
            {
                tbl1.Rows.Add(item.AccountID, item.AccountName);
            }
            DataSet datasource = new DataSet();//Tab1
            datasource.Tables.Add(tbl1);

            cboDepositAccount.Items.Clear();
            cboDepositAccount.Items.Add(new RadComboBoxItem(""));
            cboDepositAccount.DataValueField = "AccountID";
            cboDepositAccount.DataTextField = "AccountName";
            cboDepositAccount.DataSource = datasource;
            cboDepositAccount.DataBind();
            
        }
        protected void btnReportPhieuXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        protected void btnReportPhieuChuyenKhoan_Click(object sender, EventArgs e)
        {
            showReport(2);
        }
        protected void btnReportVATb_Click(object sender, EventArgs e)
        {
            showReport(3);
        }
        protected void btnReportMT202_Click(object sender, EventArgs e)
        {
            showReport(4);
        }
        protected void btnReportMT756_Click(object sender, EventArgs e)
        {
            showReport(5);
        }
        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/Settlement/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                reportData = bd.IssueLC.ImportLCPaymentReport(reportType, Convert.ToInt64(txtPaymentId.Value), this.UserInfo.Username);
                switch (reportType)
                {
                    case 1://PhieuXuatNgoaiBang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PhieuXuatNgoaiBang.doc");
                        reportSaveName = "PhieuXuatNgoaiBang" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 2://PhieuChuyenKhoan
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PhieuChuyenKhoan.doc");
                        reportSaveName = "PhieuChuyenKhoan" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 3://VAT B
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VATb.doc");
                        reportSaveName = "VATb" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        break;
                    case 4://MT 202
                        reportTemplate = Context.Server.MapPath(reportTemplate + "MT202.doc");
                        reportSaveName = "MT202" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        break;
                    case 5://MT 756
                        reportTemplate = Context.Server.MapPath(reportTemplate + "MT756.doc");
                        reportSaveName = "MT756" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        break;
                }
                if (reportData != null)
                {
                    try
                    {
                        reportData.Tables[0].TableName = "Table1";
                        bc.Reports.createFileDownload(reportTemplate, reportData, reportSaveName, saveFormat, saveType, Response);
                    }
                    catch (Exception err)
                    {
                        lblError.Text = reportData.Tables[0].TableName + "#" + err.Message;
                    }
                }
            }
            catch (Exception err)
            {
                lblError.Text = err.Message;
            }
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    CommitData();
                    break;
                case bc.Commands.Preview:
                    Response.Redirect(EditUrl("B_ExportPaymentList"));
                    break;
                case bc.Commands.Authorize:
                    //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.AUT, TabId, UserId);
                    //Response.Redirect("Default.aspx?tabid=" + TabId);
                    Authorize();
                    break;

                case bc.Commands.Reverse:
                    //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.REV, TabId, UserId);
                    //if (TabId == TabDocsWithDiscrepancies || TabId == TabDocsWithNoDiscrepancies)
                    //    Response.Redirect("Default.aspx?tabid=" + TabDocsAmend + "&tid=" + txtCode.Text.Trim());
                    //else
                    //    Response.Redirect("Default.aspx?tabid=" + TabId);
                    Reverse();
                    break;
            }
        }
        protected void UpdateStatus(string status)
        {
            var obj = entContext.B_ExportLCPayments.Where(dr => dr.LCCode == txtCode.Text).FirstOrDefault();
            if (obj != null)
            {
                var lst = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();
                if (lst != null)
                {
                    obj = entContext.B_ExportLCPayments.Where(dr => dr.DocId == lst.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        if (status == "REV")
                        {
                            obj.Status = status;
                        }
                        else
                        {
                            obj.Status = status;
                            obj.AuthorizedBy = UserId.ToString();
                            obj.AuthorizedDate = DateTime.Now;
                        }
                        entContext.SaveChanges();
                    }
                }
            }
        }
        protected void Reverse()
        {
            UpdateStatus("REV");
            Response.Redirect("Default.aspx?tabid=" + this.TabId);
        }
        protected void Authorize()
        {
            UpdateStatus("AUT");
            Response.Redirect("Default.aspx?tabid=" + this.TabId);
        }
        private void CommitData()
        {
            long paymentId = Convert.ToInt64(txtPaymentId.Value);
            //commit ExportLCPayment
            var cmExportLC = entContext.B_ExportLCPayments.Where(x => x.LCCode == txtCode.Text).FirstOrDefault();
            var DocID = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();

            if (cmExportLC == null)
            {
                B_ExportLCPayment obj = new B_ExportLCPayment
                {
                    LCCode = txtCode.Text,
                    DrawType = cboDrawType.SelectedValue,
                    DrawingAmount = Convert.ToDouble(txtDrawingAmount.Value),
                    Currency = lblCurrency.Text,
                    DepositAccount = cboDepositAccount.SelectedValue,
                    ExchangeRate = Convert.ToDouble(txtExchangeRate.Value),
                    AmtDRFrAcctCcy = txtAmtDRFrAcctCcy.Text,
                    ProvAmtRelease = Convert.ToDouble(txtProvAmtRelease.Value),
                    PaymentMethod = cboPaymentMethod.SelectedValue,
                    NostroAcct = cboNostroAcct.SelectedValue,
                    AmountCredited = Convert.ToDouble(txtAmountCredited.Value),
                    PaymentRemarks = txtPaymentRemarks.Text,
                    FullyUtilised = txtFullyUtilised.Text,
                    WaiveCharges = cboWaiveCharges.SelectedValue,
                    ChargeRemarks = txtChargeRemarks.Text,
                    VATNo = txtVatNo.Text,
                    Status = "UNA",
                    CreateDate = DateTime.Now,
                    CreateBy = UserId.ToString()
                };
                if (DocID != null)
                {
                    obj.DocId = DocID.Id;
                }
                //save to database
                entContext.B_ExportLCPayments.Add(obj);
                entContext.SaveChanges();
                //save new to mt202
                var cmMT202_v1 = entContext.B_ExportLCPaymentMT202s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
                if (cmMT202_v1 == null)
                {
                    B_ExportLCPaymentMT202 objMT202 = new B_ExportLCPaymentMT202
                    {
                        PaymentCode = txtCode.Text,
                        TransactionReferenceNumber = lblTransactionReferenceNumber.Text,
                        RelatedReference = txtRelatedReference.Text,
                        ValueDate = txtValueDate.SelectedDate,
                        Currency = comboCurrency.SelectedValue,
                        Amount = numAmount.Value,
                        OrderingInstitution = lblOrderingInstitution.Text,
                        SenderCorrespondent1 = lblSenderCorrespondent1.Text,
                        SenderCorrespondent2 = lblSenderCorrespondent2.Text,
                        ReceiverCorrespondent1 = lblReceiverCorrespondentName2.Text,
                        IntermediaryBank = txtIntermediaryBank.Text,
                        AccountWithInstitution = txtAccountWithInstitution.Text,
                        BeneficiaryBank = txtBeneficiaryBank.Text,
                        SenderToReceiverInformation = txtSenderToReceiverInformation.Text,
                        IntermediaryBankType = comboIntermediaryBankType.SelectedValue,
                        IntermediaryBankName = txtIntermediaryBankName.Text,
                        IntermediaryBankAddr1 = txtIntermediaryBankAddr1.Text,
                        IntermediaryBankAddr2 = txtIntermediaryBankAddr2.Text,
                        IntermediaryBankAddr3 = txtIntermediaryBankAddr3.Text,
                        AccountWithInstitutionType = comboAccountWithInstitutionType.SelectedValue,
                        AccountWithInstitutionName = txtAccountWithInstitutionName.Text,
                        AccountWithInstitutionAddr1 = txtAccountWithInstitutionAddr1.Text,
                        AccountWithInstitutionAddr2 = txtAccountWithInstitutionAddr2.Text,
                        AccountWithInstitutionAddr3 = txtAccountWithInstitutionAddr3.Text,
                        BeneficiaryBankType = comboBeneficiaryBankType.SelectedValue,
                        BeneficiaryBankName = txtBeneficiaryBankName.Text,
                        BeneficiaryBankAddr1 = txtBeneficiaryBankAddr1.Text,
                        BeneficiaryBankAddr2 = txtBeneficiaryBankAddr2.Text,
                        BeneficiaryBankAddr3 = txtBeneficiaryBankAddr3.Text,
                        SenderToReceiverInformation2 = txtSenderToReceiverInformation2.Text,
                        SenderToReceiverInformation3 = txtSenderToReceiverInformation3.Text
                    };
                    if (DocID != null)
                    {
                        objMT202.PaymentId = DocID.Id;
                    }
                    entContext.B_ExportLCPaymentMT202s.Add(objMT202);
                    entContext.SaveChanges();
                }
                //
                var cmMT756_v1 = entContext.B_ExportLCPaymentMT756s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
                if (cmMT756_v1 == null)
                {
                    B_ExportLCPaymentMT756 objMT756 = new B_ExportLCPaymentMT756
                    {
                        PaymentCode = txtCode.Text,
                        SendingBankTRN = txtSendingBankTRN.Text,
                        RelatedReference = txtRelatedReference.Text,
                        AmountCollected = numAmountCollected.Value,
                        ValueDate = txtValueDate.SelectedDate,
                        Currency = comboCurrency_MT400.SelectedValue,
                        Amount = numAmount_MT400.Value,
                        SenderCorrespondent1 = lblSenderCorrespondent1.Text,
                        SenderCorrespondent2 = lblSenderCorrespondent2.Text,
                        DetailOfCharges1 = txtDetailOfCharges1.Text,
                        DetailOfCharges2 = txtDetailOfCharges2.Text,
                        DetailOfCharges3 = txtDetailOfCharges3.Text,
                        ReceiverCorrespondentType = comboReceiverCorrespondentType.SelectedValue,
                        ReceiverCorrespondentNo = txtReceiverCorrespondentNo.Text,
                        ReceiverCorrespondentName = txtReceiverCorrespondentName.Text,
                        ReceiverCorrespondentAddr1 = txtReceiverCorrespondentAddr1.Text,
                        ReceiverCorrespondentAddr2 = txtReceiverCorrespondentAddr2.Text,
                        ReceiverCorrespondentAddr3 = txtReceiverCorrespondentAddr3.Text,
                        SenderCorrespondentType = txtSenderToReceiverInformation1_400_1.Text,
                        SenderCorrespondentNo = txtSenderCorrespondentNo.Text,
                        SenderCorrespondentName = txtSenderCorrespondentName.Text,
                        SenderCorrespondentAddr1 = txtSenderCorrespondentAddress1.Text,
                        SenderCorrespondentAddr2 = txtSenderCorrespondentAddress2.Text,
                        SenderCorrespondentAddr3 = txtSenderCorrespondentAddress3.Text,
                        SenderToReceiverInformation1 = txtSenderToReceiverInformation1_400_1.Text,
                        SenderToReceiverInformation2 = txtSenderToReceiverInformation1_400_2.Text,
                        SenderToReceiverInformation3 = txtSenderToReceiverInformation1_400_3.Text,
                    };
                    if (DocID != null)
                    {
                        objMT756.PaymentId = DocID.Id;
                    }
                    entContext.B_ExportLCPaymentMT756s.Add(objMT756);
                    entContext.SaveChanges();
                }
                //tab charge
                var cmCharge_v1 = entContext.B_ExportLCPaymentCharges.Where(x => x.PaymentId == DocID.Id).ToList();
                if (cmCharge_v1 == null || cmCharge_v1.Count == 0)
                {
                    //save tabCableCharge
                    B_ExportLCPaymentCharge tabCableCharge = new B_ExportLCPaymentCharge
                    {
                        PaymentId = DocID.Id,
                        ChargeTab = "tabCableCharge",
                        ChargeCode = tabCableCharge_cboChargeCode.SelectedValue,
                        ChargeAcct = tabCableCharge_cboChargeAcc.SelectedValue,
                        ChargeCcy = tabCableCharge_cboChargeCcy.SelectedValue,
                        ExchangeRate = tabCableCharge_txtExchangeRate.Value,
                        ChargeAmt = tabCableCharge_txtChargeAmt.Value,
                        PartyCharged = tabCableCharge_cboPartyCharged.SelectedValue,
                        AmortCharge = tabCableCharge_cboAmortCharge.SelectedValue,
                        ChargeStatus = tabCableCharge_cboChargeStatus.SelectedValue,
                        TaxCode = tabCableCharge_txtTaxCode.Text,
                        TaxAmt = tabCableCharge_txtTaxAmt.Value
                    };
                    entContext.B_ExportLCPaymentCharges.Add(tabCableCharge);
                    entContext.SaveChanges();
                    //save tabPaymentCharge
                    B_ExportLCPaymentCharge tabPaymentCharge = new B_ExportLCPaymentCharge
                    {
                        PaymentId = DocID.Id,
                        ChargeTab = "tabPaymentCharge",
                        ChargeCode = tabPaymentCharge_cboChargeCode.SelectedValue,
                        ChargeAcct =tabPaymentCharge_cboChargeAcc.SelectedValue,
                        ChargeCcy = tabPaymentCharge_cboChargeCcy.SelectedValue,
                        ExchangeRate = tabPaymentCharge_txtExchangeRate.Value,
                        ChargeAmt = tabPaymentCharge_txtChargeAmt.Value,
                        PartyCharged = tabPaymentCharge_cboPartyCharged.SelectedValue,
                        AmortCharge = tabPaymentCharge_cboAmortCharge.SelectedValue,
                        ChargeStatus = tabPaymentCharge_cboChargeStatus.SelectedValue,
                        TaxCode = tabPaymentCharge_txtTaxCode.Text,
                        TaxAmt = tabPaymentCharge_txtTaxAmt.Value
                    };
                    entContext.B_ExportLCPaymentCharges.Add(tabPaymentCharge);
                    entContext.SaveChanges();
                    //save tabHandlingCharge
                    B_ExportLCPaymentCharge tabHandlingCharge = new B_ExportLCPaymentCharge
                    {
                        PaymentId = DocID.Id,
                        ChargeTab = "tabHandlingCharge",
                        ChargeCode = tabHandlingCharge_cboChargeCode.SelectedValue,
                        ChargeAcct =tabHandlingCharge_cboChargeAcc.SelectedValue,
                        ChargeCcy = tabHandlingCharge_cboChargeCcy.SelectedValue,
                        ExchangeRate = tabHandlingCharge_txtExchangeRate.Value,
                        ChargeAmt = tabHandlingCharge_txtChargeAmt.Value,
                        PartyCharged = tabHandlingCharge_cboPartyCharged.SelectedValue,
                        AmortCharge = tabHandlingCharge_cboAmortCharge.SelectedValue,
                        ChargeStatus = tabPaymentCharge_cboChargeStatus.SelectedValue,
                        TaxCode = tabHandlingCharge_txtTaxCode.Text,
                        TaxAmt = tabHandlingCharge_txtTaxAmt.Value
                    };
                    entContext.B_ExportLCPaymentCharges.Add(tabHandlingCharge);
                    entContext.SaveChanges();
                    //save tabDiscrepenciesCharge
                    B_ExportLCPaymentCharge tabDiscrepenciesCharge = new B_ExportLCPaymentCharge
                    {
                        PaymentId = DocID.Id,
                        ChargeTab = "tabDiscrepenciesCharge",
                        ChargeCode = tabDiscrepenciesCharge_cboChargeCode.SelectedValue,
                        ChargeAcct = tabDiscrepenciesCharge_cboChargeAcc.SelectedValue,
                        ChargeCcy = tabDiscrepenciesCharge_cboChargeCcy.SelectedValue,
                        ExchangeRate = tabDiscrepenciesCharge_txtExchangeRate.Value,
                        ChargeAmt = tabDiscrepenciesCharge_txtChargeAmt.Value,
                        PartyCharged = tabDiscrepenciesCharge_cboPartyCharged.SelectedValue,
                        AmortCharge = tabDiscrepenciesCharge_cboAmortCharge.SelectedValue,
                        ChargeStatus = tabDiscrepenciesCharge_cboChargeStatus.SelectedValue,
                        TaxCode = tabDiscrepenciesCharge_txtTaxCode.Text,
                        TaxAmt = tabDiscrepenciesCharge_txtTaxAmt.Value
                    };
                    entContext.B_ExportLCPaymentCharges.Add(tabDiscrepenciesCharge);
                    entContext.SaveChanges();
                    //save tabOtherCharge
                    B_ExportLCPaymentCharge tabOtherCharge = new B_ExportLCPaymentCharge
                    {
                        PaymentId = DocID.Id,
                        ChargeTab = "tabOtherCharge",
                        ChargeCode = tabOtherCharge_cboChargeCode.SelectedValue,
                        ChargeAcct =tabOtherCharge_cboChargeAcc.SelectedValue,
                        ChargeCcy = tabOtherCharge_cboChargeCcy.SelectedValue,
                        ExchangeRate = tabOtherCharge_txtExchangeRate.Value,
                        ChargeAmt = tabOtherCharge_txtChargeAmt.Value,
                        PartyCharged = tabOtherCharge_cboPartyCharged.SelectedValue,
                        AmortCharge = tabOtherCharge_cboAmortCharge.SelectedValue,
                        ChargeStatus = tabOtherCharge_cboChargeStatus.SelectedValue,
                        TaxCode = tabOtherCharge_txtTaxCode.Text,
                        TaxAmt = tabOtherCharge_txtTaxAmt.Value
                    };
                    entContext.B_ExportLCPaymentCharges.Add(tabDiscrepenciesCharge);
                    entContext.SaveChanges();
                }
                Response.Redirect("Default.aspx?tabid=" + this.TabId);
            }
            else
            { 
                //update
                cmExportLC.DrawType = cboDrawType.SelectedValue;
                cmExportLC.DrawingAmount = Convert.ToDouble(txtDrawingAmount.Value);
                cmExportLC.Currency = lblCurrency.Text;
                cmExportLC.DepositAccount = cboDepositAccount.SelectedValue;
                cmExportLC.ExchangeRate = Convert.ToDouble(txtExchangeRate.Value);
                cmExportLC.AmtDRFrAcctCcy = txtAmtDRFrAcctCcy.Text;
                cmExportLC.ProvAmtRelease = Convert.ToDouble(txtProvAmtRelease.Value);
                cmExportLC.PaymentMethod = cboPaymentMethod.SelectedValue;
                cmExportLC.NostroAcct = cboNostroAcct.SelectedValue;
                cmExportLC.AmountCredited = Convert.ToDouble(txtAmountCredited.Value);
                cmExportLC.PaymentRemarks = txtPaymentRemarks.Text;
                cmExportLC.FullyUtilised = txtFullyUtilised.Text;
                cmExportLC.WaiveCharges = cboWaiveCharges.SelectedValue;
                cmExportLC.ChargeRemarks = txtChargeRemarks.Text;
                cmExportLC.VATNo = txtVatNo.Text;
                cmExportLC.Status = "UNA";
                cmExportLC.CreateDate = DateTime.Now;
                cmExportLC.CreateBy = UserId.ToString();
                entContext.SaveChanges();
            }
            //bind MT202
            var cmMT202 = entContext.B_ExportLCPaymentMT202s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
            if (cmMT202 == null)
            {
                B_ExportLCPaymentMT202 objMT202 = new B_ExportLCPaymentMT202
                {
                    PaymentCode = txtCode.Text,
                    TransactionReferenceNumber = lblTransactionReferenceNumber.Text,
                    RelatedReference = txtRelatedReference.Text,
                    ValueDate = txtValueDate.SelectedDate,
                    Currency = comboCurrency.SelectedValue,
                    Amount = numAmount.Value,
                    OrderingInstitution = lblOrderingInstitution.Text,
                    SenderCorrespondent1 = lblSenderCorrespondent1.Text,
                    SenderCorrespondent2 = lblSenderCorrespondent2.Text,
                    ReceiverCorrespondent1 = lblReceiverCorrespondentName2.Text,
                    IntermediaryBank = txtIntermediaryBank.Text,
                    AccountWithInstitution = txtAccountWithInstitution.Text,
                    BeneficiaryBank = txtBeneficiaryBank.Text,
                    SenderToReceiverInformation = txtSenderToReceiverInformation.Text,
                    IntermediaryBankType = comboIntermediaryBankType.SelectedValue,
                    IntermediaryBankName = txtIntermediaryBankName.Text,
                    IntermediaryBankAddr1 = txtIntermediaryBankAddr1.Text,
                    IntermediaryBankAddr2 = txtIntermediaryBankAddr2.Text,
                    IntermediaryBankAddr3 = txtIntermediaryBankAddr3.Text,
                    AccountWithInstitutionType = comboAccountWithInstitutionType.SelectedValue,
                    AccountWithInstitutionName = txtAccountWithInstitutionName.Text,
                    AccountWithInstitutionAddr1 = txtAccountWithInstitutionAddr1.Text,
                    AccountWithInstitutionAddr2 = txtAccountWithInstitutionAddr2.Text,
                    AccountWithInstitutionAddr3 = txtAccountWithInstitutionAddr3.Text,
                    BeneficiaryBankType = comboBeneficiaryBankType.SelectedValue,
                    BeneficiaryBankName = txtBeneficiaryBankName.Text,
                    BeneficiaryBankAddr1 = txtBeneficiaryBankAddr1.Text,
                    BeneficiaryBankAddr2 = txtBeneficiaryBankAddr2.Text,
                    BeneficiaryBankAddr3 = txtBeneficiaryBankAddr3.Text,
                    SenderToReceiverInformation2 = txtSenderToReceiverInformation2.Text,
                    SenderToReceiverInformation3 = txtSenderToReceiverInformation3.Text
                };
                if (DocID != null)
                {
                    objMT202.PaymentId = DocID.Id;
                }
                entContext.B_ExportLCPaymentMT202s.Add(objMT202);
                entContext.SaveChanges();
            }
            else
            { 
                cmMT202.PaymentCode = txtCode.Text;
                cmMT202.TransactionReferenceNumber = lblTransactionReferenceNumber.Text;
                cmMT202.RelatedReference = txtRelatedReference.Text;
                cmMT202.ValueDate = txtValueDate.SelectedDate;
                cmMT202.Currency = comboCurrency.SelectedValue;
                cmMT202.Amount = numAmount.Value;
                cmMT202.OrderingInstitution = lblOrderingInstitution.Text;
                cmMT202.SenderCorrespondent1 = lblSenderCorrespondent1.Text;
                cmMT202.SenderCorrespondent2 = lblSenderCorrespondent2.Text;
                cmMT202.ReceiverCorrespondent1 = lblReceiverCorrespondentName2.Text;
                cmMT202.IntermediaryBank = txtIntermediaryBank.Text;
                cmMT202.AccountWithInstitution = txtAccountWithInstitution.Text;
                cmMT202.BeneficiaryBank = txtBeneficiaryBank.Text;
                cmMT202.SenderToReceiverInformation = txtSenderToReceiverInformation.Text;
                cmMT202.IntermediaryBankType = comboIntermediaryBankType.SelectedValue;
                cmMT202.IntermediaryBankName = txtIntermediaryBankName.Text;
                cmMT202.IntermediaryBankAddr1 = txtIntermediaryBankAddr1.Text;
                cmMT202.IntermediaryBankAddr2 = txtIntermediaryBankAddr2.Text;
                cmMT202.IntermediaryBankAddr3 = txtIntermediaryBankAddr3.Text;
                cmMT202.AccountWithInstitutionType = comboAccountWithInstitutionType.SelectedValue;
                cmMT202.AccountWithInstitutionName = txtAccountWithInstitutionName.Text;
                cmMT202.AccountWithInstitutionAddr1 = txtAccountWithInstitutionAddr1.Text;
                cmMT202.AccountWithInstitutionAddr2 = txtAccountWithInstitutionAddr2.Text;
                cmMT202.AccountWithInstitutionAddr3 = txtAccountWithInstitutionAddr3.Text;
                cmMT202.BeneficiaryBankType = comboBeneficiaryBankType.SelectedValue;
                cmMT202.BeneficiaryBankName = txtBeneficiaryBankName.Text;
                cmMT202.BeneficiaryBankAddr1 = txtBeneficiaryBankAddr1.Text;
                cmMT202.BeneficiaryBankAddr2 = txtBeneficiaryBankAddr2.Text;
                cmMT202.BeneficiaryBankAddr3 = txtBeneficiaryBankAddr3.Text;
                cmMT202.SenderToReceiverInformation2 = txtSenderToReceiverInformation2.Text;
                cmMT202.SenderToReceiverInformation3 = txtSenderToReceiverInformation3.Text;
                entContext.SaveChanges();
            }
            //bind MT756
            var cmMT756 = entContext.B_ExportLCPaymentMT756s.Where(x => x.PaymentCode == txtCode.Text).FirstOrDefault();
            if (cmMT756 == null)
            {
                B_ExportLCPaymentMT756 objMT756 = new B_ExportLCPaymentMT756
                {
                    PaymentCode = txtCode.Text,
                    SendingBankTRN = txtSendingBankTRN.Text,
                    RelatedReference = txtRelatedReference.Text,
                    AmountCollected = numAmountCollected.Value,
                    ValueDate = txtValueDate.SelectedDate,
                    Currency = comboCurrency_MT400.SelectedValue,
                    Amount = numAmount_MT400.Value,
                    SenderCorrespondent1 = lblSenderCorrespondent1.Text,
                    SenderCorrespondent2 = lblSenderCorrespondent2.Text,
                    DetailOfCharges1 = txtDetailOfCharges1.Text,
                    DetailOfCharges2 = txtDetailOfCharges2.Text,
                    DetailOfCharges3 = txtDetailOfCharges3.Text,
                    ReceiverCorrespondentType = comboReceiverCorrespondentType.SelectedValue,
                    ReceiverCorrespondentNo = txtReceiverCorrespondentNo.Text,
                    ReceiverCorrespondentName = txtReceiverCorrespondentName.Text,
                    ReceiverCorrespondentAddr1 = txtReceiverCorrespondentAddr1.Text,
                    ReceiverCorrespondentAddr2 = txtReceiverCorrespondentAddr2.Text,
                    ReceiverCorrespondentAddr3 = txtReceiverCorrespondentAddr3.Text,
                    SenderCorrespondentType = txtSenderToReceiverInformation1_400_1.Text,
                    SenderCorrespondentNo = txtSenderCorrespondentNo.Text,
                    SenderCorrespondentName = txtSenderCorrespondentName.Text,
                    SenderCorrespondentAddr1 = txtSenderCorrespondentAddress1.Text,
                    SenderCorrespondentAddr2 = txtSenderCorrespondentAddress2.Text,
                    SenderCorrespondentAddr3 = txtSenderCorrespondentAddress3.Text,
                    SenderToReceiverInformation1 = txtSenderToReceiverInformation1_400_1.Text,
                    SenderToReceiverInformation2 = txtSenderToReceiverInformation1_400_2.Text,
                    SenderToReceiverInformation3 = txtSenderToReceiverInformation1_400_3.Text,
                };
                if (DocID != null)
                {
                    objMT756.PaymentId = DocID.Id;
                }
                entContext.B_ExportLCPaymentMT756s.Add(objMT756);
                entContext.SaveChanges();
            }
            else
            {
                cmMT756.PaymentCode = txtCode.Text;
                cmMT756.SendingBankTRN = txtSendingBankTRN.Text;
                cmMT756.RelatedReference = txtRelatedReference.Text;
                cmMT756.AmountCollected = numAmountCollected.Value;
                cmMT756.ValueDate = txtValueDate.SelectedDate;
                cmMT756.Currency = comboCurrency_MT400.SelectedValue;
                cmMT756.Amount = numAmount_MT400.Value;
                cmMT756.SenderCorrespondent1 = lblSenderCorrespondent1.Text;
                cmMT756.SenderCorrespondent2 = lblSenderCorrespondent2.Text;
                cmMT756.DetailOfCharges1 = txtDetailOfCharges1.Text;
                cmMT756.DetailOfCharges2 = txtDetailOfCharges2.Text;
                cmMT756.DetailOfCharges3 = txtDetailOfCharges3.Text;
                cmMT756.ReceiverCorrespondentType = comboReceiverCorrespondentType.SelectedValue;
                cmMT756.ReceiverCorrespondentNo = txtReceiverCorrespondentNo.Text;
                cmMT756.ReceiverCorrespondentName = txtReceiverCorrespondentName.Text;
                cmMT756.ReceiverCorrespondentAddr1 = txtReceiverCorrespondentAddr1.Text;
                cmMT756.ReceiverCorrespondentAddr2 = txtReceiverCorrespondentAddr2.Text;
                cmMT756.ReceiverCorrespondentAddr3 = txtReceiverCorrespondentAddr3.Text;
                cmMT756.SenderCorrespondentType = txtSenderToReceiverInformation1_400_1.Text;
                cmMT756.SenderCorrespondentNo = txtSenderCorrespondentNo.Text;
                cmMT756.SenderCorrespondentName = txtSenderCorrespondentName.Text;
                cmMT756.SenderCorrespondentAddr1 = txtSenderCorrespondentAddress1.Text;
                cmMT756.SenderCorrespondentAddr2 = txtSenderCorrespondentAddress2.Text;
                cmMT756.SenderCorrespondentAddr3 = txtSenderCorrespondentAddress3.Text;
                cmMT756.SenderToReceiverInformation1 = txtSenderToReceiverInformation1_400_1.Text;
                cmMT756.SenderToReceiverInformation2 = txtSenderToReceiverInformation1_400_2.Text;
                cmMT756.SenderToReceiverInformation3 = txtSenderToReceiverInformation1_400_3.Text;
                entContext.SaveChanges();
            }
            //bind tab Charge
            var cmCharge = entContext.B_ExportLCPaymentCharges.Where(x => x.PaymentId == DocID.Id).ToList();
            if (cmCharge == null || cmCharge.Count == 0)
            {
                //save tabCableCharge
                B_ExportLCPaymentCharge tabCableCharge = new B_ExportLCPaymentCharge
                {
                    PaymentId = DocID.Id,
                    ChargeTab = "tabCableCharge",
                    ChargeCode = tabCableCharge_cboChargeCode.SelectedValue,
                    ChargeAcct = tabCableCharge_cboChargeAcc.SelectedValue,
                    ChargeCcy = tabCableCharge_cboChargeCcy.SelectedValue,
                    ExchangeRate = tabCableCharge_txtExchangeRate.Value,
                    ChargeAmt = tabCableCharge_txtChargeAmt.Value,
                    PartyCharged = tabCableCharge_cboPartyCharged.SelectedValue,
                    AmortCharge = tabCableCharge_cboAmortCharge.SelectedValue,
                    ChargeStatus = tabCableCharge_cboChargeStatus.SelectedValue,
                    TaxCode = tabCableCharge_txtTaxCode.Text,
                    TaxAmt = tabCableCharge_txtTaxAmt.Value
                };
                entContext.B_ExportLCPaymentCharges.Add(tabCableCharge);
                entContext.SaveChanges();
                //save tabPaymentCharge
                B_ExportLCPaymentCharge tabPaymentCharge = new B_ExportLCPaymentCharge { 
                    PaymentId=DocID.Id,
                    ChargeTab = "tabPaymentCharge",
                    ChargeCode = tabPaymentCharge_cboChargeCode.SelectedValue,
                    ChargeAcct=tabPaymentCharge_cboChargeAcc.SelectedValue,
                    ChargeCcy=tabPaymentCharge_cboChargeCcy.SelectedValue,
                    ExchangeRate=tabPaymentCharge_txtExchangeRate.Value,
                    ChargeAmt=tabPaymentCharge_txtChargeAmt.Value,
                    PartyCharged=tabPaymentCharge_cboPartyCharged.SelectedValue,
                    AmortCharge=tabPaymentCharge_cboAmortCharge.SelectedValue,
                    ChargeStatus=tabPaymentCharge_cboChargeStatus.SelectedValue,
                    TaxCode=tabPaymentCharge_txtTaxCode.Text,
                    TaxAmt=tabPaymentCharge_txtTaxAmt.Value
                };
                entContext.B_ExportLCPaymentCharges.Add(tabPaymentCharge);
                entContext.SaveChanges();
                //save tabHandlingCharge
                B_ExportLCPaymentCharge tabHandlingCharge = new B_ExportLCPaymentCharge
                {
                    PaymentId = DocID.Id,
                    ChargeTab = "tabHandlingCharge",
                    ChargeCode = tabHandlingCharge_cboChargeCode.SelectedValue,
                    ChargeAcct=tabHandlingCharge_cboChargeAcc.SelectedValue,
                    ChargeCcy=tabHandlingCharge_cboChargeCcy.SelectedValue,
                    ExchangeRate=tabHandlingCharge_txtExchangeRate.Value,
                    ChargeAmt=tabHandlingCharge_txtChargeAmt.Value,
                    PartyCharged=tabHandlingCharge_cboPartyCharged.SelectedValue,
                    AmortCharge=tabHandlingCharge_cboAmortCharge.SelectedValue,
                    ChargeStatus=tabPaymentCharge_cboChargeStatus.SelectedValue,
                    TaxCode=tabHandlingCharge_txtTaxCode.Text,
                    TaxAmt=tabHandlingCharge_txtTaxAmt.Value
                };
                entContext.B_ExportLCPaymentCharges.Add(tabHandlingCharge);
                entContext.SaveChanges();
                //save tabDiscrepenciesCharge
                B_ExportLCPaymentCharge tabDiscrepenciesCharge = new B_ExportLCPaymentCharge {
                    PaymentId = DocID.Id,
                    ChargeTab = "tabDiscrepenciesCharge",
                    ChargeCode = tabDiscrepenciesCharge_cboChargeCode.SelectedValue,
                    ChargeAcct=tabDiscrepenciesCharge_cboChargeAcc.SelectedValue,
                    ChargeCcy=tabDiscrepenciesCharge_cboChargeCcy.SelectedValue,
                    ExchangeRate=tabDiscrepenciesCharge_txtExchangeRate.Value,
                    ChargeAmt=tabDiscrepenciesCharge_txtChargeAmt.Value,
                    PartyCharged=tabDiscrepenciesCharge_cboPartyCharged.SelectedValue,
                    AmortCharge=tabDiscrepenciesCharge_cboAmortCharge.SelectedValue,
                    ChargeStatus=tabDiscrepenciesCharge_cboChargeStatus.SelectedValue,
                    TaxCode=tabDiscrepenciesCharge_txtTaxCode.Text,
                    TaxAmt=tabDiscrepenciesCharge_txtTaxAmt.Value
                };
                entContext.B_ExportLCPaymentCharges.Add(tabDiscrepenciesCharge);
                entContext.SaveChanges();
                //save tabOtherCharge
                B_ExportLCPaymentCharge tabOtherCharge = new B_ExportLCPaymentCharge{
                    PaymentId = DocID.Id,
                    ChargeTab = "tabOtherCharge",
                    ChargeCode = tabOtherCharge_cboChargeCode.SelectedValue,
                    ChargeAcct=tabOtherCharge_cboChargeAcc.SelectedValue,
                    ChargeCcy=tabOtherCharge_cboChargeCcy.SelectedValue,
                    ExchangeRate=tabOtherCharge_txtExchangeRate.Value,
                    ChargeAmt=tabOtherCharge_txtChargeAmt.Value,
                    PartyCharged=tabOtherCharge_cboPartyCharged.SelectedValue,
                    AmortCharge=tabOtherCharge_cboAmortCharge.SelectedValue,
                    ChargeStatus=tabOtherCharge_cboChargeStatus.SelectedValue,
                    TaxCode=tabOtherCharge_txtTaxCode.Text,
                    TaxAmt=tabOtherCharge_txtTaxAmt.Value
                };
                entContext.B_ExportLCPaymentCharges.Add(tabOtherCharge);
                entContext.SaveChanges();
            }
            else
            {
                foreach (var item in cmCharge)
                {
                    if (item.ChargeCode == "ILC.CABLE")
                    {
                        item.ChargeAcct = tabCableCharge_cboChargeAcc.SelectedValue;
                        item.ChargeCcy = tabCableCharge_cboChargeCcy.SelectedValue;
                        item.ExchangeRate = tabCableCharge_txtExchangeRate.Value;
                        item.ChargeAmt = tabCableCharge_txtChargeAmt.Value;
                        item.PartyCharged = tabCableCharge_cboPartyCharged.SelectedValue;
                        item.AmortCharge = tabCableCharge_cboAmortCharge.SelectedValue;
                        item.ChargeStatus = tabCableCharge_cboChargeStatus.SelectedValue;
                        item.TaxCode = tabCableCharge_txtTaxCode.Text;
                        item.TaxAmt = tabCableCharge_txtTaxAmt.Value;
                        entContext.SaveChanges();
                    }
                    if (item.ChargeCode == "ILC.PAYMENT")
                    {
                        item.ChargeAcct = tabPaymentCharge_cboChargeAcc.SelectedValue;
                        item.ChargeCcy = tabPaymentCharge_cboChargeCcy.SelectedValue;
                        item.ExchangeRate = tabPaymentCharge_txtExchangeRate.Value;
                        item.ChargeAmt = tabPaymentCharge_txtChargeAmt.Value;
                        item.PartyCharged = tabPaymentCharge_cboPartyCharged.SelectedValue;
                        item.AmortCharge = tabPaymentCharge_cboAmortCharge.SelectedValue;
                        item.ChargeStatus = tabPaymentCharge_cboChargeStatus.SelectedValue;
                        item.TaxCode = tabPaymentCharge_txtTaxCode.Text;
                        item.TaxAmt = tabPaymentCharge_txtTaxAmt.Value;
                        entContext.SaveChanges();
                    }
                    if (item.ChargeCode == "ILC.HANDLING")
                    {
                        item.ChargeAcct = tabHandlingCharge_cboChargeAcc.SelectedValue;
                        item.ChargeCcy = tabHandlingCharge_cboChargeCcy.SelectedValue;
                        item.ExchangeRate = tabHandlingCharge_txtExchangeRate.Value;
                        item.ChargeAmt = tabHandlingCharge_txtChargeAmt.Value;
                        item.PartyCharged = tabHandlingCharge_cboPartyCharged.SelectedValue;
                        item.AmortCharge = tabHandlingCharge_cboAmortCharge.SelectedValue;
                        item.ChargeStatus = tabPaymentCharge_cboChargeStatus.SelectedValue;
                        item.TaxCode = tabHandlingCharge_txtTaxCode.Text;
                        item.TaxAmt = tabHandlingCharge_txtTaxAmt.Value;
                        entContext.SaveChanges();
                    }
                    if (item.ChargeCode == "ILC.DISCRP")
                    {
                        item.ChargeAcct = tabDiscrepenciesCharge_cboChargeAcc.SelectedValue;
                        item.ChargeCcy = tabDiscrepenciesCharge_cboChargeCcy.SelectedValue;
                        item.ExchangeRate = tabDiscrepenciesCharge_txtExchangeRate.Value;
                        item.ChargeAmt = tabDiscrepenciesCharge_txtChargeAmt.Value;
                        item.PartyCharged = tabDiscrepenciesCharge_cboPartyCharged.SelectedValue;
                        item.AmortCharge = tabDiscrepenciesCharge_cboAmortCharge.SelectedValue;
                        item.ChargeStatus = tabDiscrepenciesCharge_cboChargeStatus.SelectedValue;
                        item.TaxCode = tabDiscrepenciesCharge_txtTaxCode.Text;
                        item.TaxAmt = tabDiscrepenciesCharge_txtTaxAmt.Value;
                        entContext.SaveChanges();
                    }
                    if (item.ChargeCode == "ILC.OTHER")
                    {
                        item.ChargeAcct = tabOtherCharge_cboChargeAcc.SelectedValue;
                        item.ChargeCcy = tabOtherCharge_cboChargeCcy.SelectedValue;
                        item.ExchangeRate = tabOtherCharge_txtExchangeRate.Value;
                        item.ChargeAmt = tabOtherCharge_txtChargeAmt.Value;
                        item.PartyCharged = tabOtherCharge_cboPartyCharged.SelectedValue;
                        item.AmortCharge = tabOtherCharge_cboAmortCharge.SelectedValue;
                        item.ChargeStatus = tabOtherCharge_cboChargeStatus.SelectedValue;
                        item.TaxCode = tabOtherCharge_txtTaxCode.Text;
                        item.TaxAmt = tabOtherCharge_txtTaxAmt.Value;
                        entContext.SaveChanges();
                    }
                }
            }
            //
            Response.Redirect("Default.aspx?tabid=" + this.TabId);
        }
        protected void LoadDetail(string Code)
        {
            //bind tab charge cbo acc
            txtCode.Text = Code;
            var lstAcc = Code.Split('.');
            if (lstAcc != null)
            {
                var name = lstAcc[0].ToString();
                var bindCharge = entContext.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == name).FirstOrDefault();
                if (bindCharge != null)
                {
                    txtCustomerName.Value = bindCharge.BeneficiaryName;
                    bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                    bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                    bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                    bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                    bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, bindCharge.Currency));
                }
            }
            //
            var dsDetails = entContext.B_ExportLCPayments.Where(x => x.LCCode == Code).FirstOrDefault();
            if (dsDetails == null)
            {
                var drDetails = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == Code).FirstOrDefault();
                if (drDetails == null)
                {
                    lblError.Text = "This Docs was not found";
                    return;
                }
                else
                {
                    //kiem tra lai Status !AUT, RejectStatus 
                    if (drDetails.Status != "AUT")
                    {
                        lblError.Text = "This Docs has wrong Status (" + drDetails.Status + ") !";
                        return;
                    }
                    else
                    {
                        if (drDetails.RejectStatus != null)
                        {
                            if (!drDetails.RejectStatus.ToString().Equals(bd.TransactionStatus.REV) && !drDetails.RejectStatus.ToString().Equals(bd.TransactionStatus.AUT))
                            {
                                lblError.Text = "This Docs is waiting for reject !";
                                return;
                            }
                        }
                        if (Convert.ToInt32(drDetails.PaymentFullFlag) != 0)
                        {
                            lblError.Text = "This Doc is already payment completed !";
                            return;
                        }
                    }
                    //
                    lblCurrency.Text = drDetails.Currency;
                    if (drDetails.Amount != null)
                    {
                        txtDrawingAmount.Value = drDetails.Amount;
                    }
                    txtValueDate.SelectedDate = drDetails.BookingDate;
                    //cboDepositAccount.SelectedValue=drDetails.a
                    bc.Commont.initRadComboBox(ref cboNostroAcct, "Description", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(drDetails.Currency));
                    txtAmountCredited.Value = 0;
                    txtFullyUtilised.Text = bd.YesNo.NO;
                    //MT202
                    lblTransactionReferenceNumber.Text = Code;
                    txtRelatedReference.Text = drDetails.PresentorNo;
                    dteValueDate_MT202.SelectedDate = DateTime.Now;
                    setCurrency(ref comboCurrency, lblCurrency.Text);
                    numAmount.Value = txtDrawingAmount.Value;
                    //MT756
                    txtSendingBankTRN.Text = Code;
                    txtRelatedReferenceMT400.Text = drDetails.PresentorNo;
                    numAmountCollected.Value = txtDrawingAmount.Value;
                    dteValueDate_MT400.SelectedDate = DateTime.Now;
                    setCurrency(ref comboCurrency_MT400, lblCurrency.Text);
                    numAmount_MT400.Value = txtDrawingAmount.Value;
                    txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
                    //Charge
                    txtVatNo.Text = bd.IssueLC.GetVatNo();
                    //
                    setToolbar(1);

                    return;
                }
            }
            else
            {
                //xet Status 
                if (dsDetails.Status.ToString().Equals(bd.TransactionStatus.AUT) || dsDetails.Status.ToString().Equals(bd.TransactionStatus.REV))
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    setToolbar(0);
                }
                else
                {
                    setToolbar(1);
                }

                //tab Main
                cboDrawType.SelectedValue = dsDetails.DrawType;
                lblCurrency.Text = dsDetails.Currency;
                if (dsDetails.WaiveCharges != null)
                {
                    cboWaiveCharges.SelectedValue = dsDetails.WaiveCharges;
                }
                txtChargeRemarks.Text = dsDetails.ChargeRemarks;
                txtVatNo.Text = dsDetails.VATNo;
                if (dsDetails.DrawingAmount != null)
                {
                    txtDrawingAmount.Value = dsDetails.DrawingAmount;
                }
                if (dsDetails.UpdatedDate != null)
                {
                    txtValueDate.SelectedDate = dsDetails.UpdatedDate;
                }
                if (dsDetails.DepositAccount != null)
                {
                    cboDepositAccount.SelectedValue = dsDetails.DepositAccount;
                }
                if (dsDetails.ExchangeRate != null)
                {
                    txtExchangeRate.Value = dsDetails.ExchangeRate;
                }
                txtAmtDRFrAcctCcy.Text = dsDetails.AmtDRFrAcctCcy;
                if (dsDetails.ProvAmtRelease != null)
                {
                    txtProvAmtRelease.Value = dsDetails.ProvAmtRelease;
                }
                txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
                if (dsDetails.PaymentMethod != null)
                {
                    cboPaymentMethod.SelectedValue = dsDetails.PaymentMethod;
                }
                if (dsDetails.NostroAcct != null)
                {
                    cboNostroAcct.SelectedValue = dsDetails.NostroAcct;
                }
                if (dsDetails.AmountCredited != null)
                {
                    txtAmountCredited.Value = dsDetails.AmountCredited;
                }
                txtPaymentRemarks.Text = dsDetails.PaymentRemarks;
                txtFullyUtilised.Text = dsDetails.FullyUtilised;
                //bind tab MT202
                var dsMT202 = entContext.B_ExportLCPaymentMT202s.Where(x => x.PaymentCode == Code).FirstOrDefault();
                if (dsMT202 != null)
                {
                    lblTransactionReferenceNumber.Text = dsMT202.TransactionReferenceNumber;
                    txtRelatedReference.Text = dsMT202.RelatedReference;
                    if (dsMT202.ValueDate != null)
                    {
                        dteValueDate_MT202.SelectedDate = dsMT202.ValueDate;
                    }
                    if (dsMT202.Currency != null)
                    {
                        comboCurrency.SelectedValue = dsMT202.Currency;
                    }
                    if (dsMT202.Amount != null)
                    {
                        numAmount.Value = dsMT202.Amount;
                    }
                    lblOrderingInstitution.Text = dsMT202.OrderingInstitution;
                    lblSenderCorrespondent1.Text = dsMT202.SenderCorrespondent1;
                    lblSenderCorrespondent2.Text = dsMT202.SenderCorrespondent2;
                    lblReceiverCorrespondentName2.Text = dsMT202.ReceiverCorrespondent1;
                    if (dsMT202.IntermediaryBankType != null)
                    {
                        comboIntermediaryBankType.SelectedValue = dsMT202.IntermediaryBankType;
                    }
                    txtIntermediaryBank.Text = dsMT202.IntermediaryBank;
                    txtIntermediaryBankName.Text = dsMT202.IntermediaryBankName;
                    txtIntermediaryBankAddr1.Text = dsMT202.IntermediaryBankAddr1;
                    txtIntermediaryBankAddr2.Text = dsMT202.IntermediaryBankAddr2;
                    txtIntermediaryBankAddr3.Text = dsMT202.IntermediaryBankAddr3;
                    if (dsMT202.AccountWithInstitutionType != null)
                    {
                        comboAccountWithInstitutionType.SelectedValue = dsMT202.AccountWithInstitutionType;
                    }
                    txtAccountWithInstitution.Text = dsMT202.AccountWithInstitution;
                    txtAccountWithInstitutionName.Text = dsMT202.AccountWithInstitutionName;
                    txtAccountWithInstitutionAddr1.Text = dsMT202.AccountWithInstitutionAddr1;
                    txtAccountWithInstitutionAddr2.Text = dsMT202.AccountWithInstitutionAddr2;
                    txtAccountWithInstitutionAddr3.Text = dsMT202.AccountWithInstitutionAddr3;
                    if (dsMT202.BeneficiaryBankType != null)
                    {
                        comboBeneficiaryBankType.SelectedValue = dsMT202.BeneficiaryBankType;
                    }
                    txtBeneficiaryBank.Text = dsMT202.BeneficiaryBank;
                    txtBeneficiaryBankName.Text = dsMT202.BeneficiaryBankName;
                    txtBeneficiaryBankAddr1.Text = dsMT202.BeneficiaryBankAddr1;
                    txtBeneficiaryBankAddr2.Text = dsMT202.BeneficiaryBankAddr2;
                    txtBeneficiaryBankAddr3.Text = dsMT202.BeneficiaryBankAddr3;
                    txtSenderToReceiverInformation.Text = dsMT202.SenderToReceiverInformation;
                    txtSenderToReceiverInformation2.Text = dsMT202.SenderToReceiverInformation2;
                    txtSenderToReceiverInformation3.Text = dsMT202.SenderToReceiverInformation3;
                }
                //tab MT756
                var dsMT756 = entContext.B_ExportLCPaymentMT756s.Where(x => x.PaymentCode == Code).FirstOrDefault();
                if (dsMT756 != null)
                {
                    comboCreateMT756.SelectedValue = bd.YesNo.YES;
                    txtRelatedReferenceMT400.Text = dsMT756.RelatedReference;
                    txtSendingBankTRN.Text = dsMT756.SendingBankTRN;
                    if (dsMT756.AmountCollected != null)
                    {
                        numAmountCollected.Value = dsMT756.AmountCollected;
                    }
                    if (dsMT756.ValueDate != null)
                    {
                        dteValueDate_MT400.SelectedDate = dsMT756.ValueDate;
                    }
                    if (dsMT756.Currency != null)
                    {
                        comboCurrency_MT400.SelectedValue = dsMT756.Currency;
                    }
                    if (dsMT756.Amount != null)
                    {
                        numAmount_MT400.Value = dsMT756.Amount;
                    }
                    if (dsMT756.SenderCorrespondentType != null)
                    {
                        comboSenderCorrespondentType.SelectedValue = dsMT756.SenderCorrespondentType;
                    }
                    txtSenderCorrespondentNo.Text = dsMT756.SenderCorrespondentNo;
                    txtSenderCorrespondentName.Text = dsMT756.SenderCorrespondentName;
                    txtSenderCorrespondentAddress1.Text = dsMT756.SenderCorrespondentAddr1;
                    txtSenderCorrespondentAddress2.Text = dsMT756.SenderCorrespondentAddr2;
                    txtSenderCorrespondentAddress3.Text = dsMT756.SenderCorrespondentAddr3;
                    if (dsMT756.ReceiverCorrespondentType != null)
                    {
                        comboReceiverCorrespondentType.SelectedValue = dsMT756.ReceiverCorrespondentType;
                    }
                    txtReceiverCorrespondentNo.Text = dsMT756.ReceiverCorrespondentNo;
                    txtReceiverCorrespondentName.Text = dsMT756.ReceiverCorrespondentName;
                    txtReceiverCorrespondentAddr1.Text = dsMT756.ReceiverCorrespondentAddr1;
                    txtReceiverCorrespondentAddr2.Text = dsMT756.ReceiverCorrespondentAddr2;
                    txtReceiverCorrespondentAddr3.Text = dsMT756.ReceiverCorrespondentAddr3;
                    txtDetailOfCharges1.Text = dsMT756.DetailOfCharges1;
                    txtDetailOfCharges2.Text = dsMT756.DetailOfCharges2;
                    txtDetailOfCharges3.Text = dsMT756.DetailOfCharges3;
                    txtSenderToReceiverInformation1_400_1.Text = dsMT756.SenderToReceiverInformation1;
                    txtSenderToReceiverInformation1_400_2.Text = dsMT756.SenderToReceiverInformation2;
                    txtSenderToReceiverInformation1_400_3.Text = dsMT756.SenderToReceiverInformation3;
                    //tab charge
                    var dsCharge = entContext.B_ExportLCPaymentCharges.Where(x => x.PaymentId == dsMT756.PaymentId).ToList();
                    foreach (var item in dsCharge)
                    {
                        if (item.ChargeCode == "ILC.CABLE")
                        {
                            tabCableCharge.Style.Add("display", "block");
                            tabCableCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                            tabCableCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));

                            if (item.ExchangeRate != null)
                            {
                                tabCableCharge_txtExchangeRate.Value = item.ExchangeRate;
                            }
                            if (item.ChargeAmt != null)
                            {
                                tabCableCharge_txtChargeAmt.Value = item.ChargeAmt;
                            }
                            if (item.PartyCharged != null)
                            {
                                tabCableCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                            }
                            if (item.AmortCharge != null)
                            {
                                tabCableCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                            }
                            tabCableCharge_txtTaxCode.Text = item.TaxCode;
                            lblTaxCcy.Text = item.ChargeCcy;
                            tabCableCharge_txtTaxAmt.Value = item.TaxAmt;
                        }
                        if (item.ChargeCode == "ILC.PAYMENT")
                        {
                            tabPaymentCharge.Style.Add("display", "block");
                            tabPaymentCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                            tabPaymentCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));

                            if (item.ExchangeRate != null)
                            {
                                tabPaymentCharge_txtExchangeRate.Value = item.ExchangeRate;
                            }
                            if (item.PartyCharged != null)
                            {
                                tabPaymentCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                            }
                            if (item.AmortCharge != null)
                            {
                                tabPaymentCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                            }
                            if (item.ChargeStatus != null)
                            {
                                tabPaymentCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                            }
                            tabPaymentCharge_txtTaxCode.Text = item.TaxCode;
                            if (item.TaxAmt != null)
                            {
                                tabPaymentCharge_txtTaxAmt.Value = item.TaxAmt;
                            }

                        }
                        if (item.ChargeCode == "ILC.HANDLING")
                        {
                            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));

                            tabHandlingCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                            if (item.ChargeCcy != null)
                            {
                                tabHandlingCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                            }
                            if (item.ExchangeRate != null)
                            {
                                tabHandlingCharge_txtExchangeRate.Value = item.ExchangeRate;
                            }
                            if (item.ChargeAmt != null)
                            {
                                tabHandlingCharge_txtChargeAmt.Value = item.ChargeAmt;
                            }
                            if (item.PartyCharged != null)
                            {
                                tabHandlingCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                            }
                            if (item.AmortCharge != null)
                            {
                                tabHandlingCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                            }
                            tabHandlingCharge_cboChargeStatus.Text = item.ChargeStatus;
                            tabHandlingCharge_txtTaxCode.Text = item.TaxCode;
                            if (item.TaxAmt != null)
                            {
                                tabHandlingCharge_txtTaxAmt.Value = item.TaxAmt;
                            }
                        }
                        if (item.ChargeCode == "ILC.DISCRP")
                        {
                            tabDiscrepenciesCharge.Style.Add("display", "block");
                            tabDiscrepenciesCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));

                            if (item.ChargeCcy != null)
                            {
                                tabDiscrepenciesCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                            }
                            if (item.ExchangeRate != null)
                            {
                                tabDiscrepenciesCharge_txtExchangeRate.Value = item.ExchangeRate;
                            }
                            if (item.ChargeAmt != null)
                            {
                                tabDiscrepenciesCharge_txtChargeAmt.Value = item.ChargeAmt;
                            }
                            if (item.PartyCharged != null)
                            {
                                tabDiscrepenciesCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                            }
                            if (item.AmortCharge != null)
                            {
                                tabDiscrepenciesCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                            }
                            if (item.ChargeStatus != null)
                            {
                                tabDiscrepenciesCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                            }
                            tabDiscrepenciesCharge_txtTaxCode.Text = item.TaxCode;
                            if (item.TaxAmt != null)
                            {
                                tabDiscrepenciesCharge_txtTaxAmt.Value = item.TaxAmt;
                            }
                        }
                        if (item.ChargeCode == "ILC.OTHER")
                        {
                            tabOtherCharge.Style.Add("display", "block");
                            tabCableCharge_cboChargeAcc.SelectedValue = item.ChargeAcct;
                            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, item.ChargeCcy));
                            if (item.ChargeCcy != null)
                            {
                                tabOtherCharge_cboChargeCcy.SelectedValue = item.ChargeCcy;
                            }
                            if (item.ExchangeRate != null)
                            {
                                tabOtherCharge_txtExchangeRate.Value = item.ExchangeRate;
                            }
                            if (item.ChargeAmt != null)
                            {
                                tabOtherCharge_txtChargeAmt.Value = item.ChargeAmt;
                            }
                            if (item.PartyCharged != null)
                            {
                                tabOtherCharge_cboPartyCharged.SelectedValue = item.PartyCharged;
                            }
                            if (item.AmortCharge != null)
                            {
                                tabOtherCharge_cboAmortCharge.SelectedValue = item.AmortCharge;
                            }
                            if (item.ChargeStatus != null)
                            {
                                tabOtherCharge_cboChargeStatus.SelectedValue = item.ChargeStatus;
                            }
                            tabOtherCharge_txtTaxCode.Text = item.TaxCode;
                            if (item.TaxAmt != null)
                            {
                                tabOtherCharge_txtTaxAmt.Value = item.TaxAmt;
                            }
                        }
                    }
                }
            }
            setToolbar(1);
        }
    }
}