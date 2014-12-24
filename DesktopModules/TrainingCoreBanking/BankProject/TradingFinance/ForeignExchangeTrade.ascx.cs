using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using System.Data;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.Common;
using BankProject.DBRespository;
using System.Linq;

namespace BankProject.Views.TellerApplication
{
    public partial class ForeignExchangeTrade : PortalModuleBase
    {
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;            
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            dvAudit.Visible = false;
            txtFTNo.Enabled = true;
            SetDefault(false);
            
            bc.Commont.initRadComboBox(ref rcbCounterparty, "CustomerName", "CustomerID", bd.DataTam.B_BCUSTOMERS_GetAll());

            LoadAccountOfficer(null);

            bc.Commont.initRadComboBox(ref rcbBuyCurrency, "Code", "Code", bd.SQLData.B_BCURRENCY_GetAll());
                        
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                txtId.Text = Request.QueryString["tid"];
                LoadData();                
            }
            else
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
            }
        }

        private void SetDefault(bool isauthorize)
        {
            txtDealDate.SelectedDate = DateTime.Today;
            txtValueDate.SelectedDate = DateTime.Today;
            GeneralCode();
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    if (CheckFTNo() == false)
                    {
                        string radalertscript = "<script language='javascript'>function f(){radalert('TF No. is not found', 400, 150 , 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                        return;
                    }

                    if (CheckCustomerPayingAC() == false)
                    {
                        string radalertscript = "<script language='javascript'>function f(){radalert('Debit Account is not found.', 400, 150, 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                        return;
                    }

                    if (CheckCustomerReceivingAC() == false)
                    {
                        string radalertscript = "<script language='javascript'>function f(){radalert('Credit Account is not found.', 400, 150, 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                        return;
                    }


                    double BuyAmount = 0;
                    if (txtBuyAmount.Value > 0)
                    {
                        BuyAmount = Double.Parse(txtBuyAmount.Value.ToString());
                    }
                    double SellAmount = 0;
                    if (txtSellAmount.Value > 0)
                    {
                        SellAmount = Double.Parse(txtSellAmount.Value.ToString());
                    }
                    double rate = 0;
                    if (txtRate.Value > 0)
                    {
                        rate = Double.Parse(txtRate.Value.ToString());
                    }

                    bd.SQLData.B_BFOREIGNEXCHANGE_Insert(txtId.Text.Trim()
                        , rcbTransactionType.SelectedValue
                        , txtFTNo.Text.Trim()
                        , rcbDealType.SelectedValue
                        , rcbCounterparty.SelectedValue
                        , txtDealDate.SelectedDate.ToString()
                        , txtValueDate.SelectedDate.ToString()
                        , rcbExchangeType.SelectedValue
                        , rcbBuyCurrency.SelectedValue
                        , BuyAmount
                        , rcbSellCurrency.SelectedValue
                        , SellAmount
                        , rate
                        , txtCustomerReceivingAC.Text.Trim()
                        , txtCustomerPayingAC.Text.Trim()
                        , rcbAccountOfficer.SelectedValue
                        , UserId
                        , txtComment1.Text
                        , txtComment2.Text
                        , txtComment3.Text);

                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    SetDefault(false);
                    break;

                case bc.Commands.Preview:
                    Response.Redirect(EditUrl("chitiet"));
                    break;
                case bc.Commands.Authorize:
                case bc.Commands.Reverse:
                    int provitionTransferId = bd.SQLData.B_BFOREIGNEXCHANGE_ValidationLCNoExst_PROVISIONTRANSFER_DC(txtFTNo.Text.Trim());

                    if (txtFTNo.Text != "" && provitionTransferId <= 0)
                    {
                        ShowMsgBox("There is no Provision Transfer information. Please process function Provision Transfer");
                        return;
                    }
                    // Update Status
                    bd.SQLData.B_BFOREIGNEXCHANGE_UpdateStatus(commandName, txtId.Text.Trim(), UserId);

                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    SetDefault(true);
                    LoadToolBar(false);
                    break;
            }
        }

        void LoadData()
        {
            LoadToolBar(false);
            var dsF = bd.SQLData.B_BFOREIGNEXCHANGE_GetByCode(txtId.Text.Trim());
            if (dsF == null || dsF.Tables.Count <= 0 || dsF.Tables[0].Rows.Count <= 0)
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                txtBuyAmount.Value = 0;
                txtSellAmount.Value = 0;
                txtRate.Value = 0;
                return;
            }
            //
            var drow = dsF.Tables[0].Rows[0];

            rcbTransactionType.SelectedValue = drow["TransactionType"].ToString();
            SetRelation_TransactionType();

            txtFTNo.Text = drow["FTNo"].ToString();
            rcbDealType.SelectedValue = drow["DealType"].ToString();
            rcbCounterparty.SelectedValue = drow["Counterparty"].ToString();

            if (drow["DealDate"].ToString().IndexOf("1/1/1900") == -1)
            {
                txtValueDate.SelectedDate = DateTime.Parse(drow["DealDate"].ToString());
            }
            if (drow["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
            {
                txtDealDate.SelectedDate = DateTime.Parse(drow["ValueDate"].ToString());
            }

            rcbExchangeType.SelectedValue = drow["ExchangeType"].ToString();
            rcbBuyCurrency.SelectedValue = drow["BuyCurrency"].ToString();
            txtBuyAmount.Value = double.Parse(drow["BuyAmount"].ToString());
            rcbSellCurrency.SelectedValue = drow["SellCurrency"].ToString();
            txtSellAmount.Value = double.Parse(drow["SellAmount"].ToString());
            txtRate.Value = double.Parse(drow["Rate"].ToString());

            txtCustomerReceivingAC.Text = drow["CustomerReceiving"].ToString();
            txtCustomerPayingAC.Text = drow["CustomerPaying"].ToString();
            rcbAccountOfficer.SelectedValue = drow["AccountOfficer"].ToString();

            txtComment1.Text = drow["Comment1"].ToString();
            txtComment2.Text = drow["Comment2"].ToString();
            txtComment3.Text = drow["Comment3"].ToString();

            string Status = drow["Status"].ToString();
            switch (Status)
            {
                case bd.TransactionStatus.UNA:
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                    {
                        //4appr
                        LoadToolBar(true);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    }
                    else BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    break;
                default:
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    break;
            }
            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
        }

        private void LoadAccountOfficer(string selectedid)
        {
            AccountOfficerRepository facade = new AccountOfficerRepository();
            var src = facade.GetAll().ToList();
            Util.LoadData2RadCombo(rcbAccountOfficer, src, "Code", "description", "-Select Account Officer-", false);

            if (!String.IsNullOrEmpty(selectedid))
            {
                rcbAccountOfficer.SelectedValue = selectedid;
            }
        }

        protected void txtCustomerReceivingAC_OnTextChanged(object sender, EventArgs e)
        {
            txtCustomerReceivingAC_Change("text_chage");
        }

        protected void txtCustomerReceivingAC_Change(string CallFrom)
        {
            var customerName = "";
            var code = "";
            if (rcbCounterparty.SelectedItem != null)
            {
                customerName = rcbCounterparty.SelectedItem.Attributes["CustomerName"];
            }

            code = CallFrom == "sellcurrency_change" ? rcbCounterparty.SelectedValue : txtCustomerReceivingAC.Text;

            lblCustomerReceivingACError.Text = "";
            var dtCusRe = bd.SQLData.B_BFOREIGNEXCHANGE_GetByCreditAccount(code, rcbSellCurrency.SelectedValue, customerName, CallFrom, rcbTransactionType.SelectedValue);
            if (dtCusRe.Rows.Count <= 0)
            {
                lblCustomerReceivingACError.Text = "Not found!";
                txtCustomerReceivingAC.Text = "";
            }
            else
            {
                txtCustomerReceivingAC.Text = dtCusRe.Rows[0]["DepositCode"].ToString();
            }
        }

        protected bool CheckCustomerReceivingAC()
        {
            if (txtCustomerReceivingAC.Text.Trim().Length <= 0)
            {
                return true;
            }
            var dtCusRec = bd.SQLData.B_BFOREIGNEXCHANGE_GetByCreditAccount(txtCustomerReceivingAC.Text, rcbSellCurrency.SelectedValue, "", "text_chage", rcbTransactionType.SelectedValue);
            return dtCusRec.Rows.Count > 0;
        }

        protected bool CheckCustomerPayingAC()
        {
            if (txtCustomerPayingAC.Text.Trim().Length <= 0)
            {
                return true;
            }
            //var dtCuspay = SQLData.B_BFOREIGNEXCHANGE_CheckCustomerPayingAC(txtCustomerPayingAC.Text.Trim(),rcbSellCurrency.SelectedValue,rcbCounterparty.SelectedItem.Attributes["CustomerName"]);
            var dtCuspay = bd.SQLData.B_BFOREIGNEXCHANGE_GetByDebitAccount(txtCustomerPayingAC.Text, rcbBuyCurrency.SelectedValue, "", "text_chage");
            return dtCuspay.Rows.Count > 0;
        }

        protected bool CheckFTNo()
        {
            if (txtFTNo.Text.Trim().Length <= 0)
            {
                return true;
            }
            var dtFt = bd.SQLData.B_BFOREIGNEXCHANGE_CheckFTNo(txtFTNo.Text.Trim(), rcbTransactionType.SelectedValue);
            return dtFt.Rows.Count > 0;
            
        }

        protected void txtCustomerPayingAC_OnTextChanged(object sender, EventArgs e)
        {
            txtCustomerPayingAC_Change("text_chage");
        }

        protected void txtCustomerPayingAC_Change(string CallFrom)
        {
            var customerName = "";
            if (rcbCounterparty.SelectedItem != null)
            {
                customerName = rcbCounterparty.SelectedItem.Attributes["CustomerName"];
            }
            lblCustomerPayingACError.Text = "";
            var dtCusRe = bd.SQLData.B_BFOREIGNEXCHANGE_GetByDebitAccount(txtCustomerPayingAC.Text, rcbBuyCurrency.SelectedValue, customerName, CallFrom);
            if (dtCusRe.Rows.Count <= 0)
            {
                lblCustomerPayingACError.Text = "Not found!";
                txtCustomerPayingAC.Text = "";
            }
            else
            {
                txtCustomerPayingAC.Text = dtCusRe.Rows[0]["Id"].ToString();
            }

        }

        protected void rcbTransactionType_OnTextChanged(object sender, EventArgs e)
        {
            SetRelation_TransactionType();
        }

        protected void SetRelation_TransactionType()
        {
            switch (rcbTransactionType.SelectedValue)
            {
                case "TT":
                    txtFTNo.Enabled = false;
                    lblFTNoError.Text = "";

                    SetSourceToSellCurrency(bd.SQLData.B_BCURRENCY_GetAll().Tables[0]);
                    
                    break;
                case "LC":
                case "DP/DA":
                    txtFTNo.Enabled = true;

                    SetSourceToSellCurrency(createSellCurrencySource());
                    break;
            }
        }

        protected void txtFTNo_OnTextChanged(object sender, EventArgs e)
        {
            lblFTNoError.Text = "";
            var dsCusRe = bd.SQLData.B_PROVISIONTRANSFER_DC_GetByLCNo(txtFTNo.Text.Trim(), rcbTransactionType.SelectedValue);
            if (dsCusRe == null || dsCusRe.Tables[0].Rows.Count <= 0)
            {
                lblFTNoError.Text = "Not found!";
                rcbCounterparty.SelectedValue = string.Empty;
                //txtCustomerReceivingAC.Text = string.Empty;
                //txtCustomerPayingAC.Text = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(dsCusRe.Tables[0].Rows[0]["Cancel_Status"].ToString()))
                {
                    ShowMsgBox("This Documentary is canceled");
                    return;
                }
                rcbCounterparty.SelectedValue = dsCusRe.Tables[0].Rows[0]["CounterpartyId"].ToString();
               // txtCustomerReceivingAC.Text = dsCusRe.Tables[0].Rows[0][""].ToString();
               // txtCustomerPayingAC.Text = dsCusRe.Tables[0].Rows[0][""].ToString();
            }
        }

        protected void rcbCounterparty_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void GeneralCode()
        {
            txtId.Text = bd.SQLData.B_BMACODE_GetNewID("FOREIGNEXCHANGE", "FX");
        }

        protected void rcbSellCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCustomerReceivingACError.Text = "";
            txtCustomerReceivingAC_Change("sellcurrency_change");
        }

        protected void rcbBuyCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCustomerPayingACError.Text = "";
            txtCustomerPayingAC_Change("bullcurrency_change");
        }

        protected void rcbCounterparty_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCustomerReceivingACError.Text = "";
            txtCustomerReceivingAC_Change("sellcurrency_change");
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
  
        protected void SetSourceToSellCurrency (DataTable dtSource) 
        {
            bc.Commont.initRadComboBox(ref rcbSellCurrency, "Code", "Code", dtSource);
        }

        protected DataTable createSellCurrencySource ()
        {
            var dtSource = new DataTable();
            dtSource.Columns.Add("Code", typeof(string));
            dtSource.Columns.Add("Description", typeof(string));

            var drow = dtSource.NewRow();
            drow["Code"] = "USD";
            drow["Description"] = "USD";
            dtSource.Rows.Add(drow);

            drow = dtSource.NewRow();
            drow["Code"] = "EUR";
            drow["Description"] = "EUR";
            dtSource.Rows.Add(drow);

            drow = dtSource.NewRow();
            drow["Code"] = "VND";
            drow["Description"] = "VND";
            dtSource.Rows.Add(drow);

            return dtSource;
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            bc.Reports.createFileDownload(Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/FX_VAT.doc"),
                bd.SQLData.B_BFOREIGNEXCHANGE_Report(txtId.Text, UserInfo.Username), "FX_VAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);
        }
    }
}