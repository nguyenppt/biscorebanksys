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

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class Payment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private const int tabSightPayment = 211;
        private const int tabMatureAcceptance = 212;
        private const double VAT = 0.1;
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            setToolbar(0);
            lblError.Text = "";
            //ChargeCode
            DataTable tblList;/* = bd.SQLData.B_BCHARGECODE_GetByViewType(this.TabId);
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeCode, "Name_EN", "Code", tblList);*/
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
            //
            if (!String.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                DataSet ds = bd.IssueLC.ImportLCPaymentDetail(null, Convert.ToInt64(Request.QueryString["tid"]));
                if (ds == null || ds.Tables.Count <= 0)
                {
                    lblError.Text = "Can not found this transaction !";
                    return;
                }
                //
                bc.Commont.SetTatusFormControls(this.Controls, false);
                loadPaymentDetail(ds);
                //
                if (!String.IsNullOrEmpty(Request.QueryString["lst"]))
                    setToolbar(2);
            }
        }

        private void loadPaymentDetail(DataSet dsDetail)
        {
            //MAIN
            DataTable tDetail = dsDetail.Tables[0];
            DataRow dr = tDetail.Rows[0];
            txtCustomerID.Value = dr["CustomerID"].ToString();
            txtCustomerName.Value = dr["CustomerName"].ToString();
            txtCode.Text = dr["LCCode"].ToString();
            txtPaymentId.Value = dr["PaymentId"].ToString();
            txtDrawingAmount.Value = Convert.ToDouble(dr["DrawingAmount"]);
            if (dr["AmountCredited"] != DBNull.Value)
                txtAmountCredited.Value = Convert.ToDouble(dr["AmountCredited"]);
            bc.Commont.initRadComboBox(ref cboDepositAccount, "AccountName", "DepositCode", bd.IssueLC.GetDepositAccount(dr["CustomerID"].ToString(), dr["Currency"].ToString()));
            cboDepositAccount.SelectedValue = dr["DepositAccount"].ToString();
            cboPaymentMethod.SelectedValue = dr["PaymentMethod"].ToString();
            bc.Commont.initRadComboBox(ref cboNostroAcct, "Description", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(dr["Currency"].ToString()));
            cboNostroAcct.SelectedValue = dr["NostroAcct"].ToString();
            txtFullyUtilised.Text = dr["FullyUtilised"].ToString();
            cboWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
            txtChargeRemarks.Text = dr["ChargeRemarks"].ToString();
            txtVatNo.Text = dr["VATNo"].ToString();
            cboDrawType.SelectedValue = dr["DrawType"].ToString();
            lblCurrency.Text = dr["Currency"].ToString();
            txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
            txtValueDate.SelectedDate = Convert.ToDateTime(dr["BookingDate"]);
            txtPaymentRemarks.Text = dr["PaymentRemarks"].ToString();
            //Charge
            cboWaiveCharges.SelectedValue = bd.YesNo.NO;
            tDetail = dsDetail.Tables[1];
            if (tDetail != null && tDetail.Rows.Count > 0)
            {
                cboWaiveCharges.SelectedValue = bd.YesNo.YES;
                divWaiveCharges.Attributes.CssStyle.Add("display", "");
                DataRow drCharge;
                DataRow[] drList = tDetail.Select("ChargeTab = 'tabCableCharge'");
                if (drList.Length > 0)
                {
                    drCharge = drList[0];
                    loadCharge(drCharge, ref tabCableCharge_cboChargeCode, ref tabCableCharge_cboChargeCcy, ref tabCableCharge_cboChargeAcc, ref tabCableCharge_txtChargeAmt, ref tabCableCharge_cboPartyCharged, ref tabCableCharge_cboAmortCharge, ref tabCableCharge_txtTaxCode, ref tabCableCharge_txtTaxAmt);
                }
                drList = tDetail.Select("ChargeTab = 'tabPaymentCharge'");
                if (drList.Length > 0)
                {
                    drCharge = drList[0];
                    loadCharge(drCharge, ref tabPaymentCharge_cboChargeCode, ref tabPaymentCharge_cboChargeCcy, ref tabPaymentCharge_cboChargeAcc, ref tabPaymentCharge_txtChargeAmt, ref tabPaymentCharge_cboPartyCharged, ref tabPaymentCharge_cboAmortCharge, ref tabPaymentCharge_txtTaxCode, ref tabPaymentCharge_txtTaxAmt);
                }
                drList = tDetail.Select("ChargeTab = 'tabHandlingCharge'");
                if (drList.Length > 0)
                {
                    drCharge = drList[0];
                    loadCharge(drCharge, ref tabHandlingCharge_cboChargeCode, ref tabHandlingCharge_cboChargeCcy, ref tabHandlingCharge_cboChargeAcc, ref tabHandlingCharge_txtChargeAmt, ref tabHandlingCharge_cboPartyCharged, ref tabHandlingCharge_cboAmortCharge, ref tabHandlingCharge_txtTaxCode, ref tabHandlingCharge_txtTaxAmt);
                }
                drList = tDetail.Select("ChargeTab = 'tabDiscrepenciesCharge'");
                if (drList.Length > 0)
                {
                    drCharge = drList[0];
                    loadCharge(drCharge, ref tabDiscrepenciesCharge_cboChargeCode, ref tabDiscrepenciesCharge_cboChargeCcy, ref tabDiscrepenciesCharge_cboChargeAcc, ref tabDiscrepenciesCharge_txtChargeAmt, ref tabDiscrepenciesCharge_cboPartyCharged, ref tabDiscrepenciesCharge_cboAmortCharge, ref tabDiscrepenciesCharge_txtTaxCode, ref tabDiscrepenciesCharge_txtTaxAmt);
                }
                drList = tDetail.Select("ChargeTab = 'tabOtherCharge'");
                if (drList.Length > 0)
                {
                    drCharge = drList[0];
                    loadCharge(drCharge, ref tabOtherCharge_cboChargeCode, ref tabOtherCharge_cboChargeCcy, ref tabOtherCharge_cboChargeAcc, ref tabOtherCharge_txtChargeAmt, ref tabOtherCharge_cboPartyCharged, ref tabOtherCharge_cboAmortCharge, ref tabOtherCharge_txtTaxCode, ref tabOtherCharge_txtTaxAmt);
                }
            }
            else divWaiveCharges.Attributes.CssStyle.Add("display", "none");
            //MT 202
            tDetail = dsDetail.Tables[2];
            if (tDetail != null && tDetail.Rows.Count > 0)
            {
                dr = tDetail.Rows[0];
                lblTransactionReferenceNumber.Text = dr["TransactionReferenceNumber"].ToString();
                txtRelatedReference.Text = dr["RelatedReference"].ToString();
                if (dr["ValueDate"] != DBNull.Value)
                    dteValueDate_MT202.SelectedDate = Convert.ToDateTime(dr["ValueDate"]);
                comboCurrency.SelectedValue = dr["Currency"].ToString();
                if (dr["Amount"] != DBNull.Value)
                    numAmount.Value = Convert.ToDouble(dr["Amount"]);
                lblOrderingInstitution.Text = dr["OrderingInstitution"].ToString();
                lblSenderCorrespondent1.Text = dr["SenderCorrespondent1"].ToString();
                lblSenderCorrespondent2.Text = dr["SenderCorrespondent2"].ToString();
                lblReceiverCorrespondentName2.Text = dr["ReceiverCorrespondent1"].ToString();
                txtIntermediaryBank.Text = dr["IntermediaryBank"].ToString();
                txtAccountWithInstitution.Text = dr["AccountWithInstitution"].ToString();
                txtBeneficiaryBank.Text = dr["BeneficiaryBank"].ToString();
                txtSenderToReceiverInformation.Text = dr["SenderToReceiverInformation"].ToString();
                comboIntermediaryBankType.SelectedValue = dr["IntermediaryBankType"].ToString();
                txtIntermediaryBankName.Text = dr["IntermediaryBankName"].ToString();
                txtIntermediaryBankAddr1.Text = dr["IntermediaryBankAddr1"].ToString();
                txtIntermediaryBankAddr2.Text = dr["IntermediaryBankAddr2"].ToString();
                txtIntermediaryBankAddr3.Text = dr["IntermediaryBankAddr3"].ToString();
                comboAccountWithInstitutionType.SelectedValue = dr["AccountWithInstitutionType"].ToString();
                txtAccountWithInstitutionName.Text = dr["AccountWithInstitutionName"].ToString();
                txtAccountWithInstitutionAddr1.Text = dr["AccountWithInstitutionAddr1"].ToString();
                txtAccountWithInstitutionAddr2.Text = dr["AccountWithInstitutionAddr2"].ToString();
                txtAccountWithInstitutionAddr3.Text = dr["AccountWithInstitutionAddr3"].ToString();
                comboBeneficiaryBankType.SelectedValue = dr["BeneficiaryBankType"].ToString();
                txtBeneficiaryBankName.Text = dr["BeneficiaryBankName"].ToString();
                txtBeneficiaryBankAddr1.Text = dr["BeneficiaryBankAddr1"].ToString();
                txtBeneficiaryBankAddr2.Text = dr["BeneficiaryBankAddr2"].ToString();
                txtBeneficiaryBankAddr3.Text = dr["BeneficiaryBankAddr3"].ToString();
                txtSenderToReceiverInformation2.Text = dr["SenderToReceiverInformation2"].ToString();
                txtSenderToReceiverInformation3.Text = dr["SenderToReceiverInformation3"].ToString();
            }
            //MT 756
            comboCreateMT756.SelectedValue = bd.YesNo.NO;
            tDetail = dsDetail.Tables[3];
            if (tDetail != null && tDetail.Rows.Count > 0)
            {
                dr = tDetail.Rows[0];
                comboCreateMT756.SelectedValue = bd.YesNo.YES;
                divMT756.Attributes.CssStyle.Add("display", "");
                txtSendingBankTRN.Text = dr["SendingBankTRN"].ToString();
                txtRelatedReferenceMT400.Text = dr["RelatedReference"].ToString();
                if (dr["AmountCollected"] != DBNull.Value)
                    numAmountCollected.Value = Convert.ToDouble(dr["AmountCollected"]);
                if (dr["ValueDate"] != DBNull.Value)
                    dteValueDate_MT400.SelectedDate = Convert.ToDateTime(dr["ValueDate"]);
                comboCurrency_MT400.SelectedValue = dr["Currency"].ToString();
                if (dr["Amount"] != DBNull.Value)
                    numAmount_MT400.Value = Convert.ToDouble(dr["Amount"]);
                txtDetailOfCharges1.Text = dr["DetailOfCharges1"].ToString();
                txtDetailOfCharges2.Text = dr["DetailOfCharges2"].ToString();
                comboReceiverCorrespondentType.SelectedValue = dr["ReceiverCorrespondentType"].ToString();
                txtReceiverCorrespondentNo.Text = dr["ReceiverCorrespondentNo"].ToString();
                txtReceiverCorrespondentName.Text = dr["ReceiverCorrespondentName"].ToString();
                txtReceiverCorrespondentAddr1.Text = dr["ReceiverCorrespondentAddr1"].ToString();
                txtReceiverCorrespondentAddr2.Text = dr["ReceiverCorrespondentAddr2"].ToString();
                txtReceiverCorrespondentAddr3.Text = dr["ReceiverCorrespondentAddr3"].ToString();
                comboSenderCorrespondentType.SelectedValue = dr["SenderCorrespondentType"].ToString();
                txtSenderCorrespondentNo.Text = dr["SenderCorrespondentNo"].ToString();
                txtSenderCorrespondentName.Text = dr["SenderCorrespondentName"].ToString();
                txtSenderCorrespondentAddress1.Text = dr["SenderCorrespondentAddr1"].ToString();
                txtSenderCorrespondentAddress2.Text = dr["SenderCorrespondentAddr2"].ToString();
                txtSenderCorrespondentAddress3.Text = dr["SenderCorrespondentAddr3"].ToString();
                txtSenderToReceiverInformation1_400_1.Text = dr["SenderToReceiverInformation1"].ToString();
                txtSenderToReceiverInformation1_400_2.Text = dr["SenderToReceiverInformation2"].ToString();
                txtSenderToReceiverInformation1_400_3.Text = dr["SenderToReceiverInformation3"].ToString();
                txtDetailOfCharges3.Text = dr["DetailOfCharges3"].ToString();
            }
            else divMT756.Attributes.CssStyle.Add("display", "none");
        }
        private void loadCharge(DataRow drCharge, ref RadComboBox cboChargeCode, ref RadComboBox cboChargeCcy, ref RadComboBox cboChargeAcc, ref RadNumericTextBox txtChargeAmt
            , ref RadComboBox cboPartyCharged, ref RadComboBox cboAmortCharge, ref RadTextBox txtTaxCode, ref RadNumericTextBox txtTaxAmt)
        {
            cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
            cboChargeCcy.SelectedValue = drCharge["ChargeCcy"].ToString();
            bc.Commont.initRadComboBox(ref cboChargeAcc, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, cboChargeCcy.SelectedValue));
            cboChargeAcc.SelectedValue = drCharge["ChargeAcct"].ToString();
            if (drCharge["ChargeAmt"] != DBNull.Value)
                txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
            cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
            cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
            txtTaxCode.Text = drCharge["TaxCode"].ToString();
            if (drCharge["TaxAmt"] != DBNull.Value)
                txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
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
                    break;
                case bc.Commands.Authorize:
                    bd.IssueLC.ImportLCPaymentUpdateStatus(Convert.ToInt64(txtPaymentId.Value), bd.TransactionStatus.AUT, this.UserId.ToString());
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Reverse:
                    bd.IssueLC.ImportLCPaymentUpdateStatus(Convert.ToInt64(txtPaymentId.Value), bd.TransactionStatus.REV, this.UserId.ToString());
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
            }
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
        
        private void CommitData()
        {
            long paymentId = Convert.ToInt64(txtPaymentId.Value);
            DataTable tResult = bd.IssueLC.ImportLCPaymentUpdate(paymentId, txtCode.Text, cboDrawType.SelectedValue, txtDrawingAmount.Value, lblCurrency.Text, cboDepositAccount.SelectedValue, txtExchangeRate.Value,
                    txtAmtDrFromAcct.Value, txtProvExchangeRate.Value, "", txtProvExchangeRate.Value, txtCoverAmount.Value, cboPaymentMethod.SelectedValue, cboNostroAcct.SelectedValue, txtAmountCredited.Value, txtPaymentRemarks.Text, txtFullyUtilised.Text,
                    cboWaiveCharges.SelectedValue, txtChargeRemarks.Text, txtVatNo.Text, this.UserId.ToString());
            if (paymentId <= 0)
            {
                if (tResult == null || tResult.Rows.Count != 1)
                {
                    lblError.Text = "Commit error !";
                    return;
                }
                paymentId = Convert.ToInt64(tResult.Rows[0]["paymentID"]);
            }
            //
            bd.IssueLC.ImportLCPaymentMT202Update(paymentId, txtCode.Text, lblTransactionReferenceNumber.Text, txtRelatedReference.Text, dteValueDate_MT202.SelectedDate, comboCurrency.SelectedValue, numAmount.Value, lblOrderingInstitution.Text,
                lblSenderCorrespondent1.Text, lblSenderCorrespondent2.Text, "", lblReceiverCorrespondentName2.Text, txtIntermediaryBank.Text, txtAccountWithInstitution.Text, txtBeneficiaryBank.Text, txtSenderToReceiverInformation.Text,
                comboIntermediaryBankType.SelectedValue, txtIntermediaryBankName.Text, txtIntermediaryBankAddr1.Text, txtIntermediaryBankAddr2.Text, txtIntermediaryBankAddr3.Text, comboAccountWithInstitutionType.SelectedValue,
                txtAccountWithInstitutionName.Text, txtAccountWithInstitutionAddr1.Text, txtAccountWithInstitutionAddr2.Text, txtAccountWithInstitutionAddr3.Text, comboBeneficiaryBankType.SelectedValue, txtBeneficiaryBankName.Text,
                txtBeneficiaryBankAddr1.Text, txtBeneficiaryBankAddr2.Text, txtBeneficiaryBankAddr3.Text, txtSenderToReceiverInformation2.Text, txtSenderToReceiverInformation3.Text);

            if (comboCreateMT756.SelectedValue.Equals(bd.YesNo.YES))
            {
                bd.IssueLC.ImportLCPaymentMT756Update(paymentId, txtCode.Text, "", txtSendingBankTRN.Text, txtRelatedReferenceMT400.Text, numAmountCollected.Value, dteValueDate_MT400.SelectedDate, comboCurrency_MT400.SelectedValue, numAmount_MT400.Value, "", "", "", "",
                txtDetailOfCharges1.Text, txtDetailOfCharges2.Text, comboReceiverCorrespondentType.SelectedValue, txtReceiverCorrespondentNo.Text, txtReceiverCorrespondentName.Text,
                txtReceiverCorrespondentAddr1.Text, txtReceiverCorrespondentAddr2.Text, txtReceiverCorrespondentAddr3.Text, comboSenderCorrespondentType.SelectedValue,
                txtSenderCorrespondentNo.Text, txtSenderCorrespondentName.Text, txtSenderCorrespondentAddress1.Text, txtSenderCorrespondentAddress2.Text, txtSenderCorrespondentAddress3.Text,
                txtSenderToReceiverInformation1_400_1.Text, txtSenderToReceiverInformation1_400_2.Text, txtSenderToReceiverInformation1_400_3.Text, txtDetailOfCharges3.Text );
            }
            //
            if (cboWaiveCharges.SelectedValue.Equals(bd.YesNo.YES))
            {
                bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabCableCharge", tabCableCharge_cboChargeCode.SelectedValue, tabCableCharge_cboChargeAcc.SelectedValue, tabCableCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabCableCharge_txtExchangeRate.Value,
                    tabCableCharge_txtChargeAmt.Value, tabCableCharge_cboPartyCharged.SelectedValue, tabCableCharge_cboAmortCharge.SelectedValue, tabCableCharge_cboChargeStatus.SelectedValue, tabCableCharge_txtTaxCode.Text, tabCableCharge_txtTaxAmt.Value);
                bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabPaymentCharge", tabPaymentCharge_cboChargeCode.SelectedValue, tabPaymentCharge_cboChargeAcc.SelectedValue, tabPaymentCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabPaymentCharge_txtExchangeRate.Value,
                    tabPaymentCharge_txtChargeAmt.Value, tabPaymentCharge_cboPartyCharged.SelectedValue, tabPaymentCharge_cboAmortCharge.SelectedValue, tabPaymentCharge_cboChargeStatus.SelectedValue, tabPaymentCharge_txtTaxCode.Text, tabPaymentCharge_txtTaxAmt.Value);
                bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabHandlingCharge", tabHandlingCharge_cboChargeCode.SelectedValue, tabHandlingCharge_cboChargeAcc.SelectedValue, tabHandlingCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabHandlingCharge_txtExchangeRate.Value,
                    tabHandlingCharge_txtChargeAmt.Value, tabHandlingCharge_cboPartyCharged.SelectedValue, tabHandlingCharge_cboAmortCharge.SelectedValue, tabHandlingCharge_cboChargeStatus.SelectedValue, tabHandlingCharge_txtTaxCode.Text, tabHandlingCharge_txtTaxAmt.Value);
                bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabDiscrepenciesCharge", tabDiscrepenciesCharge_cboChargeCode.SelectedValue, tabDiscrepenciesCharge_cboChargeAcc.SelectedValue, tabDiscrepenciesCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabDiscrepenciesCharge_txtExchangeRate.Value,
                    tabDiscrepenciesCharge_txtChargeAmt.Value, tabDiscrepenciesCharge_cboPartyCharged.SelectedValue, tabDiscrepenciesCharge_cboAmortCharge.SelectedValue, tabDiscrepenciesCharge_cboChargeStatus.SelectedValue, tabDiscrepenciesCharge_txtTaxCode.Text, tabDiscrepenciesCharge_txtTaxAmt.Value);
                bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabOtherCharge", tabOtherCharge_cboChargeCode.SelectedValue, tabOtherCharge_cboChargeAcc.SelectedValue, tabOtherCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabOtherCharge_txtExchangeRate.Value,
                    tabOtherCharge_txtChargeAmt.Value, tabOtherCharge_cboPartyCharged.SelectedValue, tabOtherCharge_cboAmortCharge.SelectedValue, tabOtherCharge_cboChargeStatus.SelectedValue, tabOtherCharge_txtTaxCode.Text, tabOtherCharge_txtTaxAmt.Value);
            }
            //
            Response.Redirect("Default.aspx?tabid=" + this.TabId);
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
        //
        protected void tabCableCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabCableCharge_txtChargeAmt, tabCableCharge_cboPartyCharged, ref tabCableCharge_txtTaxAmt, ref tabCableCharge_txtTaxCode);
        }
        protected void tabCableCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabCableCharge_txtChargeAmt, tabCableCharge_cboPartyCharged, ref tabCableCharge_txtTaxAmt, ref tabCableCharge_txtTaxCode);
        }
        //
        protected void tabPaymentCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabPaymentCharge_txtChargeAmt, tabPaymentCharge_cboPartyCharged, ref tabPaymentCharge_txtTaxAmt, ref tabPaymentCharge_txtTaxCode);
        }
        protected void tabPaymentCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabPaymentCharge_txtChargeAmt, tabPaymentCharge_cboPartyCharged, ref tabPaymentCharge_txtTaxAmt, ref tabPaymentCharge_txtTaxCode);
        }
        //
        protected void tabHandlingCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabHandlingCharge_txtChargeAmt, tabHandlingCharge_cboPartyCharged, ref tabHandlingCharge_txtTaxAmt, ref tabHandlingCharge_txtTaxCode);
        }
        protected void tabHandlingCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabHandlingCharge_txtChargeAmt, tabHandlingCharge_cboPartyCharged, ref tabHandlingCharge_txtTaxAmt, ref tabHandlingCharge_txtTaxCode);
        }
        //
        protected void tabDiscrepenciesCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabDiscrepenciesCharge_txtChargeAmt, tabDiscrepenciesCharge_cboPartyCharged, ref tabDiscrepenciesCharge_txtTaxAmt, ref tabDiscrepenciesCharge_txtTaxCode);
        }
        protected void tabDiscrepenciesCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabDiscrepenciesCharge_txtChargeAmt, tabDiscrepenciesCharge_cboPartyCharged, ref tabDiscrepenciesCharge_txtTaxAmt, ref tabDiscrepenciesCharge_txtTaxCode);
        }
        //
        protected void tabOtherCharge_txtChargeAmt_TextChanged(object sender, EventArgs e)
        {
            calculateTaxAmt(tabOtherCharge_txtChargeAmt, tabOtherCharge_cboPartyCharged, ref tabOtherCharge_txtTaxAmt, ref tabOtherCharge_txtTaxCode);
        }
        protected void tabOtherCharge_cboPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateTaxAmt(tabOtherCharge_txtChargeAmt, tabOtherCharge_cboPartyCharged, ref tabOtherCharge_txtTaxAmt, ref tabOtherCharge_txtTaxCode);
        }
        //
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

        protected void btnLoadDocsInfo_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            DataSet dsDetail = bd.IssueLC.ImportLCPaymentDetail(txtCode.Text, null);
            DataTable tbDetail;
            DataRow dr;
            if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                tbDetail = dsDetail.Tables[0];
                dr = tbDetail.Rows[0];
                if (dr["Status"].ToString().Equals(bd.TransactionStatus.AUT))
                {
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    setToolbar(0);
                }
                else setToolbar(1);
                loadPaymentDetail(dsDetail);
                //lblError.Text = "This Docs payment waiting for approve !";
                return;
            }
            //
            tbDetail = bd.IssueLC.GetDocForPayment(txtCode.Text);
            if (tbDetail == null || tbDetail.Rows.Count <= 0)
            {
                lblError.Text = "This Docs not found !";
                return;
            }
            //
            dr = tbDetail.Rows[0];
            if (dr["Status"].ToString().Equals(bd.TransactionStatus.AUT))
            {
                if (dr["RejectStatus"] != DBNull.Value)
                {
                    if (!dr["RejectStatus"].ToString().Equals(bd.TransactionStatus.REV))
                    {
                        lblError.Text = "This Docs is waiting for reject !";
                        return;
                    }
                }
                if (Convert.ToInt32(dr["PaymentFullFlag"]) != 0)
                {
                    lblError.Text = "This Doc is already payment completed !";
                    return;
                }
                //Main
                txtCustomerID.Value = dr["CustomerID"].ToString();
                txtCustomerName.Value = dr["CustomerName"].ToString();
                lblCurrency.Text = dr["Currency"].ToString();
                txtDrawingAmount.Value = Convert.ToDouble(dr["Amount"]);
                txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
                txtValueDate.SelectedDate = Convert.ToDateTime(dr["BookingDate"]);
                bc.Commont.initRadComboBox(ref cboDepositAccount, "AccountName", "DepositCode", bd.IssueLC.GetDepositAccount(dr["CustomerID"].ToString(), dr["Currency"].ToString()));
                bc.Commont.initRadComboBox(ref cboNostroAcct, "Description", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(dr["Currency"].ToString()));
                txtAmountCredited.Value = 0;
                txtFullyUtilised.Text = bd.YesNo.NO;
                //MT202
                lblTransactionReferenceNumber.Text = txtCode.Text;
                txtRelatedReference.Text = dr["PresentorRefNo"].ToString();
                dteValueDate_MT202.SelectedDate = DateTime.Now;
                comboCurrency.SelectedValue = lblCurrency.Text;
                numAmount.Value = txtDrawingAmount.Value;                
                //MT756
                txtSendingBankTRN.Text = txtCode.Text;
                txtRelatedReferenceMT400.Text = dr["PresentorRefNo"].ToString();
                numAmountCollected.Value = txtDrawingAmount.Value;
                dteValueDate_MT400.SelectedDate = DateTime.Now;
                comboCurrency_MT400.SelectedValue = lblCurrency.Text;
                numAmount_MT400.Value = txtDrawingAmount.Value;
                //Charge
                txtVatNo.Text = bd.IssueLC.GetVatNo();
                //                
                setToolbar(1);
                return;
            }
            lblError.Text = "This Docs has wrong Status (" + dr["Status"] + ") !";            
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

                    if (comboCreateMT756.SelectedValue == bd.YesNo.YES)
                    {
                        txtReceiverCorrespondentNo.Text = txtAccountWithInstitution.Text;
                        txtReceiverCorrespondentName.Text = txtAccountWithInstitutionName.Text;
                    }
                }
                else
                {
                    lblAccountWithInstitutionError.Text = "No found swiftcode";
                }
            }
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

        protected void cboNostroAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Account"] = row["AccountNo"].ToString();
        }

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
    }
}