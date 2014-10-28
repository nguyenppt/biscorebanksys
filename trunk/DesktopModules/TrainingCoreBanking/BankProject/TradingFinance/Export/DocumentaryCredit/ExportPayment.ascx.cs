using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using System.Data;
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
        protected void btnLoadDocsInfo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (txtCode.Text.IndexOf('.') == -1)
            {
                lblError.Text = "This Docs was not found";
            }
            else
            {
                var dsDetails = entContext.B_ExportLCPayments.Where(x => x.LCCode == txtCode.Text).FirstOrDefault();
                if (dsDetails == null)
                {
                    var drDetails = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();
                    if (drDetails == null)
                    {
                        lblError.Text = "This Docs was not found";
                        return;
                    }
                    else
                    {
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
                        //Charge
                        txtVatNo.Text = bd.IssueLC.GetVatNo();
                        //
                        setToolbar(1);
                        return;
                    }
                }
                else
                { 
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
                        var dsCharge = entContext.B_ExportLCPaymentCharges.Where(x => x.PaymentId == dsMT756.PaymentId).FirstOrDefault();


                    }
                }
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
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    CommitData();
                    break;
            }
        }
        private void CommitData()
        {
            long paymentId = Convert.ToInt64(txtPaymentId.Value);
        }
    }
}