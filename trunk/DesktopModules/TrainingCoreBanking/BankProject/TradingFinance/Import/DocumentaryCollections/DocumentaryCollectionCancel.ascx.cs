using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using BankProject.Controls;
using BankProject.DataProvider;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace BankProject.TradingFinance.Import.DocumentaryCollections
{
    public partial class DocumentaryCollectionCancel : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (IsPostBack) return;

        //    var dsCus = DataTam.B_BCUSTOMERS_GetAll();

        //    comboDraweeCusNo.Items.Clear();
        //    comboDraweeCusNo.Items.Add(new RadComboBoxItem(""));
        //    comboDraweeCusNo.DataValueField = "CustomerID";
        //    comboDraweeCusNo.DataTextField = "CustomerID";
        //    comboDraweeCusNo.DataSource = dsCus;
        //    comboDraweeCusNo.DataBind();

        //    comboDrawerCusNo.Items.Clear();
        //    comboDrawerCusNo.Items.Add(new RadComboBoxItem(""));
        //    comboDrawerCusNo.DataValueField = "CustomerID";
        //    comboDrawerCusNo.DataTextField = "CustomerID";
        //    comboDrawerCusNo.DataSource = dsCus;
        //    comboDrawerCusNo.DataBind();

        //    InitToolBar(false);
        //}

        //protected void InitToolBar(bool flag)
        //{
        //    RadToolBar2.FindItemByValue("btAuthorize").Enabled = flag;
        //    RadToolBar2.FindItemByValue("btRevert").Enabled = flag;
        //    RadToolBar2.FindItemByValue("btReview").Enabled = flag;
        //    RadToolBar2.FindItemByValue("btSave").Enabled = flag;
        //    RadToolBar2.FindItemByValue("btPrint").Enabled = flag;
        //}

        //protected void radGridReview_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByCancel("XXXXX", "XXXX", "", "", "", "", UserId.ToString());
        //}

        //protected void btSearch_Click(object sender, EventArgs e)
        //{
        //    Search();
        //}

        //protected string geturlReview(string Id)
        //{
        //    return "Default.aspx?tabid=217&doccolcancel=" + Id;
        //}

        //protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        //{
        //    var toolBarButton = e.Item as RadToolBarButton;
        //    var commandName = toolBarButton.CommandName;

        //    switch (commandName)
        //    {
        //        case "search":
        //            Search();
        //            break;
        //    }
        //}

        //protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    var row = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
        //    e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        //}

        //protected void Search()
        //{
        //    if (string.IsNullOrEmpty(txtCode.Text.Trim())
        //        && string.IsNullOrEmpty(comboDraweeCusNo.SelectedValue)
        //        && string.IsNullOrEmpty(txtDraweeAddr.Text.Trim())
        //        && string.IsNullOrEmpty(comboDrawerCusNo.SelectedValue)
        //        && string.IsNullOrEmpty(txtDraweerAddr.Text.Trim()))
        //    {
        //        radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByCancel("XXXXX", "XXXX", "", "", "", "", UserId.ToString());
        //    }
        //    else
        //    {
        //        radGridReview.DataSource = SQLData.B_BDOCUMETARYCOLLECTION_GetByCancel(txtCode.Text.Trim(), 
        //            comboDraweeCusNo.SelectedValue, txtDraweeAddr.Text.Trim(), 
        //            comboDrawerCusNo.SelectedValue, txtDraweerAddr.Text.Trim(),
        //            "UNA", UserId.ToString());    
        //    }
        //    radGridReview.DataBind();
        //}
    }
}