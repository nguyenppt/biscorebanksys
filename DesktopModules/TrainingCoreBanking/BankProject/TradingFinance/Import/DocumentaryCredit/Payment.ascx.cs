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
            txtPaymentId.Value = "0";
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
            LoadTransInfo();          
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
                case bc.Commands.Authozize:
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

        private void LoadTransInfo()
        {
            setToolbar(0);
            lblError.Text = "";
            //ChargeCode
            DataRow dr;
            DataTable tblList;/* = bd.SQLData.B_BCHARGECODE_GetByViewType(this.TabId);
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeCode, "Name_EN", "Code", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeCode, "Name_EN", "Code", tblList);*/
            //ChargeCcy
            tblList = bd.Database.ExchangeRate();
            bc.Commont.initRadComboBox(ref tabCableCharge_cboChargeCcy, "Title", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboChargeCcy, "Title", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboChargeCcy, "Title", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboChargeCcy, "Title", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboChargeCcy, "Title", "Value", tblList);
            //Party Charged
            tblList = createTableList();
            addData2TableList(ref tblList, "A");
            addData2TableList(ref tblList, "AC");
            addData2TableList(ref tblList, "B");
            addData2TableList(ref tblList, "BC");
            bc.Commont.initRadComboBox(ref tabCableCharge_cboPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabPaymentCharge_cboPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabHandlingCharge_cboPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabDiscrepenciesCharge_cboPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref tabOtherCharge_cboPartyCharged, "Text", "Value", tblList);
            //Amort Charges
            tblList = createTableList();
            addData2TableList(ref tblList, "NO");
            addData2TableList(ref tblList, "YES");
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
            DataSet ds;
            if (Request.QueryString["tid"] != null)
            {
                ds = bd.IssueLC.ImportLCPaymentDetail(null, Convert.ToInt64(Request.QueryString["tid"]));
                if (ds == null || ds.Tables.Count <= 0)
                {
                    lblError.Text = "Can not found this transaction !";
                    return;
                }
                if (!String.IsNullOrEmpty(Request.QueryString["lst"]))
                    setToolbar(2);
            }
            else
            {
                txtCode.Text = Request.QueryString["lc"];
                if (string.IsNullOrEmpty(txtCode.Text)) return;
                ds = bd.IssueLC.ImportLCPaymentDetail(txtCode.Text, null);
                if (ds != null) lblError.Text = "This LC has a payment waiting for approve !";
            }            
            //Có payment chưa duyệt ?
            string DepositAccount = "", PaymentMethod = "", NostroAcct = "";
            DataTable tDetail;            
            if (ds == null || ds.Tables.Count <= 0)
            {
                tDetail = bd.IssueLC.GetDocForPayment(txtCode.Text);
                if (tDetail == null || tDetail.Rows.Count <= 0)
                {
                    lblError.Text = "Document for payment of this LC doesn't found !";
                    return;
                }
                //
                dr = tDetail.Rows[0];
                string Status = dr["Status"].ToString();
                if (!Status.Equals(bd.TransactionStatus.AUT))
                {
                    switch (Status)
                    {
                        case bd.TransactionStatus.UNA:
                            lblError.Text = "This Doc not authorize !";
                            break;
                        case bd.TransactionStatus.REV:
                            lblError.Text = "This Doc is reversed !";
                            break;
                        default:
                            lblError.Text = "This Doc is invalid status(" + Status + ") !";
                            break;
                    }
                    return;
                }
                //
                if (dr["RejectStatus"] != DBNull.Value)
                {
                    string RejectStatus = dr["RejectStatus"].ToString();
                    switch (RejectStatus)
                    {
                        case bd.TransactionStatus.UNA:
                            lblError.Text = "This Doc is waiting reject authorize !";
                            break;
                        case bd.TransactionStatus.AUT:
                            lblError.Text = "This Doc is rejected !";
                            break;
                        case bd.TransactionStatus.REV://Xử lý sao ? hỏi lại Nguyên
                            lblError.Text = "This Doc is reversed !";
                            break;
                        default:
                            lblError.Text = "This Doc is invalid status(" + Status + ") !";
                            break;
                    }

                    return;
                }
                //
                int PaymentFullFlag = Convert.ToInt32(dr["PaymentFullFlag"]);
                if (PaymentFullFlag != 0)
                {
                    lblError.Text = "This Doc is already payment completed !";
                    return;
                }
                setToolbar(1);
                //
                txtDrawingAmount.Value = Convert.ToDouble(dr["Amount"]);
                txtAmountCredited.Value = 0;
                txtFullyUtilised.Text = "NO";
                txtVatNo.Text = bd.IssueLC.GetVatNo();
            }
            else
            {
                bc.Commont.SetTatusFormControls(this.Controls, false);
                //setToolbar(2);
                tDetail = ds.Tables[0];
                dr = tDetail.Rows[0];
                txtCode.Text = dr["LCCode"].ToString();
                txtPaymentId.Value = dr["PaymentId"].ToString();
                txtDrawingAmount.Value = Convert.ToDouble(dr["DrawingAmount"]);
                if (dr["AmountCredited"] != DBNull.Value)
                    txtAmountCredited.Value = Convert.ToDouble(dr["AmountCredited"]);
                DepositAccount = dr["DepositAccount"].ToString();
                PaymentMethod = dr["PaymentMethod"].ToString();
                NostroAcct = dr["NostroAcct"].ToString();
                txtFullyUtilised.Text = dr["FullyUtilised"].ToString();
                cboWaiveCharges.SelectedValue = dr["WaiveCharges"].ToString();
                txtChargeRemarks.Text = dr["ChargeRemarks"].ToString();
                txtVatNo.Text = dr["VATNo"].ToString();
                //
                DataTable tCharge = ds.Tables[1];
                if (tCharge != null)
                {
                    DataRow drCharge;
                    DataRow[] drList = tCharge.Select("ChargeTab = 'tabCableCharge'");
                    if (drList.Length > 0)
                    {
                        drCharge = drList[0];
                        tabCableCharge_cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
                        tabCableCharge_cboChargeCcy.Text = drCharge["ChargeCcy"].ToString();
                        if (drCharge["ChargeAmt"] != DBNull.Value)
                            tabCableCharge_txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
                        tabCableCharge_cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
                        tabCableCharge_cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
                        tabCableCharge_txtTaxCode.Text = drCharge["TaxCode"].ToString();
                        if (drCharge["TaxAmt"] != DBNull.Value)
                            tabCableCharge_txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
                    }
                    drList = tCharge.Select("ChargeTab = 'tabPaymentCharge'");
                    if (drList.Length > 0)
                    {
                        drCharge = drList[0];
                        tabPaymentCharge_cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
                        tabPaymentCharge_cboChargeCcy.Text = drCharge["ChargeCcy"].ToString();
                        if (drCharge["ChargeAmt"] != DBNull.Value)
                            tabPaymentCharge_txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
                        tabPaymentCharge_cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
                        tabPaymentCharge_cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
                        tabPaymentCharge_txtTaxCode.Text = drCharge["TaxCode"].ToString();
                        if (drCharge["TaxAmt"] != DBNull.Value)
                            tabPaymentCharge_txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
                    }
                    drList = tCharge.Select("ChargeTab = 'tabHandlingCharge'");
                    if (drList.Length > 0)
                    {
                        drCharge = drList[0];
                        tabHandlingCharge_cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
                        tabHandlingCharge_cboChargeCcy.Text = drCharge["ChargeCcy"].ToString();
                        if (drCharge["ChargeAmt"] != DBNull.Value)
                            tabHandlingCharge_txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
                        tabHandlingCharge_cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
                        tabHandlingCharge_cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
                        tabHandlingCharge_txtTaxCode.Text = drCharge["TaxCode"].ToString();
                        if (drCharge["TaxAmt"] != DBNull.Value)
                            tabHandlingCharge_txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
                    }
                    drList = tCharge.Select("ChargeTab = 'tabDiscrepenciesCharge'");
                    if (drList.Length > 0)
                    {
                        drCharge = drList[0];
                        tabDiscrepenciesCharge_cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
                        tabDiscrepenciesCharge_cboChargeCcy.Text = drCharge["ChargeCcy"].ToString();
                        if (drCharge["ChargeAmt"] != DBNull.Value)
                            tabDiscrepenciesCharge_txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
                        tabDiscrepenciesCharge_cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
                        tabDiscrepenciesCharge_cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
                        tabDiscrepenciesCharge_txtTaxCode.Text = drCharge["TaxCode"].ToString();
                        if (drCharge["TaxAmt"] != DBNull.Value)
                            tabDiscrepenciesCharge_txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
                    }
                    drList = tCharge.Select("ChargeTab = 'tabOtherCharge'");
                    if (drList.Length > 0)
                    {
                        drCharge = drList[0];
                        tabOtherCharge_cboChargeCode.SelectedValue = drCharge["ChargeCode"].ToString();
                        tabOtherCharge_cboChargeCcy.Text = drCharge["ChargeCcy"].ToString();
                        if (drCharge["ChargeAmt"] != DBNull.Value)
                            tabOtherCharge_txtChargeAmt.Value = Convert.ToDouble(drCharge["ChargeAmt"]);
                        tabOtherCharge_cboPartyCharged.SelectedValue = drCharge["PartyCharged"].ToString();
                        tabOtherCharge_cboAmortCharge.SelectedValue = drCharge["AmortCharge"].ToString();
                        tabOtherCharge_txtTaxCode.Text = drCharge["TaxCode"].ToString();
                        if (drCharge["TaxAmt"] != DBNull.Value)
                            tabOtherCharge_txtTaxAmt.Value = Convert.ToDouble(drCharge["TaxAmt"]);
                    }
                }
            }            
            //
            txtDocId.Value = dr["DocId"].ToString();
            //
            cboDrawType.SelectedValue = dr["DrawType"].ToString();
            lblCurrency.Text = dr["Currency"].ToString();
            txtAmtDrFromAcct.Value = txtDrawingAmount.Value;
            txtValueDate.SelectedDate = Convert.ToDateTime(dr["BookingDate"]);
            //
            bc.Commont.initRadComboBox(ref cboDepositAccount, "AccountName", "DepositCode", bd.IssueLC.GetDepositAccount(dr["CustomerID"].ToString(), dr["Currency"].ToString()));
            cboDepositAccount.SelectedValue = DepositAccount;
            //
            bc.Commont.initRadComboBox(ref cboPaymentMethod, "Description", "Code", bd.IssueLC.PaymentMethod());
            cboPaymentMethod.SelectedValue = PaymentMethod;
            //
            bc.Commont.initRadComboBox(ref cboNostroAcct, "Description", "AccountNo", bd.SQLData.B_BSWIFTCODE_GetByCurrency(dr["Currency"].ToString()));
            cboNostroAcct.SelectedValue = NostroAcct;
        }        

        private void CommitData()
        {
            long paymentId = Convert.ToInt64(txtPaymentId.Value);
            DataTable tResult = bd.IssueLC.ImportLCPaymentUpdate(paymentId, Convert.ToInt64(txtDocId.Value), txtCode.Text, cboDrawType.SelectedValue, txtDrawingAmount.Value, lblCurrency.Text, cboDepositAccount.SelectedValue, txtExchangeRate.Value,
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
            bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabCableCharge", tabCableCharge_cboChargeCode.SelectedValue, tabCableCharge_txtChargeAcct.Text, tabCableCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabCableCharge_txtExchangeRate.Value, 
                tabCableCharge_txtChargeAmt.Value, tabCableCharge_cboPartyCharged.SelectedValue, tabCableCharge_cboAmortCharge.SelectedValue, tabCableCharge_cboChargeStatus.SelectedValue, tabCableCharge_txtTaxCode.Text, tabCableCharge_txtTaxAmt.Value);
            bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabPaymentCharge", tabPaymentCharge_cboChargeCode.SelectedValue, tabPaymentCharge_txtChargeAcct.Text, tabPaymentCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabPaymentCharge_txtExchangeRate.Value,
                tabPaymentCharge_txtChargeAmt.Value, tabPaymentCharge_cboPartyCharged.SelectedValue, tabPaymentCharge_cboAmortCharge.SelectedValue, tabPaymentCharge_cboChargeStatus.SelectedValue, tabPaymentCharge_txtTaxCode.Text, tabPaymentCharge_txtTaxAmt.Value);
            bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabHandlingCharge", tabHandlingCharge_cboChargeCode.SelectedValue, tabHandlingCharge_txtChargeAcct.Text, tabHandlingCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabHandlingCharge_txtExchangeRate.Value,
                tabHandlingCharge_txtChargeAmt.Value, tabHandlingCharge_cboPartyCharged.SelectedValue, tabHandlingCharge_cboAmortCharge.SelectedValue, tabHandlingCharge_cboChargeStatus.SelectedValue, tabHandlingCharge_txtTaxCode.Text, tabHandlingCharge_txtTaxAmt.Value);
            bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabDiscrepenciesCharge", tabDiscrepenciesCharge_cboChargeCode.SelectedValue, tabDiscrepenciesCharge_txtChargeAcct.Text, tabDiscrepenciesCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabDiscrepenciesCharge_txtExchangeRate.Value,
                tabDiscrepenciesCharge_txtChargeAmt.Value, tabDiscrepenciesCharge_cboPartyCharged.SelectedValue, tabDiscrepenciesCharge_cboAmortCharge.SelectedValue, tabDiscrepenciesCharge_cboChargeStatus.SelectedValue, tabDiscrepenciesCharge_txtTaxCode.Text, tabDiscrepenciesCharge_txtTaxAmt.Value);
            bd.IssueLC.ImportLCPaymentChargeUpdate(paymentId, "tabOtherCharge", tabOtherCharge_cboChargeCode.SelectedValue, tabOtherCharge_txtChargeAcct.Text, tabOtherCharge_cboChargeCcy.SelectedValue.Split('#')[0], tabOtherCharge_txtExchangeRate.Value,
                tabOtherCharge_txtChargeAmt.Value, tabOtherCharge_cboPartyCharged.SelectedValue, tabOtherCharge_cboAmortCharge.SelectedValue, tabOtherCharge_cboChargeStatus.SelectedValue, tabOtherCharge_txtTaxCode.Text, tabOtherCharge_txtTaxAmt.Value);
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
                //[9/10/2014 10:01:06 PM] Nguyen - Viet Victory: Neu Party Charge la: A hoac B thi Xuat phieu VAT (Charge Phi + 10%VAT)
                //[9/10/2014 10:01:27 PM] Nguyen - Viet Victory: Neu Party Charge la: AC hoac BC thi KHONG Xuat phieu VAT (Charge Phi)
                switch (cboPartyCharged.SelectedValue)
                {
                    case "A":
                    case "B":
                        txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value * VAT).Replace("$", "");
                        txtTaxCode.Text = "81      10% VAT on Charge";
                        break;
                    default:
                        //txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value).Replace("$", "");
                        break;
                }
                
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
            try
            {
                switch (reportType)
                {
                    case 1://PhieuXuatNgoaiBang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PhieuXuatNgoaiBang.doc");
                        reportSaveName = "PhieuXuatNgoaiBang";
                        reportData = bd.IssueLC.ImportLCPaymentReport(1, Convert.ToInt64(txtPaymentId.Value), this.UserInfo.Username);
                        break;
                    case 2://PhieuChuyenKhoan
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PhieuChuyenKhoan.doc");
                        reportSaveName = "PhieuChuyenKhoan";
                        reportData = bd.IssueLC.ImportLCPaymentReport(2, Convert.ToInt64(txtPaymentId.Value), this.UserInfo.Username);
                        break;
                    case 3://VAT B
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VATb.doc");
                        reportSaveName = "VATb";
                        reportData = bd.IssueLC.ImportLCPaymentReport(3, Convert.ToInt64(txtPaymentId.Value), this.UserInfo.Username);
                        break;
                }
                if (reportData != null)
                {
                    try
                    {
                        Aspose.Words.License license = new Aspose.Words.License();
                        license.SetLicense("Aspose.Words.lic");

                        //Open the template document
                        Aspose.Words.Document reportDoc = new Aspose.Words.Document(reportTemplate);

                        // Fill the fields in the document with user data.
                        reportData.Tables[0].TableName = "Table1";
                        reportDoc.MailMerge.ExecuteWithRegions(reportData);

                        // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
                        reportDoc.Save(reportSaveName + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc",
                                      Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
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
    }
}