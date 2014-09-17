using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Controls
{
    public class Commont
    {
        public static void SetTatusFormControls(ControlCollection ChildCtrls, bool enabel)
        {
            foreach (Control Ctrl in ChildCtrls)
            {
                if (Ctrl is TextBox)
                    ((TextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadComboBox)
                    ((RadComboBox)Ctrl).Enabled = enabel;
                else if(Ctrl is RadMaskedTextBox)
                    ((RadMaskedTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is Label)
                    ((Label)Ctrl).Enabled = enabel;
                else if (Ctrl is RadNumericTextBox)
                    ((RadNumericTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadTextBox)
                    ((RadTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadDatePicker)
                    ((RadDatePicker)Ctrl).Enabled = enabel;
                else if (Ctrl is VVTextBox)
                    ((VVTextBox)Ctrl).SetEnable(enabel);
                else if (Ctrl is VVNumberBox)
                    ((VVNumberBox)Ctrl).SetEnable(enabel);
                else if (Ctrl is VVDatePicker)
                    ((VVDatePicker)Ctrl).SetEnable(enabel);
                else
                    SetTatusFormControls(Ctrl.Controls, enabel);
            }
        }

        public static void SetEmptyFormControls(ControlCollection ChildCtrls)
        {
            foreach (Control Ctrl in ChildCtrls)
            {
                if (Ctrl is TextBox)
                    ((TextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadComboBox)
                    ((RadComboBox)Ctrl).SelectedValue = string.Empty;
                else if (Ctrl is Label)
                    ((Label)Ctrl).Text = string.Empty;
                else if (Ctrl is RadNumericTextBox)
                    ((RadNumericTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadMaskedTextBox)
                    ((RadMaskedTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadTextBox)
                    ((RadTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadDatePicker)
                    ((RadDatePicker)Ctrl).SelectedDate = null;
                else if (Ctrl is VVTextBox)
                    ((VVTextBox)Ctrl).SetTextDefault("");
                else if (Ctrl is VVNumberBox)
                    ((VVNumberBox)Ctrl).SetTextDefault("");
                else if (Ctrl is VVDatePicker)
                    ((VVDatePicker)Ctrl).SetTextDefault("");
                else
                    SetEmptyFormControls(Ctrl.Controls);
            }
        }
        //Xem Signature Management -> Amend.ascx
        public static void ShowClientMessageBox(Page pageControl, System.Type typeOfPageControl, string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            pageControl.ClientScript.RegisterStartupScript(typeOfPageControl, "radalert", radalertscript);
        }
        //Xem Signature Management -> Enquiry.ascx
        public static string GenerateEnquiryButtons(string TransId, string Status, int? viewTabId, int? amendTabId, int? reverseTabId, int? approveTabId)
        { 
            return GenerateEnquiryButtons(TransId, Status, viewTabId, amendTabId, reverseTabId, approveTabId, false);
        }
        public static string GenerateEnquiryButtons(string TransId, string Status, int? viewTabId, int? amendTabId, int? reverseTabId, int? approveTabId, bool allowAmendAuthorizeTrans)
        {
            string viewURL = "", amendURL = "", reverseURL = "", approveURL = "";
            if (viewTabId.HasValue) viewURL = "Default.aspx?tabid=" + viewTabId + "&tid=" + TransId;
            if (amendTabId.HasValue) amendURL = "Default.aspx?tabid=" + amendTabId + "&tid=" + TransId;
            if (reverseTabId.HasValue) reverseURL = "Default.aspx?tabid=" + reverseTabId + "&tid=" + TransId;
            if (approveTabId.HasValue) approveURL = "Default.aspx?tabid=" + approveTabId + "&tid=" + TransId;

            return GenerateEnquiryButtons(Status, viewURL, amendURL, reverseURL, approveURL, allowAmendAuthorizeTrans);
        }
        public static string GenerateEnquiryButtons(string Status, string viewURL, string amendURL, string reverseURL, string approveURL)
        {
            return GenerateEnquiryButtons(Status, viewURL, amendURL, reverseURL, approveURL, false);
        }
        public static string GenerateEnquiryButtons(string Status, string viewURL, string amendURL, string reverseURL, string approveURL, bool allowAmendAuthorizeTrans)
        {
            string urls = "<style>.enquiryButton {border:0px;width:20px;margin-right:5px;} .enquiryButtonDisable {opacity:0.5;}</style>", url, icon;
            //view
            if (!String.IsNullOrEmpty(viewURL))
            {
                icon = "<img src=\"Icons/bank/preview2.png\" class=\"enquiryButton\" />";
                url = viewURL;
                urls += "<a href=\"" + url + "\" title=\"View\">" + icon + "</a>";
            }
            //Edit
            if (!String.IsNullOrEmpty(amendURL))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/edit.png\" class=\"enquiryButton enquiryButtonDisable\" />";                
                if (Status.Equals(BankProject.DataProvider.TransactionStatus.UNA) ||
                    (Status.Equals(BankProject.DataProvider.TransactionStatus.AUT) && allowAmendAuthorizeTrans))
                {                    
                    url = amendURL;
                    icon = "<img src=\"Icons/bank/edit.png\" class=\"enquiryButton\" />";
                }
                urls += "<a href=\"" + url + "\" title=\"Edit\">" + icon + "</a>";
            }
            //Reverse
            if (!String.IsNullOrEmpty(reverseURL))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/delete.png\" class=\"enquiryButton enquiryButtonDisable\" />";                
                if (Status.Equals(BankProject.DataProvider.TransactionStatus.UNA))
                {
                    icon = "<img src=\"Icons/bank/delete.png\" class=\"enquiryButton\" />";
                    url = reverseURL;
                }
                urls += "<a href=\"" + url + "\" title=\"Reverse\">" + icon + "</a>";
            }
            //Approve
            if (!String.IsNullOrEmpty(approveURL))
            {
                url = "#";
                icon = "<img src=\"Icons/bank/approve.png\" class=\"enquiryButton enquiryButtonDisable\" />";                
                if (Status.Equals(BankProject.DataProvider.TransactionStatus.UNA))
                {
                    icon = "<img src=\"Icons/bank/approve.png\" class=\"enquiryButton\" />";
                    url = approveURL;
                }
                urls += "<a href=\"" + url + "\" title=\"Approve\">" + icon + "</a>";
            }
            //
            return urls;
        }
        //
        public static void initRadComboBox(ref RadComboBox cboList, string DataTextField, string DataValueField, object DataSource)
        {
            cboList.DataTextField = DataTextField;
            cboList.DataValueField = DataValueField;
            cboList.DataSource = DataSource;
            cboList.DataBind();
            if (cboList.Items.Count > 0)
            {
                cboList.Items.Insert(0, new RadComboBoxItem(""));
            }
        }
    }
}