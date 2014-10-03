using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class UnBlockAccount : PortalModuleBase
    {
        private void LoadToolBar(bool IsAuthorize)
        {
            RadToolBar1.FindItemByValue("btCommit").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !IsAuthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = IsAuthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void OnRadToolBarClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "Preview":
                    string urlFTAccountClose = this.EditUrl("UnBlockAccountPreviewList");
                    this.Response.Redirect(urlFTAccountClose);
                    break;

                case "Commit": //check or not check thi van unblock account 
                   DataProvider.Database.BOPENACCOUNT_UnBLOCK_UpdateStatus("UNA", txtId.Text, this.UserId.ToString(), false);
                   firstload();
                    break;

                case "Authorize":
                    DataProvider.Database.BOPENACCOUNT_UnBLOCK_UpdateStatus("AUT", txtId.Text, this.UserId.ToString(), false);
                    firstload();
                    break;

                case "Reverse":
                    DataProvider.Database.BOPENACCOUNT_UnBLOCK_UpdateStatus("REV", txtId.Text, this.UserId.ToString(), false);
                    firstload();
                    break;
            }
        }

        void firstload()
        {
            this.LoadToolBar(false);

            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            txtId.Enabled = true;
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;

            firstload();

            if (Request.QueryString["codeid"] != null)
            {
                SetAccountId();//khi chon tu close account
                //LoadToolBar(false);
                return;
            }
            if (Request.QueryString["BlockId"] != null)
            {
                filldata("");//khi chon preview de duyet,
                return;
            }
        }

        private void SetAccountId()
        {
            DataSet ds;
            ds = DataProvider.Database.BOPENACCOUNT_UnBLOCK_GetByID(int.Parse(Request.QueryString["codeid"].ToString()));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                lbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Block_Description"].ToString();
                txtAmount.Value = double.Parse(ds.Tables[0].Rows[0]["Block_Amount"].ToString());
                lbAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                chbBlockAccount.Checked = ds.Tables[0].Rows[0]["BlockAccount"].ToString() == "True";
                chbBlockAccount.Enabled = false;
                if (ds.Tables[0].Rows[0]["Block_FromDate"] != null && ds.Tables[0].Rows[0]["Block_FromDate"] != DBNull.Value)
                    dtpFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["Block_ToDate"] != null && ds.Tables[0].Rows[0]["Block_ToDate"] != DBNull.Value)
                    dptToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_ToDate"].ToString());

                bool isautho = ds.Tables[0].Rows[0]["UnBlock_Status"].ToString() == "AUT";
                //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["BlockId"] == null && !isautho);
                //LoadToolBar(Request.QueryString["BlockId"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                }
            }
        }

        void filldata(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BOPENACCOUNT_UnBLOCK_GetByCode(code);
            else
                ds = DataProvider.Database.BOPENACCOUNT_UnBLOCK_GetByID(int.Parse(Request.QueryString["BlockId"].ToString()));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                lbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Block_Description"].ToString();
                txtAmount.Value = double.Parse(ds.Tables[0].Rows[0]["Block_Amount"].ToString());
                lbAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                if (code != "")
                {
                    chbBlockAccount.Checked = ds.Tables[0].Rows[0]["blockAccount"].ToString() == "True";
                }
                chbBlockAccount.Enabled = false;
                if (ds.Tables[0].Rows[0]["Block_FromDate"] != null && ds.Tables[0].Rows[0]["Block_FromDate"] != DBNull.Value)
                    dtpFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["Block_ToDate"] != null && ds.Tables[0].Rows[0]["Block_ToDate"] != DBNull.Value)
                    dptToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_ToDate"].ToString());

                bool isautho = ds.Tables[0].Rows[0]["UnBlock_Status"].ToString() == "AUT";
                //BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["BlockId"] == null && !isautho);
                LoadToolBar(Request.QueryString["BlockId"] != null);

                if (isautho)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                }
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            filldata(txtId.Text);
        }
    }
}