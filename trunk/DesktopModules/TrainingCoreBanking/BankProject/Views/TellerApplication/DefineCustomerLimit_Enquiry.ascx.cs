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
    public partial class DefineCustomerLimit_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadCurrencies();
            LoadCollateralType();
            LoadToolBar();
        }
        protected void RadToolBar2_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            string CommandName = ToolBarButton.CommandName;
            if (CommandName == "search")
            {
                if (IsPostBack)
                {
                    RadGrid.DataSource = TriTT.B_CUSTOMER_LIMIT_ENQUIRY(tbMaHanMucCha.Text, tbMaHanMucCon.Text, tbFullName.Text, tbCustomerID.Text, rcbCollateralType.SelectedValue
                        , rcbCollateral.SelectedValue, rcbCurrency.SelectedValue, tbFromIntLimitAmt.Value.HasValue ? tbFromIntLimitAmt.Value.Value : 0
                        , tbToIntLimitAmt.Value.HasValue ? tbToIntLimitAmt.Value.Value : 0);
                    RadGrid.DataBind();
                }
                else 
                {
                    RadGrid.DataSource = TriTT.B_CUSTOMER_LIMIT_ENQUIRY("!", tbMaHanMucCon.Text, "<>", tbCustomerID.Text, rcbCollateralType.SelectedValue
                        , rcbCollateral.SelectedValue, rcbCurrency.SelectedValue, tbFromIntLimitAmt.Value.HasValue ? tbFromIntLimitAmt.Value.Value : 0
                        , tbToIntLimitAmt.Value.HasValue ? tbToIntLimitAmt.Value.Value : 0);
                }
            }
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_CUSTOMER_LIMIT_ENQUIRY("!","!","","","","","",-1,-1);
        }
        protected string geturlReview(string MainLimitID, string SubLimitID)
        {
            return string.Format("Default.aspx?tabid=359&MainLimitID={0}&SubLimitID={1}",MainLimitID, SubLimitID);
        }
        protected void rcbCollateralType_ONSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCollateralCode(rcbCollateralType.SelectedValue);
        }
        protected void LoadCurrencies()
        {
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD", "VND");
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataBind();
        }
        protected void LoadCollateralType()
        {
            rcbCollateralType.DataSource = TriTT.B_CUSTOMER_LIMIT_Load_CollateralType();
            rcbCollateralType.DataValueField = "CollateralTypeCode";
            rcbCollateralType.DataTextField = "CollateralTypeHasName";
            rcbCollateralType.DataBind();
        }
        protected void LoadCollateralCode(string CollateralTypeCode)
        {
            rcbCollateral.Items.Clear();
            rcbCollateral.Text = "";
            DataSet ds = TriTT.B_CUSTOMER_LIMIT_Load_CollateralCode(CollateralTypeCode);
            if (ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CollateralCode"] = "";
                dr["CollateralHasName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCollateral.DataSource = ds;
            rcbCollateral.DataValueField = "CollateralCode";
            rcbCollateral.DataTextField = "CollateralHasName";
            rcbCollateral.DataBind();
        }
        private void LoadToolBar()
        {
            RadToolBar2.FindItemByValue("btCommit").Enabled = false;
            RadToolBar2.FindItemByValue("btPreview").Enabled = false;
            RadToolBar2.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar2.FindItemByValue("btReverse").Enabled = false;
            RadToolBar2.FindItemByValue("btSearch").Enabled = true;
            RadToolBar2.FindItemByValue("btPrint").Enabled = false;
        }
    }
}