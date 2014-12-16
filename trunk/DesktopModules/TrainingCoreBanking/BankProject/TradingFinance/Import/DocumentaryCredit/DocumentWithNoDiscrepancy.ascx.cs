using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Entities.Modules;
using Telerik.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;

namespace BankProject.TradingFinance.Import.DocumentaryCredit
{
    public partial class DocumentWithNoDiscrepancy : PortalModuleBase
    {
        protected const int TabDocsWithNoDiscrepancies = 371;
        protected const int TabDocsWithDiscrepancies = 207;
        protected const int TabDocsReject = 208;
        protected const int TabDocsAmend = 373;
        protected const int TabDocsAccept = 210;
        protected int DocsType = 0;
        //
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (IsPostBack) return;
            //
            fieldsetDiscrepancies.Visible = (this.TabId == TabDocsWithDiscrepancies);
            divMT734.Attributes.CssStyle.Remove("display");
            divCharge.Attributes.CssStyle.Remove("display");
            if (this.TabId != TabDocsWithDiscrepancies)
            {
                divMT734.Attributes.CssStyle.Add("display", "none");
                if (this.TabId != TabDocsReject)
                    divCharge.Attributes.CssStyle.Add("display", "none");
            }
            InitDataSource();
            //Lấy chi tiết
            string TransId = Request.QueryString["tid"];
            if (string.IsNullOrEmpty(TransId)) return;
            DataSet dsDetail;
            if (TransId.IndexOf(".") > 0)
            {
                if (TransId.IndexOf(".", TransId.IndexOf(".") + 1) > 0)
                    dsDetail = bd.IssueLC.ImportLCDocsProcessDetail4Amend(TransId);
                else
                    dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, TransId);
            }
            else
                dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, TransId);
            if (dsDetail == null || dsDetail.Tables.Count <= 0)
            {
                lblError.Text = "This Docs not found !";
                return;
            }
            //Hiển thị thông tin docs
            DataRow drDetail = dsDetail.Tables[0].Rows[0];
            DocsType = Convert.ToInt32(drDetail["DocumentType"]);
            loadDocsDetail(dsDetail);
            string DocsStatus = drDetail["Status"].ToString();
            //mặc định là preview
            bc.Commont.SetTatusFormControls(this.Controls, false);
            switch (this.TabId)
            {
                case TabDocsWithDiscrepancies:
                case TabDocsWithNoDiscrepancies:
                    if (DocsType != this.TabId)
                    {
                        lblError.Text = "Wrong function !";
                        return;
                    }
                    if (DocsStatus.Equals(bd.TransactionStatus.AUT))
                    {
                        lblError.Text = "This docs already authorized !";
                        return;
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                    {
                        //Hiển thị nút duyệt
                        switch (DocsStatus)
                        {
                            case bd.TransactionStatus.UNA:
                                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                                RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                break;  
                            default:
                                lblError.Text = "Wrong status (" + DocsStatus + ")";
                                break;
                        }
                        return;
                    }
                    //Cho phep edit neu UNA, REV
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    bc.Commont.SetTatusFormControls(this.Controls, true);
                    DisableDefaultControl();

                    break;
                case TabDocsReject:
                case TabDocsAmend:
                case TabDocsAccept:
                    if (this.TabId == TabDocsReject)
                        comboDrawType.SelectedValue = "CR";
                    else if (this.TabId == TabDocsAccept)
                    {
                        comboDrawType.SelectedValue = "AC";                        
                    }
                    else
                        txtCode.Text = TransId;
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    else
                    {
                        string ssStatus;
                        if (this.TabId == TabDocsReject)
                        {
                            ssStatus = drDetail["RejectStatus"].ToString();
                            if (ssStatus.Equals(bd.TransactionStatus.UNA) || ssStatus.Equals(bd.TransactionStatus.REV))
                            {
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                            }
                        }
                        if (this.TabId == TabDocsAmend)
                        {
                            ssStatus = drDetail["AmendStatus"].ToString();
                            if (ssStatus.Equals(bd.TransactionStatus.UNA) || ssStatus.Equals(bd.TransactionStatus.REV))
                            {
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                            }
                        }
                        if (this.TabId == TabDocsAccept)
                        {
                            ssStatus = drDetail["AcceptStatus"].ToString();
                            if (ssStatus.Equals(bd.TransactionStatus.UNA) || ssStatus.Equals(bd.TransactionStatus.REV))
                            {
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                txtAcceptDate.Enabled = true;
                                txtAcceptRemarks.Enabled = true;
                            }
                        }
                    }
                    break;
            }
        }
        private void DisableDefaultControl()
        {
            comboDrawType.Enabled = false;
            txtDisposalOfDocs_734.Enabled = false;
            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargeCode2.Enabled = false;
            tbChargeCode3.Enabled = false;
        }
        protected void IntialEdittor(RadEditor txtEdittor)
        {
            txtEdittor.EditModes = EditModes.Design;
            txtEdittor.Modules.Clear();
        }

        private void loadDocsDetail(DataSet dsDetail)
        {
            DataTable tbDetail;
            DataRow drDetail = dsDetail.Tables[0].Rows[0];
            //Tab Main
            hiddenCustomerName.Value = drDetail["CustomerName"].ToString();
            if (drDetail["AcceptDate"] != DBNull.Value)
                txtAcceptDate.SelectedDate = Convert.ToDateTime(drDetail["AcceptDate"]);
            txtAcceptRemarks.Text = drDetail["AcceptRemarts"].ToString();
            //
            txtCode.Text = drDetail["PaymentId"].ToString();
            lblSenderTRN.Text = txtCode.Text;
            comboDrawType.SelectedValue = drDetail["DrawType"].ToString();
            txtPresentorNo.Text = drDetail["PresentorNo"].ToString();
            txtPresentorName.Text = drDetail["PresentorName"].ToString();
            txtPresentorRefNo.Text = drDetail["PresentorRefNo"].ToString();
            lblCurrency.Text = drDetail["Currency"].ToString();
            numAmount.Value = Convert.ToDouble(drDetail["Amount"]);
            if (this.TabId == TabDocsAmend && drDetail["OldAmount"] != DBNull.Value)
                txtOldAmount.Value = drDetail["OldAmount"].ToString();
            dteBookingDate.SelectedDate = Convert.ToDateTime(drDetail["BookingDate"]);
            dteDocsReceivedDate.SelectedDate = Convert.ToDateTime(drDetail["DocsReceivedDate"]);
            //
            dteDateUtilization.SelectedDate = dteDocsReceivedDate.SelectedDate;
            numAmountUtilization.Value = numAmount.Value;
            lblUtilizationCurrency.Text = lblCurrency.Text;
            //
            setDocsCodeData(drDetail, 1, ref comboDocsCode1, ref numNoOfOriginals1, ref numNoOfCopies1, ref txtOtherDocs2);
            setDocsCodeData(drDetail, 2, ref comboDocsCode2, ref numNoOfOriginals2, ref numNoOfCopies2, ref txtOtherDocs2);
            setDocsCodeData(drDetail, 3, ref comboDocsCode3, ref numNoOfOriginals3, ref numNoOfCopies3, ref txtOtherDocs3);
            if (drDetail["FullDocsAmount"] != DBNull.Value)
                txtFullDocsAmount.Value = Convert.ToDouble(drDetail["FullDocsAmount"]);
            //
            if (drDetail["TraceDate"] != DBNull.Value)
                dteTraceDate.SelectedDate = Convert.ToDateTime(drDetail["TraceDate"]);
            if (drDetail["DocsReceivedDate_Supplemental"] != DBNull.Value)
                dteDocsReceivedDate_Supplemental.SelectedDate = Convert.ToDateTime(drDetail["DocsReceivedDate_Supplemental"]);
            txtPresentorRefNo_Supplemental.Text = drDetail["PresentorRefNo_Supplemental"].ToString();
            txtDocs_Supplemental1.Text = drDetail["Docs_Supplemental1"].ToString();
            DocsType = Convert.ToInt32(drDetail["DocumentType"]);
            bool isDocsDiscrepancies =  (DocsType== TabDocsWithDiscrepancies);
            fieldsetDiscrepancies.Visible = isDocsDiscrepancies;
            if (isDocsDiscrepancies)
            {
                txtDiscrepancies.Text = drDetail["Discrepancies"].ToString();
                txtDisposalOfDocs.Text = drDetail["DisposalOfDocs"].ToString();
            }
            comboWaiveCharges.SelectedValue = drDetail["WaiveCharges"].ToString();
            tbChargeRemarks.Text = drDetail["ChargeRemarks"].ToString();
            tbVatNo.Text = drDetail["VatNo"].ToString();            
            //
            parseDocsCode(1, drDetail, ref divDocsCode1, ref comboDocsCode1, ref numNoOfOriginals1, ref numNoOfCopies1);
            parseDocsCode(2, drDetail, ref divDocsCode2, ref comboDocsCode2, ref numNoOfOriginals2, ref numNoOfCopies2);
            parseDocsCode(3, drDetail, ref divDocsCode3, ref comboDocsCode3, ref numNoOfOriginals3, ref numNoOfCopies3);
            //Tab MT734
            divMT734.Attributes.CssStyle.Remove("display");
            if (!isDocsDiscrepancies)
                divMT734.Attributes.CssStyle.Add("display", "none");
            tbDetail = dsDetail.Tables[1];
            if (tbDetail != null && tbDetail.Rows.Count > 0)
            {
                drDetail = tbDetail.Rows[0];
                txtPresentorNo_734.Text = drDetail["PresentorNo"].ToString();
                txtPresentorName_734.Text = drDetail["PresentorName"].ToString();
                txtPresentorAddr_734_1.Text = drDetail["PresentorAddr1"].ToString();
                txtPresentorAddr_734_2.Text = drDetail["PresentorAddr2"].ToString();
                txtPresentorAddr_734_3.Text = drDetail["PresentorAddr3"].ToString();
                lblSenderTRN.Text = drDetail["SenderTRN"].ToString();
                //
                txtPresentingBankRef.Text = drDetail["PresentingBankRef"].ToString();
                bc.Commont.setDate(drDetail["DateUtilization"], ref dteDateUtilization);
                numAmountUtilization.Value = Convert.ToInt32(drDetail["AmountUtilization"]);
                lblUtilizationCurrency.Text = drDetail["Currency"].ToString();
                lblSenderTRN.Text = drDetail["SenderTRN"].ToString();
                lblChargesClaimed.Text = drDetail["ChargesClaimed"].ToString();
                lblTotalAmountClaimed.Text = drDetail["TotalAmountClaimed"].ToString();
                txtAccountWithBank.Text = drDetail["AccountWithBankNo"].ToString();
                tbSendertoReceiverInfomation.Text = drDetail["SendertoReceiverInfomation"].ToString();
                txtDiscrepancies_734.Text = drDetail["Discrepancies"].ToString();
                txtDisposalOfDocs_734.Text = drDetail["DisposalOfDocs"].ToString();
            }
            //Tab Charge
            divCharge.Attributes.CssStyle.Remove("display");
            if (!isDocsDiscrepancies)
                divCharge.Attributes.CssStyle.Add("display", "none");
            tbDetail = dsDetail.Tables[2];
            if (tbDetail != null && tbDetail.Rows.Count > 0 && comboWaiveCharges.SelectedValue.Equals("YES"))
            {
                for (int i = 0; i < tbDetail.Rows.Count; i++)
                {
                    drDetail = tbDetail.Rows[i];
                    switch (drDetail["Chargecode"].ToString())
                    {
                        case "ILC.CABLE":
                            parseTabCharge(drDetail, ref tbChargeCode, ref rcbChargeCcy, ref rcbChargeAcct, ref tbChargeAmt, ref rcbPartyCharged, ref rcbOmortCharge, ref lblTaxCode, ref lblTaxAmt);
                            break;
                        case "ILC.OPEN":
                            parseTabCharge(drDetail, ref tbChargeCode2, ref rcbChargeCcy2, ref rcbChargeAcct2, ref tbChargeAmt2, ref rcbPartyCharged2, ref rcbOmortCharges2, ref lblTaxCode2, ref lblTaxAmt2);
                            break;
                        case "ILC.OPENAMORT":
                            parseTabCharge(drDetail, ref tbChargeCode3, ref rcbChargeCcy3, ref rcbChargeAcct3, ref tbChargeAmt3, ref rcbPartyCharged3, ref rcbOmortCharges3, ref lblTaxCode3, ref lblTaxAmt3);
                            break;
                    }
                }
            }
            else comboWaiveCharges.SelectedValue = "NO";
            cboWaiveChargesChanged();
        }
        private void parseDocsCode(int Order, DataRow drDetail, ref System.Web.UI.HtmlControls.HtmlGenericControl divDocsCode, ref RadComboBox cbDocsCode
            , ref RadNumericTextBox tbNoOfOriginals, ref RadNumericTextBox tbNoOfCopies)
        {
            string DocsCode = drDetail["DocsCode" + Order].ToString();
            divDocsCode.Visible = !string.IsNullOrEmpty(DocsCode);
            if (divDocsCode.Visible)
            {
                cbDocsCode.SelectedValue = DocsCode;
                if (drDetail["NoOfOriginals" + Order] != DBNull.Value)
                    tbNoOfOriginals.Value = Convert.ToInt32(drDetail["NoOfOriginals" + Order]);
                if (drDetail["NoOfCopies" + Order] != DBNull.Value)
                    tbNoOfCopies.Value = Convert.ToInt32(drDetail["NoOfCopies" + Order]);
            }
        }
        private void parseTabCharge(DataRow drDetail, ref RadComboBox cbChargeCode, ref RadComboBox cbChargeCcy, ref RadComboBox cbChargeAcct
                , ref RadNumericTextBox tbChargeAmt, ref RadComboBox cbPartyCharged, ref RadComboBox cbOmortCharges
                , ref System.Web.UI.WebControls.Label lblTaxCode, ref System.Web.UI.WebControls.Label lblTaxAmt)
        {
            cbChargeCode.SelectedValue = drDetail["Chargecode"].ToString();
            cbChargeCcy.SelectedValue = drDetail["ChargeCcy"].ToString();
            LoadChargeAcct(hiddenCustomerName.Value, cbChargeCcy.SelectedValue, ref cbChargeAcct);
            cbChargeAcct.SelectedValue = drDetail["ChargeAcct"].ToString();
            if (drDetail["ChargeAmt"] != DBNull.Value)
                tbChargeAmt.Value = Convert.ToInt32(drDetail["ChargeAmt"]);
            cbPartyCharged.SelectedValue = drDetail["PartyCharged"].ToString();
            cbOmortCharges.SelectedValue = drDetail["OmortCharges"].ToString();
            lblTaxCode.Text = drDetail["TaxCode"].ToString();
            if (drDetail["TaxAmt"] != DBNull.Value)
                lblTaxAmt.Text = drDetail["TaxAmt"].ToString();
        }

        protected void InitDataSource()
        {
            bc.Commont.initRadComboBox(ref comboDrawType, "Display", "Code", bd.SQLData.B_BDRAWTYPE_GetAll());
            var tblList = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            bc.Commont.initRadComboBox(ref comboDocsCode1, "Description", "Id", tblList);
            bc.Commont.initRadComboBox(ref comboDocsCode2, "Description", "Id", tblList);
            bc.Commont.initRadComboBox(ref comboDocsCode3, "Description", "Id", tblList);
            tblList = bd.SQLData.B_BCURRENCY_GetAll().Tables[0];
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", tblList);
            tblList = bd.SQLData.B_BCHARGECODE_GetByViewType(92);
            bc.Commont.initRadComboBox(ref tbChargeCode, "Code", "Code", tblList);
            tbChargeCode.SelectedValue = "ILC.CABLE";
            tbChargeCode.Enabled = false;
            bc.Commont.initRadComboBox(ref tbChargeCode2, "Code", "Code", tblList);
            tbChargeCode2.SelectedValue = "ILC.OPEN";
            tbChargeCode2.Enabled = false;
            bc.Commont.initRadComboBox(ref tbChargeCode3, "Code", "Code", tblList);
            tbChargeCode3.SelectedValue = "ILC.OPENAMORT";
            tbChargeCode3.Enabled = false;
            comboWaiveCharges_OnSelectedIndexChanged(null, null);
            //Party Charged
            tblList = createTableList();
            addData2TableList(ref tblList, "A");
            //addData2TableList(ref tblList, "AC");
            addData2TableList(ref tblList, "B");
            //addData2TableList(ref tblList, "BC");
            bc.Commont.initRadComboBox(ref rcbPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref rcbPartyCharged2, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref rcbPartyCharged3, "Text", "Value", tblList);
        }
        private DataTable createTableList()
        {
            DataTable tblList = new DataTable();
            tblList.Columns.Add(new DataColumn("Value", typeof(string)));
            tblList.Columns.Add(new DataColumn("Text", typeof(string)));

            return tblList;
        }
        private void addData2TableList(ref DataTable tblList, string text)
        {
            addData2TableList(ref tblList, text, text);
        }
        private void addData2TableList(ref DataTable tblList, string text, string value)
        {
            DataRow dr = tblList.NewRow();
            dr["Value"] = text;
            dr["Text"] = value;
            tblList.Rows.Add(dr);
        }

        private void LoadToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btReverse").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;

            // NO REPORT
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case bc.Commands.Commit:
                    switch (TabId)
                    {
                        case TabDocsWithNoDiscrepancies:
                        case TabDocsWithDiscrepancies:
                        case TabDocsAmend:
                            if (CheckAmountAvailable())
                            {
                                CommitData();
                                Response.Redirect("Default.aspx?tabid=" + TabId);
                            }
                            break;
                        case TabDocsReject:
                        case TabDocsAccept:
                            bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId, txtAcceptDate.SelectedDate, txtAcceptRemarks.Text);
                            Response.Redirect("Default.aspx?tabid=" + TabId);
                            break;
                    }
                    break;

                case bc.Commands.Authorize:
                    bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.AUT, TabId, UserId);
                    Response.Redirect("Default.aspx?tabid=" + TabId);
                    break;

                case bc.Commands.Reverse:
                    bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.REV, TabId, UserId);
                    Response.Redirect("Default.aspx?tabid=" + this.TabId + "&tid=" + txtCode.Text.Trim());
                    break;
            }
        }

        private void CommitData()
        {
            bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_Insert(TabId.ToString()
                                                        , txtCode.Text
                                                        , comboDrawType.SelectedValue
                                                        , txtPresentorNo.Text
                                                        , txtPresentorName.Text
                                                        , txtPresentorRefNo.Text
                                                        , lblCurrency.Text
                                                        , numAmount.Value
                                                        , dteBookingDate.SelectedDate
                                                        , dteDocsReceivedDate.SelectedDate
                                                        , comboDocsCode1.SelectedValue
                                                        , numNoOfOriginals1.Value
                                                        , numNoOfCopies1.Value
                                                        , comboDocsCode2.SelectedValue
                                                        , numNoOfOriginals2.Value
                                                        , numNoOfCopies2.Value
                                                        , comboDocsCode3.SelectedValue
                                                        , numNoOfOriginals3.Value
                                                        , numNoOfCopies2.Value
                                                        , txtOtherDocs1.Text
                                                        , txtOtherDocs2.Text
                                                        , txtOtherDocs3.Text
                                                        , dteTraceDate.SelectedDate
                                                        , dteDocsReceivedDate_Supplemental.SelectedDate
                                                        , txtPresentorRefNo_Supplemental.Text
                                                        , txtDocs_Supplemental1.Text
                                                        , ""
                                                        , ""
                                                        , UserId
                                                        , TabId
                                                        , txtDiscrepancies.Text
                                                        , txtDisposalOfDocs.Text
                                                        , comboWaiveCharges.SelectedValue
                                                        , tbChargeRemarks.Text
                                                        , tbVatNo.Text
                                                        , txtFullDocsAmount.Value);
            if (divCharge.Visible && comboWaiveCharges.SelectedValue.Equals("YES"))
            {
                double chargeAmt = 0, chargeAmt2 = 0, chargeAmt3 = 0;
                if (tbChargeAmt.Value > 0)
                {
                    chargeAmt = (double)tbChargeAmt.Value;
                }

                if (tbChargeAmt2.Value > 0)
                {
                    chargeAmt2 = (double)tbChargeAmt2.Value;
                }

                if (tbChargeAmt3.Value > 0)
                {
                    chargeAmt3 = (double)tbChargeAmt3.Value;
                }

                bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_CHARGE_Insert(txtCode.Text.Trim(),
                                                          comboWaiveCharges.SelectedValue,
                                                          tbChargeCode.SelectedValue,
                                                          rcbChargeAcct.SelectedValue,
                                                          tbChargePeriod.Text,
                                                          rcbChargeCcy.SelectedValue,
                                                          chargeAmt,
                                                          rcbPartyCharged.SelectedValue,
                                                          rcbOmortCharge.SelectedValue,
                                                          rcbChargeStatus.SelectedValue,
                                                          tbChargeRemarks.Text,
                                                          tbVatNo.Text,
                                                          lblTaxCode.Text,
                                                          string.IsNullOrEmpty(lblTaxAmt.Text) ? 0 : double.Parse(lblTaxAmt.Text),
                                                          1,
                                                          TabId);

                bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_CHARGE_Insert(txtCode.Text.Trim(),
                                                          comboWaiveCharges.SelectedValue,
                                                          tbChargeCode2.SelectedValue,
                                                          rcbChargeAcct2.SelectedValue,
                                                          tbChargePeriod2.Text,
                                                          rcbChargeCcy2.SelectedValue,
                                                          chargeAmt2,
                                                          rcbPartyCharged2.SelectedValue,
                                                          rcbOmortCharges2.SelectedValue,
                                                          rcbChargeStatus2.SelectedValue,
                                                          tbChargeRemarks.Text,
                                                          tbVatNo.Text,
                                                          lblTaxCode2.Text,
                                                          string.IsNullOrEmpty(lblTaxAmt2.Text) ? 0 : double.Parse(lblTaxAmt2.Text),
                                                          2,
                                                          TabId);
                bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_CHARGE_Insert(txtCode.Text.Trim(),
                                                          comboWaiveCharges.SelectedValue,
                                                          tbChargeCode3.SelectedValue,
                                                          rcbChargeAcct3.SelectedValue,
                                                          tbChargePeriod3.Text,
                                                          rcbChargeCcy3.SelectedValue,
                                                          chargeAmt3,
                                                          rcbPartyCharged3.SelectedValue,
                                                          rcbOmortCharges3.SelectedValue,
                                                          rcbChargeStatus3.SelectedValue,
                                                          tbChargeRemarks.Text,
                                                          tbVatNo.Text,
                                                          lblTaxCode3.Text,
                                                          string.IsNullOrEmpty(lblTaxAmt3.Text) ? 0 : double.Parse(lblTaxAmt3.Text),
                                                          3,
                                                          TabId);

            }
            if (this.TabId == TabDocsWithDiscrepancies
                || (this.TabId == TabDocsAmend && string.IsNullOrEmpty(divMT734.Attributes.CssStyle["display"])))
            {
                bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_MT734_Insert(txtCode.Text
                    , txtPresentorNo_734.Text
                    , txtPresentorName_734.Text
                    , txtPresentorAddr_734_1.Text
                    , txtPresentorAddr_734_2.Text
                    , txtPresentorAddr_734_3.Text
                    , lblSenderTRN.Text
                    , txtPresentingBankRef.Text
                    , dteDateUtilization.SelectedDate
                    , numAmountUtilization.Value
                    , lblUtilizationCurrency.Text
                    , lblChargesClaimed.Text
                    , lblTotalAmountClaimed.Text
                    , txtAccountWithBank.Text
                    , ""
                    , tbSendertoReceiverInfomation.Text
                    , txtDiscrepancies_734.Text
                    , txtDisposalOfDocs_734.Text);
            }
        }

        protected void btAddDocsCode_Click(object sender, ImageClickEventArgs e)
        {
            //divDocsCode_BL
            if (divDocsCode2.Visible == false)
            {
                divDocsCode2.Visible = true;
            }
            else if (divDocsCode3.Visible == false)
            {
                divDocsCode3.Visible = true;
            }
        }

        protected void btDeleteDocsCode2_Click(object sender, ImageClickEventArgs e)
        {
            divDocsCode2.Visible = false;
            comboDocsCode2.SelectedValue = string.Empty;
            numNoOfOriginals2.Value = 0;
            numNoOfCopies2.Value = 0;
        }

        protected void btDeleteDocsCode3_Click(object sender, ImageClickEventArgs e)
        {
            divDocsCode3.Visible = false;
            comboDocsCode3.SelectedValue = string.Empty;
            numNoOfOriginals3.Value = 0;
            numNoOfCopies3.Value = 0;
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            lblError.Text = "";
            DataSet dsDetail;
            DataTable tbDetail;
            DataRow drDetail;
            switch (this.TabId)
            {
                case TabDocsWithDiscrepancies:                    
                case TabDocsWithNoDiscrepancies:                    
                    fieldsetDiscrepancies.Visible = (this.TabId == TabDocsWithDiscrepancies);
                    //
                    if (!String.IsNullOrEmpty(txtCode.Text) && txtCode.Text.LastIndexOf(".") > 0)
                    {
                        dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, txtCode.Text);
                        if (dsDetail == null || dsDetail.Tables.Count <= 0 || dsDetail.Tables[0].Rows.Count <= 0)
                        {
                            lblError.Text = "This Docs not found !";
                            return;
                        }
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        //Hiển thị thông tin docs
                        drDetail = dsDetail.Tables[0].Rows[0];
                        switch (drDetail["Status"].ToString())
                        {
                            case bd.TransactionStatus.UNA:
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                bc.Commont.SetTatusFormControls(this.Controls, true);
                                DisableDefaultControl();
                                break;
                        }
                        loadDocsDetail(dsDetail);
                        
                        return;
                    }
                    //Có docs nào đang chờ duyệt ?
                    dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(txtCode.Text, null);
                    if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
                    {
                        lblError.Text = "Previous docs is waiting for approve !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        //Hiển thị thông tin docs đang chờ duyệt
                        drDetail = dsDetail.Tables[0].Rows[0];
                        loadDocsDetail(dsDetail);

                        return;
                    }
                    //Không có docs nào đang chờ duyệt, lấy thông tin LC
                    tbDetail = bd.IssueLC.ImportLCDetailForDocProcess(txtCode.Text);
                    if (tbDetail == null || tbDetail.Rows.Count <= 0)
                    {
                        lblError.Text = "This LC not found !";
                        return;
                    }
                    drDetail = tbDetail.Rows[0];
                    double Amount = Convert.ToDouble(drDetail["Amount"]) - Convert.ToDouble(drDetail["TotalDocsAmount"]);
                    if (Amount <= 0)
                    {
                        lblError.Text = "This LC Amount is empty !";
                        return;
                    }
                    if (!drDetail["Status"].ToString().Equals(bd.TransactionStatus.AUT))
                    {
                        lblError.Text = "This LC not authorize !";
                        return;
                    }
                    if (drDetail["Amend_Status"] != DBNull.Value && !drDetail["Amend_Status"].ToString().Equals(bd.TransactionStatus.AUT))
                    {
                        lblError.Text = "This LC waiting for amend approve !";
                        return;
                    }
                    if (drDetail["Accept_Status"] != DBNull.Value && !drDetail["Accept_Status"].ToString().Equals(bd.TransactionStatus.AUT))
                    {
                        lblError.Text = "This LC waiting for accept approve !";
                        return;
                    }
                    if (drDetail["Cancel_Status"] != DBNull.Value && !drDetail["Cancel_Status"].ToString().Equals(bd.TransactionStatus.REV))
                    {
                        lblError.Text = "This LC is canceled !";
                        return;
                    }
                    txtCode.Text = drDetail["PaymentId"].ToString();
                    lblSenderTRN.Text = txtCode.Text;
                    hiddenCustomerName.Value = drDetail["ApplicantName"].ToString();
                    lblCurrency.Text = drDetail["Currency"].ToString();
                    lblUtilizationCurrency.Text = lblCurrency.Text;
                    numAmount.Value = Amount;
                    numAmountUtilization.Value = numAmount.Value;
                    txtFullDocsAmount.Value = Convert.ToDouble(drDetail["Amount"]);
                    dteBookingDate.SelectedDate = DateTime.Now;
                    comboDrawType.SelectedValue = "CO";
                    comboDrawType.Enabled = false;
                    tbVatNo.Text = bd.IssueLC.GetVatNo();
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;

                    break;
                case TabDocsReject:                    
                case TabDocsAmend:
                case TabDocsAccept:
                    if (this.TabId != TabDocsAmend)
                        dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, txtCode.Text);
                    else
                        dsDetail = bd.IssueLC.ImportLCDocsProcessDetail4Amend(txtCode.Text);
                    if (dsDetail == null || dsDetail.Tables.Count <= 0 || dsDetail.Tables[0].Rows.Count <= 0)
                    {
                        lblError.Text = "This Docs not found !";
                        return;
                    }
                    //
                    drDetail = dsDetail.Tables[0].Rows[0];
                    if (Convert.ToInt32(drDetail["PaymentFullFlag"]) != 0)
                    {
                        lblError.Text = "This Doc is already payment completed !";
                        return;
                    }
                    string Status = "", RejectStatus = "";
                    if (drDetail["Status"] != DBNull.Value) Status = drDetail["Status"].ToString();
                    if (!Status.Equals(bd.TransactionStatus.AUT))
                    {
                        lblError.Text = "This Docs is not authorize !";
                        return;
                    }
                    if (drDetail["RejectStatus"] != DBNull.Value) RejectStatus = drDetail["RejectStatus"].ToString();
                    if (!(String.IsNullOrEmpty(RejectStatus) || RejectStatus.Equals(bd.TransactionStatus.REV)))
                    {
                        lblError.Text = "This Docs is rejected !";
                        return;
                    }
                    switch (this.TabId)
                    {
                        case TabDocsReject:
                        case TabDocsAccept:
                            if (this.TabId == TabDocsAccept)
                            {
                                txtAcceptDate.SelectedDate = DateTime.Now;
                                string AcceptStatus = "";
                                if (drDetail["AcceptStatus"] != DBNull.Value) AcceptStatus = drDetail["AcceptStatus"].ToString();
                                /*if (AcceptStatus.Equals(bd.TransactionStatus.AUT))
                                {
                                    lblError.Text = "This Docs is accepted !";
                                    return;
                                }*/
                                if (AcceptStatus.Equals(bd.TransactionStatus.UNA))
                                {
                                    lblError.Text = "This Docs is waiting for accept approve !";
                                    return;
                                }                                
                            }
                            break;
                        case TabDocsAmend:
                            if (!Status.Equals(bd.TransactionStatus.AUT))
                            {
                                lblError.Text = "This Docs is not allow amend !";
                                return;
                            }
                            break;
                    }
                    loadDocsDetail(dsDetail);
                    comboDrawType.Enabled = false;
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                    switch (this.TabId)
                    {
                        case TabDocsReject:
                            bc.Commont.SetTatusFormControls(this.Controls, false);
                            comboDrawType.SelectedValue = "CR";
                            break;
                        case TabDocsAccept:
                            bc.Commont.SetTatusFormControls(this.Controls, false);
                            comboDrawType.SelectedValue = "AC";                            
                            txtAcceptDate.Enabled = true;
                            txtAcceptRemarks.Enabled = true;
                            break;
                        case TabDocsAmend:
                            if (Convert.ToInt32(drDetail["IsPaying"]) > 0)
                            {
                                lblError.Text = "This docs is under payment !";
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                                bc.Commont.SetTatusFormControls(this.Controls, false);
                                return;
                            }
                            if (string.IsNullOrEmpty(drDetail["NewAmendNo"].ToString()))
                            {
                                txtCode.Text = drDetail["AmendNo"].ToString();
                                string AmendStatus = "";
                                if (drDetail["AmendStatus"] != DBNull.Value) AmendStatus = drDetail["AmendStatus"].ToString();
                                if (AmendStatus.Equals(bd.TransactionStatus.UNA))
                                {
                                    bc.Commont.SetTatusFormControls(this.Controls, true);
                                    DisableDefaultControl();
                                    return;
                                }
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                                bc.Commont.SetTatusFormControls(this.Controls, false);
                                return;
                            }
                            txtCode.Text = drDetail["NewAmendNo"].ToString();
                            bc.Commont.SetTatusFormControls(this.Controls, true);
                            DisableDefaultControl();
                            break;
                    }                    

                    break;
            }
        }

        private void setDocsCodeData(DataRow drDetail, int stt, ref RadComboBox cboDocsCode, ref RadNumericTextBox txtNumOfOriginals, ref RadNumericTextBox txtNumOfCopies, ref RadTextBox txtOtherDocs)
        {
            cboDocsCode.SelectedValue = drDetail["DocsCode" + stt].ToString();
            if (drDetail["NoOfOriginals" + stt] != DBNull.Value)
                txtNumOfOriginals.Value = Convert.ToDouble(drDetail["NoOfOriginals" + stt]);
            if (drDetail["NoOfCopies" + stt] != DBNull.Value)
                txtNumOfCopies.Value = Convert.ToDouble(drDetail["NoOfCopies" + stt]);
            if (drDetail["OtherDocs" + stt] != DBNull.Value)
                txtOtherDocs.Text = drDetail["OtherDocs" + stt].ToString();

            switch (stt)
            {
                case 1:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumOfCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;
                case 2:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumOfCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;
                case 3:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumOfCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;
            }
        }
        
        protected bool CheckAmountAvailable()
        {
            var orginalCode = "";
            var flag = true;

            if (txtCode.Text.Trim().Length > 15)
            {
                orginalCode = txtCode.Text.Trim().Substring(0, 14);
            }
            var dtCheck = bd.SQLData.B_BIMPORT_NORMAILLC_GetOne(orginalCode);
            if (dtCheck != null && dtCheck.Rows.Count > 0)
            {
                if (numAmount.Value > double.Parse(dtCheck.Rows[0]["Amount"].ToString()))
                {
                    bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Can not process because of Document Amount greater than LC Amount");
                    flag = false;
                }
            }

            return flag;
        }

        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cboWaiveChargesChanged();
        }
        private void cboWaiveChargesChanged()
        {
            bool WaiveCharges = (comboWaiveCharges.SelectedValue == "YES");
            divACCPTCHG.Visible = WaiveCharges;
            divCABLECHG.Visible = WaiveCharges;
            divPAYMENTCHG.Visible = WaiveCharges;
        }

        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy.SelectedValue, ref rcbChargeAcct);
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy2.SelectedValue, ref rcbChargeAcct2);
        }

        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy3.SelectedValue, ref rcbChargeAcct3);
        }

        private void LoadChargeAcct(string CustomerName, string Currency, ref RadComboBox cboChargeAcct)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(CustomerName, Currency));
        }

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            chargeAmt_Changed(tbChargeAmt.Value, ref lblTaxAmt, ref lblTaxCode);
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            chargeAmt_Changed(tbChargeAmt2.Value, ref lblTaxAmt2, ref lblTaxCode2);
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            chargeAmt_Changed(tbChargeAmt3.Value, ref lblTaxAmt3, ref lblTaxCode3);
        }

        private void chargeAmt_Changed(double? ChargeAmt, ref System.Web.UI.WebControls.Label labelTaxAmt, ref System.Web.UI.WebControls.Label labelTaxCode)
        {
            double sotien = 0;

            if (ChargeAmt.HasValue)
            {
                sotien = ChargeAmt.Value * 0.1;
            }

            labelTaxAmt.Text = String.Format("{0:C}", sotien).Replace("$", "");
            labelTaxCode.Text = "81      10% VAT on Charge";
        }

        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            /*var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();*/
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
        }

        protected void GenerateVAT()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
        }

        protected void btDownloadMT734_Click(object sender, EventArgs e)
        {
            showReport(1);
        }

        protected void btDownloadVAT_Click(object sender, EventArgs e)
        {
            showReport(2);
        }

        protected void btDownloadXuatNB_Click(object sender, EventArgs e)
        {
            showReport(3);
        }

        protected void btDownloadNhapNB_Click(object sender, EventArgs e)
        {
            showReport(4);
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/DocumentaryCredit/";
            string saveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                switch (reportType)
                {
                    case 1://MT734
                        reportTemplate = Context.Server.MapPath(reportTemplate + "DocumentMT734.doc");
                        saveName = "DocumentMT734_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        reportData = bd.IssueLC.ImportLCDocumentReport(1, txtCode.Text.Trim(), this.UserInfo.Username);
                        reportData.Tables[0].TableName = "Table1";
                        break;
                    case 2://VAT
                        reportTemplate = Context.Server.MapPath(reportTemplate + "VAT.doc");
                        saveName = "VAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.IssueLC.ImportLCDocumentReport(2, txtCode.Text.Trim(), this.UserInfo.Username);
                        reportData.Tables[0].TableName = "Table1";
                        break;
                    case 3:
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PHIEUXUATNGOAIBANG.doc");
                        saveName = "PHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.IssueLC.ImportLCDocumentReport(3, txtCode.Text.Trim(), this.UserInfo.Username);
                        reportData.Tables[0].TableName = "Table1";
                        break;
                    case 4:
                        reportTemplate = Context.Server.MapPath(reportTemplate + "PHIEUNHAPNGOAIBANG.doc");
                        saveName = "PHIEUNHAPNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.IssueLC.ImportLCDocumentReport(3, txtCode.Text.Trim(), this.UserInfo.Username);
                        reportData.Tables[0].TableName = "Table1";
                        break;
                }
                if (reportData != null)
                {
                    try
                    {
                        bc.Reports.createFileDownload(reportTemplate, reportData, saveName, saveFormat, saveType, Response);
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

        protected void txtPresentorNo_OnTextChanged(object sender, EventArgs e)
        {
            lblPresentorNoMsg.Text = "";
            txtPresentorName.Text = "";
            DataRow dr = bc.Commont.loadBankSwiftCodeInfo(txtPresentorNo.Text, ref lblPresentorNo_734Msg, ref txtPresentorName_734, ref txtPresentorAddr_734_1, ref txtPresentorAddr_734_2, ref txtPresentorAddr_734_3);
            if (dr == null)
            {
                lblPresentorNoMsg.Text = "Can not find this Bank.";
                return;
            }
            txtPresentorNo_734.Text = txtPresentorNo.Text;
            txtPresentorName.Text = txtPresentorName_734.Text;
        }
        protected void txtPresentorNo_734_OnTextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtPresentorNo_734.Text, ref lblPresentorNo_734Msg, ref txtPresentorName_734, ref txtPresentorAddr_734_1, ref txtPresentorAddr_734_2, ref txtPresentorAddr_734_3);
        }
    }
}