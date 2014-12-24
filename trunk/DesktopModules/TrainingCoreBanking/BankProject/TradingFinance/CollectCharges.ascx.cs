using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using Telerik.Web.UI;
using System.Data;
using BankProject.DBContext;
using BankProject.Helper;
using System.Data.Entity.Validation;

namespace BankProject.TradingFinance
{
    public partial class CollectCharges : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        VietVictoryCoreBankingEntities db = new VietVictoryCoreBankingEntities();
        protected string ChargeAcctMessage = "Can not find this acc.";
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            bc.Commont.initRadComboBox(ref cboAccountOfficer, "Description", "Code", bd.SQLData.B_BACCOUNTOFFICER_GetAll());
            txtCode.Text = Request.QueryString["tid"];
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                if (cc == null)
                {
                    lblError.Text = "TransCode not found !";
                    return;
                }
                loadTransDetail(cc);
                bc.Commont.SetTatusFormControls(this.Controls, false);
                divCmdChargeType.Visible = false;
                divCmdChargeType1.Visible = false;
                divCmdChargeType2.Visible = false;
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                {
                    if (cc.Status.Equals(bd.TransactionStatus.UNA))
                    {
                        //Duyet
                        RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                        return;
                    }
                }

                return;
            }
            //
            txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("CollectCharges", "FT", ".");
            txtVATNo.Text = getVATNo();
        }
        private string getVATNo()
        {
            DataSet ds = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0]["SoTT"].ToString();

            return "0";
        }
        private void DisableDefaultControl()
        {
            txtVATNo.Enabled = false;
        }
        private void loadTransDetail(B_CollectCharges cc)
        {
            txtChargeAcct.Text = cc.ChargeAcct;
            lblChargeAcctName.Text = cc.ChargeAcctName;
            lblChargeCurrency.Text = cc.ChargeCurrency;
            cboTransactionType_ChargeCommission.SelectedValue = cc.TransactionType;
            loadChargeType();
            cboChargeType.SelectedValue = cc.ChargeType1;
            txtChargeAmount.Value = cc.ChargeAmount1;
            divChargeType1.Attributes.CssStyle.Remove("Display");
            if (!string.IsNullOrEmpty(cc.ChargeType2))
            {
                cboChargeType1.SelectedValue = cc.ChargeType2;
                txtChargeAmount1.Value = cc.ChargeAmount2;
            }
            else divChargeType1.Attributes.CssStyle.Add("Display", "none");
            divChargeType2.Attributes.CssStyle.Remove("Display");
            if (!string.IsNullOrEmpty(cc.ChargeType3))
            {
                cboChargeType2.SelectedValue = cc.ChargeType3;
                txtChargeAmount2.Value = cc.ChargeAmount3;
            }
            else divChargeType2.Attributes.CssStyle.Add("Display", "none");
            cboChargeFor.SelectedValue = cc.ChargeFor;
            txtVATNo.Text = cc.VATNo;
            txtAddRemarks_Charges1.Text = cc.AddRemarks1;
            txtAddRemarks_Charges2.Text = cc.AddRemarks2;
            cboAccountOfficer.SelectedValue = cc.AccountOfficer;
            if (cc.TotalChargeAmount.HasValue)
                lblTotalChargeAmount.Text = String.Format("{0:C}", cc.TotalChargeAmount.Value).Replace("$", "");
            if (cc.TotalTaxAmount.HasValue)
                lblTotalTaxAmount.Text = String.Format("{0:C}", cc.TotalTaxAmount.Value).Replace("$", "");
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            B_CollectCharges cc;
            switch (commandName)
            {
                case bc.Commands.Commit:                    
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    bool isUpdate = (cc != null);
                    if (!isUpdate)
                    {
                        cc = new B_CollectCharges();
                        cc.TransCode = txtCode.Text;
                        cc.Status = bd.TransactionStatus.UNA;                        
                        cc.UserCreate = this.UserInfo.Username;
                        cc.DateTimeCreate = DateTime.Now;
                    }
                    else
                    {
                        cc.Status = bd.TransactionStatus.UNA;
                        cc.UserUpdate = this.UserInfo.Username;
                        cc.DateTimeUpdate = DateTime.Now;
                    }
                    cc.ChargeAcct = txtChargeAcct.Text;
                    cc.ChargeAcctName = lblChargeAcctName.Text;
                    cc.ChargeCurrency = lblChargeCurrency.Text;
                    cc.TransactionType = cboTransactionType_ChargeCommission.SelectedValue;
                    cc.ChargeType1 = cboChargeType.SelectedValue;
                    cc.ChargeAmount1 = txtChargeAmount.Value;
                    cc.ChargeType2 = cboChargeType1.SelectedValue;
                    cc.ChargeAmount2 = txtChargeAmount1.Value;
                    cc.ChargeType3 = cboChargeType2.SelectedValue;
                    cc.ChargeAmount3 = txtChargeAmount2.Value;
                    cc.ChargeFor = cboChargeFor.SelectedValue;
                    cc.VATNo = txtVATNo.Text;
                    cc.AddRemarks1 = txtAddRemarks_Charges1.Text;
                    cc.AddRemarks2 = txtAddRemarks_Charges2.Text;
                    cc.AccountOfficer = cboAccountOfficer.SelectedValue;
                    if (!string.IsNullOrEmpty(lblTotalChargeAmount.Text))
                        cc.TotalChargeAmount = Convert.ToDouble(lblTotalChargeAmount.Text);
                    else
                        cc.TotalChargeAmount = null;
                    if (!string.IsNullOrEmpty(lblTotalTaxAmount.Text))
                        cc.TotalTaxAmount = Convert.ToDouble(lblTotalTaxAmount.Text);
                    else
                        cc.TotalTaxAmount = null;
                    if (!isUpdate) db.B_CollectCharges.Add(cc);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                System.Diagnostics.Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                                    validationErrors.Entry.Entity.GetType().FullName,
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                            }
                        }

                        throw;  // You can also choose to handle the exception here...
                    }
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Authorize:
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    if (cc == null)
                    {
                        lblError.Text = "TransCode not found !";
                        return;
                    }
                    cc.Status = bd.TransactionStatus.AUT;
                    cc.UserApprove = this.UserInfo.Username;
                    cc.DateTimeApprove = DateTime.Now;

                    db.SaveChanges();

                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
                case bc.Commands.Reverse:
                    cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                    if (cc == null)
                    {
                        lblError.Text = "TransCode not found !";
                        return;
                    }
                    cc.Status = bd.TransactionStatus.REV;
                    cc.UserUpdate = this.UserInfo.Username;
                    cc.DateTimeUpdate = DateTime.Now;

                    db.SaveChanges();
                    Response.Redirect("Default.aspx?tabid=" + this.TabId);
                    break;
            }
        }

        protected void btnLoadCodeInfo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                lblError.Text = "Please enter TransCode !";
                return;
            }
            B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
            if (cc == null)
            {
                lblError.Text = "TransCode not found !";
                return;
            }
            loadTransDetail(cc);
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            if (cc.Status.Equals(bd.TransactionStatus.AUT))
            {
                divCmdChargeType.Visible = false;
                divCmdChargeType1.Visible = false;
                divCmdChargeType2.Visible = false;
                //Preview
                bc.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btCommit").Enabled = false;
                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                return;
            }
            //Cho phep edit
            divCmdChargeType.Visible = true;
            divCmdChargeType1.Visible = true;
            divCmdChargeType2.Visible = true;
            bc.Commont.SetTatusFormControls(this.Controls, true);
            RadToolBar1.FindItemByValue("btCommit").Enabled = true;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            //RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            DisableDefaultControl();
        }

        private void loadChargeType()
        {
            cboChargeType.Items.Clear();
            cboChargeType1.Items.Clear();
            cboChargeType2.Items.Clear();
            //
            string TransType = cboTransactionType_ChargeCommission.SelectedValue;
            if (string.IsNullOrEmpty(TransType)) return;
            //
            DataTable dtList = bd.Database.B_BCHARGECODE_ByTransType(TransType);
            if (dtList == null || dtList.Rows.Count <= 0) return;

            bc.Commont.initRadComboBox(ref cboChargeType, "Code", "Code", dtList);
            bc.Commont.initRadComboBox(ref cboChargeType1, "Code", "Code", dtList);
            bc.Commont.initRadComboBox(ref cboChargeType2, "Code", "Code", dtList);
        }
        protected void cboTransactionType_ChargeCommission_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            loadChargeType();
        }

        protected void txtChargeAcct_OnTextChanged(object sender, EventArgs e)
        {
            lblChargeCurrency.Text = "";
            lblChargeAcctName.Text = "";
            if (!String.IsNullOrEmpty(txtChargeAcct.Text))
            {
                var cc = db.BDRFROMACCOUNTs.Where(p => p.Id.Equals(txtChargeAcct.Text)).FirstOrDefault();
                if (cc == null)
                    lblChargeAcctName.Text = ChargeAcctMessage;
                else
                {
                    lblChargeCurrency.Text = cc.Currency;
                    lblChargeAcctName.Text = cc.Name;
                }
            }
        }

        private void calculateChargeTotalAmount()
        {
            var totalAmount = 0.0;
            if (!string.IsNullOrEmpty(cboChargeType.SelectedValue) && txtChargeAmount.Value.HasValue) totalAmount += txtChargeAmount.Value.Value;
            if (!string.IsNullOrEmpty(cboChargeType1.SelectedValue) && txtChargeAmount1.Value.HasValue) totalAmount += txtChargeAmount1.Value.Value;
            if (!string.IsNullOrEmpty(cboChargeType2.SelectedValue) && txtChargeAmount2.Value.HasValue) totalAmount += txtChargeAmount2.Value.Value;
            //
            lblTotalChargeAmount.Text = "";
            if (totalAmount > 0) lblTotalChargeAmount.Text = String.Format("{0:C}", totalAmount).Replace("$", "");
            lblTotalTaxAmount.Text = "";
            if (totalAmount > 0 && !string.IsNullOrEmpty(cboChargeFor.SelectedValue))
            {
                switch (cboChargeFor.SelectedValue)
                {
                    case "A":
                    case "B":
                        lblTotalTaxAmount.Text = String.Format("{0:C}", totalAmount * 0.1).Replace("$", "");
                        break;
                    default:
                        //txtTaxAmt.Text = String.Format("{0:C}", txtChargeAmt.Value.Value).Replace("$", "");
                        break;
                }
            }
        }
        protected void txtChargeAmount_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void txtChargeAmount1_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void txtChargeAmount2_OnTextChanged(object sender, EventArgs e)
        {
            calculateChargeTotalAmount();
        }
        protected void cboChargeFor_ChargeCommission_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            calculateChargeTotalAmount();
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/CollectCharges/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                B_CollectCharges cc = db.B_CollectCharges.Where(p => p.TransCode.Equals(txtCode.Text)).FirstOrDefault();
                if (cc == null)
                {
                    lblError.Text = "TransCode not found !";
                    return;
                }

                //reportData = bd.IssueLC.ImportLCPaymentReport(reportType, Convert.ToInt64(1), this.UserInfo.Username);
                switch (reportType)
                {
                    case 1://VAT
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VAT.doc");
                        reportSaveName = "VAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var cc2 = db.B_CollectCharges.Where(p => p.TransCode.Equals(cc.TransCode)).FirstOrDefault();
                        if (cc2 == null)
                        {
                            lblError.Text = "TransCode not found !";
                            return;
                        }
                        var VAT = new Model.CollectCharges.Reports.VAT()
                        {
                            VATNo = cc2.VATNo, TransCode = cc2.TransCode, UserName = cc.UserCreate, ChargeAcct = cc2.ChargeAcct, ChargeRemarks = cc2.AddRemarks1 + " " + cc2.AddRemarks2, 
                            ChargeType1 = getChargeTypeInfo(cc2.ChargeType1, 1), ChargeAmount1 = (!String.IsNullOrEmpty(cc2.ChargeType1)?cc2.ChargeAmount1:null), 
                            ChargeType2 = getChargeTypeInfo(cc2.ChargeType2, 1), ChargeAmount2 = (!String.IsNullOrEmpty(cc2.ChargeType2)?cc2.ChargeAmount2:null), 
                            ChargeType3 = getChargeTypeInfo(cc2.ChargeType3, 1), ChargeAmount3 = (!String.IsNullOrEmpty(cc2.ChargeType2)?cc2.ChargeAmount3:null), 
                            TotalTaxAmount = cc2.TotalTaxAmount, TotalChargeAmount = cc2.TotalChargeAmount, ChargeCurrency = cc2.ChargeCurrency
                        };
                        if (!String.IsNullOrEmpty(cc2.ChargeType1) && cc2.ChargeAmount1.HasValue && cc2.ChargeAmount1 != 0)
                        {
                            VAT.ChargeType1 = getChargeTypeInfo(cc2.ChargeType1, 1);
                            VAT.ChargeAmount1Text = cc2.ChargeAmount1.Value + cc2.ChargeCurrency + " " + getChargeTypeInfo(cc2.ChargeType1, 2);
                        }
                        if (!String.IsNullOrEmpty(cc2.ChargeType2) && cc2.ChargeAmount2.HasValue && cc2.ChargeAmount2 != 0)
                        {
                            VAT.ChargeType2 = getChargeTypeInfo(cc2.ChargeType2, 1);
                            VAT.ChargeAmount2Text = cc2.ChargeAmount2.Value + cc2.ChargeCurrency + " " + getChargeTypeInfo(cc2.ChargeType2, 2);
                        }
                        if (!String.IsNullOrEmpty(cc2.ChargeType3) && cc2.ChargeAmount3.HasValue && cc2.ChargeAmount3 != 0)
                        {
                            VAT.ChargeType3 = getChargeTypeInfo(cc2.ChargeType3, 1);
                            VAT.ChargeAmount3Text = cc2.ChargeAmount3.Value + cc2.ChargeCurrency + " " + getChargeTypeInfo(cc2.ChargeType3, 2);
                        }                        
                        //Phân loại phí phát sinh VAT hoặc phí không phát sinh VAT => Thảo sẽ gửi anh danh sách Code phí phân theo có VAT và không
                        if (VAT.TotalTaxAmount.HasValue)
                        {
                            VAT.TotalTaxText = "VAT";
                            VAT.TotalTaxAmountText = VAT.TotalTaxAmount.Value + VAT.ChargeCurrency + " PL90304";
                            if (VAT.TotalChargeAmount.HasValue) 
                                VAT.TotalChargeAmount += VAT.TotalTaxAmount;
                            else
                                VAT.TotalChargeAmount = VAT.TotalTaxAmount;
                        }
                        if (VAT.TotalChargeAmount.HasValue)
                        {
                            VAT.TotalChargeAmountText = VAT.TotalChargeAmount.Value + VAT.ChargeCurrency;
                            VAT.TotalChargeAmountWord = Utils.ReadNumber(VAT.ChargeCurrency, VAT.TotalChargeAmount.Value);
                        }
                        var cc3 = db.BDRFROMACCOUNTs.Where(p => p.Id.Equals(cc2.ChargeAcct)).FirstOrDefault();
                        if (cc3 != null)
                        {
                            var cc4 = db.BCUSTOMERS.Where(p => p.CustomerID.Equals(cc3.CustomerID)).FirstOrDefault();
                            if (cc4 != null)
                            {
                                VAT.CustomerID = cc4.CustomerID;
                                VAT.CustomerName = cc4.CustomerName;
                                VAT.CustomerAddress = cc4.Address;
                                VAT.IdentityNo = cc4.IdentityNo;
                            }
                        }                        
                        var lst2 = new List<Model.CollectCharges.Reports.VAT>();
                        lst2.Add(VAT);
                        reportData = new DataSet();
                        reportData.Tables.Add(Utils.CreateDataTable<Model.CollectCharges.Reports.VAT>(lst2));
                        break;                    
                }
                if (reportData != null)
                {
                    try
                    {
                        reportData.Tables[0].TableName = "Table1";
                        bc.Reports.createFileDownload(reportTemplate, reportData, reportSaveName, saveFormat, saveType, Response);
                    }
                    catch (Exception err)
                    {
                        lblError.Text = reportData.Tables[0].TableName + "#" + err.Message;
                    }
                }
            }
            catch (Exception err)
            {
                lblError.Text = err.Message;
            }
        }
        protected void btnReportVAT_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        private string getChargeTypeInfo(string ChargeType, int infoType)
        {
            var cc = db.BCHARGECODEs.Where(p => p.Code.Equals(ChargeType)).FirstOrDefault();
            if (cc == null) return "";
            switch (infoType)
            {
                case 1:
                    return cc.Name_VN;
                case 2:
                    return cc.PLAccount;
            }

            return "";
        }
    }
}