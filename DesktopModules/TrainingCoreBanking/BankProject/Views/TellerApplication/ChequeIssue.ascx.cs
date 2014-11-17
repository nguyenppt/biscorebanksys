using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeIssue : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            FirstLoad();
            if (!this.IsPostBack)
            {

                if (Request.QueryString["ChequeID"] != null)
                {
                    LoadCheque(Request.QueryString["ChequeID"].ToString());
                }
                else
                {
                    LoadToolBar(true);
                }
            }
        }
       
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var commandName = ToolBarButton.CommandName;
            if (commandName == "commit")
            {
                string ChequeID = tbChequeID.Text.Trim();
                if (ChequeID.Length == 22 && ChequeID.Substring(2, 1) == "." && ChequeID.Substring(15, 1) == ".")
                {
                    string ChequeType = ChequeID.Substring(0, 2);
                    string WorkingAcct = ChequeID.Substring(3, 12);
                    string[] ChequeStatus = rcbChequeStatus.Text.Split('-');
                    TriTT.B_CHEQUEISSUE_Insert_Update(ChequeID, WorkingAcct, ChequeType, ChequeStatus[0].Trim(), ChequeStatus[1].Trim()
                        , rcbCurrency.SelectedValue, rdpIssueDate.SelectedDate, tbQuantityOfIssued.Value.Value, tbChequeNoStart.Value.Value, tbChequeNoStart.Value.Value + tbQuantityOfIssued.Value.Value - 1,
                        double.Parse(lblNextTransCom.Text.Length !=0? lblNextTransCom.Text: "0"), "UNA", UserInfo.Username.ToString(),lblCustomerID.Text );
                    Response.Redirect("Default.aspx?tabid=131");
                }
                else { ShowMsgBox("Cheque ID not Correct, check again"); return; }
            }
            if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("ChequeIssue_PL"));
            }
            if (commandName == "authorize" )
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                TriTT.B_CHEQUEISSUE_Update_Status(tbChequeID.Text, "AUT");
                Response.Redirect("Default.aspx?tabid=131");

            }
            if (commandName == "reverse")
            {
                LoadToolBar(false);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                TriTT.B_CHEQUEISSUE_Update_Status(tbChequeID.Text, "REV");
                LoadToolBar(true);
                //Response.Redirect("Default.aspx?tabid=131");
            }
            if (commandName == "search")
            {
                LoadCheque(tbChequeID.Text.Trim());
            }
            if (commandName == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                LoadToolBar(true);
            }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            LoadCheque(tbChequeID.Text.Trim());
        }
        #region Help Method
        protected void LoadCheque(string ChequeID)
        {
            if (ChequeID.Length == 22 && ChequeID.Substring(2, 1) == "." && ChequeID.Substring(15, 1) == ".")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls); //f5 form de load tiep du lieu
                FirstLoad();
                tbChequeID.Text = ChequeID;
                var WorkingAcctID = ChequeID.Substring(3, 12);
                DataSet ds =TriTT.B_CHEQUEISSUE_Check_WorkingAcct(WorkingAcctID,"VND");
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblCustomeName.Text = "";
                    lblCustomeName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    lblCustomerID.Text = ds.Tables[0].Rows[0]["CustomerID"].ToString();
                    DataSet ds1 = TriTT.B_CHEQUEISSUE_LoadChequeID(ChequeID);
                    if(ds1.Tables != null && ds1.Tables[0].Rows.Count>0)
                    {
                         DataRow dr = ds1.Tables[0].Rows[0];
                         rcbChequeStatus.SelectedValue = dr["ChequeStatusID"].ToString();
                         rcbCurrency.SelectedValue = dr["Currency"].ToString();
                         if (dr["IssueDate"].ToString() != "")
                         {
                             rdpIssueDate.SelectedDate = Convert.ToDateTime(dr["IssueDate"].ToString());
                         }
                         tbQuantityOfIssued.Text = dr["Quantity"].ToString();
                         tbChequeNoStart.Text = dr["ChequeNoStart"].ToString();
                        if(dr["NextTransCom"].ToString()=="0")
                         lblNextTransCom.Text = "";
                        LoadToolBar(false);
                         if (dr["Status"].ToString() == "AUT")
                         {
                             BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                             LoadToolBar_AllFalse();
                         }
                         return;
                    }
                    LoadToolBar(true);
                }
                else
                {
                    lblCustomeName.Text = "Working Account does not exist !";
                    lblCustomerID.Text = "";
                }
            }
            else
            {
                ShowMsgBox("Cheque ID is incorrect, please check again, Cheque ID length is 22 characters"); return;
            }
        }
        #endregion
        #region Properties
        protected void FirstLoad()
        {
            rcbCurrency.SelectedValue = "VND";
            LoadChequeStatus();
            this.rdpIssueDate.SelectedDate = DateTime.Now;
        }
        //private void LoadChequeStatus()
        //{
        //    rcbChequeStatus.Items.Clear();
        //    DataSet ds = TriTT.B_CHEQUEISSUE_Load_ChequeStatus();
        //    if (ds.Tables != null & ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow dr = ds.Tables[0].NewRow();
        //        dr["ID"] = "";
        //        dr["Description"] = "";
        //        ds.Tables[0].Rows.InsertAt(dr, 0);
        //    }
        //    rcbChequeStatus.DataSource = ds;
        //    rcbChequeStatus.DataValueField = "ID";
        //    rcbChequeStatus.DataTextField = "Description";
        //    rcbChequeStatus.DataBind();
        //}
        private void LoadChequeStatus()
        {
            rcbChequeStatus.Items.Clear();
            DataSet ds1 = TriTT.B_CHEQUEISSUE_Load_ChequeStatus();
            if (ds1.Tables != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds1.Tables[0].NewRow();
                dr["Code"] = "";
                dr["Description"] = "";
                ds1.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbChequeStatus.DataSource = ds1;
            rcbChequeStatus.DataTextField = "Description";
            rcbChequeStatus.DataValueField = "Code";
            rcbChequeStatus.DataBind();
        }
        private void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = !isauthorize;
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = true;
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
       
        #endregion

    }
}