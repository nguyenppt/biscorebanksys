using System;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.FTTeller
{
    public partial class InwardCashWithdraw : PortalModuleBase
    {
        private static int Id = 480;
        
        #region events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            //var dsWiftCode = SQLData.B_BSWIFTCODE_GetAll();

            //rcbCreditAccount.DataValueField = "AccountNo";
            //rcbCreditAccount.DataTextField = "AccountNoDescription";
            //rcbCreditAccount.DataSource = dsWiftCode;
            //rcbCreditAccount.DataBind();

            if (Request.QueryString["IsAuthorize"] != null)
            {
                LoadToolBar(true);
                loaddataPreview();
                //dvAudit.Visible = true;
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
            }
            else 
            {
                LoadToolBar(false);
                FirstLoad();
                //dvAudit.Visible = false;
            }

        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //if (hdfDisable.Value == "0") return;
            
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                FirstLoad();
            }

            if(commandName=="Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }

            if (commandName == "authorize" || commandName == "reverse")
            {
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                FirstLoad();
                LoadToolBar(false);
            }
        }
        protected void rcbClearingID_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcbClearingID.SelectedValue)
            {
                case "1":
                    lbDebitCurrency.Text = "VND";
                    lbDebitAccount.Text = "03.000168655.7";
                    lbDebitAmtLCY.Text = "9,000,000";

                    rcbCreditCurrency.SelectedValue = "VND";
                    lbCrAmtLCY.Text = "9,000,000";

                    txtBOName.Text = "Đỗ Bảo Lộc";
                    txtFOName.SetTextDefault("Nguyễn Văn Trung");
                    txtNarrative.Text = "TRA VON LAI VANG HANG THANG";

                    txtIdentityCard.Text = "192522003";
                    txtIsssueDate.SelectedDate = new DateTime(2000, 5, 10);
                    txtIsssuePlace.Text = "TP.HCM";
                    txtTel.Text = "09033220011";
                    break;

                case "2":
                    lbDebitCurrency.Text = "VND";
                    lbDebitAccount.Text = "03.000259755.9";
                    lbDebitAmtLCY.Text = "10,000,000";

                    rcbCreditCurrency.SelectedValue = "VND";
                    lbCrAmtLCY.Text = "10,000,000";

                    txtBOName.Text = "Trẩn Bửu Thạch";
                    txtFOName.SetTextDefault("Tô Văn Hoa");
                    txtNarrative.Text = "TRA VON LAI VANG HANG THANG";

                    txtIdentityCard.Text = "045231544";
                    txtIsssueDate.SelectedDate = new DateTime(2005, 6, 19);
                    txtIsssuePlace.Text = "TP.HCM";
                    txtTel.Text = "09333778789";
                    break;

                case "3":
                    lbDebitCurrency.Text = "VND";
                    lbDebitAccount.Text = "03.000699787.4";
                    lbDebitAmtLCY.Text = "15,000,000";

                    rcbCreditCurrency.SelectedValue = "VND";
                    lbCrAmtLCY.Text = "15,000,000";

                    txtBOName.Text = "Quang Nhật Tường Quyên";
                    txtFOName.SetTextDefault("Lưu Văn Trí");
                    txtNarrative.Text = "TRA VON LAI VANG HANG THANG";

                    txtIdentityCard.Text = "029002154";
                    txtIsssueDate.SelectedDate = new DateTime(2000, 5, 10);
                    txtIsssuePlace.Text = "TP.HCM";
                    txtTel.Text = "09815457157";
                    break;
            }
        }
        #endregion

        #region method
        void loaddataPreview()
        {
            if (Request.QueryString["LCCode"] != null)
            {
                string LCCode = Request.QueryString["LCCode"].ToString();
                switch (LCCode)
                {
                    case "1":
                        this.txtId.Text = "TT/09164/00481";
                        //txtBOName.Text = "Đỗ Bảo Lộc";
                        //txtFOName.SetTextDefault("Nguyễn Văn Trung");

                        //txtNarrative.Text = "TRA VON LAI VANG HANG THANG";
                        rcbCreditAccount.SelectedValue = "VND";

                        rcbClearingID.SelectedValue = "1";
                        rcbClearingID_OnSelectedIndexChanged(null,null);

                        //lbDebitAmtLCY.Text = "40,000,000";
                        //lbCrAmtLCY.Text = "40,000,000";

                        txtIdentityCard.Text = "025984158";
                        txtIsssueDate.SelectedDate = new DateTime(2001, 1, 2);
                        txtIsssuePlace.Text = "TP.HCM";
                        txtTel.Text = "0909256879";
                        break;

                    case "2":
                        this.txtId.Text = "TT/09164/00482";
                        //txtBOName.Text = "Trẩn Bửu Thạch";
                        //txtFOName.SetTextDefault("Tô Văn Hoa");

                        rcbCreditAccount.SelectedValue = "VND";
                        //txtNarrative.Text = "TRA VON LAI VANG HANG THANG";

                        rcbClearingID.SelectedValue = "2";
                        rcbClearingID_OnSelectedIndexChanged(null,null);

                        //lbDebitAmtLCY.Text = "5,000,000";
                        //lbDebitAmtLCY.Text = "5,000,000";
                       
                        txtIdentityCard.Text = "362584157";
                        txtIsssueDate.SelectedDate = new DateTime(1999, 5, 6);
                        txtIsssuePlace.Text = "TP.HCM";
                        txtTel.Text = "01222586488";
                        break;

                    case "3":
                        this.txtId.Text = "TT/09164/00483";
                        //txtReceivingName.SetTextDefault("Lý Thánh Tông");
                        //txtSendingName.Text = "Trần Minh Tâm";
                        //txtSendingAddress.SetTextDefault("632 Trần Hưng Đạo");

                        rcbCreditAccount.SelectedValue = "VND";
                        rcbClearingID.SelectedValue = "3";
                        rcbClearingID_OnSelectedIndexChanged(null,null);
                        //txtNarrative.Text = "TRA VON LAI VANG HANG THANG";

                        txtIdentityCard.Text = "025639587";
                        txtIsssueDate.SelectedDate = new DateTime(1990, 7, 21);
                        txtIsssuePlace.Text = "TP.HCM";
                        txtTel.Text = "0988365487";
                        break;
                }
            }
        }
        private void FirstLoad()
        {
            Id++;
            txtFOName.SetTextDefault("");
            this.txtId.Text = "TT/09164/00" + Id.ToString();
        }
        private void LoadToolBar(bool isauthorise)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btPreview").Enabled = !isauthorise;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btReverse").Enabled = isauthorise;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        #endregion
    }
}