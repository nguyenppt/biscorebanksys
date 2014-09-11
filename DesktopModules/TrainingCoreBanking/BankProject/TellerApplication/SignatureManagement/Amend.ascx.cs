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
    public partial class Amend : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lblCustomerName.Text = this.GetType().ToString();
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            //
            txtCustomerId.Text = Request.QueryString["tid"];
            if (String.IsNullOrEmpty(txtCustomerId.Text)) return;
            //
            txtCustomerIdOld.Value = txtCustomerId.Text;
            DataTable tDetail = bd.Customer.SignatureDetail(txtCustomerId.Text);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                lblCustomerName.Text = "This Customer does not exist.";
                return;
            }
            //
            DataRow dr = tDetail.Rows[0];            
            txtCustomerId.Enabled = false;
            lblCustomerName.Text = dr["CustomerName"].ToString();
            imgSignatureOld.ImageUrl = "~/" + bd.Customer.SignaturePath + "/" + dr["Signatures"];
            lnkSignatureOld.NavigateUrl = imgSignatureOld.ImageUrl;
            //
            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName.Equals(bc.Commands.Commit) && txtSignature.FileName != "")
            {
                try
                {
                    //upload file
                    string fileName = "Signature-" + txtCustomerId.Text.Trim() + "-1";//Signature-CIF-Order
                    int i = txtSignature.FileName.LastIndexOf(".");
                    string fileExt = txtSignature.FileName.Substring(i, txtSignature.FileName.Length - i);
                    fileName += fileExt;
                    txtSignature.SaveAs(Server.MapPath(bd.Customer.SignaturePath) + @"\" + fileName);
                    //save to database
                    bd.Customer.InsertSignature(txtCustomerId.Text, fileName, this.UserInfo.Username);
                    bc.Commont.SetEmptyFormControls(this.Controls);
                    //RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    txtCustomerId.Enabled = true;
                    txtCustomerIdOld.Value = "";
                    imgSignatureOld.ImageUrl = "";
                    lnkSignatureOld.NavigateUrl = "#";
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data success !");
                }
                catch (Exception err)
                {
                    bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data error : " + err.Message);
                }
            }
        }
    }
}