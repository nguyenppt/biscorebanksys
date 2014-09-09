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
    public partial class AmendLoanContract_Item : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                if (Request.QueryString["LoanContractReference"] != null)
                {
                    LoadToolBar(true);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    LoadData_toAuthorize();
                }
                //tbID.Text= TriTT.B_BMACODE_Amend_Loan_Contract("AMEND_LOAN_CONTRACT","LD");
                //LoadToolBar(true);
            }
        }
        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var CommandName = ToolBarButton.CommandName;
            switch (CommandName)
            { 
                case "commit":
                    defaultSetting();
                    LoadToolBar(false);
                    break;
                case "authorize":
                case "reverse":
                    LoadToolBar(false);
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    defaultSetting();
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("AmendLoanContract_PL"));
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    break;
            }
        }
        protected void LoadPreview_toCommit()
        {
            string CustomerID = Request.QueryString["CustomerID"].ToString();
            switch (CustomerID)
            {
                case "2102928":
                    DetailData("LD/14200/0035", "2102928", "Wall Street Corp.", "", "Tất toán HĐ mua nhà, đất","DungHan",100000000);
                    break;
                case "2102926":
                    DetailData("LD/14201/0036", "2102926", "CTY TNHH PHAT TRIEN PHAN MEM ABC", "", "Tất toán HĐ mua nhà, đất","DungHan",100000000);
                    break;
                case "1100005":
                    DetailData("LD/14202/0037", "1100005", "Truong Cong Dinh", "", "Tất toán HĐ mua nhà, đất","DungHan",100000000);
                    break;
                case "1100004":
                    DetailData("LD/14203/0039", "1100004", "Vo Thi Sau", "", "Tất toán HĐ mua căn hộ","TruocHan",200000000);
                    break;
                case "1100003":
                    DetailData("LD/14204/0040", "1100003", "Pham Ngoc Thach", "", "Tất toán HĐ mua căn hộ","TruocHan",200000000);
                    break;
                case "2102927":
                    DetailData("LD/14205/0041", "2102927", "Travelocity Corp.", "", "Tất toán HĐ mua xe hơi","TruocHan",200000000);
                    break;
                case "2102925":
                    DetailData("LD/14206/0042", "2102925", "CTY TNHH SONG HONG", "", "Tất toán HĐ mua nhà, đất","TreHan",250000000);
                    break;
                case "1100001":
                    DetailData("LD/14207/0043", "1100001", "Phan Van Han", "", "Tất toán HĐ mua nhà, đất","TreHan",250000000);
                    break;
                case "1100002":
                    DetailData("LD/14209/0045", "1100002", "Dinh Tien Hoang", "", "Tất toán HĐ mua xe hơi", "TreHan", 250000000);
                    break;

            }
        }
        protected void LoadData_toAuthorize()
        {
            string LoanContractReference = Request.QueryString["LoanContractReference"].ToString();
            switch (LoanContractReference)
            {
                case "LD142000035":
                    DetailData("PDLD/14200/0035", "2102928", "Wall Street Corp.", "07/15/2014", "Tất toán HĐ mua nhà, đất", "DungHan", 100000000);
                    break;
                case "LD142010036":
                    DetailData("PDLD/14201/0036", "2102926", "CTY TNHH PHAT TRIEN PHAN MEM ABC", "07/16/2014", "Tất toán HĐ mua nhà, đất", "DungHan", 100000000);
                    break;
                case "LD142020037":
                    DetailData("PDLD/14202/0037", "1100005", "Truong Cong Dinh", "07/17/2014", "Tất toán HĐ mua nhà, đất", "DungHan", 100000000);
                    break;
                case "LD142030039":
                    DetailData("PDLD/14203/0039", "1100004", "Vo Thi Sau", "07/20/2014", "Tất toán HĐ mua căn hộ", "TruocHan", 200000000);
                    break;
                case "LD142040040":
                    DetailData("PDLD/14204/0040", "1100003", "Pham Ngoc Thach", "07/21/2014", "Tất toán HĐ mua căn hộ", "TruocHan", 200000000);
                    break;
                case "LD142050041":
                    DetailData("PDLD/14205/0041", "2102927", "Travelocity Corp.", "09/15/2014", "Tất toán HĐ mua xe hơi", "TruocHan", 200000000);
                    break;
                case "LD142060042":
                    DetailData("PDLD/14206/0042", "2102925", "CTY TNHH SONG HONG", "10/15/2014","Tất toán HĐ mua nhà, đất","TreHan",250000000);
                    break;
                case "LD142070043":
                    DetailData("PDLD/14207/0043", "1100001", "Phan Van Han", "12/15/2014", "Tất toán HĐ mua nhà, đất", "TreHan", 250000000);
                    break;
                case "LD142090045":
                    DetailData("PDLD/14209/0045", "1100002", "Dinh Tien Hoang", "11/19/2014", "Tất toán HĐ mua xe hơi", "TreHan", 250000000);
                    break;
            }
        }
        protected void DetailData(string AmendLoanContractID,string CustomerID, string CustomerName, string MatDate, string Caption,string KindOfContract,
                                    double LoanAmt)
        {
            tbID.Text=AmendLoanContractID;
            lblCustomreID.Text = CustomerID;
            if (MatDate != "")  /// co de phan biet form authorize voi form commit, gia tri MatDate se duoc thay doi  trong CASE:
            {
                rdpNewMatDate.SelectedDate = Convert.ToDateTime(MatDate);
                tbCustRemark.Text = "Tat toan Hop Dong cho Khach Hang";
            }
            else tbCustRemark.Text = "";

            lblCustomreName.Text = CustomerName;
            rcbCurrency.SelectedValue = "VND"; rcbCurrency.Enabled = false;
            lblSubCategory__ID_Caption.Text = Caption;
            lblLoanAmt.Text = string.Format("{0:C}", LoanAmt).Replace("$", "");
            double DrawdownAmt = 290000000;
            lblDrawdownAmt.Text = lblApproveAmt.Text = string.Format("{0:C}", DrawdownAmt).Replace("$", "");
            rdpValueDate.SelectedDate =Convert.ToDateTime("06/11/2014");
            double TotalIntAmt = 0;
            double TotalAmt = 0;
            switch (KindOfContract)
            { 
                case "DungHan":
                    rdpOrigMatDate.SelectedDate = DateTime.Today;
                    rdpNewMatDate.SelectedDate =rdpLoanClassDate.SelectedDate= rdpOrigMatDate.SelectedDate;
                    var songay = rdpOrigMatDate.SelectedDate - rdpValueDate.SelectedDate;
                    TotalIntAmt = LoanAmt * (0.144 / 365) *Convert.ToInt16(songay.Value.TotalDays);
                    TotalAmt = TotalIntAmt + LoanAmt;
                    lblTotalIntAmt.Text = string.Format("{0:C}", TotalIntAmt).Replace("$", "") + " - tất toán đúng hạn";
                    lblTotalAmt.Text = string.Format("{0:C}", TotalAmt).Replace("$", "");
                    break;
                case "TruocHan":
                    rdpOrigMatDate.SelectedDate = DateTime.Today;
                    rdpNewMatDate.SelectedDate =rdpLoanClassDate.SelectedDate= rdpOrigMatDate.SelectedDate.Value.AddDays(-30);
                    var songay2 = rdpNewMatDate.SelectedDate - rdpValueDate.SelectedDate;
                    TotalIntAmt = LoanAmt * (0.1 / 365) * Convert.ToInt16(songay2.Value.TotalDays);
                    TotalAmt = TotalIntAmt + LoanAmt;
                    lblInterestedRate.Text = "10.00";
                    lblTotalIntAmt.Text = string.Format("{0:C}", TotalIntAmt).Replace("$", "") + " - tất toán trước hạn";
                    lblTotalAmt.Text = string.Format("{0:C}", TotalAmt).Replace("$", "");
                    break;
                case "TreHan":
                    rdpOrigMatDate.SelectedDate = DateTime.Today;
                    rdpNewMatDate.SelectedDate = rdpLoanClassDate.SelectedDate = rdpOrigMatDate.SelectedDate.Value.AddDays(35);
                    var songay3 = rdpOrigMatDate.SelectedDate - rdpValueDate.SelectedDate;
                    var songay_phatthem = rdpNewMatDate.SelectedDate - rdpOrigMatDate.SelectedDate;
                    TotalIntAmt = LoanAmt * (0.1 / 365) * Convert.ToInt16(songay_phatthem.Value.TotalDays) + LoanAmt * (0.144 / 365) * Convert.ToInt16(songay3.Value.TotalDays);
                    TotalAmt = TotalIntAmt + LoanAmt;
                    lblTotalIntAmt.Text = string.Format("{0:C}", TotalIntAmt).Replace("$", "") + " - tất toán trể hạn";
                    lblTotalAmt.Text = string.Format("{0:C}", TotalAmt).Replace("$", "");
                    break;
            }
            rcbLoanClass.SelectedValue = "1";
            rcbAutoSch.SelectedValue =rcbDefSch.SelectedValue= "NO";
            rcbChargeCode.SelectedValue = "1";
            rcbRepaySchType.SelectedValue = "N";
           
        }
        protected void defaultSetting()
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            //rdpValueDate.SelectedDate = DateTime.Today;
            //rdpOrigMatDate.SelectedDate = DateTime.Now;
            rdpNewMatDate.SelectedDate = DateTime.Now;
            rdpLoanClassDate.SelectedDate = DateTime.Now;
            //tbID.Text =lblMainCategory_ID.Text=lblMainCategory_ID_Caption.Text= "";
            //lblCustomreID.Text = "";
            //lblCustomreName.Text = "";
            //rcbCurrency.SelectedValue = ""; rcbCurrency.Enabled = true;
           
            //rcbLoanClass.SelectedValue = "";
            //tbCustRemark.Text = "";
            //tbAutoSch.Text = tbDefSch.Text = "";
            //rcbChargeCode.SelectedValue = "";
            //rcbRepaySchType.SelectedValue = "";
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
    }
}