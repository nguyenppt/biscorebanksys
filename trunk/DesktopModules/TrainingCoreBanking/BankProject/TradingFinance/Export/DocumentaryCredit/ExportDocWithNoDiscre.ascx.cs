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
using BankProject.Model;
using System.Reflection;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class ExportDocWithNoDiscre : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private readonly ExportLC entContext = new ExportLC();
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
            var TabId = this.TabId;
            //fieldsetDiscrepancies.Visible = (this.TabId == TabDocsWithDiscrepancies);
            InitDataSource();
            if (string.IsNullOrEmpty(Request.QueryString["tid"])) 
                return;
            
                var tid = Request.QueryString["tid"].ToString();
                var dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.PaymentId == tid).FirstOrDefault();
                if (TabId == TabDocsAmend)
                {
                    var findTypeAmend = tid.Split('.');
                    if (findTypeAmend != null && findTypeAmend.Length > 0)
                    {
                        if (findTypeAmend.Length == 2)
                        {
                            dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == tid && (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES)).FirstOrDefault();
                        }
                        else {
                            dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == tid).FirstOrDefault();
                        }
                    }
                }
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
            tbChargeCode.Enabled = false;
            tbChargeCode2.Enabled = false;
            tbChargeCode3.Enabled = false;
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
                        if (this.TabId == TabDocsAmend && (DocsStatus.Equals(bd.TransactionStatus.AUT) || DocsStatus.Equals(bd.TransactionStatus.REV)))
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
            divCharge.Style.Add("display", "none");

            divPresentorNo.Visible = true;
            divDocCode.Visible = true;
            divLast.Visible = true;

            switch (TabId)
            {
                case TabDocsWithNoDiscrepancies: // Docs With No Discrepancies
                    comboDrawType.SelectedValue = "CO";
                    divCharge.Style.Add("display", "none");
                    break;
                case TabDocsWithDiscrepancies: // Docs With Discrepancies
                    comboDrawType.SelectedValue = "CO";
                    fieldsetDiscrepancies.Visible = true;
                    divCharge.Style.Add("display", "block");
                    break;
                case TabDocsReject: // Reject Docs Sent For Collection
                    bc.Commont.SetTatusFormControls(this.Controls, false);
                    divCharge.Style.Add("display", "block");

                    divPresentorNo.Visible = false;
                    divDocCode.Visible = false;
                    divLast.Visible = false;
                    fieldsetDiscrepancies.Visible = true;
                    txtCode.Enabled = true;
                    break;
                default:
                    divCharge.Style.Add("display", "none");
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
            
            //numAmountUtilization.Value = 0;
        }
        protected void InitDataSource()
        {
            bc.Commont.initRadComboBox(ref comboDrawType, "Display", "Code", bd.SQLData.B_BDRAWTYPE_GetAll());
            //bc.Commont.initRadComboBox(ref comboPresentorNo, "SwiftCode", "SwiftCode", bd.SQLData.B_BBANKSWIFTCODE_GetByType("all"));
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
            //comboWaiveCharges_OnSelectedIndexChanged(null, null);
            //Party Charged
            tblList = createTableList();
            addData2TableList(ref tblList, "A");
            //addData2TableList(ref tblList, "AC");
            addData2TableList(ref tblList, "B");
            //addData2TableList(ref tblList, "BC");
            bc.Commont.initRadComboBox(ref rcbPartyCharged, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref rcbPartyCharged2, "Text", "Value", tblList);
            bc.Commont.initRadComboBox(ref rcbPartyCharged3, "Text", "Value", tblList);
            if (TabId != TabDocsWithDiscrepancies && TabId!=TabDocsReject)
            {
                divCharge.Style.Add("display", "none");
            }
            else {
                divCharge.Style.Add("display", "block");
            }
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
        //protected void comboPresentorNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    txtPresentorName.Text = comboPresentorNo.SelectedItem != null
        //                                ? comboPresentorNo.SelectedItem.Attributes["BankName"]
        //                                : "";
        //}
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
        private void SetNullValue()
        { 
                    comboDrawType.SelectedValue=null;
                    comboPresentorNo.Text=null;
                    txtPresentorName.Text="";
                    txtPresentorRefNo.Text="";
                    lblCurrency.Text="";
                    dteBookingDate.SelectedDate=null;
                    dteDocsReceivedDate.SelectedDate=null;
                    comboDocsCode1.SelectedValue=null;
                    comboDocsCode2.SelectedValue=null;
                    comboDocsCode3.SelectedValue=null;
                    txtOtherDocs1.Text="";
                    txtOtherDocs2.Text="";
                    txtOtherDocs3.Text="";
                    txtDiscrepancies.Text="";
                    txtDisposalOfDocs.Text="";
                    dteTraceDate.SelectedDate=DateTime.Now;
                    dteDocsReceivedDate_Supplemental.SelectedDate=DateTime.Now;
                    txtPresentorRefNo_Supplemental.Text="";
                    txtDocs_Supplemental1.Text = "";
                    numAmount.Text = "";
                    numNoOfOriginals1.Text = "";
                    numNoOfOriginals2.Text="";
                    numNoOfOriginals3.Text="";
                    numNoOfOriginals1.Text = "";
                    numNoOfOriginals2.Text = "";
                    numNoOfOriginals3.Text = "";

                    //
                    comboWaiveCharges.SelectedValue=null;
                    
                    rcbChargeAcct.SelectedValue=null;
                    tbChargePeriod.Text="";
                    rcbChargeCcy.SelectedValue=null;
                    rcbPartyCharged.SelectedValue=null;
                    rcbOmortCharge.SelectedValue=null;
                    rcbChargeStatus.SelectedValue=null;
                    tbChargeRemarks.Text="";
                    tbVatNo.Text="";
                    lblTaxCode.Text="";
                    lblTaxAmt.Text="";
                    tbExcheRate.Text="";
                    tbChargeAmt.Text="";
                    //
                    
                    rcbChargeAcct2.SelectedValue = null;
                    tbChargePeriod2.Text = "";
                    rcbChargeCcy2.SelectedValue = null;
                    rcbPartyCharged2.SelectedValue = null;
                    rcbOmortCharges2.SelectedValue = null;
                    rcbChargeStatus2.SelectedValue = null;
                    tbChargeRemarks.Text = "";
                    tbVatNo.Text = "";
                    lblTaxCode2.Text = "";
                    lblTaxAmt2.Text = "";
                    tbExcheRate2.Text = "";
                    tbChargeAmt2.Text = "";
                    //
                    
                    rcbChargeAcct3.SelectedValue = null;
                    tbChargePeriod3.Text = "";
                    rcbChargeCcy3.SelectedValue = null;
                    rcbPartyCharged3.SelectedValue = null;
                    rcbOmortCharges3.SelectedValue = null;
                    rcbChargeStatus3.SelectedValue = null;
                    tbChargeRemarks.Text = "";
                    tbVatNo.Text = "";
                    lblTaxCode3.Text = "";
                    lblTaxAmt3.Text = "";
                    tbExcheRate3.Text = "";
                    tbChargeAmt3.Text = "";
        }
        private void SaveAmend()
        {
            try
            {
                int AmendPreId = 0;
                string AmendNo = "";
                var findTypeAmend = txtCode.Text.Split('.');
                
                if (findTypeAmend != null && findTypeAmend.Length > 0)
                {
                    //truong hop them moi
                    if (findTypeAmend.Length == 3)
                    {
                        //xet xem so TF nay co hay chua
                        var chkAmend = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == txtCode.Text).FirstOrDefault();
                        //neu chua co thi them moi, co roi thi update
                        //truong hop chua co--> se them moi
                        if (chkAmend == null)
                        {
                            //xet record truoc do bang cach lay id -1 neu lon hon 0 se update TF.x.y, nguoc lai chi update TF.x
                            if (!String.IsNullOrEmpty(findTypeAmend[2]))
                            {
                                AmendPreId = int.Parse(findTypeAmend[2]) - 1;
                            }
                            if (AmendPreId > 0)
                            {
                                //xet Amend truoc do
                                AmendNo = findTypeAmend[0] + "." + findTypeAmend[1] + "." + AmendPreId;
                                var objPreAmend = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == AmendNo).FirstOrDefault();
                                if (objPreAmend != null)
                                {
                                    objPreAmend.ActiveRecordFlag = YesNo.NO;
                                }
                                //
                            }
                            else
                            {
                                AmendNo = findTypeAmend[0] + "." + findTypeAmend[1];
                                var objPreAmend = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == AmendNo).FirstOrDefault();
                                if (objPreAmend != null)
                                {
                                    objPreAmend.ActiveRecordFlag = YesNo.NO;
                                }
                            }
                            //update
                            entContext.SaveChanges();
                            //them moi record hien tai
                            BEXPORT_DOCUMENTPROCESSING objInsertAmend = new BEXPORT_DOCUMENTPROCESSING
                            {
                                DrawType = comboDrawType.SelectedValue,
                                PresentorNo = comboPresentorNo.Text,
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
                                LCCode = findTypeAmend[0],
                                PaymentNo = long.Parse(findTypeAmend[1]),
                                PaymentId = findTypeAmend[0] + "." + findTypeAmend[1],
                                Id = long.Parse(findTypeAmend[1]),
                                DocumentType = TabId.ToString(),
                                FullDocsAmount = txtFullDocsAmount.Value,
                                WaiveCharges = comboWaiveCharges.SelectedValue,
                                ChargeRemarks = tbChargeRemarks.Text,
                                VATNo = tbVatNo.Text,
                                AmendId = int.Parse(findTypeAmend[2]),
                                AmendNo = txtCode.Text,
                                ActiveRecordFlag = YesNo.YES,
                                Status = "AUT",
                                OldDocsReceivedDate = dteDocsReceivedDate.SelectedDate,
                                RefAmendNo = AmendNo,
                                AmendStatus = "UNA"
                            };
                            if (!String.IsNullOrEmpty(numAmount.Text))
                            {
                                objInsertAmend.Amount = double.Parse(numAmount.Text);
                                objInsertAmend.OldAmount = numAmount.Value;
                            }
                            if (!String.IsNullOrEmpty(numNoOfOriginals1.Text))
                            {
                                objInsertAmend.NoOfOriginals1 = long.Parse(numNoOfOriginals1.Text);
                            }
                            if (!String.IsNullOrEmpty(numNoOfOriginals2.Text))
                            {
                                objInsertAmend.NoOfOriginals2 = long.Parse(numNoOfOriginals2.Text);
                            }
                            if (!String.IsNullOrEmpty(numNoOfOriginals3.Text))
                            {
                                objInsertAmend.NoOfOriginals3 = long.Parse(numNoOfOriginals3.Text);
                            }

                            if (!String.IsNullOrEmpty(numNoOfCopies1.Text))
                            {
                                objInsertAmend.NoOfCopies1 = long.Parse(numNoOfCopies1.Text);
                            }
                            if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                            {
                                objInsertAmend.NoOfCopies2 = long.Parse(numNoOfCopies2.Text);
                            }
                            if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                            {
                                objInsertAmend.NoOfCopies3 = long.Parse(numNoOfCopies3.Text);
                            }
                            entContext.BEXPORT_DOCUMENTPROCESSINGs.Add(objInsertAmend);
                            entContext.SaveChanges();
                            //
                        }
                        else
                        { 
                            //truong hop update du lieu da co
                            chkAmend.DrawType = comboDrawType.SelectedValue;
                            chkAmend.PresentorNo = comboPresentorNo.Text;
                                chkAmend.PresentorName = txtPresentorName.Text;
                                chkAmend.PresentorRefNo = txtPresentorRefNo.Text;
                                chkAmend.Currency = lblCurrency.Text;
                                chkAmend.BookingDate = dteBookingDate.SelectedDate;
                                chkAmend.DocsReceivedDate = dteDocsReceivedDate.SelectedDate;
                                chkAmend.DocsCode1 = comboDocsCode1.SelectedValue;
                                chkAmend.DocsCode2 = comboDocsCode2.SelectedValue;
                                chkAmend.DocsCode3 = comboDocsCode3.SelectedValue;
                                chkAmend.OtherDocs1 = txtOtherDocs1.Text;
                                chkAmend.OtherDocs2 = txtOtherDocs2.Text;
                                chkAmend.OtherDocs3 = txtOtherDocs3.Text;
                                chkAmend.Discrepancies = txtDiscrepancies.Text;
                                chkAmend.DisposalOfDocs = txtDisposalOfDocs.Text;
                                chkAmend.TraceDate = dteTraceDate.SelectedDate;
                                chkAmend.DocsReceivedDate_Supplemental = dteDocsReceivedDate_Supplemental.SelectedDate;
                                chkAmend.PresentorRefNo_Supplemental = txtPresentorRefNo_Supplemental.Text;
                                chkAmend.Docs_Supplemental1 = txtDocs_Supplemental1.Text;
                                chkAmend.LCCode = findTypeAmend[0];
                                chkAmend.PaymentNo = long.Parse(findTypeAmend[1]);
                                chkAmend.PaymentId = findTypeAmend[0] + "." + findTypeAmend[1];
                                //chkAmend.Id = long.Parse(findTypeAmend[1]);
                                chkAmend.DocumentType = TabId.ToString();
                                chkAmend.FullDocsAmount = txtFullDocsAmount.Value;
                                chkAmend.WaiveCharges = comboWaiveCharges.SelectedValue;
                                chkAmend.ChargeRemarks = tbChargeRemarks.Text;
                                chkAmend.VATNo = tbVatNo.Text;
                                chkAmend.AmendId = int.Parse(findTypeAmend[2]);
                                chkAmend.AmendNo = txtCode.Text;
                                chkAmend.ActiveRecordFlag = YesNo.YES;
                                chkAmend.Status = "AUT";
                                chkAmend.OldDocsReceivedDate = dteDocsReceivedDate.SelectedDate;
                                chkAmend.RefAmendNo = AmendNo;
                                chkAmend.AmendStatus = "UNA";
                            //
                                if (!String.IsNullOrEmpty(numAmount.Text))
                                {
                                    chkAmend.Amount = double.Parse(numAmount.Text);
                                    chkAmend.OldAmount = numAmount.Value;
                                }
                                if (!String.IsNullOrEmpty(numNoOfOriginals1.Text))
                                {
                                    chkAmend.NoOfOriginals1 = long.Parse(numNoOfOriginals1.Text);
                                }
                                if (!String.IsNullOrEmpty(numNoOfOriginals2.Text))
                                {
                                    chkAmend.NoOfOriginals2 = long.Parse(numNoOfOriginals2.Text);
                                }
                                if (!String.IsNullOrEmpty(numNoOfOriginals3.Text))
                                {
                                    chkAmend.NoOfOriginals3 = long.Parse(numNoOfOriginals3.Text);
                                }

                                if (!String.IsNullOrEmpty(numNoOfCopies1.Text))
                                {
                                    chkAmend.NoOfCopies1 = long.Parse(numNoOfCopies1.Text);
                                }
                                if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                                {
                                    chkAmend.NoOfCopies2 = long.Parse(numNoOfCopies2.Text);
                                }
                                if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                                {
                                    chkAmend.NoOfCopies3 = long.Parse(numNoOfCopies3.Text);
                                }
                                entContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
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
                foreach (BEXPORT_DOCUMENTPROCESSINGCHARGE item in dsCharge)
                {
                    entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Remove(item);
                }
                entContext.SaveChanges();
            }
            if (ds == null && dr == null)
            {
                var CCode = "";
                var CId = "";
                if (txtCode.Text.IndexOf('.') != -1)
                {
                    var code = txtCode.Text.Split('.');
                    CCode = code[0];
                    CId = code[1];
                }
                BEXPORT_DOCUMENTPROCESSING obj = new BEXPORT_DOCUMENTPROCESSING
                {
                    DrawType = comboDrawType.SelectedValue,
                    PresentorNo = comboPresentorNo.Text,
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
                    LCCode=CCode,
                    PaymentNo = long.Parse(CId),
                    PaymentId=txtCode.Text,
                    Id=long.Parse(CId),
                    DocumentType = TabId.ToString(),
                    FullDocsAmount=txtFullDocsAmount.Value,
                    WaiveCharges=comboWaiveCharges.SelectedValue,
                    ChargeRemarks = tbChargeRemarks.Text,
                    VATNo=tbVatNo.Text

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
                    obj.NoOfCopies1 = long.Parse(numNoOfCopies1.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfCopies2.Text))
                {
                    obj.NoOfCopies2 = long.Parse(numNoOfCopies2.Text);
                }
                if (!String.IsNullOrEmpty(numNoOfCopies3.Text))
                {
                    obj.NoOfCopies3 = long.Parse(numNoOfCopies3.Text);
                }
                if (TabId == TabDocsWithNoDiscrepancies || TabId == TabDocsWithDiscrepancies)
                {
                    obj.Status = "UNA";
                }
                entContext.BEXPORT_DOCUMENTPROCESSINGs.Add(obj);
                entContext.SaveChanges();
                if (divCharge.Visible && comboWaiveCharges.SelectedValue.Equals("YES"))
                {
                    //save tab charge
                    BEXPORT_DOCUMENTPROCESSINGCHARGE charge = new BEXPORT_DOCUMENTPROCESSINGCHARGE
                    {
                        LCCode = txtCode.Text,
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
                        Rowchages = "1",
                        ViewType = TabId
                    };
                    if (!String.IsNullOrEmpty(lblTaxAmt.Text))
                    {
                        charge.TaxAmt = double.Parse(lblTaxAmt.Text);
                    }
                    if (!String.IsNullOrEmpty(tbExcheRate.Text))
                    {
                        charge.ExchRate = double.Parse(tbExcheRate.Text);
                    }
                    if (!String.IsNullOrEmpty(tbChargeAmt.Text))
                    {
                        charge.ChargeAmt = double.Parse(tbChargeAmt.Text);
                    }
                    entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge);
                    entContext.SaveChanges();
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
                    if (!String.IsNullOrEmpty(lblTaxAmt2.Text))
                    {
                        charge2.TaxAmt = double.Parse(lblTaxAmt2.Text);
                    }
                    if (!String.IsNullOrEmpty(tbExcheRate2.Text))
                    {
                        charge2.ExchRate = double.Parse(tbExcheRate2.Text);
                    }
                    if (!String.IsNullOrEmpty(tbChargeAmt2.Text))
                    {
                        charge2.ChargeAmt = double.Parse(tbChargeAmt2.Text);
                    }
                    entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge2);
                    entContext.SaveChanges();
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
                    if (!String.IsNullOrEmpty(lblTaxAmt3.Text))
                    {
                        charge3.TaxAmt = double.Parse(lblTaxAmt3.Text);
                    }
                    if (!String.IsNullOrEmpty(tbExcheRate3.Text))
                    {
                        charge3.ExchRate = double.Parse(tbExcheRate3.Text);
                    }
                    if (!String.IsNullOrEmpty(tbChargeAmt3.Text))
                    {
                        charge3.ChargeAmt = double.Parse(tbChargeAmt3.Text);
                    }
                    entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Add(charge3);
                    //
                    entContext.SaveChanges();
                }
            }
            else
            {
                if (ds != null)
                {
                    if (TabId == TabDocsReject || TabId == TabDocsAccept)
                    {
                        if (TabId == TabDocsReject)
                        {
                            ds.RejectDrawType = comboDrawType.SelectedValue;
                            ds.RejectBy = UserId.ToString();
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
                        ds.PresentorNo = comboPresentorNo.Text;
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
                        ds.FullDocsAmount = txtFullDocsAmount.Value;
                        ds.WaiveCharges = comboWaiveCharges.SelectedValue;
                        ds.ChargeRemarks = tbChargeRemarks.Text;
                        ds.VATNo = tbVatNo.Text;
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
                        dr.PresentorNo = comboPresentorNo.Text;
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
                        
                            if (CheckAmountAvailable())
                            {
                                CommitData();
                                //xu ly submit cho Amend

                                if (this.TabId == TabDocsAmend)
                                //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId);
                                {
                                    //UpdateStatus("UNA");
                                }
                                Response.Redirect("Default.aspx?tabid=" + TabId);
                            }
                            break;
                        case TabDocsAmend:
                            if (CheckAmountAvailable())
                            {
                                SaveAmend();
                                //xu ly submit cho Amend
                                
                                Response.Redirect("Default.aspx?tabid=" + TabId);
                            }
                            break;

                        case TabDocsReject:
                        case TabDocsAccept:
                            //bd.SQLData.B_BIMPORT_DOCUMENTPROCESSING_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.UNA, TabId, UserId, txtAcceptRemarks.Text);
                            CommitData();
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
            if (TabId == TabDocsAmend)
            {
                var findTypeAmend = txtCode.Text.Split('.');
                if (findTypeAmend != null && findTypeAmend.Length > 0)
                {
                    if (findTypeAmend.Length == 2)
                    {
                        var AmendNo = findTypeAmend[0] + "." + findTypeAmend[1];
                        obj = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == AmendNo && (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES)).FirstOrDefault();
                    }
                    else
                    {
                        obj = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == txtCode.Text).FirstOrDefault();
                    }
                }
            }
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
        //protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    DataRowView row = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["Id"] = row["Id"].ToString();
        //    e.Item.Attributes["Name"] = row["Name"].ToString();
        //}
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

            
            bc.Commont.initRadComboBox(ref rcbChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, rcbChargeCcy.SelectedValue));
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
            bc.Commont.initRadComboBox(ref rcbChargeAcct2, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, rcbChargeCcy2.SelectedValue));
            
        }

        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
            bc.Commont.initRadComboBox(ref rcbChargeAcct3, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, rcbChargeCcy3.SelectedValue));
            
        }
        //protected void comboWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    bool WaiveCharges = (comboWaiveCharges.SelectedValue == "YES");
        //    //divCharge.Visible = WaiveCharges;
        //    divACCPTCHG.Visible = WaiveCharges;
        //    divCABLECHG.Visible = WaiveCharges;
        //    divPAYMENTCHG.Visible = WaiveCharges;
        //}

        private void loadDocsDetail(BEXPORT_DOCUMENTPROCESSING dsDetail, List<BEXPORT_DOCUMENTPROCESSINGCHARGE> dsCharge)
        {
            if ((!String.IsNullOrEmpty(dsDetail.AcceptDate.ToString())) && (dsDetail.AcceptDate.ToString().IndexOf("1/1/1900") == -1))
            {
                txtAcceptDate.SelectedDate = Convert.ToDateTime(dsDetail.AcceptDate);
            }
            txtAcceptRemarks.Text = dsDetail.AcceptRemarts;
            //
            txtFullDocsAmount.Value = dsDetail.FullDocsAmount;
            //
            if (TabId != TabDocsAmend)
            {
                txtCode.Text = dsDetail.PaymentId;
            }
            else {
                txtCode.Text = dsDetail.AmendNo;
            }
            comboDrawType.SelectedValue = dsDetail.DrawType;
            comboPresentorNo.Text = dsDetail.PresentorNo;
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
            bool isDocsDiscrepancies = (TabId == TabDocsWithDiscrepancies);
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
            

            if (isDocsDiscrepancies)
            {
                divCharge.Visible = isDocsDiscrepancies;
                divCharge.Style.Add("display", "block");
                if (dsCharge != null && dsCharge.Count > 0)
                {

                    TCharge.Style.Add("display", "block");
                    TCharge.Visible = true;

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
                else
                {
                    comboWaiveCharges.SelectedValue = "NO";

                    TCharge.Visible = false;
                    TCharge.Style.Add("display", "none");

                }
            }
            else
            {
                divCharge.Style.Add("display", "none");
                divCharge.Visible = isDocsDiscrepancies;
            }
            //comboWaiveCharges_OnSelectedIndexChanged(null, null);
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
                    var dsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.LCCode == txtCode.Text && dr.Status == "UNA"&&(dr.ActiveRecordFlag==null||dr.ActiveRecordFlag==YesNo.YES)).FirstOrDefault();                    
                    var dsCharge = new List<BEXPORT_DOCUMENTPROCESSINGCHARGE>();
                    if (!String.IsNullOrEmpty(txtCode.Text) && txtCode.Text.LastIndexOf(".") > 0)
                    {
                        var drDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text && (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES)).FirstOrDefault();
                        if (drDetail == null)
                        {
                            lblError.Text = "This Docs not found !";
                            SetNullValue();
                            bc.Commont.SetTatusFormControls(this.Controls, true);
                            tbChargeCode.Enabled = false;
                            tbChargeCode2.Enabled = false;
                            tbChargeCode3.Enabled = false;
                            return;
                        }
                        bc.Commont.SetTatusFormControls(this.Controls, false);
                        txtCode.Enabled = true;
                        //
                        var name=txtCode.Text.Split('.');
                        var namese = name[0];
                        if(name!=null)
                        {
                            var lstOriginalBA = entContext.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == namese&&(x.ActiveRecordFlag==null||x.ActiveRecordFlag==YesNo.YES)).FirstOrDefault();
                            if(lstOriginalBA!=null)
                            {
                                txtCustomerName.Value = lstOriginalBA.BeneficiaryName;
                                bc.Commont.initRadComboBox(ref rcbChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginalBA.Currency));
                                bc.Commont.initRadComboBox(ref rcbChargeAcct2, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginalBA.Currency));
                                bc.Commont.initRadComboBox(ref rcbChargeAcct3, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginalBA.Currency));
                            }
                        }
                        //
                        //Hiển thị thông tin docs
                        comboPresentorNo.Text = txtCode.Text;
                        switch (drDetail.Status)
                        {
                            case bd.TransactionStatus.UNA:
                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                bc.Commont.SetTatusFormControls(this.Controls, true);
                                break;
                            case bd.TransactionStatus.AUT:
                                RadToolBar1.FindItemByValue("btPreview").Enabled = false;
                                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
                                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                bc.Commont.SetTatusFormControls(this.Controls, false);
                                txtCode.Enabled = false;
                                lblError.Text = "This LC has approved";
                                break;
                        }
                        if (drDetail.WaiveCharges == "YES")
                        {
                            dsCharge = entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Where(dr => dr.LCCode == drDetail.PaymentId).ToList();
                        }
                        loadDocsDetail(drDetail,dsCharge);
                        
                        return;
                    }
                    if (dsDetail != null)
                    {
                        
                            lblError.Text = "Previous docs is wating for approve!";
                            if (dsDetail.WaiveCharges == "YES")
                            {
                                dsCharge = entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs.Where(dr => dr.LCCode == dsDetail.PaymentId).ToList();
                            }
                            //bc.Commont.SetTatusFormControls(this.Controls, false);
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
                     SetNullValue();

                     txtFullDocsAmount.Value = lstOriginal.Amount;
                     //load charge acc
                     txtCustomerName.Value = lstOriginal.BeneficiaryName;
                     bc.Commont.initRadComboBox(ref rcbChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginal.Currency));
                     bc.Commont.initRadComboBox(ref rcbChargeAcct2, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginal.Currency));
                     bc.Commont.initRadComboBox(ref rcbChargeAcct3, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, lstOriginal.Currency));
                     //
                     bc.Commont.SetTatusFormControls(this.Controls, true);
                     tbChargeCode.Enabled = false;
                     tbChargeCode2.Enabled = false;
                     tbChargeCode3.Enabled = false;
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
                    comboPresentorNo.Text = txtCode.Text;
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
                    var chkdsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(dr => dr.PaymentId == txtCode.Text&&(dr.ActiveRecordFlag==null||dr.ActiveRecordFlag==YesNo.YES)).FirstOrDefault();
                    if (this.TabId == TabDocsAmend)
                    {
                        var chkfindTypeAmend = txtCode.Text.Split('.');
                        if (chkfindTypeAmend != null && chkfindTypeAmend.Length > 0)
                        {
                            if (chkfindTypeAmend.Length == 3) {
                                chkdsDetail = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == txtCode.Text).FirstOrDefault();
                            }
                        }
                    }
                    var chkdsCharge = new List<BEXPORT_DOCUMENTPROCESSINGCHARGE>();
                    if (chkdsDetail != null)
                    {
                        //
                        string Status = "", RejectStatus = "",AmendStatus="";
                        if (chkdsDetail.Status != null) 
                            Status = chkdsDetail.Status;
                        if (chkdsDetail.RejectStatus != null)
                            RejectStatus = chkdsDetail.RejectStatus;
                        if (chkdsDetail.AmendStatus != null)
                            AmendStatus = chkdsDetail.AmendStatus;
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
                                //xet xem truong hop Amend user nhap TF.x hay TF.x.y
                                var findTypeAmend = txtCode.Text.Split('.');
                                if (findTypeAmend != null && findTypeAmend.Length > 0)
                                {
                                    if (findTypeAmend.Length == 2)
                                    {
                                        if (!Status.Equals(bd.TransactionStatus.AUT))
                                        {
                                            lblError.Text = "This Docs is not allow amend !";
                                            return;
                                        }
                                        //else if (AmendStatus == "AUT")
                                        //{
                                        //    lblError.Text = "This LC was approve for amend";
                                        //}
                                        else if (!String.IsNullOrEmpty(RejectStatus))
                                        {
                                            lblError.Text = "This LC was rejected or waited for approve reject";
                                            return;
                                        }
                                        //them phan Amend ngay 18/11/2014
                                        else
                                        {
                                            var objAmend = new BEXPORT_DOCUMENTPROCESSING();
                                            var chkAmend = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.PaymentId == txtCode.Text).ToList();
                                            if (chkAmend != null && chkAmend.Count > 0)
                                            {
                                                objAmend = chkAmend.Where(x => (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES) && (x.AmendStatus == "UNA")).FirstOrDefault();
                                                if (objAmend != null)
                                                {
                                                    loadDocsDetail(objAmend, chkdsCharge);
                                                    bc.Commont.SetTatusFormControls(this.Controls, this.TabId == TabDocsAmend);
                                                    comboDrawType.Enabled = false;
                                                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                                }
                                                else
                                                {
                                                    var maxAmendId = chkAmend.Max(x => x.AmendId);
                                                    if (maxAmendId == null)
                                                    {
                                                        maxAmendId = 1;
                                                    }
                                                    else
                                                    {
                                                        maxAmendId = maxAmendId + 1;
                                                    }
                                                    var code = txtCode.Text;
                                                    
                                                    //lay dong dang active de Amend tiep
                                                    var ctnAmend = chkAmend.Where(x => x.ActiveRecordFlag==null||x.ActiveRecordFlag == YesNo.YES).FirstOrDefault();
                                                    //
                                                    loadDocsDetail(ctnAmend, chkdsCharge);
                                                    bc.Commont.SetTatusFormControls(this.Controls, this.TabId == TabDocsAmend);
                                                    comboDrawType.Enabled = false;
                                                    RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                                    txtCode.Text = code + "." + maxAmendId;
                                                }
                                            }
                                            //chua co dong Amend nao nhu vay

                                        }
                                        return;
                                    }
                                    else if(findTypeAmend.Length==3) { 
                                        if (!Status.Equals(bd.TransactionStatus.AUT))
                                        {
                                            lblError.Text = "This Docs is not allow amend !";
                                            return;
                                        }
                                        else if (AmendStatus == "AUT")
                                        {
                                            lblError.Text = "This LC amend has approve ";
                                            return;
                                        }
                                        else if (!String.IsNullOrEmpty(RejectStatus))
                                        {
                                            lblError.Text = "This LC was rejected or waited for approve reject";
                                            return;
                                        }
                                        //them phan Amend ngay 18/11/2014
                                        else
                                        {
                                            var objAmend = new BEXPORT_DOCUMENTPROCESSING();
                                            var chkAmend = entContext.BEXPORT_DOCUMENTPROCESSINGs.Where(x => x.AmendNo == txtCode.Text).FirstOrDefault();
                                            if (chkAmend != null)
                                            {
                                                loadDocsDetail(chkAmend, chkdsCharge);
                                                bc.Commont.SetTatusFormControls(this.Controls, this.TabId == TabDocsAmend);
                                                comboDrawType.Enabled = false;
                                                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                                            }
                                        }

                                    }
                                }
                                
                                //
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

        protected void RadDatePicker1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs args)
        {
            if (dteDocsReceivedDate.SelectedDate != null)
            {
                var dt = DateTime.Parse(dteDocsReceivedDate.SelectedDate.ToString());
                DateTime time = dt.AddDays(12);
                dteTraceDate.SelectedDate = time.Date;
            }
        }
        protected void btnReportThuGoiChungTu_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        protected void btnReportPhieuXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport(2);
        }
        protected void btnReportPhieuThu_Click(object sender, EventArgs e)
        {
            showReport(3);
        }
        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/Export/";
            string reportSaveName = "";
            DataSet reportData = new DataSet();
            DataTable tbl1 = new DataTable();
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                var name = txtCode.Text.Split('.');
                var strname = "";
                if (name.Length > 0)
                {
                    strname = name[0];
                }
                switch (reportType)
                {
                    case 1:
                        reportTemplate = Context.Server.MapPath(reportTemplate + "ThuThongBaoBoChungTu.doc");
                        reportSaveName = "ThuThongBaoBoChungTu" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //bind Data
                        var strnow = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                        var query = (from DC in entContext.BEXPORT_DOCUMENTPROCESSINGs
                             join AD in entContext.BAdvisingAndNegotiationLCs on DC.LCCode equals AD.NormalLCCode
                             where DC.PaymentId==txtCode.Text
                             select new {DC,AD });
                        var lst = new List<CoverNhoThu>();
                        var strDateEpri = "";
                        foreach (var item in query)
                        {
                            CoverNhoThu itemdata = new CoverNhoThu { 
                                 AdCurrency = item.AD.Currency,
                                ApplicantAddress = item.AD.ApplicantAddr1,
                                ApplicantName = item.AD.ApplicantName,
                                ApplicantNo = item.AD.ApplicantNo,
                                BeneficiaryAddress = item.AD.BeneficiaryAddr1,
                                BeneficiaryName = item.AD.BeneficiaryName,
                                BeneficiaryNo = item.AD.BeneficiaryNo,
                                CollectionNo = item.DC.PaymentId,
                                CurrentDate = strnow,
                                DateExpirity = strDateEpri,
                                DocCurrency1 = item.DC.Currency,
                                DraweeName = item.AD.DraweeName,
                                DraweeNo = item.AD.DraweeNo,
                                LCCode = item.AD.NormalLCCode,
                                DocsCode1 = item.DC.DocsCode1 + " " + item.DC.NoOfCopies1 + " " + item.DC.NoOfOriginals1 + " C",
                                DocsCode2 = item.DC.DocsCode2 + " " + item.DC.NoOfCopies2 + " " + item.DC.NoOfOriginals2 + " C",
                                DocsCode3 = item.DC.DocsCode3 + " " + item.DC.NoOfCopies3 + " " + item.DC.NoOfOriginals3 + " C",
                            };
                            if (item.AD.DateExpiry != null)
                            {
                                itemdata.DateExpirity= item.AD.DateExpiry.Value.Date.Day + "/" + item.AD.DateExpiry.Value.Date.Month + "/" + item.AD.DateExpiry.Value.Date.Year;
                            }
                            if (item.DC.Amount != null)
                            {
                                itemdata.DocAmount1 = double.Parse(item.DC.Amount.ToString());
                            }
                            if (item.DC.FullDocsAmount != null)
                            {
                                itemdata.TotalAmount = double.Parse(item.DC.FullDocsAmount.ToString());
                            }
                            lst.Add(itemdata);
                        }
                        tbl1 = Utils.CreateDataTable<CoverNhoThu>(lst);
                        reportData.Tables.Add(tbl1);
                        break;
                    case 2:

                        reportTemplate = Context.Server.MapPath(reportTemplate + "Export_PhieuXuatNgoaiBang.doc");
                        reportSaveName = "PhieuXuatNgoaiBang" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        var PNgoaiBang = (from BE in entContext.BEXPORT_DOCUMENTPROCESSINGs
                                          join AD in entContext.BAdvisingAndNegotiationLCs on BE.LCCode equals AD.NormalLCCode
                                          join CU in entContext.BCUSTOMERS on AD.BeneficiaryNo equals CU.CustomerID
                                          where BE.PaymentId==txtCode.Text
                                          select new { BE,AD,CU});
                        var BNgoaiBang = new List<PhieuXuatNgoaiBang>();
                        foreach (var item in PNgoaiBang)
                        {
                            var dataPhieuNgoaiBang = new PhieuXuatNgoaiBang {
                                ApplicantAddr1 = item.AD.ApplicantAddr1,
                                ApplicantAddr2 = item.AD.ApplicantAddr2,
                                ApplicantAddr3 = item.AD.ApplicantAddr3,
                                ApplicantName = item.AD.ApplicantName,
                                Currency = item.BE.Currency,
                                CurrentUserLogin = UserInfo.DisplayName,
                                IdentityNo = item.CU.IdentityNo,
                                NormalLCCode = item.BE.LCCode         
                            };
                            if (item.BE.Amount != null)
                            {
                                dataPhieuNgoaiBang.Amount = double.Parse(item.BE.Amount.ToString());
                            }
                            BNgoaiBang.Add(dataPhieuNgoaiBang);
                        }
                        //
                        tbl1 = Utils.CreateDataTable<PhieuXuatNgoaiBang>(BNgoaiBang);
                        reportData.Tables.Add(tbl1);
                        break;
                    case 3:
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionVAT.doc");
                        reportSaveName = "PhieuThu" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        var queryPhieuThu = (from CHA in entContext.BEXPORT_DOCUMENTPROCESSINGCHARGEs
                                             join BE in entContext.BEXPORT_DOCUMENTPROCESSINGs on CHA.LCCode equals BE.PaymentId
                                             join AD in entContext.BAdvisingAndNegotiationLCs on BE.LCCode equals AD.NormalLCCode
                                             join CU in entContext.BCUSTOMERS on AD.BeneficiaryNo equals CU.CustomerID
                                             join BC in entContext.BCHARGECODEs on CHA.Chargecode equals BC.Code
                                             where CHA.LCCode == txtCode.Text
                                             select new { CHA, BE, AD, CU, BC });
                        var tbPhieuThu = new List<PhieuThu>();
                        var DataPhieuThu = new PhieuThu();
                        foreach (var item in queryPhieuThu)
                        {
                            DataPhieuThu.VATNo = item.CHA.VATNo;
                            DataPhieuThu.CustomerName = item.AD.BeneficiaryName;
                            DataPhieuThu.DocCollectCode = item.CHA.LCCode;
                            DataPhieuThu.CustomerAddress = item.AD.BeneficiaryAddr1;
                            DataPhieuThu.UserNameLogin = UserInfo.DisplayName;
                            DataPhieuThu.IdentityNo = item.CU.IdentityNo;
                            DataPhieuThu.ChargeAcct = item.CHA.ChargeAcct;
                            DataPhieuThu.Remarks = item.CHA.ChargeRemarks;
                            DataPhieuThu.MCurrency = item.AD.Currency;
                            DataPhieuThu.CustomerID = item.AD.BeneficiaryNo;
                            if (item.CHA.Chargecode == "ELC.ADVISE")
                            {
                                if (item.BC.Code == "ELC.ADVISE")
                                {
                                    DataPhieuThu.Cot9_1Name = item.BC.Name_VN;
                                    DataPhieuThu.PL1 = item.BC.PLAccount;
                                }
                                if (item.CHA.ChargeAmt != null)
                                {
                                    DataPhieuThu.Amount1 = double.Parse(item.CHA.ChargeAmt.ToString());
                                }
                                DataPhieuThu.Currency1 = item.CHA.ChargeCcy;
                                
                            }
                            //tab2
                            if (item.CHA.Chargecode == "ELC.CONFIRM")
                            {
                                if (item.BC.Code == "ELC.CONFIRM")
                                {
                                    DataPhieuThu.Cot9_2Name = item.BC.Name_VN;
                                    DataPhieuThu.PL2 = item.BC.PLAccount;
                                }
                                if (item.CHA.ChargeAmt != null)
                                {
                                    DataPhieuThu.Amount2 = double.Parse(item.CHA.ChargeAmt.ToString());
                                }
                                DataPhieuThu.Currency2 = item.CHA.ChargeCcy;
                            }

                            //tab3
                            if (item.CHA.Chargecode == "ELC.OTHER")
                            {
                                if (item.BC.Code == "ELC.OTHER")
                                {
                                    DataPhieuThu.Cot9_3Name = item.BC.Name_VN;
                                    DataPhieuThu.PL3 = item.BC.PLAccount;
                                }
                                if (item.CHA.ChargeAmt != null)
                                {
                                    DataPhieuThu.Amount3 = double.Parse(item.CHA.ChargeAmt.ToString());
                                }
                                DataPhieuThu.Currency3 = item.CHA.ChargeCcy;
                            }
                            
                        }
                        tbPhieuThu.Add(DataPhieuThu);
                        tbl1= Utils.CreateDataTable<PhieuThu>(tbPhieuThu);
                        reportData.Tables.Add(tbl1);
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
            catch(Exception ex)
            {
            
            }
        }
    }
}