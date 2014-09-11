using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.TellerApplication.SignatureManagement
{
    public partial class Preview : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtCustomerId.Text = Request.QueryString["tid"];
            if (String.IsNullOrEmpty(txtCustomerId.Text)) return;
            //
            lblCustomerName.Text = "";
            imgSignature.ImageUrl = "";
            lnkSignature.NavigateUrl = "#";
            //
            DataTable tDetail = bd.Customer.SignatureDetail(txtCustomerId.Text);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                lblCustomerName.Text = "This Customer does not exist.";
                return;
            }
            //
            DataRow dr = tDetail.Rows[0];
            lblCustomerName.Text = dr["CustomerName"].ToString();
            imgSignature.ImageUrl = "~/" + bd.Customer.SignaturePath + "/" + dr["Signatures"];
            lnkSignature.NavigateUrl = imgSignature.ImageUrl;
        }
    }
}