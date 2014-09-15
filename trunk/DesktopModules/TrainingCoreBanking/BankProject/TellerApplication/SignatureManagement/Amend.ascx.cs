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
            if (dr["Status"].ToString().Equals(bd.TransactionStatus.UNA))
            {
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    if (!String.IsNullOrEmpty(txtSignature.FileName))
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
                    break;
                case bc.Commands.Authozize:
                case bc.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(bc.Commands.Authozize))
                            bd.Customer.UpdateSignatureStatus(txtCustomerId.Text, bd.TransactionStatus.AUT, this.UserInfo.Username);
                        else
                            bd.Customer.UpdateSignatureStatus(txtCustomerId.Text, bd.TransactionStatus.REV, this.UserInfo.Username);
                        Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    }
                    catch (Exception err)
                    {
                        bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Error : " + err.Message);
                    }
                    break;
            }            
        }
    }
}