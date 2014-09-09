using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject
{
    public partial class Amendment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var dtB_BOperation = DataProvider.SQLData.B_BOperation_GetAll().Tables[0];

            comboOperation.Items.Clear();
            comboOperation.DataTextField = "Code";
            comboOperation.DataValueField = "Name";
            comboOperation.DataSource = dtB_BOperation;
            comboOperation.DataBind();

            comboLCType.Items.Clear();
            comboLCType.DataTextField = "LCTYPE";
            comboLCType.DataValueField = "LCTYPE";
            comboLCType.DataSource = DataProvider.DataTam.B_BLCTYPES_GetAll();
            comboLCType.DataBind();
            //var result = dtB_BOperation.Select("Code = " + comboOperation.SelectedValue);
            //if (result.Length > 0)
            //{
            //    lblLcTypeName.Text = result[0]["Name"].ToString();
            //}
        }

        protected void comboOperation_SelectIndexChange(object sender, EventArgs e)
        {
            lblOperationName.Text = comboOperation.SelectedValue;
        }

        protected void tbEssurLCCode_OnTextChanged(object sender, EventArgs e)
        {
        }

        protected void comboLCType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var ds = DataProvider.SQLData.B_BLCTYPES_GetByLCType(comboLCType.SelectedValue);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblLcTypeName.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            }
            else
            {
                lblLcTypeName.Text = "";
            }
        }

        protected void comboLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        } 
    }
}