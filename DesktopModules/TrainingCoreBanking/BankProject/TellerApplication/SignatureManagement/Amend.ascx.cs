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
    public partial class Amend : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            if (Request.QueryString["cid"] == null) return;
            txtCustomerId.Text = Request.QueryString["cid"];
            if (txtCustomerId.Text == "") return;
            txtCustomerIdOld.Value = txtCustomerId.Text;
            DataTable tDetail = BankProject.DataProvider.Customer.SignatureDetail(txtCustomerId.Text);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                return;
            }
            //
            DataRow dr = tDetail.Rows[0];            
            txtCustomerId.Enabled = false;
            lblCustomerName.Text = dr["CustomerName"].ToString();
            imgSignature.ImageUrl = "~/" + BankProject.DataProvider.Customer.SignaturePath + "/" + dr["Signatures"];
            lnkSignature.NavigateUrl = imgSignature.ImageUrl;
            //
            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName.Equals(BankProject.Controls.Commands.Commit) && txtSignature.FileName != "")
            {
                try
                {
                    //upload file
                    string fileName = "Signature-" + txtCustomerId.Text.Trim() + "-1";//Signature-CIF-Order
                    int i = txtSignature.FileName.LastIndexOf(".");
                    string fileExt = txtSignature.FileName.Substring(i, txtSignature.FileName.Length - i);
                    fileName += fileExt;
                    txtSignature.SaveAs(Server.MapPath(BankProject.DataProvider.Customer.SignaturePath) + @"\" + fileName);
                    //save to database
                    BankProject.DataProvider.Customer.InsertSignature(txtCustomerId.Text, fileName, this.UserInfo.Username);
                    BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                    //RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    txtCustomerId.Enabled = true;
                    txtCustomerIdOld.Value = "";
                    imgSignature.ImageUrl = "";
                    lnkSignature.NavigateUrl = "#";
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    lblSignaturePreview.Text = "Signature old";
                    ShowMsgBox("Save data success !");
                }
                catch (Exception err)
                {
                    ShowMsgBox("Save data error : " + err.Message);
                }
            }
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
    }
}