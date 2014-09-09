using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.TellerApplication.SignatureManagement
{
    public partial class Preview : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["cid"] == null) return;
            //
            txtCustomerId.Text = Request.QueryString["cid"].ToString();
            lblCustomerName.Text = "";
            imgSignature.ImageUrl = "";
            lnkSignature.NavigateUrl = "#";
            //
            DataTable tDetail = BankProject.DataProvider.Customer.SignatureDetail(txtCustomerId.Text);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                return;
            }
            //
            DataRow dr = tDetail.Rows[0];
            lblCustomerName.Text = dr["CustomerName"].ToString();
            imgSignature.ImageUrl = "~/" + BankProject.DataProvider.Customer.SignaturePath + "/" + dr["Signatures"];
            lnkSignature.NavigateUrl = imgSignature.ImageUrl;
        }
    }
}