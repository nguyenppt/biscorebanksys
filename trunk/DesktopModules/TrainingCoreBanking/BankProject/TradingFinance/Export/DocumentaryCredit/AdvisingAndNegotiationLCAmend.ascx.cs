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
using System.Globalization;

namespace BankProject.TradingFinance.Export.DocumentaryCredit
{
    public partial class AdvisingAndNegotiationLCAmend : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        VietVictoryCoreBankingEntities db = new VietVictoryCoreBankingEntities();
        //
        private void setDefaultControls()
        {
            rcbWaiveCharges.SelectedValue = "NO";
            rcbWaiveCharges_OnSelectedIndexChanged(null, null);
            tbVatNo.Enabled = false;
            rcbChargeCode.Enabled = false;
            rcbChargeCode2.Enabled = false;
            rcbChargeCode3.Enabled = false;
            //
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
            //
            var ds = bd.DataTam.B_ISSURLC_GetNewID();
            tbLCCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
            //
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            setDefaultControls();
            rcbIssuingBankType_OnSelectedIndexChanged(null, null);
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName.ToLower();
            switch (commandName)
            {
                case bc.Commands.Commit:
                    break;
                case bc.Commands.Authorize:
                case bc.Commands.Reverse:
                    break;

            }
        }

        protected void rcbIssuingBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            bc.Commont.BankTypeChange(rcbIssuingBankType.SelectedValue, ref lblIssuingBankMessage, ref txtIssuingBankNo, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }

        //ABBKVNVX : AN BINH COMMERCIAL JOINT STOCK BANK
        protected void txtIssuingBankNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtIssuingBankNo.Text, ref lblIssuingBankMessage, ref txtIssuingBankName, ref txtIssuingBankAddr1, ref txtIssuingBankAddr2, ref txtIssuingBankAddr3);
        }
        protected void txtBeneficiaryNo_TextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtBeneficiaryNo.Text, ref lblBeneficiaryMessage, ref txtBeneficiaryName, ref txtBeneficiaryAddr1, ref txtBeneficiaryAddr2, ref txtBeneficiaryAddr3);
        }

        protected void rcbWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string WaiveCharges = rcbWaiveCharges.SelectedValue;
            divACCPTCHG.Visible = WaiveCharges.Equals("NO");
            divCABLECHG.Visible = WaiveCharges.Equals("NO");
            divPAYMENTCHG.Visible = WaiveCharges.Equals("NO");
        }

        protected void LoadChargeAcct(ref RadComboBox cboChargeAcct, string ChargeCcy)
        {
            bc.Commont.initRadComboBox(ref cboChargeAcct, "Display", "Id", bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(txtCustomerName.Value, ChargeCcy));
        }
        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct, rcbChargeCcy.SelectedValue);
        }
        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct2, rcbChargeCcy2.SelectedValue);
        }
        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct(ref rcbChargeAcct3, rcbChargeCcy3.SelectedValue);
        }

        protected void btnReportThuThongBao_Click(object sender, EventArgs e)
        {
            showReport("ThuThongBao");
        }
        protected void btnReportPhieuXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport("PhieuXuatNgoaiBang");
        }
        protected void btnReportPhieuThu_Click(object sender, EventArgs e)
        {
            showReport("PhieuThu");
        }
        private void showReport(string reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/Export/";
            string reportSaveName = "";
            DataSet reportData = new DataSet();
            DataTable tbl1 = new DataTable();
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                var obj = db.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == tbLCCode.Text).FirstOrDefault();
                var objCharge = new List<BAdvisingAndNegotiationLCCharge>();
                if (obj == null)
                {
                    obj = new BAdvisingAndNegotiationLC();
                }
                else
                {
                    objCharge = db.BAdvisingAndNegotiationLCCharges.Where(x => x.DocCollectCode == tbLCCode.Text).ToList();
                }
                switch (reportType)
                {
                    case "ThuThongBao":
                        reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/Export/";
                        reportTemplate = Context.Server.MapPath(reportTemplate + "BM_TTQT_LCXK_02A.doc");
                        reportSaveName = "ThuThongBao2A" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        //
                        var query = db.BAdvisingAndNegotiationLCs.Where(x => x.NormalLCCode == tbLCCode.Text).FirstOrDefault();
                        var TBThuTinDung = new List<ThuThongBao>();
                        if (query != null)
                        {
                            var DataThuThongBao = new ThuThongBao()
                            {
                                BeneficiaryNo = query.BeneficiaryNo,
                                BeneficiaryName = query.BeneficiaryName,
                                BeneficiaryAddress = query.BeneficiaryAddr1,
                                NormalLCCode = query.NormalLCCode,
                                ReceivingBank = query.ReceivingBank,
                                ApplicantNo = query.ApplicantNo,
                                Currency = query.Currency,
                                ApplicantName = query.ApplicantName,
                                ApplicantAddress = query.ApplicantAddr1
                            };
                            if (query.Amount != null)
                            {
                                DataThuThongBao.Amount = double.Parse(query.Amount.ToString());
                            }
                            if (query.DateOfIssue != null)
                            {
                                DataThuThongBao.DateIssue = obj.DateOfIssue.Value.Date.Day + "/" + obj.DateOfIssue.Value.Date.Month + "/" + obj.DateOfIssue.Value.Date.Year;
                            }
                            if (query.DateExpiry != null)
                            {
                                DataThuThongBao.DateExpiry = obj.DateExpiry.Value.Date.Day + "/" + obj.DateExpiry.Value.Date.Month + "/" + obj.DateExpiry.Value.Date.Year;
                            }
                            DataThuThongBao.Date = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                            TBThuTinDung.Add(DataThuThongBao);
                        }
                        tbl1 = Utils.CreateDataTable<ThuThongBao>(TBThuTinDung);
                        reportData.Tables.Add(tbl1);
                        break;
                    case "PhieuThu":
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionVAT.doc");
                        reportSaveName = "PhieuThu" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        var queryPhieuThu = (from CHA in db.BAdvisingAndNegotiationLCCharges
                                             join AD in db.BAdvisingAndNegotiationLCs on CHA.DocCollectCode equals AD.NormalLCCode
                                             join CU in db.BCUSTOMERS on AD.BeneficiaryNo equals CU.CustomerID
                                             join BC in db.BCHARGECODEs on CHA.Chargecode equals BC.Code
                                             where AD.NormalLCCode == tbLCCode.Text
                                             select new { CHA, AD, CU, BC });
                        var tbPhieuThu = new List<PhieuThu>();
                        var DataPhieuThu = new PhieuThu();
                        foreach (var item in queryPhieuThu)
                        {
                            DataPhieuThu.VATNo = item.CHA.VATNo;
                            DataPhieuThu.CustomerName = item.AD.BeneficiaryName;
                            DataPhieuThu.DocCollectCode = item.CHA.DocCollectCode;
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
                        tbl1 = Utils.CreateDataTable<PhieuThu>(tbPhieuThu);
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
            catch (Exception ex)
            { }
        }
    }
}