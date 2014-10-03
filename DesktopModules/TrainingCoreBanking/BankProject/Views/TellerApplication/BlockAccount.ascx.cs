using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

namespace BankProject.Views.TellerApplication
{
    public partial class BlockAccount : PortalModuleBase
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
                    string urlFTAccountClose = this.EditUrl("BlockAccountList");
                    this.Response.Redirect(urlFTAccountClose);
                    break;

                case "Commit":
                    BankProject.DataProvider.Database.BOPENACCOUNT_BLOCK_Update(txtId.Text, txtAmount.Value.HasValue ? txtAmount.Value.Value : 0,dtpFromDate.SelectedDate
                        , dptToDate.SelectedDate, txtDescription.Text, chbBlockAccount.Checked ? chbBlockAccount.Checked : false);

                    firstload();
                    break;

                case "Authorize":
                    DataProvider.Database.BOPENACCOUNT_BLOCK_UpdateStatus("AUT", txtId.Text, this.UserId.ToString());
                    firstload();
                    break;

                case "Reverse":
                    DataProvider.Database.BOPENACCOUNT_BLOCK_UpdateStatus("REV", txtId.Text, this.UserId.ToString());
                    firstload();
                    break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;

            if (Request.QueryString["codeid"] != null)
            {
                SetAccountId();//khi chon tu close account
                LoadToolBar(false);
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
            DataSet ds = new DataSet();
            int codeId = 0;

            codeId = int.Parse(Request.QueryString["codeid"]);

            ds = DataProvider.Database.BOPENACCOUNT_BLOCK_GetByID(codeId);

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                ds = BankProject.DataProvider.Database.BOPENACCOUNT_GetByID(codeId);

            if (ds != null || ds.Tables.Count > 0 || ds.Tables[0].Rows.Count > 0)
            {
                lbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                lbAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                chbBlockAccount.Checked = ds.Tables[0].Rows[0]["BlockAccount"].ToString() == "True";
                if (ds.Tables[0].Rows[0]["Block_Amount"] != null && ds.Tables[0].Rows[0]["Block_Amount"] != DBNull.Value)
                {
                    txtAmount.Value = double.Parse(ds.Tables[0].Rows[0]["Block_Amount"].ToString());

                    lbAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();

                    if (ds.Tables[0].Rows[0]["Block_FromDate"] != null && ds.Tables[0].Rows[0]["Block_FromDate"] != DBNull.Value)
                        dtpFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_FromDate"].ToString());

                    if (ds.Tables[0].Rows[0]["Block_ToDate"] != null && ds.Tables[0].Rows[0]["Block_ToDate"] != DBNull.Value)
                        dptToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_ToDate"].ToString());
                    txtDescription.Text = ds.Tables[0].Rows[0]["Block_Description"].ToString();
                }
                else
                {
                    txtDescription.Text = "PHONG TOA TK: " + ds.Tables[0].Rows[0]["AccountCode"].ToString();
                    dtpFromDate.SelectedDate = DateTime.Now;
                    this.dptToDate.SelectedDate = DateTime.Now.AddDays(5);
                }
            }
        }

        void firstload()
        {
            this.LoadToolBar(false);

            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            dtpFromDate.SelectedDate = DateTime.Now;
            this.dptToDate.SelectedDate = DateTime.Now.AddDays(5);
            chbBlockAccount.Checked = false;
        }

        void filldata(string code)
        {
            DataSet ds;
            if (code != "")
                ds = DataProvider.Database.BOPENACCOUNT_BLOCK_GetByCode(code);
            else
                ds = DataProvider.Database.BOPENACCOUNT_BLOCK_GetByID(int.Parse(Request.QueryString["BlockId"].ToString()));

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                txtId.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();
                lbCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                lbCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Block_Description"].ToString();
                if (ds.Tables[0].Rows[0]["Block_Amount"] != null && ds.Tables[0].Rows[0]["Block_Amount"] != DBNull.Value)
                    txtAmount.Value = double.Parse(ds.Tables[0].Rows[0]["Block_Amount"].ToString());

                lbAccount.Text = ds.Tables[0].Rows[0]["AccountCode"].ToString();

                if (ds.Tables[0].Rows[0]["Block_FromDate"] != null && ds.Tables[0].Rows[0]["Block_FromDate"] != DBNull.Value)
                    dtpFromDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_FromDate"].ToString());

                if (ds.Tables[0].Rows[0]["Block_ToDate"] != null && ds.Tables[0].Rows[0]["Block_ToDate"] != DBNull.Value)
                    dptToDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["Block_ToDate"].ToString());

                bool isautho = ds.Tables[0].Rows[0]["Block_Status"].ToString() == "AUT";
                bool isrev = ds.Tables[0].Rows[0]["Block_Status"].ToString() == "REV";
                chbBlockAccount.Checked = ds.Tables[0].Rows[0]["BlockAccount"].ToString() == "True";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, Request.QueryString["BlockId"] == null && !isautho);
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

                if (isrev)
                {
                    RadToolBar1.FindItemByValue("btCommit").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                }
            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            filldata(txtId.Text);
        }
    }
}