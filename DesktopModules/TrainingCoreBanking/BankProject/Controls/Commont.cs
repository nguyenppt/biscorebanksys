using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Controls
{
    public class Commont
    {
        public const string breakLine = "\r\n";

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
                else if (Ctrl is RadEditor)
                    ((RadEditor)Ctrl).Enabled = enabel;
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
                if (Ctrl is RadEditor)
                    ((RadEditor)Ctrl).Content = string.Empty;
                else
                    SetEmptyFormControls(Ctrl.Controls);
            }
        }
        //Xem Signature Management -> Amend.ascx
        public static void ShowClientMessageBox(Page pageControl, System.Type typeOfPageControl, string contents, int width = 420, int heigth = 150)
        {
            ShowClientMessageBox(pageControl, typeOfPageControl, contents, null);
        }
        public static void ShowClientMessageBox(Page pageControl, System.Type typeOfPageControl, string contents, string redirectPage, int width = 420, int heigth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + heigth +
                "', 'Warning');" + (String.IsNullOrEmpty(redirectPage) ? "" : "window.location='" + redirectPage + "';") + " Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
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
            cboList.Items.Clear();
            //
            cboList.DataTextField = DataTextField;
            cboList.DataValueField = DataValueField;
            cboList.DataSource = DataSource;
            cboList.DataBind();
            if (cboList.Items.Count > 0)
            {
                cboList.Items.Insert(0, new RadComboBoxItem(""));
            }
        }

        /*
        * Method Revision History:
        * Version        Date            Author            Comment
        * ----------------------------------------------------------
        * 0.1            Jan 15, 2015    Hien Nguyen       init code
        */
        public static void removeCurrencyItem(RadComboBox comboBox, string removedItem)
        {
            //remove "GOLD" from list of currency
            int index = comboBox.Items.FindItemIndexByValue(removedItem, true);
            if (index != -1)
            {
                comboBox.Items.Remove(index);
            }
        }

        //
        public static DataRow loadBankSwiftCodeInfo(string bankCode, ref Label lblMessage, ref RadTextBox txtBankName)
        {
            lblMessage.Text = "";
            txtBankName.Text = "";
            bankCode = bankCode.Trim();
            if (string.IsNullOrEmpty(bankCode)) return null;
            //
            DataTable t = DataProvider.SQLData.B_BBANKSWIFTCODE_GetByCode(bankCode);
            if (t == null || t.Rows.Count <= 0)
            {
                lblMessage.Text = "Can not find this Bank.";
                return null;
            }
            DataRow dr = t.Rows[0];
            txtBankName.Text = dr["BankName"].ToString();

            return dr;
        }
        public static DataRow loadBankSwiftCodeInfo(string BankCode, ref Label lblMessage, ref RadTextBox txtBankName, ref RadTextBox txtBankAddr, ref RadTextBox txtBankCity, ref RadTextBox txtBankCountry)
        {
            txtBankAddr.Text = "";
            txtBankCity.Text = "";
            txtBankCountry.Text = "";
            DataRow dr = loadBankSwiftCodeInfo(BankCode, ref lblMessage, ref txtBankName);
            if (dr == null) return null;
            txtBankCity.Text = dr["City"].ToString();
            txtBankCountry.Text = dr["Country"].ToString();

            return dr;
        }
        //Fix bug
        //A critical error has occurred. Value of '1/1/1900 12:00:00 AM' is not valid for 'SelectedDate'. 'SelectedDate' should be between 'MinDate' and 'MaxDate'. Parameter name: SelectedDate
        public static void setDate(object dbDate, ref RadDatePicker txtDate)
        {
            if (dbDate != DBNull.Value)
            {
                if (!Convert.ToDateTime(dbDate).ToString("yyyyMMdd").Equals("19000101"))
                    txtDate.SelectedDate = Convert.ToDateTime(dbDate);
            }
        }
        //
        public static void BankTypeChange(string BankType, ref Label lblMessage, ref RadTextBox txtBankCode, ref RadTextBox txtBankName, ref RadTextBox txtBankAddr, ref RadTextBox txtBankCity, ref RadTextBox txtBankCountry)
        {
            lblMessage.Text = "";
            txtBankCode.Enabled = BankType.Equals("A");
            txtBankCode.Text = "";
            txtBankName.Enabled = !BankType.Equals("A");
            txtBankName.Text = "";
            txtBankAddr.Enabled = !BankType.Equals("A");
            txtBankAddr.Text = "";
            txtBankCity.Enabled = !BankType.Equals("A");
            txtBankCity.Text = "";
            txtBankCountry.Enabled = !BankType.Equals("A");
            txtBankCountry.Text = "";
        }
    }
}