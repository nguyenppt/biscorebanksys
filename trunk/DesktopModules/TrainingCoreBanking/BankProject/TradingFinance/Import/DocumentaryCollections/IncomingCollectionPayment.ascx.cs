using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bd = BankProject.DataProvider;
using Telerik.Web.UI;
using bc = BankProject.Controls;

namespace BankProject.TradingFinance.Import.DocumentaryCollections
{
    public partial class IncomingCollectionPayment : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public string PartyCharged = string.Empty;
        public string CreateMT400 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            SetRelation_IntermediaryBank();
            SetRelation_AccountWithInstitution();
            SetRelation_BeneficiaryBank();
            SetRelation_ReceiverCorrespondent();
            SetRelation_SenderCorrespondent();
            
            RadToolBar1.FindItemByValue("btSave").Enabled = true;
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            RadToolBar1.FindItemByValue("btprint").Enabled = false;

            ReadOnlyTabMt400(false);

            LoadPartyCharged();

            LoadChargeCode();

            InitToolBar(false);

            bc.Commont.initRadComboBox(ref comboPresentorCusNo, "Name", "Name", bd.SQLData.B_BBANKING_GetAll());
            bc.Commont.initRadComboBox(ref comboCountryCode, "TenTA", "TenTA", bd.SQLData.B_BCOUNTRY_GetAll());
            var dsCurrency = bd.SQLData.B_BCURRENCY_GetAll();
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref rcbChargeCcy4, "Code", "Code", dsCurrency);
            bc.Commont.initRadComboBox(ref comboCurrency_MT103, "Code", "Code", dsCurrency);

            bc.Commont.initRadComboBox(ref comboOrderingCustAcc_MT103, "CustomerID", "CustomerID", bd.SQLData.B_BCUSTOMERS_OnlyBusiness());
            // BGIN MT 400
            
            SetDefaultValue();
            //END

            if (!string.IsNullOrEmpty(Request.QueryString["paycode"]))
            {
                txtCode.Text = Request.QueryString["paycode"];
                InitToolBar(true);

                LoadData();

                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
                RadToolBar1.FindItemByValue("btReview").Enabled = false;
            }
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "save":
                    if (CheckFtVaild() == false)
                    {
                        return;
                    }
                    if (B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt())
                    {
                        SaveData();

                        Response.Redirect("Default.aspx?tabid=221");
                    }
                    break;

                case "review":
                    Response.Redirect(EditUrl("colpaypreview"));
                    break;

                case "authorize":
                    if (CheckFtVaild() == false)
                    {
                        return;
                    }

                    bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_UpdateStatus(txtCode.Text.Trim(), "AUT", UserId.ToString());
                    
                    Response.Redirect("Default.aspx?tabid=221");
                    break;

                case "revert":
                    if (CheckFtVaild() == false)
                    {
                        return;
                    }
                    Revert();
                    break;
            }
        }

        protected void comboDRFromAccount_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }

        protected void comboDRFromAccount_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblDRFromAccountName.Text = comboDRFromAccount.SelectedItem.Attributes["CustomerName2"];
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            //LoadNostroAcct();
            LoadChargeAcct();
            LoadData();
            lblError.Text = "";
            // check coi du tien chua, neu DƯ/THIẾU thi ko cho payment nua
            //PaymentAmount: so tien goc
            //IncreaseMental: when AUT so tien se dc cong don len dua vao DrawingAmount

            //Total payment amount: tong so tien bio chubg tu PaymentAmount
            //Amt Credited:IncreaseMental = Credited amount = IncreaseMental

            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                var orginalCode = txtCode.Text.Trim().Substring(0, 14);
                var dtCheck = bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt(orginalCode, txtCode.Text.Trim());

                double paymentAmount = Double.Parse(dtCheck.Rows[0]["PaymentAmount"].ToString());
                double IncreaseMentalB4Aut = Double.Parse(dtCheck.Rows[0]["IncreaseMentalB4Aut"].ToString());

                if (IncreaseMentalB4Aut > 0 && paymentAmount == IncreaseMentalB4Aut &&
                    dtCheck.Rows[0]["Status"].ToString() == "AUT")
                {
                    txtCode.Text = orginalCode;
                    InitToolBar(false);
                    SetDisableByReview(false);
                    RadToolBar1.FindItemByValue("btSave").Enabled = false;
                    RadToolBar1.FindItemByValue("btReview").Enabled = true;
                    lblError.Text = "This Documentary has payment full";
                }
            }

            txtSendingBankTRN.Text = Cal_PaymentNo();
        }

        protected void commom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void SetDefaultValue()
        {
            // not use
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            // not use

            txtCode.Text = string.Empty;
            txtCode.Focus();

            txtDRFromAccount.Enabled = false;
            numChargeAmtFCY2.Visible = false;
            numChargeAmtFCY1.Visible = false;

            tbChargeCode.SelectedValue = "IC.CABLE";
            tbChargecode2.SelectedValue = "IC.PAYMENT";
            tbChargecode3.SelectedValue = "IC.HANDLING";
            tbChargecode4.SelectedValue = "IC.OTHER";

            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargecode2.Enabled = false;
            tbChargecode3.Enabled = false;
            tbChargecode4.Enabled = false;

            rcbPartyCharged.SelectedValue = "B";
            rcbPartyCharged2.SelectedValue = "B";
            rcbPartyCharged3.SelectedValue = "B";
            rcbPartyCharged4.SelectedValue = "B";

            rcbOmortCharge.SelectedValue = "NO";
            rcbOmortCharges2.SelectedValue = "NO";
            rcbOmortCharges3.SelectedValue = "NO";
            rcbOmortCharges4.SelectedValue = "NO";

            lblTotalPaymentAmount.Text = String.Format("{0:C}", 0).Replace("$", "");
            lblAmtCredited.Text = String.Format("{0:C}", 0).Replace("$", "");

            tbChargeAmt.Value = 0;
            tbChargeAmt2.Value = 0;
            tbChargeAmt3.Value = 0;
            tbChargeAmt4.Value = 0;

            numAmountCollected.Value = 0;
            numAmount_MT400.Value = 0;
            numAmount.Value = 0;
            numDrawingAmount.Value = 0;
            numAmtDrFromAcct.Value = 0;

            dteValueDate.SelectedDate = DateTime.Now;
            dteValueDate_MT202.SelectedDate = DateTime.Now;
            dteValueDate_MT400.SelectedDate = DateTime.Now;

            dteValueDate_MT103.SelectedDate = DateTime.Now;
            dteValueDate_MT103.Enabled = false;
            txtReceiverCorrespondent_MT103.Enabled = false;
            comboDetailOfCharges_MT103.SelectedValue = "SHA";
        }

        protected void LoadData()
        {
            // neu FT = null thì ko get data
            if (string.IsNullOrEmpty(txtCode.Text)) return;

            var dsPayment = bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_GetByPaymentCode(txtCode.Text.Trim());
            var status = string.Empty;

            lblTransactionReferenceNumber.Text = txtCode.Text.Trim();
            txtSendingBankTRN.Text = txtCode.Text.Trim();

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btReview").Enabled = true;

            if (dsPayment != null && dsPayment.Tables.Count > 0)
            {
                if (dsPayment.Tables[5].Rows.Count > 0)
                {
                    lblCurrency.Text = dsPayment.Tables[5].Rows[0]["Currency"].ToString();
                }
                LoadNostroAcct();

                #region tab Main

                if (dsPayment.Tables[0].Rows.Count > 0)
                {
                    RadToolBar1.FindItemByValue("btReview").Enabled = false;

                    var drow = dsPayment.Tables[0].Rows[0];

                    numDrawingAmount.Value = double.Parse(drow["DrawingAmount"].ToString());
                    lblAmtCredited.Text =
                        String.Format("{0:C}", double.Parse(drow["IncreaseMental"].ToString())).Replace("$", "");
                    status = drow["Status"].ToString();

                    lblDrawType.Text = drow["DrawType"].ToString();
                    lblCurrency.Text = drow["Currency"].ToString();
                    //numDrawingAmount.Text = drow["DrawingAmount"].ToString();
                    if (drow["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteValueDate.SelectedDate = DateTime.Parse(drow["ValueDate"].ToString());
                    }

                    txtDRFromAccount.Text = drow["DrFromAccount"].ToString();

                    numExchRate.Text = drow["ExchRate"].ToString();
                    lblAmtDRFrAcctCcy.Text = drow["AmtDrFrAcctCcy"].ToString();
                    numAmtDrFromAcct.Text = drow["AmtDrFromAcct"].ToString();
                    comboPaymentMethod.SelectedValue = drow["PaymentMethod"].ToString();

                    comboNostroAcct.SelectedValue = drow["NostroAcct"].ToString();
                    if (comboNostroAcct.SelectedItem != null)
                    {
                        lblNostroAcctName.Text = comboNostroAcct.SelectedItem.Attributes["Description"];
                    }

                    //lblAmtCredited.Text = drow["AmtCredited"].ToString();
                    txtPaymentRemarks1.Text = drow["PaymentRemarks1"].ToString();
                    txtPaymentRemarks2.Text = drow["PaymentRemarks2"].ToString();

                    comboPresentorCusNo.SelectedValue = drow["PresentorCusNo"].ToString();
                    comboCountryCode.SelectedValue = drow["CountryCode"].ToString();
                    rcbIsCreateMT103.SelectedValue = drow["SelectedTypeMT"].ToString();
                }
                else
                {
                    numDrawingAmount.Value = 0;
                    dteValueDate.SelectedDate = DateTime.Now;

                    txtDRFromAccount.Text = string.Empty;

                    numExchRate.Value = 0;
                    lblAmtDRFrAcctCcy.Text = string.Empty;
                    numAmtDrFromAcct.Value = 0;
                    comboPaymentMethod.SelectedValue = string.Empty;

                    comboNostroAcct.SelectedValue = string.Empty;
                    lblNostroAcctName.Text = string.Empty;

                    lblAmtCredited.Text = "0";
                    txtPaymentRemarks1.Text = string.Empty;
                    txtPaymentRemarks2.Text = string.Empty;
                    lblDrawType.Text = string.Empty;
                    lblCurrency.Text = string.Empty;

                    comboPresentorCusNo.SelectedValue = string.Empty;
                    comboCountryCode.SelectedValue = string.Empty;
                    rcbIsCreateMT103.SelectedValue = "0";
                }

                #endregion

                #region tab Charge
                if (dsPayment.Tables[1].Rows.Count > 0)
                {
                    var drow1 = dsPayment.Tables[1].Rows[0];

                    comboWaiveCharges.SelectedValue = drow1["WaiveCharges"].ToString();

                    PartyCharged = drow1["PartyCharged"].ToString();
                    txtChargeAcct1.Text = drow1["ChargeAcct"].ToString();

                    tbChargePeriod.Text = drow1["ChargePeriod"].ToString();
                    rcbChargeCcy.SelectedValue = drow1["ChargeCcy"].ToString();
                    tbExcheRate.Text = drow1["ExchRate"].ToString();
                    tbChargeAmt.Text = drow1["ChargeAmt"].ToString();
                    rcbPartyCharged.SelectedValue = drow1["PartyCharged"].ToString();
                    //lblPartyCharged.Text = drow1["PartyCharged"].ToString();
                    rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                    rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                    lblChargeStatus.Text = drow1["ChargeStatus"].ToString();

                    tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                    tbVatNo.Text = drow1["VATNo"].ToString();
                    lblTaxCode.Text = drow1["TaxCode"].ToString();
                    lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                    lblTaxAmt.Text = drow1["TaxAmt"].ToString();
                    if (lblTaxAmt.Text == "0")
                    {
                        lblTaxAmt.Text = "";
                    }
                    else
                    {
                        lblTaxAmt.Text = String.Format("{0:C}", lblTaxAmt.Text).Replace("$", "");
                    }
                    tbChargeCode.SelectedValue = drow1["Chargecode"].ToString();
                    numChargeAmtFCY1.Text = drow1["ChargeAmtFCY"].ToString();
                }
                else
                {
                    comboWaiveCharges.SelectedValue = string.Empty;
                    txtChargeAcct1.Text = string.Empty;
                    tbChargePeriod.Text = "1";
                    rcbChargeCcy.SelectedValue = string.Empty;
                    tbExcheRate.Value = 0;
                    tbChargeAmt.Value = 0;
                    rcbPartyCharged.SelectedValue = string.Empty;
                    lblPartyCharged.Text = string.Empty;
                    rcbOmortCharge.SelectedValue = string.Empty;
                    rcbChargeStatus.SelectedValue = string.Empty;
                    lblChargeStatus.Text = string.Empty;

                    tbChargeRemarks.Text = string.Empty;
                    tbVatNo.Text = string.Empty;
                    lblTaxCode.Text = string.Empty;
                    lblTaxCcy.Text = string.Empty;
                    lblTaxAmt.Text = string.Empty;

                    lblPartyCharged.Text = string.Empty;
                    lblChargeStatus.Text = string.Empty;

                    numChargeAmtFCY1.Value = 0;
                    numChargeAmtFCY2.Value = 0;
                }

                // charge code 2
                if (dsPayment.Tables[2].Rows.Count > 0)
                {
                    var drow2 = dsPayment.Tables[2].Rows[0];
                    txtChargeAcct2.Text = drow2["ChargeAcct"].ToString();
                    tbChargePeriod2.Text = drow2["ChargePeriod"].ToString();
                    rcbChargeCcy2.SelectedValue = drow2["ChargeCcy"].ToString();
                    tbExcheRate2.Text = drow2["ExchRate"].ToString();
                    tbChargeAmt2.Text = drow2["ChargeAmt"].ToString();
                    rcbPartyCharged2.SelectedValue = drow2["PartyCharged"].ToString();
                    rcbOmortCharges2.SelectedValue = drow2["OmortCharges"].ToString();
                    rcbChargeStatus2.SelectedValue = drow2["ChargeStatus"].ToString();
                    lblChargeStatus2.Text = drow2["ChargeStatus"].ToString();

                    lblTaxCode2.Text = drow2["TaxCode"].ToString();
                    lblTaxCcy2.Text = drow2["TaxCcy"].ToString();
                    lblTaxAmt2.Text = drow2["TaxAmt"].ToString();
                    if (lblTaxAmt2.Text == "0")
                    {
                        lblTaxAmt2.Text = "";
                    }
                    else
                    {
                        lblTaxAmt2.Text = String.Format("{0:C}", lblTaxAmt2.Text).Replace("$", "");
                    }

                    tbChargecode2.SelectedValue = drow2["Chargecode"].ToString();
                    numChargeAmtFCY2.Text = drow2["ChargeAmtFCY"].ToString();
                }
                else
                {
                    txtChargeAcct2.Text = string.Empty;
                    tbChargePeriod2.Text = string.Empty;
                    rcbChargeCcy2.SelectedValue = string.Empty;
                    tbExcheRate2.Value = 0;
                    tbChargeAmt2.Value = 0;
                    rcbPartyCharged2.SelectedValue = string.Empty;
                    rcbOmortCharges2.SelectedValue = string.Empty;
                    rcbChargeStatus2.SelectedValue = string.Empty;
                    lblChargeStatus2.Text = string.Empty;

                    lblTaxCode2.Text = string.Empty;
                    lblTaxCcy2.Text = string.Empty;
                    lblTaxAmt2.Text = string.Empty;

                    lblChargeStatus2.Text = string.Empty;
                }

                // charge code 3
                if (dsPayment.Tables[7].Rows.Count > 0)
                {
                    var drow3 = dsPayment.Tables[7].Rows[0];
                    txtChargeAcct3.Text = drow3["ChargeAcct"].ToString();
                    tbChargecode3.SelectedValue = drow3["Chargecode"].ToString();
                    tbChargePeriod3.Text = drow3["ChargePeriod"].ToString();
                    rcbChargeCcy3.SelectedValue = drow3["ChargeCcy"].ToString();
                    tbExcheRate3.Text = drow3["ExchRate"].ToString();
                    tbChargeAmt3.Text = drow3["ChargeAmt"].ToString();
                    rcbPartyCharged3.SelectedValue = drow3["PartyCharged"].ToString();
                    rcbOmortCharges3.SelectedValue = drow3["OmortCharges"].ToString();
                    rcbChargeStatus3.SelectedValue = drow3["ChargeStatus"].ToString();
                    lblChargeStatus3.Text = drow3["ChargeStatus"].ToString();

                    lblTaxCode3.Text = drow3["TaxCode"].ToString();
                    lblTaxCcy3.Text = drow3["TaxCcy"].ToString();
                    lblTaxAmt3.Text = drow3["TaxAmt"].ToString();
                    if (lblTaxAmt3.Text == "0")
                    {
                        lblTaxAmt3.Text = "";
                    }
                    else
                    {
                        lblTaxAmt3.Text = String.Format("{0:C}", lblTaxAmt3.Text).Replace("$", "");
                    }
                }
                else
                {
                    txtChargeAcct3.Text = string.Empty;

                    tbChargePeriod3.Text = string.Empty;
                    rcbChargeCcy3.SelectedValue = string.Empty;
                    tbExcheRate3.Value = 0;
                    tbChargeAmt3.Value = 0;
                    rcbPartyCharged3.SelectedValue = string.Empty;
                    rcbOmortCharges3.SelectedValue = string.Empty;
                    rcbChargeStatus3.SelectedValue = string.Empty;
                    lblChargeStatus3.Text = string.Empty;

                    lblTaxCode3.Text = string.Empty;
                    lblTaxCcy3.Text = string.Empty;
                    lblTaxAmt3.Text = "";
                }

                // charge code 4
                if (dsPayment.Tables[8].Rows.Count > 0)
                {
                    var drow3 = dsPayment.Tables[8].Rows[0];
                    txtChargeAcct4.Text = drow3["ChargeAcct"].ToString();
                    tbChargecode4.SelectedValue = drow3["Chargecode"].ToString();
                    tbChargePeriod4.Text = drow3["ChargePeriod"].ToString();
                    rcbChargeCcy4.SelectedValue = drow3["ChargeCcy"].ToString();
                    tbExcheRate4.Text = drow3["ExchRate"].ToString();
                    tbChargeAmt4.Text = drow3["ChargeAmt"].ToString();
                    rcbPartyCharged4.SelectedValue = drow3["PartyCharged"].ToString();
                    rcbOmortCharges4.SelectedValue = drow3["OmortCharges"].ToString();
                    rcbChargeStatus4.SelectedValue = drow3["ChargeStatus"].ToString();
                    lblChargeStatus4.Text = drow3["ChargeStatus"].ToString();

                    lblTaxCode4.Text = drow3["TaxCode"].ToString();
                    lblTaxCcy4.Text = drow3["TaxCcy"].ToString();
                    lblTaxAmt4.Text = drow3["TaxAmt"].ToString();
                    if (lblTaxAmt4.Text == "0")
                    {
                        lblTaxAmt4.Text = "";
                    }
                    else
                    {
                        lblTaxAmt4.Text = String.Format("{0:C}", lblTaxAmt4.Text).Replace("$", "");
                    }
                }
                else
                {
                    txtChargeAcct4.Text = string.Empty;

                    tbChargePeriod4.Text = string.Empty;
                    rcbChargeCcy4.SelectedValue = string.Empty;
                    tbExcheRate4.Value = 0;
                    tbChargeAmt4.Value = 0;
                    rcbPartyCharged4.SelectedValue = string.Empty;
                    rcbOmortCharges4.SelectedValue = string.Empty;
                    rcbChargeStatus4.SelectedValue = string.Empty;
                    lblChargeStatus4.Text = string.Empty;

                    lblTaxCode4.Text = string.Empty;
                    lblTaxCcy4.Text = string.Empty;
                    lblTaxAmt4.Text = "";
                }
                comboWaiveCharges_OnSelectedIndexChanged(null, null);

                #endregion

                #region tab MT202

                if (dsPayment.Tables[3].Rows.Count > 0)
                {
                    var drowMT202 = dsPayment.Tables[3].Rows[0];

                    //lblTransactionReferenceNumber.Text = drowMT202["TransactionReferenceNumber"].ToString();
                    txtRelatedReference.Text = drowMT202["RelatedReference"].ToString();
                    //lblRelatedReferenceName.Text = comboRelatedReference.SelectedItem.Attributes["Description"];

                    if (drowMT202["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteValueDate_MT202.SelectedDate = DateTime.Parse(drowMT202["ValueDate"].ToString());
                    }

                    comboCurrency.SelectedValue = drowMT202["Currency"].ToString();
                    numAmount.Text = drowMT202["Amount"].ToString();

                    txtReceiverCorrespondentNo.Text = drowMT202["ReceiverCorrespondent1"].ToString();
                    lblReceiverCorrespondentName2.Text = drowMT202["ReceiverCorrespondent2"].ToString();

                    lblSenderCorrespondent1.Text = drowMT202["SenderCorrespondent1"].ToString();
                    lblSenderCorrespondent2.Text = drowMT202["SenderCorrespondent2"].ToString();

                    txtIntermediaryBank.Text = drowMT202["IntermediaryBank"].ToString();
                    //CheckSwiftCodeExist();

                    txtAccountWithInstitution.Text = drowMT202["AccountWithInstitution"].ToString();

                    txtBeneficiaryBank.Text = drowMT202["BeneficiaryBank"].ToString();
                    txtSenderToReceiverInformation.Text = drowMT202["SenderToReceiverInformation"].ToString();
                    txtSenderToReceiverInformation2.Text = drowMT202["SenderToReceiverInformation2"].ToString();
                    txtSenderToReceiverInformation3.Text = drowMT202["SenderToReceiverInformation3"].ToString();

                    comboIntermediaryBankType.SelectedValue = drowMT202["IntermediaryBankType"].ToString();


                    txtIntermediaryBankName.Text = drowMT202["IntermediaryBankName"].ToString();
                    txtIntermediaryBankAddr1.Text = drowMT202["IntermediaryBankAddr1"].ToString();
                    txtIntermediaryBankAddr2.Text = drowMT202["IntermediaryBankAddr2"].ToString();
                    txtIntermediaryBankAddr3.Text = drowMT202["IntermediaryBankAddr3"].ToString();
                    comboAccountWithInstitutionType.SelectedValue = drowMT202["AccountWithInstitutionType"].ToString();
                    txtAccountWithInstitutionName.Text = drowMT202["AccountWithInstitutionName"].ToString();
                    txtAccountWithInstitutionAddr1.Text = drowMT202["AccountWithInstitutionAddr1"].ToString();
                    txtAccountWithInstitutionAddr2.Text = drowMT202["AccountWithInstitutionAddr2"].ToString();
                    txtAccountWithInstitutionAddr3.Text = drowMT202["AccountWithInstitutionAddr3"].ToString();
                    comboBeneficiaryBankType.SelectedValue = drowMT202["BeneficiaryBankType"].ToString();
                    txtBeneficiaryBankName.Text = drowMT202["BeneficiaryBankName"].ToString();
                    txtBeneficiaryBankAddr1.Text = drowMT202["BeneficiaryBankAddr1"].ToString();
                    txtBeneficiaryBankAddr2.Text = drowMT202["BeneficiaryBankAddr2"].ToString();
                    txtBeneficiaryBankAddr3.Text = drowMT202["BeneficiaryBankAddr3"].ToString();


                }
                else
                {
                    txtRelatedReference.Text = string.Empty;

                    dteValueDate_MT202.SelectedDate = DateTime.Now;

                    comboCurrency.SelectedValue = string.Empty;
                    numAmount.Value = 0;

                    txtReceiverCorrespondentNo.Text = string.Empty;
                    lblReceiverCorrespondentName2.Text = string.Empty;

                    lblSenderCorrespondent1.Text = string.Empty;
                    lblSenderCorrespondent2.Text = string.Empty;

                    txtIntermediaryBank.Text = string.Empty;

                    txtAccountWithInstitution.Text = string.Empty;
                    txtBeneficiaryBank.Text = string.Empty;
                    txtSenderToReceiverInformation.Text = string.Empty;
                    txtSenderToReceiverInformation2.Text = string.Empty;
                    txtSenderToReceiverInformation3.Text = string.Empty;

                    comboIntermediaryBankType.SelectedValue = string.Empty;

                    txtIntermediaryBankName.Text = string.Empty;
                    txtIntermediaryBankAddr1.Text = string.Empty;
                    txtIntermediaryBankAddr2.Text = string.Empty;
                    txtIntermediaryBankAddr3.Text = string.Empty;
                    comboAccountWithInstitutionType.SelectedValue = string.Empty;
                    txtAccountWithInstitutionName.Text = string.Empty;
                    txtAccountWithInstitutionAddr1.Text = string.Empty;
                    txtAccountWithInstitutionAddr2.Text = string.Empty;
                    txtAccountWithInstitutionAddr3.Text = string.Empty;
                    comboBeneficiaryBankType.SelectedValue = string.Empty;
                    txtBeneficiaryBankName.Text = string.Empty;
                    txtBeneficiaryBankAddr1.Text = string.Empty;
                    txtBeneficiaryBankAddr2.Text = string.Empty;
                    txtBeneficiaryBankAddr3.Text = string.Empty;

                    
                }

                #endregion

                #region tab MT400

                if (dsPayment.Tables[4].Rows.Count > 0)
                {
                    var drowMT400 = dsPayment.Tables[4].Rows[0];

                    comboCreateMT410.SelectedValue = drowMT400["GeneralMT400"].ToString();
                    CreateMT400 = comboCreateMT410.SelectedValue;
                    SetRelation_CreateMT410();

                    txtSendingBankTRN.Text = drowMT400["SendingBankTRN"].ToString();

                    txtRelatedReferenceMT400.Text = drowMT400["RelatedReference"].ToString();

                    numAmountCollected.Text = drowMT400["AmountCollected"].ToString();

                    if (drowMT400["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteValueDate_MT400.SelectedDate = DateTime.Parse(drowMT400["ValueDate"].ToString());
                    }


                    comboCurrency_MT400.SelectedValue = drowMT400["Currency"].ToString();
                    numAmount_MT400.Text = drowMT400["Amount"].ToString();

                    lblReceiverCorrespondentNameMT4001.Text = drowMT400["ReceiverCorrespondent1"].ToString();
                    lblReceiverCorrespondentNameMT4002.Text = drowMT400["ReceiverCorrespondent2"].ToString();

                    //lblSenderCorrespondentNameMT4001.Text = drowMT400["SenderCorrespondent1"].ToString();
                    //lblSenderCorrespondentNameMT4002.Text = drowMT400["SenderCorrespondent2"].ToString();

                    txtDetailOfCharges1.Text = drowMT400["DetailOfCharges1"].ToString();
                    txtDetailOfCharges2.Text = drowMT400["DetailOfCharges2"].ToString();
                    txtDetailOfCharges3.Text = drowMT400["DetailOfCharges3"].ToString();
                    txtSenderToReceiverInformation1_400_1.Text = drowMT400["SenderToReceiverInformation1"].ToString();
                    txtSenderToReceiverInformation1_400_2.Text = drowMT400["SenderToReceiverInformation2"].ToString();
                    txtSenderToReceiverInformation1_400_3.Text = drowMT400["SenderToReceiverInformation3"].ToString();

                    comboReceiverCorrespondentType.SelectedValue = drowMT400["ReceiverCorrespondentType"].ToString();
                    txtReceiverCorrespondentNo.Text = drowMT400["ReceiverCorrespondentNo"].ToString();
                    txtReceiverCorrespondentName.Text = drowMT400["ReceiverCorrespondentName"].ToString();
                    txtReceiverCorrespondentAddr1.Text = drowMT400["ReceiverCorrespondentAddr1"].ToString();
                    txtReceiverCorrespondentAddr2.Text = drowMT400["ReceiverCorrespondentAddr2"].ToString();
                    txtReceiverCorrespondentAddr3.Text = drowMT400["ReceiverCorrespondentAddr3"].ToString();

                    comboSenderCorrespondentType.SelectedValue = drowMT400["SenderCorrespondentType"].ToString();
                    txtSenderCorrespondentNo.Text = drowMT400["SenderCorrespondentNo"].ToString();
                    txtSenderCorrespondentName.Text = drowMT400["SenderCorrespondentName"].ToString();
                    txtSenderCorrespondentAddress1.Text = drowMT400["SenderCorrespondentAddr1"].ToString();
                    txtReceiverCorrespondentAddr2.Text = drowMT400["SenderCorrespondentAddr2"].ToString();
                    txtSenderCorrespondentAddress3.Text = drowMT400["SenderCorrespondentAddr3"].ToString();
                }
                else
                {
                    comboCreateMT410.SelectedValue = string.Empty;
                    SetRelation_CreateMT410();

                    txtSendingBankTRN.Text = string.Empty;

                    txtRelatedReferenceMT400.Text = string.Empty;

                    numAmountCollected.Value = 0;
                    dteValueDate_MT400.SelectedDate = DateTime.Now;


                    comboCurrency_MT400.SelectedValue = string.Empty;
                    numAmount_MT400.Value = 0;

                    lblReceiverCorrespondentNameMT4001.Text = string.Empty;
                    lblReceiverCorrespondentNameMT4002.Text = string.Empty;

                    //lblSenderCorrespondentNameMT4001.Text = string.Empty;
                   // lblSenderCorrespondentNameMT4002.Text = string.Empty;

                    txtDetailOfCharges1.Text = string.Empty;
                    txtDetailOfCharges2.Text = string.Empty;
                    txtDetailOfCharges3.Text = string.Empty;
                    txtSenderToReceiverInformation1_400_1.Text = string.Empty;
                    txtSenderToReceiverInformation1_400_2.Text = string.Empty;
                    txtSenderToReceiverInformation1_400_3.Text = string.Empty;

                    comboReceiverCorrespondentType.SelectedValue = string.Empty;
                    txtReceiverCorrespondentNo.Text = string.Empty;
                    txtReceiverCorrespondentName.Text = string.Empty;
                    txtReceiverCorrespondentAddr1.Text = string.Empty;
                    txtReceiverCorrespondentAddr2.Text = string.Empty;
                    txtReceiverCorrespondentAddr3.Text = string.Empty;

                    comboSenderCorrespondentType.SelectedValue = string.Empty;
                    txtSenderCorrespondentNo.Text = string.Empty;
                    txtSenderCorrespondentName.Text = string.Empty;
                    txtSenderCorrespondentAddress1.Text = string.Empty;
                    txtReceiverCorrespondentAddr2.Text = string.Empty;
                    txtSenderCorrespondentAddress3.Text = string.Empty;
                }

                #endregion
                
                #region tab MT103
                if (dsPayment.Tables[9].Rows.Count > 0)
                {
                    var drow103 = dsPayment.Tables[9].Rows[0];

                    lblSenderReference_MT103.Text = drow103["SenderReference"].ToString();

                    lblBankOperationCode_MT103.Text = drow103["BankOperationCode"].ToString();
                    if (drow103["ValueDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteValueDate_MT103.SelectedDate = DateTime.Parse(drow103["ValueDate"].ToString());
                    }
                    
                    comboCurrency_MT103.SelectedValue = drow103["Currency"].ToString();
                    lblInterBankSettleAmount_MT103.Text = String.Format("{0:C}", drow103["InterBankSettleAmount"]).Replace("$", "");
                    lblInstancedAmount_MT103.Text = String.Format("{0:C}", drow103["InstancedAmount"]).Replace("$", "");

                    comboOrderingCustAcc_MT103.SelectedValue = drow103["OrderingCustAcc"].ToString();
                    txtOrderingInstitution_MT103.Text = drow103["OrderingInstitution"].ToString();
                    lblSenderCorrespondent_MT103.Text = drow103["SenderCorrespondent"].ToString();

                    txtReceiverCorrBankAct_MT103.Text = drow103["ReceiverCorrBankAct"].ToString();

                    txtIntermediaryInstitutionNo_MT103.Text = drow103["IntermediaryInstruction"].ToString();
                    lblIntermediaryInstitutionName_MT103.Text = drow103["IntermediaryInstructionName"].ToString();

                    txtIntermediaryBankAcct_MT103.Text = drow103["IntermediaryBankAcct"].ToString();

                    txtAccountWithBankAcct_MT103.Text = drow103["AccountWithBankAcct"].ToString();
                    txtRemittanceInformation_MT103.Text = drow103["RemittanceInformation"].ToString();
                    comboDetailOfCharges_MT103.SelectedValue = drow103["DetailOfCharges"].ToString();

                    if (!string.IsNullOrEmpty(drow103["SenderCharges"].ToString()) && drow103["SenderCharges"].ToString() != "0")
                    {
                        numSenderCharges_MT103.Text = drow103["SenderCharges"].ToString();
                        //String.Format("{0:C}", drow103["SenderCharges"]).Replace("$", "");
                    }

                    if (!string.IsNullOrEmpty(drow103["ReceiverCharges"].ToString()) && drow103["ReceiverCharges"].ToString() != "0")
                    {
                        lblReceiverCharges_MT103.Text = String.Format("{0:C}", drow103["ReceiverCharges"]).Replace("$", "");
                    }

                    txtSenderToReceiverInfo_MT103.Text = drow103["SenderToReceiveInfo"].ToString();

                    txtAccountWithInstitutionNo_MT103.Text = drow103["AccountWithInstitution"].ToString();
                    lblAccountWithInstitutionName_MT103.Text = drow103["AccountWithInstitutionName"].ToString();

                    txtReceiverCorrespondent_MT103.Text = drow103["ReceiverCorrespondent"].ToString();

                    comboAccountType_MT103.SelectedValue = drow103["AccountType"].ToString();
                    txtAccountWithBankAcct2_MT103.Text = drow103["AccountWithBankAcct2"].ToString();
                    txtAccountWithBankAcct3_MT103.Text = drow103["AccountWithBankAcct3"].ToString();
                    txtAccountWithBankAcct4_MT103.Text = drow103["AccountWithBankAcct4"].ToString();

                    txtBeneficiaryCustomer1_MT103.Text = drow103["BeneficiaryCustomer1"].ToString();
                    txtBeneficiaryCustomer2_MT103.Text = drow103["BeneficiaryCustomer2"].ToString();
                    txtBeneficiaryCustomer3_MT103.Text = drow103["BeneficiaryCustomer3"].ToString();

                    txtBeneficiaryCustomer4_MT103.Text = drow103["BeneficiaryCustomer4"].ToString();
                    txtBeneficiaryCustomer5_MT103.Text = drow103["BeneficiaryCustomer5"].ToString();
                    comboIntermediaryType_MT103.SelectedValue = drow103["IntermediaryType"].ToString();
                    txtIntermediaryInstruction1_MT103.Text = drow103["IntermediaryInstruction1"].ToString();
                    txtIntermediaryInstruction2_MT103.Text = drow103["IntermediaryInstruction2"].ToString();

                    SetRelation_AccountWithInstitution();
                    SetRelation_IntermediaryInstruction();

                    if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
                    {
                        txtIntermediaryInstitutionNo_MT103.Enabled = false;
                        txtAccountWithInstitutionNo_MT103.Enabled = false;
                    }

                    txtOrderingCustomer1_MT103.Text = drow103["OrderingCustAccName"].ToString();
                    txtOrderingCustomer2_MT103.Text = drow103["OrderingCustAccAddr1"].ToString();
                    txtOrderingCustomer3_MT103.Text = drow103["OrderingCustAccAddr2"].ToString();
                    txtOrderingCustomer4_MT103.Text = drow103["OrderingCustAccAddr3"].ToString();
                }
                else
                {
                    lblSenderReference_MT103.Text = string.Empty;
                    lblBankOperationCode_MT103.Text = string.Empty;
                    dteValueDate_MT103.SelectedDate = DateTime.Now;
                    comboCurrency_MT103.SelectedValue = lblCurrency.Text;
                    lblInterBankSettleAmount_MT103.Text = "0";
                    lblInstancedAmount_MT103.Text = "0";
                    comboOrderingCustAcc_MT103.SelectedValue = string.Empty;
                    txtOrderingInstitution_MT103.Text = string.Empty;
                    lblSenderCorrespondent_MT103.Text = string.Empty;
                    txtReceiverCorrespondent_MT103.Text = string.Empty;
                    txtReceiverCorrBankAct_MT103.Text = string.Empty;
                    txtIntermediaryInstitutionNo_MT103.Text = string.Empty;
                    lblIntermediaryInstitutionName_MT103.Text = string.Empty;

                    txtAccountWithInstitutionNo_MT103.Text = string.Empty;
                    lblAccountWithInstitutionName_MT103.Text = string.Empty;
                    txtIntermediaryBankAcct_MT103.Text = string.Empty;

                    txtAccountWithBankAcct_MT103.Text = string.Empty;
                    txtRemittanceInformation_MT103.Text = string.Empty;
                    comboDetailOfCharges_MT103.SelectedValue = string.Empty;
                    numSenderCharges_MT103.Text = "";
                    lblReceiverCharges_MT103.Text = "";
                    txtSenderToReceiverInfo_MT103.Text = string.Empty;

                    txtOrderingCustomer1_MT103.Text = string.Empty;
                    txtOrderingCustomer2_MT103.Text = string.Empty;
                    txtOrderingCustomer3_MT103.Text = string.Empty;
                    txtOrderingCustomer4_MT103.Text = string.Empty;

                    comboAccountType_MT103.SelectedValue = string.Empty;
                    txtAccountWithBankAcct2_MT103.Text = string.Empty;
                    txtAccountWithBankAcct3_MT103.Text = string.Empty;
                    txtAccountWithBankAcct4_MT103.Text = string.Empty;

                    txtBeneficiaryCustomer1_MT103.Text = string.Empty;
                    txtBeneficiaryCustomer2_MT103.Text = string.Empty;
                    txtBeneficiaryCustomer3_MT103.Text = string.Empty;

                    txtBeneficiaryCustomer4_MT103.Text = string.Empty;
                    txtBeneficiaryCustomer5_MT103.Text = string.Empty;
                    comboIntermediaryType_MT103.SelectedValue = string.Empty;
                    txtIntermediaryInstruction1_MT103.Text = string.Empty;
                    txtIntermediaryInstruction2_MT103.Text = string.Empty;
                }
                #endregion

                #region get data from Register Documetary Collection, neu chua co thong tin trong BINCOMINGCOLLECTIONPAYMENT

                if (dsPayment.Tables[5].Rows.Count > 0 && dsPayment.Tables[0].Rows.Count <= 0)
                {
                    var drow = dsPayment.Tables[5].Rows[0];
                    double drawingAmt = 0;

                    if (drow["Amount"] != DBNull.Value)
                    {
                        drawingAmt = double.Parse(drow["Amount"].ToString());
                        numDrawingAmount.Value = drawingAmt;
                        numAmtDrFromAcct.Value = drawingAmt;
                        lblInterBankSettleAmount_MT103.Text = String.Format("{0:C}", drawingAmt).Replace("$", "");
                        lblInstancedAmount_MT103.Text = String.Format("{0:C}", drawingAmt).Replace("$", "");
                    }

                    //LoadDebitAcctNo(drow["DraweeCusName"].ToString());

                    // call func check status
                    CheckStatus(drow);

                    // ham nay se move vao CheckStatus
                    if (!string.IsNullOrEmpty(status))
                    {
                        if (status == "AUT")
                        {
                            // Neu AUT thi ko cho phep sua
                            InitToolBar(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btReview").Enabled = false;
                            RadToolBar1.FindItemByValue("btprint").Enabled = true;
                        }
                    }

                    lblTotalPaymentAmount.Text = String.Format("{0:C}", drawingAmt).Replace("$", "");
                    

                    // User enter code moi
                    if (dsPayment.Tables[0].Rows.Count <= 0)
                    {
                        tbVatNo.Text = drow["VATNo"].ToString();
                        if (string.IsNullOrEmpty(tbVatNo.Text))
                        {
                            GenerateVAT();
                        }

                        lblDrawType.Text = drow["DrawType"].ToString();
                        lblCurrency.Text = drow["Currency"].ToString();
                        txtDRFromAccount.Text = drow["DRFromAccount"].ToString();

                        txtRelatedReference.Text = drow["RemittingBankRef"].ToString();
                        txtRelatedReferenceMT400.Text = drow["RemittingBankRef"].ToString();

                        // Tab 212
                        comboCurrency.SelectedValue = drow["Currency"].ToString();
                        // Tab 212

                        // Tab 400
                        comboCurrency_MT400.SelectedValue = drow["Currency"].ToString();
                        // Tab 400

                        rcbChargeCcy.SelectedValue = drow["Currency"].ToString();
                        rcbChargeCcy2.SelectedValue = drow["Currency"].ToString();
                        rcbChargeCcy3.SelectedValue = drow["Currency"].ToString();
                        rcbChargeCcy4.SelectedValue = drow["Currency"].ToString();

                        if (!string.IsNullOrEmpty(drow["RMA_Flag"].ToString()))
                        {
                            //divMT400.Visible = true;
                            ReadOnlyTabMt400(true);
                            comboCreateMT410.SelectedValue = "YES";
                        }
                        else
                        {
                            //divMT400.Visible = false;
                            ReadOnlyTabMt400(false);
                            comboCreateMT410.SelectedValue = "NO";
                        }

                        switch (drow["DrawerType"].ToString())
                        {
                            case "A":
                                lblReceiverCorrespondentNameMT4001.Text = drow["DrawerCusNo"].ToString();
                                lblReceiverCorrespondentNameMT4002.Text = drow["DrawerCusName"].ToString();
                                break;
                            case "B":
                            case "D":
                                var addrr = drow["DrawerAddr1"].ToString();
                                if (!string.IsNullOrEmpty(drow["DrawerAddr1"].ToString()))
                                {
                                    addrr = ", " + drow["DrawerAddr2"];
                                }
                                lblReceiverCorrespondentNameMT4001.Text = drow["DrawerAddr"].ToString();
                                lblReceiverCorrespondentNameMT4002.Text = addrr;
                                break;
                        }

                        switch (drow["DraweeType"].ToString())
                        {
                            case "A":
                                lblSenderCorrespondent1.Text = drow["DraweeCusNo"] + " - " + drow["DraweeCusName"];
                                lblSenderCorrespondent2.Text = drow["DraweeAddr1"] + ", " + drow["DraweeAddr2"] + ", " +
                                                               drow["DraweeAddr3"];

                                //lblSenderCorrespondentNameMT4001.Text = lblSenderCorrespondent1.Text;
                                //lblSenderCorrespondentNameMT4002.Text = lblSenderCorrespondent2.Text;
                                break;
                            case "B":
                            case "D":
                                var addrr = drow["DraweeAddr2"].ToString();
                                if (!string.IsNullOrEmpty(drow["DraweeAddr3"].ToString()))
                                {
                                    addrr = ", " + drow["DraweeAddr3"];
                                }
                                lblReceiverCorrespondentNameMT4001.Text = drow["DraweeAddr1"].ToString();
                                lblReceiverCorrespondentNameMT4002.Text = addrr;

                                lblSenderCorrespondent1.Text = drow["DraweeAddr1"].ToString();
                                lblSenderCorrespondent2.Text = addrr;
                                break;
                        }

                        // Tab MT103
                        comboDetailOfCharges_MT103.SelectedValue = "SHA";
                        lblSenderReference_MT103.Text = txtCode.Text;

                        comboCurrency_MT103.SelectedValue = drow["Currency"].ToString();

                        comboOrderingCustAcc_MT103.SelectedValue = drow["DraweeCusNo"].ToString();
                        txtOrderingCustomer1_MT103.Text = drow["DraweeCusName"].ToString();
                        txtOrderingCustomer2_MT103.Text = drow["DraweeAddr1"].ToString();
                        txtOrderingCustomer3_MT103.Text = drow["DraweeAddr2"].ToString();
                        txtOrderingCustomer4_MT103.Text = drow["DraweeAddr3"].ToString();

                        txtBeneficiaryCustomer1_MT103.Text = drow["DrawerCusNo"].ToString();
                        txtBeneficiaryCustomer2_MT103.Text = drow["DrawerCusName"].ToString();
                        txtBeneficiaryCustomer3_MT103.Text = drow["DrawerAddr"].ToString();
                        txtBeneficiaryCustomer4_MT103.Text = drow["DrawerAddr1"].ToString();
                        txtBeneficiaryCustomer5_MT103.Text = drow["DrawerAddr2"].ToString();
                    }
                }

                #endregion

                // The previous payment has not been authorized yet. 
                // kiem tra khi Preview

                if (CheckReview())
                {
                    // call function check status 
                    // AUT -> review, not edit
                    // UNA,REV -> edit
                    var dtPre = dsPayment.Tables[6];
                    if (dtPre.Rows[0]["Status"].ToString() == "AUT")
                    {
                        SetRelation_IntermediaryBank();
                        SetRelation_AccountWithInstitution();
                        SetRelation_BeneficiaryBank();
                        SetRelation_ReceiverCorrespondent();
                        SetRelation_SenderCorrespondent();

                        InitToolBar(false);
                        SetDisableByReview(false);
                        RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        RadToolBar1.FindItemByValue("btReview").Enabled = false;
                        RadToolBar1.FindItemByValue("btprint").Enabled = true;
                    }
                    else
                    {
                        //InitToolBar(true);
                        SetDisableByReview(true);
                        RadToolBar1.FindItemByValue("btSave").Enabled = true;
                        RadToolBar1.FindItemByValue("btReview").Enabled = false;

                        SetRelation_IntermediaryBank();
                        SetRelation_AccountWithInstitution();
                        SetRelation_BeneficiaryBank();
                        SetRelation_ReceiverCorrespondent();
                        SetRelation_SenderCorrespondent();
                    }
                } else if (string.IsNullOrEmpty(Request.QueryString["disable"]) && CheckPreviousPayment(dsPayment))
                {
                    SetRelation_IntermediaryBank();
                    SetRelation_AccountWithInstitution();
                    SetRelation_BeneficiaryBank();
                    SetRelation_ReceiverCorrespondent();
                    SetRelation_SenderCorrespondent();

                    InitToolBar(false);
                    SetDisableByReview(false);
                    RadToolBar1.FindItemByValue("btSave").Enabled = false;
                    RadToolBar1.FindItemByValue("btReview").Enabled = true;
                }
                else
                {
                    RadToolBar1.FindItemByValue("btSave").Enabled = true;

                    SetRelation_IntermediaryBank();
                    SetRelation_AccountWithInstitution();
                    SetRelation_BeneficiaryBank();
                    SetRelation_ReceiverCorrespondent();
                    SetRelation_SenderCorrespondent();
                }

                // Set default Credited Amount
                lblAmtCredited.Text =
                    String.Format("{0:C}", double.Parse(dsPayment.Tables[6].Rows[0]["IncreaseMental"].ToString()))
                          .Replace("$", "");

                if (Request.QueryString["disable"] != null)
                {
                    txtCode.Text = Request.QueryString["paycode"];
                }
                else
                {
                    txtCode.Text = dsPayment.Tables[6].Rows[0]["PaymentId"].ToString();
                }
                txtChargeAcct1.Text = txtCode.Text;
                txtChargeAcct2.Text = txtCode.Text;
            }
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btRevert").Enabled = flag;
            if (Request.QueryString["disable"] != null && !string.IsNullOrEmpty(txtCode.Text))
                RadToolBar1.FindItemByValue("btprint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btprint").Enabled = false;
        }

        protected void Revert()
        {
            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_UpdateStatus(txtCode.Text.Trim(), "REV", UserId.ToString());

            // Active control
            SetDisableByReview(true);

            // ko cho Authorize/Preview
            InitToolBar(false);
            RadToolBar1.FindItemByValue("btSave").Enabled = true;
            RadToolBar1.FindItemByValue("btReview").Enabled = false;
        }

        protected void SaveData()
        {
            double DrawingAmount = 0;
            double ExchRate = 0;
            double AmtDrFromAcct = 0;
            double AmtCredited = 0;

            if (numDrawingAmount.Value > 0)
            {
                DrawingAmount = double.Parse(numDrawingAmount.Value.ToString());
            }
            if (numExchRate.Value > 0)
            {
                ExchRate = double.Parse(numExchRate.Value.ToString());
            }
            if (numAmtDrFromAcct.Value > 0)
            {
                AmtDrFromAcct = double.Parse(numAmtDrFromAcct.Value.ToString());
            }

            if (!string.IsNullOrEmpty(lblAmtCredited.Text))
            {
                AmtCredited = double.Parse(lblAmtCredited.Text);
            }

            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_Insert(txtCode.Text.Trim()
                                                       , lblDrawType.Text
                                                       , lblCurrency.Text
                                                       , DrawingAmount
                                                       , dteValueDate.SelectedDate.ToString()
                                                       , txtDRFromAccount.Text
                                                       , ExchRate
                                                       , lblAmtDRFrAcctCcy.Text
                                                       , AmtDrFromAcct
                                                       , comboPaymentMethod.SelectedValue
                                                       , comboNostroAcct.SelectedValue
                                                       , AmtCredited
                                                       , txtPaymentRemarks1.Text.Trim()
                                                       , txtPaymentRemarks2.Text.Trim()
                                                       , ""
                                                       , UserId
                                                       , comboPresentorCusNo.SelectedValue
                                                       , comboCountryCode.SelectedValue, int.Parse(rcbIsCreateMT103.SelectedValue));

            if (tbChargeCode.Text != "")
            {
                bd.SQLData.B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                                                                  tbChargeCode.SelectedValue, txtChargeAcct1.Text.Trim(),
                                                                  tbChargePeriod.Text,
                                                                  rcbChargeCcy.SelectedValue, tbExcheRate.Text,
                                                                  tbChargeAmt.Text, rcbPartyCharged.SelectedValue,
                                                                  rcbOmortCharge.SelectedValue, "", "",
                                                                  rcbChargeStatus.SelectedValue, tbChargeRemarks.Text,
                                                                  tbVatNo.Text, lblTaxCode.Text, lblTaxCcy.Text,
                                                                  string.IsNullOrEmpty(lblTaxAmt.Text) ? "0" : lblTaxAmt.Text, "", "", "1",
                                                                  numChargeAmtFCY1.Value.ToString());
            }

            // charge code 2
            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                                                              tbChargecode2.SelectedValue, txtChargeAcct2.Text.Trim(),
                                                              tbChargePeriod2.Text,
                                                              rcbChargeCcy2.SelectedValue, tbExcheRate2.Text,
                                                              tbChargeAmt2.Text, rcbPartyCharged2.SelectedValue,
                                                              rcbOmortCharges2.SelectedValue, "", "",
                                                              rcbChargeStatus2.SelectedValue, tbChargeRemarks.Text,
                                                              tbVatNo.Text, lblTaxCode2.Text, lblTaxCcy2.Text,
                                                              string.IsNullOrEmpty(lblTaxAmt2.Text) ? "0" : lblTaxAmt2.Text, "", "", "2",
                                                              numChargeAmtFCY2.Value.ToString());

            // ccharge code 3
            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                                                              tbChargecode3.SelectedValue,
                                                              txtChargeAcct3.Text.Trim(),
                                                              tbChargePeriod3.Text,
                                                              rcbChargeCcy3.SelectedValue,
                                                              tbExcheRate3.Text,
                                                              tbChargeAmt3.Text,
                                                              rcbPartyCharged3.SelectedValue,
                                                              rcbOmortCharges3.SelectedValue, "", "",
                                                              rcbChargeStatus3.SelectedValue,
                                                              tbChargeRemarks.Text,
                                                              tbVatNo.Text,
                                                              lblTaxCode3.Text,
                                                              lblTaxCcy3.Text,
                                                              string.IsNullOrEmpty(lblTaxAmt3.Text) ? "0" : lblTaxAmt3.Text, "", "", "3",
                                                              numChargeAmtFCY2.Value.ToString());

            // ccharge code 4
            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENTCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                                                              tbChargecode4.SelectedValue,
                                                              txtChargeAcct4.Text.Trim(),
                                                              tbChargePeriod4.Text,
                                                              rcbChargeCcy4.SelectedValue,
                                                              tbExcheRate4.Text,
                                                              tbChargeAmt4.Text,
                                                              rcbPartyCharged4.SelectedValue,
                                                              rcbOmortCharges4.SelectedValue, "", "",
                                                              rcbChargeStatus4.SelectedValue,
                                                              tbChargeRemarks.Text,
                                                              tbVatNo.Text,
                                                              lblTaxCode4.Text,
                                                              lblTaxCcy4.Text,
                                                              string.IsNullOrEmpty(lblTaxAmt4.Text) ? "0" : lblTaxAmt4.Text, "", "", "4",
                                                              numChargeAmtFCY2.Value.ToString());

            // tab MT 202
            bd.SQLData.B_INCOMINGCOLLECTIONPAYMENTMT202_Insert(txtCode.Text.Trim()
                                                            , lblTransactionReferenceNumber.Text
                                                            , txtRelatedReference.Text
                                                            , dteValueDate_MT202.SelectedDate.ToString()
                                                            , comboCurrency.SelectedValue
                                                            , numAmount.Text
                                                            , lblOrderingInstitution.Text
                                                            //, comboSenderCorrespondent.SelectedValue
                                                            //, comboReceiverCorrespondent.SelectedValue
                                                            , txtIntermediaryBank.Text.Trim()
                                                            , txtAccountWithInstitution.Text.Trim()
                                                            , txtBeneficiaryBank.Text.Trim()
                                                            , txtSenderToReceiverInformation.Text.Trim()
                                                            , lblSenderCorrespondent1.Text
                                                            , lblSenderCorrespondent2.Text
                                                            , txtReceiverCorrespondentNo.Text
                                                            , lblReceiverCorrespondentName2.Text
                                                            , comboIntermediaryBankType.SelectedValue
                                                            , txtIntermediaryBankName.Text.Trim()
                                                            , txtIntermediaryBankAddr1.Text.Trim()
                                                            , txtIntermediaryBankAddr2.Text.Trim()
                                                            , txtIntermediaryBankAddr3.Text.Trim()
                                                            , comboAccountWithInstitutionType.SelectedValue
                                                            , txtAccountWithInstitutionName.Text.Trim()
                                                            , txtAccountWithInstitutionAddr1.Text.Trim()
                                                            , txtAccountWithInstitutionAddr2.Text.Trim()
                                                            , txtAccountWithInstitutionAddr3.Text.Trim()
                                                            , comboBeneficiaryBankType.SelectedValue
                                                            , txtBeneficiaryBankName.Text.Trim()
                                                            , txtBeneficiaryBankAddr1.Text.Trim()
                                                            , txtBeneficiaryBankAddr2.Text.Trim()
                                                            , txtBeneficiaryBankAddr3.Text.Trim()
                                                            , txtSenderToReceiverInformation2.Text
                                                            , txtSenderToReceiverInformation3.Text);

            // tab MT 400
            bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTMT400_Insert(txtCode.Text.Trim()
                                                             , comboCreateMT410.SelectedValue
                                                             , txtSendingBankTRN.Text.Trim()
                                                             , txtRelatedReferenceMT400.Text
                                                             , numAmountCollected.Text
                                                             , dteValueDate_MT400.SelectedDate.ToString()
                                                             , comboCurrency_MT400.SelectedValue
                                                             , numAmount_MT400.Text
                                                             //, comboSenderCorrespondentMT400.SelectedValue
                                                             //, comboReceiverCorrespondentMT400.SelectedValue
                                                             , txtDetailOfCharges1.Text.Trim()
                                                             , txtDetailOfCharges2.Text
                                                             , ""//lblSenderCorrespondentNameMT4001.Text
                                                             , ""//lblSenderCorrespondentNameMT4002.Text
                                                             , lblReceiverCorrespondentNameMT4001.Text
                                                             , lblReceiverCorrespondentNameMT4002.Text
                                                             , comboReceiverCorrespondentType.SelectedValue
                                                             , txtReceiverCorrespondentNo.Text.Trim()
                                                             , txtReceiverCorrespondentName.Text.Trim()
                                                             , txtReceiverCorrespondentAddr1.Text.Trim()
                                                             , txtReceiverCorrespondentAddr2.Text.Trim()
                                                             , txtReceiverCorrespondentAddr3.Text.Trim()
                                                             
                                                             , comboSenderCorrespondentType.SelectedValue
                                                             , txtSenderCorrespondentNo.Text.Trim()
                                                             , txtSenderCorrespondentName.Text.Trim()
                                                             , txtSenderCorrespondentAddress1.Text.Trim()
                                                             , txtReceiverCorrespondentAddr2.Text.Trim()
                                                             , txtSenderCorrespondentAddress3.Text.Trim()
                                                             , txtSenderToReceiverInformation1_400_1.Text
                                                             , txtSenderToReceiverInformation1_400_2.Text
                                                             , txtSenderToReceiverInformation1_400_3.Text
                                                              , txtDetailOfCharges3.Text
            );

            double? InterBankSettleAmount = null;
            double? InstancedAmount = null;
            double? ReceiverCharges = null;

            if (!string.IsNullOrEmpty(lblInterBankSettleAmount_MT103.Text))
            {
                InterBankSettleAmount = double.Parse(lblInterBankSettleAmount_MT103.Text);
            }
            if (!string.IsNullOrEmpty(lblInstancedAmount_MT103.Text))
            {
                InstancedAmount = double.Parse(lblInstancedAmount_MT103.Text);
            }
        
            if (!string.IsNullOrEmpty(lblReceiverCharges_MT103.Text) && lblReceiverCharges_MT103.Text != "0")
            {
                ReceiverCharges = double.Parse(lblReceiverCharges_MT103.Text);
            }

            bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTMT103_Insert(txtCode.Text.Trim()
                , ""
                , lblSenderReference_MT103.Text
                , ""
                , lblBankOperationCode_MT103.Text.Trim()
                , ""
                , dteValueDate.SelectedDate.ToString()
                , comboCurrency.SelectedValue
                , InterBankSettleAmount
                , InstancedAmount
                , comboOrderingCustAcc_MT103.SelectedValue
                , txtOrderingInstitution_MT103.Text.Trim()
                , lblSenderCorrespondent_MT103.Text
                , txtReceiverCorrespondent_MT103.Text
                , txtReceiverCorrBankAct_MT103.Text.Trim()
                , txtIntermediaryInstitutionNo_MT103.Text.Trim()
                , txtIntermediaryBankAcct_MT103.Text.Trim()
                , txtAccountWithInstitutionNo_MT103.Text.Trim()
                , txtAccountWithBankAcct_MT103.Text.Trim()
                , txtRemittanceInformation_MT103.Text.Trim()
                , comboDetailOfCharges_MT103.SelectedValue
                , numSenderCharges_MT103.Value
                , ReceiverCharges
                , txtSenderToReceiverInfo_MT103.Text.Trim()
                , UserId.ToString()
                , txtBeneficiaryCustomer1_MT103.Text.Trim()
                , txtBeneficiaryCustomer2_MT103.Text.Trim()
                , txtBeneficiaryCustomer3_MT103.Text.Trim()
                , comboAccountType_MT103.SelectedValue
                , txtAccountWithBankAcct2_MT103.Text.Trim()                
                , txtBeneficiaryCustomer4_MT103.Text.Trim()
                , txtBeneficiaryCustomer5_MT103.Text.Trim()
                , comboIntermediaryType_MT103.SelectedValue
                , txtIntermediaryInstruction1_MT103.Text.Trim()
                , txtIntermediaryInstruction2_MT103.Text.Trim()
                , txtOrderingCustomer1_MT103.Text
                , txtOrderingCustomer2_MT103.Text
                , txtOrderingCustomer3_MT103.Text
                , txtOrderingCustomer4_MT103.Text
                , txtAccountWithBankAcct3_MT103.Text.Trim()
                , txtAccountWithBankAcct4_MT103.Text.Trim()
                , txtIntermediaryInstruction3_MT103.Text.Trim()
                , txtIntermediaryInstruction4_MT103.Text.Trim()
                );
        }

        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            tbVatNo.Enabled = false;
            tbChargeCode.Enabled = false;
            tbChargecode2.Enabled = false;
            tbChargecode3.Enabled = false;
            tbChargecode4.Enabled = false;
            txtDRFromAccount.Enabled = false;
            dteValueDate_MT103.Enabled = false;
            txtReceiverCorrespondent_MT103.Enabled = false;
        }

        // === tab Charges =============================================================
        protected void tbWaiveCharges_TextChanged(object sender, EventArgs e)
        {

        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }
        
        protected void btThem_Click(object sender, ImageClickEventArgs e)
        {
            //divCharge2.Visible = true;
            //divChargeInfo2.Visible = true;
        }

        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }

        protected void numAmtDrFromAcct_OnTextChanged(object sender, EventArgs e)
        {
            double tigia = 1;
            double sotien = 0;
            if (numExchRate.Value > 0)
            {
                tigia = Double.Parse(numExchRate.Value.ToString());
            }

            if (numAmtDrFromAcct.Value > 0)
            {
                sotien = Double.Parse(numAmtDrFromAcct.Value.ToString());
            }

            numAmtDrFromAcct.Text = (sotien*tigia).ToString();

            numDrawingAmount.Value = numAmtDrFromAcct.Value;
            numAmount.Value = numAmtDrFromAcct.Value;
            numAmount_MT400.Value = numAmtDrFromAcct.Value;
            //Cal_ChargeAmt();
        }

        protected void comboRelatedReference_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        private void loadReport(string report)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/", reportSaveName = "";
            Aspose.Words.SaveFormat reportSaveFormat = Aspose.Words.SaveFormat.Doc;
            DataSet reportData = null;
            switch (report)
            {
                case "MT202":
                    reportTemplate += "IncomingCollectionPaymentMT202.doc";
                    reportData = bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTMT202_Report(txtCode.Text);
                    reportSaveName = "IncomingCollectionPaymentMT202_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    reportSaveFormat = Aspose.Words.SaveFormat.Pdf;
                    break;
                case "MT400":
                    reportTemplate += "IncomingCollectionPaymentMT400.doc";
                    reportData = bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTMT400_Report(txtCode.Text);
                    reportSaveName = "IncomingCollectionPaymentMT400_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    reportSaveFormat = Aspose.Words.SaveFormat.Pdf;
                    break;
                case "VAT":
                    reportTemplate += "HOADONVAT.doc";
                    reportData = bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENT_HOADONVAT(txtCode.Text, UserInfo.Username);
                    reportSaveName = "HOADONVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                    break;
                case "PHIEUNHAPNGOAIBANG":
                    reportTemplate += "IncomingCollectionPaymentPHIEUXUATNGOAIBANG.doc";
                    reportData = bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTPHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                    reportSaveName = "IncomingCollectionPaymentPHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                    break;
                case "VATb":
                    reportTemplate += "IncomingCollectionPaymentVAT_B.doc";
                    reportData = bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_VAT_B_Report(txtCode.Text, UserInfo.Username);
                    reportSaveName = "IncomingCollectionPaymentVAT_B_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                    break;
                case "PHIEUCHUYENKHOAN":
                    reportTemplate += "IncomingCollectionPaymentPHIEUCHUYENKHOAN.doc";
                    reportData = bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_PHIEUCHUYENKHOAN_Report(txtCode.Text, UserInfo.Username);
                    reportSaveName = "IncomingCollectionPaymentPHIEUCHUYENKHOAN_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                    break;
                case "MT103":
                    reportTemplate += "IncomingCollectionPaymentMT103.doc";
                    reportData = bd.SQLData.B_BINCOMINGCOLLECTIONPAYMENTMT103_Report(txtCode.Text);
                    reportSaveName = "IncomingCollectionPaymentMT103_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                    reportSaveFormat = Aspose.Words.SaveFormat.Pdf;
                    break;
            }
            if (reportData != null)
            {
                bc.Reports.createFileDownload(Context.Server.MapPath(reportTemplate), reportData, reportSaveName, reportSaveFormat, Aspose.Words.SaveType.OpenInApplication, Response);
            }
        }
        protected void btnMT202Report_Click(object sender, EventArgs e)
        {
            loadReport("MT202");
        }

        protected void btnMT400Report_Click(object sender, EventArgs e)
        {
            loadReport("MT400");
        }

        protected void btnVATReport_Click(object sender, EventArgs e)
        {
            loadReport("VAT");
        }

        protected void comboCountryCode_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblCountryName.Text = comboCountryCode.SelectedValue;
        }

        protected void btnPHIEUNHAPNGOAIBANGReport_Click(object sender, EventArgs e)
        {
            loadReport("PHIEUNHAPNGOAIBANG");
        }

        protected void LoadNostroAcct()
        {
            comboNostroAcct.Items.Clear();
            comboNostroAcct.Items.Add(new RadComboBoxItem(""));
            comboNostroAcct.DataValueField = "Code";
            comboNostroAcct.DataTextField = "Description";
            comboNostroAcct.DataSource = bd.SQLData.B_BSWIFTCODE_GetByCurrency(lblCurrency.Text);
            comboNostroAcct.DataBind();
        }

        protected void comboAccountWithInstitutionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AccountWithInstitution();
        }
        protected void SetRelation_AccountWithInstitution()
        {
            txtAccountWithInstitution.Text = "";
            txtAccountWithInstitutionName.Text = "";
            txtAccountWithInstitutionAddr1.Text = "";
            txtAccountWithInstitutionAddr2.Text = "";
            txtAccountWithInstitutionAddr3.Text = "";
            switch (comboAccountWithInstitutionType.SelectedValue)
            {
                case "A":
                    txtAccountWithInstitution.Enabled = true;
                    txtAccountWithInstitutionName.Enabled = false;
                    txtAccountWithInstitutionAddr1.Enabled = false;
                    txtAccountWithInstitutionAddr2.Enabled = false;
                    txtAccountWithInstitutionAddr3.Enabled = false;
                    break;

                case "B":
                case "D":
                    txtAccountWithInstitution.Enabled = false;
                    txtAccountWithInstitutionName.Enabled = true;
                    txtAccountWithInstitutionAddr1.Enabled = true;
                    txtAccountWithInstitutionAddr2.Enabled = true;
                    txtAccountWithInstitutionAddr3.Enabled = true;
                    break;
            }
            comboReceiverCorrespondentType.SelectedValue = comboAccountWithInstitutionType.SelectedValue;
            comboReceiverCorrespondentType_OnSelectedIndexChanged(null, null);
        }

        protected void comboIntermediaryBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_IntermediaryBank();
        }

        protected void SetRelation_IntermediaryBank()
        {
            switch (comboIntermediaryBankType.SelectedValue)
            {
                case "A":
                    txtIntermediaryBank.Enabled = true;
                    txtIntermediaryBankName.Enabled = false;
                    txtIntermediaryBankAddr1.Enabled = false;
                    txtIntermediaryBankAddr2.Enabled = false;
                    txtIntermediaryBankAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtIntermediaryBank.Enabled = false;
                    txtIntermediaryBankName.Enabled = true;
                    txtIntermediaryBankAddr1.Enabled = true;
                    txtIntermediaryBankAddr2.Enabled = true;
                    txtIntermediaryBankAddr3.Enabled = true;
                    break;
            }
        }

        protected void comboBeneficiaryBankType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_BeneficiaryBank();
        }

        protected void SetRelation_BeneficiaryBank()
        {
            switch (comboBeneficiaryBankType.SelectedValue)
            {
                case "A":
                    txtBeneficiaryBank.Enabled = true;
                    txtBeneficiaryBankName.Enabled = false;
                    txtBeneficiaryBankAddr1.Enabled = false;
                    txtBeneficiaryBankAddr2.Enabled = false;
                    txtBeneficiaryBankAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtBeneficiaryBank.Enabled = false;
                    txtBeneficiaryBankName.Enabled = true;
                    txtBeneficiaryBankAddr1.Enabled = true;
                    txtBeneficiaryBankAddr2.Enabled = true;
                    txtBeneficiaryBankAddr3.Enabled = true;
                    break;
            }
        }

        protected void comboCreateMT410_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_CreateMT410();
        }

        protected void SetRelation_CreateMT410()
        {
            if (comboCreateMT410.SelectedValue.ToLower() == "yes")
            {
                txtSendingBankTRN.Enabled = true;
                txtRelatedReference.Enabled = true;
                numAmountCollected.Enabled = true;
                dteValueDate_MT400.Enabled = true;
                comboCurrency_MT400.Enabled = true;
                numAmount_MT400.Enabled = true;
                txtDetailOfCharges1.Enabled = true;
                txtDetailOfCharges2.Enabled = true;
                txtDetailOfCharges3.Enabled = true;
                txtSenderToReceiverInformation1_400_1.Enabled = true;
                txtSenderToReceiverInformation1_400_2.Enabled = true;
                txtSenderToReceiverInformation1_400_3.Enabled = true;
                
                txtRelatedReferenceMT400.Enabled = true;
                comboSenderCorrespondentType.Enabled = true;
                txtSenderCorrespondentNo.Enabled = true;
                comboReceiverCorrespondentType.Enabled = true;
                txtReceiverCorrespondentNo.Enabled = true;
            }
            else
            {
                txtSendingBankTRN.Enabled = false;
                txtRelatedReference.Enabled = false;
                numAmountCollected.Enabled = false;
                dteValueDate_MT400.Enabled = false;
                comboCurrency_MT400.Enabled = false;
                numAmount_MT400.Enabled = false;
                txtDetailOfCharges1.Enabled = false;
                txtDetailOfCharges2.Enabled = false;
                txtDetailOfCharges3.Enabled = false;
                txtSenderToReceiverInformation1_400_1.Enabled = false;
                txtSenderToReceiverInformation1_400_2.Enabled = false;
                txtSenderToReceiverInformation1_400_3.Enabled = false;
                txtRelatedReferenceMT400.Enabled = false;
                comboSenderCorrespondentType.Enabled = false;
                txtSenderCorrespondentNo.Enabled = false;
                comboReceiverCorrespondentType.Enabled = false;
                txtReceiverCorrespondentNo.Enabled = false;
            }
        }

        protected string Cal_PaymentNo()
        {
            return txtCode.Text;
        }

        protected void comboNostroAcct_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblSenderCorrespondent1.Text = comboNostroAcct.SelectedValue + " - " +
                                           comboNostroAcct.SelectedItem.Attributes["Description"];

            txtReceiverCorrespondent_MT103.Text = comboNostroAcct.SelectedValue;

            if (comboCreateMT410.SelectedValue == "YES")
            {
                txtSenderCorrespondentNo.Text = comboNostroAcct.SelectedValue;
                txtSenderCorrespondentName.Text = comboNostroAcct.SelectedItem != null ? comboNostroAcct.SelectedItem.Attributes["Description"] : "";
            }    
        }

        protected void comboReceiverCorrespondentType_OnSelectedIndexChanged(object sender,
                                                                             RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_ReceiverCorrespondent();
        }
        protected void SetRelation_ReceiverCorrespondent()
        {
            txtReceiverCorrespondentNo.Text = "";
            txtReceiverCorrespondentName.Text = "";
            txtReceiverCorrespondentAddr1.Text = "";
            txtReceiverCorrespondentAddr2.Text = "";
            txtReceiverCorrespondentAddr3.Text = "";
            switch (comboReceiverCorrespondentType.SelectedValue)
            {
                case "A":
                    txtReceiverCorrespondentNo.Enabled = true;
                    txtReceiverCorrespondentName.Enabled = false;
                    txtReceiverCorrespondentAddr1.Enabled = false;
                    txtReceiverCorrespondentAddr2.Enabled = false;
                    txtReceiverCorrespondentAddr3.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtReceiverCorrespondentNo.Enabled = false;
                    txtReceiverCorrespondentName.Enabled = true;
                    txtReceiverCorrespondentAddr1.Enabled = true;
                    txtReceiverCorrespondentAddr2.Enabled = true;
                    txtReceiverCorrespondentAddr3.Enabled = true;
                    break;
            }
        }

        protected void LoadChargeAcct()
        {
            //var code = txtCode.Text.Substring(0, 14);
            //var dsAcct = SQLData.B_PROVISIONTRANSFER_DC_GetByLCNo(code);
            //if (dsAcct != null)
            //{
            //rcbChargeAcct.Items.Clear();
            //rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            //rcbChargeAcct.DataValueField = "ProvisionNo";
            //rcbChargeAcct.DataTextField = "ProvisionNo";
            //rcbChargeAcct.DataSource = dsAcct;
            //rcbChargeAcct.DataBind();

            //rcbChargeAcct2.Items.Clear();
            //rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            //rcbChargeAcct2.DataValueField = "ProvisionNo";
            //rcbChargeAcct2.DataTextField = "ProvisionNo";
            //rcbChargeAcct2.DataSource = dsAcct;
            //rcbChargeAcct2.DataBind();
            //}
        }

        protected void btnVAT_B_Report_Click(object sender, EventArgs e)
        {
            loadReport("VATb");
        }

        protected void btnMT103Report_Click(object sender, EventArgs e)
        {
            loadReport("MT103");
        }

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
            e.Item.Attributes["Currency"] = row["Currency"].ToString();
        }

        protected void btnPhieuCK_Report_Click(object sender, EventArgs e)
        {
            loadReport("PHIEUCHUYENKHOAN");
        }

        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender,
                                                                RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (comboWaiveCharges.SelectedValue == "NO")
            {
                divACCPTCHG.Visible = true;
                divCABLECHG.Visible = true;
                divOTHERCHG.Visible = true;
                divPAYMENTCHG.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                divACCPTCHG.Visible = false;
                divCABLECHG.Visible = false;
                divOTHERCHG.Visible = false;
                divPAYMENTCHG.Visible = false;

                tbChargeAmt.Value = 0;
                tbChargeAmt2.Value = 0;
                tbChargeAmt3.Value = 0;
                tbChargeAmt4.Value = 0;

                rcbPartyCharged.SelectedValue = "B";
                rcbPartyCharged2.SelectedValue = "B";
                rcbPartyCharged3.SelectedValue = "B";
                rcbPartyCharged4.SelectedValue = "B";

                rcbOmortCharge.SelectedValue = "NO";
                rcbOmortCharges2.SelectedValue = "NO";
                rcbOmortCharges3.SelectedValue = "NO";
                rcbOmortCharges4.SelectedValue = "NO";

                lblTaxAmt.Text = "";
                lblTaxAmt2.Text = "";
                lblTaxAmt3.Text = "";
                lblTaxAmt4.Text = "";

                lblTaxCode.Text = "";
                lblTaxCode2.Text = "";
                lblTaxCode3.Text = "";
                lblTaxCode4.Text = "";

                Cal_ChargeAmt();
            }
        }
        protected bool B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt()
        {
            var code = txtCode.Text.Trim().Substring(0, 14);
            var dtAmt = bd.SQLData.B_INCOMINGCOLLECTIONPAYMENT_CheckAvailableAmt(code, txtCode.Text.Trim());

            double paymentAmount = Double.Parse(dtAmt.Rows[0]["PaymentAmount"].ToString());
            double creditAmount = Double.Parse(dtAmt.Rows[0]["CreditAmount"].ToString());
            double increaseMental = Double.Parse(dtAmt.Rows[0]["IncreaseMentalB4Aut"].ToString());
            double drawingAmt = 0;
            var flag = true;

            //PaymentAmount: so tien goc
            //IncreaseMental: when AUT so tien se dc cong don len dua vao DrawingAmount, số tien User thanh toán
            //Total payment amount = PaymentAmount: tong so tien bộ chung tu 

            // hungthh can check lai cho nay

            if (numDrawingAmount.Value > 0)
            {
                drawingAmt = Double.Parse(numDrawingAmount.Value.ToString());
            }

            // Chi chothanhtoanNeu so tienKyQuy column CreditAmountphai>= Drawing Amount (*)  trong tab MAIN
            if (creditAmount >= drawingAmt)
            {
                //if ((increaseMental + drawingAmt) > paymentAmount)
                //{
                //    string radalertscript =
                //        "<script language='javascript'>function f(){radalert('Can not process payment because Total payment is greater than Documentary Collection amount', 420, 150, 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                //    flag = false;
                //}
                flag = true;
            }
            else
            {
                string radalertscript =
                    "<script language='javascript'>function f(){radalert('Can not process because of not enough money for payment', 420, 150, 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
                flag = false;
            }
            return flag;
        }

        protected void CheckStatus(DataRow drow)
        {
            switch (drow["CollectionType"].ToString())
            {
                case "DA":
                    if (!string.IsNullOrEmpty(drow["Cancel_Status"].ToString()) &&
                        drow["Cancel_Status"].ToString() == "AUT")
                    {
                        lblError.Text = "This Documentary is cancel";
                        DisableFormForStatus();
                    }
                    else if (!string.IsNullOrEmpty(drow["Amend_Status"].ToString()) &&
                             drow["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Documentary has not authorized at Amendment step.";
                        DisableFormForStatus();
                    }
                    else if (!string.IsNullOrEmpty(drow["Accept_Status"].ToString()) &&
                             drow["Accept_Status"].ToString() != "AUT")
                    {
                        lblError.Text =
                            "This Incoming Documentary Collection has Not Authorized at the Acception step. Do not allow to process Payment Acceptance!";
                        DisableFormForStatus();
                    }

                    break;
                case "DP":
                    if (!string.IsNullOrEmpty(drow["Cancel_Status"].ToString()) &&
                        drow["Cancel_Status"].ToString() == "AUT")
                    {
                        lblError.Text = "This Documentary is cancel";
                        DisableFormForStatus();
                    }
                    else if (!string.IsNullOrEmpty(drow["Amend_Status"].ToString()) &&
                             drow["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Documentary has not authorized at Amendment step.";
                        DisableFormForStatus();
                    }
                    break;
            }
        }

        protected void DisableFormForStatus()
        {
            RadToolBar1.FindItemByValue("btSave").Enabled = false;
            RadToolBar1.FindItemByValue("btReview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btRevert").Enabled = false;
            RadToolBar1.FindItemByValue("btprint").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;

            SetDisableByReview(false);
        }

        protected void txtIntermediaryBank_OnTextChanged(object sender, EventArgs e)
        {
            lblIntermediaryBankNoError.Text = "";
            txtIntermediaryBankName.Text = "";
            if (!string.IsNullOrEmpty(txtIntermediaryBank.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtIntermediaryBank.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtIntermediaryBankName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblIntermediaryBankNoError.Text = "No found swiftcode";
                }
            }
        }

        protected void txtBeneficiaryBank_OnTextChanged(object sender, EventArgs e)
        {
            lblBeneficiaryBankError.Text = "";
            txtBeneficiaryBankName.Text = "";
            if (!string.IsNullOrEmpty(txtBeneficiaryBank.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtBeneficiaryBank.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtBeneficiaryBankName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblBeneficiaryBankError.Text = "No found swiftcode";
                }
            }
        }

        protected void txtAccountWithInstitution_OnTextChanged(object sender, EventArgs e)
        {
            bc.Commont.loadBankSwiftCodeInfo(txtAccountWithInstitution.Text.Trim(), ref lblAccountWithInstitutionError, ref txtAccountWithInstitutionName, ref txtAccountWithInstitutionAddr1, ref txtAccountWithInstitutionAddr2, ref txtAccountWithInstitutionAddr3);
            txtReceiverCorrespondentNo.Text = txtAccountWithInstitution.Text.Trim();
            txtReceiverCorrespondentName.Text = txtAccountWithInstitutionName.Text;
            txtReceiverCorrespondentAddr1.Text = txtAccountWithInstitutionAddr1.Text;
            txtReceiverCorrespondentAddr2.Text = txtAccountWithInstitutionAddr2.Text;
            txtReceiverCorrespondentAddr3.Text = txtAccountWithInstitutionAddr3.Text;
        }

        protected void txtReceiverCorrespondentNo_OnTextChanged(object sender, EventArgs e)
        {
            lblReceiverCorrespondentError.Text = "";
            txtReceiverCorrespondentName.Text = "";
            if (!string.IsNullOrEmpty(txtReceiverCorrespondentNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtReceiverCorrespondentNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtReceiverCorrespondentName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblReceiverCorrespondentError.Text = "No found swiftcode";
                }
            }
        }

        protected void numDrawingAmount_OnTextChanged(object sender, EventArgs e)
        {
            double drawingAmt = 0;
            if (numDrawingAmount.Value.HasValue) drawingAmt = numDrawingAmount.Value.Value;

            numAmtDrFromAcct.Value = drawingAmt;
            numAmountCollected.Value = drawingAmt;
            /*numAmount.Value = drawingAmt - chargeAmt;
            numAmount_MT400.Value = drawingAmt - chargeAmt;*/
            Cal_ChargeAmt();            
            lblInstancedAmount_MT103.Text = String.Format("{0:C}", drawingAmt).Replace("$", "");
        }
        
        protected void LoadPartyCharged()
        {
            var dtSource = bd.SQLData.CreateGenerateDatas("PartyCharged");

            rcbPartyCharged.Items.Clear();
            rcbPartyCharged.DataValueField = "Id";
            rcbPartyCharged.DataTextField = "Id";
            rcbPartyCharged.DataSource = dtSource;
            rcbPartyCharged.DataBind();

            rcbPartyCharged2.Items.Clear();
            rcbPartyCharged2.DataValueField = "Id";
            rcbPartyCharged2.DataTextField = "Id";
            rcbPartyCharged2.DataSource = dtSource;
            rcbPartyCharged2.DataBind();

            rcbPartyCharged3.Items.Clear();
            rcbPartyCharged3.DataValueField = "Id";
            rcbPartyCharged3.DataTextField = "Id";
            rcbPartyCharged3.DataSource = dtSource;
            rcbPartyCharged3.DataBind();

            rcbPartyCharged4.Items.Clear();
            rcbPartyCharged4.DataValueField = "Id";
            rcbPartyCharged4.DataTextField = "Id";
            rcbPartyCharged4.DataSource = dtSource;
            rcbPartyCharged4.DataBind();
        }

        protected bool CheckPreviousPayment(DataSet dsPayment)
        {
            if (dsPayment.Tables[6].Rows.Count > 0 != null && dsPayment.Tables[6].Rows[0]["Status"].ToString() == "UNA")
            {
                ShowMsgBox("The previous payment has not been authorized yet.");
                return true;
            }
            return false;
        }

        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }

        public string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                       ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                       : pagePathWithoutQueryString;
        }

        protected void btnChargecode2_Click(object sender, ImageClickEventArgs e)
        {
            //divCharge2.Visible = false;
            // divChargeInfo2.Visible = false;

            tbChargecode2.SelectedValue = string.Empty;
            rcbChargeCcy2.SelectedValue = string.Empty;
            tbChargeAmt2.Value = 0;
            rcbPartyCharged2.SelectedValue = string.Empty;
            rcbOmortCharges2.SelectedValue = string.Empty;
            rcbChargeStatus2.SelectedValue = string.Empty;

            lblTaxCode2.Text = string.Empty;
            lblTaxCcy2.Text = string.Empty;
            lblTaxAmt2.Text = "0";
        }

        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            chargeAmt_Changed();
        }
        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            chargeAmt2_Changed();
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            chargeAmt3_Changed();
        }
        protected void tbChargeAmt4_TextChanged(object sender, EventArgs e)
        {
            chargeAmt4_Changed();
        }

        protected void Cal_ChargeAmt()
        {
            double amountTab200 = 0;
            double drawingAmt = 0;
            double chargeAmt = 0;
            double amountForB = 0;
            double VATForB = 0;
            double amountForAC = 0;

            if (numDrawingAmount.Value > 0)
            {
                drawingAmt = double.Parse(numDrawingAmount.Value.ToString());
            }

            if (tbChargeAmt.Value > 0)
            {
                chargeAmt = double.Parse(tbChargeAmt.Value.ToString());
                switch (rcbPartyCharged.SelectedValue)
                {
                    case "B":
                        amountTab200 = drawingAmt - (chargeAmt + (chargeAmt * 0.1));
                        amountForB = chargeAmt;
                        break;
                    case "AC":
                        amountTab200 = drawingAmt + chargeAmt;
                        amountForAC = chargeAmt;
                        break;
                }
            }

            if (tbChargeAmt2.Value > 0)
            {
                chargeAmt = double.Parse(tbChargeAmt2.Value.ToString());
                switch (rcbPartyCharged2.SelectedValue)
                {
                    case "B":
                        amountTab200 = amountTab200 - (chargeAmt + (chargeAmt * 0.1));
                        amountForB += chargeAmt;
                        break;
                    case "AC":
                        amountTab200 = amountTab200 + chargeAmt;
                        amountForAC += chargeAmt;
                        break;
                }
            }

            if (tbChargeAmt3.Value > 0)
            {
                chargeAmt = double.Parse(tbChargeAmt3.Value.ToString());
                switch (rcbPartyCharged3.SelectedValue)
                {
                    case "B":
                        amountTab200 = amountTab200 - (chargeAmt + (chargeAmt * 0.1));
                        amountForB += chargeAmt;
                        break;
                    case "AC":
                        amountTab200 = amountTab200 + chargeAmt;
                        amountForAC += chargeAmt;
                        break;
                }
            }

            if (tbChargeAmt4.Value > 0)
            {
                chargeAmt = double.Parse(tbChargeAmt4.Value.ToString());
                switch (rcbPartyCharged4.SelectedValue)
                {
                    case "B":
                        amountTab200 = amountTab200 - (chargeAmt + (chargeAmt * 0.1));
                        amountForB += chargeAmt;
                        break;
                    case "AC":
                        amountTab200 = amountTab200 + chargeAmt;
                        amountForAC += chargeAmt;
                        break;
                }
            }

            if (amountTab200 <= 0)
            {
                amountTab200 = drawingAmt;
            }

            // SenderToReceiverInformation1 = (B + B + B + (VAT of B)) + AC
            VATForB = amountForB * 0.1;
            amountForB = amountForB + amountForAC + VATForB;
            txtSenderToReceiverInformation1_400_1.Text = String.Format("{0:C}", amountForB).Replace("$", "");
            // end
            numAmount_MT400.Value = amountTab200;
            numAmount.Value = amountTab200;
            lblInterBankSettleAmount_MT103.Text = String.Format("{0:C}", amountTab200).Replace("$", "");
        }

        protected void chargeAmt_Changed()
        {
            numChargeAmtFCY1.Value = tbChargeAmt.Value;
            double sotien = 0;

            if (tbChargeAmt.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt.Value.ToString());
                sotien = sotien * 0.1;
            }

            lblTaxAmt.Text = rcbPartyCharged.SelectedValue != "AC" ? String.Format("{0:C}", sotien).Replace("$", "") : "";
            lblTaxCode.Text = "81      10% VAT on Charge";

            Cal_ChargeAmt();

            CalculatorInterBankSettleAmount();
            CalculatorSenderCharges();
        }
        protected void chargeAmt2_Changed()
        {
            numChargeAmtFCY2.Value = tbChargeAmt2.Value;
            double sotien = 0;

            if (tbChargeAmt2.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt2.Value.ToString());
                sotien = sotien * 0.1;
            }
            lblTaxAmt2.Text = rcbPartyCharged2.SelectedValue != "AC" ? String.Format("{0:C}", sotien).Replace("$", "") : "";
            lblTaxCode2.Text = "81      10% VAT on Charge";

            Cal_ChargeAmt();

            CalculatorInterBankSettleAmount();
            CalculatorSenderCharges();
        }
        protected void chargeAmt3_Changed()
        {
            double sotien = 0;
            if (tbChargeAmt3.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt3.Value.ToString());
                sotien = sotien * 0.1;
            }
            lblTaxAmt3.Text = rcbPartyCharged3.SelectedValue != "AC" ? String.Format("{0:C}", sotien).Replace("$", "") : "";
            lblTaxCode3.Text = "81      10% VAT on Charge";

            Cal_ChargeAmt();

            CalculatorInterBankSettleAmount();
            CalculatorSenderCharges();
        }
        protected void chargeAmt4_Changed()
        {
            double sotien = 0;
            if (tbChargeAmt4.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt4.Value.ToString());
                sotien = sotien * 0.1;
            }
            lblTaxAmt4.Text = rcbPartyCharged4.SelectedValue != "AC" ? String.Format("{0:C}", sotien).Replace("$", "") : "";
            lblTaxCode4.Text = "81      10% VAT on Charge";

            Cal_ChargeAmt();

            CalculatorInterBankSettleAmount();
            CalculatorSenderCharges();
        }

        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Cal_ChargeAmt();
            chargeAmt_Changed();
        }
        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Cal_ChargeAmt();
            chargeAmt2_Changed();
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Cal_ChargeAmt();
            chargeAmt3_Changed();
        }
        protected void rcbPartyCharged4_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Cal_ChargeAmt();
            chargeAmt4_Changed();
        }

        protected void ReadOnlyTabMt400(bool flag)
        {
            comboCreateMT410.Enabled = flag;
            txtSendingBankTRN.Enabled = flag;
            txtRelatedReferenceMT400.Enabled = flag;
            numAmountCollected.Enabled = flag;
            dteValueDate_MT400.Enabled = flag;
            comboCurrency_MT400.Enabled = flag;
            numAmount_MT400.Enabled = flag;

            comboReceiverCorrespondentType.Enabled = flag;
            txtReceiverCorrespondentNo.Enabled = flag;
            txtReceiverCorrespondentName.Enabled = flag;
            txtReceiverCorrespondentAddr1.Enabled = flag;
            txtReceiverCorrespondentAddr2.Enabled = flag;
            txtReceiverCorrespondentAddr3.Enabled = flag;

            comboSenderCorrespondentType.Enabled = flag;
            txtSenderCorrespondentNo.Enabled = flag;
            txtSenderCorrespondentName.Enabled = flag;
            txtSenderCorrespondentAddress1.Enabled = flag;
            txtSenderCorrespondentAddress2.Enabled = flag;
            txtSenderCorrespondentAddress3.Enabled = flag;

            txtDetailOfCharges1.Enabled = flag;
            txtDetailOfCharges2.Enabled = flag;
            txtDetailOfCharges3.Enabled = flag;
            txtSenderToReceiverInformation1_400_1.Enabled = flag;
            txtSenderToReceiverInformation1_400_2.Enabled = flag;
            txtSenderToReceiverInformation1_400_3.Enabled = flag;
        }

        protected void comboSenderCorrespondentType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_SenderCorrespondent();
        }

        protected void SetRelation_SenderCorrespondent()
        {
            switch (comboSenderCorrespondentType.SelectedValue)
            {
                case "A":
                    txtSenderCorrespondentNo.Enabled = true;
                    txtSenderCorrespondentName.Enabled = false;
                    txtSenderCorrespondentAddress1.Enabled = false;
                    txtSenderCorrespondentAddress2.Enabled = false;
                    txtSenderCorrespondentAddress3.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtSenderCorrespondentNo.Enabled = false;
                    txtSenderCorrespondentName.Enabled = true;
                    txtSenderCorrespondentAddress1.Enabled = true;
                    txtSenderCorrespondentAddress2.Enabled = true;
                    txtSenderCorrespondentAddress3.Enabled = true;
                    break;
            }
        }

        protected void txtSenderCorrespondentNo_OnTextChanged(object sender, EventArgs e)
        {
            lblSenderCorrespondentNoError.Text = "";
            txtSenderCorrespondentName.Text = "";
            if (!string.IsNullOrEmpty(txtSenderCorrespondentNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtSenderCorrespondentNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtSenderCorrespondentName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                }
                else
                {
                    lblSenderCorrespondentNoError.Text = "No found swiftcode";
                }
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

        protected void LoadChargeCode()
        {
            var datasource = bd.SQLData.CreateGenerateDatas("ChargeCode_Payment", TabId);

            tbChargeCode.Items.Clear();
            tbChargeCode.Items.Add(new RadComboBoxItem(""));
            tbChargeCode.DataValueField = "Code";
            tbChargeCode.DataTextField = "Code";
            tbChargeCode.DataSource = datasource;
            tbChargeCode.DataBind();

            tbChargecode2.Items.Clear();
            tbChargecode2.Items.Add(new RadComboBoxItem(""));
            tbChargecode2.DataValueField = "Code";
            tbChargecode2.DataTextField = "Code";
            tbChargecode2.DataSource = datasource;
            tbChargecode2.DataBind();

            tbChargecode3.Items.Clear();
            tbChargecode3.Items.Add(new RadComboBoxItem(""));
            tbChargecode3.DataValueField = "Code";
            tbChargecode3.DataTextField = "Code";
            tbChargecode3.DataSource = datasource;
            tbChargecode3.DataBind();

            tbChargecode4.Items.Clear();
            tbChargecode4.Items.Add(new RadComboBoxItem(""));
            tbChargecode4.DataValueField = "Code";
            tbChargecode4.DataTextField = "Code";
            tbChargecode4.DataSource = datasource;
            tbChargecode4.DataBind();
        }

        protected void GenerateVAT()
        {
            DataSet vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
        }

        #region Tab 103
        
        protected void comboIntermediaryType_MT103_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_IntermediaryInstruction();
        }

        protected void SetRelation_IntermediaryInstruction()
        {
            switch (comboIntermediaryType_MT103.SelectedValue)
            {
                case "A":
                    txtIntermediaryInstruction1_MT103.Enabled = true;
                    txtIntermediaryInstruction2_MT103.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtIntermediaryInstruction1_MT103.Enabled = false;
                    txtIntermediaryInstruction2_MT103.Enabled = true;
                    break;
            }
        }

        protected void txtIntermediaryInstitutionNo_MT103_OnTextChanged(object sender, EventArgs e)
        {
            lblIntermediaryInstitutionNoError_MT103.Text = string.Empty;
            lblIntermediaryInstitutionName_MT103.Text = string.Empty;

            var dt = CheckExistBankSwiftCode(txtIntermediaryInstitutionNo_MT103.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                lblIntermediaryInstitutionName_MT103.Text = dt.Rows[0]["BankName"].ToString();
            }
            else
            {
                lblIntermediaryInstitutionNoError_MT103.Text = "No found Intermediary Institution";
            }
        }

        protected void comboAccountType_MT103_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_AccountType_MT103();
        }

        protected void SetRelation_AccountType_MT103()
        {
            switch (comboAccountType_MT103.SelectedValue)
            {
                case "A":
                    txtAccountWithInstitutionNo_MT103.Enabled = true;
                    txtAccountWithBankAcct_MT103.Enabled = false;
                    txtAccountWithBankAcct2_MT103.Enabled = false;
                    txtAccountWithBankAcct3_MT103.Enabled = false;
                    txtAccountWithBankAcct4_MT103.Enabled = false;
                    break;
                case "B":
                case "D":
                    txtAccountWithInstitutionNo_MT103.Enabled = false;
                    txtAccountWithBankAcct_MT103.Enabled = true;
                    txtAccountWithBankAcct2_MT103.Enabled = true;
                    txtAccountWithBankAcct3_MT103.Enabled = true;
                    txtAccountWithBankAcct4_MT103.Enabled = true;
                    break;
            }
        }

        protected void txtAccountWithInstitutionNo_MT103_OnTextChanged(object sender, EventArgs e)
        {
            lblAccountWithInstitutionNoError_MT103.Text = string.Empty;
            lblAccountWithInstitutionName_MT103.Text = string.Empty;

            var dt = CheckExistBankSwiftCode(txtAccountWithInstitutionNo_MT103.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                lblAccountWithInstitutionName_MT103.Text = dt.Rows[0]["BankName"].ToString();
            }
            else
            {
                lblAccountWithInstitutionNoError_MT103.Text = "No found Account With Institution";
            }
        }

        protected void comboDetailOfCharges_MT103_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CalculatorInstructedAmount();
            CalculatorInterBankSettleAmount();

            CalculatorSenderCharges();
        }

        //protected void LoadDebitAcctNo(string customerName)
        //{
        //    var dtAcc = SQLData.B_BDRFROMACCOUNT_GetByNameWithoutVND(customerName);

        //    comboOrderingCustAcc_MT103.Items.Clear();
        //    comboOrderingCustAcc_MT103.Items.Add(new RadComboBoxItem(""));
        //    comboOrderingCustAcc_MT103.DataValueField = "Id";
        //    comboOrderingCustAcc_MT103.DataTextField = "Display";
        //    comboOrderingCustAcc_MT103.DataSource = dtAcc;
        //    comboOrderingCustAcc_MT103.DataBind();
        //}

        protected DataTable CheckExistBankSwiftCode(string bankSwiftCode)
        {
            return bd.SQLData.B_BBANKSWIFTCODE_GetByCode(bankSwiftCode);
        }

        protected void CalculatorInstructedAmount()
        {
            var type = comboDetailOfCharges_MT103.SelectedValue;
            switch (type)
            {
                case "SHA":
                case "OUR":
                    if (numDrawingAmount.Value > 0)
                    {
                        lblInstancedAmount_MT103.Text = String.Format("{0:C}", numDrawingAmount.Value).Replace("$", "");
                    }
                    break;

                case "BEN":
                    if (numDrawingAmount.Value > 0)
                    {
                        lblInstancedAmount_MT103.Text = String.Format("{0:C}", numDrawingAmount.Value).Replace("$", "");
                    }
                    break;
            }
        }

        protected void CalculatorInterBankSettleAmount()
        {
            var type = comboDetailOfCharges_MT103.SelectedValue;
            var totalAmount = tbChargeAmt.Value + tbChargeAmt2.Value + tbChargeAmt3.Value + tbChargeAmt4.Value;
            var totalCharges = ((totalAmount) * (110)) / 100;

            switch (type)
            {
                case "SHA":
                case "OUR":
                    if (numDrawingAmount.Value > 0)
                    {
                        lblInterBankSettleAmount_MT103.Text = String.Format("{0:C}", numDrawingAmount.Value).Replace("$", "");
                    }
                    break;

                case "BEN":
                    totalAmount = numDrawingAmount.Value - (totalCharges);
                    if (totalAmount > 0)
                    {
                        lblInterBankSettleAmount_MT103.Text = String.Format("{0:C}", totalAmount).Replace("$", "");
                    }
                    break;
            }
        }

        protected void CalculatorSenderCharges()
        {
            var type = comboDetailOfCharges_MT103.SelectedValue;
            var totalAmount = tbChargeAmt.Value + tbChargeAmt2.Value + tbChargeAmt3.Value + tbChargeAmt4.Value;
            var totalCharges = ((totalAmount) * (110)) / 100;

            switch (type)
            {
                case "SHA":
                case "OUR":
                    numSenderCharges_MT103.Text = string.Empty;
                    break;
                case "BEN":
                    if (totalCharges > 0)
                    {
                        numSenderCharges_MT103.Value = totalCharges;
                            //String.Format("{0:C}", totalCharges).Replace("$", "");
                    }
                    break;
            }
        }
        
        protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }
        #endregion

        protected bool CheckFtVaild()
        {
            if (txtCode.Text.Length < 14)
            {
                bc.Commont.ShowClientMessageBox(Page, GetType(), "FT No. is invalid", 150);
                return false;
            }
            return true;
        }
    }
}
