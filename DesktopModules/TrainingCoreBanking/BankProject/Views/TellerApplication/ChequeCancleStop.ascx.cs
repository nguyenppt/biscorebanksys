using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    public partial class ChequeCancleStop : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public static int autoID = 2;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (IsPostBack) return;
                rdpActivityDate.SelectedDate = DateTime.Now;
                LoadChequeType();
                if (Request.QueryString["AccountID"] != null)
                {
                    LoadDataPreview(Request.QueryString["AccountID"].ToString(),Convert.ToDouble(Request.QueryString["SerialNo"].ToString()));
                }
                else
                {
                    LoadToolBar(true);
                }
        }
        
        
        protected void RadToolBar_OnButtonClick(object sender, RadToolBarEventArgs e)
        {
            var ToolBarButton = e.Item as RadToolBarButton;
            var CommandName = ToolBarButton.CommandName;
            switch (CommandName)
            { 
                case "commit":
                    if (TriTT.CHEQUE_CANCLE_STOP_check_exists(tbID.Text.Trim()).Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = TriTT.CHEQUE_CANCLE_STOP_check_exists(tbID.Text.Trim()).Tables[0].Rows[0];
                        if (tbSerialNo.Value.Value >= Convert.ToDouble(dr["FromChequeSerial"].ToString()) && tbSerialNo.Value.Value <= Convert.ToDouble(dr["ToChequeSerial"].ToString()))
                        {
                            if (rcbChequeType.SelectedValue == dr["ChequeType"].ToString())
                            {
                                TriTT.CHEQUE_CANCLE_STOP_Insert_Update(tbID.Text, "UNA", tbSerialNo.Value.HasValue? tbSerialNo.Value.Value : 0, rcbChequeType.SelectedValue
                                    ,rcbChequeType.Text , rdpActivityDate.SelectedDate.HasValue? rdpActivityDate.SelectedDate : null, UserInfo.Username.ToString());
                                Response.Redirect("Default.aspx?tabid=135");
                            }
                            else { ShowMsgBox("Cheque type is incorrect, Please check again !"); return; }
                        }
                        else { ShowMsgBox("Your inputed Cheque Serials are in normal status, they do not need this action !"); return; }
                    }
                    else { ShowMsgBox("Your Working Account does not exist or Cheques are not authorized, Please check again !"); return; }
                    break;
                case "Preview":
                    Response.Redirect(EditUrl("ChequeCancleStop_PL"));
                    break;
                case "reverse":
                    TriTT.CHEQUE_CANCLE_STOP_Update_Status(tbID.Text, tbSerialNo.Value.HasValue ? tbSerialNo.Value.Value : 0, "REV");
                    LoadToolBar(true);
                    break;
                case "authorize":
                    TriTT.CHEQUE_CANCLE_STOP_Update_Status(tbID.Text, tbSerialNo.Value.HasValue ? tbSerialNo.Value.Value : 0, "AUT");
                    Response.Redirect("Default.aspx?tabid=135");
                    break;
            }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            //LoadCustomer_Info(tbID.Text.Trim());
        }
        protected void LoadDataPreview(string AccountID, double SerialNo)
        {
            DataRow dr = TriTT.CHEQUE_CANCLE_STOP_LoadData(AccountID, SerialNo);
            LoadChequeType();
            tbID.Text = dr["AccountID"].ToString();
            tbSerialNo.Text = dr["SerialNo"].ToString();
            rcbChequeType.SelectedValue = dr["ChequeType"].ToString();
            if (dr["ActiveDate"].ToString() != "")
            {
                rdpActivityDate.SelectedDate = Convert.ToDateTime(dr["ActiveDate"].ToString());
            }
            if (dr["Status"].ToString() == "AUT")
            {
                LoadToolBar_AllFalse();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                return;
            }
            LoadToolBar(false);
        }
        protected void LoadChequeType()
        {
            rcbChequeType.Items.Clear();
            rcbChequeType.Items.Add(new RadComboBoxItem("", ""));
            rcbChequeType.DataSource = TriTT.CHEQUE_WITHDRAWAL_LoadChequeType();
            rcbChequeType.DataTextField = "Description";
            rcbChequeType.DataValueField = "Code";
            rcbChequeType.DataBind();
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        protected void LoadToolBar(bool isauthorize)
        {
            RadToolBar.FindItemByValue("btCommitData").Enabled = isauthorize;
            RadToolBar.FindItemByValue("btPreview").Enabled = isauthorize;
            RadToolBar.FindItemByValue("btAuthorize").Enabled = !isauthorize;
            RadToolBar.FindItemByValue("btReverse").Enabled = !isauthorize;
            RadToolBar.FindItemByValue("btSearch").Enabled = false;
            RadToolBar.FindItemByValue("btPrint").Enabled = false;
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar.FindItemByValue("btPreview").Enabled = false;
            RadToolBar.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar.FindItemByValue("btReverse").Enabled = false;
            RadToolBar.FindItemByValue("btSearch").Enabled = false;
            RadToolBar.FindItemByValue("btPrint").Enabled = false;
        }
    }
}