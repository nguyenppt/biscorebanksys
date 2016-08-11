using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankProject.Controls;
using BankProject.DataProvider;
using Telerik.Web.UI;

namespace BankProject.TradingFinance
{
    public partial class IssueFreeFormatMessage : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public string CableType = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
            InitToolBar(false);
            SetDefaultNarrative();

            txtEdittor_Narrative.EditModes = EditModes.Design;
            txtEdittor_Narrative.Modules.Clear();

            if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
            {
                hiddenId.Value = Request.QueryString["CodeID"];
                LoadData();
            }
            
            if (!string.IsNullOrEmpty(Request.QueryString["disable"]))
            {
                InitToolBar(true);
                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
                
            }
        }


        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "save":
                    if (CheckTransactionReferenceNumber() == false)
                    {
                        ShowMsgBox("Transaction Reference Number is not found");
                        return;
                    }
                    SaveData();

                    // Active control
                    SetDisableByReview(true);

                    // Reset Data
                    LoadData();

                    //  InitToolBar
                    InitToolBar(false);

                    hiddenId.Value = string.Empty;
                    break;

                case "review":
                    Response.Redirect(EditUrl("freeformatmessage_preview"));
                    break;

                case "authorize":
                    UpdateStatus("AUT");

                    hiddenId.Value = string.Empty;
                    // Active control
                    SetDisableByReview(true);

                    // Reset Data
                    LoadData();

                    //  InitToolBar
                    InitToolBar(false);
                    RadToolBar1.FindItemByValue("btSave").Enabled = true;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    SetDefaultNarrative();
                    break;

                case "revert":
                    UpdateStatus("REV");

                    // Active control
                    SetDisableByReview(true);

                    // ko cho Authorize/Preview
                    InitToolBar(false);
                    RadToolBar1.FindItemByValue("btSave").Enabled = true;
                    RadToolBar1.FindItemByValue("btReview").Enabled = false;
                    SetDefaultNarrative();
                    break;
            }
        }

        protected void txtTFNo_OnTextChanged(object sender, EventArgs e)
        {
            CheckExistTfNo();
        }

        protected void txtReviver_OnTextChanged(object sender, EventArgs e)
        {
            CheckSwiftCodeExist();
        }

        protected void CheckExistTfNo()
        {
            lblTFNoError.Text = "";
            if (!string.IsNullOrEmpty(txtTFNo.Text.Trim()))
            {
                if (CheckTransactionReferenceNumber() == false)
                {
                    lblTFNoError.Text = "No found";
                }
            }
        }

        protected bool CheckTransactionReferenceNumber()
        {
            return SQLData.B_TRANSACTIONREFERENCENUMBER_CheckByType(txtTFNo.Text.Trim(), comboWaiveCharges.SelectedValue).Rows.Count > 0;
        }
        
        protected void CheckSwiftCodeExist()
        {
            lblReviverError.Text = "";
            lblReviverCode.Text = "";
            if (!string.IsNullOrEmpty(txtReviver.Text.Trim()))
            {
                var dtBSWIFTCODE = SQLData.B_BSWIFTCODE_GetByCode(txtReviver.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    lblReviverCode.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                    txtReviver.Text = dtBSWIFTCODE.Rows[0]["SwiftCode"].ToString();
                }
                else
                {
                    lblReviverError.Text = "No found swiftcode";
                }
            }
        }

        protected void LoadData()
        {
            var dtSource = SQLData.B_BFREETEXTMESSAGE_GetById(hiddenId.Value);

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btReview").Enabled = true;

            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                RadToolBar1.FindItemByValue("btReview").Enabled = false;

                CableType = dtSource.Rows[0]["CableType"].ToString();

                comboWaiveCharges.SelectedValue = dtSource.Rows[0]["WaiveCharge"].ToString();
                txtTFNo.Text = dtSource.Rows[0]["TFNo"].ToString();
                comboCableType.SelectedValue = dtSource.Rows[0]["CableType"].ToString();
                txtReviver.Text = dtSource.Rows[0]["ReviverCode"].ToString();
                lblReviverCode.Text = dtSource.Rows[0]["ReviverDesc"].ToString(); 
                txtRelatedReference.Text = dtSource.Rows[0]["RelatedReference"].ToString();
                txtEdittor_Narrative.Content = dtSource.Rows[0]["Narrative"].ToString();
            }
            else
            {
                Commont.SetEmptyFormControls(this.Controls);
            }
        }

        protected void SaveData()
        {
            SQLData.B_BFREETEXTMESSAGE_Insert(hiddenId.Value, comboWaiveCharges.SelectedValue, txtTFNo.Text.Trim(),
                                              comboCableType.SelectedValue, txtReviver.Text.Trim(), lblReviverCode.Text,
                                              txtRelatedReference.Text.Trim(), txtEdittor_Narrative.Content, UserId);
            SetDefaultNarrative();
        }

        protected void UpdateStatus(string status)
        {
            SQLData.B_BFREETEXTMESSAGE_UpdateSatus(hiddenId.Value, status, UserId);
        }

        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btRevert").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;

            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
        }

        protected void SetVisibilityByStatus(ref DataRow drow)
        {
            if (drow == null)
            {
                return;
            }

            if (drow["Status"].ToString() == "AUT")
            {
                InitToolBar(false);
                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
            }
        }

        protected void SetDefaultNarrative()
        {
            txtEdittor_Narrative.Content = "";
        }

        protected void btnIssueFreeFormatMessage_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/IssueFreeFormatMessage.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.B_BFREETEXTMESSAGE_Report(hiddenId.Value);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("IssueFreeFormatMessage_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf", Aspose.Words.SaveFormat.Pdf, Aspose.Words.SaveType.OpenInApplication, Response);
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
    }
}