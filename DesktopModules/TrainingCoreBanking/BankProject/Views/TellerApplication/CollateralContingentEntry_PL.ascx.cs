using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;
using DotNetNuke.Entities.Tabs;

namespace BankProject.Views.TellerApplication
{
    public partial class CollateralContingentEntry_PL : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadCurrencies();
            LoadToolBar();
        }
        protected void RadToolBar2_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var TollbarButton = e.Item as RadToolBarButton;
            string CommandName = TollbarButton.CommandName;
            if (CommandName == "search")
            {
                RadGridPreview.DataSource = TriTT_Credit.BCONTINGENT_ENQUIRY(tbContingentID.Text, tbRefID.Text, tbFullName.Text, tbCustomerID.Text, rcbCurrency.SelectedValue
                    , Convert.ToDouble(tbFromNominalValue.Text == "" ? "0" : tbFromNominalValue.Text), Convert.ToDouble(tbToNominalValue.Text == "" ? "0" : tbToNominalValue.Text)
                    , tbLegalID.Text);
                RadGridPreview.DataBind();
            }

        }
        protected void RadGridPreview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (IsPostBack)
            {
                RadGridPreview.DataSource = TriTT_Credit.BCONTINGENT_ENQUIRY(tbContingentID.Text, tbRefID.Text, tbFullName.Text, tbCustomerID.Text, rcbCurrency.SelectedValue
                   , Convert.ToDouble(tbFromNominalValue.Text == "" ? "0" : tbFromNominalValue.Text), Convert.ToDouble(tbToNominalValue.Text == "" ? "0" : tbToNominalValue.Text)
                   , tbLegalID.Text);
            }
            else RadGridPreview.DataSource = TriTT_Credit.BCONTINGENT_ENQUIRY("!","!","!","!","!",0,0,"");
        }
        public string getUrlPreview(string id)
        {
            TabController tCtlr = new TabController();
            TabInfo tInfoP = tCtlr.GetTabByName("Contingent Entry", PortalId);
            int tID = 392;
            if (tInfoP != null)
            {
                int tpID = tInfoP.TabID;
                TabInfo tInfo = tCtlr.GetTabByName("Input", PortalId, tpID);

                if (tInfo != null)
                {
                    tID = tInfo.TabID;
                }
            }

            //return "Default.aspx?tabid=383" + "&ID=" + id;
            //return "Default.aspx?tabid=392" + "&ID=" + id;
            return "Default.aspx?tabid=" + tID + "&ID=" + id;
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