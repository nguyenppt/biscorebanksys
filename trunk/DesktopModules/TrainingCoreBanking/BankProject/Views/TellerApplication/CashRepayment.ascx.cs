using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class CashRepayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            else
            {
                tbID.Text = TriTT.B_BMACODE_NewID_3par_CashRepayment("CASH_REPAYMENT", "TT");
                this.rcbCurrency.Focus();
                tbTellerID.Text = UserInfo.Username;
                LoadToolBar(false);

                if (Request.QueryString["PLCode"] != null)
                {
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    LoadDataPreview();
                }
                else LoadToolBar(false);
            }
        }
        protected void LoadToolBar(bool flag)
        {
            RadToolBar.FindItemByValue("btCommitData").Enabled = !flag;
            RadToolBar.FindItemByValue("btPreview").Enabled = !flag;
            RadToolBar.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar.FindItemByValue("btReverse").Enabled = flag;
            RadToolBar.FindItemByValue("btSearch").Enabled = false;
            RadToolBar.FindItemByValue("btPrint").Enabled = false;
        }
        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            switch (commandName)
            { 
                case "commit":
                    DefaultSetting();
                    break;
                case "Preview":
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    Response.Redirect(EditUrl("CashRepayment_PL"));
                    break;
                case "authorize":
                case "reverse":
                    DefaultSetting();
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    break;
            }
        }
        protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadLOANACCOUNT("");
        }

        void loadLOANACCOUNT(string customername)
        {
            DataSet ds = BankProject.DataProvider.TriTT.B_BLOANACCOUNT_getbyCurrency(customername, rcbCurrency.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rcbCustAccount.Items.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["DisplayHasCurrency"] = "";
                dr["AccountID"] = "";
                dr["CustomerID"] = "";
                dr["CustomerName"] = "";  //CustomerName
                ds.Tables[0].Rows.InsertAt(dr, 0);
                rcbCustAccount.DataTextField = "DisplayHasCurrency";
                rcbCustAccount.DataValueField = "AccountID";
                rcbCustAccount.DataSource = ds;
                rcbCustAccount.DataBind();
            }
        }
        // gan cac thuoc tinh Name, ID cho cac khach hang khi duoc chon , su kien xay ra khi co du lieu do vao combobox
        protected void rcbCustAccount_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName"].ToString();  //CustomerName
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        }

        protected void rcbCurrencyDeposited_rcbCurrencyDeposited(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadCashAccount("");
        }

        void loadCashAccount(string customername)
        {
            DataSet ds = BankProject.DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer(customername, rcbCurrencyDeposited.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rcbCashAccount.Items.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["DisplayHasCurrency"] = "";
                dr["ID"] = "";
                dr["CustomerID"] = "";
                dr["Name"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                rcbCashAccount.DataTextField = "DisplayHasCurrency";
                rcbCashAccount.DataValueField = "ID";
                rcbCashAccount.DataSource = ds;
                rcbCashAccount.DataBind();
            }
        }
        void LoadDataPreview()
        {
            string PLCode = Request.QueryString["PLCode"].ToString();
            switch (PLCode)
            { 
                case "0":
                    LoadDetail("TT/14205/00079", "1100001", "Phan Van Han", 9000000, "VND - 080002595928 - Phan Van Han", "VND - 060002595928 - Phan Van Han");
                    break;
                case "1":
                    LoadDetail("TT/14205/00078", "1100002", "Dinh Tien Hoang", 10000000, "VND - 080002595838 - Dinh Tien Hoang", "VND - 060002595838 - Dinh Tien Hoang");
                    break;
                case "2":
                    LoadDetail("TT/14205/00077", "1100003", "Pham Ngoc Thach", 20000000, "VND - 080002596268 - Pham Ngoc Thach", "VND - 060002596268 - Pham Ngoc Thach");
                    break;
                case "3":
                    LoadDetail("TT/14205/00076", "1100004", "Vo Thi Sau", 30000000, "VND - 080002596358 - Vo Thi Sau", "VND - 060002596358 - Vo Thi Sau");
                    break;
                case "4":
                    LoadDetail("TT/14205/00075", "1100005", "Truong Cong Dinh", 40000000, "VND - 080002596448 - Truong Cong Dinh", "VND - 060002596448 - Truong Cong Dinh");
                    break;
                case "5":
                    LoadDetail("TT/14205/00074", "2102925", "CTY TNHH SONG HONG", 50000000, "VND - 080002595948 - CTY TNHH SONG HONG", "VND - 060002595948 - CTY TNHH SONG HONG");
                    break;
                case "6":
                    LoadDetail("TT/14205/00073", "2102926", "CTY TNHH PHAT TRIEN PHAN MEM ABC", 60000000, "VND - 080002596538 - CTY TNHH PHAT TRIEN PHAN MEM ABC", "VND - 060002596538 - CTY TNHH PHAT TRIEN PHAN MEM ABC");
                    break;
                case "7":
                    LoadDetail("TT/14205/00072", "2102927", "Travelocity Corp.", 70000000, "VND - 080002595968 - Travelocity Corp.", "VND - 060002595968 - Travelocity Corp.");
                    break;
                case "8":
                    LoadDetail("TT/14205/00071", "2102928", "Wall Street Corp.", 80000000, "VND - 080002596628 - Wall Street Corp.", "VND - 060002596628 - Wall Street Corp.");
                    break;
            }

        }

        void LoadDetail(string ID,string CustomerID, string CustomerName, int AmtDeposit,string CustAccount,string CashAccount)
        { 
            tbID.Text =ID;
            lblCustomerID.Text=CustomerID;
            lblCustomerName.Text=CustomerName;
            lblAmtpaidToCust.Text=lblNewCustBalance.Text = tbAmtLCYDeposited.Text=AmtDeposit.ToString();
            rcbCurrency.SelectedValue = rcbCurrencyDeposited.SelectedValue="VND";
            loadLOANACCOUNT(CustomerName);
            loadCashAccount(CustomerName);
            rcbCustAccount.SelectedIndex = 1;
            rcbCashAccount.SelectedIndex = 1;
            //rcbCustAccount.Text = CustAccount;
            //rcbCashAccount.Text = CashAccount;
            rcbWaiveCharge.SelectedValue="YES";
            tbNarrative.Text="Nop tien mat thanh toan giam von truoc han";
        }

        void DefaultSetting()
        {
            rcbCurrency.Focus();
            tbID.Text = TriTT.B_BMACODE_NewID_3par_CashRepayment("CASH_REPAYMENT", "TT");
            this.rcbCustAccount.Focus();
            tbTellerID.Text = UserInfo.Username;
            lblCustomerID.Text = "";
            lblCustomerName.Text = "";
            rcbCashAccount.Text=rcbCustAccount.Text=""; 
            rcbCurrency.SelectedValue=rcbCurrencyDeposited.SelectedValue=rcbWaiveCharge.SelectedValue ="";
            rcbCustAccount.SelectedValue = rcbCashAccount.SelectedValue = "";
            lblAmtpaidToCust.Text = lblNewCustBalance.Text = tbAmtLCYDeposited.Text= tbDealRate.Text= tbNarrative.Text=tbPrint.Text="";
        }

        protected void LoadCustomerAccount()
        {
            rcbCustAccount.Items.Clear();
            if (rcbCurrency.SelectedValue != null && rcbCustAccount.SelectedValue != null)
            {
                DataSet ds = Database.B_BCRFROMACCOUNT_OtherCustomer(rcbCustAccount.SelectedItem.Attributes["Name"].ToString(), rcbCurrency.SelectedValue);
                if (ds != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["Display"] = "";
                    dr["ID"] = "";
                    dr["CustomerID"] = "";
                    dr["Name"] = "";
                    ds.Tables[0].Rows.InsertAt(dr, 0);
                    rcbCustAccount.DataTextField = "Display";
                    rcbCustAccount.DataValueField = "ID";
                    rcbCustAccount.DataSource = ds;
                    rcbCustAccount.DataBind();
                }
            }
        }

        protected void rcbCustAccount_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //LoadCustomerAccount();
        }
    }
}