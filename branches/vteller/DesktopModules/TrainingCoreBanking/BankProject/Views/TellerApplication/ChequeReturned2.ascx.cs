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
    public partial class ChequeReturned2 : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["ChequeID"] != null) // link tu ChequeReturn qua
                {
                    LoadChequeData(Request.QueryString["ChequeID"].ToString());
                }
            else if (Request.QueryString["RefCheque"] != null)
            {
                LoadChequeData_2Aut(Request.QueryString["RefCheque"].ToString(), Convert.ToDouble(Request.QueryString["ReturnedCheque"].ToString()));
            } else
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                    RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                }
        }
        protected void RadToolBar1_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBar = e.Item as RadToolBarButton;
            var commandName = ToolBar.CommandName;
            switch (commandName)
            {
                case "commit":
                    DataSet dss = TriTT.B_CHEQUE_RETURN_check_cheque_in_used(tbID.Text.Trim().Substring(0,2), tbReturnedCheque.Value.HasValue ? tbReturnedCheque.Value.Value : 0);
                    if (dss.Tables != null && dss.Tables[0].Rows.Count > 0) // check xem cheque nay da duoc su dung chua, dc su dung roi thi  k can` return
                    {
                        if (dss.Tables[0].Rows[0]["InUsed"].ToString() == "NO")
                        {
                            string WorkingAcct2 = tbID.Text.Trim().Substring(3, 14);
                            string[] ChequeNoNum = tbChequeNos.Text.Split('-');
                            if (tbReturnedCheque.Value.Value >= Convert.ToDouble(ChequeNoNum[0].Trim()) && tbReturnedCheque.Value.Value < Convert.ToDouble(ChequeNoNum[1].Trim()))
                            {
                                TriTT.B_CHEQUE_RETURN_Insert_Update(tbID.Text.Trim(), "UNA",WorkingAcct2, tbTotalIssued.Value.Value, tbTotalUsed.Value.Value, tbTotalHeld.Value.Value
                                , tbChequeNos.Text, Convert.ToDouble(ChequeNoNum[0].Trim()), Convert.ToDouble(ChequeNoNum[1].Trim()), null, tbStoppedCheque.Value.Value
                                , tbReturnedCheque.Value.Value, lblNarrative.Text, UserInfo.Username.ToString());
                                Response.Redirect("Default.aspx?tabid=136");
                            }
                            else { ShowMsgBox("The Inputed Returned Cheque is incorrect , please check again !"); return; }
                        }
                        else
                        {
                            ShowMsgBox("The Inputed Returned Cheque was used, It is not valid in this action !"); return;
                        }
                    }
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("ChequeReturned_PL"));
                    break;
                case "reverse":
                    LoadToolBar(true);
                    TriTT.B_CHEQUE_RETURN_Update_Status(tbID.Text, tbReturnedCheque.Value.Value , "REV");
                    break;
                case "authorize":
                    TriTT.B_CHEQUE_RETURN_Update_Status(tbID.Text, tbReturnedCheque.Value.Value, "AUT");
                    Response.Redirect("Default.aspx?tabid=136");
                    break;
            }
        }

        protected void LoadChequeData(string chequeID)
        {
            var WorkingAcct = chequeID.Substring(3, 14);
            DataSet ds = TriTT.B_CHEQUE_RETURN_LoadCheque_Data(chequeID, WorkingAcct);
            tbID.Text = chequeID.Substring(0, 17);
            tbTotalIssued.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
            tbChequeNos.Text = ds.Tables[0].Rows[0]["ChequeNos"].ToString();
            DataSet dsUsed = TriTT.B_CHEQUE_RETURN_LoadCheque_Totalused(chequeID, WorkingAcct);
            if (dsUsed.Tables != null && dsUsed.Tables[0].Rows.Count > 0)
            {
                tbTotalUsed.Text = dsUsed.Tables[0].Rows[0]["TotalUsed"].ToString();
            }
           
            DataSet dsCancle = TriTT.B_CHEQUE_RETURN_LoadCheque_TotalCancle(chequeID, WorkingAcct);// cheque cancle duoc lay tu cheque stoppped
            var TotalCancle = 0;
            if (dsCancle.Tables != null && dsCancle.Tables[0].Rows.Count > 0)
            {
                TotalCancle = Convert.ToInt16(dsCancle.Tables[0].Rows[0]["TotalCancle"].ToString());
            }

            DataSet dsStopped = TriTT.B_CHEQUE_RETURN_LoadCheque_TotalStopped(chequeID, WorkingAcct);
            if (dsStopped.Tables != null && dsStopped.Tables[0].Rows.Count > 0)
                tbStoppedCheque.Text = (Convert.ToDouble(dsStopped.Tables[0].Rows[0]["NoOfLeave"].ToString()) - TotalCancle).ToString();
            else tbStoppedCheque.Text = (0 - TotalCancle).ToString();
            tbTotalHeld.Text = (tbTotalIssued.Value.Value - tbTotalUsed.Value.Value - tbStoppedCheque.Value.Value).ToString();
            LoadToolBar(true);
        }
        protected void LoadChequeData_2Aut(string RefCheque, double ReturnedCheque)
        {
            DataSet ds = TriTT.B_CHEQUE_RETURN_Preview_List(RefCheque, ReturnedCheque);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            { 
                DataRow dr = ds.Tables[0].Rows[0];
                tbID.Text = dr["RefCheque"].ToString();
                tbID.Enabled = false;
                tbTotalIssued.Text = dr["TotalIssued"].ToString();
                tbTotalUsed.Text = dr["TotalUsed"].ToString();
                tbTotalHeld.Text = dr["TotalHeld"].ToString();
                tbChequeNos.Text = dr["ChequeNos"].ToString();
                tbStoppedCheque.Text = dr["TotalStopped"].ToString();
                tbReturnedCheque.Text = dr["ReturnedCheque"].ToString();
                LoadToolBar(false);
                if (dr["Status"].ToString() == "AUT")
                {
                    BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                    LoadToolBar_AllFalse();
                }
            }
        }
        protected void LoadToolBar(bool isauthorize)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
       
        
    }
}