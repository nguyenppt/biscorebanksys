﻿using System;
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
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //
            InitDataSource();
            if (!string.IsNullOrEmpty(Request.QueryString["tid"]))
            {
                //Lấy chi tiết
                DataSet dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, Request.QueryString["tid"]);
                if (dsDetail == null || dsDetail.Tables.Count <= 0)
                {
                    lblError.Text = "This Docs not found !";
                    return;
                }
                //Hiển thị thông tin docs
                DataRow drDetail = dsDetail.Tables[0].Rows[0];
                loadDocsDetail(drDetail);
                //mặc định là preview
                bc.Commont.SetTatusFormControls(this.Controls, false);
                switch (this.TabId)
                {
                    case TabDocsWithDiscrepancies:
                    case TabDocsWithNoDiscrepancies:
                        if (Convert.ToInt32(drDetail["DocumentType"]) != this.TabId)
                        {
                            lblError.Text = "Wrong function !";
                            return;
                        }
                        if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                        {
                            //Hiển thị nút duyệt
                            switch (drDetail["Status"].ToString())
                            {
                                case bd.TransactionStatus.UNA:
                                    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                                    RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                                    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                                    RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                    break;  
                                default:
                                    lblError.Text = "Wrong status (" + drDetail["Status"] + ")";
                                    break;
                            }
                            return;
                        }                        
                        break;
                    case TabDocsReject:
                    case TabDocsAmend:
                    case TabDocsAccept:
                        if (this.TabId == TabDocsReject)
                            comboDrawType.SelectedValue = "CR";
                        else if (this.TabId == TabDocsAccept)
                            comboDrawType.SelectedValue = "AC";
                        if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                        {
                            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                        }
                        break;
                }
            }
        }

        private void loadDocsDetail(DataRow drDetail)
        {
            txtCode.Text = drDetail["PaymentId"].ToString();
            comboDrawType.SelectedValue = drDetail["DrawType"].ToString();
            comboPresentorNo.SelectedValue = drDetail["PresentorNo"].ToString();
            txtPresentorName.Text = drDetail["PresentorName"].ToString();
            txtPresentorRefNo.Text = drDetail["PresentorRefNo"].ToString();
            lblCurrency.Text = drDetail["Currency"].ToString();
            numAmount.Value = Convert.ToDouble(drDetail["Amount"]);
            dteBookingDate.SelectedDate = Convert.ToDateTime(drDetail["BookingDate"]);
            dteDocsReceivedDate.SelectedDate = Convert.ToDateTime(drDetail["DocsReceivedDate"]);
            //
            setDocsCodeData(drDetail, 1, ref comboDocsCode1, ref numNoOfOriginals1, ref numNoOfCopies1, ref txtOtherDocs1);
            setDocsCodeData(drDetail, 2, ref comboDocsCode2, ref numNoOfOriginals2, ref numNoOfCopies2, ref txtOtherDocs2);
            setDocsCodeData(drDetail, 3, ref comboDocsCode3, ref numNoOfOriginals3, ref numNoOfCopies3, ref txtOtherDocs3);
            //
            if (drDetail["TraceDate"] != DBNull.Value)
                dteTraceDate.SelectedDate = Convert.ToDateTime(drDetail["TraceDate"]);
            if (drDetail["DocsReceivedDate_Supplemental"] != DBNull.Value)
                dteDocsReceivedDate_Supplemental.SelectedDate = Convert.ToDateTime(drDetail["DocsReceivedDate_Supplemental"]);
            txtPresentorRefNo_Supplemental.Text = drDetail["PresentorRefNo_Supplemental"].ToString();
            txtDocs_Supplemental1.Text = drDetail["Docs_Supplemental1"].ToString();
            if (Convert.ToInt32(drDetail["DocumentType"]) == TabDocsWithDiscrepancies)
            {
                fieldsetDiscrepancies.Visible = true;
                txtDiscrepancies.Text = drDetail["Discrepancies"].ToString();
                txtDisposalOfDocs.Text = drDetail["DisposalOfDocs"].ToString();
            }
        }

        protected void InitDataSource()
        {
            bc.Commont.initRadComboBox(ref comboDrawType, "Display", "Code", bd.SQLData.B_BDRAWTYPE_GetAll());
            bc.Commont.initRadComboBox(ref comboPresentorNo, "SwiftCode", "SwiftCode", bd.SQLData.B_BBANKSWIFTCODE_GetByType("all"));
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
            bc.Commont.initRadComboBox(ref tbChargeCode2, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref tbChargeCode3, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref comboPresentorNo_734, "CustomerName", "CustomerID", bd.SQLData.B_BCUSTOMERS_OnlyBusiness());
        }

        protected void SetDefaultValue()
        {
            comboDrawType.Enabled = false;

            fieldsetDiscrepancies.Visible = false;
            divMT734.Visible = false;
            divCharge.Visible = false;

            divPresentorNo.Visible = true;
            divDocCode.Visible = true;
            divLast.Visible = true;
            switch (TabId)
            {
                case TabDocsWithNoDiscrepancies: // Docs With No Discrepancies
                    comboDrawType.SelectedValue = "CO";
                    break;
                case TabDocsWithDiscrepancies: // Docs With Discrepancies
                    comboDrawType.SelectedValue = "CO";
                    fieldsetDiscrepancies.Visible = true;
                    divMT734.Visible = true;
                    divCharge.Visible = true;
                    break;
                case TabDocsReject: // Reject Docs Sent For Collection
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    divCharge.Visible = true;

                    divPresentorNo.Visible = false;
                    divDocCode.Visible = false;
                    divLast.Visible = false;
                    fieldsetDiscrepancies.Visible = true;
                    txtCode.Enabled = true;
                    break;
            }

            divDocsCode2.Visible = false;
            divDocsCode3.Visible = false;

            comboDocsCode1.Enabled = false;
            comboDocsCode2.Enabled = false;
            comboDocsCode3.Enabled = false;

            comboDocsCode1.SelectedValue = "INV";
            comboDocsCode2.SelectedValue = "BL";
            comboDocsCode2.SelectedValue = "PL";

            numNoOfOriginals1.Value = 0;
            numNoOfCopies1.Value = 0;

            numNoOfOriginals2.Value = 0;
            numNoOfCopies2.Value = 0;

            numNoOfOriginals3.Value = 0;
            numNoOfCopies3.Value = 0;

            dteBookingDate.SelectedDate = DateTime.Now;
            dteBookingDate.Enabled = false;

            numAmount.Value = 0;

            tbChargeCode.SelectedValue = "ILC.CABLE";
            tbChargeCode2.SelectedValue = "ILC.OPEN";
            tbChargeCode3.SelectedValue = "ILC.OPENAMORT";

            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargeCode2.Enabled = false;
            tbChargeCode3.Enabled = false;

            rcbPartyCharged.SelectedValue = "A";
            rcbPartyCharged2.SelectedValue = "A";
            rcbPartyCharged3.SelectedValue = "A";

            rcbOmortCharge.SelectedValue = "NO";
            rcbOmortCharges2.SelectedValue = "NO";
            rcbOmortCharges3.SelectedValue = "NO";

            numAmountUtilization.Value = 0;
        }

        protected void SetDisableByReview(bool flag)
        {
            bc.Commont.SetTatusFormControls(this.Controls, flag);
            comboDrawType.Enabled = false;

            comboDocsCode1.Enabled = false;
            comboDocsCode2.Enabled = false;
            comboDocsCode3.Enabled = false;

            dteBookingDate.Enabled = false;

            tbVatNo.Enabled = false;
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
                                if (this.TabId == TabDocsAmend)
                                    bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId);
                                Response.Redirect("Default.aspx?tabid=" + TabId);
                            }
                            break;
                        case TabDocsReject:
                        case TabDocsAccept:
                            bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId);
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
                    Response.Redirect("Default.aspx?tabid=" + TabId);
                    break;
            }
        }

        private void CommitData()
        {
            bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_Insert(TabId.ToString()
                                                        , txtCode.Text
                                                        , comboDrawType.SelectedValue
                                                        , comboPresentorNo.SelectedValue
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
                                                        , txtDisposalOfDocs.Text);
            if (divCharge.Visible)
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
            if (divMT734.Visible)
            {
                bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_MT734_Insert(txtCode.Text
                    , comboPresentorNo_734.SelectedValue
                    , txtPresentorName_734.Text
                    , txtPresentorAddr_734_1.Text
                    , txtPresentorAddr_734_2.Text
                    , txtPresentorAddr_734_3.Text
                    , lblSenderTRN.Text
                    , txtPresentingBankRef.Text
                    , dteDateUtilization.SelectedDate
                    , numAmountUtilization.Value
                    , ""
                    , lblChargesClaimed.Text
                    , lblTotalAmountClaimed.Text
                    , txtAccountWithBank.Text
                    , ""
                    , tbSendertoReceiverInfomation.Text
                    , txtDiscrepancies_734.Text
                    , txtDisposalOfDocs_734.Text);
            }
        }

        protected void SwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["BankName"] = row["BankName"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
            e.Item.Attributes["Continent"] = row["Continent"].ToString();
            e.Item.Attributes["SwiftCode"] = row["SwiftCode"].ToString();
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
            lblError.Text = "";
            DataSet dsDetail;
            DataTable tbDetail;
            DataRow drDetail;
            switch (this.TabId)
            {
                case TabDocsWithDiscrepancies:
                case TabDocsWithNoDiscrepancies:
                    //Có docs nào đang chờ duyệt ?
                    dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(txtCode.Text, null);
                    if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
                    {
                        lblError.Text = "Previous docs is waiting for approve !";
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        //Hiển thị thông tin docs đang chờ duyệt
                        drDetail = dsDetail.Tables[0].Rows[0];
                        loadDocsDetail(drDetail);

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
                    hiddenCustomerName.Value = drDetail["ApplicantName"].ToString();
                    lblCurrency.Text = drDetail["Currency"].ToString();
                    numAmount.Value = Convert.ToDouble(drDetail["Amount"]) - Convert.ToDouble(drDetail["TotalDocsAmount"]);
                    dteBookingDate.SelectedDate = DateTime.Now;
                    comboDrawType.SelectedValue = "CO";
                    comboDrawType.Enabled = false;
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;

                    break;
                case TabDocsReject:                    
                case TabDocsAmend:
                case TabDocsAccept:
                    dsDetail = bd.IssueLC.ImportLCDocsProcessDetail(null, txtCode.Text);
                    if (dsDetail == null || dsDetail.Tables.Count <= 0 || dsDetail.Tables[0].Rows.Count <= 0)
                    {
                        lblError.Text = "This Docs not found !";
                        return;
                    }
                    //
                    drDetail = dsDetail.Tables[0].Rows[0];
                    string Status = "", RejectStatus = "";
                    if (drDetail["Status"] != DBNull.Value) Status = drDetail["Status"].ToString();
                    if (drDetail["RejectStatus"] != DBNull.Value) RejectStatus = drDetail["RejectStatus"].ToString();
                    switch (this.TabId)
                    {
                        case TabDocsReject:
                        case TabDocsAccept:
                            if (!Status.Equals(bd.TransactionStatus.AUT))
                            {
                                lblError.Text = "This Docs is not authorize !";
                                return;
                            }
                            if (!(String.IsNullOrEmpty(RejectStatus) || RejectStatus.Equals(bd.TransactionStatus.REV)))
                            {
                                lblError.Text = "This Docs is reject !";
                                return;
                            }
                            if (Convert.ToInt32(drDetail["PaymentFullFlag"]) != 0)
                            {
                                lblError.Text = "This Doc is already payment completed !";
                                return;
                            }
                            if (this.TabId == TabDocsAccept)
                            {
                                string AcceptStatus = "";
                                if (drDetail["AcceptStatus"] != DBNull.Value) AcceptStatus = drDetail["AcceptStatus"].ToString();
                                if (AcceptStatus.Equals(bd.TransactionStatus.AUT))
                                {
                                    lblError.Text = "This Docs is accepted !";
                                    return;
                                }
                                if (AcceptStatus.Equals(bd.TransactionStatus.UNA))
                                {
                                    lblError.Text = "This Docs is waiting for accept approve !";
                                    return;
                                }
                            }
                            break;
                        case TabDocsAmend:
                            if (!Status.Equals(bd.TransactionStatus.UNA))
                            {
                                lblError.Text = "This Docs is not allow amend !";
                                return;
                            }
                            break;
                    }
                    loadDocsDetail(drDetail);
                    switch (this.TabId)
                    {
                        case TabDocsReject:
                            comboDrawType.SelectedValue = "CR";
                            break;
                        case TabDocsAccept:
                            comboDrawType.SelectedValue = "AC";
                            break;
                    }
                    bc.Commont.SetTatusFormControls(this.Controls, this.TabId == TabDocsAmend);
                    comboDrawType.Enabled = false;
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;

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
        protected void LoadData(ref DataRow drowProcessing)
        {
            var dsDoc = bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_GetByCode(txtCode.Text.Trim(), TabId, UserId);
            var status = string.Empty;
            var rejectStatus = string.Empty;

            if (dsDoc == null || dsDoc.Tables.Count <= 0)
            {
                lblError.Text = "LC No. is not found";
                return;
            }

            lblSenderTRN.Text = txtCode.Text;

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btPreview").Enabled = true;

            #region Tab Main
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[0].Rows[0];

                status = drow["Status"].ToString();
                rejectStatus = drow["rejectStatus"].ToString();

                comboDrawType.SelectedValue = drow["DrawType"].ToString();
                comboPresentorNo.SelectedValue = drow["PresentorNo"].ToString();
                txtPresentorName.Text = drow["PresentorName"].ToString();
                txtPresentorRefNo.Text = drow["PresentorRefNo"].ToString();
                lblCurrency.Text = drow["Currency"].ToString();
                numAmount.Text = drow["Amount"].ToString();

                if (!string.IsNullOrEmpty(drow["BookingDate"].ToString()) &&
                    drow["BookingDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteBookingDate.SelectedDate = DateTime.Parse(drow["BookingDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["DocsReceivedDate"].ToString()) &&
                    drow["DocsReceivedDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDocsReceivedDate.SelectedDate = DateTime.Parse(drow["DocsReceivedDate"].ToString());
                }

                comboDocsCode1.SelectedValue = drow["DocsCode1"].ToString();
                numNoOfOriginals1.Text = drow["NoOfOriginals1"].ToString();
                numNoOfCopies1.Text = drow["NoOfCopies1"].ToString();

                comboDocsCode2.SelectedValue = drow["DocsCode2"].ToString();
                numNoOfOriginals2.Text = drow["NoOfOriginals2"].ToString();
                numNoOfCopies2.Text = drow["NoOfCopies2"].ToString();
                divDocsCode2.Visible = (numNoOfOriginals2.Value > 0);

                comboDocsCode3.SelectedValue = drow["DocsCode3"].ToString();
                numNoOfOriginals3.Text = drow["NoOfOriginals3"].ToString();
                numNoOfCopies3.Text = drow["NoOfCopies3"].ToString();
                divDocsCode3.Visible = (numNoOfOriginals3.Value > 0);

                txtOtherDocs1.Text = drow["OtherDocs1"].ToString();
                txtOtherDocs2.Text = drow["OtherDocs2"].ToString();
                txtOtherDocs3.Text = drow["OtherDocs3"].ToString();

                if (!string.IsNullOrEmpty(drow["TraceDate"].ToString()) &&
                    drow["TraceDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteTraceDate.SelectedDate = DateTime.Parse(drow["TraceDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["DocsReceivedDate_Supplemental"].ToString()) &&
                    drow["DocsReceivedDate_Supplemental"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDocsReceivedDate_Supplemental.SelectedDate =
                        DateTime.Parse(drow["DocsReceivedDate_Supplemental"].ToString());
                }

                txtPresentorRefNo_Supplemental.Text = drow["PresentorRefNo_Supplemental"].ToString();
                txtDocs_Supplemental1.Text = drow["Docs_Supplemental1"].ToString();

                txtDiscrepancies.Text = drow["Discrepancies"].ToString();
                txtDisposalOfDocs.Text = drow["DisposalOfDocs"].ToString();
            }
            else
            {
                comboPresentorNo.SelectedValue = string.Empty;
                txtPresentorName.Text = string.Empty;
                txtPresentorRefNo.Text = string.Empty;
                lblCurrency.Text = string.Empty;
                numAmount.Value = 0;

                dteBookingDate.SelectedDate = DateTime.Now;
                dteDocsReceivedDate.SelectedDate = DateTime.Now;

                comboDocsCode1.SelectedValue = string.Empty;
                numNoOfOriginals1.Value = 0;
                numNoOfCopies1.Value = 0;

                comboDocsCode2.SelectedValue = string.Empty;
                numNoOfOriginals2.Value = 0;
                numNoOfCopies2.Value = 0;
                

                comboDocsCode3.SelectedValue = string.Empty;
                numNoOfOriginals3.Value = 0;
                numNoOfCopies3.Value = 0;

                divDocsCode2.Visible = false;
                divDocsCode3.Visible = false;

                txtOtherDocs1.Text = string.Empty;
                txtOtherDocs2.Text = string.Empty;
                txtOtherDocs3.Text = string.Empty;

                dteTraceDate.SelectedDate = null;
                dteDocsReceivedDate_Supplemental.SelectedDate = null;

                txtPresentorRefNo_Supplemental.Text = string.Empty;
                txtDocs_Supplemental1.Text = string.Empty;

                txtDiscrepancies.Text = string.Empty;
                txtDisposalOfDocs.Text = string.Empty;
            }

            #endregion

            #region Tab Charges
            if (dsDoc.Tables[3].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[3].Rows[0];
                comboWaiveCharges.SelectedValue = drow1["WaiveCharges"].ToString();
                tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                tbVatNo.Text = drow1["VATNo"].ToString();

                rcbChargeCcy.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                {
                    LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy.SelectedValue, ref rcbChargeAcct);
                    rcbChargeAcct.SelectedValue = drow1["ChargeAcct"].ToString();
                }

                tbChargeAmt.Value = (double?)drow1["ChargeAmt"];
                rcbPartyCharged.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode.Text = drow1["TaxCode"].ToString();
                lblTaxAmt.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct.SelectedValue = string.Empty;
                rcbChargeCcy.SelectedValue = string.Empty;
                tbChargeAmt.Value = 0;
                rcbPartyCharged.SelectedValue = "A";
                rcbChargeStatus.SelectedValue = string.Empty;
                lblTaxCode.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;
            }

            if (dsDoc.Tables[4].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[4].Rows[0];

                rcbChargeCcy2.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                {
                    LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy2.SelectedValue, ref rcbChargeAcct2);
                    rcbChargeAcct2.SelectedValue = drow1["ChargeAcct"].ToString();
                }

                tbChargeAmt2.Value = (double?)drow1["ChargeAmt"];
                rcbPartyCharged2.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharges2.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus2.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode2.Text = drow1["TaxCode"].ToString();
                lblTaxAmt2.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbChargeAmt2.Value = 0;
                rcbPartyCharged2.SelectedValue = string.Empty;
                rcbOmortCharges2.SelectedValue = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;
            }

            if (dsDoc.Tables[5].Rows.Count > 0)
            {
                var drow1 = dsDoc.Tables[5].Rows[0];

                rcbChargeCcy3.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy3.SelectedValue))
                {
                    LoadChargeAcct(hiddenCustomerName.Value, rcbChargeCcy3.SelectedValue, ref rcbChargeAcct3);
                    rcbChargeAcct3.SelectedValue = drow1["ChargeAcct"].ToString();
                }

                tbChargeAmt3.Value = (double?)drow1["ChargeAmt"];
                rcbPartyCharged3.SelectedValue = drow1["PartyCharged"].ToString();
                rcbOmortCharges3.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus3.SelectedValue = drow1["ChargeStatus"].ToString();
                lblTaxCode3.Text = drow1["TaxCode"].ToString();
                lblTaxAmt3.Text = String.Format("{0:C}", drow1["TaxAmt"]).Replace("$", "");
            }
            else
            {
                rcbChargeAcct3.SelectedValue = string.Empty;
                rcbChargeCcy3.SelectedValue = string.Empty;
                tbChargeAmt3.Value = 0;
                rcbPartyCharged3.SelectedValue = "A";
                rcbOmortCharges3.SelectedValue = string.Empty;
                rcbChargeStatus3.SelectedValue = string.Empty;
                lblTaxCode3.Text = string.Empty;
                lblTaxAmt3.Text = string.Empty;
            }
            #endregion

            if (dsDoc.Tables[1].Rows.Count > 0)
            {
                var drow = dsDoc.Tables[1].Rows[0];
                hiddenCustomerName.Value = drow["ApplicantName"].ToString();
                lblCurrency.Text = drow["Currency"].ToString();

                // call func check status
                CheckStatus(drow);

                // User enter code moi
                if (dsDoc.Tables[0].Rows.Count <= 0)
                {
                    numAmount.Value = (double?) drow["Amount"];
                }

                if (status.Equals(bd.TransactionStatus.AUT))
                {
                    LoadToolBar(false);
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                    RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    //Reject
                    if (this.TabId == TabDocsReject)
                    {
                        comboDrawType.SelectedValue = "CR";
                        RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                        if (String.IsNullOrEmpty(rejectStatus))
                        {
                            //Chuan bi Reject
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                        }
                        else
                        {
                            if (rejectStatus.Equals(bd.TransactionStatus.UNA))
                            {
                                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                            }
                            /*else if (rejectStatus.Equals(bd.TransactionStatus.AUT))
                            {
                                RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                            }*/
                        }

                        return;
                    }                    
                    // Neu AUT thi ko cho phep sua
                    lblError.Text = "This LC has authorized";
                }
            }

            // The previous payment has not been authorized yet. 
            // kiem tra khi Preview
            if (CheckReview())
            {
                if (string.IsNullOrEmpty(Request.QueryString["disable"]) && CheckPreviousPayment(dsDoc))
                {
                    LoadToolBar(false);
                    SetDisableByReview(false);
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                    RadToolBar1.FindItemByValue("btPreview").Enabled = true;
                }
                else
                {
                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                }
            }

            if (Request.QueryString["disable"] != null)
            {
                txtCode.Text = Request.QueryString["paycode"];
            }
            else
            {
                txtCode.Text = dsDoc.Tables[2].Rows[0]["PaymentId"].ToString();
            }
        }

        protected void comboPresentorNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPresentorName.Text = comboPresentorNo.SelectedItem != null
                                        ? comboPresentorNo.SelectedItem.Attributes["BankName"]
                                        : "";
        }

        protected void CheckStatus(DataRow drow)
        {
            if (drow["Status"].ToString().Equals(bd.TransactionStatus.UNA))
            {
                lblError.Text = "This LC has not authorized at Amend step.";
                SetDisableByReview(false);
                txtCode.Enabled = true;
                divDocsCode2.Disabled = true;
                divDocsCode3.Disabled = true;
                divDocsCode1.Disabled = true;
            }
            else if (!string.IsNullOrEmpty(drow["Cancel_Status"].ToString()) &&
                        drow["Cancel_Status"].ToString().Equals(bd.TransactionStatus.AUT))
            {
                lblError.Text = "This LC is cancel";
                SetDisableByReview(false);
                txtCode.Enabled = true;
                divDocsCode2.Disabled = true;
                divDocsCode3.Disabled = true;
                divDocsCode1.Disabled = true;
            }
            else if (!string.IsNullOrEmpty(drow["Amend_Status"].ToString()) &&
                     !drow["Amend_Status"].ToString().Equals(bd.TransactionStatus.AUT))
            {
                lblError.Text = "This LC has not authorized at Amend step.";
                SetDisableByReview(false);
                txtCode.Enabled = true;
                divDocsCode2.Disabled = true;
                divDocsCode3.Disabled = true;
                divDocsCode1.Disabled = true;
            }
        }

         protected bool CheckReview()
        {
            if (txtCode.Text.Trim().Length > 15)
            {
                return true;
            }
            return false;
        }

         protected bool CheckPreviousPayment(DataSet dsPayment)
         {
             DataTable tbl = dsPayment.Tables[2];
             if (tbl != null && tbl.Rows.Count > 0 && tbl.Rows[0]["Status"].ToString().Equals(bd.TransactionStatus.UNA))
             {
                 bc.Commont.ShowClientMessageBox(Page, this.GetType(), "The previous payment has not been authorized yet.");
                 return true;
             }
             return false;
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
            if (comboWaiveCharges.SelectedValue == "NO")
            {
                divACCPTCHG.Visible = true;
                divCABLECHG.Visible = true;
                divPAYMENTCHG.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                divACCPTCHG.Visible = false;
                divCABLECHG.Visible = false;
                divPAYMENTCHG.Visible = false;
            }
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
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(CustomerName, Currency));
        }

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
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
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
        }

        protected void GenerateVAT()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
        }

        protected void rcbApplicantID_SelectIndexChange(object sender, EventArgs e)
        {
            txtPresentorName_734.Text = comboPresentorNo_734.SelectedItem != null
                                       ? comboPresentorNo_734.SelectedItem.Attributes["CustomerName"]
                                       : "";
            txtPresentorAddr_734_1.Text = comboPresentorNo_734.SelectedItem != null
                                       ? comboPresentorNo_734.SelectedItem.Attributes["Address"]
                                       : "";
            txtPresentorAddr_734_2.Text = comboPresentorNo_734.SelectedItem != null
                                       ? comboPresentorNo_734.SelectedItem.Attributes["City"]
                                       : "";
            txtPresentorAddr_734_3.Text = comboPresentorNo_734.SelectedItem != null
                                       ? comboPresentorNo_734.SelectedItem.Attributes["Country"]
                                       : "";
        }

        protected void rcbApplicantID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
        }
    }
}