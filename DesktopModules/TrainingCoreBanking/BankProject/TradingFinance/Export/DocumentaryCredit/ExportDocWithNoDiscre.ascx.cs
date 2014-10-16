using System;
using System.Data;
using System.Web.UI;
using DotNetNuke.Common;
using Telerik.Web.UI;
using BankProject.DBContext;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.DataProvider;
using System.Linq;
using System.Collections.Generic;
using System.Data.Objects;
using BankProject.Helper;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ExportDocWithNoDiscre : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly VietVictoryCoreBankingEntities entContext = new VietVictoryCoreBankingEntities();
        private BEXPORT_DOCUMENTPROCESSING _exportDoc;
        protected const int TabDocsWithNoDiscrepancies = 239;
        protected const int TabDocsWithDiscrepancies = 240;
        protected const int TabDocsReject = 241;
        protected const int TabDocsAmend = 376;
        protected const int TabDocsAccept = 244;
        protected int DocsType = 0;
        //
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            //fieldsetDiscrepancies.Visible = (this.TabId == TabDocsWithDiscrepancies);
            InitDataSource();
            if (string.IsNullOrEmpty(Request.QueryString["tid"])) return;
            var dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.Id == long.Parse(Request.QueryString["tid"])).FirstOrDefault();

            if (dsDetail == null)
            {
                lblError.Text = "This Docs not found !";
                return;
            }
            var dsCharge = new List<BEXPORT_DOCUMENTPROCESSINGCHARGE>();
            dsCharge = entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Where(dr => dr.LCCode == dsDetail.PaymentId).ToList();
            //Hiển thị thông tin docs
            DocsType = Convert.ToInt32(dsDetail.DocumentType);

            loadDocsDetail(dsDetail,dsCharge);
            string DocsStatus = dsDetail.Status;
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
                                lblError.Text = "Wrong status (" + _exportDoc.Status + ")";
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
                    {
                        comboDrawType.SelectedValue = "AC";
                    }
                    if (!string.IsNullOrEmpty(Request.QueryString["lst"]))
                    {
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
                        RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                        RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                    }
                    else
                    {
                        if (this.TabId == TabDocsAmend && (DocsStatus.Equals(bd.TransactionStatus.UNA) || DocsStatus.Equals(bd.TransactionStatus.REV)))
                        {
                            //Cho phép Edit
                            bc.Commont.SetTatusFormControls(this.Controls, true);
                            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                        }
                    }
                    break;
            }
            //SetDefaultValue();
            //
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

        protected void SetDefaultValue()
        {
            dteBookingDate.SelectedDate = DateTime.Now;
            dteBookingDate.Enabled = false;
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

        protected void SwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["BankName"] = row["BankName"].ToString();
            e.Item.Attributes["City"] = row["City"].ToString();
            e.Item.Attributes["Country"] = row["Country"].ToString();
            e.Item.Attributes["Continent"] = row["Continent"].ToString();
            e.Item.Attributes["SwiftCode"] = row["SwiftCode"].ToString();
        }
        protected void comboPresentorNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtPresentorName.Text = comboPresentorNo.SelectedItem != null
                                        ? comboPresentorNo.SelectedItem.Attributes["BankName"]
                                        : "";
        }
        protected bool CheckAmountAvailable()
        {
            var orginalCode = "";
            var flag = true;

            if (txtCode.Text.Trim().Length > 15)
            {
                orginalCode = txtCode.Text.Trim().Substring(0, 14);
            }
            var dtCheck = entContext.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == orginalCode).FirstOrDefault();
            if (dtCheck != null)
            {
                if (numAmount.Value > double.Parse(dtCheck.Amount.ToString()))
                {
                    bc.Commont.ShowClientMessageBox(Page, this.GetType(), "Can not process because of Document Amount greater than LC Amount");
                    flag = false;
                }
            }

            return flag;
        }
        private void CommitData()
        {
            var ds = new BEXPORT_DOCUMENTPROCESSING();
            var dr = new BEXPORT_DOCUMENTPROCESSING();
            ds = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();
            if (ds == null)
            {
                dr = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();    
            }
            var dsCharge = entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Where(x => x.LCCode == txtCode.Text).ToList();
            if (dsCharge != null && dsCharge.Count > 0)
            {
                
            }
            if (ds == null && dr == null)
            {
                BEXPORT_DOCUMENTPROCESSING obj = new BEXPORT_DOCUMENTPROCESSING
                {
                    DrawType = comboDrawType.SelectedValue,
                    PresentorNo = comboPresentorNo.SelectedValue,
                    PresentorName = txtPresentorName.Text,
                    PresentorRefNo = txtPresentorRefNo.Text,
                    Currency = lblCurrency.Text,
                    BookingDate = dteBookingDate.SelectedDate,
                    DocsReceivedDate = dteDocsReceivedDate.SelectedDate,
                    DocsCode1 = comboDocsCode1.SelectedValue,
                    DocsCode2 = comboDocsCode2.SelectedValue,
                    DocsCode3 = comboDocsCode3.SelectedValue,
                    OtherDocs1 = txtOtherDocs1.Text,
                    OtherDocs2 = txtOtherDocs2.Text,
                    OtherDocs3 = txtOtherDocs3.Text,
                    Discrepancies = txtDiscrepancies.Text,
                    DisposalOfDocs = txtDisposalOfDocs.Text,
                    TraceDate = dteTraceDate.SelectedDate,
                    DocsReceivedDate_Supplemental = dteDocsReceivedDate_Supplemental.SelectedDate,
                    PresentorRefNo_Supplemental = txtPresentorRefNo_Supplemental.Text,
                    Docs_Supplemental1 = txtDocs_Supplemental1.Text,
                    DocumentType=TabId.ToString()
                };
                if (!String.IsNullOrEmpty(numAmount.Text))
                {
                    obj.Amount = double.Parse(numAmount.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfOriginals1.Text))
                {
                    obj.NoOfOriginals1 = long.Parse(numNoOfOriginals1.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfOriginals2.Text))
                {
                    obj.NoOfOriginals2 = long.Parse(numNoOfOriginals2.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfOriginals3.Text))
                {
                    obj.NoOfOriginals3 = long.Parse(numNoOfOriginals3.Text);
                }

                if (!String.IsNullOrEmpty(numNoOfCopies1.Text))
                {
                    obj.NoOfCopies1 = long.Parse(numNoOfOriginals1.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                {
                    obj.NoOfCopies2 = long.Parse(numNoOfOriginals2.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                {
                    obj.NoOfCopies3 = long.Parse(numNoOfOriginals3.Text);
                }
                if (TabId == TabDocsWithNoDiscrepancies || TabId == TabDocsWithDiscrepancies)
                {
                    obj.Status = "UNA";
                }
                entContext.BEXPORT_DOCUMENTPROCESSINGs.Add(obj);
                //save tab charge
                BEXPORT_DOCUMENTPROCESSINGCHARGE charge = new BEXPORT_DOCUMENTPROCESSINGCHARGE { 
                    LCCode=txtCode.Text,
                    WaiveCharges = comboWaiveCharges.SelectedValue,
                    Chargecode = tbChargeCode.SelectedValue,
                    ChargeAcct = rcbChargeAcct.SelectedValue,
                    ChargePeriod = tbChargePeriod.Text,
                    ChargeCcy = rcbChargeCcy.SelectedValue,
                    PartyCharged = rcbPartyCharged.SelectedValue,
                    OmortCharges = rcbOmortCharge.SelectedValue,
                    ChargeStatus = rcbChargeStatus.SelectedValue,
                    ChargeRemarks = tbChargeRemarks.Text,
                    VATNo = tbVatNo.Text,
                    TaxCode = lblTaxCode.Text,
                    Rowchages="1",
                    ViewType=TabId
                };
                if (lblTaxAmt.Text != null)
                {
                    charge.TaxAmt = double.Parse(lblTaxAmt.Text);
                }
                if (tbExcheRate.Text != null)
                {
                    charge.ExchRate = double.Parse(tbExcheRate.Text);
                }
                if (tbChargeAmt.Text != null)
                {
                    charge.ChargeAmt = double.Parse(tbChargeAmt.Text);
                }
                entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge);
                //
                BEXPORT_DOCUMENTPROCESSINGCHARGE charge2 = new BEXPORT_DOCUMENTPROCESSINGCHARGE
                {
                    LCCode = txtCode.Text,
                    WaiveCharges = comboWaiveCharges.SelectedValue,
                    Chargecode = tbChargeCode2.SelectedValue,
                    ChargeAcct = rcbChargeAcct2.SelectedValue,
                    ChargePeriod = tbChargePeriod2.Text,
                    ChargeCcy = rcbChargeCcy2.SelectedValue,
                    PartyCharged = rcbPartyCharged2.SelectedValue,
                    OmortCharges = rcbOmortCharges2.SelectedValue,
                    ChargeStatus = rcbChargeStatus2.SelectedValue,
                    ChargeRemarks = tbChargeRemarks.Text,
                    VATNo = tbVatNo.Text,
                    TaxCode = lblTaxCode.Text,
                    Rowchages = "2",
                    ViewType = TabId
                };
                if (lblTaxAmt2.Text != null)
                {
                    charge2.TaxAmt = double.Parse(lblTaxAmt2.Text);
                }
                if (tbExcheRate2.Text != null)
                {
                    charge2.ExchRate = double.Parse(tbExcheRate2.Text);
                }
                if (tbChargeAmt2.Text != null)
                {
                    charge2.ChargeAmt = double.Parse(tbChargeAmt2.Text);
                }
                entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge2);
                //
                //kiem tra tab 
                BEXPORT_DOCUMENTPROCESSINGCHARGE charge3 = new BEXPORT_DOCUMENTPROCESSINGCHARGE
                {
                    LCCode = txtCode.Text,
                    WaiveCharges = comboWaiveCharges.SelectedValue,
                    Chargecode = tbChargeCode3.SelectedValue,
                    ChargeAcct = rcbChargeAcct3.SelectedValue,
                    ChargePeriod = tbChargePeriod3.Text,
                    ChargeCcy = rcbChargeCcy3.SelectedValue,
                    PartyCharged = rcbPartyCharged3.SelectedValue,
                    OmortCharges = rcbOmortCharges3.SelectedValue,
                    ChargeStatus = rcbChargeStatus3.SelectedValue,
                    ChargeRemarks = tbChargeRemarks.Text,
                    VATNo = tbVatNo.Text,
                    TaxCode = lblTaxCode.Text,
                    Rowchages = "3",
                    ViewType = TabId
                };
                if (lblTaxAmt3.Text != null)
                {
                    charge3.TaxAmt = double.Parse(lblTaxAmt3.Text);
                }
                if (tbExcheRate3.Text != null)
                {
                    charge3.ExchRate = double.Parse(tbExcheRate3.Text);
                }
                if (tbChargeAmt3.Text != null)
                {
                    charge3.ChargeAmt = double.Parse(tbChargeAmt3.Text);
                }
                entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge3);
                //
            }
            else
            {
                if (ds != null)
                {
                    if (TabId == TabDocsReject || TabId == TabDocsAccept)
                    {
                        if (TabId == TabDocsReject)
                        { 
                            ds.RejectDrawType=comboDrawType.SelectedValue;
                            ds.RejectBy=UserId.ToString();
                            ds.RejectStatus = TransactionStatus.UNA;
                            ds.RejectDate = DateTime.Now;
                        }
                        else if (TabId == TabDocsAccept)
                        {
                            ds.AcceptStatus = TransactionStatus.UNA;
                            ds.AcceptRemarts = txtAcceptRemarks.Text;
                            ds.AcceptDrawType = comboDrawType.SelectedValue;
                            ds.AcceptBy = UserId.ToString();
                            ds.AcceptDate = txtAcceptDate.SelectedDate;
                        }
                        entContext.SaveChanges();
                    }
                    else
                    {
                        ds.DrawType = comboDrawType.SelectedValue;
                        ds.PresentorNo = comboPresentorNo.SelectedValue;
                        ds.PresentorName = txtPresentorName.Text;
                        ds.PresentorRefNo = txtPresentorRefNo.Text;
                        ds.Currency = lblCurrency.Text;
                        ds.BookingDate = dteBookingDate.SelectedDate;
                        ds.DocsReceivedDate = dteDocsReceivedDate.SelectedDate;
                        ds.DocsCode1 = comboDocsCode1.SelectedValue;
                        ds.DocsCode2 = comboDocsCode2.SelectedValue;
                        ds.DocsCode3 = comboDocsCode3.SelectedValue;
                        ds.OtherDocs1 = txtOtherDocs1.Text;
                        ds.OtherDocs2 = txtOtherDocs2.Text;
                        ds.OtherDocs3 = txtOtherDocs3.Text;
                        ds.Discrepancies = txtDiscrepancies.Text;
                        ds.DisposalOfDocs = txtDisposalOfDocs.Text;
                        ds.TraceDate = dteTraceDate.SelectedDate;
                        ds.DocsReceivedDate_Supplemental = dteDocsReceivedDate_Supplemental.SelectedDate;
                        ds.PresentorRefNo_Supplemental = txtPresentorRefNo_Supplemental.Text;
                        ds.Docs_Supplemental1 = txtDocs_Supplemental1.Text;
                        ds.DocumentType = TabId.ToString();
                        if (!String.IsNullOrEmpty(numAmount.Text))
                        {
                            ds.Amount = double.Parse(numAmount.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals1.Text))
                        {
                            ds.NoOfOriginals1 = long.Parse(numNoOfOriginals1.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals2.Text))
                        {
                            ds.NoOfOriginals2 = long.Parse(numNoOfOriginals2.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals3.Text))
                        {
                            ds.NoOfOriginals3 = long.Parse(numNoOfOriginals3.Text);
                        }

                        if (!String.IsNullOrEmpty(numNoOfCopies1.Text))
                        {
                            ds.NoOfCopies1 = long.Parse(numNoOfOriginals1.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                        {
                            ds.NoOfCopies2 = long.Parse(numNoOfOriginals2.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                        {
                            ds.NoOfCopies3 = long.Parse(numNoOfOriginals3.Text);
                        }
                        if (TabId == TabDocsWithNoDiscrepancies || TabId == TabDocsWithDiscrepancies)
                        {
                            ds.Status = TransactionStatus.UNA;
                        }
                        else if (TabId == TabDocsAmend)
                        {
                            ds.AmendStatus = TransactionStatus.UNA;
                        }

                        entContext.SaveChanges();
                    }
                }
                else if (dr != null)
                {

                    if (TabId == TabDocsReject || TabId == TabDocsAccept)
                    {
                        if (TabId == TabDocsReject)
                        {
                            dr.RejectDrawType = comboDrawType.SelectedValue;
                            dr.RejectBy = UserId.ToString();
                            dr.RejectStatus = TransactionStatus.UNA;
                            dr.RejectDate = DateTime.Now;
                        }
                        else if (TabId == TabDocsAccept)
                        {
                            dr.AcceptStatus = TransactionStatus.UNA;
                            dr.AcceptRemarts = txtAcceptRemarks.Text;
                            dr.AcceptDrawType = comboDrawType.SelectedValue;
                            dr.AcceptBy = UserId.ToString();
                            dr.AcceptDate = txtAcceptDate.SelectedDate;
                        }
                        entContext.SaveChanges();
                    }
                    else
                    {
                        dr.DrawType = comboDrawType.SelectedValue;
                        dr.PresentorNo = comboPresentorNo.SelectedValue;
                        dr.PresentorName = txtPresentorName.Text;
                        dr.PresentorRefNo = txtPresentorRefNo.Text;
                        dr.Currency = lblCurrency.Text;
                        dr.BookingDate = dteBookingDate.SelectedDate;
                        dr.DocsReceivedDate = dteDocsReceivedDate.SelectedDate;
                        dr.DocsCode1 = comboDocsCode1.SelectedValue;
                        dr.DocsCode2 = comboDocsCode2.SelectedValue;
                        dr.DocsCode3 = comboDocsCode3.SelectedValue;
                        dr.OtherDocs1 = txtOtherDocs1.Text;
                        dr.OtherDocs2 = txtOtherDocs2.Text;
                        dr.OtherDocs3 = txtOtherDocs3.Text;
                        dr.Discrepancies = txtDiscrepancies.Text;
                        dr.DisposalOfDocs = txtDisposalOfDocs.Text;
                        dr.TraceDate = dteTraceDate.SelectedDate;
                        dr.DocsReceivedDate_Supplemental = dteDocsReceivedDate_Supplemental.SelectedDate;
                        dr.PresentorRefNo_Supplemental = txtPresentorRefNo_Supplemental.Text;
                        dr.Docs_Supplemental1 = txtDocs_Supplemental1.Text;
                        dr.DocumentType = TabId.ToString();
                        if (!String.IsNullOrEmpty(numAmount.Text))
                        {
                            dr.Amount = double.Parse(numAmount.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals1.Text))
                        {
                            dr.NoOfOriginals1 = long.Parse(numNoOfOriginals1.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals2.Text))
                        {
                            dr.NoOfOriginals2 = long.Parse(numNoOfOriginals2.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfOriginals3.Text))
                        {
                            dr.NoOfOriginals3 = long.Parse(numNoOfOriginals3.Text);
                        }

                        if (!String.IsNullOrEmpty(numNoOfCopies1.Text))
                        {
                            dr.NoOfCopies1 = long.Parse(numNoOfOriginals1.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                        {
                            dr.NoOfCopies2 = long.Parse(numNoOfOriginals2.Text);
                        }
                        if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                        {
                            dr.NoOfCopies3 = long.Parse(numNoOfOriginals3.Text);
                        }
                        if (TabId == TabDocsWithNoDiscrepancies || TabId == TabDocsWithDiscrepancies)
                        {
                            dr.Status = TransactionStatus.UNA;
                        }
                        else if (TabId == TabDocsAmend)
                        {
                            dr.AmendStatus = TransactionStatus.UNA;
                        }
                        entContext.SaveChanges();
                    }
                }
            }
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
                                //CommitData();
                                if (this.TabId == TabDocsAmend)
                                //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId);
                                {
                                    UpdateStatus("UNA");
                                }
                                Response.Redirect("Default.aspx?tabid=" + TabId);
                            }
                            break;
                        case TabDocsReject:
                        case TabDocsAccept:
                            //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId, txtAcceptRemarks.Text);
                            Response.Redirect("Default.aspx?tabid=" + TabId);
                            break;
                    }
                    break;

                case bc.Commands.Authorize:
                    //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.AUT, TabId, UserId);
                    //Response.Redirect("Default.aspx?tabid=" + TabId);
                    Authorize();
                    break;

                case bc.Commands.Reverse:
                    //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.REV, TabId, UserId);
                    //if (TabId == TabDocsWithDiscrepancies || TabId == TabDocsWithNoDiscrepancies)
                    //    Response.Redirect("Default.aspx?tabid=" + TabDocsAmend + "&tid=" + txtCode.Text.Trim());
                    //else
                    //    Response.Redirect("Default.aspx?tabid=" + TabId);
                    Reverse();
                    break;
                case bc.Commands.Preview:
                    Response.Redirect(EditUrl("List"));
                    break;
            }
        }
        protected void Authorize()
        {
            UpdateStatus("AUT");
            Response.Redirect(Globals.NavigateURL(TabId));
        }
        protected void Reverse()
        {
            UpdateStatus("REV");
            Response.Redirect(Globals.NavigateURL(TabId, "", "tid=" + txtCode.Text));
        }
        protected void UpdateStatus(string status)
        {
            var obj = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.PaymentId == txtCode.Text).FirstOrDefault();
            if (obj != null)
            {
                switch (TabId)
                {
                    case TabDocsWithDiscrepancies:
                    case TabDocsWithNoDiscrepancies:
                        obj.Status = status;
                        obj.AuthorizedBy = UserId;
                        obj.AuthorizedDate = DateTime.Now;
                        break;
                    case TabDocsAmend:
                        if (status == "REV")
                        {
                            obj.AmendStatus = status;
                        }
                        else
                        {
                            obj.AmendStatus = status;
                            obj.AmendBy = UserId.ToString();
                        }
                        break;
                    case TabDocsReject:
                        if (status == "REV")
                        {
                            obj.RejectStatus = status;
                        }
                        else
                        {
                            obj.RejectStatus = status;
                            obj.RejectBy = UserId.ToString();
                            obj.RejectDate = DateTime.Now;
                        }
                        break;
                    case TabDocsAccept:
                        if (status == "REV")
                        {
                            obj.AcceptStatus = status;
                        }
                        else
                        {
                            obj.AcceptStatus = status;
                            obj.AcceptBy = UserId.ToString();
                            obj.AcceptDate = DateTime.Now;
                        }
                        break;
                }
                entContext.SaveChanges();
            }
        }
        private void setDocsCodeData(BEXPORT_DOCUMENTPROCESSING dsDetail, int stt, ref RadComboBox cboDocsCode, ref RadNumericTextBox txtNumOfOriginals, ref RadNumericTextBox txtNumofCopies, ref RadTextBox txtOtherDocs)
        {
            cboDocsCode.SelectedValue = dsDetail.DocsCode1;
            if (stt == 1)
            {
                if (dsDetail.NoOfOriginals1 != null)
                {
                    txtNumOfOriginals.Value = Convert.ToDouble(dsDetail.NoOfOriginals1);
                }
                if (dsDetail.OtherDocs1 != null)
                {
                    txtOtherDocs.Text = dsDetail.OtherDocs1;
                }
            }
            else if (stt == 2)
            {
                if (dsDetail.NoOfOriginals2 != null)
                {
                    txtNumOfOriginals.Value = Convert.ToDouble(dsDetail.NoOfOriginals2);
                }
                if (dsDetail.OtherDocs2 != null)
                {
                    txtOtherDocs.Text = dsDetail.OtherDocs2;
                }

            }
            else if (stt == 3)
            {
                if (dsDetail.NoOfOriginals3 != null)
                {
                    txtNumOfOriginals.Value = Convert.ToDouble(dsDetail.NoOfOriginals3);
                }
                if (dsDetail.OtherDocs3 != null)
                {
                    txtOtherDocs.Text = dsDetail.OtherDocs3;
                }
            }
            switch (stt)
            {
                case 1:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumofCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;
                case 2:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumofCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;
                case 3:
                    divDocsCode1.Visible = (txtNumOfOriginals.Value > 0 || txtNumofCopies.Value > 0 || !String.IsNullOrEmpty(txtOtherDocs.Text));
                    break;


            }
        }
        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            /*var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();*/
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
        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        private void parseDocsCode(int Order, BEXPORT_DOCUMENTPROCESSING dsDetail, ref System.Web.UI.HtmlControls.HtmlGenericControl divDocsCode, ref RadComboBox cbDocsCode, ref RadNumericTextBox tbNoOfOriginals, ref RadNumericTextBox tbNoOfCopies)
        {
            if (Order == 1)
            {
                string DocsCode = dsDetail.DocsCode1;
                if (dsDetail.NoOfOriginals1 != null)
                {
                    tbNoOfOriginals.Value = Convert.ToInt32(dsDetail.NoOfOriginals1);
                }
                if (dsDetail.NoOfCopies1 != null)
                {
                    tbNoOfCopies.Value = Convert.ToInt32(dsDetail.NoOfCopies1);
                }
            }
            else if (Order == 2)
            {
                string DocsCode = dsDetail.DocsCode2;
                if (dsDetail.NoOfOriginals2 != null)
                {
                    tbNoOfOriginals.Value = Convert.ToInt32(dsDetail.NoOfOriginals2);
                }
                if (dsDetail.NoOfCopies2 != null)
                {
                    tbNoOfCopies.Value = Convert.ToInt32(dsDetail.NoOfCopies2);
                }
            }
            else if (Order == 3)
            {
                string DocsCode = dsDetail.DocsCode3;
                if (dsDetail.NoOfOriginals3 != null)
                {
                    tbNoOfOriginals.Value = Convert.ToInt32(dsDetail.NoOfOriginals3);
                }
                if (dsDetail.NoOfCopies2 != null)
                {
                    tbNoOfCopies.Value = Convert.ToInt32(dsDetail.NoOfCopies3);
                }
            }
        }
        private void LoadChargeAcct(string CustomerName, string Currency, ref RadComboBox cboChargeAcct)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Id", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(CustomerName, Currency));
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
        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bool WaiveCharges = (comboWaiveCharges.SelectedValue == "YES");
            divACCPTCHG.Visible = WaiveCharges;
            divCABLECHG.Visible = WaiveCharges;
            divPAYMENTCHG.Visible = WaiveCharges;
        }

        private void loadDocsDetail(BEXPORT_DOCUMENTPROCESSING dsDetail, List<BEXPORT_DOCUMENTPROCESSINGCHARGE> dsCharge)
        {
            if ((!String.IsNullOrEmpty(dsDetail.AcceptDate.ToString())) && (dsDetail.AcceptDate.ToString().IndexOf("1/1/1900") == -1))
            {
                txtAcceptDate.SelectedDate = Convert.ToDateTime(dsDetail.AcceptDate);
            }
            txtAcceptRemarks.Text = dsDetail.AcceptRemarts;
            //
            txtCode.Text = dsDetail.PaymentId;
            comboDrawType.SelectedValue = dsDetail.DrawType;
            comboPresentorNo.SelectedValue = dsDetail.PresentorNo;
            txtPresentorName.Text = dsDetail.PresentorName;
            txtPresentorRefNo.Text = dsDetail.PresentorRefNo;
            lblCurrency.Text = dsDetail.Currency;
            numAmount.Value = Convert.ToDouble(dsDetail.Amount);
            
            if ((!String.IsNullOrEmpty(dsDetail.BookingDate.ToString())) && (dsDetail.BookingDate.ToString().IndexOf("1/1/1900") == -1))
            {
                dteBookingDate.SelectedDate = Convert.ToDateTime(dsDetail.BookingDate);
            }
            
            if ((!String.IsNullOrEmpty(dsDetail.DocsReceivedDate.ToString())) && (dsDetail.DocsReceivedDate.ToString().IndexOf("1/1/1900") == -1))
            {
                dteDocsReceivedDate.SelectedDate = Convert.ToDateTime(dsDetail.DocsReceivedDate);
            }
            setDocsCodeData(dsDetail, 1, ref comboDocsCode1, ref numNoOfOriginals1, ref numNoOfCopies1, ref txtOtherDocs1);
            setDocsCodeData(dsDetail, 2, ref comboDocsCode2, ref numNoOfOriginals2, ref numNoOfCopies2, ref txtOtherDocs2);
            setDocsCodeData(dsDetail, 3, ref comboDocsCode3, ref numNoOfOriginals3, ref numNoOfCopies3, ref txtOtherDocs3);
            if ((!String.IsNullOrEmpty(dsDetail.TraceDate.ToString())) && (dsDetail.TraceDate.ToString().IndexOf("1/1/1900") == -1))
            {
                dteTraceDate.SelectedDate = Convert.ToDateTime(dsDetail.TraceDate);
            }
            if ((!String.IsNullOrEmpty(dsDetail.DocsReceivedDate_Supplemental.ToString())) && (dsDetail.DocsReceivedDate_Supplemental.ToString().IndexOf("1/1/1900") == -1))
            {
                dteDocsReceivedDate_Supplemental.SelectedDate = Convert.ToDateTime(dsDetail.DocsReceivedDate_Supplemental);
            }
            txtPresentorRefNo_Supplemental.Text = dsDetail.PresentorRefNo_Supplemental;
            txtDocs_Supplemental1.Text = dsDetail.Docs_Supplemental1;
            DocsType = Convert.ToInt32(dsDetail.DocumentType);
            bool isDocsDiscrepancies = (DocsType == TabDocsWithDiscrepancies);
            fieldsetDiscrepancies.Visible = isDocsDiscrepancies;
            if (isDocsDiscrepancies)
            {
                txtDiscrepancies.Text = dsDetail.Discrepancies;
                txtDisposalOfDocs.Text = dsDetail.DisposalOfDocs;
            }
            comboWaiveCharges.SelectedValue = dsDetail.WaiveCharges;
            tbChargeRemarks.Text = dsDetail.ChargeRemarks;
            tbVatNo.Text = dsDetail.VATNo;

            parseDocsCode(1, dsDetail, ref divDocsCode1, ref comboDocsCode1, ref numNoOfOriginals1, ref numNoOfCopies1);
            parseDocsCode(2, dsDetail, ref divDocsCode2, ref comboDocsCode2, ref numNoOfOriginals2, ref numNoOfCopies2);
            parseDocsCode(3, dsDetail, ref divDocsCode3, ref comboDocsCode3, ref numNoOfOriginals3, ref numNoOfCopies3);

            //TAB CHARGE
            divCharge.Visible = isDocsDiscrepancies;
            if (dsCharge != null && dsCharge.Count > 0)
            {
                foreach (var item in dsCharge)
                {
                    if (item.Chargecode == "ILC.CABLE")
                    {
                        parseTabCharge(item, ref tbChargeCode, ref rcbChargeCcy, ref rcbChargeAcct, ref tbChargeAmt, ref rcbPartyCharged, ref rcbOmortCharge, ref lblTaxCode, ref lblTaxAmt);
                    }
                    else if (item.Chargecode == "ILC.OPEN")
                    {
                        parseTabCharge(item, ref tbChargeCode2, ref rcbChargeCcy2, ref rcbChargeAcct2, ref tbChargeAmt2, ref rcbPartyCharged2, ref rcbOmortCharges2, ref lblTaxCode2, ref lblTaxAmt2);
                    }
                    else if (item.Chargecode == "ILC.OPENAMORT")
                    {
                        parseTabCharge(item, ref tbChargeCode3, ref rcbChargeCcy3, ref rcbChargeAcct3, ref tbChargeAmt3, ref rcbPartyCharged3, ref rcbOmortCharges3, ref lblTaxCode3, ref lblTaxAmt3);
                    }
                }
            }
            else comboWaiveCharges.SelectedValue = "NO";
            comboWaiveCharges_OnSelectedIndexChanged(null, null);
        }
        private void parseTabCharge(BEXPORT_DOCUMENTPROCESSINGCHARGE drDetail, ref RadComboBox cbChargeCode, ref RadComboBox cbChargeCcy, ref RadComboBox cbChargeAcct
                , ref RadNumericTextBox tbChargeAmt, ref RadComboBox cbPartyCharged, ref RadComboBox cbOmortCharges
                , ref System.Web.UI.WebControls.Label lblTaxCode, ref System.Web.UI.WebControls.Label lblTaxAmt)
        {
            cbChargeCode.SelectedValue = drDetail.Chargecode;
            cbChargeCcy.SelectedValue = drDetail.ChargeCcy;
            cbChargeAcct.SelectedValue = drDetail.ChargeAcct;
            if (drDetail.ChargeAmt != null)
                tbChargeAmt.Value = Convert.ToInt32(drDetail.ChargeAmt);
            cbPartyCharged.SelectedValue = drDetail.PartyCharged;
            cbOmortCharges.SelectedValue = drDetail.OmortCharges;
            lblTaxCode.Text = drDetail.TaxCode;
            if (drDetail.TaxAmt!=null)
                lblTaxAmt.Text = drDetail.TaxAmt.ToString();
        }
    
    
    
        protected void btSearch_Click(object sender, EventArgs e)
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            lblError.Text = "";
            switch (this.TabId)
            {
                case TabDocsWithDiscrepancies:
                case TabDocsWithNoDiscrepancies:
                    //fieldsetDiscrepancies.Visible = (this.TabId == TabDocsWithDiscrepancies);
                    var dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.LCCode == txtCode.Text && dr.Status == "UNA").FirstOrDefault();                    
                    var dsCharge = new List<BEXPORT_DOCUMENTPROCESSINGCHARGE>();
                    if (!String.IsNullOrEmpty(txtCode.Text) && txtCode.Text.LastIndexOf(".") > 0)
                    {
                        var drDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).FirstOrDefault();
                        if (drDetail == null)
                        {
                            lblError.Text = "This Docs not found !";
                            return;
                        }
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        //Hiển thị thông tin docs
                        
                        switch (drDetail.Status)
                        {
                            case bd.TransactionStatus.UNA:
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                bc.Commont.SetTatusFormControls(this.Controls, true);
                                break;
                            case bd.TransactionStatus.AUT:
                                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                                RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                bc.Commont.SetTatusFormControls(this.Controls, true);
                                break;
                        }
                        loadDocsDetail(dsDetail,dsCharge);
                        return;
                    }
                    if (dsDetail != null)
                    {
                        
                            lblError.Text = "Previous docs is wating for approve!";
                            if (dsDetail.WaiveCharges == "YES")
                            {
                                dsCharge = entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Where(dr => dr.LCCode == dsDetail.PaymentId).ToList();
                            }
                            bc.Commont.SetTatusFormControls(this.Controls, false);
                            //hien thi thong tin docs dang cho duyet
                        loadDocsDetail(dsDetail, dsCharge);
                        return;
                            //
                     }
                     //get data from advisingnegotiaton
                     var lstOriginal=entContext.BAdvisingAndNegotiationLCs.Where(dr=>dr.NormalLCCode==txtCode.Text).FirstOrDefault();
                     if(lstOriginal==null)
                     {
                        lblError.Text="This LC was not found"; 
                         return;
                     }
                     if(!lstOriginal.Status.Equals(bd.TransactionStatus.AUT))
                     {
                            lblError.Text = "This LC has not authorized";
                            return;
                     }
                     else if (lstOriginal.AmendStatus != null && !lstOriginal.AmendStatus.Equals(bd.TransactionStatus.AUT))
                     {
                            lblError.Text = "This LC waiting for amend approve !";
                            return;
                     }
                     else if (lstOriginal.AcceptStatus != null && !lstOriginal.AcceptStatus.Equals(bd.TransactionStatus.AUT))
                     {
                            lblError.Text = "This LC waiting for accept approve !";
                            return;
                     }
                     else if (lstOriginal.CancelStatus != null && !lstOriginal.CancelStatus.Equals(bd.TransactionStatus.REV))
                     {
                            lblError.Text = "This LC is rejected !";
                            return;
                     }
                    //sinh ra PaymentID
                    var dsPayDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.LCCode == txtCode.Text).ToList();
                    var maxId = dsPayDetail.Max(x => x.PaymentNo);
                    if (maxId == null)
                    {
                        maxId = 1;
                    }
                    else
                    {
                        maxId = maxId + 1;
                    }
                    txtCode.Text = lstOriginal.NormalLCCode + "." + maxId;
                    //
                     //txtCode.Text = lstOriginal.NormalLCCode;
                        //hiddenCustomerName.Value = dsDetail.ApplicantName;
                     lblCurrency.Text = lstOriginal.Currency;
                        //numAmount.Value = Convert.ToDouble(dsDetail.Amount) - Convert.ToDouble(dsDetail.t.TotalDocsAmount);
                     dteBookingDate.SelectedDate = DateTime.Now;
                     comboDrawType.SelectedValue = "CO";
                     comboDrawType.Enabled = false;
                     RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                     break;
                case TabDocsReject:
                case TabDocsAmend:
                case TabDocsAccept:
                    var chkdsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.PaymentId == txtCode.Text).FirstOrDefault();                    
                    var chkdsCharge = new List<BEXPORT_DOCUMENTPROCESSINGCHARGE>();
                    if (chkdsDetail != null)
                    {
                        //
                        string Status = "", RejectStatus = "";
                        if (chkdsDetail.Status != null) 
                            Status = chkdsDetail.Status;
                        if (chkdsDetail.RejectStatus != null)
                            RejectStatus = chkdsDetail.RejectStatus;
                        //
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
                                if (Convert.ToInt32(chkdsDetail.PaymentFullFlag)!= 0)
                                {
                                    lblError.Text = "This Doc is already payment completed !";
                                    return;
                                }
                                if (this.TabId == TabDocsAccept)
                                {
                                    string AcceptStatus = "";
                                    if (chkdsDetail.AcceptStatus != null) 
                                        AcceptStatus = chkdsDetail.AcceptStatus.ToString();
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
                        //
                        loadDocsDetail(chkdsDetail, chkdsCharge);
                        bc.Commont.SetTatusFormControls(this.Controls, this.TabId == TabDocsAmend);
                        comboDrawType.Enabled = false;
                        RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                        switch (this.TabId)
                        {
                            case TabDocsReject:
                                comboDrawType.SelectedValue = "CR";
                                break;
                            case TabDocsAccept:
                                comboDrawType.SelectedValue = "AC";
                                txtAcceptDate.SelectedDate = DateTime.Now;
                                txtAcceptRemarks.Enabled = true;
                                break;
                        }
                        break;
                        //
                    }
                    else
                    {

                        lblError.Text = "This Docs not found !";
                        return;
                    }
                    
            }
        }
    }
}