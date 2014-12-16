using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class ProvisionTransfer_DC : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadToolBar();

            RadToolBar1.FindItemByValue("btSearch").Enabled = false;

            if (Request.QueryString["Codeid"] != null)
            {
                LoadData(Request.QueryString["Codeid"]);
                if (Request.QueryString["disable"] != null)
                {
                    hdfDisable.Value = Request.QueryString["disable"];
                    if (hdfDisable.Value == "1")
                    {
                        BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                        RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                    }
                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                }
            }
            else
            {
                FirstLoad();
            }
            //Session["DataKey"] = tbDepositCode.Text;
            //tbAddResmarks.ReLoadControl(tbDepositCode.Text);
            //tbAddResmarks.Width = "500";
        }

        #region Events
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "print" || commandName == "commit")
            {
                if (hdfDisable.Value == "0") return;

                bd.Database.ProvisionTransfer_DC_Insert(tbDepositCode.Text,tbLCNo.Text,rcbOrderedby.SelectedValue, "", rcbDebitAccount.Text,
                    rcbDebitCurrency.SelectedValue, tbDebitAmout.Text, rdpDebitDDate.SelectedDate.ToString(), rcbCreditAccount.Text, lblCreditCurrency.Text, "",
                    tbCreditAmount.Text, rdpCreditDate.SelectedDate.ToString(), "", rcbType.SelectedValue, rcbType.SelectedItem.Text, this.UserId.ToString(), txtAddRemarks1.Text, txtAddRemarks2.Text);
                
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                bd.Database.ProvisionTransfer_DC_UpdateStatus("UNA", tbDepositCode.Text, this.UserId.ToString());

                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                clearComboBox();
                FirstLoad();
            }
            else if (commandName == "Preview")
            {
                Response.Redirect(EditUrl("chitiet"));
            }
            else if (commandName == "authorize")
            {
                DataProvider.Database.ProvisionTransfer_DC_UpdateStatus("AUT", tbDepositCode.Text, this.UserId.ToString());
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;

                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                clearComboBox();
                FirstLoad();
            }
            else if (commandName == "reverse")
            {
                DataProvider.Database.ProvisionTransfer_DC_UpdateStatus("REV", tbDepositCode.Text, this.UserId.ToString());
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;

                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;

                BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                clearComboBox();
                FirstLoad();
            }
            else if (commandName == "search")
            {
                LoadData(tbDepositCode.Text);
            }
            hdfDisable.Value = "0";
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {

        }

        protected void tbLCNo_TextChanged(object sender, EventArgs e)
        {
            LoadDataChangeLCNo();

            SetDebitAcc();
        }

        protected void SetDebitAcc()
        {
            var dtDebitAcc = bd.SQLData.B_BFOREIGNEXCHANGE_GetByDebitAccount("", rcbDebitCurrency.SelectedValue, rcbOrderedby.SelectedValue, "provision_transfers");
            if (dtDebitAcc != null && dtDebitAcc.Rows.Count > 0)
            {
                rcbDebitAccount.Text = dtDebitAcc.Rows[0]["Id"].ToString();
                lblDebitAccountName.Text = dtDebitAcc.Rows[0]["Name"].ToString();
                hdDebitAccount_CustomerID.Text = dtDebitAcc.Rows[0]["CustomerID"].ToString();
                hdfCheckDebitAcc.Text = dtDebitAcc.Rows[0]["CustomerID"].ToString() + ";";
            }
        }

        protected void rcbType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadDataChangeLCNo();
        }
        #endregion

        #region methods
        private void clearComboBox()
        {
            rcbOrderedby.Items.Clear();
            rcbOrderedby.Text = "";
            txtAddRemarks1.Text = "";
            txtAddRemarks2.Text = "";
            //tbAddResmarks.SetTextDefault("");
            //rcbCreditAccount.Items.Clear();
            //rcbCreditAccount.Text = "";
            //rcbDebitAccount.Items.Clear();
            //rcbDebitAccount.Text = "";
        }
        private void FirstLoad()
        {
            DataSet ds = bd.DataTam.ProvisionTransfer_GetNewID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbDepositCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            }
            rdpDebitDDate.SelectedDate = DateTime.Now;
            rdpDebitDDate.Enabled = false;
            rdpCreditDate.SelectedDate = DateTime.Now;
            rdpCreditDate.Enabled = false;
            txtAddRemarks1.Text = "";
            txtAddRemarks2.Text = "";
            hdDebitAccount_CustomerID.Text = string.Empty;
        }
        private void LoadDataChangeLCNo()
        {
            clearComboBox();

            tbDebitAmout.Text = "";
            tbCreditAmount.Text = "";
            rcbDebitCurrency.SelectedValue = "";
            lblCreditCurrency.Text = "";

            DataSet ds = DataProvider.Database.ProvisionTransfer_DC_GetByLCNo(tbLCNo.Text, rcbType.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LoadComboByChange(rcbOrderedby, "ApplicantID", "ApplicantName", ds.Tables[0].Rows[0]["ApplicantID"].ToString(), ds.Tables[0].Rows[0]["ApplicantName"].ToString());
                rcbCreditAccount.Text = ds.Tables[0].Rows[0]["DepositCode"].ToString();
                lblCreditAccountName.Text = ds.Tables[0].Rows[0]["AccountName"].ToString();

                hdfCheckCreditAcc.Text = "";
                hdfCheckCreditAcc.Text = ds.Tables[0].Rows[0]["DepositCode"].ToString();
                rcbOrderedby.SelectedIndex = 1;

                DataSet dsDA = DataProvider.Database.B_BDRFROMACCOUNT_GetByCustomer(ds.Tables[0].Rows[0]["ApplicantName"].ToString(), ds.Tables[0].Rows[0]["Currentcy"].ToString());

                hdfCheckDebitAcc.Text = "";
                foreach (DataRow dr in dsDA.Tables[0].Rows)
                {
                    //hdfCheckDebitAcc.Text += dr["ID"] + ";";
                    hdfCheckDebitAcc.Text += dr["DR_CustomerID"] + ";";
                }

                rcbDebitCurrency.SelectedValue = ds.Tables[0].Rows[0]["Currency"].ToString();

                if(ds.Tables[0].Rows[0]["ProvisionNo"] != null
                   && ds.Tables[0].Rows[0]["ProvisionNo"].ToString() != ""  
                   && ds.Tables[0].Rows[0]["ProvisionNo"] != DBNull.Value)
                {
                    tbDepositCode.Text = ds.Tables[0].Rows[0]["ProvisionNo"].ToString();
                }

                lblCreditCurrency.Text = rcbDebitCurrency.SelectedValue;
                //Không cần load lên nữa cho tự nhập
                txtAmountCredited.Value = (double?)ds.Tables[0].Rows[0]["CreditAmount"];
                txtAmountDebited.Value = (double?)ds.Tables[0].Rows[0]["CreditAmount"];
            }
        }
        private void LoadData(string lccode)
        {
            DataSet ds = DataProvider.Database.ProvisionTransfer_DC_GetByNormalLCCode(lccode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                tbDepositCode.Text = lccode;
                tbLCNo.Text = ds.Tables[0].Rows[0]["LCNo"].ToString();
                rcbType.SelectedValue = ds.Tables[0].Rows[0]["Type"].ToString();

                DataSet dslcno = DataProvider.Database.ProvisionTransfer_DC_GetByLCNo(tbLCNo.Text, rcbType.SelectedValue);
                if (dslcno != null && dslcno.Tables.Count > 0 && dslcno.Tables[0].Rows.Count > 0)
                {
                    LoadComboByChange(rcbOrderedby, "ApplicantID", "ApplicantName", dslcno.Tables[0].Rows[0]["ApplicantID"].ToString(), dslcno.Tables[0].Rows[0]["ApplicantName"].ToString());
                    lblCreditAccountName.Text = dslcno.Tables[0].Rows[0]["AccountName"].ToString();
                }

                rcbOrderedby.SelectedIndex = 1;
                rcbCreditAccount.Text = ds.Tables[0].Rows[0]["CreditAccount"].ToString();
                rcbDebitAccount.Text = ds.Tables[0].Rows[0]["DebitAccount"].ToString();
                rcbDebitCurrency.SelectedValue = ds.Tables[0].Rows[0]["DebitCurrency"].ToString();
                SetDebitAcc();

                tbDebitAmout.Text = ds.Tables[0].Rows[0]["DebitAmout"].ToString();
                txtAmountDebited.Text = ((decimal)ds.Tables[0].Rows[0]["CreditAmount"] + (decimal)ds.Tables[0].Rows[0]["DebitAmout"]).ToString();
                rdpDebitDDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["DebitDate"].ToString());
                //rcbCreditAccount.SelectedValue = ds.Tables[0].Rows[0]["CreditAccount"].ToString();
                lblCreditCurrency.Text = ds.Tables[0].Rows[0]["CreditCurrency"].ToString();
                //tbTreasuryRate.Text = ds.Tables[0].Rows[0]["TreasuryRate"].ToString();
                tbCreditAmount.Text = ds.Tables[0].Rows[0]["DebitAmout"].ToString();
                txtAmountCredited.Text = ((decimal)ds.Tables[0].Rows[0]["CreditAmount"] + (decimal)ds.Tables[0].Rows[0]["DebitAmout"]).ToString();
                rdpCreditDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["CreditDate"].ToString());

                txtAddRemarks1.Text = ds.Tables[0].Rows[0]["AddRemarks1"].ToString();
                txtAddRemarks2.Text = ds.Tables[0].Rows[0]["AddRemarks2"].ToString();

                if (ds.Tables[0].Rows[0]["Status"].ToString() == "UNA" && Request.QueryString["disable"] != null)
                {
                    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;

                }
            }
        }
        private void LoadToolBar()
        {
            //RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
        }
        private bool CheckBeforeChangeLCNo()
        {
            if(tbLCNo.Text == "")
            {
                //RadWindowManager1.RadAlert("LC must have value!!!", 200, 100, "Message", "");
                tbLCNo.Focus();
                return false;
            }
            if (rcbType.SelectedValue == "")
            {
                //RadWindowManager1.RadAlert("Type must have value!!!", 200, 100, "Message", "");
                rcbType.Focus();
                return false;
            }
            return true;
        }
        private void LoadComboByChange(RadComboBox rcb, string id, string name, string idvalue, string namevalue)
        {
            DataSet dsc = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add(id);
            dt.Columns.Add(name);
            dt.Columns.Add("OrginalName");

            DataRow dr = dt.NewRow();
            dr[id] = idvalue;
            dr[name] = idvalue + " - " + namevalue;
            dr["OrginalName"] = namevalue;
            dt.Rows.Add(dr);
            dsc.Tables.Add(dt);

            bc.Commont.initRadComboBox(ref rcb, name, id, dsc);
        }
        #endregion

        protected void btnPCK_Report_Click(object sender, EventArgs e)
        {
            bc.Reports.createFileDownload(Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/ProvisionTransfersPHIEUCHUYENKHOAN.doc"), bd.SQLData.B_PROVISIONTRANSFER_DC_PHIEUCHUYENKHOAN_REPORT(tbLCNo.Text.Trim(), UserInfo.Username),
                "ProvisionTransfersPHIEUCHUYENKHOAN_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInApplication, Response);
        }

        protected void rcbOrderedby_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ApplicantID"] = row["ApplicantID"].ToString();
            e.Item.Attributes["ApplicantName"] = row["OrginalName"].ToString();
        }

        protected void rcbDebitAccount_OnTextChanged(object sender, EventArgs e)
        {
            SetDebitAcc();
        }
    }
}