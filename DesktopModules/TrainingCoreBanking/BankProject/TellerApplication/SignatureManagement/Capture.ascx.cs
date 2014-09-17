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
    public partial class Capture : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            imgSignaturePreview.Attributes.Add("css", "imgSignaturePreview NoDisplay");
            txtCustomerId.Text = Request.QueryString["tid"];
            if (String.IsNullOrEmpty(txtCustomerId.Text)) return;
            bc.Commont.SetTatusFormControls(this.Controls, false);
            //
            lblCustomerName.Text = "";
            imgSignaturePreview.ImageUrl = "";
            //
            DataTable tDetail = bd.Customer.SignatureDetail(txtCustomerId.Text);
            if (tDetail == null || tDetail.Rows.Count <= 0)
            {
                lblCustomerName.Text = "This Customer signature does not exist.";
                return;
            }
            //
            DataRow dr = tDetail.Rows[0];
            lblCustomerName.Text = dr["CustomerName"].ToString();
            cmdSelectSignatureImage.Visible = false;            
            imgSignaturePreview.Attributes.Remove("css");
            imgSignaturePreview.Attributes.Add("css", "imgSignaturePreview");
            string Signatures = dr["Signatures"].ToString(), SignaturesNew = "";
            if (dr["SignaturesNew"] != DBNull.Value) SignaturesNew = dr["SignaturesNew"].ToString();
            if (!String.IsNullOrEmpty(SignaturesNew))
            {
                lblNewSignature.Text = "New Signature";
                imgSignaturePreview.ImageUrl = "~/" + bd.Customer.SignaturePath + "/" + SignaturesNew;
                //
                imgOldSignaturePreview.Attributes.Add("css", "imgSignaturePreview");
                imgOldSignaturePreview.ImageUrl = "~/" + bd.Customer.SignaturePath + "/" + Signatures;
                lblOldSignature.Visible = true;
                imgOldSignaturePreview.Visible = true;
            }
            else
            {
                imgSignaturePreview.ImageUrl = "~/" + bd.Customer.SignaturePath + "/" + Signatures;
            }
            string Status = dr["Status"].ToString();
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = Status.Equals(bd.TransactionStatus.UNA);
            RadToolBar1.FindItemByValue("btReverse").Enabled = Status.Equals(bd.TransactionStatus.UNA);
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
                            //
                            Response.Redirect("Default.aspx?tabid=" + this.TabId);
                        }
                        catch (Exception err)
                        {
                            bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Save data error : " + err.Message);
                        }
                    }
                    break;
                case bc.Commands.Preview:
                    //Xem Preview.ascx
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

        protected void RadAjaxPanel1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "loadCustomerName":
                    DataTable tDetail = bd.Customer.CustomerDetail(txtCustomerId.Text);
                    if (tDetail == null || tDetail.Rows.Count <= 0)
                        lblCustomerName.Text = "This Customer does not exist.";
                    else
                        lblCustomerName.Text = tDetail.Rows[0]["GBFullName"].ToString();
                    break;
                default:
                    break;
            }
        }
    }
}