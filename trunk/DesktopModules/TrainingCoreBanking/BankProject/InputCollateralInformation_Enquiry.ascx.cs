using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;

namespace BankProject
{
    public partial class InputCollateralInformation_Enquiry : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadCollateralType();
            LoadCurrencies();
            LoadToolBar();
        }
        protected void RadToolBar2_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var TollbarButton = e.Item as RadToolBarButton;
            string CommandName = TollbarButton.CommandName;
            if (CommandName == "search")
            {
                RadGrid.DataSource = TriTT.B_COLLATERAL_INFO_Enquiry(tbRightID.Text, tbCollInfoID.Text, tbFullName.Text, tbCustomerID.Text, rcbCollateralType.SelectedValue
                    ,rcbCollateral.SelectedValue, rcbCurrency.SelectedValue, Convert.ToDecimal( tbFromNominalValue.Value.HasValue? tbFromNominalValue.Value : 0),
                    Convert.ToDecimal(tbToNominalValue.Value.HasValue?tbToNominalValue.Value:0), tbContingentAccID.Text);
                RadGrid.DataBind();
            }

        }
        protected void rcbCollateralType_ONSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadCollateralCode(rcbCollateralType.SelectedValue);
        }
        protected void RadGrid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = TriTT.B_COLLATERAL_INFO_Enquiry("!!", "", "", "", "", "", "", 0, 0, "");
        }
        public string geturlReview(string CollInfoID)
        {
            return string.Format("Default.aspx?tabid=194&CollInfoID={0}", CollInfoID);
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
        protected void LoadCurrencies()
        {
            rcbCurrency.DataSource = TriTT.B_LoadCurrency("USD", "VND");
            rcbCurrency.DataValueField = "Code";
            rcbCurrency.DataTextField = "Code";
            rcbCurrency.DataBind();
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