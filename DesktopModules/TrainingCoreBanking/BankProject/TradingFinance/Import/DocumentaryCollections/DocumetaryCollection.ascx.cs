using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bc = BankProject.Controls;
using bd = BankProject.DataProvider;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace BankProject.TradingFinance.Import.DocumentaryCollections
{
    public partial class DocumetaryCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private string Refix_BMACODE()
        {
            return "TF";
        }

        public double Amount = 0;
        public double Amount_Old = 0;
        public double B4_AUT_Amount = 0;
        public double ChargeAmount = 0;
        public string CreateMT410 = string.Empty;
        public bool isMT412Active = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (TabId == 281) // Incoming Collection Acception
            {
                isMT412Active = true;
            }
            if (IsPostBack) return;
            //this.TabId
            // Register Documetary Collection => tabid=217
            // Incoming Collection Amendments => tabid=218
            // Documentary Collection Cancel => tabid=219
            // Incoming Collection Acception => tabid=281
            IntialEdittor(txtEdittor_OtherDocs);
            IntialEdittor(txtEdittor_InstructionToCus);

            lblError.Text = "";
            lblTaxAmt.Text = "0";
            lblTaxAmt2.Text = "0";

           
            // default no use
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            // default no use

            //tab charge
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;
            //tab charge
            
            // new
            divAmount.Visible = false;
            divTenor.Visible = false;
            divTracerDate.Visible = false;
            // new

            // DocumentaryCollectionCancel
            divDocumentaryCollectionCancel.Visible = false;
            dteAcceptedDate.SelectedDate = DateTime.Now;
            // DocumentaryCollectionCancel

            // Incoming Collection Acception
            divIncomingCollectionAcception.Visible = false;
            // Incoming Collection Acception

            Cal_TracerDate(false);
            divDocsCode.Visible = false;

            GeneralVATNo();

            InitToolBar(false);
            InitData();
            InitDataChargeCode();

            DataRow dataRow = null;
            if (TabId == 218)
            {
                // Incoming Collection Amendments => tabid=218
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData(ref dataRow);
            }
            else if (TabId == 219)
            {
                // Documentary Collection Cancel => tabid=219
                txtCode.Text = Request.QueryString["CodeID"];
                txtCode.Focus();

                divDocumentaryCollectionCancel.Visible = true;
                dteCancelDate.SelectedDate = DateTime.Now;
                dteContingentExpiryDate.SelectedDate = DateTime.Now;

                LoadData(ref dataRow);

                SetDisableByReview(false);

                txtCode.Enabled = true;
                tbChargeRemarks.Enabled = true;
                // =tab charge
                if (dataRow != null && dataRow["Status"].ToString() == bd.TransactionStatus.AUT)
                {
                    txtCancelRemark.Enabled = true;
                    txtRemittingBankRef.Enabled = true;
                    dteCancelDate.Enabled = true;
                    dteContingentExpiryDate.Enabled = true;

                    //===========================
                    comboWaiveCharges.Enabled = true;
                    tbChargeCode.Enabled = true;
                    rcbChargeCcy.Enabled = true;

                    rcbChargeAcct.Enabled = true;
                    tbChargeAmt.Enabled = true;
                    rcbPartyCharged.Enabled = true;
                    rcbOmortCharge.Enabled = true;
                    rcbChargeStatus.Enabled = true;

                    tbChargecode2.Enabled = true;
                    rcbChargeCcy2.Enabled = true;
                    rcbChargeAcct2.Enabled = true;
                    tbChargeAmt2.Enabled = true;
                    rcbPartyCharged2.Enabled = true;
                    rcbOmortCharges2.Enabled = true;
                    rcbChargeStatus2.Enabled = true;

                    btThem.Enabled = true;
                }
            }
            else if (TabId == 281) // Incoming Collection Acception
            {
                txtCode.Text = Request.QueryString["CodeID"];
                txtCode.Focus();
                isMT412Active = true;
                LoadData(ref dataRow);
                SetDisableByReview(false);
                txtCode.Enabled = true;
                tbChargeRemarks.Enabled = true;
                

                // tab MT410
                if (string.IsNullOrEmpty(Request.QueryString["disable"]))
                {
                    comboCreateMT410.Enabled = true;
                    txtGeneralMT410_2.Enabled = true;
                    txtSendingBankTRN.Enabled = true;
                    txtRelatedReference.Enabled = true;
                    comboCurrency_TabMT410.Enabled = true;
                    numAmount_TabMT410.Enabled = true;
                    txtSenderToReceiverInfo_410_1.Enabled = true;
                    txtSenderToReceiverInfo_410_2.Enabled = true;
                    txtSenderToReceiverInfo_410_3.Enabled = true;
                    dteMaturityDateMT412.Enabled = true;
                    //txtSenderToReceiverInfo.SetEnable(true);
                }
                

                // =tab charge
                if (dataRow != null && dataRow["Status"].ToString() == bd.TransactionStatus.AUT)
                {
                    divIncomingCollectionAcception.Visible = true;
                    dteAcceptedDate.Enabled = true;
                    txtAcceptedRemarks.Enabled = true;

                    // =============== 
                    comboWaiveCharges.Enabled = true;
                    tbChargeCode.Enabled = true;
                    rcbChargeCcy.Enabled = true;

                    rcbChargeAcct.Enabled = true;
                    tbChargeAmt.Enabled = true;
                    rcbPartyCharged.Enabled = true;
                    rcbOmortCharge.Enabled = true;
                    rcbChargeStatus.Enabled = true;

                    tbChargecode2.Enabled = true;
                    rcbChargeCcy2.Enabled = true;
                    rcbChargeAcct2.Enabled = true;
                    tbChargeAmt2.Enabled = true;
                    rcbPartyCharged2.Enabled = true;
                    rcbOmortCharges2.Enabled = true;
                    rcbChargeStatus2.Enabled = true;

                    btThem.Enabled = true;
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
            {
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData(ref dataRow);
                SetVisibilityByStatus(ref dataRow);
            }
            else
            {
                // trupng hoppage moi
                GeneralCode();
                SetDisableByReview(true);

                //divTM410.Visible = false;
                DisabledTab410(false);

            }

            if (!string.IsNullOrEmpty(Request.QueryString["disable"]))
            {
                InitToolBar(true);
                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["enquiry"]))
            {
                InitToolBar(false);
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            }
            Session["DataKey"] = txtCode.Text;

            // never allow to edit
            comboRemittingType.Enabled = false;
            tbVatNo.Enabled = false;
        }

        protected void IntialEdittor(RadEditor txtEdittor)
        {
            txtEdittor.EditModes = EditModes.Design;
            txtEdittor.Modules.Clear();
        }

        protected void InitDataChargeCode()
        {
            var datasource = bd.SQLData.CreateGenerateDatas("XXXX");
            switch (TabId)
            {
                case 217: // Register Documetary Collection
                    datasource = bd.SQLData.CreateGenerateDatas("ChargeCode_Register", 217);
                    break;
                case 218: // Incoming Collection Amendments
                    datasource = bd.SQLData.CreateGenerateDatas("ChargeCode_Amendments", 218);
                    break;
                case 219: // Documentary Collection Cancel
                    datasource = bd.SQLData.CreateGenerateDatas("ChargeCode_Cancel", 219);
                    break;
                case 281: // Incoming Collection Acception
                    datasource = bd.SQLData.CreateGenerateDatas("ChargeCode_Acception", 281);
                    break;
            }

            tbChargeCode.Items.Clear();
            tbChargeCode.Items.Add(new RadComboBoxItem(""));
            tbChargeCode.DataValueField = "Id";
            tbChargeCode.DataTextField = "Id";
            tbChargeCode.DataSource = datasource;
            tbChargeCode.DataBind();

            tbChargecode2.Items.Clear();
            tbChargecode2.Items.Add(new RadComboBoxItem(""));
            tbChargecode2.DataValueField = "Id";
            tbChargecode2.DataTextField = "Id";
            tbChargecode2.DataSource = datasource;
            tbChargecode2.DataBind();
        }

        protected void InitData()
        {
            comboCollectionType.Items.Clear();
            comboCollectionType.Items.Add(new RadComboBoxItem(""));
            comboCollectionType.DataValueField = "Id";
            comboCollectionType.DataTextField = "Id";
            comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_CollectionType");
            comboCollectionType.DataBind();

            comboDraweeCusNo.Items.Clear();
            comboDraweeCusNo.Items.Add(new RadComboBoxItem(""));
            comboDraweeCusNo.DataValueField = "CustomerID";
            comboDraweeCusNo.DataTextField = "CustomerID";
            comboDraweeCusNo.DataSource = bd.SQLData.B_BCUSTOMERS_OnlyBusiness();
            comboDraweeCusNo.DataBind();

            comboCommodity.Items.Clear();
            comboCommodity.Items.Add(new RadComboBoxItem(""));
            comboCommodity.DataValueField = "ID";
            comboCommodity.DataTextField = "Name";
            comboCommodity.DataSource = bd.SQLData.B_BCOMMODITY_GetByTransactionType("OTC");
            comboCommodity.DataBind();

            comboDocsCode1.Items.Clear();
            comboDocsCode1.Items.Add(new RadComboBoxItem(""));
            comboDocsCode1.DataValueField = "Id";
            comboDocsCode1.DataTextField = "Description";
            comboDocsCode1.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode1.DataBind();

            comboDocsCode2.Items.Clear();
            comboDocsCode2.Items.Add(new RadComboBoxItem(""));
            comboDocsCode2.DataValueField = "Id";
            comboDocsCode2.DataTextField = "Description";
            comboDocsCode2.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode2.DataBind();

            comboAccountOfficer.Items.Clear();
            comboAccountOfficer.Items.Add(new RadComboBoxItem(""));
            comboAccountOfficer.DataValueField = "Code";
            comboAccountOfficer.DataTextField = "Description";
            comboAccountOfficer.DataSource = bd.SQLData.B_BACCOUNTOFFICER_GetAll();
            comboAccountOfficer.DataBind();
        }

        #region Associate OnSelectedIndexChanged
        protected void comboCollectionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];
            SetValueToSendingBankTRN();
        }

        protected void commom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void comboRemittingBankNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblRemittingBankName.Text = comboRemittingBankNo.SelectedItem.Attributes["Description"];
            //TabMT410.Visible = true;
        }

        //protected void comboDrawerCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    lblDrawerCusName.Text = comboDrawerCusNo.SelectedValue;
        //    var dsCus = DataTam.B_BCUSTOMERS_GetbyID(comboDrawerCusNo.SelectedItem.Text);
        //    if (dsCus != null && dsCus.Tables.Count > 0 && dsCus.Tables[0].Rows.Count > 0)
        //    {
        //        var drow = dsCus.Tables[0].Rows[0];

        //        lblDrawerCusName.Text = drow["CustomerName"].ToString();
        //        txtDrawerAddr.Text = drow["Address"].ToString();
        //        txtDrawerAddr1.Text = drow["City"].ToString();
        //        txtDrawerAddr2.Text = drow["Country"].ToString();
        //    }
        //}

        protected void comboDraweeCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var drow = bd.DataTam.B_BCUSTOMERS_GetbyID(comboDraweeCusNo.SelectedItem.Text).Tables[0].Rows[0];

            txtDraweeCusName.Text = drow["CustomerName"].ToString();
            txtDraweeAddr1.Text = drow["Address"].ToString();
            txtDraweeAddr2.Text = drow["City"].ToString();
            txtDraweeAddr3.Text = drow["Country"].ToString();
        }

        protected void comboCommodity_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name"];
        }

        protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }

        protected void comboDocsCode_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblDocsCodeName.Text = comboDocsCode.SelectedValue;
        }
        #endregion

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            DataRow dataRow = null;

            switch (commandName)
            {
                case "save":
                    if (!CheckFtVaild()) return;

                    SaveData();

                    // reset form
                    GeneralCode();

                    SetDisableByReview(true);

                    LoadData(ref dataRow);

                    GeneralVATNo();

                    //tab charge
                    divCharge2.Visible = false;
                    divChargeInfo2.Visible = false;
                    //tab charge

                    // DocumentaryCollectionCancel
                    divDocumentaryCollectionCancel.Visible = false;
                    // DocumentaryCollectionCancel

                    // Incoming Collection Acception
                    divIncomingCollectionAcception.Visible = false;
                    // Incoming Collection Acception

                    dteDocsReceivedDate.SelectedDate = DateTime.Now;
                    divDocsCode.Visible = false;

                    txtTenor.Text = "AT SIGHT";

                    divAmount.Visible = false;
                    divTenor.Visible = false;
                    divTracerDate.Visible = false;

                    SetRelation_DrawerType();
                    SetRelation_DraweeType();
                    SetRelation_RemittingType();
                    Cal_TracerDate(false);
                    Session["DataKey"] = txtCode.Text;

                    Response.Redirect("Default.aspx?tabid=" + TabId);
                    break;

                case "review":
                    if (TabId == 218)
                    {
                        // Incoming Collection Amendments => tabid=218
                        Response.Redirect(EditUrl("incollamendment"));
                    }
                    else if (TabId == 219)
                    {
                        // Documentary Collection Cancel => tabid=219
                        Response.Redirect(EditUrl("doccolcancel"));
                    }
                    else if (TabId == 281)
                    {
                        // Incoming Collection Acception => tabid=281
                        Response.Redirect(EditUrl("incollaccepted"));
                    }
                    else
                    {
                        Response.Redirect(EditUrl("doccollreview"));
                    }
                    break;

                case "authorize":
                    if (!CheckFtVaild()) return;

                    Authorize();
                    break;

                case "revert":
                    if (!CheckFtVaild()) return;
                    Revert();
                    break;
            }
        }

        protected void Authorize()
        {
            // Update status
            var comeFromUrl = "";
            DataRow dataRow = null;

            if (Request.QueryString["key"] != null)
            {
                comeFromUrl = Request.QueryString["key"];
            }
            bd.SQLData.B_BDOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), bd.TransactionStatus.AUT, UserId.ToString(), comeFromUrl);

            // Generate Code
            txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("OVERSEASTRANSFER", Refix_BMACODE());

            // Generate VAT No
            GeneralVATNo();

            // Active control
            SetDisableByReview(true);

            // Reset Data
            LoadData(ref dataRow);

            //  InitToolBar
            InitToolBar(false);
            RadToolBar1.FindItemByValue("btSave").Enabled = true;

            // reset form default value
            dteDocsReceivedDate.SelectedDate = DateTime.Now;

            //tab charge
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;
            //tab charge

            // DocumentaryCollectionCancel
            divDocumentaryCollectionCancel.Visible = false;
            dteCancelDate.SelectedDate = DateTime.Now;
            dteContingentExpiryDate.SelectedDate = DateTime.Now;
            // DocumentaryCollectionCancel

            // Incoming Collection Acception
            divIncomingCollectionAcception.Visible = false;
            dteAcceptedDate.SelectedDate = DateTime.Now;
            // Incoming Collection Acception

            divDocsCode.Visible = false;
            txtTenor.Text = "AT SIGHT";

            Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
        }

        protected void Revert()
        {
            // Update status REV
            var comeFromUrl = "";
            if (Request.QueryString["key"] != null)
            {
                comeFromUrl = Request.QueryString["key"];
            }
            bd.SQLData.B_BDOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), "REV", UserId.ToString(), comeFromUrl);

            // Active control
            SetDisableByReview(true);

            // ko cho Authorize/Preview
            InitToolBar(false);
            RadToolBar1.FindItemByValue("btSave").Enabled = true;
            RadToolBar1.FindItemByValue("btReview").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            DataRow dataRow = null;
            LoadData(ref dataRow);
        }

        protected void InitToolBar(bool flag)
        {
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = flag;
            RadToolBar1.FindItemByValue("btRevert").Enabled = flag;
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;

            if (TabId == 219)
            {
               // RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            }
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 22, 2015    Hien Nguyen       Fix bug 80, 81
         */
        protected void LoadData(ref DataRow drowDocColl)
        {
            // neu FT = null thì ko get data
            if (string.IsNullOrEmpty(txtCode.Text)) return;

            var dsDoc = bd.SQLData.B_BDOCUMETARYCOLLECTION_GetByDocCollectCode(txtCode.Text.Trim(), TabId);
            var AmendNo = "";
            if (dsDoc == null || dsDoc.Tables.Count <= 0)
            {
                lblError.Text = "Can not find this Docs !";
                return;
            }

            // truong hop Edit, thi` ko cho click Preview
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            // ==========================
            #region tab main
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                RadToolBar1.FindItemByValue("btReview").Enabled = false;

                var drow = dsDoc.Tables[0].Rows[0];
                AmendNo = drow["AmendNo"].ToString();
                Amount_Old = double.Parse(drow["Amount_Old"].ToString());
                Amount = double.Parse(drow["Amount"].ToString());
                B4_AUT_Amount = double.Parse(drow["B4_AUT_Amount"].ToString());
                
                drowDocColl = drow;

                SetVisibilityByStatus(ref drow);

                comboCollectionType.SelectedValue = drow["CollectionType"].ToString();
                lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Value;

                txtRemittingBankNo.Text = drow["RemittingBankNo"].ToString();
                CheckSwiftCodeExist();

                txtRemittingBankAddr1.Text = drow["RemittingBankAddr"].ToString();
                txtRemittingBankAddr2.Text = drow["RemittingBankAddr2"].ToString();
                txtRemittingBankAddr3.Text = drow["RemittingBankAddr3"].ToString();

                comboRemittingBankAcct.SelectedValue = drow["RemittingBankAcct"].ToString();
                txtRemittingBankRef.Text = drow["RemittingBankRef"].ToString();

                comboDraweeCusNo.SelectedValue = drow["DraweeCusNo"].ToString();
                txtDraweeCusName.Text = drow["DraweeCusName"].ToString();
                txtDraweeAddr1.Text = drow["DraweeAddr1"].ToString();
                txtDraweeAddr2.Text = drow["DraweeAddr2"].ToString();
                txtDraweeAddr3.Text = drow["DraweeAddr3"].ToString();

                comboReimDraweeAcct.SelectedValue = drow["ReimbDraweeAcct"].ToString();

                txtDrawerCusNo.Text = drow["DrawerCusNo"].ToString();
                txtDrawerCusName.Text = drow["DrawerCusName"].ToString();
                txtDrawerAddr.Text = drow["DrawerAddr"].ToString();
                txtDrawerAddr1.Text = drow["DrawerAddr1"].ToString();
                txtDrawerAddr2.Text = drow["DrawerAddr2"].ToString();

                comboCurrency.SelectedValue = drow["Currency"].ToString();
                //numAmount.Text = drow["Amount"].ToString();
                numAmount.Text = drow["B4_AUT_Amount"].ToString();
                if (drow["DocsReceivedDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDocsReceivedDate.SelectedDate = DateTime.Parse(drow["DocsReceivedDate"].ToString());
                }
                if (drow["MaturityDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteMaturityDate.SelectedDate = DateTime.Parse(drow["MaturityDate"].ToString());
                }
                txtTenor.Text = drow["Tenor"].ToString();
                numDays.Text = drow["Days"].ToString();
                if (drow["TracerDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteTracerDate.SelectedDate = DateTime.Parse(drow["TracerDate"].ToString());
                }
                numReminderDays.Text = drow["ReminderDays"].ToString();

                comboCommodity.SelectedValue = drow["Commodity"].ToString();
                txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name"];

                comboDocsCode1.SelectedValue = drow["DocsCode1"].ToString();
                numNoOfOriginals1.Text = drow["NoOfOriginals1"].ToString();
                numNoOfCopies1.Text = drow["NoOfCopies1"].ToString();

                if (!string.IsNullOrEmpty(drow["DocsCode2"].ToString())
                    && (drow["NoOfOriginals2"].ToString() != "0"
                    || drow["NoOfCopies2"].ToString() != "0"))
                {
                    divDocsCode.Visible = true;
                }
                comboDocsCode2.SelectedValue = drow["DocsCode2"].ToString();
                numNoOfOriginals2.Text = drow["NoOfOriginals2"].ToString();
                numNoOfCopies2.Text = drow["NoOfCopies2"].ToString();

                txtEdittor_OtherDocs.Content = drow["OtherDocs"].ToString();
                txtEdittor_InstructionToCus.Content = drow["InstructionToCus"].ToString();

                txtRemarks.Text = drow["Remarks"].ToString();

                // DocumentaryCollectionCancel
                if (!string.IsNullOrEmpty(drow["CancelDate"].ToString()) && drow["CancelDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteCancelDate.SelectedDate = DateTime.Parse(drow["CancelDate"].ToString());
                }

                if (!string.IsNullOrEmpty(drow["ContingentExpiryDate"].ToString()) && drow["ContingentExpiryDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteContingentExpiryDate.SelectedDate = DateTime.Parse(drow["ContingentExpiryDate"].ToString());
                }
                txtCancelRemark.Text = drow["CancelRemark"].ToString();
                // DocumentaryCollectionCancel

                //Incoming Collection Acception
                if (!string.IsNullOrEmpty(drow["AcceptedDate"].ToString()) && drow["AcceptedDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteAcceptedDate.SelectedDate = DateTime.Parse(drow["AcceptedDate"].ToString());
                }
                txtAcceptedRemarks.Text = drow["AcceptRemarks"].ToString();
                //Incoming Collection Acception

                // ===Amount_New, Tenor_New, TracerDate_New =================
                // Incoming Collection Amendments => tabid=218
                if (TabId == 218)
                {
                    if (!string.IsNullOrEmpty(drow["Amount_Old"].ToString()))
                    {
                        lblAmount_New.Text = Double.Parse(drow["Amount_Old"].ToString()).ToString("C", CultureInfo.CurrentCulture).Replace("$", "");
                        divAmount.Visible = true;
                    }
                    if (!string.IsNullOrEmpty(drow["Tenor_New"].ToString()))
                    {
                        lblTenor_New.Text = drow["Tenor_New"].ToString();
                        divTenor.Visible = true;
                    }
                    if (!string.IsNullOrEmpty(drow["TracerDate_New"].ToString()))
                    {
                        if (drow["TracerDate_New"].ToString().IndexOf("1/1/1900") == -1)
                        {
                            lblTracerDate_New.Text = String.Format("{0:M/d/yyyy}", DateTime.Parse(drow["TracerDate_New"].ToString()));//.ToString("MM/dd/yyyy");
                            divTracerDate.Visible = true;
                        }
                    }
                    if (txtCode.Text.IndexOf(".") <= 0)
                    {
                        if (!string.IsNullOrEmpty(drow["NewAmendNo"].ToString()))
                            txtCode.Text = drow["NewAmendNo"].ToString();
                        else if (!string.IsNullOrEmpty(drow["AmendNo"].ToString()))
                            txtCode.Text = drow["AmendNo"].ToString();
                    }
                }
                // ===================================================

                comboDraweeType.SelectedValue = drow["DraweeType"].ToString();
                comboDrawerType.SelectedValue = drow["DrawerType"].ToString();

                comboAccountOfficer.SelectedValue = drow["AccountOfficer"].ToString();
                txtExpressNo.Text = drow["ExpressNo"].ToString();
                txtInvoiceNo.Text = drow["InvoiceNo"].ToString();
                txtDraftNo.Text = drow["DraftNo"].ToString();
                

                SetRelation_DrawerType();
                SetRelation_DraweeType();

                SetRelation_RemittingType();
            }
            else
            {
                comboCollectionType.SelectedValue = string.Empty;
                txtRemittingBankNo.Text = string.Empty;

                txtRemittingBankAddr1.Text = string.Empty;
                txtRemittingBankAddr2.Text = string.Empty;
                txtRemittingBankAddr3.Text = string.Empty;

                comboRemittingBankAcct.SelectedValue = string.Empty;
                txtRemittingBankRef.Text = string.Empty;

                comboDraweeCusNo.SelectedValue = string.Empty;
                txtDraweeAddr1.Text = string.Empty;
                txtDraweeAddr2.Text = string.Empty;
                txtDraweeAddr3.Text = string.Empty;

                comboReimDraweeAcct.SelectedValue = string.Empty;
                txtDrawerCusNo.Text = string.Empty;
                comboCurrency.SelectedValue = string.Empty;
                numAmount.Text = string.Empty;
                dteDocsReceivedDate.SelectedDate = null;
                dteMaturityDate.SelectedDate = null;
                dteTracerDate.SelectedDate = null;
                txtTenor.Text = string.Empty;
                numDays.Text = string.Empty;
                numReminderDays.Text = string.Empty;

                comboCommodity.SelectedValue = string.Empty;
                txtCommodityName.Text = "";

                comboDocsCode1.SelectedValue = string.Empty;
                numNoOfOriginals1.Text = string.Empty;
                numNoOfCopies1.Text = string.Empty;

                comboDocsCode2.SelectedValue = string.Empty;
                numNoOfOriginals2.Text = string.Empty;
                numNoOfCopies2.Text = string.Empty;

                txtEdittor_OtherDocs.Content = string.Empty;
                txtEdittor_InstructionToCus.Content = string.Empty;

                lblCollectionTypeName.Text = string.Empty;
                lblRemittingBankName.Text = string.Empty;
                txtDraweeCusName.Text = string.Empty;
                txtDrawerCusName.Text = string.Empty;

                txtDrawerAddr.Text = string.Empty;
                txtDrawerAddr1.Text = string.Empty;
                txtDrawerAddr2.Text = string.Empty;

                txtRemarks.Text = string.Empty;

                dteCancelDate.SelectedDate = DateTime.Now;
                dteContingentExpiryDate.SelectedDate = DateTime.Now;

                //Incoming Collection Acception
                dteAcceptedDate.SelectedDate = DateTime.Now;
                txtAcceptedRemarks.Text = string.Empty;
                //Incoming Collection Acception

                comboDraweeType.SelectedValue = "A";
                comboDrawerType.SelectedValue = "A";

                comboAccountOfficer.SelectedValue = string.Empty;
                txtExpressNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtDraftNo.Text = string.Empty;
                txtCancelRemark.Text = string.Empty;

                SetRelation_DrawerType();
                SetRelation_DraweeType();
                SetRelation_RemittingType();
                Cal_TracerDate(false);
            }

            #endregion
            
            #region tab Charge
            if (dsDoc.Tables[1].Rows.Count > 0 && (TabId!= 218 || AmendNo.Equals(txtCode.Text)))
            {
                var drow1 = dsDoc.Tables[1].Rows[0];

                comboWaiveCharges.SelectedValue = drow1["WaiveCharges"].ToString();

                tbChargePeriod.Text = drow1["ChargePeriod"].ToString();
                rcbChargeCcy.SelectedValue = drow1["ChargeCcy"].ToString();
                if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                {
                    LoadChargeAcct();
                    rcbChargeAcct.SelectedValue = drow1["ChargeAcct"].ToString();
                }
                tbExcheRate.Text = drow1["ExchRate"].ToString();
                tbChargeAmt.Text = drow1["ChargeAmt"].ToString();

                ChargeAmount = double.Parse(drow1["ChargeAmt"].ToString());

                rcbPartyCharged.SelectedValue = drow1["PartyCharged"].ToString();
                lblPartyCharged.Text = drow1["PartyCharged"].ToString();

                rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                lblChargeStatus.Text = drow1["ChargeStatus"].ToString();

                tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                tbVatNo.Text = drow1["VATNo"].ToString();
                lblTaxCode.Text = drow1["TaxCode"].ToString();
                lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                lblTaxAmt.Text = drow1["TaxAmt"].ToString();

                tbChargeCode.SelectedValue = drow1["Chargecode"].ToString();
            }
            else
            {
                comboWaiveCharges.SelectedValue = string.Empty;
                rcbChargeAcct.SelectedValue = string.Empty;
                tbChargePeriod.Text = "1";
                rcbChargeCcy.SelectedValue = string.Empty;
                tbExcheRate.Text = string.Empty;
                tbChargeAmt.Text = string.Empty;

                rcbPartyCharged.SelectedValue = string.Empty;
                lblPartyCharged.Text = string.Empty;

                rcbOmortCharge.SelectedValue = string.Empty;

                rcbChargeStatus.SelectedValue = string.Empty;
                lblChargeStatus.Text = string.Empty;

                tbChargeRemarks.Text = string.Empty;
                //tbVatNo.Text = string.Empty;
                GeneralVATNo();

                lblTaxCode.Text = string.Empty;
                lblTaxCcy.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;

                tbChargeCode.SelectedValue = string.Empty;
                lblPartyCharged.Text = string.Empty;
            }

            if (dsDoc.Tables[2].Rows.Count > 0 &&  !string.IsNullOrEmpty(dsDoc.Tables[2].Rows[0]["Chargecode"].ToString()))
            {
                var drow2 = dsDoc.Tables[2].Rows[0];

                divCharge2.Visible = true;
                divChargeInfo2.Visible = true;

                tbChargePeriod2.Text = drow2["ChargePeriod"].ToString();
                rcbChargeCcy2.SelectedValue = drow2["ChargeCcy"].ToString();

                if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                {
                    LoadChargeAcct2();
                    rcbChargeAcct2.SelectedValue = drow2["ChargeAcct"].ToString();
                }

                tbExcheRate2.Text = drow2["ExchRate"].ToString();
                tbChargeAmt2.Text = drow2["ChargeAmt"].ToString();
                rcbPartyCharged2.SelectedValue = drow2["PartyCharged"].ToString();
                lblPartyCharged2.Text = drow2["PartyCharged"].ToString();
                rcbOmortCharges2.SelectedValue = drow2["OmortCharges"].ToString();
                rcbChargeStatus2.SelectedValue = drow2["ChargeStatus"].ToString();
                lblChargeStatus2.Text = drow2["ChargeStatus"].ToString();

                lblTaxCode2.Text = drow2["TaxCode"].ToString();
                lblTaxCcy2.Text = drow2["TaxCcy"].ToString();
                lblTaxAmt2.Text = drow2["TaxAmt"].ToString();

                tbChargecode2.SelectedValue = drow2["Chargecode"].ToString();
            }
            else
            {
                rcbChargeAcct2.SelectedValue = string.Empty;
                tbChargePeriod2.Text = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbExcheRate2.Text = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = string.Empty;
                lblPartyCharged2.Text = string.Empty;
                rcbOmortCharges2.SelectedValue = string.Empty;

                rcbChargeStatus2.SelectedValue = string.Empty;
                lblChargeStatus2.Text = string.Empty;

                lblTaxCode2.Text = string.Empty;
                lblTaxCcy2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;

                tbChargecode2.SelectedValue = string.Empty;
                lblPartyCharged2.Text = string.Empty;
            }

            WaiveCharge_Changed();

            #endregion

            #region tab MT410
            if (dsDoc.Tables[3].Rows.Count > 0 && !isMT412Active)
            {
                var drowMT410 = dsDoc.Tables[3].Rows[0];

                comboCreateMT410.SelectedValue = drowMT410["GeneralMT410_1"].ToString();
                CreateMT410 = drowMT410["GeneralMT410_1"].ToString();
                SetRelation_CreateMT410();

                txtGeneralMT410_2.Text = drowMT410["GeneralMT410_2"].ToString();
                txtSendingBankTRN.Text = drowMT410["SendingBankTRN"].ToString();
                txtRelatedReference.Text = drowMT410["RelatedReference"].ToString();
                comboCurrency_TabMT410.SelectedValue = drowMT410["Currency"].ToString();
                numAmount_TabMT410.Text = drowMT410["Amount"].ToString();

                txtSenderToReceiverInfo_410_1.Text = drowMT410["SenderToReceiverInfo1"].ToString();
                txtSenderToReceiverInfo_410_2.Text = drowMT410["SenderToReceiverInfo2"].ToString();
                txtSenderToReceiverInfo_410_3.Text = drowMT410["SenderToReceiverInfo3"].ToString();

                dteMaturityDateMT412.SelectedDate = dteMaturityDate.SelectedDate;
            }
            else if (dsDoc.Tables[4].Rows.Count > 0 && isMT412Active)//MT412
            {
                var drowMT412 = dsDoc.Tables[4].Rows[0];

                comboCreateMT410.SelectedValue = drowMT412["GeneralMT412_1"].ToString();
                CreateMT410 = drowMT412["GeneralMT412_1"].ToString();
                SetRelation_CreateMT410();

                txtGeneralMT410_2.Text = drowMT412["GeneralMT412_2"].ToString();
                txtSendingBankTRN.Text = drowMT412["SendingBankTRN"].ToString();
                txtRelatedReference.Text = drowMT412["RelatedReference"].ToString();
                comboCurrency_TabMT410.SelectedValue = drowMT412["Currency"].ToString();
                numAmount_TabMT410.Text = drowMT412["Amount"].ToString();

                txtSenderToReceiverInfo_410_1.Text = drowMT412["SenderToReceiverInfo1"].ToString();
                txtSenderToReceiverInfo_410_2.Text = drowMT412["SenderToReceiverInfo2"].ToString();
                txtSenderToReceiverInfo_410_3.Text = drowMT412["SenderToReceiverInfo3"].ToString();

                dteMaturityDateMT412.SelectedDate = dteMaturityDate.SelectedDate;
            }
            else
            {
                comboCreateMT410.SelectedValue = string.Empty;
                SetRelation_CreateMT410();

                txtGeneralMT410_2.Text = string.Empty;
                txtSendingBankTRN.Text = string.Empty;
                txtRelatedReference.Text = txtRemittingBankRef.Text;
                comboCurrency_TabMT410.SelectedValue = string.Empty;
                numAmount_TabMT410.Text = string.Empty;

                txtSenderToReceiverInfo_410_1.Text = string.Empty;
                txtSenderToReceiverInfo_410_2.Text = string.Empty;
                txtSenderToReceiverInfo_410_3.Text = string.Empty;
            }
            #endregion
        }

        protected void SaveData()
        {
            // Update status
            var comeFromUrl = "";
            if (Request.QueryString["key"] != null)
            {
                comeFromUrl = Request.QueryString["key"];
            }

            if (TabId == 218)
            {
                comeFromUrl = "incollamendment";
            }
            if (TabId == 219)
            {
                comeFromUrl = "doccolcancel";
            }
            if (TabId == 281)
            {
                comeFromUrl = "incollaccepted";
            }

            bd.SQLData.B_BDOCUMETARYCOLLECTION_Insert(txtCode.Text.Trim()
                , comboCollectionType.SelectedValue
                , txtRemittingBankNo.Text
                , txtRemittingBankAddr1.Text.Trim()
                , comboRemittingBankAcct.SelectedValue
                , txtRemittingBankRef.Text
                , comboDraweeCusNo.SelectedValue
                , txtDraweeAddr1.Text.Trim()
                , txtDraweeAddr2.Text.Trim()
                , txtDraweeAddr3.Text.Trim()
                , comboReimDraweeAcct.SelectedValue
                , txtDrawerCusNo.Text.Trim()
                , txtDrawerAddr.Text.Trim()
                , comboCurrency.SelectedValue
                , numAmount.Value.ToString()
                , dteDocsReceivedDate.SelectedDate.ToString()
                , dteMaturityDate.SelectedDate.ToString()
                , txtTenor.Text.Trim()
                , numDays.Value.ToString()
                , dteTracerDate.SelectedDate.ToString()
                , numReminderDays.Value.ToString()
                , comboCommodity.SelectedValue
                , comboDocsCode1.SelectedValue
                , numNoOfOriginals1.Value.ToString()
                , numNoOfCopies1.Value.ToString()
                , comboDocsCode2.SelectedValue
                , numNoOfOriginals2.Value.ToString()
                , numNoOfCopies2.Value.ToString()
                , txtEdittor_OtherDocs.Content
                , txtEdittor_InstructionToCus.Content
                , UserId.ToString()
                , txtDrawerAddr1.Text.Trim()
                , txtDrawerAddr2.Text.Trim()
                , txtRemarks.Text.Trim()
                , dteCancelDate.SelectedDate.ToString()
                , dteContingentExpiryDate.SelectedDate.ToString()
                , txtDrawerCusName.Text.Trim()
                , txtDraweeCusName.Text.Trim()
                , comboDraweeType.SelectedValue
                , comboDrawerType.SelectedValue
                , comboAccountOfficer.SelectedValue
                , txtExpressNo.Text.Trim()
                , txtInvoiceNo.Text.Trim()
                , txtCancelRemark.Text.Trim()
                , txtRemittingBankAddr2.Text.Trim()
                , txtRemittingBankAddr3.Text.Trim()
                , comeFromUrl
                , dteAcceptedDate.SelectedDate.ToString()
                , txtAcceptedRemarks.Text.Trim()
                , txtDraftNo.Text
                );

            bd.SQLData.B_BDOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                tbChargeCode.SelectedValue, rcbChargeAcct.SelectedValue, tbChargePeriod.Text,
                rcbChargeCcy.SelectedValue, tbExcheRate.Text, tbChargeAmt.Text, rcbPartyCharged.SelectedValue,
                rcbOmortCharge.SelectedValue, "", "",
                rcbChargeStatus.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode.Text, lblTaxCcy.Text,
                lblTaxAmt.Text, "", "", "1", TabId);

            //Ban đầu tạo 2 code phí, sau đó xóa đi 1 code phí, thì hệ thống không ghi nhận (không xóa được code phí)
            //-> Xư lý dấu '-' remove all value control 2
            bd.SQLData.B_BDOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(), comboWaiveCharges.SelectedValue,
                tbChargecode2.SelectedValue, rcbChargeAcct2.SelectedValue, tbChargePeriod2.Text,
                rcbChargeCcy2.SelectedValue, tbExcheRate2.Text, tbChargeAmt2.Text, rcbPartyCharged2.SelectedValue,
                rcbOmortCharges2.SelectedValue, "", "",
                rcbChargeStatus2.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode2.Text,
                lblTaxCcy2.Text, lblTaxAmt2.Text, "", "", "2", TabId);


            if (isMT412Active)
            {
                bd.SQLData.B_BDOCUMETARYCOLLECTIONMT412_Insert(txtCode.Text.Trim(),
                comboCreateMT410.SelectedValue.Trim(),
                txtGeneralMT410_2.Text.Trim(),
                txtSendingBankTRN.Text.Trim(),
                txtRelatedReference.Text.Trim(),
                comboCurrency_TabMT410.SelectedValue,
                numAmount_TabMT410.Text,
                txtSenderToReceiverInfo_410_1.Text,
                txtSenderToReceiverInfo_410_2.Text,
                txtSenderToReceiverInfo_410_3.Text
                );
            }
            else
            {
                bd.SQLData.B_BDOCUMETARYCOLLECTIONMT410_Insert(txtCode.Text.Trim(),
                comboCreateMT410.SelectedValue.Trim(),
                txtGeneralMT410_2.Text.Trim(),
                txtSendingBankTRN.Text.Trim(),
                txtRelatedReference.Text.Trim(),
                comboCurrency_TabMT410.SelectedValue,
                numAmount_TabMT410.Text,
                txtSenderToReceiverInfo_410_1.Text,
                txtSenderToReceiverInfo_410_2.Text,
                txtSenderToReceiverInfo_410_3.Text
                );
            }
            
        }

        protected void btAddDocsCode_Click(object sender, ImageClickEventArgs e)
        {
            divDocsCode.Visible = true;
        }

        protected void comboCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }

        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedValue.ToString();
        }

        protected void rcbChargeAcct2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblChargeAcct2.Text = "TKTT VND " + rcbChargeAcct2.SelectedValue.ToString();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue.ToString();
        }

        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged.Text = rcbPartyCharged.SelectedValue.ToString();
        }

        protected void rcbChargeAcct_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblChargeAcct.Text = "TKTT VND " + rcbChargeAcct.SelectedValue.ToString();
        }
        
        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            double sotien = 0;
            if (tbChargeAmt.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt.Value.ToString());
                sotien = sotien*0.1;
            }
            lblTaxAmt.Text = String.Format("{0:C}", sotien).Replace("$", "");
            lblTaxCode.Text = "81      10% VAT on Charge";
        }

        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            double sotien = 0;
            if (tbChargeAmt2.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt2.Value.ToString());
                sotien = sotien*0.1;
            }
            lblTaxAmt2.Text = String.Format("{0:C}", sotien).Replace("$", "");
            lblTaxCode2.Text = "81      10% VAT on Charge";
        }

        protected void btThem_Click(object sender, ImageClickEventArgs e)
        {
            divCharge2.Visible = true;
            divChargeInfo2.Visible = true;
        }

        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            btThem.Enabled = flag;
            //txtSenderToReceiverInfo.SetEnable(flag);
            tbVatNo.Enabled = false;
        }

        protected void comboDraweeType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_DraweeType();
        }

        protected void comboDrawerType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_DrawerType();
        }

        protected void SetRelation_DraweeType()
        {
            //switch (comboDraweeType.SelectedValue)
            //{
            //    case "A":
            //        comboDraweeCusNo.Enabled = true;
            //        txtDraweeCusName.Enabled = false;
            //        txtDraweeAddr1.Enabled = false;
            //        txtDraweeAddr2.Enabled = false;
            //        txtDraweeAddr3.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        comboDraweeCusNo.Enabled = false;
            //        txtDraweeCusName.Enabled = true;
            //        txtDraweeAddr1.Enabled = true;
            //        txtDraweeAddr2.Enabled = true;
            //        txtDraweeAddr3.Enabled = true;
            //        break;
            //}
        }

        protected void SetRelation_DrawerType()
        {
            //switch (comboDrawerType.SelectedValue)
            //{
            //    case "A":
            //        txtDrawerCusNo.Enabled = true;
            //        txtDrawerCusName.Enabled = true;
            //        txtDrawerAddr.Enabled = false;
            //        txtDrawerAddr1.Enabled = false;
            //        txtDrawerAddr2.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        txtDrawerCusNo.Enabled = false;
            //        txtDrawerCusName.Enabled = false;
            //        txtDrawerAddr.Enabled = true;
            //        txtDrawerAddr1.Enabled = true;
            //        txtDrawerAddr2.Enabled = true;
            //        break;
            //}
        }

        protected void comboRemittingType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_RemittingType();
        }

        protected void SetRelation_RemittingType()
        {
            //switch (comboRemittingType.SelectedValue)
            //{
            //    case "A":
            //        comboRemittingBankNo.Enabled = true;
            //        txtRemittingBankAddr1.Enabled = false;
            //        txtRemittingBankAddr2.Enabled = false;
            //        txtRemittingBankAddr3.Enabled = false;
            //        break;
            //    case "B":
            //    case "D":
            //        comboRemittingBankNo.Enabled = false;
            //        txtRemittingBankAddr1.Enabled = true;
            //        txtRemittingBankAddr2.Enabled = true;
            //        txtRemittingBankAddr3.Enabled = true;
            //        break;
            //}
        }

        protected void Cal_TracerDate(bool isCallFromSelected)
        {
            var dteNow = DateTime.Now;
            dteNow = dteNow.AddDays(30);
            dteTracerDate.SelectedDate = dteNow;
            dteDocsReceivedDate.SelectedDate = DateTime.Now;
        }

        protected void dteDocsReceivedDate_OnSelectedDateChanged(object sender, SelectedDateChangedEventArgs e)
        {
            if (dteDocsReceivedDate.SelectedDate != null)
            {
                var dteNow = DateTime.Parse(dteDocsReceivedDate.SelectedDate.ToString());
                dteNow = dteNow.AddDays(30);
                dteTracerDate.SelectedDate = dteNow;
            }
            else
            {
                dteTracerDate.SelectedDate = null;
            }
        }

        protected void txtRemittingBankRef_OnTextChanged(object sender, EventArgs e)
        {
            txtRelatedReference.Text = txtRemittingBankRef.Text;
        }

        protected void comboCurrency_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            comboCurrency_TabMT410.SelectedValue = comboCurrency.SelectedValue;
        }

        protected void numAmount_OnTextChanged(object sender, EventArgs e)
        {
            numAmount_TabMT410.Value = numAmount.Value;
        }

        protected void comboRemittingBankNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct();
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct2();
        }

        protected void LoadChargeAcct()
        {
            rcbChargeAcct.Items.Clear();
            rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct.DataValueField = "Id";
            rcbChargeAcct.DataTextField = "Id";
            rcbChargeAcct.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDraweeCusNo.SelectedItem.Attributes["CustomerName2"] ,rcbChargeCcy.SelectedValue);
            rcbChargeAcct.DataBind();
        }

        protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDraweeCusNo.SelectedItem.Attributes["CustomerName2"], rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        protected void comboCreateMT410_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            SetRelation_CreateMT410();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 22, 2015    Hien Nguyen       Fix bug 80
         */
        protected void SetRelation_CreateMT410()
        {
            if (comboCreateMT410.SelectedValue.ToLower() == "yes")
            {
                txtSendingBankTRN.Enabled = true;
                txtRelatedReference.Enabled = true;
                comboCurrency_TabMT410.Enabled = true;
                numAmount_TabMT410.Enabled = true;
                dteMaturityDateMT412.Enabled = true;
                //txtSenderToReceiverInfo.SetEnable(true);
            }
            else
            {
                txtSendingBankTRN.Enabled = false;
                txtRelatedReference.Enabled = false;
                comboCurrency_TabMT410.Enabled = false;
                numAmount_TabMT410.Enabled = false;
                dteMaturityDateMT412.Enabled = false;
                //txtSenderToReceiverInfo.SetEnable(false);
            }
        }

        protected void SetValueToSendingBankTRN()
        {
            txtSendingBankTRN.Text = txtCode.Text.Trim() + comboCollectionType.SelectedValue;
        }

        protected void txtRemittingBankNo_OnTextChanged(object sender, EventArgs e)
        {
            CheckSwiftCodeExist();
        }

        protected void CheckSwiftCodeExist()
        {
            lblRemittingBankNoError.Text = "";
            lblRemittingBankName.Text = "";
            txtRemittingBankAddr1.Text = "";
            txtRemittingBankAddr2.Text = "";
            txtRemittingBankAddr3.Text = "";
            if (string.IsNullOrEmpty(txtRemittingBankNo.Text.Trim())) return;
            //
            var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtRemittingBankNo.Text.Trim());
            if (dtBSWIFTCODE == null || dtBSWIFTCODE.Rows.Count <= 0)
            {
                DisabledTab410(false);
                lblRemittingBankNoError.Text = "No found swiftcode";
                return;
            }
            var dr = dtBSWIFTCODE.Rows[0];
            lblRemittingBankName.Text = dr["BankName"].ToString();
            //txtRemittingBankAddr1.Text = "";
            txtRemittingBankAddr2.Text = dr["City"].ToString();
            txtRemittingBankAddr3.Text = dr["Country"].ToString();
            if (!string.IsNullOrEmpty(dr["RMA_Flag"].ToString()))
            {
                switch (TabId)
                {
                    case 217: //Register Documetary Collection
                    case 281: //Incoming Collection Amendments
                        //divTM410.Visible = true;
                        DisabledTab410(true);
                        break;
                }
            }
            else
            {
                //divTM410.Visible = false;
                DisabledTab410(false);
            }
        }

        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            WaiveCharge_Changed();
        }

        private void WaiveCharge_Changed()
        {
            if (comboWaiveCharges.SelectedValue == "NO")
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

                //lblChargeAcct.Visible = true;
                lblPartyCharged.Visible = true;
                lblChargeStatus.Visible = true;
            }
            else if (comboWaiveCharges.SelectedValue == "YES")
            {
                //ResetValueTabChargeIf();

                btThem.Visible = false;
                tbChargeAmt.Visible = false;
                tbChargeAmt.Value = null;

                tbChargeCode.Visible = false;
                tbChargeCode.SelectedValue = string.Empty;

                tbChargePeriod.Visible = false;
                tbChargePeriod.Text = "1";

                tbChargeRemarks.Visible = false;
                tbChargeRemarks.Text = string.Empty;

                tbExcheRate.Visible = false;
                tbExcheRate.Value = null;

                tbVatNo.Visible = false;
                rcbChargeAcct.Visible = false;
                rcbChargeAcct.SelectedValue = string.Empty;

                rcbChargeStatus.Visible = false;
                rcbChargeStatus.SelectedValue = string.Empty;

                rcbOmortCharge.Visible = false;
                rcbOmortCharge.SelectedValue = string.Empty;

                rcbPartyCharged.Visible = false;
                rcbPartyCharged.SelectedValue = string.Empty;

                rcbChargeCcy.Visible = false;
                rcbChargeCcy.SelectedValue = string.Empty;

                lblPartyCharged.Visible = false;
                lblChargeStatus.Visible = false;

                lblTaxCode.Text = string.Empty;
                lblTaxAmt.Text = "0";

                // an chargecode 2
                divCharge2.Visible = false;
                divChargeInfo2.Visible = false;

                rcbChargeAcct2.SelectedValue = string.Empty;
                tbChargecode2.SelectedValue = string.Empty;

                tbChargePeriod2.Text = "1";
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbExcheRate2.Text = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = string.Empty;
                lblPartyCharged2.Text = string.Empty;
                rcbOmortCharges2.SelectedValue = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblChargeStatus2.Text = string.Empty;
                lblTaxCode2.Text = string.Empty;
                lblTaxCcy2.Text = string.Empty;
                lblTaxAmt2.Text = "0";

                lblPartyCharged2.Text = string.Empty;
                lblChargeStatus2.Text = string.Empty;
            }
        }

        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/";
            string reportSaveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {                
                switch (reportType)
                {
                    case 1://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionMT410.doc");
                        reportSaveName = "RegisterDocumentaryCollectionMT410" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        if (isMT412Active)
                        {
                            reportData = bd.SQLData.B_BDOCUMETARYCOLLECTIONMT412_Report(txtCode.Text);
                        }
                        else
                        {
                            reportData = bd.SQLData.B_BDOCUMETARYCOLLECTIONMT410_Report(txtCode.Text);
                        }

                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        break;
                    case 2://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG.doc");
                        reportSaveName = "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_BDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        break;
                    case 3://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionVAT.doc");
                        reportSaveName = "RegisterDocumentaryCollectionVAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_BDOCUMETARYCOLLECTION_VAT_Report(txtCode.Text, UserInfo.Username, TabId);
                        break;
                    case 4://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAmendmentsPHIEUXUATNGOAIBANG.doc");
                        reportSaveName = "IncomingCollectionAmendmentsPHIEUXUATNGOAIBANG" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_INCOMINGCOLLECTIONAMENDMENT_PHIEUXUATNGOAIBANG_REPORT(txtCode.Text, UserInfo.Username);
                        break;
                    case 5://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG.doc");
                        reportSaveName = "IncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_INCOMINGCOLLECTIONAMENDMENT_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        break;
                    case 6://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAmendmentsVAT.doc");
                        reportSaveName = "IncomingCollectionAmendmentsVAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_INCOMINGCOLLECTIONAMENDMENT_VAT_Report(txtCode.Text, UserInfo.Username, TabId);
                        break;
                    case 7://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAmendmentsMT410.doc");
                        reportSaveName = "IncomingCollectionAmendmentsMT410" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        if (isMT412Active)
                        {
                            reportData = bd.SQLData.B_INCOMINGCOLLECTIONAMENDMENT_MT412_Report(txtCode.Text);
                        }
                        else
                        {
                            reportData = bd.SQLData.B_INCOMINGCOLLECTIONAMENDMENT_MT410_Report(txtCode.Text);
                        }
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        break;
                    case 8://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "DocumentaryCollectionCancelVAT.doc");
                        reportSaveName = "DocumentaryCollectionCancelVAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_DOCUMENTARYCOLLECTIONCANCEL_VAT_REPORT(txtCode.Text, UserInfo.Username, TabId);
                        break;
                    case 9://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "DocumentaryCollectionCancelPHIEUXUATNGOAIBANG.doc");
                        reportSaveName = "DocumentaryCollectionCancelPHIEUXUATNGOAIBANG" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_DOCUMENTARYCOLLECTIONCANCEL_PHIEUXUATNGOAIBANG_REPORT(txtCode.Text, UserInfo.Username);
                        break;
                    case 10://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAcceptionMT412.doc");
                        reportSaveName = "IncomingCollectionAcceptionMT412" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                        if (isMT412Active)
                        {
                            reportData = bd.SQLData.B_INCOMINGCOLLECTIONACCEPTION_MT412_Report(txtCode.Text);
                        }
                        else
                        {
                            reportData = bd.SQLData.B_INCOMINGCOLLECTIONACCEPTION_MT410_Report(txtCode.Text);
                        }
                        saveFormat = Aspose.Words.SaveFormat.Pdf;
                        break;
                    case 11://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAcceptionVAT.doc");
                        reportSaveName = "IncomingCollectionAcceptionVAT" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_INCOMINGCOLLECTIONACCEPTION_VAT_REPORT(txtCode.Text, UserInfo.Username, TabId);
                        break;
                    case 12://
                        reportTemplate = Context.Server.MapPath(reportTemplate + "IncomingCollectionAcceptionPHIEUNHAPNGOAIBANG.doc");
                        reportSaveName = "IncomingCollectionAcceptionPHIEUNHAPNGOAIBANG" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.B_INCOMINGCOLLECTIONACCEPTION_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        break;
                }
                if (reportData != null)
                {
                    try
                    {
                        //reportData.Tables[0].TableName = "Table1";
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
        protected void btnMT410Report_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        protected void btnPhieuNgoaiBangReport_Click(object sender, EventArgs e)
        {
            showReport(2);
        }
        protected void btnVATReport_Click(object sender, EventArgs e)
        {
            showReport(3);
        }

        protected void btnIncomingCollectionAmendmentsPHIEUXUATNGOAIBANG_Click(object sender, EventArgs e)
        {
            showReport(4);
        }
        protected void btnIncomingCollectionAmendmentsPHIEUNHAPNGOAIBANG_Click(object sender, EventArgs e)
        {
            showReport(5);
        }
        protected void btnIncomingCollectionAmendmentsVAT_Click(object sender, EventArgs e)
        {
            showReport(6);
        }
        protected void btnIncomingCollectionAmendmentsMT410_Click(object sender, EventArgs e)
        {
            showReport(7);
        }

        protected void btnCancelDocumentaryVAT_Click(object sender, EventArgs e)
        {
            showReport(8);
        }
        protected void btnCancelDocumentaryPHIEUXUATNGOAIBANG_Click(object sender, EventArgs e)
        {
            showReport(9);
        }

        protected void btnIncomingCollectionAcceptionMT412_Click(object sender, EventArgs e)
        {
            showReport(10);
        }
        protected void btnIncomingCollectionAcceptionVAT_Click(object sender, EventArgs e)
        {
            showReport(11);
        }
        protected void btnIncomingCollectionAcceptionPHIEUNHAPNGOAIBANG_Click(object sender, EventArgs e)
        {
            showReport(12);
        }
        
        protected void ResetValueTabChargeIf()
        {
            btThem.Enabled = true;

            comboWaiveCharges.SelectedValue = string.Empty;
            comboWaiveCharges.Enabled = true;

            rcbChargeAcct.SelectedValue = string.Empty;
            rcbChargeAcct.Enabled = true;

            tbChargePeriod.Text = "1";
            tbChargePeriod.Enabled = true;

            rcbChargeCcy.SelectedValue = string.Empty;
            rcbChargeCcy.Enabled = true;

            tbExcheRate.Text = string.Empty;
            tbExcheRate.Enabled = true;

            tbChargeAmt.Text = string.Empty;
            tbChargeAmt.Enabled = true;

            rcbPartyCharged.SelectedValue = string.Empty;
            rcbPartyCharged.Enabled = true;

            lblPartyCharged.Text = string.Empty;
            lblPartyCharged.Enabled = true;

            rcbOmortCharge.SelectedValue = string.Empty;
            rcbOmortCharge.Enabled = true;

            rcbChargeStatus.SelectedValue = string.Empty;
            rcbChargeStatus.Enabled = true;
            lblChargeStatus.Text = string.Empty;

            tbChargeRemarks.Text = string.Empty;
            tbChargeRemarks.Enabled = true;

            tbVatNo.Text = string.Empty;
            lblTaxCode.Text = string.Empty;
            lblTaxCcy.Text = string.Empty;
            lblTaxAmt.Text = string.Empty;

            tbChargeCode.SelectedValue = string.Empty;
            tbChargeCode.Enabled = true;

            lblPartyCharged.Text = string.Empty;
            lblChargeStatus.Text = string.Empty;

            // charge 2
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;

            rcbChargeAcct2.SelectedValue = string.Empty;
            rcbChargeAcct2.Enabled = true;

            tbChargePeriod2.Text = "1";
            tbChargePeriod2.Enabled = true;

            rcbChargeCcy2.SelectedValue = string.Empty;
            rcbChargeCcy2.Enabled = true;

            tbExcheRate2.Text = string.Empty;
            tbExcheRate2.Enabled = true;

            tbChargeAmt2.Text = string.Empty;
            tbChargeAmt2.Enabled = true;

            rcbPartyCharged2.SelectedValue = string.Empty;
            rcbPartyCharged2.Enabled = true;

            lblPartyCharged2.Text = string.Empty;
            rcbOmortCharges2.SelectedValue = string.Empty;
            rcbOmortCharges2.Enabled = true;

            rcbChargeStatus2.SelectedValue = string.Empty;
            rcbChargeStatus2.Enabled = true;

            lblChargeStatus2.Text = string.Empty;

            lblTaxCode2.Text = string.Empty;
            lblTaxCcy2.Text = string.Empty;
            lblTaxAmt2.Text = string.Empty;

            tbChargecode2.SelectedValue = string.Empty;
            tbChargecode2.Enabled = true;

            lblPartyCharged2.Text = string.Empty;
            lblChargeStatus2.Text = string.Empty;

            GeneralVATNo();
        }
        protected void GeneralVATNo()
        {
            tbVatNo.Text = bd.Database.B_BMACODE_GetNewSoTT("VATNO").Tables[0].Rows[0]["SoTT"].ToString();
        }
        protected void GeneralCode()
        {
            if (TabId == 217) //Register Documetary Collection
            {
                txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("DOCUMETARYCOLLECTION", Refix_BMACODE());
            }
            else
            {
                txtCode.Text = string.Empty;
            }
        }
        protected void SetVisibilityByStatus(ref DataRow drow)
        {
            // Incoming Collection Amendments => tabid=218
            // Documentary Collection Cancel => tabid=219
            // Incoming Collection Acception => tabid=281
            if (drow == null)
            {
                return;
            }
            lblError.Text = "";
            var errorUn_AUT = "This Incoming Documentary Collection has Not Authorized yet. Do not allow to process Payment Acceptance!";
            switch (TabId)
            {
                case 218: // Incoming Collection Amendments
                    if (TabId == 218 && Request.QueryString["key"] == null)
                    {
                        //Chỉ cho phép tu chỉnh đối với BCT:
                        //1. Đã authorised màn hình nhập nhờ thu (Reg)
                        //2, Chưa thực hiện cancel
                        //3, Chưa thực hiện thanh toán full 
                        if (drow["Status"].ToString() != bd.TransactionStatus.AUT)
                        {
                            lblError.Text = errorUn_AUT;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                            RadToolBar1.FindItemByValue("btRevert").Enabled = false;
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        } else if (drow["Amend_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary was authorized";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;

                        } else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary is cancel";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;

                        }
                        else if (drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This Documentary has payment full";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                    }
                    break;
                case 219: //  Documentary Collection Cancel
                    if (TabId == 219 && Request.QueryString["key"] == null)
                    {
                        //Chỉ cho cancel khi BCT:
                        //1, Nhập BCT đã được authorised
                        //2, Chưa thực hiện thanh toán full
                        if (drow["Status"].ToString() != bd.TransactionStatus.AUT)
                        {
                            lblError.Text = errorUn_AUT;
                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
                            RadToolBar1.FindItemByValue("btRevert").Enabled = false;
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        }
                        else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary is cancel";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                        else if (drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This Documentary has payment full";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                    }
                    break;
                case 281: //Incoming Collection Acception
                    if (TabId == 281 && Request.QueryString["key"] == null)
                    {
                        if (drow["Status"].ToString() != bd.TransactionStatus.AUT
                            || (!string.IsNullOrEmpty(drow["Cancel_Status"].ToString()) &&  drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                            || (!string.IsNullOrEmpty(drow["Amend_Status"].ToString()) && drow["Amend_Status"].ToString() != bd.TransactionStatus.AUT))
                        {
                            if (!string.IsNullOrEmpty(drow["Cancel_Status"].ToString()) && drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                            {
                                lblError.Text = "This Documentary is cancel";
                            }
                            else if (!string.IsNullOrEmpty(drow["Amend_Status"].ToString()) && drow["Amend_Status"].ToString() != bd.TransactionStatus.AUT)
                            {
                                lblError.Text = "This Documentary has not authorized at Amendment step.";
                            }
                            else
                            {
                                lblError.Text = errorUn_AUT;
                            }

                            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            RadToolBar1.FindItemByValue("btRevert").Enabled = false;
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        }
                        else if (drow["Accept_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary was authorized";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;

                        }
                        else if (drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This Documentary has payment full";

                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                    }
                    break;
                case 217: // Register Documetary Collection
                    if (TabId == 217 && Request.QueryString["key"] == null)
                    {
                        if (drow["Status"].ToString() == bd.TransactionStatus.AUT && drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This Documentary has payment full";
                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;

                        }
                        else if (drow["Cancel_Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary was canceled";
                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                        else if (drow["Status"].ToString() == bd.TransactionStatus.AUT)
                        {
                            lblError.Text = "This Documentary was authorized";
                            InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                            RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        }
                    }
                    break;
            }
        }

        protected void btnChargecode2_Click(object sender, ImageClickEventArgs e)
        {
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;

            tbChargecode2.SelectedValue = string.Empty;
            rcbChargeCcy2.SelectedValue = string.Empty;
            rcbChargeAcct2.SelectedValue = string.Empty;
            tbChargeAmt2.Value = 0;
            rcbPartyCharged2.SelectedValue = string.Empty;
            rcbOmortCharges2.SelectedValue = string.Empty;
            rcbChargeStatus2.SelectedValue = string.Empty;

            lblTaxCode2.Text = string.Empty;
            lblTaxCcy2.Text = string.Empty;
            lblTaxAmt2.Text = "0";
        }

        protected bool CheckFtVaild()
        {
            /*if (txtCode.Text.Length > 14 || txtCode.Text.Length < 14)
            {
                bc.Commont.ShowClientMessageBox(Page, GetType(), "FT No. is invalid", 150);
                return false;
            }*/
            return true;
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 22, 2015    Hien Nguyen       Fix bug 80
         */
        protected void DisabledTab410(bool flag)
        {
            comboCreateMT410.Enabled = flag;
            txtGeneralMT410_2.Enabled = flag;
            txtSendingBankTRN.Enabled = flag;
            txtRelatedReference.Enabled = flag;
            comboCurrency_TabMT410.Enabled = flag;
            numAmount_TabMT410.Enabled = flag;
            txtSenderToReceiverInfo_410_1.Enabled = flag;
            txtSenderToReceiverInfo_410_2.Enabled = flag;
            txtSenderToReceiverInfo_410_3.Enabled = flag;
            dteMaturityDateMT412.Enabled = flag;
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.21           Oct 22, 2015    Hien Nguyen       Fix bug 80
         */
        protected void dteMaturityDateMT412_SelectedDateChanged(object sender, EventArgs e)
        {
            dteMaturityDate.SelectedDate = ((SelectedDateChangedEventArgs)e).NewDate;
        }
    }
}