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
    public partial class CollateralContingentEntry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public static int AutoID = 1;
        private string Refix_BMACODE()
        {
            return "DC";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
           // gia tri mac dinh khi page loaded lan dau
            rcbCustomerID.Focus();
            rdpValueDate.SelectedDate = DateTime.Now;           
            tbCollateralContengentEntry.Text = TriTT.B_BMACODE_GetNewID_2("COLL_CONTIN_ENTRY",Refix_BMACODE(), "-");
            rcbCustomerID.DataSource = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            rcbCustomerID.DataTextField = "CustomerName";
            rcbCustomerID.DataValueField = "CustomerID";
            rcbCustomerID.DataBind();
            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                LoadPreview();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else
            {
                LoadToolBar(false);
            }
        }

        protected void rcbCustomerID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled= !isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolbarbutton = e.Item as RadToolBarButton;
            var commandname = toolbarbutton.CommandName;
            if (commandname == "commit")
            {
                AutoID++;
                rcbCustomerID.Focus();
                tbCollateralContengentEntry.Text = TriTT.B_BMACODE_GetNewID_2("COLL_CONTIN_ENTRY", Refix_BMACODE(), "-");
                rcbCustomerID.SelectedValue = "";
                //tbCustomerName.Text = "";
                tbAddress.Text = "";
                tbID.Text = "";
                rdpDateOfIssue.SelectedDate = null;
                rcbTransactionCode.SelectedValue = "";
                rcbDebitOrCredit.SelectedValue = "";
                tbAmountLCY.Text = "";
               // tbAmountFCY.Text = "";
                tbDealRate.Text = "";
                rcbFreignCcy.SelectedValue = "";
                rdpValueDate.SelectedDate = DateTime.Now;
                tbReferenceNo.Text = "";
                tbNarrative.Text = "";
            }

            if (commandname == "Preview")
            {
                Response.Redirect(EditUrl("CollateralContingentEntry_PL"));
            }

            if (commandname == "authorize" || commandname == "reverse")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(false);
                AfterProc();
            }

        }
        public void AfterProc()
        {
            rcbCustomerID.Focus();
            rdpValueDate.SelectedDate = DateTime.Now;
            tbCollateralContengentEntry.Text = TriTT.B_BMACODE_GetNewID_2("COLL_CONTIN_ENTRY", Refix_BMACODE(), "-");
        }
         void LoadPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                Random RandID = new Random();
                
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                { 
                    case "0":
                        tbCollateralContengentEntry.Text = "DC-14190-001-963-063";
                        rcbCustomerID.SelectedValue = "1234";
                        //tbCustomerName.Text = "Vu Thi My Hanh";
                        tbAddress.Text = "400 Le Van Tri, P24, Q. Go Vap";
                        tbID.Text =Convert.ToString(RandID.Next(457676734,546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/10/2009");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "1500000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/9/2014");
                        tbReferenceNo.Text = "1235.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;

                    case "1":
                        tbCollateralContengentEntry.Text = "DC-14170-001-963-053";
                        rcbCustomerID.SelectedValue = "1100001";
                        //tbCustomerName.Text = "Phan Van Han";
                        tbAddress.Text = "100 Phan Van Han, Phuong 17, Quan Binh Thanh";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/10/2009");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "500000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/6/2014");
                        tbReferenceNo.Text = "1236.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                    case "2":
                        tbCollateralContengentEntry.Text = "DC-14171-001-963-054";
                        rcbCustomerID.SelectedValue = "1100002";
                        //tbCustomerName.Text = "Dinh Tien Hoang";
                        tbAddress.Text = "150 Dinh Tien Hoang, Phuong 1, Quan 1";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "600000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/5/2014");
                        tbReferenceNo.Text = "1237.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                    case "3":
                        tbCollateralContengentEntry.Text = "DC-14172-001-963-056";
                        rcbCustomerID.SelectedValue = "1100003";
                       // tbCustomerName.Text = "Pham Ngoc Thach";
                        tbAddress.Text = "180 Pham Ngoc Thach, Phuong 1, Quan 1";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "700000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/4/2014");
                        tbReferenceNo.Text = "1238.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                    case "4":
                        tbCollateralContengentEntry.Text = "DC-14173-001-963-057";
                        rcbCustomerID.SelectedValue = "1100004";
                       // tbCustomerName.Text = "Vo Thi Sau";
                        tbAddress.Text = "200 Tran Quoc Thao, Phuong 5, Quan 3";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "770000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/3/2014");
                        tbReferenceNo.Text = "1239.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                    case "5":
                        tbCollateralContengentEntry.Text = "DC-14174-001-963-058";
                        rcbCustomerID.SelectedValue = "1100005";
                       // tbCustomerName.Text = "Truong Cong Dinh";
                        tbAddress.Text = "270 Truong Cong Dinh, Phuong 5, Quan 3";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "990000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/2/2014");
                        tbReferenceNo.Text = "1240.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;

                    case "6":
                        tbCollateralContengentEntry.Text = "DC-14190-001-963-059";
                        rcbCustomerID.SelectedValue = "2102925";
                       // tbCustomerName.Text = "CTY TNHH SONG HONG";
                        tbAddress.Text = "90 An Duong, Quan Tay Ho";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "1000000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("7/1/2014");
                        tbReferenceNo.Text = "1240.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;

                    case "7":
                        tbCollateralContengentEntry.Text = "DC-14175-001-963-060";
                        rcbCustomerID.SelectedValue = "2102926";
                       // tbCustomerName.Text = "CTY TNHH PHAT TRIEN PHAN MEM ABC";
                        tbAddress.Text = "378 Nam Ky Khoi Nghia, Quan Phu Nhuan";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "1100000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("1/2/2014");
                        tbReferenceNo.Text = "1241.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;

                    case "8":
                        tbCollateralContengentEntry.Text = "DC-14176-001-963-061";
                        rcbCustomerID.SelectedValue = "2102927";
                       // tbCustomerName.Text = "Travelocity Corp.";
                        tbAddress.Text = "178 Washington Boulevard";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "1200000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("1/3/2014");
                        tbReferenceNo.Text = "1242.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                    case "9":
                        tbCollateralContengentEntry.Text = "DC-14177-001-963-0562";
                        rcbCustomerID.SelectedValue = "2102928";
                       // tbCustomerName.Text = "Wall Street Corp.";
                        tbAddress.Text = "100 Broadway";
                        tbID.Text = Convert.ToString(RandID.Next(457676734, 546750964));
                        rdpDateOfIssue.SelectedDate = Convert.ToDateTime("1/9/2007");
                        rcbTransactionCode.SelectedValue = "901";
                        rcbDebitOrCredit.SelectedValue = "D";
                        rcbAccountNo.SelectedValue = "VND-19411-0001";
                        tbAmountLCY.Text = "1300000000";
                        rdpValueDate.SelectedDate = Convert.ToDateTime("1/4/2014");
                        tbReferenceNo.Text = "1243.1.1";
                        tbNarrative.Text = "thong tin tai san dam bao";
                        break;
                }
            }
        }

        
    }
}