using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;



namespace BankProject.Views.TellerApplication
{
    public partial class ChequePaymentStop : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                FirstLoad();
                if (Request.QueryString["AccountID"] != null)
                {
                    LoadCustomer_Info(Request.QueryString["AccountID"].ToString());
                }
            }

        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarButton = e.Item as RadToolBarButton;
            var commandName = toolbarButton.CommandName;
            switch (commandName)
            { 
                case "commit":
                    if (tbFromSerial.Value.Value < tbFromSeri_H.Value.Value || tbFromSerial.Value.Value > tbToSeri_H.Value.Value ||
                        tbToSerial.Value.Value < tbFromSeri_H.Value.Value || tbToSerial.Value.Value > tbToSeri_H.Value.Value ) 
                    {
                        ShowMsgBox("Cheque No does not exists, please check again, ChequeNo must be within scale " + tbFromSeri_H.Text + " - " + tbToSeri_H.Text); return;
                    }
                    if (tbToSerial.Value.Value < tbFromSerial.Value.Value)
                    {
                        ShowMsgBox("From Serial must be less than To Serial , please check again"); return;
                    }
                    TriTT.CHEQUE_STOP_Insert_Update(tbID.Text, "UNA", tbCustomerID.Text, tbCustomerName.Text, tbCurrency.Text, rcbReasonForStopping.SelectedValue
                        , rcbReasonForStopping.Text.Replace(rcbReasonForStopping.SelectedValue+" - ",""), tbFromSerial.Value.Value, tbToSerial.Value.Value,
                        tbNoOfLeave.Value.Value, rcbChequeType.SelectedValue, rcbChequeType.Text.Replace(rcbChequeType.SelectedValue + " - ", ""),
                        tbFromAmount.Value.HasValue ? tbFromAmount.Value.Value : 0, tbToAmount.Value.HasValue ? tbToAmount.Value.Value : 0, rcbWaiveCharges.SelectedValue
                        , rdpActivityDate.SelectedDate.HasValue ? rdpActivityDate.SelectedDate : null, UserInfo.Username.ToString());
                    Response.Redirect("Default.aspx?tabid=134");
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("ChequePaytmentStop_PL"));
                    break;
                case "authorize":
                    TriTT.CHEQUE_STOP_Update_Status(tbID.Text, "AUT");
                    Response.Redirect("Default.aspx?tabid=134");
                    break;
                case "reverse":
                    TriTT.CHEQUE_STOP_Update_Status(tbID.Text, "REV");
                    LoadToolBar(true);
                    break;
                case "search":
                    LoadCustomer_Info(tbID.Text.Trim());
                    break;
            }
        }
        #region
        protected void FirstLoad()
        {
            LoadChequeType();
            this.LoadToolBar(true);
            this.rdpActivityDate.SelectedDate = DateTime.Now;
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
        private void LoadCustomer_Info(string AccountID)
        {
            if (AccountID.Length == 12)
            {
                //BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                FirstLoad();
                tbID.Text = AccountID;
                DataSet ds1 = TriTT.CHEQUE_STOP_LoadAcct_FromChequeStopPayment(AccountID);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    lblNote.Text = "";
                    DataRow dr1 = ds1.Tables[0].Rows[0]; 
                    tbCustomerID.Text = dr1["CustomerID"].ToString();
                    tbCustomerName.Text = dr1["CustomerName"].ToString();
                    tbCurrency.Text = dr1["Currency"].ToString();
                    tbFromSeri_H.Text = dr1["ChequeNoStart"].ToString();
                    tbToSeri_H.Text = dr1["ChequeNoEnd"].ToString();
                    rcbReasonForStopping.SelectedValue = dr1["ReasonStopID"].ToString();
                    tbFromSerial.Text = dr1["FromChequeSerial"].ToString();
                    tbToSerial.Text = dr1["ToChequeSerial"].ToString();
                    tbNoOfLeave.Text = dr1["NoOfLeave"].ToString();
                    rcbChequeType.SelectedValue = dr1["ChequeType"].ToString();
                    tbFromAmount.Text = dr1["FromAmount"].ToString();
                    tbToAmount.Text = dr1["ToAmount"].ToString();
                    rcbWaiveCharges.SelectedValue = dr1["WaiveCharge"].ToString();
                    if (dr1["ActiveDate"].ToString() != "")
                    {
                        rdpActivityDate.SelectedDate = Convert.ToDateTime(dr1["ActiveDate"].ToString());
                    }
                    if (dr1["Status"].ToString() == "AUT")
                    {
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                        LoadToolBar_AllFalse();
                        return;
                    }
                    LoadToolBar(false);
                    return;
                }
                DataSet ds = TriTT.CHEQUE_STOP_LoadAcct_FromChequeIssues(AccountID);// tao moi stop payment neu co ton tai working Account
                if (ds.Tables[0].Rows.Count >0 )
                {

                    lblNote.Text = "";
                    DataRow dr = ds.Tables[0].Rows[0];
                    tbCustomerID.Text = dr["CustomerID"].ToString();
                    tbCustomerName.Text = dr["CustomerName"].ToString();
                    tbCurrency.Text = dr["Currency"].ToString();
                    tbFromSeri_H.Text = dr["ChequeNoStart"].ToString();
                    tbToSeri_H.Text = dr["ChequeNoEnd"].ToString();

                } else
                {
                    lblNote.Text = "Your Working Account does not exist, please check again !";
                    tbCustomerID.Text = tbCustomerName.Text = tbCurrency.Text = tbFromSerial.Text = tbToSerial.Text = tbFromSeri_H.Text = tbToSeri_H.Text =
                        tbNoOfLeave.Text = tbFromAmount.Text = tbToAmount.Text = "";
                }
            }
            else
            {
                ShowMsgBox("Account ID is incorrect, please check again !!"); return;
            }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadCustomer_Info(tbID.Text.Trim());
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
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
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
        #endregion
    }
}