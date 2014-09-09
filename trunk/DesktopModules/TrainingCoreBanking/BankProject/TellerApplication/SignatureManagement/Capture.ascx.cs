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
    public partial class Capture : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case BankProject.Controls.Commands.Commit:
                    if (txtSignature.FileName != "")
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
                            //
                            txtCustomerIdOld.Value = "";
                            ShowMsgBox("Save data success !");
                        }
                        catch (Exception err)
                        {
                            ShowMsgBox("Save data error : " + err.Message);
                        }
                    }
                    break;
                case BankProject.Controls.Commands.Preview:
                    //Xem Preview.ascx
                    break;
                case BankProject.Controls.Commands.Authozize:
                case BankProject.Controls.Commands.Reverse:
                    try
                    {
                        if (commandName.Equals(BankProject.Controls.Commands.Authozize))
                            BankProject.DataProvider.Customer.UpdateSignatureStatus(txtCustomerId.Text, BankProject.DataProvider.TransactionStatus.AUT, this.UserInfo.Username);
                        else
                            BankProject.DataProvider.Customer.UpdateSignatureStatus(txtCustomerId.Text, BankProject.DataProvider.TransactionStatus.REV, this.UserInfo.Username);
                        BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                    }
                    catch (Exception err)
                    {
                        ShowMsgBox("Error : " + err.Message);
                    }
                    break;
            }            
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        protected void RadAjaxPanel1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            switch (e.Argument)
            {
                case "loadCustomerName":
                    DataTable tDetail = BankProject.DataProvider.Customer.CustomerDetail(txtCustomerId.Text);
                    if (tDetail == null || tDetail.Rows.Count <= 0)
                        lblCustomerName.Text = "Customer not found !";
                    else
                        lblCustomerName.Text = tDetail.Rows[0]["GBFullName"].ToString();
                    break;
                default:
                    break;
            }
        }
    }
}