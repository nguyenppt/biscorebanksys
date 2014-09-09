using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class PastDueLoanRepayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            else
            {
                if (Request.QueryString["CustomerID"] != null)
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    LoadPreview_toCommit();
                }
                else
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                }
                if (Request.QueryString["LoanContractReference"] != null) //&& Request.QueryString["CustomerID"] != null
                {
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    LoadPreview_toAuthorize();
                }
                //tbID.Text = TriTT.B_BMACODE_NewID_3par_PastDueLoanReference("PAST_DUE_LOAN_REFER_ID", "PDLD");
            }
        }
        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var RadToolBar = e.Item as RadToolBarButton;
            var CommandName = RadToolBar.CommandName;
            switch (CommandName)
            { 
                case "commit":
                    DefaultSetting();
                    LoadToolBar(false);
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("PastDueLoanRepayment_PL"));
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    break;
                case "authorize":
                case "reverse":
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    DefaultSetting();
                    LoadToolBar(false);
                    break;
            }
        }

        protected void LoadPreview_toAuthorize()
        {
            var LoanContractReference = Request.QueryString["LoanContractReference"].ToString();
            switch (LoanContractReference)
            {
                case "LD142000035":
                    LoadDetail("PDLD/14200/0035", "2102928", "Wall Street Corp.", "7/15/2014", "100000000", "060002596628");
                    break;
                case "LD142010036":
                    LoadDetail("PDLD/14201/0036", "2102926", "CTY TNHH PHAT TRIEN PHAN MEM ABC", "7/16/2014", "200000000", "060002596538");
                    break;
                case "LD142020037":
                    LoadDetail("PDLD/14202/0037", "1100005", "Truong Cong Dinh", "7/17/2014", "300000000", "060002596448");
                    break;
                case "LD142030039":
                    LoadDetail("PDLD/14203/0039", "1100004", "Vo Thi Sau", "7/20/2014", "400000000", "060002596358");
                    break;
                case "LD142040040":
                    LoadDetail("PDLD/14204/0040", "1100003", "Pham Ngoc Thach", "7/21/2014", "500000000", "060002596268");
                    break;
                case "LD142050041":
                    LoadDetail("PDLD/14205/0041", "2102927", "Travelocity Corp.", "9/15/2014", "600000000", "060002595968");
                    break;
                case "LD142060042":
                    LoadDetail("PDLD/14206/0042", "2102925", "CTY TNHH SONG HONG", "10/15/2014", "700000000", "060002595948");
                    break;
                case "LD142070043":
                    LoadDetail("PDLD/14207/0043", "1100001", "Phan Van Han", "12/15/2014", "900000000", "060002595928");
                    break;
                case "LD142090045":
                    LoadDetail("PDLD/14209/0045", "1100002", "Dinh Tien Hoang", "11/19/2014","1000000000", "060002595838");
                    break;

            }
        }
       
        protected void LoadPreview_toCommit()
        {
            var CustomerId = Request.QueryString["CustomerID"].ToString();
            switch (CustomerId)
            {
                case "2102928":
                    LoadDetail("PDLD/14200/0035", "2102928", "Wall Street Corp.", "", "","");
                    break;
                case "2102926":
                    LoadDetail("PDLD/14201/0036", "2102926", "CTY TNHH PHAT TRIEN PHAN MEM ABC", "", "","");
                    break;
                case "1100005":
                    LoadDetail("PDLD/14202/0037", "1100005", "Truong Cong Dinh", "", "","");
                    break;
                case "1100004":
                    LoadDetail("PDLD/14203/0039", "1100004", "Vo Thi Sau", "", "","");
                    break;
                case "1100003":
                    LoadDetail("PDLD/14204/0040", "1100003", "Pham Ngoc Thach", "", "","");
                    break;
                case "2102927":
                    LoadDetail("PDLD/14205/0041", "2102927", "Travelocity Corp.", "", "","");
                    break;
                case "2102925":
                    LoadDetail("PDLD/14206/0042", "2102925", "CTY TNHH SONG HONG", "", "","");
                    break;
                case "1100001":
                    LoadDetail("PDLD/14207/0043", "1100001", "Phan Van Han", "", "","");
                    break;
                case "1100002":
                    LoadDetail("PDLD/14209/0045", "1100002", "Dinh Tien Hoang", "", "","");
                    break;

            }
        }

        void LoadccountToDebit()
        {
            DataSet ds = Database.B_BDRFROMACCOUNT_GetByCustomer("", rcbCurrency.SelectedValue);
            if (ds.Tables.Count > 0 && ds != null && ds.Tables[0].Rows.Count > 0)
            {
                rcbAccountToDebit.Items.Clear();
                DataRow dr = ds.Tables[0].NewRow();
                dr["DisplayHasCurrency"] = "";
                dr["ID"] = "";
                dr["CustomerID"] = "";
                dr["Name"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
                rcbAccountToDebit.DataTextField = "DisplayHasCurrency";
                rcbAccountToDebit.DataValueField = "ID";  /// ma tai khoan depost acccount
                rcbAccountToDebit.DataSource = ds;
                rcbAccountToDebit.DataBind();
            }
        }
        protected void rcbCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadccountToDebit();
        }
        protected void LoadDetail(string ID, string CustomerID, string CustomerName, string RepayDate, string RepayAmt,string AccountToDebit)
        { 
            tbID.Text=ID;
            lblCustomreID.Text=CustomerID;
            lblCustomreName.Text=CustomerName;
            rcbCurrency.SelectedValue="";
            if (RepayDate == "") // repay date la flag de su dung function cho 2 loai Preview_toCommit va priview_toAuthorize
                rdpRepayDate.SelectedDate = DateTime.Now;
            else
            {
                rcbCurrency.SelectedValue = "VND";
                LoadccountToDebit();
                rcbAccountToDebit.SelectedValue = AccountToDebit;
                rdpRepayDate.SelectedDate = Convert.ToDateTime(RepayDate);
                tbAmount.Text = "160000000";
            }
            tbRepayAmt.Text = RepayAmt;

        }

        protected void LoadToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !flag;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !flag;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = flag;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void DefaultSetting()
        {
            tbID.Text = TriTT.B_BMACODE_NewID_3par_PastDueLoanReference("PAST_DUE_LOAN_REFER_ID", "PDLD");
            lblCustomreID.Text = lblCustomreName.Text=lblMainCategory_Amt.Text=lblMainCategory_Amt_Caption.Text = "";
            lblSubCategory_Amt.Text = lblSubCategory__Amt_Caption.Text =lblPurposeCode.Text=lblPurposeCode_Caption.Text= "";
            rcbCurrency.SelectedValue = ""; 
            lblPenaltyRate.Text = lblPenaltySpread.Text = tbRepayAmt.Text = "";
            rdpRepayDate.SelectedDate = DateTime.Today;
            lblContractStatus.Text = lblOverDueDays.Text = "";
            lblPDType1_type.Text = lblPDType1_Caption.Text = lblPDType2_type.Text = lblPDType2_Caption.Text = lblPDType3_type.Text = lblPDType3_Caption.Text = "";
            lblPDType4_type.Text = lblPDType4_Caption.Text = lblDueItem1.Text = lblDueItem1_Caption.Text = lblDueItem1_Amt_col2.Text = "";
            lblDueItem2.Text = lblDueItem2_Caption.Text = lblDueItem2_Amt_col2.Text = "";
            lblDueItem3.Text = lblDueItem3_Caption.Text = lblDueItem3_Amt_col2.Text = "";
            lblDueItem4.Text = lblDueItem4_Caption.Text = lblDueItem4_Amt_col2.Text =lblRepaidStatus.Text= "";
            lblPDType1_Amt.Text = lblPDType2_Amt.Text = lblPDType3_Amt.Text = lblPDType4_Amt.Text = lblTotalPDAmount.Text = lblDueDate.Text = lblDueItem1_Amt.Text
            = lblDueItem2_Amt.Text = lblDueItem3_Amt.Text =lblDueItem4_Amt.Text= "";
            rcbAccountToDebit.SelectedValue = "";
            tbChargeCode.Text = tbChargeAmount.Text = tbAmount.Text = "";
        }
    }
}