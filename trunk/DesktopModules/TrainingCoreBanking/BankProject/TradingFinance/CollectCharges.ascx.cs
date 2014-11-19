using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DBContext;
using BankProject.Helper;

namespace BankProject.TradingFinance
{
    public partial class CollectCharges : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        VietVictoryCoreBankingEntities db = new VietVictoryCoreBankingEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            bc.Commont.initRadComboBox(ref cboTransactionType, "Description", "Id", bd.SQLData.CreateGenerateDatas("TabAccountTransfer_TransactionType"));
            bc.Commont.initRadComboBox(ref cboCountryCode, "TenTA", "TenTA", bd.SQLData.B_BCOUNTRY_GetAll());
            bc.Commont.initRadComboBox(ref cboAccountOfficer, "Description", "Code", bd.SQLData.B_BACCOUNTOFFICER_GetAll());
            txtCode.Text = Request.QueryString["tid"];
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                if (cc == null)
                {
                    lblError.Text = "TransCode not found !";
                    return;
                }
                loadTransDetail(cc);
                bc.Commont.SetTatusFormControls(this.Controls, false);
                divCmdChargeType.Visible = false;
                divCmdChargeType1.Visible = false;
                divCmdChargeType2.Visible = false;
                if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                {
                    if (cc.Status.Equals(bd.TransactionStatus.UNA))
                    {
                        //Duyet
                        RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;                        
                        return;
                    }
                }

                return;
            }
            //
            txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("CollectCharges", "FT", ".");
            txtDebitAmount.Value = 0;
            txtCreditAmount.Value = 0;
            txtProcessingDate.SelectedDate = DateTime.Now;
            txtValueDate.SelectedDate = DateTime.Now;
            cboDetailOfCharges.SelectedValue = "SHA";
            txtVATNo.Text = getVATNo();
            lblSenderReference.Text = txtCode.Text;
        }
        private string getVATNo()
        {
            DataSet ds = bd.Database.B_BMACODE_GetNewSoTT("CollectCharges");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["SoTT"].ToString();

            return "0";
        }
        private void DisableDefaultControl()
        {
            txtProcessingDate.Enabled = false;
            txtValueDate.Enabled = false;
            txtInterBankSettleAmount.Enabled = false;
            txtInstructedAmount.Enabled = false;
            txtReceiverCorrespondent.Enabled = false;
            txtVATNo.Enabled = false;
            txtDebitCurrency.Enabled = false;
            txtCreditCurrency.Enabled = false;
        }
        private void loadTransDetail(B_CollectCharges cc)
        {
            //
            cboTransactionType.SelectedValue = cc.TransactionType;
            cboCountryCode.SelectedValue = cc.CountryCode;
            loadCommodityServices();
            cboCommodityServices.SelectedValue = cc.CommodityServices;
            txtOtherInfo.Text = cc.OtherInfo;
            txtOrderCustomerID.Text = cc.OrderCustomerID;
            txtOrderCustomerName.Text = cc.OrderCustomerName;
            loadBDRFROMACCOUNTAccount();
            txtOrderCustomerAddr1.Text = cc.OrderCustomerAddr1;
            txtOrderCustomerAddr2.Text = cc.OrderCustomerAddr2;
            txtOrderCustomerAddr3.Text = cc.OrderCustomerAddr3;
            txtDebitRef.Text = cc.DebitRef;
            cboDebitAcctNo.SelectedValue = cc.DebitAcctNo;
            txtDebitCurrency.Text = cc.DebitCurrency;
            loadBSWIFTCODE();
            txtDebitAmount.Value = cc.DebitAmount;
            cboCreditAccount.SelectedValue = cc.CreditAccount;
            txtCreditCurrency.Text = cc.CreditCurrency;
            txtCreditAmount.Value = cc.CreditAmount;
            txtProcessingDate.SelectedDate = cc.ProcessingDate;
            txtAddRemarks.Text = cc.AddRemarks;
            //
            B_CollectCharges_MT103 cc1 = db.B_CollectCharges_MT103.Where(p => p.TransCode.Equals(cc.TransCode)).FirstOrDefault();
            if (cc1 != null)
            {
                lblSenderReference.Text = cc1.SenderReference;
                lblBankOperationCode.Text = cc1.BankOperationCode;
                txtValueDate.SelectedDate = cc1.ValueDate;
                lblCurrency.Text = cc1.Currency;
                txtInterBankSettleAmount.Value = cc1.InterBankSettleAmount;
                txtInstructedAmount.Value = cc1.InstructedAmount;
                cboOrderingCustomerAcc.SelectedValue = cc1.OrderingCustAcc;
                txtOrderingCustomerName.Text = cc1.OrderingCustAccName;
                txtOrderingCustomerAddr1.Text = cc1.OrderingCustAccAddr1;
                txtOrderingCustomerAddr2.Text = cc1.OrderingCustAccAddr2;
                txtOrderingCustomerAddr3.Text = cc1.OrderingCustAccAddr3;
                txtOrderingInstitution.Text = cc1.OrderingInstitution;
                lblSenderCorrespondent.Text = cc1.SenderCorrespondent;
                txtReceiverCorrespondent.Text = cc1.ReceiverCorrespondent;
                lblReceiverCorrespondentName.Text = cc1.ReceiverCorrespondentName;
                txtReceiverCorrBankAcct.Text = cc1.ReceiverCorrBankAct;
                cboIntermediaryType.SelectedValue = cc1.IntermediaryType;
                txtIntermediaryInstitution.Text = cc1.IntermediaryInstitution;
                lblIntermediaryInstitutionName.Text = cc1.IntermediaryInstitutionName;
                txtIntermediaryAcct1.Text = cc1.IntermediaryAcct1;
                txtIntermediaryAcct2.Text = cc1.IntermediaryAcct2;
                txtIntermediaryBankAcct.Text = cc1.IntermediaryBankAcct;
                cboAccountType.SelectedValue = cc1.AccountType;
                txtAccountWithInstitution.Text = cc1.AccountWithInstitution;
                lblAccountWithInstitutionName.Text = cc1.AccountWithInstitutionName;
                txtAccountWithBankAcct1.Text = cc1.AccountWithBankAcct1;
                txtAccountWithBankAcct2.Text = cc1.AccountWithBankAcct2;
                txtBeneficiaryAccount.Text = cc1.BeneficiaryAccount;
                txtBeneficiaryName.Text = cc1.BeneficiaryName;
                txtBeneficiaryAddr1.Text = cc1.BeneficiaryAddr1;
                txtBeneficiaryAddr2.Text = cc1.BeneficiaryAddr2;
                txtBeneficiaryAddr3.Text = cc1.BeneficiaryAddr3;
                txtRemittanceInformation.Text = cc1.RemittanceInformation;
                cboDetailOfCharges.SelectedValue = cc1.DetailOfCharges;
                if (cc1.SenderCharges.HasValue)
                    lblSenderCharges.Text = String.Format("{0:C}", cc1.SenderCharges.Value).Replace("$", "");
                if (cc1.ReceiverCharges.HasValue)
                    lblReceiverCharges.Text = String.Format("{0:C}", cc1.ReceiverCharges.Value).Replace("$", "");
                txtSendertoReceiverInfo.Text = cc1.SenderToReceiveInfo;
            }
            //
            B_CollectCharges_ChargeCommission cc2 = db.B_CollectCharges_ChargeCommission.Where(p => p.TransCode.Equals(cc.TransCode)).FirstOrDefault();
            if (cc2 != null)
            {
                cboChargeAcct.SelectedValue = cc2.ChargeAcct;
                lblChargeCurrency.Text = cc2.ChargeCurrency;
                cboTransactionType_ChargeCommission.SelectedValue = cc2.TransactionType;
                loadChargeType();
                cboChargeType.SelectedValue = cc2.ChargeType1;
                txtChargeAmount.Value = cc2.ChargeAmount1;
                divChargeType1.Attributes.CssStyle.Remove("Display");
                if (!string.IsNullOrEmpty(cc2.ChargeType2))
                {
                    cboChargeType1.SelectedValue = cc2.ChargeType2;
                    txtChargeAmount1.Value = cc2.ChargeAmount2;
                }
                else divChargeType1.Attributes.CssStyle.Add("Display", "none");
                divChargeType2.Attributes.CssStyle.Remove("Display");
                if (!string.IsNullOrEmpty(cc2.ChargeType3))
                {
                    cboChargeType2.SelectedValue = cc2.ChargeType3;
                    txtChargeAmount2.Value = cc2.ChargeAmount3;
                }
                else divChargeType2.Attributes.CssStyle.Add("Display", "none");
                cboChargeFor.SelectedValue = cc2.ChargeFor;
                txtVATNo.Text = cc2.VATNo;
                txtAddRemarks_Charges1.Text = cc2.AddRemarks1;
                txtAddRemarks_Charges2.Text = cc2.AddRemarks2;
                cboAccountOfficer.SelectedValue = cc2.AccountOfficer;
                if (cc2.TotalChargeAmount.HasValue)
                    lblTotalChargeAmount.Text = String.Format("{0:C}", cc2.TotalChargeAmount.Value).Replace("$", "");
                if (cc2.TotalTaxAmount.HasValue)
                    lblTotalTaxAmount.Text = String.Format("{0:C}", cc2.TotalTaxAmount.Value).Replace("$", "");
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            B_CollectCharges cc;
            switch (commandName)
            {
                case bc.Commands.Commit:                    
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    bool isUpdate = (cc != null);
                    if (!isUpdate)
                    {
                        cc = new B_CollectCharges();
                        cc.TransCode = txtCode.Text;
                        cc.Status = bd.TransactionStatus.UNA;
                        cc.CreateDate = DateTime.Now;
                        cc.CreateBy = this.UserInfo.Username;
                    }
                    else
                    {
                        cc.UpdatedBy = this.UserInfo.Username;
                        cc.UpdatedDate = DateTime.Now;
                    }
                    //
                    cc.TransactionType = cboTransactionType.SelectedValue;
                    cc.CountryCode = cboCountryCode.SelectedValue;
                    cc.CommodityServices = cboCommodityServices.SelectedValue;
                    cc.OtherInfo = txtOtherInfo.Text;
                    cc.OrderCustomerID = txtOrderCustomerID.Text;
                    cc.OrderCustomerName = txtOrderCustomerName.Text;                    
                    cc.OrderCustomerAddr1 = txtOrderCustomerAddr1.Text;
                    cc.OrderCustomerAddr2 = txtOrderCustomerAddr2.Text;
                    cc.OrderCustomerAddr3 = txtOrderCustomerAddr3.Text;
                    cc.DebitRef = txtDebitRef.Text;
                    cc.DebitAcctNo = cboDebitAcctNo.SelectedValue;
                    cc.DebitCurrency = txtDebitCurrency.Text;                    
                    cc.DebitAmount = txtDebitAmount.Value;
                    cc.CreditAccount = cboCreditAccount.SelectedValue;
                    cc.CreditCurrency = txtCreditCurrency.Text;
                    cc.CreditAmount = txtCreditAmount.Value;
                    cc.ProcessingDate = txtProcessingDate.SelectedDate;
                    cc.AddRemarks = txtAddRemarks.Text;
                    if (!isUpdate) db.B_CollectCharges.Add(cc);
                    //
                    B_CollectCharges_MT103 cc1 = db.B_CollectCharges_MT103.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    isUpdate = (cc1 != null);
                    if (!isUpdate)
                    {
                        cc1 = new B_CollectCharges_MT103();
                        cc1.TransCode = txtCode.Text;
                    }
                    cc1.SenderReference = lblSenderReference.Text;
                    cc1.BankOperationCode = lblBankOperationCode.Text;
                    cc1.ValueDate = txtValueDate.SelectedDate;
                    cc1.Currency = lblCurrency.Text;
                    cc1.InterBankSettleAmount = txtInterBankSettleAmount.Value;
                    cc1.InstructedAmount = txtInstructedAmount.Value;
                    cc1.OrderingCustAcc = cboOrderingCustomerAcc.SelectedValue;
                    cc1.OrderingCustAccName = txtOrderingCustomerName.Text;                    
                    cc1.OrderingCustAccAddr1 = txtOrderingCustomerAddr1.Text;
                    cc1.OrderingCustAccAddr2 = txtOrderingCustomerAddr2.Text;
                    cc1.OrderingCustAccAddr3 = txtOrderingCustomerAddr3.Text;
                    cc1.OrderingInstitution = txtOrderingInstitution.Text;
                    cc1.SenderCorrespondent = lblSenderCorrespondent.Text;
                    //cc1.SenderCorrespondentName = lblse
                    cc1.ReceiverCorrespondent = txtReceiverCorrespondent.Text;
                    cc1.ReceiverCorrespondentName = lblReceiverCorrespondentName.Text;
                    cc1.ReceiverCorrBankAct = txtReceiverCorrBankAcct.Text;
                    cc1.IntermediaryType = cboIntermediaryType.SelectedValue;
                    cc1.IntermediaryInstitution = txtIntermediaryInstitution.Text;
                    cc1.IntermediaryInstitutionName = lblIntermediaryInstitutionName.Text;
                    cc1.IntermediaryAcct1 = txtIntermediaryAcct1.Text;
                    cc1.IntermediaryAcct2 = txtIntermediaryAcct2.Text;
                    cc1.IntermediaryBankAcct = txtIntermediaryBankAcct.Text;
                    cc1.AccountType = cboAccountType.SelectedValue;
                    cc1.AccountWithInstitution = txtAccountWithInstitution.Text;
                    cc1.AccountWithInstitutionName = lblAccountWithInstitutionName.Text;
                    cc1.AccountWithBankAcct1 = txtAccountWithBankAcct1.Text;
                    cc1.AccountWithBankAcct2 = txtAccountWithBankAcct2.Text;
                    cc1.BeneficiaryAccount = txtBeneficiaryAccount.Text;
                    cc1.BeneficiaryName = txtBeneficiaryName.Text;
                    cc1.BeneficiaryAddr1 = txtBeneficiaryAddr1.Text;
                    cc1.BeneficiaryAddr2 = txtBeneficiaryAddr2.Text;
                    cc1.BeneficiaryAddr3 = txtBeneficiaryAddr3.Text;
                    cc1.RemittanceInformation = txtRemittanceInformation.Text;
                    cc1.DetailOfCharges = cboDetailOfCharges.SelectedValue;
                    if (!string.IsNullOrEmpty(lblSenderCharges.Text))
                        cc1.SenderCharges = Convert.ToDouble(lblSenderCharges.Text);
                    else
                        cc1.SenderCharges = null;
                    if (!string.IsNullOrEmpty(lblReceiverCharges.Text))
                        cc1.ReceiverCharges = Convert.ToDouble(lblReceiverCharges.Text);
                    else
                        cc1.ReceiverCharges = null;
                    cc1.SenderToReceiveInfo = txtSendertoReceiverInfo.Text;
                    if (!isUpdate) db.B_CollectCharges_MT103.Add(cc1);
                    //
                    B_CollectCharges_ChargeCommission cc2 = db.B_CollectCharges_ChargeCommission.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    isUpdate = (cc2 != null);
                    if (!isUpdate)
                    {
                        cc2 = new B_CollectCharges_ChargeCommission();
                        cc2.TransCode = txtCode.Text;
                    }
                    cc2.ChargeAcct = cboChargeAcct.SelectedValue;
                    cc2.ChargeCurrency = lblChargeCurrency.Text;
                    cc2.TransactionType = cboTransactionType_ChargeCommission.SelectedValue;
                    cc2.ChargeType1 = cboChargeType.SelectedValue;
                    cc2.ChargeAmount1 = txtChargeAmount.Value;
                    cc2.ChargeType2 = cboChargeType1.SelectedValue;
                    cc2.ChargeAmount2 = txtChargeAmount1.Value;
                    cc2.ChargeType3 = cboChargeType2.SelectedValue;
                    cc2.ChargeAmount3 = txtChargeAmount2.Value;
                    cc2.ChargeFor = cboChargeFor.SelectedValue;
                    cc2.VATNo = txtVATNo.Text;
                    cc2.AddRemarks1 = txtAddRemarks_Charges1.Text;
                    cc2.AddRemarks2 = txtAddRemarks_Charges2.Text;
                    cc2.AccountOfficer = cboAccountOfficer.SelectedValue;
                    if (!string.IsNullOrEmpty(lblTotalChargeAmount.Text))
                        cc2.TotalChargeAmount = Convert.ToDouble(lblTotalChargeAmount.Text);
                    else
                        cc2.TotalChargeAmount = null;
                    if (!string.IsNullOrEmpty(lblTotalTaxAmount.Text))
                        cc2.TotalTaxAmount = Convert.ToDouble(lblTotalTaxAmount.Text);
                    else
                        cc2.TotalTaxAmount = null;
                    if (!isUpdate) db.B_CollectCharges_ChargeCommission.Add(cc2);

                    db.SaveChanges();
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Authorize:
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    if (cc == null)
                    {
                        lblError.Text = "TransCode not found !";
                        return;
                    }
                    cc.Status = bd.TransactionStatus.AUT;
                    cc.AuthorizedBy = this.UserInfo.Username;
                    cc.AuthorizedDate = DateTime.Now;

                    db.SaveChanges();

                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Reverse:
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    if (cc == null)
                    {
                        lblError.Text = "TransCode not found !";
                        return;
                    }
                    cc.Status = bd.TransactionStatus.REV;
                    cc.UpdatedBy = this.UserInfo.Username;
                    cc.UpdatedDate = DateTime.Now;

                    db.SaveChanges();
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
            }
        }

        protected void btnLoadCodeInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblError.Text = "Please enter TransCode !";
                return;
            }
            B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
            if (cc == null)
            {
                lblError.Text = "TransCode not found !";
                return;
            }
            loadTransDetail(cc);
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            if (!cc.Status.Equals(bd.TransactionStatus.UNA))
            {
                divCmdChargeType.Visible = false;
                divCmdChargeType1.Visible = false;
                divCmdChargeType2.Visible = false;
                //Preview
                bc.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                return;
            }
            //Cho phep edit
            divCmdChargeType.Visible = true;
            divCmdChargeType1.Visible = true;
            divCmdChargeType2.Visible = true;
            bc.Commont.SetTatusFormControls(this.Controls, true);
            RadToolBar1.FindItemByValue("btCommit").Enabled = true;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            DisableDefaultControl();
        }

        private void loadCommodityServices()
        {
            bc.Commont.initRadComboBox(ref cboCommodityServices, "Name", "Id", bd.SQLData.B_BCOMMODITY_GetByTransactionType(cboTransactionType.SelectedValue.Substring(0, 3)));
        }
        protected void cboTransactionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadCommodityServices();
        }

        private void loadBDRFROMACCOUNTAccount()
        {
            DataTable dsAcc = bd.SQLData.B_BDRFROMACCOUNT_GetByNameWithoutVND(txtOrderCustomerName.Text);
            bc.Commont.initRadComboBox(ref cboDebitAcctNo, "Display", "Id", dsAcc);//Name
            bc.Commont.initRadComboBox(ref cboOrderingCustomerAcc, "Display", "Id", dsAcc);
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", dsAcc); 
        }
        protected void txtOrderCustomerID_OnTextChanged(object sender, EventArgs e)
        {
            txtOrderCustomerName.Text = "";
            txtOrderCustomerAddr1.Text = "";
            txtOrderCustomerAddr2.Text = "";
            txtOrderCustomerAddr3.Text = "";
            cboDebitAcctNo.Items.Clear();
            txtOrderingCustomerName.Text = "";
            txtOrderingCustomerAddr1.Text = "";
            txtOrderingCustomerAddr2.Text = "";
            txtOrderingCustomerAddr3.Text = "";            
            //
            DataSet ds = bd.DataTam.B_BCUSTOMERS_GetbyID(txtOrderCustomerID.Text);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) return;
            //
            DataRow dr = ds.Tables[0].Rows[0];
            txtOrderCustomerName.Text = dr["CustomerName"].ToString();
            txtOrderCustomerAddr1.Text = dr["Address"].ToString();
            txtOrderCustomerAddr2.Text = dr["City"].ToString();
            txtOrderCustomerAddr3.Text = dr["Country"].ToString();
            //
            loadBDRFROMACCOUNTAccount();
            //
            txtOrderingCustomerName.Text = txtOrderCustomerName.Text;
            txtOrderingCustomerAddr1.Text = txtOrderCustomerAddr1.Text;
            txtOrderingCustomerAddr2.Text = txtOrderCustomerAddr2.Text;
            txtOrderingCustomerAddr3.Text = txtOrderCustomerAddr3.Text;
        }
        
        protected void BDRFROMACCOUNT_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }
        protected void cboDebitAcctNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtDebitCurrency.Text = "";
            cboCreditAccount.Items.Clear();
            if (!String.IsNullOrEmpty(cboDebitAcctNo.SelectedValue))
            {
                txtDebitCurrency.Text = cboDebitAcctNo.SelectedItem.Attributes["Currency"].ToString();
                loadBSWIFTCODE();
            }
            txtCreditCurrency.Text = txtDebitCurrency.Text;
            lblCurrency.Text = txtDebitCurrency.Text;
        }

        private void loadBSWIFTCODE()
        {
            bc.Commont.initRadComboBox(ref cboCreditAccount, "AccountNo", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(txtDebitCurrency.Text));
        }
        protected void BSWIFTCODE_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["AccountNo"] = row["AccountNo"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }
        protected void cboCreditAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtCreditCurrency.Text = "";
            txtReceiverCorrespondent.Text = "";
            lblReceiverCorrespondentName.Text = "";
            if (!String.IsNullOrEmpty(cboCreditAccount.SelectedValue))
            {
                RadComboBoxItem cbi = cboCreditAccount.SelectedItem;
                txtCreditCurrency.Text = cbi.Attributes["Currency"].ToString();
                txtReceiverCorrespondent.Text = cbi.Attributes["Code"].ToString();
                lblReceiverCorrespondentName.Text = cbi.Attributes["Description"].ToString();
            }
        }

        protected void txtCreditAmount_OnTextChanged(object sender, EventArgs e)
        {
            txtInstructedAmount.Value = txtCreditAmount.Value;
        }

        protected void txtIntermediaryInstitution_OnTextChanged(object sender, EventArgs e)
        {
            lblIntermediaryInstitutionName.Text = "";
            txtIntermediaryAcct1.Text = "";
            //
            DataTable dt = bd.SQLData.B_BSWIFTCODE_GetByCode(txtIntermediaryInstitution.Text);
            if (dt == null || dt.Rows.Count <= 0) return;
            DataRow dr = dt.Rows[0];
            lblIntermediaryInstitutionName.Text = dr["Description"].ToString();
            txtIntermediaryAcct1.Text = dr["AccountNo"].ToString();
        }
        protected void txtAccountWithInstitution_OnTextChanged(object sender, EventArgs e)
        {
            lblAccountWithInstitutionName.Text = "";
            txtAccountWithBankAcct1.Text = "";
            //
            DataTable dt = bd.SQLData.B_BSWIFTCODE_GetByCode(txtAccountWithInstitution.Text);
            if (dt == null || dt.Rows.Count <= 0) return;
            DataRow dr = dt.Rows[0];
            lblAccountWithInstitutionName.Text = dr["Description"].ToString();
            txtAccountWithBankAcct1.Text = dr["AccountNo"].ToString();
        }

        protected void cboDetailOfCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {            
            lblSenderCharges.Text = string.Empty;
            txtInstructedAmount.Value = 0;
            switch (cboDetailOfCharges.SelectedValue)
            {
                case "SHA":
                case "OUR":
                    if (txtCreditAmount.Value > 0)
                        txtInstructedAmount.Value = txtCreditAmount.Value;

                    break;
                case "BEN":
                    var totalAmount = 0.0;
                    if (txtChargeAmount.Value.HasValue) totalAmount += txtChargeAmount.Value.Value;
                    if (txtChargeAmount1.Value.HasValue) totalAmount += txtChargeAmount1.Value.Value;
                    if (txtChargeAmount2.Value.HasValue) totalAmount += txtChargeAmount2.Value.Value;
                    var totalCharges = ((totalAmount) * (110)) / 100;
                    if (totalCharges > 0)
                        lblSenderCharges.Text = String.Format("{0:C}", totalCharges).Replace("$", "");
                    if (txtCreditAmount.Value > 0)
                        txtInstructedAmount.Value = txtCreditAmount.Value;

                    break; 
            }
        }

        private void loadChargeType()
        {
            cboChargeType.Items.Clear();
            cboChargeType1.Items.Clear();
            cboChargeType2.Items.Clear();
            //
            string TransType = cboTransactionType_ChargeCommission.SelectedValue;
            if (string.IsNullOrEmpty(TransType)) return;
            //
            DataTable dtList = bd.Database.B_BCHARGECODE_ByTransType(TransType);
            if (dtList == null || dtList.Rows.Count <= 0) return;

            bc.Commont.initRadComboBox(ref cboChargeType, "Code", "Code", dtList);
            bc.Commont.initRadComboBox(ref cboChargeType1, "Code", "Code", dtList);
            bc.Commont.initRadComboBox(ref cboChargeType2, "Code", "Code", dtList);
        }
        protected void cboTransactionType_ChargeCommission_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadChargeType();
        }

        protected void cboChargeAcct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeCurrency.Text = "";
            if (!String.IsNullOrEmpty(cboChargeAcct.SelectedValue))
            {
                RadComboBoxItem cbi = cboChargeAcct.SelectedItem;
                lblChargeCurrency.Text = cbi.Attributes["Currency"].ToString();
            }
        }

        private void calculateChargeTotalAmount()
        {
            var totalAmount = 0.0;
            if (!string.IsNullOrEmpty(cboChargeType.SelectedValue) && txtChargeAmount.Value.HasValue) totalAmount += txtChargeAmount.Value.Value;
            if (!string.IsNullOrEmpty(cboChargeType1.SelectedValue) && txtChargeAmount1.Value.HasValue) totalAmount += txtChargeAmount1.Value.Value;
            if (!string.IsNullOrEmpty(cboChargeType2.SelectedValue) && txtChargeAmount2.Value.HasValue) totalAmount += txtChargeAmount2.Value.Value;
            //
            lblTotalChargeAmount.Text = "";
            if (totalAmount > 0) lblTotalChargeAmount.Text = String.Format("{0:C}", totalAmount).Replace("$", "");
            lblTotalTaxAmount.Text = "";
            if (totalAmount > 0 && !string.IsNullOrEmpty(cboChargeFor.SelectedValue))
            {
                switch (cboChargeFor.SelectedValue)
                {
                    case "A":
                    case "B":
                        lblTotalTaxAmount.Text = String.Format("{0:C}", totalAmount * 0.1).Replace("$", "");
                        break;
                    default:
                        //txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value).Replace("$", "");
                        break;
                }
            }
        }
        protected void txtChargeAmount_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void txtChargeAmount1_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void txtChargeAmount2_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void cboChargeFor_ChargeCommission_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateChargeTotalAmount();
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/CollectCharges/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                if (cc == null)
                {
                    lblError.Text = "TransCode not found !";
                    return;
                }

                //reportData = bd.IssueLC.ImportLCPaymentReport(reportType, Convert.ToInt64(1), this.UserInfo.Username);
                switch (reportType)
                {
                    case 1://PhieuChuyenKhoan
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PhieuChuyenKhoan.doc");
                        reportSaveName = "PhieuChuyenKhoan" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var PhieuChuyenKhoan = new Model.CollectCharges.Reports.PhieuChuyenKhoan()
                        {
                            TransCode = cc.TransCode, CreateBy = cc.CreateBy, DebitAcctNo = cc.DebitAcctNo, DebitCurrency = cc.DebitCurrency, DebitAmount = cc.DebitAmount,
                            CreditAccount = cc.CreditAccount, CreditCurrency = cc.CreditCurrency, CreditAmount = cc.CreditAmount, AddRemarks = cc.AddRemarks
                        };
                        PhieuChuyenKhoan.Day = DateTime.Now.Day.ToString().PadLeft(2, '0');
                        PhieuChuyenKhoan.Month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                        PhieuChuyenKhoan.Year = DateTime.Now.Year.ToString();
                        var acc = db.BDRFROMACCOUNTs.Where(p => p.Id.Equals(PhieuChuyenKhoan.DebitAcctNo.Replace("-",""))).FirstOrDefault();
                        if (acc != null)
                            PhieuChuyenKhoan.DebitAcctName = acc.Name;
                        PhieuChuyenKhoan.DebitAmountWord = Utils.ReadNumber(PhieuChuyenKhoan.DebitCurrency, PhieuChuyenKhoan.DebitAmount.Value);
                        var acc1 = db.BSWIFTCODEs.Where(p => p.AccountNo.Equals(PhieuChuyenKhoan.CreditAccount)).FirstOrDefault();
                        if (acc1 != null)
                            PhieuChuyenKhoan.CreditAccountName = acc1.Description;
                        PhieuChuyenKhoan.CreditAmountWord = Utils.ReadNumber(PhieuChuyenKhoan.CreditCurrency, PhieuChuyenKhoan.CreditAmount.Value);

                        var lst = new List<Model.CollectCharges.Reports.PhieuChuyenKhoan>();
                        lst.Add(PhieuChuyenKhoan);
                        reportData = new DataSet();
                        reportData.Tables.Add(Utils.CreateDataTable<Model.CollectCharges.Reports.PhieuChuyenKhoan>(lst));
                        break;
                    case 2://MT 103
                        reportTemplate = Context.Server.MapPath(reportTemplate + "MT103.doc");
                        reportSaveName = "MT103" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        //
                        var cc1 = db.B_CollectCharges_MT103.Where(p => p.TransCode.Equals(cc.TransCode)).FirstOrDefault();
                        if (cc1 == null)
                        {
                            lblError.Text = "TransCode not found !";
                            return;
                        }
                        var MT103 = new Model.CollectCharges.Reports.MT103()
                        {
                            CreditAccount = cc.CreditAccount, TransCode = cc.TransCode, ValueDate = cc1.ValueDate, Currency = cc.DebitCurrency, InterBankSettleAmount = cc1.InterBankSettleAmount,
                            InstructedAmount = cc1.InstructedAmount, OrderingCustAcc = cc1.OrderingCustAcc, OrderingCustAccName = cc1.OrderingCustAccName, OrderingCustAccAddr1 = cc1.OrderingCustAccAddr1,
                            OrderingCustAccAddr2 = cc1.OrderingCustAccAddr2, OrderingCustAccAddr3 = cc1.OrderingCustAccAddr3, AccountWithBankAcct1 = cc1.AccountWithBankAcct1, AccountWithInstitution = cc1.AccountWithInstitution,
                            AccountWithInstitutionName = cc1.AccountWithInstitutionName, BeneficiaryAccount = cc1.BeneficiaryAccount, BeneficiaryName= cc1.BeneficiaryName, BeneficiaryAddr1 = cc1.BeneficiaryAddr1,
                            BeneficiaryAddr2 = cc1.BeneficiaryAddr2, BeneficiaryAddr3 = cc1.BeneficiaryAddr3, RemittanceInformation = cc1.RemittanceInformation, DetailOfCharges = cc1.DetailOfCharges,
                            SenderCharges = cc1.SenderCharges, SenderToReceiveInfo = cc1.SenderToReceiveInfo, IntermediaryInstitution = cc1.IntermediaryInstitution, IntermediaryInstitutionName = cc1.IntermediaryInstitutionName
                        };
                        var acc2 = db.BSWIFTCODEs.Where(p => p.AccountNo.Equals(MT103.CreditAccount)).FirstOrDefault();
                        if (acc2 != null)
                        {
                            MT103.CreditAccountName = acc2.Description;
                        }
                        var lst1 = new List<Model.CollectCharges.Reports.MT103>();
                        lst1.Add(MT103);
                        reportData = new DataSet();
                        reportData.Tables.Add(Utils.CreateDataTable<Model.CollectCharges.Reports.MT103>(lst1));
                        break;
                    case 3://VAT
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var cc2 = db.B_CollectCharges_ChargeCommission.Where(p => p.TransCode.Equals(cc.TransCode)).FirstOrDefault();
                        if (cc2 == null)
                        {
                            lblError.Text = "TransCode not found !";
                            return;
                        }
                        var VAT = new Model.CollectCharges.Reports.VAT()
                        {
                            VATNo = cc2.VATNo, TransCode = cc2.TransCode, UserName = cc.CreateBy, CustomerID = cc.OrderCustomerID, CustomerName = cc.OrderCustomerName,
                            CustomerAddress = cc.OrderCustomerAddr1, ChargeAcct = cc2.ChargeAcct, ChargeRemarks = cc2.AddRemarks1 + " " + cc2.AddRemarks2, 
                            ChargeType1 = cc2.ChargeType1, ChargeAmount1 = cc2.ChargeAmount1, ChargeType2 = cc2.ChargeType2, ChargeAmount2 = cc2.ChargeAmount2, 
                            ChargeType3 = cc2.ChargeType3, ChargeAmount3 = cc2.ChargeAmount3, TotalTaxAmount = cc2.TotalTaxAmount
                        };
                        if (VAT.TotalChargeAmount.HasValue)
                            VAT.TotalChargeAmountWord = Utils.ReadNumber(VAT.ChargeCurrency, VAT.TotalChargeAmount.Value);
                        var lst2 = new List<Model.CollectCharges.Reports.VAT>();
                        lst2.Add(VAT);
                        reportData = new DataSet();
                        reportData.Tables.Add(Utils.CreateDataTable<Model.CollectCharges.Reports.VAT>(lst2));
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
        protected void btnReportPhieuCK_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        protected void btnReportMT103_Click(object sender, EventArgs e)
        {
            showReport(2);
        }
        protected void btnReportVAT_Click(object sender, EventArgs e)
        {
            showReport(3);
        }
    }
}