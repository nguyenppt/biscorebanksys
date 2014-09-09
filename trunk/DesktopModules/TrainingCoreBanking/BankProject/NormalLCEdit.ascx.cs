using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using Telerik.Web.UI.Calendar;
using System.IO;
using Aspose.Words;
using System.Diagnostics;

namespace BankProject
{
    public partial class NormalLCEdit : System.Web.UI.UserControl
    {
        BaseNormalLCChargeTabs _chargesControl;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadChargesControl();

            if (IsPostBack) return;
            LoadToolBar();
            //tbAppAddr.Visible = false;
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;
            //ChiLoan keu hidden
            tbAdviseBankAddr.Visible = false;
            tbReimbBankAddr.Visible = false;
            tbAvailWithNameAddr.Visible = false;
            tbApplicantBank.Visible = false;

            rcbLCType.Items.Clear();
            rcbLCType.Items.Add(new RadComboBoxItem(""));
            rcbLCType.DataTextField = "LCTYPE";
            rcbLCType.DataValueField = "LCTYPE";
            rcbLCType.DataSource = DataProvider.DataTam.B_BLCTYPES_GetAll();
            rcbLCType.DataBind();

            rcCommodity.Items.Clear();
            rcCommodity.Items.Add(new RadComboBoxItem(""));
            rcCommodity.DataTextField = "ID";
            rcCommodity.DataValueField = "ID";
            rcCommodity.DataSource = DataProvider.DataTam.B_BCOMMODITY_GetAll();
            rcCommodity.DataBind();


            DataSet dsc = DataProvider.DataTam.B_BCUSTOMERS_GetAll();
            BindComboCustomer(dsc, rcbApplicantID);
            BindComboCustomer(dsc, rcbAccountOfficer);
            BindComboCustomer(dsc, comBeneficiary59);

            //Data load tu swift code
            //combo42DDrawee.DataSource = dsc;
            //combo42DDrawee.DataTextField = "CustomerName";
            //combo42DDrawee.DataValueField = "CustomerID";
            //combo42DDrawee.DataBind();

            DataSet dsSwiftCode = DataProvider.SQLData.B_BSWIFTCODE_GetAll();

            BindComboSwift(dsSwiftCode, combo42DDrawee);
            BindComboSwift(dsSwiftCode, comboRevivingBank);
            BindComboSwift(dsSwiftCode, tbAdviseThruNo);
            BindComboSwift(dsSwiftCode, rcbAdviseBankNo);
            BindComboSwift(dsSwiftCode, rcbReimbBankNo);
            BindComboSwift(dsSwiftCode, rcbAvailWithNo);
            BindComboSwift(dsSwiftCode, comboAvailableWith);

            DateTime now = DateTime.Now;

            tbIssuingDate.SelectedDate = now;
            tbExpiryDate.SelectedDate = now;
            tbContingentExpiry.SelectedDate = now.AddDays(15);
            dteDateOfIssue.SelectedDate = now;
            dteMT700DateAndPlaceOfExpiry.SelectedDate = now;
            tbLatesDateofShipment.SelectedDate = now;

            dteDateOfIssue.Enabled = false;
            dteMT700DateAndPlaceOfExpiry.Enabled = false;
            tbPlaceOfExpiry.Enabled = false;

            comboCurrencyCode32B.Enabled = false;
            numAmount.Enabled = false;
            numPercentCreditAmount1.Enabled = false;
            numPercentCreditAmount2.Enabled = false;
            //Cho chinh sua
            //tbIssuingDate.Enabled = false;
            //tbExpiryDate.Enabled = false;

            //tbContingentExpiry.Enabled = false;
            //dteDateOfIssue.Enabled = false;
            //dteMT700DateAndPlaceOfExpiry.Enabled = false;
            tbLatesDateofShipment.Enabled = false;

            if (Request.QueryString["CodeID"] != null)
            {
                tbEssurLCCode.Text = Request.QueryString["CodeID"].ToString();
                LoadTabMt700();
                LoadTabMt740();
                LoadNomalLC();
                RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            }
            else
            {
                DataSet ds = DataProvider.DataTam.B_ISSURLC_GetNewID();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tbEssurLCCode.Text = ds.Tables[0].Rows[0]["Code"].ToString();
                    Session["DataKey"] = tbEssurLCCode.Text;
                }
                DataSet vatno = DataProvider.Database.B_BMACODE_GetNewSoTT("VATNO");
                tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
            }

            if (Request.QueryString["disable"] != null)
            {
                hdfDisable.Value = "1";
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                RadToolBar1.FindItemByValue("btPreview").Enabled = true;
            }

            //TextBox4.Text = DateTime.Now.ToString("");
        }

        static void BindCombo(object dataSource, RadComboBox combo, string textField, string valueField)
        {
            combo.DataSource = dataSource;
            combo.DataTextField = textField;
            combo.DataValueField = valueField;
            combo.DataBind();
        }
        static void BindComboCustomer(object dataSource, RadComboBox combo)
        {
            BindCombo(dataSource, combo, "CustomerName", "CustomerID");
        }
        static void BindComboSwift(object dataSource, RadComboBox combo)
        {
            BindCombo(dataSource, combo, "CodeDescription", "Code");
        }


        public string getCodeLC()
        {
            return "Test phat nao";
        }
        public string getKeyID()
        {
            return tbEssurLCCode.Text;
        }
        protected void rcChargeAcct_SelectIndexChange(object sender, EventArgs e)
        {
            lblCommodity.Text = rcCommodity.SelectedItem.Attributes["Name"].ToString();
        }
        protected void rcChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["CBank"] = row["CBank"].ToString();
        }
        protected void rcbApplicantID_SelectIndexChange(object sender, EventArgs e)
        {
            RadComboBoxItem row = rcbApplicantID.SelectedItem;
            tbApplicantName.Text = row.Attributes["CustomerName"];
            tbApplicantAddr.ReLoadControl(rcbApplicantID.SelectedValue);
            //lblCustomer.Text = rcbApplicantID.SelectedValue.ToString();
            //tbAppAddr.Visible = true;
            //tbApplicantAddr.Text = rcbApplicantID.SelectedItem.Text.Replace("0", "").Replace("1", "").Replace("-","").Replace("2","");
            //tbApplicant50.Text = rcbApplicantID.SelectedItem.Text;
            //lblApplicant50.Text = rcbApplicantID.SelectedItem.Text;
        }
        protected void rcCommodity_SelectIndexChange(object sender, EventArgs e)
        {
            lblCommodity.Text = rcCommodity.SelectedItem.Attributes["Name"].ToString();
        }
        protected void rcbLCType_SelectIndexChange(object sender, EventArgs e)
        {
            lblLCType.Text = rcbLCType.SelectedItem.Attributes["Description"].ToString();
        }
        protected void rcCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name"] = row["Name2"].ToString();
        }
        protected void rcbLCType_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["LCTYPE"] = row["LCTYPE"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
            e.Item.Attributes["Category"] = row["Category"].ToString();
        }
        private void LoadToolBar()
        {
            //RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            //RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            if (Request.QueryString["CodeID"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            string commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                DataProvider.Database.B_BNORMAILLC_Insert(tbEssurLCCode.Text, rcbLCType.SelectedItem.Text, rcbApplicantID.SelectedValue, tbApplicantName.Text, "", "", ""
                   , rcbApplicantAcct.SelectedValue, rcbCcyAmount.SelectedValue, ntSoTien.Text, tbcrTolerance.Text, tbdrTolerance.Text, tbIssuingDate.SelectedDate.ToString(), tbExpiryDate.SelectedDate.ToString(), tbExpiryPlace.Text
                    , tbContingentExpiry.SelectedDate.ToString(), lblPayType.Text, lblPaymentpCt.Text, lblPaymentPortion.Text, "", tbLimitRef.SelectedValue, rcbBeneficiaryDetails.SelectedValue, ""
                    , "", "", tbAdviseBankRef.Text, rcbAdviseBankNo.SelectedValue, "", "", "", rcbAdviseBankAcct.SelectedValue, rcbReimbBankNo.SelectedValue,
                    "", "", "", tbReimbBankAcct.Text, tbAdviseThruNo.Text, "", "", "", rcbAdviseThruAcct.Text, rcbAvailWithNo.SelectedValue,
                    "", rcCommodity.SelectedValue, tbProv.Text, "", this.UserId.ToString(), rcbAccountOfficer.SelectedValue, tbContactNo.Text, tbLcAmountSecured.Text,
                tbLcAmountUnSecured.Text, tbLoanPrincipal.Text);
                // insert tab MT700 - 740
                InsertTabMt700();
                InsertTabMt740();
                if (tbChargeCode.Text != "")
                {
                    DataProvider.Database.B_BNORMALLCCHARGES_Insert(tbEssurLCCode.Text, tbWaiveCharges.Text, tbChargeCode.SelectedValue, rcbChargeAcct.SelectedItem.Text, tbChargePeriod.Text,
                        rcbChargeCcy.SelectedValue, tbExcheRate.Text, tbChargeAmt.Text, rcbPartyCharged.SelectedValue, rcbOmortCharge.SelectedValue, "", "",
                        rcbChargeStatus.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode.Text, lblTaxCcy.Text, lblTaxAmt.Text, "", "", "1");
                }
                if (tbChargecode2.Text != "")
                {
                    DataProvider.Database.B_BNORMALLCCHARGES_Insert(tbEssurLCCode.Text, tbWaiveCharges.Text, tbChargecode2.SelectedValue, rcbChargeAcct2.SelectedItem.Text, tbChargePeriod2.Text,
                        rcbChargeCcy2.SelectedValue, tbExcheRate2.Text, tbChargeAmt2.Text, rcbPartyCharged2.SelectedValue, rcbOmortCharges2.SelectedValue, "", "",
                        rcbChargeStatus2.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode2.Text, lblTaxCcy2.Text, lblTaxAmt2.Text, "", "", "2");
                }
                if (hdfDisable.Value == "0")
                {
                    DataProvider.KhanhND.B_BNORMAILLC_UpdateStatus("UNA", tbEssurLCCode.Text);
                    Response.Redirect(EditUrl("ph") + "&lid=" + tbEssurLCCode.Text);
                }
                else
                {
                    Response.Redirect(EditUrl("ph") + "&lid=" + tbEssurLCCode.Text + "&disable=1");
                }
            }
            else if (commandName == "print")
            {

                InDuLieu();

            }
            else if (commandName == "authorize")
            {
                DataProvider.KhanhND.B_BNORMAILLC_UpdateStatus("AUT", tbEssurLCCode.Text);
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            }
            else if (commandName == "reverse")
            {
                DataProvider.KhanhND.B_BNORMAILLC_UpdateStatus("REV", tbEssurLCCode.Text);
                RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            }
            else if (commandName == "Preview")
            {
                Response.Redirect("Default.aspx?tabid=92&ctl=reviewlist&mid=462");
            }
            else if (commandName == "search")
            {
                LoadTabMt700();
                LoadTabMt740();
                LoadNomalLC();
            }
        }

        private void InDuLieu()
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/MT700.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = DataProvider.KhanhND.B_BNORMAILLC_Print(tbEssurLCCode.Text);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 

            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            doc.Save("MT700_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        protected void tbContingentExpiry_TextChanged(object sender, EventArgs e)
        {
            //tbContingentExpiry.Text = DateTime.ParseExact(tbContingentExpiry.Text, "dd MMM yyyy", null).AddDays(30).ToString();
        }
        protected void tbExpiryDate_SelectedDateChange(object sender, SelectedDatesEventArgs e)
        {
            tbContingentExpiry.SelectedDate = DateTime.Parse(tbExpiryDate.SelectedDate.ToString()).AddDays(30);
        }
        protected void NavigationChanged(object sender, DefaultViewChangedEventArgs e)
        {
            tbContingentExpiry.SelectedDate = DateTime.Parse(tbExpiryDate.SelectedDate.ToString()).AddDays(30);
        }

        protected void ntSoTien_TextChanged(object sender, EventArgs e)
        {
            lblPaymentpCt.Text = "100.00";
            lblPaymentPortion.Text = ntSoTien.Text;
            lblPayType.Text = "SP";
            tbcrTolerance.Focus();
        }
        protected void rcbAvailWithNo_SelectIndexChange(object sender, EventArgs e)
        {
            lblAvailWithNo.Text = rcbAvailWithNo.SelectedValue.ToString();
            if (rcbAvailWithNo.SelectedValue.ToString() != "")
                tbAvailWithNameAddr.Enabled = false;
            else
                tbAvailWithNameAddr.Enabled = true;
        }
        protected void rcbAdviseBankNo_SelectIndexChange(object sender, EventArgs e)
        {
            lblAdviseBankNo.Text = rcbAdviseBankNo.SelectedValue.ToString();
        }
        protected void rcbReimbBankNo_SelectIndexChange(object sender, EventArgs e)
        {
            lblReimbBankNo.Text = rcbReimbBankNo.SelectedValue.ToString();
        }
        protected void tbAdviseThruNo_SelectIndexChange(object sender, EventArgs e)
        {
            lblAdviseThruNo.Text = tbAdviseThruNo.SelectedValue.ToString();
        }
        protected void rcbChargeStatus_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
        protected void rcbChargeStatus2_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, EventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedValue.ToString();
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, EventArgs e)
        {
            lblPartyCharged.Text = rcbPartyCharged.SelectedValue.ToString();
        }
        protected void rcbChargeAcct_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeAcct.Text = "TKTT VND " + rcbChargeAcct.SelectedValue.ToString();
        }
        protected void rcbChargeAcct2_SelectIndexChange(object sender, EventArgs e)
        {
            lblChargeAcct2.Text = "TKTT VND " + rcbChargeAcct2.SelectedValue.ToString();
        }
        protected void tbWaiveCharges_TextChanged(object sender, EventArgs e)
        {
            if (tbWaiveCharges.Text.ToUpper() == "NO")
            {
                tbChargeAmt.Visible = true;
                tbChargeCode.Visible = true;
                tbChargePeriod.Visible = true;
                tbChargeRemarks.Visible = true;
                tbExcheRate.Visible = true;
                tbVatNo.Visible = true;
                rcbChargeAcct.Visible = true;
                rcbChargeStatus.Visible = true;
                rcbOmortCharge.Visible = true;
                rcbPartyCharged.Visible = true;
                btThem.Visible = true;
                rcbChargeCcy.Visible = true;
                tbChargeCode.Focus();
            }
            else if (tbWaiveCharges.Text.ToUpper() == "YES")
            {
                btThem.Visible = false;
                tbChargeAmt.Visible = false;
                tbChargeCode.Visible = false;
                tbChargePeriod.Visible = false;
                tbChargeRemarks.Visible = false;
                tbExcheRate.Visible = false;
                tbVatNo.Visible = false;
                rcbChargeAcct.Visible = false;
                rcbChargeStatus.Visible = false;
                rcbOmortCharge.Visible = false;
                rcbPartyCharged.Visible = false;
                rcbChargeCcy.Visible = false;
            }
        }

        protected void OnWaiveChargesChange(object sender, EventArgs e)
        {
            var yes = !this.chkWaiveCharges.Checked;
            tbChargeAmt.Visible = yes;
            tbChargeCode.Visible = yes;
            tbChargePeriod.Visible = yes;
            tbChargeRemarks.Visible = yes;
            tbExcheRate.Visible = yes;
            tbVatNo.Visible = yes;
            rcbChargeAcct.Visible = yes;
            rcbChargeStatus.Visible = yes;
            rcbOmortCharge.Visible = yes;
            rcbPartyCharged.Visible = yes;
            btThem.Visible = yes;
            rcbChargeCcy.Visible = yes;

            if (yes)
                tbChargeCode.Focus();
        }

        //Aspose.Pdf.License license = new Aspose.Pdf.License();
        //license.SetLicense("Aspose.Cells.lic");
        //Aspose.Pdf.Generator.Pdf pdf = new Aspose.Pdf.Generator.Pdf();
        //// add the section to PDF document sections collection
        //Aspose.Pdf.Generator.Section section = pdf.Sections.Add();

        //// Read the contents of HTML file into StreamReader object
        //StreamReader r = File.OpenText(Server.MapPath("DesktopModules/TrainingCoreBanking/BankProject/Report/Template/NormalLC/HtmlPage1.html"));
        ////Create text paragraphs containing HTML text
        //Aspose.Pdf.Generator.Text text2 = new Aspose.Pdf.Generator.Text(section, r.ReadToEnd());
        //// enable the property to display HTML contents within their own formatting
        //text2.IsHtmlTagSupported = true;
        ////Add the text paragraphs containing HTML text to the section
        //section.Paragraphs.Add(text2);
        //// Specify the URL which serves as images database
        //pdf.HtmlInfo.ImgUrl = Server.MapPath("DesktopModules/TrainingCoreBanking/BankProject/Report/Export/");

        ////Save the pdf document
        //pdf.Save(Server.MapPath("DesktopModules/TrainingCoreBanking/BankProject/Report/Export/HTML2pdf.pdf"));
        //protected void tbChargeAcct_TextChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = DataProvider.DataTam.B_BCUSTOMERS_GetbyID(tbChargeAcct.Text.ToUpper().Replace("CAVND",""));
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        tbChargeAcct.Text = ds.Tables[0].Rows[0]["BankCode"].ToString();
        //        lblChargeAcct.Text = ds.Tables[0].Rows[0]["CBank"].ToString();
        //    }
        //}
        //protected void tbChargeAcct2_TextChanged(object sender, EventArgs e)
        //{
        //    DataSet ds = DataProvider.DataTam.B_BCUSTOMERS_GetbyID(tbChargeAcct2.Text.ToUpper().Replace("CAVND", ""));
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        tbChargeAcct2.Text = ds.Tables[0].Rows[0]["BankCode"].ToString();
        //        lblChargeAcct2.Text = ds.Tables[0].Rows[0]["CBank"].ToString();
        //    }
        //}
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            double tigia = double.Parse(tbExcheRate.Text);
            double sotien = double.Parse(tbChargeAmt.Text);

            sotien = sotien * tigia;
            //sotien = sotien / 1.1;
            float kq = (float)(sotien / 1.1);
            tbChargeAmt.Text = kq.ToString();
            lblTaxAmt.Text = String.Format("{0:#,##0}", (float)(sotien - kq));

            lblTaxCode.Text = "81      10% VAT on Charge";
            lblTaxCcy.Text = rcbChargeCcy.SelectedValue;
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            double tigia = double.Parse(tbExcheRate2.Text);
            double sotien = double.Parse(tbChargeAmt2.Text);

            sotien = sotien * tigia;
            //sotien = sotien / 1.1;
            float kq = (float)(sotien / 1.1);
            tbChargeAmt2.Text = kq.ToString();
            lblTaxAmt2.Text = String.Format("{0:#,##0}", (float)(sotien - kq));

            lblTaxCode2.Text = "81      10% VAT on Charge";
            lblTaxCcy2.Text = rcbChargeCcy2.SelectedValue;
        }

        protected void btThem_Click(object sender, ImageClickEventArgs e)
        {
            divCharge2.Visible = true;
            divChargeInfo2.Visible = true;
        }

        #region tab MT700
        protected void comboRevivingBank_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbRevivingBankName.Text = comboRevivingBank.SelectedValue;
        }
        protected void comboFormOfDocumentaryCredit_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbFormOfDocumentaryCreditName.Text = comboFormOfDocumentaryCredit.SelectedValue;
        }
        //protected void comboAvailableRule_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    tbAvailableRuleName.Text = comboAvailableRule.SelectedValue;
        //}

        protected void comboAvailableWithBy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
        }
        protected void LoadTabMt700()
        {
            var dsMT700 = DataProvider.SQLData.B_BNORMAILLCMT700_GetByNormalLCCode(tbEssurLCCode.Text.Trim());
            if (dsMT700 != null && dsMT700.Tables.Count > 0 && dsMT700.Tables[0].Rows.Count > 0)
            {
                var drMT700 = dsMT700.Tables[0].Rows[0];

                comboRevivingBank.SelectedValue = drMT700["RevevingBank"].ToString();
                tbRevivingBankName.Text = drMT700["RevevingBank"].ToString();

                tbBaquenceOfTotal.Text = drMT700["BounceOfTotal"].ToString();
                comboFormOfDocumentaryCredit.SelectedValue = drMT700["FormOfDocumentaryCredit"].ToString();
                tbFormOfDocumentaryCreditName.Text = comboFormOfDocumentaryCredit.SelectedValue;

                if (drMT700["DateOfIssue"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDateOfIssue.SelectedDate = Convert.ToDateTime(drMT700["DateOfIssue"].ToString());
                }
                if (drMT700["Date31D"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteMT700DateAndPlaceOfExpiry.SelectedDate = Convert.ToDateTime(drMT700["Date31D"].ToString());
                }
                tbPlaceOfExpiry.Text = drMT700["PlaceOfExpiry31D"].ToString();

                //tbApplicantBank.Text = drMT700["ApplicantBank51"].ToString();
                tbApplicant50.Text = drMT700["Applicant50"].ToString();

                comBeneficiary59.SelectedValue = drMT700["DocumentaryCusNo"].ToString();
                tbDocumentary_NameAndAddress.Text = drMT700["DocumentaryNameAddress"].ToString();

                comboCurrencyCode32B.SelectedValue = drMT700["CurrencyCode32B"].ToString();
                if (!string.IsNullOrEmpty(drMT700["Amount32B"].ToString()))
                {
                    numAmount.Value = Convert.ToDouble(drMT700["Amount32B"].ToString());
                }
                else
                {
                    numAmount.Value = 0;
                }


                if (!string.IsNullOrEmpty(drMT700["PercentCreditAmount39A1"].ToString()))
                {
                    numPercentCreditAmount1.Value = Convert.ToDouble(drMT700["PercentCreditAmount39A1"].ToString());
                }
                else
                {
                    numPercentCreditAmount1.Value = 0;
                }

                if (!string.IsNullOrEmpty(drMT700["PercentCreditAmount39A2"].ToString()))
                {
                    numPercentCreditAmount2.Value = Convert.ToDouble(drMT700["PercentCreditAmount39A2"].ToString());
                }
                else
                {
                    numPercentCreditAmount2.Value = 0;
                }

                comboMaximumCreditAmount39B.SelectedValue = drMT700["MaximumCreditAmount39B"].ToString();
                //tbAdditionalAmountComment.Text = drMT700["AdditionalAmountComment"].ToString();

                comboAvailableRule.SelectedValue = drMT700["AvailableRule40E"].ToString();
                tbAvailableRuleName.Text = comboAvailableRule.SelectedValue;

                comboAvailableWith.SelectedValue = drMT700["AvailableWith41A"].ToString();

                tbAvailableWithNameAddress.Text = drMT700["AvailableWithNameAddress"].ToString();

                tb42CDraftsAt.ReLoadControl(tbEssurLCCode.Text);// = drMT700["C42"].ToString();

                combo42DDrawee.SelectedValue = drMT700["D42"].ToString();
                //tb42DDraweeName.Text = combo42DDrawee.SelectedValue;

                tbMixedPaymentDetails.Text = drMT700["MixedPaymentDetails"].ToString();

                tbDeferredPaymentDetails.Text = drMT700["DeferredPaymentDetails"].ToString();
                rcbPatialShipment.SelectedValue = drMT700["PatialShipment"].ToString();
                rcbTranshipment.SelectedValue = drMT700["Transhipment"].ToString();
                tbPlaceoftakingincharge.Text = drMT700["Placeoftakingincharge"].ToString();
                tbPortofloading.Text = drMT700["Portofloading"].ToString();
                tbPortofDischarge.Text = drMT700["PortofDischarge"].ToString();
                tbPlaceoffinalindistination.Text = drMT700["Placeoffinalindistination"].ToString();
                //tbLatesDateofShipment.SelectedDate = DateTime.Parse(drMT700["LatesDateofShipment"].ToString());
                tbShipmentPeriod.Text = drMT700["ShipmentPeriod"].ToString();
                tbDescrpofGoods.Text = drMT700["DescrpofGoods"].ToString();
                rcbDocsRequired.SelectedValue = drMT700["DocsRequired"].ToString();
                tbOrderDocs.Text = drMT700["OrderDocs"].ToString();
                tbAdditionalConditions.Text = drMT700["AdditionalConditions"].ToString();
                //,[AdditionalConditions]

                //LCType
                comboAvailableWithBy.SelectedValue = drMT700["AvailableWithBy"].ToString();
                tbAvailableWithByName.Text = comboAvailableWithBy.SelectedValue;
                if (tbAvailableWithByName.Text == "")
                {
                    switch (drMT700["LCType"].ToString())
                    {
                        case "ILCP":
                            tbAvailableWithByName.Text = "BY PAYMENT";
                            comboAvailableWithBy.SelectedValue = "BY PAYMENT";
                            break;
                        case "ILCS":
                            tbAvailableWithByName.Text = "BY NEGPTIATION";
                            comboAvailableWithBy.SelectedValue = "BY NEGPTIATION";
                            break;
                    }
                }
            }
            else
            {
                comboRevivingBank.SelectedValue = string.Empty;
                tbAvailableRuleName.Text = string.Empty;

                tbBaquenceOfTotal.Text = string.Empty;
                comboFormOfDocumentaryCredit.SelectedValue = string.Empty;
                tbFormOfDocumentaryCreditName.Text = string.Empty;
                dteDateOfIssue.SelectedDate = null;
                dteMT700DateAndPlaceOfExpiry.SelectedDate = null;
                tbPlaceOfExpiry.Text = string.Empty;

                tbApplicantBank.ReLoadControl(tbEssurLCCode.Text);
                tbApplicant50.Text = string.Empty;

                comBeneficiary59.SelectedValue = string.Empty;
                tbDocumentary_NameAndAddress.Text = string.Empty;

                comboCurrencyCode32B.SelectedValue = string.Empty;
                numAmount.Value = 0;
                numPercentCreditAmount1.Value = 0;
                numPercentCreditAmount2.Value = 0;

                comboMaximumCreditAmount39B.SelectedValue = string.Empty;
                tbAdditionalAmountComment.ReLoadControl(tbEssurLCCode.Text);// = string.Empty;

                comboAvailableRule.SelectedValue = string.Empty;
                tbAvailableRuleName.Text = string.Empty;

                comboAvailableWith.SelectedValue = string.Empty;

                tbAvailableWithNameAddress.Text = string.Empty;

                tb42CDraftsAt.ReLoadControl(tbEssurLCCode.Text);

                combo42DDrawee.SelectedValue = string.Empty;
                //tb42DDraweeName.Text = string.Empty;

                tbMixedPaymentDetails.Text = string.Empty;

                //LCType
                comboAvailableWithBy.SelectedValue = string.Empty;
                tbAvailableWithByName.Text = string.Empty;
            }
        }
        protected void InsertTabMt700()
        {
            DataProvider.SQLData.B_BNORMAILLCMT700_Insert(tbEssurLCCode.Text.Trim(), comboRevivingBank.SelectedValue, tbBaquenceOfTotal.Text.Trim(), comboFormOfDocumentaryCredit.SelectedValue,
                dteDateOfIssue.SelectedDate.ToString(), dteMT700DateAndPlaceOfExpiry.SelectedDate.ToString(), tbPlaceOfExpiry.Text.Trim(), "", tbApplicant50.Text.Trim(),
                comBeneficiary59.SelectedValue, tbDocumentary_NameAndAddress.Text.Trim(), comboCurrencyCode32B.SelectedValue, numAmount.Value.ToString(),
                numPercentCreditAmount1.Value.ToString(), numPercentCreditAmount2.Value.ToString(), "", comboAvailableRule.Text,
                comboAvailableWith.SelectedValue, tbAvailableWithNameAddress.Text.Trim(), "",
                combo42DDrawee.SelectedValue, comboMaximumCreditAmount39B.SelectedValue, tbMixedPaymentDetails.Text.Trim(), comboAvailableWithBy.SelectedValue
                , tbDeferredPaymentDetails.Text, rcbPatialShipment.SelectedValue, rcbTranshipment.SelectedValue, tbPlaceoftakingincharge.Text, tbPortofloading.Text, tbPortofDischarge.Text
                , tbPlaceoffinalindistination.Text, tbLatesDateofShipment.SelectedDate.ToString(), tbShipmentPeriod.Text, tbDescrpofGoods.Text, rcbDocsRequired.SelectedValue,
                tbOrderDocs.Text, tbAdditionalConditions.Text, tbCharges.Text, tbPeriodforPresentation.Text, rcbConfimationInstructions.SelectedValue, tbNegotgBank.Text, tbSendertoReceiverInfomation.Text);
        }
        #endregion

        #region MT740
        protected void comboReceivingBankMT740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbReceivingBankMT740Name.Text = comboReceivingBankMT740.SelectedValue;
        }
        protected void comboDrawee42D_MT740_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            tbDraweeName42D_MT740.Text = comboDrawee42D_MT740.SelectedValue;
        }
        protected void LoadTabMt740()
        {
            var dsMT740 = DataProvider.SQLData.B_BNORMAILLCMT740_GetByNormalLCCode(tbEssurLCCode.Text.Trim());
            if (dsMT740 != null && dsMT740.Tables.Count > 0 && dsMT740.Tables[0].Rows.Count > 0)
            {
                var drRow = dsMT740.Tables[0].Rows[0];

                comGenerate.SelectedValue = drRow["Generate"].ToString();

                comboReceivingBankMT740.SelectedValue = drRow["ReceivingBank"].ToString();
                tbReceivingBankMT740Name.Text = comboReceivingBankMT740.SelectedValue;

                tbDocumentaryCreditNumber.Text = drRow["DocumentaryCreditNumber"].ToString();

                if (drRow["Date31D"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dte31DDate.SelectedDate = Convert.ToDateTime(drRow["Date31D"].ToString());
                }
                tb31DPlaceOfExpiry.Text = drRow["PlaceOfExpiry"].ToString();
                comboBeneficial.SelectedValue = drRow["Beneficial"].ToString();
                tbBeneficialNameAddress.Text = drRow["BeneficialNameAndAddress"].ToString();
                comboCredit32USD.SelectedValue = drRow["CreditMoneyType32"].ToString();

                if (!string.IsNullOrEmpty(drRow["CreditAmount32"].ToString()))
                {
                    numUSDAmount.Value = Convert.ToDouble(drRow["CreditAmount32"].ToString());
                }
                else
                {
                    numUSDAmount.Value = 0;
                }

                comboAvailableWith_MT740.SelectedValue = drRow["AvailableWith41A"].ToString();
                tbAvailableNameAddr_MT740.Text = drRow["AvailableNameAndAddress"].ToString();
                tb42CDraff.Text = drRow["Draffy42C"].ToString();

                comboDrawee42D_MT740.SelectedValue = drRow["Drawee42D"].ToString();
                tbDraweeName42D_MT740.Text = comboDrawee42D_MT740.SelectedValue;

                tbNameAddress.Text = drRow["NameAddress"].ToString(); // chua co trong store
                comboBankChange.SelectedValue = drRow["BankChanges"].ToString();
                tbSenderReceiverInformation.Text = drRow["SenderToReceiverIinformation"].ToString();
            }
            else
            {
                comGenerate.SelectedValue = string.Empty;

                comboReceivingBankMT740.SelectedValue = string.Empty;
                tbReceivingBankMT740Name.Text = string.Empty;

                tbDocumentaryCreditNumber.Text = string.Empty;
                dte31DDate.SelectedDate = null;
                tb31DPlaceOfExpiry.Text = string.Empty;
                comboBeneficial.SelectedValue = string.Empty;
                tbBeneficialNameAddress.Text = string.Empty;
                comboCredit32USD.SelectedValue = string.Empty;

                numUSDAmount.Value = 0;

                comboAvailableWith_MT740.SelectedValue = string.Empty;
                tbAvailableNameAddr_MT740.Text = string.Empty;
                tb42CDraff.Text = string.Empty;

                comboDrawee42D_MT740.SelectedValue = string.Empty;
                tbDraweeName42D_MT740.Text = string.Empty;

                tbNameAddress.Text = string.Empty;
                comboBankChange.SelectedValue = string.Empty;
                tbSenderReceiverInformation.Text = string.Empty;
            }
        }
        protected void InsertTabMt740()
        {
            DataProvider.SQLData.B_BNORMAILLCMT740_Insert(tbEssurLCCode.Text.Trim(), comGenerate.SelectedValue, comboReceivingBankMT740.SelectedValue,
                tbDocumentaryCreditNumber.Text.Trim(), dte31DDate.SelectedDate.ToString(), tb31DPlaceOfExpiry.Text.Trim(), comboBeneficial.SelectedValue, tbBeneficialNameAddress.Text.Trim(),
                comboCredit32USD.SelectedValue, numUSDAmount.Value.ToString(), comboAvailableWith_MT740.SelectedValue, tbAvailableNameAddr_MT740.Text.Trim(), tb42CDraff.Text.Trim(),
                comboDrawee42D_MT740.SelectedValue, comboBankChange.SelectedValue, tbSenderReceiverInformation.Text.Trim(), tbNameAddress.Text.Trim());
        }
        #endregion
        //protected void tbEssurLCCode_OnTextChanged(object sender, EventArgs e)
        //{
        //    //OnTextChanged="tbEssurLCCode_OnTextChanged" AutoPostBack="True"
        //    LoadTabMt700();
        //    LoadTabMt740();
        //}
        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadTabMt700();
            LoadTabMt740();
            LoadNomalLC();
        }
        public string stbApplicantAddr = "";
        public string getGiaTriDynamic(string KeyName)
        {
            string giatri = "";
            switch (KeyName)
            {
                case "ApplicantAddr":
                    {
                        giatri = KeyName;
                        break;
                    }
            }
            return giatri;
        }
        private void LoadNomalLC()
        {
            DataSet ds = DataProvider.Database.B_BNORMAILLC_GetbyNormalLCCode(tbEssurLCCode.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rcbLCType.SelectedValue = ds.Tables[0].Rows[0]["LCType"].ToString();
                lblLCType.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                rcbApplicantID.SelectedValue = ds.Tables[0].Rows[0]["ApplicantID"].ToString();
                tbApplicantName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                tbApplicantAddr.ReLoadControl(rcbApplicantID.SelectedValue);
                //lblCustomer.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                //tbApplicantAddr.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();

                stbApplicantAddr = ds.Tables[0].Rows[0]["CustomerName"].ToString();


                //tbAppAddr.Visible = true;
                rcbApplicantAcct.SelectedValue = ds.Tables[0].Rows[0]["ApplicantAcct"].ToString();

                rcbCcyAmount.SelectedValue = ds.Tables[0].Rows[0]["CcyAmount"].ToString();
                ntSoTien.Text = ds.Tables[0].Rows[0]["Sotien"].ToString();
                tbcrTolerance.Text = ds.Tables[0].Rows[0]["CrTolerance"].ToString();
                tbdrTolerance.Text = ds.Tables[0].Rows[0]["DrTolerance"].ToString();
                tbIssuingDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["IssuingDate"].ToString());
                tbExpiryDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ExpiryDate"].ToString());
                tbExpiryPlace.Text = ds.Tables[0].Rows[0]["ExpiryPlace"].ToString();
                tbContingentExpiry.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContingentExpiry"].ToString());
                lblPayType.Text = ds.Tables[0].Rows[0]["PayType"].ToString();
                lblPaymentpCt.Text = ds.Tables[0].Rows[0]["PaymentpCt"].ToString();
                lblPaymentPortion.Text = ds.Tables[0].Rows[0]["PaymentPortion"].ToString();
                tbLimitRef.SelectedValue = ds.Tables[0].Rows[0]["LimitRef"].ToString();
                rcbBeneficiaryDetails.SelectedValue = ds.Tables[0].Rows[0]["BeneficiaryNo"].ToString();
                //tbBeneficiaryNameAddr1.Text = ds.Tables[0].Rows[0]["BeneficiaryNameAddr"].ToString();
                //tbBeneficiaryNameAddr1.Text = ds.Tables[0].Rows[0]["BeneficiaryNameAddr1"].ToString();
                tbAdviseBankRef.Text = ds.Tables[0].Rows[0]["AdviseBankRef"].ToString();

                rcbAdviseBankNo.SelectedValue = ds.Tables[0].Rows[0]["AdviseBankNo"].ToString();
                lblAdviseBankNo.Text = ds.Tables[0].Rows[0]["AdviseBankNo"].ToString();

                tbAdviseBankAddr.ReLoadControl(tbEssurLCCode.Text); // = ds.Tables[0].Rows[0]["AdviseBankAddr1"].ToString();
                rcbAdviseBankAcct.SelectedValue = ds.Tables[0].Rows[0]["AdviseBankAcct"].ToString();
                rcbReimbBankNo.SelectedValue = ds.Tables[0].Rows[0]["ReimbBankNo"].ToString();
                tbReimbBankAddr.ReLoadControl(tbEssurLCCode.Text); // = ds.Tables[0].Rows[0]["ReimbBankAddr1"].ToString();

                tbReimbBankAcct.SelectedValue = ds.Tables[0].Rows[0]["ReimbBankAcct"].ToString();
                tbAdviseThruNo.SelectedValue = ds.Tables[0].Rows[0]["AdviseThruNo"].ToString();
                tbAdviseThruAddr.ReLoadControl(tbEssurLCCode.Text); // = ds.Tables[0].Rows[0]["AdviseThruAddr1"].ToString();

                rcbAdviseThruAcct.SelectedValue = ds.Tables[0].Rows[0]["AdviseThruAcct"].ToString();
                rcbAvailWithNo.SelectedValue = ds.Tables[0].Rows[0]["AvailWithNo"].ToString();
                lblAvailWithNo.Text = ds.Tables[0].Rows[0]["AvailWithNo"].ToString();
                tbAvailWithNameAddr.ReLoadControl(tbEssurLCCode.Text); // = ds.Tables[0].Rows[0]["AvailWithNameAddr"].ToString();

                rcCommodity.SelectedValue = ds.Tables[0].Rows[0]["Commodity"].ToString();
                tbProv.Text = ds.Tables[0].Rows[0]["Prov"].ToString();
                //tbOldLCNo.Text = ds.Tables[0].Rows[0]["OldLCNo"].ToString();


                rcbAccountOfficer.SelectedValue = ds.Tables[0].Rows[0]["AccountOfficer"].ToString();
                tbContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                tbLcAmountSecured.Text = ds.Tables[0].Rows[0]["LcAmountSecured"].ToString();

                tbLcAmountUnSecured.Text = ds.Tables[0].Rows[0]["LcAmountUnSecured"].ToString();
                tbLoanPrincipal.Text = ds.Tables[0].Rows[0]["LoanPrincipal"].ToString();


                if (ds.Tables[0].Rows[0]["Status"].ToString() == "UNA" && Request.QueryString["disable"] != null)
                {
                    RadToolBar1.FindItemByValue("btReverse").Enabled = true;
                    RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;

                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                tbWaiveCharges.Text = ds.Tables[1].Rows[0]["WaiveCharges"].ToString();
                rcbChargeAcct.SelectedValue = ds.Tables[1].Rows[0]["CustomerName"].ToString();
                lblChargeAcct.Text = ds.Tables[1].Rows[0]["CustomerName"].ToString();
                tbChargePeriod.Text = ds.Tables[1].Rows[0]["ChargePeriod"].ToString();
                rcbChargeCcy.SelectedValue = ds.Tables[1].Rows[0]["ChargeCcy"].ToString();
                tbExcheRate.Text = ds.Tables[1].Rows[0]["ExchRate"].ToString();
                tbChargeAmt.Text = ds.Tables[1].Rows[0]["ChargeAmt"].ToString();
                rcbPartyCharged.SelectedValue = ds.Tables[1].Rows[0]["PartyCharged"].ToString();
                lblPartyCharged.Text = ds.Tables[1].Rows[0]["PartyCharged"].ToString();
                rcbOmortCharge.SelectedValue = ds.Tables[1].Rows[0]["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = ds.Tables[1].Rows[0]["ChargeStatus"].ToString();
                lblChargeStatus.Text = ds.Tables[1].Rows[0]["ChargeStatus"].ToString();

                tbChargeRemarks.Text = ds.Tables[1].Rows[0]["ChargeRemarks"].ToString();
                tbVatNo.Text = ds.Tables[1].Rows[0]["VATNo"].ToString();
                lblTaxCode.Text = ds.Tables[1].Rows[0]["TaxCode"].ToString();
                lblTaxCcy.Text = ds.Tables[1].Rows[0]["TaxCcy"].ToString();
                lblTaxAmt.Text = ds.Tables[1].Rows[0]["TaxAmt"].ToString();

                tbChargeCode.SelectedValue = ds.Tables[1].Rows[0]["Chargecode"].ToString();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                divCharge2.Visible = true;
                divChargeInfo2.Visible = true;
                rcbChargeAcct2.SelectedValue = ds.Tables[2].Rows[0]["CustomerName"].ToString();
                lblChargeAcct2.Text = ds.Tables[2].Rows[0]["CustomerName"].ToString();
                tbChargePeriod2.Text = ds.Tables[2].Rows[0]["ChargePeriod"].ToString();
                rcbChargeCcy2.SelectedValue = ds.Tables[2].Rows[0]["ChargeCcy"].ToString();
                tbExcheRate2.Text = ds.Tables[2].Rows[0]["ExchRate"].ToString();
                tbChargeAmt2.Text = ds.Tables[2].Rows[0]["ChargeAmt"].ToString();
                rcbPartyCharged2.SelectedValue = ds.Tables[2].Rows[0]["PartyCharged"].ToString();
                lblPartyCharged2.Text = ds.Tables[2].Rows[0]["PartyCharged"].ToString();
                rcbOmortCharges2.SelectedValue = ds.Tables[2].Rows[0]["OmortCharges"].ToString();
                rcbChargeStatus2.SelectedValue = ds.Tables[2].Rows[0]["ChargeStatus"].ToString();
                lblChargeStatus2.Text = ds.Tables[2].Rows[0]["ChargeStatus"].ToString();

                lblTaxCode2.Text = ds.Tables[2].Rows[0]["TaxCode"].ToString();
                lblTaxCcy2.Text = ds.Tables[2].Rows[0]["TaxCcy"].ToString();
                lblTaxAmt2.Text = ds.Tables[2].Rows[0]["TaxAmt"].ToString();

                tbChargecode2.SelectedValue = ds.Tables[2].Rows[0]["Chargecode"].ToString();
            }
        }

        protected void rcbApplicantID_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerName"] = row["CustomerName2"].ToString();
            e.Item.Attributes["Address"] = row["Address"].ToString();
            e.Item.Attributes["IdentityNo"] = row["IdentityNo"].ToString();
            e.Item.Attributes["IssueDate"] = row["IssueDate"].ToString();
            e.Item.Attributes["IssuePlace"] = row["IssuePlace"].ToString();
        }

        protected void tbAdviseThruNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "")
                tbAdviseThruAddr.SetEnable(true);
            else
            {
                tbAdviseThruAddr.SetEnable(false);
                tbAdviseThruAddr.Clear();
                tbAdviseThruAddr.ReLoadControl();
            }
        }

        #region //TN: Wrapper module
        internal DotNetNuke.Entities.Modules.PortalModuleBase PortalModule { get; set; }

        string EditUrl(string controlKey)
        {
            Debug.Assert(this.PortalModule != null, "The PortalModule instance cannot be null !");
            return PortalModule.EditUrl(controlKey);
        }


        int UserId
        {
            get
            {
                Debug.Assert(this.PortalModule != null, "The PortalModule instance cannot be null !");
                return this.PortalModule.UserId;
            }
        }

        #endregion

        #region //TN: Extended LC properties
        public int EditMode { get; set; }

        public string LCID {
            get
            {
                return this.tbEssurLCCode.Text;
            }
            set
            {
                this.tbEssurLCCode.Text = value;
            }
        }
        #endregion

        #region //TN: LC Toolbar
        void EnableToolbarButton(string buttonName, bool enable)
        {
            RadToolBar1.FindItemByValue(buttonName).Enabled = enable;
        }
        #endregion

        void LoadChargesControl()
        {
            _chargesControl = this.NormalLCChargeIssue;
            _chargesControl.Visible = true;
        }
    }
}