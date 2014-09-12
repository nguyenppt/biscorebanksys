using DotNetNuke.Entities.Modules;
using System;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;


namespace BankProject.Views.TellerApplication
{
    public partial class ChequeTransferDrawnOnUs : PortalModuleBase
    {
        protected string B_MACODE()
        {
            return "TT";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FirstLoad();
                this.tbID.Text = TriTT.B_BMACODE_ID_3par_CHEQUE_TRANS(B_MACODE(), ".", "CHEQUE_TRANSFER");

                if (Request.QueryString["ID"] != null)
                {
                    LoadTransferID_Data(Request.QueryString["ID"].ToString());
                }
                else
                {
                    LoadToolBar(true);
                }
            }

        }


        protected void FirstLoad()
        {
            LoadChequeType();
            this.rcbDebitCurrency.Focus();
            rdpCreditValueDate.SelectedDate = rdpValueDate.SelectedDate = this.rdpExposureDate.SelectedDate = DateTime.Now;
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarButton = e.Item as RadToolBarButton;
            var CommandName = toolbarButton.CommandName;
            if (CommandName == "commit")
            {
                if (TriTT.B_CHEQUE_RETURN_check_cheque_in_used(rcbChequeType.SelectedValue, Convert.ToDouble(tbChequeNo.Value.Value)).Tables[0].Rows[0]["InUsed"].ToString() == "NO") // KIEM TRa xem chequeNo co duoc su dung chua
                {
                    if (TriTT.B_CHEQUE_RETURN_check_cheque_in_Returned(rcbChequeType.SelectedValue, Convert.ToDouble(tbChequeNo.Value.Value)).Tables[0].Rows.Count == 0)// check xem chequeNo co bi Returned chua ?
                    {
                        decimal dealrate = Convert.ToDecimal(tbDealRate.Value.Value);
                        if (rcbDebitCurrency.SelectedValue == rcbCreditCurrency.SelectedValue) { dealrate = 1; }
                        TriTT.BCHEQUE_TRANSFER_Insert_Update(tbID.Text, "UNA", tbCustomerID.Text, tbCustomerName.Text, rcbDebitCurrency.SelectedValue, rcbDebitAccount.SelectedValue
                            , rcbDebitAccount.Text, Convert.ToDouble(tbDebitAmountLCY.Text), Convert.ToDouble(tbOldCustBal.Text), Convert.ToDouble(tbNewCustBal.Text),
                            rcbChequeType.SelectedValue, rcbChequeType.Text.Replace(rcbChequeType.SelectedValue + " - ", ""), tbChequeNo.Value.Value, rdpValueDate.SelectedDate,
                            rcbCreditCurrency.SelectedValue, rcbCreditAccount.SelectedValue, rcbCreditAccount.Text, dealrate, rdpExposureDate.SelectedDate, Convert.ToDouble(tbAmtCreditForCust.Text),
                            rdpCreditValueDate.SelectedDate, rcbWaiveCharges.SelectedValue, tbNarrative.Text, tbBeneName.Text, tbAddress.Text, tbLegalID.Text, rdpIssDate.SelectedDate,
                            tbPlaceOfIss.Text, UserInfo.Username.ToString());
                        Response.Redirect("Default.aspx?tabid=133");
                    }
                    else { ShowMsgBox("ChequeNo was Returned back to the Bank, please choose another ChequeNo !"); return; }
                }
                else { ShowMsgBox("ChequeNo was used, please choose another ChequeNo !"); return; }

            }
            if (CommandName == "Preview")
            {
                Response.Redirect(EditUrl("ChequeTransferDrawnOnUs_PL"));
            }
            if (CommandName == "authorize")
            {
                TriTT.BCHEQUE_TRANSFER_Update_Status(tbID.Text, "AUT", rcbDebitAccount.SelectedValue, rcbDebitCurrency.SelectedValue, tbNewCustBal.Value.Value);
                Response.Redirect("Default.aspx?tabid=133");
            }
            if (CommandName == "reverse")
            {
                TriTT.BCHEQUE_TRANSFER_Update_Status(tbID.Text, "REV", rcbDebitAccount.SelectedValue, rcbDebitCurrency.SelectedValue, tbNewCustBal.Value.Value);
                LoadToolBar(true);
            }
        }

        #region Properties
        protected void LoadTransferID_Data(string ID)
        {
            LoadChequeType();
            DataRow dr = TriTT.BCHEQUE_TRANSFER_LoadTransferID_Data(ID);
            tbID.Text = dr["ID"].ToString();
            tbCustomerID.Text = dr["CustomerID"].ToString();
            tbCustomerName.Text = dr["CustomerName"].ToString();
            rcbDebitCurrency.SelectedValue = dr["DebitCurrency"].ToString();
            rcbDebitCurrency_SelectedIndexChanged(null  ,null);
            rcbDebitAccount.SelectedValue = dr["DebitAcctCode"].ToString();
            tbDebitAmountLCY.Text = dr["DebitAmount"].ToString();
            tbChequeNo.Text = dr["ChequeNo"].ToString();
            rcbChequeType.SelectedValue = dr["ChequeType"].ToString();
            tbOldCustBal.Text = dr["OldCustBalance"].ToString();
            tbNewCustBal.Text = dr["NewCustBalance"].ToString();
            if (dr["DebitValueDate"].ToString() != "")
            {
                rdpValueDate.SelectedDate = Convert.ToDateTime(dr["DebitValueDate"].ToString());
            }
            rcbCreditCurrency.SelectedValue = dr["CreditCurrency"].ToString();
            rcbCreditCurrency_SelectedIndexChanged(null, null);
            rcbCreditAccount.SelectedValue = dr["CreditAcctCode"].ToString();
            tbDealRate.Text = dr["DealRate"].ToString();
            if (dr["ExposureDate"].ToString() != "")
            {
                rdpExposureDate.SelectedDate = Convert.ToDateTime(dr["ExposureDate"].ToString());
            }
            if (dr["CreditValueDate"].ToString() != "")
            {
                rdpCreditValueDate.SelectedDate = Convert.ToDateTime(dr["CreditValueDate"].ToString());
            }
            tbAmtCreditForCust.Text = dr["AmtCreditForCust"].ToString();
            rcbWaiveCharges.SelectedValue = dr["WaiveCharge"].ToString();
            tbNarrative.Text = dr["Narrative"].ToString();
            tbBeneName.Text = dr["BeneficialName"].ToString();
            tbAddress.Text = dr["Address"].ToString();
            tbLegalID.Text = dr["LegalID"].ToString();
            tbPlaceOfIss.Text = dr["PlaceOfIssue"].ToString();
            if (dr["IssuedDate"].ToString() != "")
            {
                rdpIssDate.SelectedDate = Convert.ToDateTime(dr["IssuedDate"].ToString());
            }
            if (dr["Status"].ToString() == "AUT")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
                return;
            }
            LoadToolBar(false);
        }
        protected void rcbDebitCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbDebitAccount.Items.Clear();
            rcbDebitAccount.AppendDataBoundItems = true;
            rcbDebitAccount.Items.Add(new RadComboBoxItem("", ""));
            rcbDebitAccount.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadCustomerAcct(rcbDebitCurrency.SelectedValue);
            rcbDebitAccount.DataValueField = "AccountCode";
            rcbDebitAccount.DataTextField = "AcctHasName";
            rcbDebitAccount.DataBind();
        }
        protected void LoadChequeType()
        {
            rcbChequeType.Items.Clear();
            rcbChequeType.Items.Add(new RadComboBoxItem("", ""));
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
        protected void rcbCreditCurrency_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCreditAccount.Items.Clear();
            rcbCreditAccount.Items.Add(new RadComboBoxItem("", ""));
            rcbCreditAccount.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadAcctPaid(rcbCreditCurrency.SelectedValue);
            rcbCreditAccount.DataTextField = "AcctHasName";
            rcbCreditAccount.DataValueField = "AccountCode";
            rcbCreditAccount.DataBind();
        }
        protected void rcbDebitAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView Data = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = Data["CustomerID"].ToString();
            e.Item.Attributes["CustomerName"] = Data["CustomerName"].ToString();
            e.Item.Attributes["Address"] = Data["Address"].ToString();
            e.Item.Attributes["WorkingAmount"] = Data["WorkingAmount"].ToString();
            e.Item.Attributes["DocID"] = Data["DocID"].ToString();
            e.Item.Attributes["DocIssuePlace"] = Data["DocIssuePlace"].ToString();
            e.Item.Attributes["DocIssueDate"] = Data["DocIssueDate"].ToString();
            e.Item.Attributes["ChequeNoStart"] = Data["ChequeNoStart"].ToString();
            e.Item.Attributes["ChequeNoEnd"] = Data["ChequeNoEnd"].ToString();
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        #endregion
        //void LoadCreidtAccount()
        //{
        //    rcbCreditAccount.Items.Clear();
        //    if (rcbDebitAccount.SelectedValue != "" && rcbCreditCurrency.SelectedValue != "")
        //    {
        //        DataSet ds = Database.B_BCRFROMACCOUNT_OtherCustomer(rcbDebitAccount.SelectedItem.Attributes["Name"].ToString(), rcbCreditCurrency.SelectedValue);
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow dr = ds.Tables[0].NewRow();
        //            dr["Display"] = "";
        //            dr["Id"] = "";
        //            dr["CustomerID"] = "";
        //            dr["Name"] = "";
        //            ds.Tables[0].Rows.InsertAt(dr, 0);

        //            rcbCreditAccount.DataTextField = "Display";
        //            rcbCreditAccount.DataValueField = "Id";
        //            rcbCreditAccount.DataSource = ds;
        //            rcbCreditAccount.DataBind();
        //        }
        //    }
        //}
        //protected void rcbDebitAccount_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    LoadCreidtAccount();
        //}
    }
}