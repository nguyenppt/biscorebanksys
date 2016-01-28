using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using bd = BankProject.DataProvider;
using bc = BankProject.Controls;
using BankProject.DBContext;
using DotNetNuke.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;
using BankProject.DataProvider;
using System.Collections.Generic;


namespace BankProject.TradingFinance.Export.DocumentaryCollections
{
    
    public enum ExportDocumentaryScreenType
    {
        Register,
        Amend,
        Cancel,
        RegisterCc,
        Acception
    }

    public partial class ExportDocumentaryCollection : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        private VietVictoryCoreBankingEntities _entities = new VietVictoryCoreBankingEntities();
        public double Amount = 0;
        public double AmountNew = 0;
        public double AmountOld = 0;
        public double AmountAut = 0;
        public double ChargeAmount = 0;
        private DataRow _exportDoc;
        private BEXPORT_DOCUMETARYCOLLECTION _exportCollection;
        private ExportDocumentaryScreenType ScreenType
        {
            get
            {
                switch (TabId)
                {
                    case 229 :
                        return ExportDocumentaryScreenType.Amend;
                    case 230:
                        return ExportDocumentaryScreenType.Cancel;
                    case 227:
                        return ExportDocumentaryScreenType.RegisterCc;
                    case 377:
                        return ExportDocumentaryScreenType.Acception;
                    default:
                        return ExportDocumentaryScreenType.Register;
                }
            }
        }

        private string CodeId
        {
            get { return Request.QueryString["CodeID"]; }
        }
        private string StatusEnquiry
        {
            get { return Request.QueryString["Status"]; }
        }
        private bool Enqiry
        {
            get { return Request.QueryString["enquiry"] == "true"; }
        }
        private bool Disable
        {
            get { return Request.QueryString["disable"] == "1"; }
        }

        private string Refix_BMACODE()
        {
            return "TF";
        }
        //load phan thong tin amend
        private void LoadExportDocAmend()
        {
            try
            {
                var Code = CodeId;
                if (Code != null)
                {
                    //nhap vao khong co dau .
                    if (Code.IndexOf('.') == -1)
                    {
                        var objAmend = new BEXPORT_DOCUMETARYCOLLECTION();
                        var lstDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => (x.DocCollectCode == Code)).ToList();
                        if (lstDoc != null && lstDoc.Count > 0)
                        {

                            objAmend = lstDoc.Where(x => (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES) && (x.Amend_Status == "UNA" || x.Amend_Status == "REV")).FirstOrDefault();
                            if (objAmend != null)
                            {
                                var objCharge = _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Where(x => x.DocCollectCode == objAmend.DocCollectCode).ToList();
                                LoadDataForAmend(objAmend, objCharge);
                                txtCode.Text = objAmend.AmendNo;
                                _exportCollection = new BEXPORT_DOCUMETARYCOLLECTION();
                                _exportCollection = objAmend;
                            }
                            else
                            {
                                //truong hop chua co
                                var maxAmendId = lstDoc.Max(x => x.AmendId);
                                if (maxAmendId == null)
                                {
                                    maxAmendId = 1;
                                }
                                else
                                {
                                    maxAmendId = maxAmendId + 1;
                                }
                                var code = CodeId;
                                var ctnAmend = lstDoc.Where(x => (x.ActiveRecordFlag == null || x.ActiveRecordFlag == YesNo.YES)).FirstOrDefault();
                                if (ctnAmend != null)
                                {
                                    _exportCollection = new BEXPORT_DOCUMETARYCOLLECTION();
                                    _exportCollection = ctnAmend;

                                    var objCharge = new List<BEXPORT_DOCUMETARYCOLLECTIONCHARGES>();
                                    LoadDataForAmend(ctnAmend, objCharge);
                                }
                                RadToolBar1.FindItemByValue("btSave").Enabled = true;
                                txtCode.Text = code + "." + maxAmendId;
                                //
                            }
                        }
                    }
                    else
                    {
                        var lstDoc = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.AmendNo == Code).FirstOrDefault();
                        if (lstDoc != null)
                        {
                            var objCharge = _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Where(x => x.DocCollectCode == lstDoc.DocCollectCode && x.AmendNo == Code).ToList();
                            LoadDataForAmend(lstDoc, objCharge);
                            txtCode.Text = lstDoc.AmendNo;
                            _exportCollection = new BEXPORT_DOCUMETARYCOLLECTION();
                            _exportCollection = lstDoc;
                        }
                        else
                        {
                            lblError.Text = "This Amend ID was not found";
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEX)
            {
                Exception raise = dbEX;
                foreach (var validationError in dbEX.EntityValidationErrors)
                {
                    string message = string.Format("{0}:{1}", validationError.Entry.Entity.ToString(), validationError.ValidationErrors);
                    raise = new InvalidOperationException(message, raise);
                }
                lblError.Text = raise.Message;
                throw raise;
            }
        }
        //
        
        private void LoadExportDoc()
        {
            var dsDoc = bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetByDocCollectCode(CodeId,TabId);
            if (dsDoc == null || dsDoc.Tables.Count <= 0 || dsDoc.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            _exportDoc = dsDoc.Tables[0].Rows[0];
            txtCode.Text = CodeId;
            LoadData(dsDoc);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Disable)
            {
                SetDisableByReview(false);
            }
            InitDefaultData();
            //LoadExportDoc();

            switch (ScreenType)
            {
                case ExportDocumentaryScreenType.RegisterCc:
                case ExportDocumentaryScreenType.Register:
                    LoadExportDoc();
                    if (Enqiry)
                    {
                        SetButtonForEnquiry();
                    }
                    else
                    {
                        InitToolBarForRegister();
                    }
                    lblAmount_New.Visible = false;
                    divAmount.Visible = false;
                    newAmountLb.Visible = false;
                    amountLb.Visible = true;
                    break;
                case ExportDocumentaryScreenType.Amend:
                    LoadExportDocAmend();
                    if (Enqiry)
                    {
                        SetButtonForEnquiry();
                    }
                    else
                    {
                        InitToolBarForAmend();
                    }
                    //tabCharges.Visible = false;
                    //Charges.Visible = false;
                    break;
                case ExportDocumentaryScreenType.Cancel:
                    LoadExportDoc();
                    if (Enqiry)
                    {
                        SetButtonForEnquiry();
                    }
                    else
                    {
                        InitToolBarForCancel();
                    }
                    lblAmount_New.Visible = false;
                    divDocumentaryCollectionCancel.Visible = true;
                    break;
                case ExportDocumentaryScreenType.Acception:
                    LoadExportDoc();
                    if (Enqiry)
                    {
                        SetButtonForEnquiry();
                    }
                    else
                    {
                        InitToolBarForAccept();
                    }
                    lblAmount_New.Visible = false;
                    divOutgoingCollectionAcception.Visible = true;
                    break;
            }
            #region Old Code
            /*
            dteDocsReceivedDate.SelectedDate = DateTime.Now;
            divDocsCode.Visible = false;

            RadToolBar1.FindItemByValue("btSearch").Enabled = false;

            GenerateVATNo();
            GenerateFTCode();

            InitToolBar(false);

            //tab charge
            divCharge2.Visible = false;
            divChargeInfo2.Visible = false;
            //tab charge

            Cal_TracerDate(false);

            // new value for Amount/Tenor/Tracer date
            divAmount.Visible = false;
            divTenor.Visible = false;
            divTracerDate.Visible = false;
            // new


            var dsSwiftCode = SQLData.B_BSWIFTCODE_GetAll();
            if (TabId == 227)
            {
                comboCollectionType.Items.Clear();
                comboCollectionType.DataValueField = "Id";
                comboCollectionType.DataTextField = "Id";
                comboCollectionType.DataSource =
                    SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
                comboCollectionType.DataBind();
                comboCollectionType.Enabled = false;
            }
            else
            {
                comboCollectionType.Items.Clear();
                comboCollectionType.Items.Add(new RadComboBoxItem(""));
                comboCollectionType.DataValueField = "Id";
                comboCollectionType.DataTextField = "Id";
                comboCollectionType.DataSource =
                    SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_CollectionType");
                comboCollectionType.DataBind();
            }
            comboDraweeCusNo.Items.Clear();
            comboDraweeCusNo.Items.Add(new RadComboBoxItem(""));
            comboDraweeCusNo.DataValueField = "CustomerID";
            comboDraweeCusNo.DataTextField = "CustomerID";
            comboDraweeCusNo.DataSource = DataTam.B_BCUSTOMERS_GetAll();
            comboDraweeCusNo.DataBind();

            comboNostroCusNo.Items.Clear();
            comboNostroCusNo.Items.Add(new RadComboBoxItem(""));
            comboNostroCusNo.DataValueField = "Code";
            comboNostroCusNo.DataTextField = "Code";
            comboNostroCusNo.DataSource = dsSwiftCode;
            comboNostroCusNo.DataBind();

            comboCollectingBankNo.Items.Clear();
            comboCollectingBankNo.Items.Add(new RadComboBoxItem(""));
            comboCollectingBankNo.DataValueField = "Code";
            comboCollectingBankNo.DataTextField = "Code";
            comboCollectingBankNo.DataSource = dsSwiftCode;
            comboCollectingBankNo.DataBind();

            comboCommodity.Items.Clear();
            comboCommodity.Items.Add(new RadComboBoxItem(""));
            comboCommodity.DataValueField = "ID";
            comboCommodity.DataTextField = "ID";
            comboCommodity.DataSource = DataTam.B_BCOMMODITY_GetAll();
            comboCommodity.DataBind();

            comboDocsCode1.Items.Clear();
            comboDocsCode1.Items.Add(new RadComboBoxItem(""));
            comboDocsCode1.DataValueField = "Id";
            comboDocsCode1.DataTextField = "Description";
            comboDocsCode1.DataSource = SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode1.DataBind();

            comboDocsCode2.Items.Clear();
            comboDocsCode2.Items.Add(new RadComboBoxItem(""));
            comboDocsCode2.DataValueField = "Id";
            comboDocsCode2.DataTextField = "Description";
            comboDocsCode2.DataSource = SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode2.DataBind();

            LoadDataSourceComboPartyCharged();
            LoadDataSourceComboChargeCcy();
            

            if (TabId == 218)
            {
                // Outgoing Collection Amendment => tabid=229
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData();
            }
            else if (TabId == 219)
            {
                // Documentary Collection Cancel => tabid=230
                txtCode.Text = Request.QueryString["CodeID"];
                txtCode.Focus();

                //divDocumentaryCollectionCancel.Visible = true;
                //dteCancelDate.SelectedDate = DateTime.Now;
                //dteContingentExpiryDate.SelectedDate = DateTime.Now;

                LoadData();

                SetDisableByReview(false);

                //txtCancelRemark.Enabled = true;
                //txtRemittingBankRef.Enabled = true;
                //dteCancelDate.Enabled = true;
               // dteContingentExpiryDate.Enabled = true;
                txtCode.Enabled = true;
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
            {
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["CodeID"]))
            {
                txtCode.Text = Request.QueryString["CodeID"];
                LoadData();
            }

            if (!string.IsNullOrEmpty(Request.QueryString["disable"]))
            {
                InitToolBar(true);
                SetDisableByReview(false);
                RadToolBar1.FindItemByValue("btSave").Enabled = false;
            }
            Session["DataKey"] = txtCode.Text;
             * */
            #endregion
        }
        protected void SetButtonForEnquiry()
        {
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                if (StatusEnquiry == "UNA")
                {
                    RadToolBar1.FindItemByValue("btRevert").Enabled = true;
                    
                }
                else if (StatusEnquiry == "REV")
                {
                    RadToolBar1.FindItemByValue("btSave").Enabled = true;
                    RadToolBar1.FindItemByValue("btRevert").Enabled = false;
                }
                else
                {
                    RadToolBar1.FindItemByValue("btRevert").Enabled = false;
                }
            
        }
        protected float ConvertStringToFloat(string num)
        {
            try
            {
                return float.Parse(num);
            }
            catch 
            {
                return 0;
            }
        }
        
            
        
        protected void InitDefaultData()
        {
            foreach (RadToolBarItem item in RadToolBar1.Items)
            {
                item.Enabled = false;
            } 

            LoadDataSourceComboPartyCharged();
            LoadChargeCode();
            GenerateVATNo();

            dteDocsReceivedDate.SelectedDate = DateTime.Now;
            dteTracerDate.SelectedDate = DateTime.Now.AddDays(30);

            divDocsCode2.Visible = false;
            divDocsCode3.Visible = false;

            // bind value collection type
            if (ScreenType == ExportDocumentaryScreenType.RegisterCc)
            {
                comboCollectionType.Items.Clear();
                comboCollectionType.DataValueField = "ID";
                comboCollectionType.DataTextField = "ID";
                comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
                comboCollectionType.DataBind();
                divCollectionType.Visible = false;
                divDocsCode.Visible = false;
                divDocsCode2.Visible = false;
                divDocsCode3.Visible = false;
            }
            else
            {
                comboCollectionType.Items.Clear();
                comboCollectionType.DataValueField = "ID";
                comboCollectionType.DataTextField = "ID";
                comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_CollectionType");
                comboCollectionType.DataBind();
                
            }
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

            // bind drawer
            DataView dv = new DataView(bd.DataTam.B_BCUSTOMERS_GetAll().Tables[0]);

            dv.RowFilter = "CustomerID like '2%'"; 
            comboDrawerCusNo.Items.Clear();
            comboDrawerCusNo.Items.Add(new RadComboBoxItem(""));
            comboDrawerCusNo.DataValueField = "CustomerID";
            comboDrawerCusNo.DataTextField = "CustomerID";
            comboDrawerCusNo.DataSource = dv;
            comboDrawerCusNo.DataBind();

            // bind collecting bank no
            var dsSwiftCode = bd.SQLData.B_BBANKSWIFTCODE_GETALL();
            //comboCollectingBankNo.Items.Clear();
            //comboCollectingBankNo.Items.Add(new RadComboBoxItem(""));
            //comboCollectingBankNo.DataValueField = "SwiftCode";
            //comboCollectingBankNo.DataTextField = "BankName";
            //comboCollectingBankNo.DataSource = dsSwiftCode;
            //comboCollectingBankNo.DataBind();

            // bind nostro cus no
            //comboNostroCusNo.Items.Clear();
            //comboNostroCusNo.Items.Add(new RadComboBoxItem(""));
            //comboNostroCusNo.DataValueField = "SwiftCode";
            //comboNostroCusNo.DataTextField = "BankName";
            //comboNostroCusNo.DataSource = dsSwiftCode;
            //comboNostroCusNo.DataBind();

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

            comboDocsCode3.Items.Clear();
            comboDocsCode3.Items.Add(new RadComboBoxItem(""));
            comboDocsCode3.DataValueField = "Id";
            comboDocsCode3.DataTextField = "Description";
            comboDocsCode3.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode3.DataBind();

            comboCommodity.Items.Clear();
            comboCommodity.Items.Add(new RadComboBoxItem(""));
            comboCommodity.DataValueField = "ID";
            comboCommodity.DataTextField = "Name2";
            //comboCommodity.DataSource = bd.DataTam.B_BCOMMODITY_GetAll();
            comboCommodity.DataSource = bd.SQLData.B_BCOMMODITY_GetAllByTransactionType("OTC");
            comboCommodity.DataBind();

            //
            comboAccountOfficer.Items.Clear();
            comboAccountOfficer.Items.Add(new RadComboBoxItem(""));
            comboAccountOfficer.DataTextField = "Description";
            comboAccountOfficer.DataValueField = "Code";
            comboAccountOfficer.DataSource = bd.SQLData.B_BACCOUNTOFFICER_GetAll();
            comboAccountOfficer.DataBind();
            //
            var tblList = bd.SQLData.B_BCURRENCY_GetAll().Tables[0];
            bc.Commont.initRadComboBox(ref comboCurrency, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy2, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy3, "Code", "Code", tblList);
            bc.Commont.initRadComboBox(ref rcbChargeCcy4, "Code", "Code", tblList);

            //remove "GOLD" from currency List
            bc.Commont.removeCurrencyItem(comboCurrency, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy2, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy3, "GOLD");
            bc.Commont.removeCurrencyItem(rcbChargeCcy4, "GOLD");

            if (TabId == 229)
            {
                tbChargeCode.SelectedValue = "EC.AMEND";
                tbChargeCode.Enabled = false;
                tbChargeCode2.SelectedValue = "EC.CABLE";
                tbChargeCode2.Enabled = false;
                tbChargeCode3.SelectedValue = "EC.COURIER";
                tbChargeCode3.Enabled = false;
                tbChargeCode4.SelectedValue = "EC.OTHER";
                tbChargeCode4.Enabled = false;
            }
            else if (TabId == 226)
            {
                tbChargeCode.SelectedValue = "EC.RECEIVE";
                tbChargeCode.Enabled = false;
                tbChargeCode2.SelectedValue = "EC.COURIER";
                tbChargeCode2.Enabled = false;
                tbChargeCode3.SelectedValue = "EC.OTHER";
                tbChargeCode3.Enabled = false;
            }
            else if (TabId == 230)
            {
                tbChargeCode.SelectedValue = "EC.CABLE";
                tbChargeCode.Enabled = false;
                tbChargeCode2.SelectedValue = "EC.CANCEL";
                tbChargeCode2.Enabled = false;
                tbChargeCode3.SelectedValue = "EC.OTHER";
                tbChargeCode3.Enabled = false;
                tbChargeCode4.SelectedValue = "";
                tbChargeCode4.Enabled = false;
            }
            else if (TabId == 377)
            {
                tbChargeCode.SelectedValue = "EC.ACCEPT";
                tbChargeCode.Enabled = false;
                tbChargeCode2.SelectedValue = "EC.CABLE";
                tbChargeCode2.Enabled = false;
                tbChargeCode3.SelectedValue = "EC.OTHER";
                tbChargeCode3.Enabled = false;
                tbChargeCode4.SelectedValue = "EC.RECEIVE";
                tbChargeCode4.Enabled = false;
            }
            else
            {
                tbChargeCode.SelectedValue = "EC.RECEIVE";
                tbChargeCode.Enabled = false;
                tbChargeCode2.SelectedValue = "EC.COURIER";
                tbChargeCode2.Enabled = false;
                tbChargeCode3.SelectedValue = "EC.OTHER";
                tbChargeCode3.Enabled = false;
                tbChargeCode4.SelectedValue = "";
                tbChargeCode4.Enabled = false;
            }
        }

        private void SetVisibilityByStatus(DataSet dsDoc)
        {
            DataRow drow = dsDoc.Tables[0].Rows[0];
            lblError.Text = "";
            var errorUn_AUT = "This Incoming Documentary Collection has Not Authorized yet. Do not allow to process Payment Acceptance!";
            switch (TabId)
            {
                case 226: // Register
                    if (Request.QueryString["key"] == null)
                    {
                        if (drow["Status"].ToString() == "AUT" && drow["PaymentFullFlag"].ToString() == "1")
                        {
                            lblError.Text = "This Documentary has payment full";
                            //InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;

                        }
                        else if (drow["Cancel_Status"].ToString() == "AUT")
                        {
                            lblError.Text = "This Documentary was canceled";
                            //InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        }
                        else if (drow["Status"].ToString() == "AUT")
                        {
                            lblError.Text = "This Documentary was authorized";
                            //InitToolBar(false);
                            SetDisableByReview(false);
                            RadToolBar1.FindItemByValue("btSave").Enabled = false;
                        }
                    }
                    break;
            }
        }
        protected void LoadChargeCode()
        {
            var datasource = bd.SQLData.B_BCHARGECODE_GetByViewType(226);
            if (datasource != null)
            {
                if (TabId == 229)
                {
                    datasource.Rows.Add("EC.RECEIVE", "");
                    datasource.Rows.Add("EC.AMEND", "");
                }
                else if (TabId == 377)
                {
                    datasource.Rows.Add("EC.ACCEPT", "");
                    datasource.Rows.Add("EC.RECEIVE", "");
                }
                else if (TabId == 230)
                {
                    datasource.Rows.Add("EC.CANCEL", "");
                }
                datasource.Rows.Add("", "");
            }
            tbChargeCode.Items.Clear();
            tbChargeCode.Items.Add(new RadComboBoxItem(""));
            tbChargeCode.DataValueField = "Code";
            tbChargeCode.DataTextField = "Code";
            tbChargeCode.DataSource = datasource;
            tbChargeCode.DataBind();

            tbChargeCode2.Items.Clear();
            tbChargeCode2.Items.Add(new RadComboBoxItem(""));
            tbChargeCode2.DataValueField = "Code";
            tbChargeCode2.DataTextField = "Code";
            tbChargeCode2.DataSource = datasource;
            tbChargeCode2.DataBind();

            tbChargeCode3.Items.Clear();
            tbChargeCode3.Items.Add(new RadComboBoxItem(""));
            tbChargeCode3.DataValueField = "Code";
            tbChargeCode3.DataTextField = "Code";
            tbChargeCode3.DataSource = datasource;
            tbChargeCode3.DataBind();

            tbChargeCode4.Items.Clear();
            tbChargeCode4.Items.Add(new RadComboBoxItem(""));
            tbChargeCode4.DataValueField = "Code";
            tbChargeCode4.DataTextField = "Code";
            tbChargeCode4.DataSource = datasource;
            tbChargeCode4.DataBind();
        }
        protected void SetEnableForReview()
        {
            comboWaiveCharges.Enabled = true;
            tbChargeRemarks.Enabled = true;
            //tbVatNo.Enabled = true;


            rcbChargeCcy.Enabled = true;
            rcbChargeAcct.Enabled = true;
            tbChargeAmt.Enabled = true;
            rcbPartyCharged.Enabled = true;
            rcbOmortCharge.Enabled = true;
            rcbChargeStatus.Enabled = true;
            lblTaxCode.Enabled = true;
            lblTaxAmt.Enabled = true;

            rcbChargeCcy2.Enabled = true;
            rcbChargeAcct2.Enabled = true;
            tbChargeAmt2.Enabled = true;
            rcbPartyCharged2.Enabled = true;
            rcbOmortCharge2.Enabled = true;
            rcbChargeStatus2.Enabled = true;
            lblTaxCode2.Enabled = true;
            lblTaxAmt2.Enabled = true;

            rcbChargeCcy3.Enabled = true;
            rcbChargeAcct3.Enabled = true;
            tbChargeAmt3.Enabled = true;
            rcbPartyCharged3.Enabled = true;
            rcbOmortCharge3.Enabled = true;
            rcbChargeStatus3.Enabled = true;
            lblTaxCode3.Enabled = true;
            lblTaxAmt3.Enabled = true;

            rcbChargeCcy4.Enabled = true;
            rcbChargeAcct4.Enabled = true;
            tbChargeAmt4.Enabled = true;
            rcbPartyCharged4.Enabled = true;
            rcbOmortCharge4.Enabled = true;
            rcbChargeStatus4.Enabled = true;
            lblTaxCode4.Enabled = true;
            lblTaxAmt4.Enabled = true;
        }
        protected void SetDisableByReview(bool flag)
        {
            BankProject.Controls.Commont.SetTatusFormControls(this.Controls, flag);
            if (Request.QueryString["disable"] != null)
                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
            else
                RadToolBar1.FindItemByValue("btPrint").Enabled = false;
        }

        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            switch (commandName)
            {
                case "save":
                    if (TabId == 229)
                    {
                        SaveDataAmend();
                    }
                    else
                    {
                        SaveData();
                    }
                    Response.Redirect(Globals.NavigateURL(TabId));
                    /*
                    // reset form
                    GenerateFTCode();

                    LoadData();

                    
                    dteDocsReceivedDate.SelectedDate = DateTime.Now;
                    divDocsCode2.Visible = false;
                    divDocsCode3.Visible = false;

                    txtTenor.Text = "AT SIGHT";

                    divAmount.Visible = false;
                    divTenor.Visible = false;
                    divTracerDate.Visible = false;

                    Cal_TracerDate(false);

                    GenerateVATNo();
                    Session["DataKey"] = txtCode.Text;
                     * */
                    break;
                    
                case "review":
                    Response.Redirect(EditUrl("preview_exportdoc"));
                    break;

                case "authorize":
                    Authorize();
                    break;

                case "revert":
                    Revert();
                    break;

                case "print":
                    //Aspose.Words.License license = new Aspose.Words.License();
                    //license.SetLicense("Aspose.Words.lic");

                    ////Open template
                    //string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/RegisterDocumentaryCollection.doc");
                    ////Open the template document
                    //Aspose.Words.Document doc = new Aspose.Words.Document(path);
                    ////Execute the mail merge.
                    //DataSet ds = new DataSet();
                    //ds = SQLData.B_BDOCUMETARYCOLLECTION_Report(txtCode.Text);

                    //// Fill the fields in the document with user data.
                    //doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
                    //// Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
                    //doc.Save("RegisterDocumentaryCollection_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
                    break;
            }
        }

        
        protected void InitToolBarForAmend()
        {
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            if (_exportCollection != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportCollection.Status != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportCollection.Amend_Status == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                    }
                    else if (_exportCollection.Cancel_Status == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was canceled";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btRevert").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else // Editing
                {
                    if (_exportCollection.Status != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                        SetDisableByReview(false);
                    }
                    else if (_exportCollection.Cancel_Status == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was canceled";
                    }
                    else if (_exportCollection.Amend_Status == "AUT" && _exportCollection.AmendNo == txtCode.Text) // Authorized
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                        SetDisableByReview(false);
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btSave").Enabled = true;
                        SetEnableForReview();
                        SetEnableForReview();
                    }

                }
            }
            else
            {

            }
        }
        protected void InitToolBarForRegister()
        {
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            if (_exportDoc != null)
            {
                if (!String.IsNullOrEmpty(_exportDoc["DocsType"].ToString()))
                {
                    if (_exportDoc["DocsType"].ToString() == TabId.ToString())
                    {
                        if (Disable) // Authorizing
                        {
                            if (_exportDoc["Status"].ToString() == "AUT") // Authorized
                            {
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                lblError.Text = "This Documentary was authorized";
                            }
                            else // Not yet authorize
                            {
                                RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                                RadToolBar1.FindItemByValue("btRevert").Enabled = true;
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                            }
                            SetDisableByReview(false);
                        }
                        else // Editing
                        {
                            if (_exportDoc["Status"].ToString() == "AUT") // Authorized
                            {
                                RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                                lblError.Text = "This Documentary was authorized";
                                SetDisableByReview(false);
                            }
                            else // Not yet authorize
                            {
                                RadToolBar1.FindItemByValue("btSave").Enabled = true;
                                SetEnableForReview();
                            }
                        }
                    }
                    else
                    {
                          Response.Redirect("Default.aspx?tabid=" + TabId.ToString());
                    }
                }
            }
            else // Creating
            {
                RadToolBar1.FindItemByValue("btSave").Enabled = true;
                txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("EXPORT_DOCUMETARYCOLLECTION", Refix_BMACODE());
            }
        }
        protected void InitToolBarForAccept()
        {
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (!string.IsNullOrEmpty(_exportDoc["Amend_Status"].ToString()) &&  _exportDoc["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc["AcceptStatus"].ToString() == "AUT")
                    {
                        lblError.Text = "This Acception Documentary was authorized";
                    }
                    //else if (!string.IsNullOrEmpty(_exportDoc["AcceptStatus"].ToString()) && _exportDoc["AcceptStatus"].ToString() != "AUT")
                    //{
                    //    lblError.Text = "This Acception Documentary was not authorized";
                    //}
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Cancel Documentary was authorized";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btRevert").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                }
                else // Editing
                {
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (!string.IsNullOrEmpty(_exportDoc["Amend_Status"].ToString()) && _exportDoc["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc["AcceptStatus"].ToString() == "AUT")
                    {
                        lblError.Text = "This Acception Documentary was authorized";
                    }
                    //else if (!string.IsNullOrEmpty(_exportDoc["AcceptStatus"].ToString()) && _exportDoc["AcceptStatus"].ToString() != "AUT")
                    //{
                    //    lblError.Text = "This Acception Documentary was not authorized";
                    //}
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Cancel Documentary was authorized";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btSave").Enabled = true;
                    }
                    SetDisableByReview(false);
                    
                    if (_exportDoc["AcceptStatus"].ToString() != "AUT")
                    {
                        dtAcceptDate.Enabled = true;
                        txtAcceptREmark.Enabled = true;
                        SetEnableForReview();
                    }
                }

            }
            else
            {

            }
        }
        protected void InitToolBarForCancel()
        {
            RadToolBar1.FindItemByValue("btReview").Enabled = true;
            if (_exportDoc != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (!string.IsNullOrEmpty(_exportDoc["Amend_Status"].ToString()) && _exportDoc["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Cancel Documentary was authorized";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btAuthorize").Enabled = true;
                        RadToolBar1.FindItemByValue("btRevert").Enabled = true;
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                    }
                    SetDisableByReview(false);
                   
                }
                else // Editing
                {
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (!string.IsNullOrEmpty(_exportDoc["Amend_Status"].ToString()) && _exportDoc["Amend_Status"].ToString() != "AUT")
                    {
                        lblError.Text = "This Amend Documentary was not authorized";
                    }
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Cancel Documentary was authorized";
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btSave").Enabled = true;
                    }
                    SetDisableByReview(false);
                    if (_exportDoc["Cancel_Status"].ToString() != "AUT")
                    {
                        dteCancelDate.Enabled = true;
                        dteContingentExpiryDate.Enabled = true;
                        txtCancelRemark.Enabled = true;
                        SetEnableForReview();
                    }
                }

            }
            else
            {

            }
        }
        protected void SaveDataAmend()
        {
            try
            {
                int AmendPreId = 0;
                string AmendNo = "";
                var Code = txtCode.Text.Trim();
                var objPreAmend = new BEXPORT_DOCUMETARYCOLLECTION();
                if (!String.IsNullOrEmpty(Code))
                {
                    if (Code.IndexOf('.') != -1)
                    {

                        var findTypeAmend = Code.Split('.');
                        if (findTypeAmend != null && findTypeAmend.Length > 0)
                        {
                            if (findTypeAmend.Length == 2)
                            {
                                var chkAmend = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.AmendNo == Code).FirstOrDefault();
                                //truong hop chua co
                                if (chkAmend == null)
                                {
                                    if (!String.IsNullOrEmpty(findTypeAmend[1]))
                                    {
                                        AmendPreId = int.Parse(findTypeAmend[1]) - 1;
                                    }
                                    if (AmendPreId > 0)
                                    {
                                        ///xet Amend truoc do
                                        ///
                                        AmendNo = findTypeAmend[0] + "." + AmendPreId;

                                        objPreAmend = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.AmendNo == AmendNo).FirstOrDefault();
                                        if (objPreAmend != null)
                                        {
                                            objPreAmend.ActiveRecordFlag = YesNo.NO;
                                        }
                                    }
                                    else
                                    {
                                        //nguoc lai update Status cho Amend goc
                                        AmendNo = findTypeAmend[0];
                                        objPreAmend = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.DocCollectCode == AmendNo).FirstOrDefault();
                                        if (objPreAmend != null)
                                        {
                                            objPreAmend.ActiveRecordFlag = YesNo.NO;
                                        }
                                    }
                                    _entities.SaveChanges();
                                    //them moi dong hien tai
                                    BEXPORT_DOCUMETARYCOLLECTION objInsert = new BEXPORT_DOCUMETARYCOLLECTION
                                    {
                                        DocCollectCode = findTypeAmend[0],
                                        DrawerCusNo = comboDrawerCusNo.SelectedValue,
                                        DrawerCusName = txtDrawerCusName.Text.Trim(),
                                        DrawerAddr1 = txtDrawerAddr1.Text.Trim(),
                                        DrawerAddr2 = txtDrawerAddr2.Text.Trim(),
                                        DrawerAddr3 = txtDrawerAddr3.Text.Trim(),
                                        DrawerRefNo = txtDrawerRefNo.Text.Trim(),
                                        CollectingBankNo = comboCollectingBankNo.Text,
                                        CollectingBankName = txtCollectingBankName.Text.Trim(),
                                        CollectingBankAddr1 = txtCollectingBankAddr1.Text.Trim(),
                                        CollectingBankAddr2 = txtCollectingBankAddr2.Text.Trim(),
                                        CollectingBankAddr3 = txtCollectingBankAddr3.Text.Trim(),
                                        CollectingBankAcct = comboCollectingBankAcct.SelectedValue,
                                        DraweeCusNo = txtDraweeCusNo.Text.Trim(),
                                        DraweeCusName = txtDraweeCusName.Text.Trim(),
                                        DraweeAddr1 = txtDraweeAddr1.Text.Trim(),
                                        DraweeAddr2 = txtDraweeAddr2.Text.Trim(),
                                        DraweeAddr3 = txtDraweeAddr3.Text.Trim(),
                                        DraweeAddr4 = txtDraweeAddr4.Text.Trim(),
                                        NostroCusNo = comboNostroCusNo.Text,
                                        Currency = comboCurrency.SelectedValue,
                                        Amount = numAmount.Value,
                                        DocsReceivedDate = dteDocsReceivedDate.SelectedDate,
                                        MaturityDate = dteMaturityDate.SelectedDate,
                                        Tenor = txtTenor.Text.Trim(),
                                        TracerDate = dteTracerDate.SelectedDate,
                                        Commodity = comboCommodity.SelectedValue,
                                        DocsCode1 = comboDocsCode1.SelectedValue,
                                        DocsCode2 = comboDocsCode2.SelectedValue,
                                        DocsCode3 = comboDocsCode3.SelectedValue,
                                        OtherDocs = txtOtherDocs.Text.Trim(),
                                        Remarks = txtRemarks.Text.Trim(),
                                        Remarks1 = txtRemarks1.Text.Trim(),
                                        Remarks2 = txtRemarks2.Text.Trim(),
                                        Remarks3 = txtRemarks3.Text.Trim(),
                                        CreateDate = DateTime.Now,
                                        CreateBy = UserId.ToString(),
                                        CollectionType = comboCollectionType.SelectedValue,
                                        CancelDate = dteCancelDate.SelectedDate,
                                        ContingentExpiryDate = dteContingentExpiryDate.SelectedDate,
                                        CancelRemark = txtCancelRemark.Text.Trim(),
                                        Accountofficer = comboAccountOfficer.SelectedValue,
                                        AcceptedDate = dtAcceptDate.SelectedDate,
                                        AcceptedRemarks = txtAcceptREmark.Text.Trim(),
                                        //Amend
                                        Amend_Status = "UNA",
                                        AmendBy = UserId.ToString(),
                                        AmendNo = txtCode.Text,
                                        RefAmendNo = findTypeAmend[0],
                                        ActiveRecordFlag=YesNo.YES,
                                        Status="AUT"
                                        
                                        //
                                    };
                                    if (objPreAmend != null)
                                    {

                                        objInsert.OldAmount = objPreAmend.Amount;
                                        objInsert.OldDocsReceivedDate = objPreAmend.OldDocsReceivedDate;
                                    }
                                    if (!String.IsNullOrEmpty(findTypeAmend[1]))
                                    {
                                        objInsert.AmendId = int.Parse(findTypeAmend[1]);
                                    }
                                    if (!String.IsNullOrEmpty(numReminderDays.Value.ToString()))
                                    {
                                        objInsert.ReminderDays = long.Parse(numReminderDays.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals1.Value.ToString()))
                                    {
                                        objInsert.NoOfOriginals1 = int.Parse(numNoOfOriginals1.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies1.Value.ToString()))
                                    {
                                        objInsert.NoOfCopies1 = int.Parse(numNoOfCopies1.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals2.Value.ToString()))
                                    {
                                        objInsert.NoOfOriginals2 = int.Parse(numNoOfOriginals2.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies2.Value.ToString()))
                                    {
                                        objInsert.NoOfCopies2 = int.Parse(numNoOfCopies2.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals3.Value.ToString()))
                                    {
                                        objInsert.NoOfOriginals3 = int.Parse(numNoOfOriginals3.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies3.Value.ToString()))
                                    {
                                        objInsert.NoOfCopies3 = int.Parse(numNoOfCopies3.Value.ToString());
                                    }
                                    _entities.BEXPORT_DOCUMETARYCOLLECTION.Add(objInsert);
                                    _entities.SaveChanges();
                                }
                                else
                                {
                                    chkAmend.DocCollectCode = findTypeAmend[0];
                                    chkAmend.DrawerCusNo = comboDrawerCusNo.SelectedValue;
                                    chkAmend.DrawerCusName = txtDrawerCusName.Text.Trim();
                                    chkAmend.DrawerAddr1 = txtDrawerAddr1.Text.Trim();
                                    chkAmend.DrawerAddr2 = txtDrawerAddr2.Text.Trim();
                                    chkAmend.DrawerAddr3 = txtDrawerAddr3.Text.Trim();
                                    chkAmend.DrawerRefNo = txtDrawerRefNo.Text.Trim();
                                    chkAmend.CollectingBankNo = comboCollectingBankNo.Text;
                                    chkAmend.CollectingBankName = txtCollectingBankName.Text.Trim();
                                    chkAmend.CollectingBankAddr1 = txtCollectingBankAddr1.Text.Trim();
                                    chkAmend.CollectingBankAddr2 = txtCollectingBankAddr2.Text.Trim();
                                    chkAmend.CollectingBankAddr3 = txtCollectingBankAddr3.Text.Trim();
                                    chkAmend.CollectingBankAcct = comboCollectingBankAcct.SelectedValue;
                                    chkAmend.DraweeCusNo = txtDraweeCusNo.Text.Trim();
                                    chkAmend.DraweeCusName = txtDraweeCusName.Text.Trim();
                                    chkAmend.DraweeAddr1 = txtDraweeAddr1.Text.Trim();
                                    chkAmend.DraweeAddr2 = txtDraweeAddr2.Text.Trim();
                                    chkAmend.DraweeAddr3 = txtDraweeAddr3.Text.Trim();
                                    chkAmend.DraweeAddr4 = txtDraweeAddr4.Text.Trim();
                                    chkAmend.NostroCusNo = comboNostroCusNo.Text;
                                    chkAmend.Currency = comboCurrency.SelectedValue;
                                    chkAmend.Amount = numAmount.Value;
                                    chkAmend.DocsReceivedDate = dteDocsReceivedDate.SelectedDate;
                                    chkAmend.MaturityDate = dteMaturityDate.SelectedDate;
                                    chkAmend.Tenor = txtTenor.Text.Trim();
                                    chkAmend.TracerDate = dteTracerDate.SelectedDate;
                                    chkAmend.Commodity = comboCommodity.SelectedValue;
                                    chkAmend.DocsCode1 = comboDocsCode1.SelectedValue;
                                    chkAmend.DocsCode2 = comboDocsCode2.SelectedValue;
                                    chkAmend.DocsCode3 = comboDocsCode3.SelectedValue;
                                    chkAmend.OtherDocs = txtOtherDocs.Text.Trim();
                                    chkAmend.Remarks = txtRemarks.Text.Trim();
                                    chkAmend.Remarks1 = txtRemarks1.Text.Trim();
                                    chkAmend.Remarks2 = txtRemarks2.Text.Trim();
                                    chkAmend.Remarks3 = txtRemarks3.Text.Trim();
                                    chkAmend.UpdatedBy = UserId.ToString();
                                    chkAmend.UpdatedDate = DateTime.Now;
                                    chkAmend.CollectionType = comboCollectionType.SelectedValue;
                                    chkAmend.CancelDate = dteCancelDate.SelectedDate;
                                    chkAmend.ContingentExpiryDate = dteContingentExpiryDate.SelectedDate;
                                    chkAmend.CancelRemark = txtCancelRemark.Text.Trim();
                                    chkAmend.Accountofficer = comboAccountOfficer.SelectedValue;
                                    chkAmend.AcceptedDate = dtAcceptDate.SelectedDate;
                                    chkAmend.AcceptedRemarks = txtAcceptREmark.Text.Trim();
                                    //Amend
                                    chkAmend.Amend_Status = "UNA";
                                    chkAmend.AmendBy = UserId.ToString();
                                    chkAmend.AmendNo = txtCode.Text;
                                    chkAmend.RefAmendNo = findTypeAmend[0];
                                    chkAmend.Status = "AUT";
                                    if (!String.IsNullOrEmpty(findTypeAmend[1]))
                                    {
                                        chkAmend.AmendId = int.Parse(findTypeAmend[1]);
                                    }
                                    if (!String.IsNullOrEmpty(numReminderDays.Value.ToString()))
                                    {
                                        chkAmend.ReminderDays = long.Parse(numReminderDays.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals1.Value.ToString()))
                                    {
                                        chkAmend.NoOfOriginals1 = int.Parse(numNoOfOriginals1.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies1.Value.ToString()))
                                    {
                                        chkAmend.NoOfCopies1 = int.Parse(numNoOfCopies1.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals2.Value.ToString()))
                                    {
                                        chkAmend.NoOfOriginals2 = int.Parse(numNoOfOriginals2.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies2.Value.ToString()))
                                    {
                                        chkAmend.NoOfCopies2 = int.Parse(numNoOfCopies2.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfOriginals3.Value.ToString()))
                                    {
                                        chkAmend.NoOfOriginals3 = int.Parse(numNoOfOriginals3.Value.ToString());
                                    }
                                    if (!String.IsNullOrEmpty(numNoOfCopies3.Value.ToString()))
                                    {
                                        chkAmend.NoOfCopies3 = int.Parse(numNoOfCopies3.Value.ToString());
                                    }
                                    _entities.SaveChanges();
                                }
                                //save phan Charge
                                var name = findTypeAmend[0];
                                var lstCharge = _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Where(x => x.DocCollectCode == name && x.TabId == TabId).ToList();
                                //truong hop da co -->update
                                if (lstCharge != null && lstCharge.Count > 0)
                                {
                                    String[] chargeCode = { "EC.AMEND", "EC.CABLE", "EC.COURIER", "EC.OTHER" };
                                    var listChargeCode = chargeCode.ToList();
                                    foreach (var item in lstCharge)
                                    {
                                        if (item.Chargecode == "EC.AMEND")
                                        {
                                            listChargeCode.Remove(item.Chargecode);
                                            item.DocCollectCode = findTypeAmend[0];
                                            item.WaiveCharges = comboWaiveCharges.SelectedValue;
                                            item.Chargecode = tbChargeCode.SelectedValue;
                                            item.ChargeAcct = rcbChargeAcct.SelectedValue;
                                            item.ChargeCcy = rcbChargeCcy.SelectedValue;
                                            if (!String.IsNullOrEmpty(tbChargeAmt.Value.ToString()))
                                            {
                                                item.ChargeAmt = decimal.Parse(tbChargeAmt.Value.ToString());
                                            }
                                            item.PartyCharged = rcbPartyCharged.SelectedValue;
                                            item.OmortCharges = rcbOmortCharge.SelectedValue;
                                            item.ChargeStatus = rcbChargeStatus.SelectedValue;
                                            item.ChargeRemarks = tbChargeRemarks.Text.Trim();
                                            item.VATNo = tbVatNo.Text;
                                            item.TaxCode = lblTaxCode.Text;
                                            item.TaxAmt = lblTaxAmt.Text;
                                            item.Rowchages = "1";
                                            item.TabId = TabId;
                                            item.AmendNo = Code;
                                            if(!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                                            {
                                                item.AmendId = int.Parse(findTypeAmend[1].ToString());
                                            }
                                        }
                                        else if (item.Chargecode == "EC.CABLE")
                                        {
                                            listChargeCode.Remove(item.Chargecode);
                                            item.DocCollectCode = findTypeAmend[0];
                                            item.WaiveCharges = comboWaiveCharges.SelectedValue;
                                            item.Chargecode = tbChargeCode2.SelectedValue;
                                            item.ChargeAcct = rcbChargeAcct2.SelectedValue;
                                            item.ChargeCcy = rcbChargeCcy2.SelectedValue;
                                            if (!String.IsNullOrEmpty(tbChargeAmt2.Value.ToString()))
                                            {
                                                item.ChargeAmt = decimal.Parse(tbChargeAmt2.Value.ToString());
                                            }
                                            item.PartyCharged = rcbPartyCharged2.SelectedValue;
                                            item.OmortCharges = rcbOmortCharge2.SelectedValue;
                                            item.ChargeStatus = rcbChargeStatus2.SelectedValue;
                                            item.ChargeRemarks = tbChargeRemarks.Text.Trim();
                                            item.VATNo = tbVatNo.Text;
                                            item.TaxCode = lblTaxCode2.Text;
                                            item.TaxAmt = lblTaxAmt2.Text;
                                            item.Rowchages = "2";
                                            item.TabId = TabId;
                                            item.AmendNo = Code;
                                            if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                                            {
                                                item.AmendId = int.Parse(findTypeAmend[1].ToString());
                                            }
                                        }
                                        else if (item.Chargecode == "EC.COURIER")
                                        {
                                            listChargeCode.Remove(item.Chargecode);
                                            item.DocCollectCode = findTypeAmend[0];
                                            item.WaiveCharges = comboWaiveCharges.SelectedValue;
                                            item.Chargecode = tbChargeCode3.SelectedValue;
                                            item.ChargeAcct = rcbChargeAcct3.SelectedValue;
                                            item.ChargeCcy = rcbChargeCcy3.SelectedValue;
                                            if (!String.IsNullOrEmpty(tbChargeAmt3.Value.ToString()))
                                            {
                                                item.ChargeAmt = decimal.Parse(tbChargeAmt3.Value.ToString());
                                            }
                                            item.PartyCharged = rcbPartyCharged3.SelectedValue;
                                            item.OmortCharges = rcbOmortCharge3.SelectedValue;
                                            item.ChargeStatus = rcbChargeStatus3.SelectedValue;
                                            item.ChargeRemarks = tbChargeRemarks.Text.Trim();
                                            item.VATNo = tbVatNo.Text;
                                            item.TaxCode = lblTaxCode3.Text;
                                            item.TaxAmt = lblTaxAmt3.Text;
                                            item.Rowchages = "3";
                                            item.TabId = TabId;
                                            item.AmendNo = Code;
                                            if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                                            {
                                                item.AmendId = int.Parse(findTypeAmend[1].ToString());
                                            }
                                        }
                                        else if (item.Chargecode == "EC.OTHER")
                                        {
                                            listChargeCode.Remove(item.Chargecode);
                                            item.DocCollectCode = findTypeAmend[0];
                                            item.WaiveCharges = comboWaiveCharges.SelectedValue;
                                            item.Chargecode = tbChargeCode4.SelectedValue;
                                            item.ChargeAcct = rcbChargeAcct4.SelectedValue;
                                            item.ChargeCcy = rcbChargeCcy4.SelectedValue;
                                            if (!String.IsNullOrEmpty(tbChargeAmt4.Value.ToString()))
                                            {
                                                item.ChargeAmt = decimal.Parse(tbChargeAmt4.Value.ToString());
                                            }
                                            item.PartyCharged = rcbPartyCharged4.SelectedValue;
                                            item.OmortCharges = rcbOmortCharge4.SelectedValue;
                                            item.ChargeStatus = rcbChargeStatus4.SelectedValue;
                                            item.ChargeRemarks = tbChargeRemarks.Text.Trim();
                                            item.VATNo = tbVatNo.Text;
                                            item.TaxCode = lblTaxCode4.Text;
                                            item.TaxAmt = lblTaxAmt4.Text;
                                            item.Rowchages = "4";
                                            item.TabId = TabId;
                                        }
                                        _entities.SaveChanges();
                                    }

                                    //in case new charge code is added to existing item, add it to DB
                                    if (listChargeCode.Count > 0)
                                    {
                                        foreach (String item in listChargeCode)
                                        {
                                            var objCharge = addNewChagre(item, Code, findTypeAmend);
                                            _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Add(objCharge);
                                            _entities.SaveChanges();
                                        }
                                    }
                                }
                                
                                else
                                {

                                    var objCharge = addNewChagre("EC.AMEND", Code, findTypeAmend);
                                    _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Add(objCharge);
                                    _entities.SaveChanges();

                                    objCharge = addNewChagre("EC.CABLE", Code, findTypeAmend);
                                    _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Add(objCharge);
                                    _entities.SaveChanges();

                                    objCharge = addNewChagre("EC.COURIER", Code, findTypeAmend);
                                    _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Add(objCharge);
                                    _entities.SaveChanges();

                                    objCharge = addNewChagre("EC.OTHER", Code, findTypeAmend);
                                    _entities.BEXPORT_DOCUMETARYCOLLECTIONCHARGES.Add(objCharge);
                                    _entities.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEX)
            {
                Exception raise = dbEX;
                foreach (var validationError in dbEX.EntityValidationErrors)
                {
                    string message = string.Format("{0}:{1}", validationError.Entry.Entity.ToString(), validationError.ValidationErrors);
                    raise = new InvalidOperationException(message, raise);
                }
                throw raise;
            }
        }

        private BEXPORT_DOCUMETARYCOLLECTIONCHARGES addNewChagre(string Chargecode, string Code, String[] findTypeAmend)
        {
            //chua co them moi
            var objCharge = new BEXPORT_DOCUMETARYCOLLECTIONCHARGES();
            if (Chargecode == "EC.AMEND" && !String.IsNullOrWhiteSpace(tbChargeAmt.Text))
            {
                objCharge.DocCollectCode = findTypeAmend[0];
                objCharge.WaiveCharges = comboWaiveCharges.SelectedValue;
                objCharge.Chargecode = tbChargeCode.SelectedValue;
                objCharge.ChargeAcct = rcbChargeAcct.SelectedValue;
                objCharge.ChargeCcy = rcbChargeCcy.SelectedValue;
                if (!String.IsNullOrEmpty(tbChargeAmt.Value.ToString()))
                {
                    objCharge.ChargeAmt = decimal.Parse(tbChargeAmt.Value.ToString());
                }
                objCharge.PartyCharged = rcbPartyCharged.SelectedValue;
                objCharge.OmortCharges = rcbOmortCharge.SelectedValue;
                objCharge.ChargeStatus = rcbChargeStatus.SelectedValue;
                objCharge.ChargeRemarks = tbChargeRemarks.Text.Trim();
                objCharge.VATNo = tbVatNo.Text;
                objCharge.TaxCode = lblTaxCode.Text;
                objCharge.TaxAmt = lblTaxAmt.Text;
                objCharge.Rowchages = "1";
                objCharge.TabId = TabId;
                objCharge.AmendNo = Code;
                //item.AmendNo = Code;
                if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                {
                    objCharge.AmendId = int.Parse(findTypeAmend[1].ToString());
                }
            }

            if (Chargecode=="EC.CABLE" && !String.IsNullOrWhiteSpace(tbChargeAmt2.Text))
            {
                objCharge.DocCollectCode = findTypeAmend[0];
                objCharge.WaiveCharges = comboWaiveCharges.SelectedValue;
                objCharge.Chargecode = tbChargeCode2.SelectedValue;
                objCharge.ChargeAcct = rcbChargeAcct2.SelectedValue;
                objCharge.ChargeCcy = rcbChargeCcy2.SelectedValue;
                if (!String.IsNullOrEmpty(tbChargeAmt2.Value.ToString()))
                {
                    objCharge.ChargeAmt = decimal.Parse(tbChargeAmt2.Value.ToString());
                }
                objCharge.PartyCharged = rcbPartyCharged2.SelectedValue;
                objCharge.OmortCharges = rcbOmortCharge2.SelectedValue;
                objCharge.ChargeStatus = rcbChargeStatus2.SelectedValue;
                objCharge.ChargeRemarks = tbChargeRemarks.Text.Trim();
                objCharge.VATNo = tbVatNo.Text;
                objCharge.TaxCode = lblTaxCode2.Text;
                objCharge.TaxAmt = lblTaxAmt2.Text;
                objCharge.Rowchages = "2";
                objCharge.TabId = TabId;
                objCharge.AmendNo = Code;
                if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                {
                    objCharge.AmendId = int.Parse(findTypeAmend[1].ToString());
                }
            }

            if (Chargecode=="EC.COURIER" && !String.IsNullOrWhiteSpace(tbChargeAmt3.Text))
            {
                objCharge.DocCollectCode = findTypeAmend[0];
                objCharge.WaiveCharges = comboWaiveCharges.SelectedValue;
                objCharge.Chargecode = tbChargeCode3.SelectedValue;
                objCharge.ChargeAcct = rcbChargeAcct3.SelectedValue;
                objCharge.ChargeCcy = rcbChargeCcy3.SelectedValue;
                if (!String.IsNullOrEmpty(tbChargeAmt3.Value.ToString()))
                {
                    objCharge.ChargeAmt = decimal.Parse(tbChargeAmt3.Value.ToString());
                }
                objCharge.PartyCharged = rcbPartyCharged3.SelectedValue;
                objCharge.OmortCharges = rcbOmortCharge3.SelectedValue;
                objCharge.ChargeStatus = rcbChargeStatus3.SelectedValue;
                objCharge.ChargeRemarks = tbChargeRemarks.Text.Trim();
                objCharge.VATNo = tbVatNo.Text;
                objCharge.TaxCode = lblTaxCode3.Text;
                objCharge.TaxAmt = lblTaxAmt3.Text;
                objCharge.Rowchages = "3";
                objCharge.TabId = TabId;
                objCharge.AmendNo = Code;
                if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                {
                    objCharge.AmendId = int.Parse(findTypeAmend[1].ToString());
                }
    
            }

            if (Chargecode=="EC.OTHER" && !String.IsNullOrWhiteSpace(tbChargeAmt4.Text))
            {
                objCharge.DocCollectCode = findTypeAmend[0];
                objCharge.WaiveCharges = comboWaiveCharges.SelectedValue;
                objCharge.Chargecode = tbChargeCode4.SelectedValue;
                objCharge.ChargeAcct = rcbChargeAcct4.SelectedValue;
                objCharge.ChargeCcy = rcbChargeCcy4.SelectedValue;
                if (!String.IsNullOrEmpty(tbChargeAmt4.Value.ToString()))
                {
                    objCharge.ChargeAmt = decimal.Parse(tbChargeAmt4.Value.ToString());
                }
                objCharge.PartyCharged = rcbPartyCharged4.SelectedValue;
                objCharge.OmortCharges = rcbOmortCharge4.SelectedValue;
                objCharge.ChargeStatus = rcbChargeStatus4.SelectedValue;
                objCharge.ChargeRemarks = tbChargeRemarks.Text.Trim();
                objCharge.VATNo = tbVatNo.Text;
                objCharge.TaxCode = lblTaxCode4.Text;
                objCharge.TaxAmt = lblTaxAmt4.Text;
                objCharge.Rowchages = "4";
                objCharge.TabId = TabId;
                objCharge.AmendNo = Code;
                if (!String.IsNullOrEmpty(findTypeAmend[1].ToString()))
                {
                    objCharge.AmendId = int.Parse(findTypeAmend[1].ToString());
                }
            }

            return objCharge;
        }

        protected void SaveData()
        {
            try
            {

                bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_Insert(txtCode.Text.Trim()
                                                              , comboDrawerCusNo.SelectedValue
                                                              , txtDrawerCusName.Text.Trim()
                                                              , txtDrawerAddr1.Text.Trim()
                                                              , txtDrawerAddr2.Text.Trim()
                                                              , txtDrawerAddr3.Text.Trim()
                                                              , txtDrawerRefNo.Text.Trim()
                                                              , comboCollectingBankNo.Text
                                                              , txtCollectingBankName.Text.Trim()
                                                              , txtCollectingBankAddr1.Text.Trim(),
                                                              txtCollectingBankAddr2.Text.Trim()
                                                              , txtCollectingBankAddr3.Text.Trim()
                                                              , comboCollectingBankAcct.SelectedValue
                                                              , txtDraweeCusNo.Text
                                                              , txtDraweeCusName.Text.Trim()
                                                              , txtDraweeAddr1.Text.Trim()
                                                              , txtDraweeAddr2.Text.Trim()
                                                              , txtDraweeAddr3.Text.Trim()
                                                              , txtDraweeAddr4.Text.Trim()
                                                              , comboNostroCusNo.Text
                                                              , comboCurrency.SelectedValue
                                                              , numAmount.Value.ToString()
                                                              , dteDocsReceivedDate.SelectedDate.ToString()
                                                              , dteMaturityDate.SelectedDate.ToString()
                                                              , txtTenor.Text.Trim()
                                                              , "0"//numDays.Value.ToString()
                                                              , dteTracerDate.SelectedDate.ToString()
                                                              , numReminderDays.Value.ToString()
                                                              , comboCommodity.SelectedValue
                                                              , comboDocsCode1.SelectedValue
                                                              , numNoOfOriginals1.Value.ToString()
                                                              , numNoOfCopies1.Value.ToString()
                                                              , comboDocsCode2.SelectedValue
                                                              , numNoOfOriginals2.Value.ToString()
                                                              , numNoOfCopies2.Value.ToString()
                                                              , comboDocsCode3.SelectedValue
                                                              , numNoOfOriginals3.Value.ToString()
                                                              , numNoOfCopies3.Value.ToString()
                                                              , txtOtherDocs.Text.Trim()
                                                              , txtRemarks.Text.Trim()
                                                              , txtRemarks1.Text.Trim()
                                                              , txtRemarks2.Text.Trim()
                                                              , txtRemarks3.Text.Trim()
                                                              , UserId.ToString()
                                                              , comboCollectionType.SelectedValue
                                                              , dteCancelDate.SelectedDate.ToString()
                                                              , dteContingentExpiryDate.SelectedDate.ToString()
                                                              , txtCancelRemark.Text
                                                              , comboAccountOfficer.SelectedValue
                                                              , TabId.ToString()
                                                              , dtAcceptDate.SelectedDate.ToString()
                                                              , txtAcceptREmark.Text
                                                              , ScreenType.ToString("G")
                    );

                if (!string.IsNullOrWhiteSpace(tbChargeAmt.Text))
                {
                    bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                        comboWaiveCharges.SelectedValue, tbChargeCode.SelectedValue, rcbChargeAcct.SelectedValue, ""
                        /*tbChargePeriod.Text*/,
                        rcbChargeCcy.SelectedValue, "0" /*tbExcheRate.Text*/, tbChargeAmt.Text,
                        rcbPartyCharged.SelectedValue, rcbOmortCharge.SelectedValue, "", "",
                        rcbChargeStatus.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode.Text, ""
                        /*lblTaxCcy.Text*/, lblTaxAmt.Text, "", "", "1", TabId);
                }
                if (!string.IsNullOrWhiteSpace(tbChargeAmt2.Text))
                {
                    bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                        comboWaiveCharges.SelectedValue, tbChargeCode2.SelectedValue, rcbChargeAcct2.SelectedValue, ""
                        /*tbChargePeriod2.Text*/,
                        rcbChargeCcy2.SelectedValue, "0" /*tbExcheRate2.Text*/, tbChargeAmt2.Text,
                        rcbPartyCharged2.SelectedValue, rcbOmortCharge2.SelectedValue, "", "",
                        rcbChargeStatus2.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode2.Text, ""
                        /*lblTaxCcy2.Text*/, lblTaxAmt2.Text, "", "", "2", TabId);
                }
                if (!string.IsNullOrWhiteSpace(tbChargeAmt3.Text))
                {
                    bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                        comboWaiveCharges.SelectedValue, tbChargeCode3.SelectedValue, rcbChargeAcct3.SelectedValue, ""
                        /*tbChargePeriod3.Text*/,
                        rcbChargeCcy3.SelectedValue, "0" /*tbExcheRate2.Text*/, tbChargeAmt3.Text,
                        rcbPartyCharged3.SelectedValue, rcbOmortCharge3.SelectedValue, "", "",
                        rcbChargeStatus3.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode3.Text, ""
                        /*lblTaxCcy2.Text*/, lblTaxAmt3.Text, "", "", "3", TabId);
                }
                if (!string.IsNullOrWhiteSpace(tbChargeAmt4.Text))
                {
                    bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                         comboWaiveCharges.SelectedValue, tbChargeCode4.SelectedValue, rcbChargeAcct4.SelectedValue, ""
                        /*tbChargePeriod4.Text*/,
                         rcbChargeCcy4.SelectedValue, "0" /*tbExcheRate2.Text*/, tbChargeAmt4.Text,
                         rcbPartyCharged4.SelectedValue, rcbOmortCharge4.SelectedValue, "", "",
                         rcbChargeStatus4.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode4.Text, ""
                        /*lblTaxCcy2.Text*/, lblTaxAmt4.Text, "", "", "4", TabId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void LoadDataForAmend(BEXPORT_DOCUMETARYCOLLECTION obj, List<BEXPORT_DOCUMETARYCOLLECTIONCHARGES> lstCharge)
        {
            try
            {
                if (obj != null)
                {
                    RadToolBar1.FindItemByValue("btReview").Enabled = false;

                    if (!String.IsNullOrEmpty(obj.OldAmount.ToString()))
                    {
                        AmountOld = double.Parse(obj.OldAmount.ToString());
                    }
                    if (!String.IsNullOrEmpty(obj.Amount.ToString()))
                    {
                        Amount = double.Parse(obj.Amount.ToString());
                    }

                    /*
                     * If this is first time Amend data, old amount will be 0
                     * In that case, show old amount as original amount
                     */
                    if (0 == AmountOld)
                    {
                        AmountOld = Amount;
                    }

                    // DocumentaryCollectionCancel
                    if (!string.IsNullOrEmpty(obj.CancelDate.ToString()) && obj.CancelDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteCancelDate.SelectedDate = DateTime.Parse(obj.CancelDate.ToString());
                    }
                    if (!string.IsNullOrEmpty(obj.ContingentExpiryDate.ToString()) && obj.ContingentExpiryDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteContingentExpiryDate.SelectedDate = DateTime.Parse(obj.ContingentExpiryDate.ToString());
                    }

                    if (string.IsNullOrEmpty(obj.Cancel_Status))
                    {
                        dteCancelDate.SelectedDate = DateTime.Now;
                        dteContingentExpiryDate.SelectedDate = DateTime.Now;
                    }

                    txtCancelRemark.Text = obj.CancelRemark;

                    // Outgoing Document Acception
                    if (!string.IsNullOrEmpty(obj.AcceptedDate.ToString()) && obj.AcceptedDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dtAcceptDate.SelectedDate = DateTime.Parse(obj.AcceptedDate.ToString());
                    }


                    if (string.IsNullOrEmpty(obj.AcceptStatus))
                    {
                        dtAcceptDate.SelectedDate = DateTime.Now;
                    }

                    txtAcceptREmark.Text = obj.AcceptedRemarks;

                    ///////////////////////////////////////
                    // CC
                    if (obj.CollectionType == "CC")
                    {
                        comboCollectionType.Items.Clear();
                        comboCollectionType.DataValueField = "ID";
                        comboCollectionType.DataTextField = "ID";
                        comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
                        comboCollectionType.DataBind();
                        divCollectionType.Visible = false;
                        divDocsCode.Visible = false;
                        divDocsCode2.Visible = false;
                        divDocsCode3.Visible = false;

                    }
                    // end cc
                    comboCollectionType.SelectedValue = obj.CollectionType;
                    lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

                    comboAccountOfficer.SelectedValue = obj.Accountofficer;
                    comboDrawerCusNo.SelectedValue = obj.DrawerCusNo;
                    txtDrawerCusName.Text = obj.DrawerCusName;
                    txtDrawerAddr1.Text = obj.DrawerAddr1;
                    txtDrawerAddr2.Text = obj.DrawerAddr2;
                    txtDrawerAddr3.Text = obj.DrawerAddr3;
                    txtDrawerRefNo.Text = obj.DrawerRefNo;
                    comboCollectingBankNo.Text = obj.CollectingBankNo;
                    txtCollectingBankName.Text = obj.CollectingBankName;
                    txtCollectingBankAddr1.Text = obj.CollectingBankAddr1;
                    txtCollectingBankAddr2.Text = obj.CollectingBankAddr2;
                    comboCollectingBankAcct.SelectedValue = obj.CollectingBankAcct;
                    txtDraweeCusNo.Text = obj.DraweeCusNo;
                    txtDraweeCusName.Text = obj.DraweeCusName;
                    txtDraweeAddr1.Text = obj.DraweeAddr1;
                    txtDraweeAddr2.Text = obj.DraweeAddr2;
                    txtDraweeAddr3.Text = obj.DraweeAddr3;
                    txtDraweeAddr4.Text = obj.DraweeAddr4;
                    comboNostroCusNo.Text = obj.NostroCusNo;
                    loadNostroDes();
                    //lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
                    comboCurrency.SelectedValue = obj.Currency;
                    numAmount.Value = Amount;
                    lblAmount_New.Text = AmountOld.ToString("C");
                    txtTenor.Text = obj.Tenor;
                    numReminderDays.Text = obj.ReminderDays.ToString();

                    comboCommodity.SelectedValue = obj.Commodity;
                    //txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name2;

                    comboDocsCode1.SelectedValue = obj.DocsCode1;
                    numNoOfOriginals1.Text = obj.NoOfOriginals1.ToString();
                    numNoOfCopies1.Text = obj.NoOfCopies1.ToString();


                    comboDocsCode2.SelectedValue = obj.DocsCode2;
                    numNoOfOriginals2.Text = obj.NoOfOriginals2.ToString();
                    numNoOfCopies2.Text = obj.NoOfCopies2.ToString();

                    comboDocsCode3.SelectedValue = obj.DocsCode3;
                    numNoOfOriginals3.Text = obj.NoOfOriginals3.ToString();
                    numNoOfCopies3.Text = obj.NoOfCopies3.ToString();



                    if ((!string.IsNullOrWhiteSpace(obj.NoOfOriginals2.ToString()) &&
                         int.Parse(obj.NoOfOriginals2.ToString()) > 0) ||
                        (!string.IsNullOrWhiteSpace(obj.NoOfCopies2.ToString()) &&
                         int.Parse(obj.NoOfCopies2.ToString()) > 0))
                    {
                        divDocsCode2.Visible = true;
                    }
                    if ((!string.IsNullOrWhiteSpace(obj.NoOfOriginals3.ToString()) &&
                         int.Parse(obj.NoOfOriginals3.ToString()) > 0) ||
                        (!string.IsNullOrWhiteSpace(obj.NoOfCopies3.ToString()) &&
                         int.Parse(obj.NoOfCopies3.ToString()) > 0))
                    {
                        divDocsCode3.Visible = true;
                    }

                    txtOtherDocs.Text = obj.OtherDocs;
                    txtRemarks.Text = obj.Remarks;
                    txtRemarks1.Text = obj.Remarks1;
                    txtRemarks2.Text = obj.Remarks2;
                    txtRemarks3.Text = obj.Remarks3;

                    if (!string.IsNullOrEmpty(obj.DocsReceivedDate.ToString()) && obj.DocsReceivedDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteDocsReceivedDate.SelectedDate = DateTime.Parse(obj.DocsReceivedDate.ToString());
                    }
                    if (!string.IsNullOrEmpty(obj.MaturityDate.ToString()) && obj.MaturityDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteMaturityDate.SelectedDate = DateTime.Parse(obj.MaturityDate.ToString());
                    }
                    if (!string.IsNullOrEmpty(obj.TracerDate.ToString()) && obj.TracerDate.ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteTracerDate.SelectedDate = DateTime.Parse(obj.TracerDate.ToString());
                    }
                }
                else
                {
                    comboCollectionType.SelectedValue = string.Empty;
                    lblCollectionTypeName.Text = string.Empty;


                    comboNostroCusNo.Text = string.Empty;
                    txtDrawerCusName.Text = string.Empty;
                    txtDrawerAddr1.Text = string.Empty;
                    txtDrawerAddr2.Text = string.Empty;
                    txtDrawerAddr3.Text = string.Empty;
                    txtDrawerRefNo.Text = string.Empty;
                    comboCollectingBankNo.Text = string.Empty;
                    txtCollectingBankName.Text = string.Empty;
                    txtCollectingBankAddr1.Text = string.Empty;
                    txtCollectingBankAddr2.Text = string.Empty;
                    comboCollectingBankAcct.SelectedValue = string.Empty;
                    txtDraweeCusName.Text = string.Empty;
                    txtDraweeAddr1.Text = string.Empty;
                    txtDraweeAddr2.Text = string.Empty;
                    txtDraweeAddr3.Text = string.Empty;
                    txtDraweeAddr4.Text = string.Empty;
                    comboNostroCusNo.Text = string.Empty;
                    lblNostroCusName.Text = string.Empty;
                    comboCurrency.SelectedValue = string.Empty;
                    numAmount.Text = string.Empty;
                    txtTenor.Text = "AT SIGHT";
                    numReminderDays.Text = string.Empty;

                    comboCommodity.SelectedValue = string.Empty;
                    comboDocsCode1.SelectedValue = string.Empty;
                    numNoOfOriginals1.Text = string.Empty;
                    numNoOfCopies1.Text = string.Empty;

                    comboDocsCode2.SelectedValue = string.Empty;
                    numNoOfOriginals2.Text = string.Empty;
                    numNoOfCopies2.Text = string.Empty;

                    txtOtherDocs.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtRemarks1.Text = string.Empty;
                    txtRemarks2.Text = string.Empty;
                    txtRemarks3.Text = string.Empty;

                    dteDocsReceivedDate.SelectedDate = null;
                    dteMaturityDate.SelectedDate = null;
                    dteTracerDate.SelectedDate = null;

                    Cal_TracerDate(false);
                }
                //khong load tab charge
                foreach (var item in lstCharge)
                {
                    if (item.Chargecode == "EC.AMEND")
                    {
                        comboWaiveCharges.SelectedValue = item.WaiveCharges;
                        rcbChargeAcct.SelectedValue = item.ChargeAcct;
                        //tbChargePeriod.Text = item.ChargePeriod;
                        rcbChargeCcy.SelectedValue = item.ChargeCcy;
                        if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                        {
                            LoadChargeAcct();
                        }
                        //tbExcheRate.Text = item.ExchRate;
                        tbChargeAmt.Text = item.ChargeAmt.ToString();
                        rcbPartyCharged.SelectedValue = item.PartyCharged;
                        rcbOmortCharge.SelectedValue = item.OmortCharges;
                        rcbChargeStatus.SelectedValue = item.ChargeStatus;
                        lblChargeStatus.Text = item.ChargeStatus;

                        tbChargeRemarks.Text = item.ChargeRemarks;
                        tbVatNo.Text = item.VATNo;
                        lblTaxCode.Text = item.TaxCode;
                        //lblTaxCcy.Text = item.TaxCcy;
                        lblTaxAmt.Text = item.TaxAmt;
                        tbChargeCode.SelectedValue = item.Chargecode;
                        ChargeAmount += ConvertStringToFloat(item.ChargeAmt.ToString());
                    }
                    else if (item.Chargecode == "EC.CABLE")
                    {
                        rcbChargeAcct2.SelectedValue = item.ChargeAcct;

                        rcbChargeCcy2.SelectedValue = item.ChargeCcy;
                        if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                        {
                            LoadChargeAcct2();
                        }

                        tbChargeAmt2.Text = item.ChargeAmt.ToString();
                        rcbPartyCharged2.SelectedValue = item.PartyCharged;
                        rcbChargeStatus2.SelectedValue = item.ChargeStatus;
                        lblChargeStatus2.Text = item.ChargeStatus;

                        lblTaxCode2.Text = item.TaxCode;
                        lblTaxAmt2.Text = item.TaxAmt;

                        tbChargeCode2.SelectedValue = item.Chargecode;
                        ChargeAmount += ConvertStringToFloat(item.ChargeAmt.ToString());
                    }
                    else if (item.Chargecode == "EC.COURIER")
                    {
                        rcbChargeAcct3.SelectedValue = item.ChargeAcct;

                        rcbChargeCcy3.SelectedValue = item.ChargeCcy;
                        if (!string.IsNullOrEmpty(rcbChargeCcy3.SelectedValue))
                        {
                            LoadChargeAcct3();
                        }

                        tbChargeAmt3.Text = item.ChargeAmt.ToString();
                        rcbPartyCharged3.SelectedValue = item.PartyCharged;
                        //lblPartyCharged3.Text = item.PartyCharged;
                        rcbChargeStatus3.SelectedValue = item.ChargeStatus;
                        //lblChargeStatus3.Text = item.ChargeStatus;

                        lblTaxCode3.Text = item.TaxCode;
                        lblTaxAmt3.Text = item.TaxAmt;

                        tbChargeCode3.SelectedValue = item.Chargecode;
                        ChargeAmount += ConvertStringToFloat(item.ChargeAmt.ToString());
                    }
                    else if (item.Chargecode == "EC.OTHER")
                    {
                        rcbChargeAcct4.SelectedValue = item.ChargeAcct;

                        rcbChargeCcy4.SelectedValue = item.ChargeCcy;
                        if (!string.IsNullOrEmpty(rcbChargeCcy4.SelectedValue))
                        {
                            LoadChargeAcct4();
                        }

                        tbChargeAmt4.Text = item.ChargeAmt.ToString();
                        rcbPartyCharged4.SelectedValue = item.PartyCharged;
                        //lblPartyCharged4.Text = item.PartyCharged;
                        rcbChargeStatus4.SelectedValue = item.ChargeStatus;
                        //lblChargeStatus3.Text = item.ChargeStatus;

                        lblTaxCode4.Text = item.TaxCode;
                        lblTaxAmt4.Text = item.TaxAmt;

                        tbChargeCode4.SelectedValue = item.Chargecode;
                        ChargeAmount += ConvertStringToFloat(item.ChargeAmt.ToString());
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEX)
            {
                Exception raise = dbEX;
                foreach (var validationError in dbEX.EntityValidationErrors)
                {
                    string message = string.Format("{0}:{1}", validationError.Entry.Entity.ToString(), validationError.ValidationErrors);
                    raise = new InvalidOperationException(message, raise);
                }
                lblError.Text = raise.Message;
                throw raise;
            }
            //SetVisibilityByStatus(dsDoc);
        }
        protected void LoadData(DataSet dsDoc)
        {
            try
            {
                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    RadToolBar1.FindItemByValue("btReview").Enabled = false;
                    var drow = dsDoc.Tables[0].Rows[0];
                    if (drow["Amend_Status"].ToString() == "AUT")
                    {
                        double.TryParse(drow["Amount"].ToString(), out Amount);
                        double.TryParse(drow["AmountOld"].ToString(), out AmountOld);
                    }
                    else
                    {
                        double.TryParse(drow["Amount"].ToString(), out Amount);
                        double.TryParse(drow["OldAmount"].ToString(), out AmountOld);
                    }

                    // DocumentaryCollectionCancel
                    if (!string.IsNullOrEmpty(drow["CancelDate"].ToString()) && drow["CancelDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteCancelDate.SelectedDate = DateTime.Parse(drow["CancelDate"].ToString());
                    }


                    if (!string.IsNullOrEmpty(drow["ContingentExpiryDate"].ToString()) && drow["ContingentExpiryDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteContingentExpiryDate.SelectedDate = DateTime.Parse(drow["ContingentExpiryDate"].ToString());
                    }

                    if (string.IsNullOrEmpty(drow["Cancel_Status"].ToString()))
                    {
                        dteCancelDate.SelectedDate = DateTime.Now;
                        dteContingentExpiryDate.SelectedDate = DateTime.Now;
                    }

                    txtCancelRemark.Text = drow["CancelRemark"].ToString();

                    // Outgoing Document Acception
                    if (!string.IsNullOrEmpty(drow["AcceptedDate"].ToString()) && drow["AcceptedDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dtAcceptDate.SelectedDate = DateTime.Parse(drow["AcceptedDate"].ToString());
                    }


                    if (string.IsNullOrEmpty(drow["AcceptStatus"].ToString()))
                    {
                        dtAcceptDate.SelectedDate = DateTime.Now;
                    }

                    txtAcceptREmark.Text = drow["AcceptedRemarks"].ToString();

                    ///////////////////////////////////////
                    // CC
                    if (drow["CollectionType"].ToString() == "CC")
                    {
                        comboCollectionType.Items.Clear();
                        comboCollectionType.DataValueField = "ID";
                        comboCollectionType.DataTextField = "ID";
                        comboCollectionType.DataSource = bd.SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
                        comboCollectionType.DataBind();
                        divCollectionType.Visible = false;
                        divDocsCode.Visible = false;
                        divDocsCode2.Visible = false;
                        divDocsCode3.Visible = false;

                    }
                    // end cc
                    comboCollectionType.SelectedValue = drow["CollectionType"].ToString();
                    lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

                    comboAccountOfficer.SelectedValue = drow["Accountofficer"].ToString();
                    comboDrawerCusNo.SelectedValue = drow["DrawerCusNo"].ToString();
                    txtDrawerCusName.Text = drow["DrawerCusName"].ToString();
                    txtDrawerAddr1.Text = drow["DrawerAddr1"].ToString();
                    txtDrawerAddr2.Text = drow["DrawerAddr2"].ToString();
                    txtDrawerAddr3.Text = drow["DrawerAddr3"].ToString();
                    txtDrawerRefNo.Text = drow["DrawerRefNo"].ToString();
                    comboCollectingBankNo.Text = drow["CollectingBankNo"].ToString();
                    txtCollectingBankName.Text = drow["CollectingBankName"].ToString();
                    txtCollectingBankAddr1.Text = drow["CollectingBankAddr1"].ToString();
                    txtCollectingBankAddr2.Text = drow["CollectingBankAddr2"].ToString();
                    comboCollectingBankAcct.SelectedValue = drow["CollectingBankAcct"].ToString();
                    txtDraweeCusNo.Text = drow["DraweeCusNo"].ToString();
                    txtDraweeCusName.Text = drow["DraweeCusName"].ToString();
                    txtDraweeAddr1.Text = drow["DraweeAddr1"].ToString();
                    txtDraweeAddr2.Text = drow["DraweeAddr2"].ToString();
                    txtDraweeAddr3.Text = drow["DraweeAddr3"].ToString();
                    txtDraweeAddr4.Text = drow["DraweeAddr4"].ToString();
                    comboNostroCusNo.Text = drow["NostroCusNo"].ToString();
                    comboCurrency.SelectedValue = drow["Currency"].ToString();
                    //
                    //var obj = _entities.BSWIFTCODEs.Where(x => x.Code == comboNostroCusNo.Text && x.Currency == comboCurrency.SelectedValue).FirstOrDefault();
                    loadNostroDes();
                    //
                    //lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];

                    numAmount.Value = Amount;
                    lblAmount_New.Text = AmountOld.ToString("C");
                    txtTenor.Text = drow["Tenor"].ToString();
                    numReminderDays.Text = drow["ReminderDays"].ToString();

                    comboCommodity.SelectedValue = drow["Commodity"].ToString();
                    //txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name2"];

                    comboDocsCode1.SelectedValue = drow["DocsCode1"].ToString();
                    numNoOfOriginals1.Text = drow["NoOfOriginals1"].ToString();
                    numNoOfCopies1.Text = drow["NoOfCopies1"].ToString();


                    comboDocsCode2.SelectedValue = drow["DocsCode2"].ToString();
                    numNoOfOriginals2.Text = drow["NoOfOriginals2"].ToString();
                    numNoOfCopies2.Text = drow["NoOfCopies2"].ToString();

                    comboDocsCode3.SelectedValue = drow["DocsCode3"].ToString();
                    numNoOfOriginals3.Text = drow["NoOfOriginals3"].ToString();
                    numNoOfCopies3.Text = drow["NoOfCopies3"].ToString();



                    if ((!string.IsNullOrWhiteSpace(drow["NoOfOriginals2"].ToString()) &&
                         int.Parse(drow["NoOfOriginals2"].ToString()) > 0) ||
                        (!string.IsNullOrWhiteSpace(drow["NoOfCopies2"].ToString()) &&
                         int.Parse(drow["NoOfCopies2"].ToString()) > 0))
                    {
                        divDocsCode2.Visible = true;
                    }
                    if ((!string.IsNullOrWhiteSpace(drow["NoOfOriginals3"].ToString()) &&
                         int.Parse(drow["NoOfOriginals3"].ToString()) > 0) ||
                        (!string.IsNullOrWhiteSpace(drow["NoOfCopies3"].ToString()) &&
                         int.Parse(drow["NoOfCopies3"].ToString()) > 0))
                    {
                        divDocsCode3.Visible = true;
                    }

                    txtOtherDocs.Text = drow["OtherDocs"].ToString();
                    txtRemarks.Text = drow["Remarks"].ToString();
                    txtRemarks1.Text = drow["Remarks1"].ToString();
                    txtRemarks2.Text = drow["Remarks2"].ToString();
                    txtRemarks3.Text = drow["Remarks3"].ToString();
                    if (!string.IsNullOrEmpty(drow["DocsReceivedDate"].ToString()) && drow["DocsReceivedDate"].ToString().IndexOf("1/1/1900") == -1)
                    //if (drow["DocsReceivedDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteDocsReceivedDate.SelectedDate = DateTime.Parse(drow["DocsReceivedDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(drow["MaturityDate"].ToString()) && drow["MaturityDate"].ToString().IndexOf("1/1/1900") == -1)
                    //if (drow["MaturityDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteMaturityDate.SelectedDate = DateTime.Parse(drow["MaturityDate"].ToString());
                    }
                    if (!string.IsNullOrEmpty(drow["TracerDate"].ToString()) && drow["TracerDate"].ToString().IndexOf("1/1/1900") == -1)
                    //if (drow["TracerDate"].ToString().IndexOf("1/1/1900") == -1)
                    {
                        dteTracerDate.SelectedDate = DateTime.Parse(drow["TracerDate"].ToString());
                    }
                }
                else
                {
                    comboCollectionType.SelectedValue = string.Empty;
                    lblCollectionTypeName.Text = string.Empty;


                    comboNostroCusNo.Text = string.Empty;
                    txtDrawerCusName.Text = string.Empty;
                    txtDrawerAddr1.Text = string.Empty;
                    txtDrawerAddr2.Text = string.Empty;
                    txtDrawerAddr3.Text = string.Empty;
                    txtDrawerRefNo.Text = string.Empty;
                    comboCollectingBankNo.Text = string.Empty;
                    txtCollectingBankName.Text = string.Empty;
                    txtCollectingBankAddr1.Text = string.Empty;
                    txtCollectingBankAddr2.Text = string.Empty;
                    txtCollectingBankAddr3.Text = string.Empty;
                    comboCollectingBankAcct.SelectedValue = string.Empty;
                    txtDraweeCusName.Text = string.Empty;
                    txtDraweeAddr1.Text = string.Empty;
                    txtDraweeAddr2.Text = string.Empty;
                    txtDraweeAddr3.Text = string.Empty;
                    txtDraweeAddr4.Text = string.Empty;
                    comboNostroCusNo.Text = string.Empty;
                    comboCurrency.SelectedValue = string.Empty;
                    numAmount.Text = string.Empty;
                    txtTenor.Text = "AT SIGHT";
                    numReminderDays.Text = string.Empty;

                    comboCommodity.SelectedValue = string.Empty;
                    comboDocsCode1.SelectedValue = string.Empty;
                    numNoOfOriginals1.Text = string.Empty;
                    numNoOfCopies1.Text = string.Empty;

                    comboDocsCode2.SelectedValue = string.Empty;
                    numNoOfOriginals2.Text = string.Empty;
                    numNoOfCopies2.Text = string.Empty;

                    txtOtherDocs.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    txtRemarks1.Text = string.Empty;
                    txtRemarks2.Text = string.Empty;
                    txtRemarks3.Text = string.Empty;

                    dteDocsReceivedDate.SelectedDate = null;
                    dteMaturityDate.SelectedDate = null;
                    dteTracerDate.SelectedDate = null;

                    Cal_TracerDate(false);
                }


                #region tab Charge
                if (dsDoc.Tables[1].Rows.Count > 0)
                {
                    var drow1 = dsDoc.Tables[1].Rows[0];

                    comboWaiveCharges.SelectedValue = drow1["WaiveCharges"].ToString();
                    rcbChargeAcct.SelectedValue = drow1["ChargeAcct"].ToString();

                    //tbChargePeriod.Text = drow1["ChargePeriod"].ToString();
                    rcbChargeCcy.SelectedValue = drow1["ChargeCcy"].ToString();
                    if (!string.IsNullOrEmpty(rcbChargeCcy.SelectedValue))
                    {
                        LoadChargeAcct();
                    }

                    //tbExcheRate.Text = drow1["ExchRate"].ToString();
                    tbChargeAmt.Text = drow1["ChargeAmt"].ToString();
                    rcbPartyCharged.SelectedValue = drow1["PartyCharged"].ToString();
                    rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                    rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                    lblChargeStatus.Text = drow1["ChargeStatus"].ToString();

                    tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                    tbVatNo.Text = drow1["VATNo"].ToString();
                    lblTaxCode.Text = drow1["TaxCode"].ToString();
                    //lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                    lblTaxAmt.Text = drow1["TaxAmt"].ToString();

                    //tbChargeCode.SelectedValue = drow1["Chargecode"].ToString();

                    ChargeAmount += ConvertStringToFloat(drow1["ChargeAmt"].ToString());

                }
                else
                {
                    comboWaiveCharges.SelectedValue = "NO";
                    rcbChargeAcct.SelectedValue = string.Empty;
                    //tbChargePeriod.Text = "1";
                    rcbChargeCcy.SelectedValue = string.Empty;
                    //tbExcheRate.Text = string.Empty;
                    tbChargeAmt.Text = string.Empty;
                    rcbPartyCharged.SelectedValue = string.Empty;
                    rcbOmortCharge.SelectedValue = string.Empty;
                    rcbChargeStatus.SelectedValue = string.Empty;
                    lblChargeStatus.Text = string.Empty;

                    tbChargeRemarks.Text = string.Empty;
                    tbVatNo.Text = "154";
                    lblTaxCode.Text = string.Empty;
                    //lblTaxCcy.Text = string.Empty;
                    lblTaxAmt.Text = string.Empty;

                    //tbChargeCode.SelectedValue = string.Empty;

                    //lblChargeAcct.Text = string.Empty;
                    lblChargeStatus.Text = string.Empty;
                }

                if (dsDoc.Tables[2].Rows.Count > 0)
                {
                    var drow2 = dsDoc.Tables[2].Rows[0];

                    //divChargeInfo2.Visible = true;

                    rcbChargeAcct2.SelectedValue = drow2["ChargeAcct"].ToString();

                    rcbChargeCcy2.SelectedValue = drow2["ChargeCcy"].ToString();
                    if (!string.IsNullOrEmpty(rcbChargeCcy2.SelectedValue))
                    {
                        LoadChargeAcct2();
                    }

                    tbChargeAmt2.Text = drow2["ChargeAmt"].ToString();
                    rcbPartyCharged2.SelectedValue = drow2["PartyCharged"].ToString();
                    rcbChargeStatus2.SelectedValue = drow2["ChargeStatus"].ToString();
                    lblChargeStatus2.Text = drow2["ChargeStatus"].ToString();

                    lblTaxCode2.Text = drow2["TaxCode"].ToString();
                    lblTaxAmt2.Text = drow2["TaxAmt"].ToString();

                    //tbChargeCode2.SelectedValue = drow2["Chargecode"].ToString();
                    ChargeAmount += ConvertStringToFloat(drow2["ChargeAmt"].ToString());
                }
                else
                {
                    rcbChargeAcct2.SelectedValue = string.Empty;
                    rcbChargeCcy2.SelectedValue = string.Empty;
                    tbChargeAmt2.Text = string.Empty;
                    rcbPartyCharged2.SelectedValue = string.Empty;
                    rcbChargeStatus2.SelectedValue = string.Empty;
                    lblChargeStatus2.Text = string.Empty;

                    lblTaxCode2.Text = string.Empty;
                    lblTaxAmt2.Text = string.Empty;

                    //tbChargeCode2.SelectedValue = string.Empty;

                    //lblChargeAcct2.Text = string.Empty;
                    lblChargeStatus2.Text = string.Empty;
                }
                if (dsDoc.Tables[3].Rows.Count > 0)
                {
                    var drow3 = dsDoc.Tables[3].Rows[0];

                    //divChargeInfo2.Visible = true;

                    rcbChargeAcct3.SelectedValue = drow3["ChargeAcct"].ToString();

                    rcbChargeCcy3.SelectedValue = drow3["ChargeCcy"].ToString();
                    if (!string.IsNullOrEmpty(rcbChargeCcy3.SelectedValue))
                    {
                        LoadChargeAcct3();
                    }

                    tbChargeAmt3.Text = drow3["ChargeAmt"].ToString();
                    rcbPartyCharged3.SelectedValue = drow3["PartyCharged"].ToString();
                    //lblPartyCharged3.Text = drow3["PartyCharged"].ToString();
                    rcbChargeStatus3.SelectedValue = drow3["ChargeStatus"].ToString();
                    //lblChargeStatus3.Text = drow3["ChargeStatus"].ToString();

                    lblTaxCode3.Text = drow3["TaxCode"].ToString();
                    lblTaxAmt3.Text = drow3["TaxAmt"].ToString();

                    //tbChargeCode3.SelectedValue = drow3["Chargecode"].ToString();
                    ChargeAmount += ConvertStringToFloat(drow3["ChargeAmt"].ToString());
                }
                else
                {
                    rcbChargeAcct3.SelectedValue = string.Empty;
                    rcbChargeCcy3.SelectedValue = string.Empty;
                    tbChargeAmt3.Text = string.Empty;
                    rcbPartyCharged3.SelectedValue = string.Empty;
                    //lblPartyCharged3.Text = string.Empty;
                    rcbChargeStatus3.SelectedValue = string.Empty;
                    //lblChargeStatus3.Text = string.Empty;

                    lblTaxCode3.Text = string.Empty;
                    lblTaxAmt3.Text = string.Empty;

                    //tbChargeCode3.SelectedValue = string.Empty;

                    //lblChargeAcct3.Text = string.Empty;
                    //lblPartyCharged3.Text = string.Empty;
                    //lblChargeStatus3.Text = string.Empty;
                }
                if (dsDoc.Tables[4].Rows.Count > 0)
                {
                    var drow4 = dsDoc.Tables[4].Rows[0];

                    //divChargeInfo2.Visible = true;

                    rcbChargeAcct4.SelectedValue = drow4["ChargeAcct"].ToString();

                    rcbChargeCcy4.SelectedValue = drow4["ChargeCcy"].ToString();
                    if (!string.IsNullOrEmpty(rcbChargeCcy4.SelectedValue))
                    {
                        LoadChargeAcct4();
                    }

                    tbChargeAmt4.Text = drow4["ChargeAmt"].ToString();
                    rcbPartyCharged4.SelectedValue = drow4["PartyCharged"].ToString();
                    //lblPartyCharged4.Text = drow4["PartyCharged"].ToString();
                    rcbChargeStatus4.SelectedValue = drow4["ChargeStatus"].ToString();
                    //lblChargeStatus4.Text = drow4["ChargeStatus"].ToString();

                    lblTaxCode4.Text = drow4["TaxCode"].ToString();
                    lblTaxAmt4.Text = drow4["TaxAmt"].ToString();

                    //tbChargeCode4.SelectedValue = drow4["Chargecode"].ToString();
                    ChargeAmount += ConvertStringToFloat(drow4["ChargeAmt"].ToString());
                }
                else
                {
                    rcbChargeAcct4.SelectedValue = string.Empty;
                    rcbChargeCcy4.SelectedValue = string.Empty;
                    tbChargeAmt4.Text = string.Empty;
                    rcbPartyCharged4.SelectedValue = string.Empty;
                    //lblPartyCharged4.Text = string.Empty;
                    rcbChargeStatus4.SelectedValue = string.Empty;
                    //lblChargeStatus4.Text = string.Empty;

                    lblTaxCode4.Text = string.Empty;
                    lblTaxAmt4.Text = string.Empty;

                    //tbChargeCode4.SelectedValue = string.Empty;

                    //lblChargeAcct4.Text = string.Empty;
                    //lblPartyCharged4.Text = string.Empty;
                    //lblChargeStatus4.Text = string.Empty;
                }
                #endregion

                SetVisibilityByStatus(dsDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadNostroDes()
        {
            var obj = _entities.BSWIFTCODEs.Where(x => x.Code == comboNostroCusNo.Text).FirstOrDefault();
            if (obj != null)
            {
                lblNostroCusName.Text = obj.Description;
            }
            else if (!comboNostroCusNo.Text.Equals(""))
            {
                lblNostroCusName.Text = "It's not Nostro account";
            }
            else
            {
                lblNostroCusName.Text = "";
            }
        }

        protected void Authorize()
        {
            if (TabId != 229)
            {
                bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), "AUT", UserId.ToString(), ScreenType.ToString("G"));
            }
            else
            {
                var detail = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.AmendNo == txtCode.Text && x.ActiveRecordFlag == YesNo.YES).FirstOrDefault();
                if (detail != null)
                {
                    detail.Amend_Status = "AUT";
                }
                _entities.SaveChanges();
            }
            Response.Redirect(Globals.NavigateURL(TabId));
        }

        protected void Revert()
        {
            if (TabId != 229)
            {
                bd.SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), "REV", UserId.ToString(), ScreenType.ToString("G"));
            }
            else
            {
                var detail = _entities.BEXPORT_DOCUMETARYCOLLECTION.Where(x => x.AmendNo == txtCode.Text && x.ActiveRecordFlag == YesNo.YES).FirstOrDefault();
                if (detail != null)
                {
                    detail.Amend_Status = "REV";
                }
                _entities.SaveChanges();
            }
            //// Active control
            //SetDisableByReview(true);

            //// ko cho Authorize/Preview
            ////InitToolBar(false);
            //RadToolBar1.FindItemByValue("btSave").Enabled = true;
            //RadToolBar1.FindItemByValue("btReview").Enabled = false;

            Response.Redirect(Globals.NavigateURL(TabId,"","CodeID=" + txtCode.Text));
        }

        protected void comboCollectionType_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];
        }

        protected void commom_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }


        protected void comboDraweeCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }
        protected void comboDrawerCusNo_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["CustomerID"] = row["CustomerID"].ToString();
            e.Item.Attributes["CustomerName2"] = row["CustomerName2"].ToString();
        }
        protected void comboNostroCusNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            loadNostroDes();
        }
        protected void comboCollectingBankNo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //txtCollectingBankName.Text = comboCollectingBankNo.SelectedItem.Attributes["BankName"];
            bc.Commont.loadBankSwiftCodeInfo(comboCollectingBankNo.Text, ref lblAdviseBankMessage, ref txtCollectingBankName, ref txtCollectingBankAddr3, ref txtCollectingBankAddr1, ref txtCollectingBankAddr2);
            //txtRevivingBank700.Text = txtAdviseBankNo.Text;
            //txtCollectingBankName.Text = tbAdviseBankName.Text;
        }

        protected void commomSwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["SwiftCode"] = row["SwiftCode"].ToString();
            e.Item.Attributes["BankName"] = row["BankName"].ToString();
        }

        protected void comboDrawerCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var drow = bd.DataTam.B_BCUSTOMERS_GetbyID(comboDrawerCusNo.SelectedItem.Text).Tables[0].Rows[0];

            txtDrawerCusName.Text = drow["CustomerName"].ToString();
            txtDrawerAddr1.Text = drow["Address"].ToString();
            txtDrawerAddr2.Text = drow["City"].ToString();
            txtDrawerAddr3.Text = drow["Country"].ToString();

            LoadChargeAcct();
            LoadChargeAcct2();
            LoadChargeAcct3();
        }

        //protected void comboNostroCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
        //}

        //protected void comboCommodity_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    //txtCommodityName.Text = comboCommodity.Text;
        //}

        //protected void comboCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        //{
        //    DataRowView row = e.Item.DataItem as DataRowView;
        //    e.Item.Attributes["ID"] = row["ID"].ToString();
        //    e.Item.Attributes["Name2"] = row["Name2"].ToString();
        //}

        protected void btAddDocsCode_Click(object sender, ImageClickEventArgs e)
        {
            if (!divDocsCode2.Visible)
            {
                divDocsCode2.Visible = true;
            }
            else if (!divDocsCode3.Visible)
            {
                divDocsCode3.Visible = true;
            }
        }

        protected void btRemoveDocsCode2_Click(object sender, ImageClickEventArgs e)
        {
            divDocsCode2.Visible = false;
        }

        protected void btRemoveDocsCode3_Click(object sender, ImageClickEventArgs e)
        {
            divDocsCode3.Visible = false;
        }

        protected void rcbChargeAcct_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblChargeAcct.Text = "TKTT VND " + rcbChargeAcct.SelectedValue.ToString();
        }

        protected void CalcTax()
        {
            double sotien = 0;
            if (rcbPartyCharged.SelectedValue != "AC" && tbChargeAmt.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt.Text = "";
                lblTaxCode.Text = "";
            }
        }

        protected void CalcTax2()
        {
            double sotien = 0;
            if (rcbPartyCharged2.SelectedValue != "AC" && tbChargeAmt2.Value > 0)
            {
                sotien = double.Parse(tbChargeAmt2.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt2.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode2.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt2.Text = "";
                lblTaxCode2.Text = "";
            }
        }

        protected void CalcTax3()
        {
            
            double sotien = 0;
            if (rcbPartyCharged3.SelectedValue != "AC" && tbChargeAmt3.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt3.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt3.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode3.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt3.Text = "";
                lblTaxCode3.Text = "";
            }
        }
        //
        protected void CalcTax4()
        {

            double sotien = 0;
            if (rcbPartyCharged4.SelectedValue != "AC" && tbChargeAmt4.Value > 0)
            {
                sotien = Double.Parse(tbChargeAmt4.Value.ToString());
                sotien = sotien * 0.1;
                lblTaxAmt4.Text = String.Format("{0:C}", sotien).Replace("$", "");
                lblTaxCode4.Text = "81      10% VAT on Charge";
            }
            else
            {
                lblTaxAmt4.Text = "";
                lblTaxCode4.Text = "";
            }
        }

        protected void tbChargeAmt_TextChanged(object sender, EventArgs e)
        {
            CalcTax();
        }

        protected void tbChargeAmt2_TextChanged(object sender, EventArgs e)
        {
            CalcTax2();
        }
        protected void tbChargeAmt3_TextChanged(object sender, EventArgs e)
        {
            CalcTax3();
        }
        protected void tbChargeAmt4_TextChanged(object sender, EventArgs e)
        {
            CalcTax4();
        }


        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged.Text = rcbPartyCharged.SelectedValue;
            //lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
            CalcTax();
        }

        protected void rcbChargeStatus_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus.Text = rcbChargeStatus.SelectedValue;
        }

        protected void rcbChargeAcct2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
           // lblPartyCharged2.Text = rcbPartyCharged2.SelectedValue;
            
        }

        protected void rcbPartyCharged2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
            CalcTax2();
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
            CalcTax3();
        }
        protected void rcbPartyCharged4_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged4.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
            CalcTax4();
        }


        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }

        protected void GenerateVATNo()
        {
            var vatno = bd.Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
            
            tbVatNo.Text = "154"; //hard code fixed VAT no
        }

        protected void GenerateFTCode()
        {
            if (TabId == 226)
            {
                txtCode.Text = bd.SQLData.B_BMACODE_GetNewID("EXPORT_DOCUMETARYCOLLECTION", Refix_BMACODE());
            }
            else
            {
                txtCode.Text = string.Empty;
            }
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

        protected void rcbChargeAcct_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }

        protected void rcbChargeAcct2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeAcct3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void rcbChargeAcct4_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Name"] = row["Name"].ToString();
        }
        protected void LoadNosTroAcc()
        {
            if (!String.IsNullOrEmpty(comboCurrency.SelectedValue) && !String.IsNullOrEmpty(comboNostroCusNo.Text))
            {
                loadNostroDes();
            }
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 83
         */
        protected void LoadChargeAcct()
        {
            rcbChargeAcct.Items.Clear();
            rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct.DataValueField = "Id";
            rcbChargeAcct.DataTextField = "Id";
            rcbChargeAcct.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCusIDAndCurrency(comboDrawerCusNo.SelectedValue, rcbChargeCcy.SelectedValue);
            rcbChargeAcct.DataBind();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 83
         */
        protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCusIDAndCurrency(comboDrawerCusNo.SelectedValue, rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 83
         */
        protected void LoadChargeAcct3()
        {
            rcbChargeAcct3.Items.Clear();
            rcbChargeAcct3.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct3.DataValueField = "Id";
            rcbChargeAcct3.DataTextField = "Id";
            rcbChargeAcct3.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCusIDAndCurrency(comboDrawerCusNo.SelectedValue, rcbChargeCcy3.SelectedValue);
            rcbChargeAcct3.DataBind();
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 83
         */
        protected void LoadChargeAcct4()
        {
            rcbChargeAcct4.Items.Clear();
            rcbChargeAcct4.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct4.DataValueField = "Id";
            rcbChargeAcct4.DataTextField = "Id";
            rcbChargeAcct4.DataSource = bd.SQLData.B_BDRFROMACCOUNT_GetByCusIDAndCurrency(comboDrawerCusNo.SelectedValue, rcbChargeCcy4.SelectedValue);
            rcbChargeAcct4.DataBind();
        }


        protected void comboWaiveCharges_OnSelectedIndexChanged(object sender,
                                                                RadComboBoxSelectedIndexChangedEventArgs e)
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

        protected void btnChargecode2_Click(object sender, ImageClickEventArgs e)
        {
            //divChargeInfo2.Visible = false;

            tbChargeCode2.SelectedValue = string.Empty;
            rcbChargeCcy2.SelectedValue = string.Empty;
            rcbChargeAcct2.SelectedValue = string.Empty;
            tbChargeAmt2.Value = 0;
            rcbPartyCharged2.SelectedValue = string.Empty;
            rcbOmortCharge2.SelectedValue = string.Empty;
            rcbChargeStatus2.SelectedValue = string.Empty;

            lblTaxCode2.Text = string.Empty;
            lblTaxAmt2.Text = "0";
        }

        protected void rcbPartyCharged_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged2_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged3_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void rcbPartyCharged4_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Id"] = row["Id"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }
        protected void LoadDataSourceComboPartyCharged()
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
            //lblPartyCharged4.Text = rcbPartyCharged4.SelectedItem.Attributes["Description"];
        }

        protected void LoadDataSourceComboChargeCcy()
        {
            var dtSource = bd.SQLData.B_BCURRENCY_GetAll();

            rcbChargeCcy.Items.Clear();
            rcbChargeCcy.DataValueField = "Code";
            rcbChargeCcy.DataTextField = "Code";
            rcbChargeCcy.DataSource = dtSource;
            rcbChargeCcy.DataBind();

            rcbChargeCcy2.Items.Clear();
            rcbChargeCcy2.DataValueField = "Code";
            rcbChargeCcy2.DataTextField = "Code";
            rcbChargeCcy2.DataSource = dtSource;
            rcbChargeCcy2.DataBind();

            rcbChargeCcy3.Items.Clear();
            rcbChargeCcy3.DataValueField = "Code";
            rcbChargeCcy3.DataTextField = "Code";
            rcbChargeCcy3.DataSource = dtSource;
            rcbChargeCcy3.DataBind();

            rcbChargeCcy4.Items.Clear();
            rcbChargeCcy4.DataValueField = "Code";
            rcbChargeCcy4.DataTextField = "Code";
            rcbChargeCcy4.DataSource = dtSource;
            rcbChargeCcy4.DataBind();
        }
        protected void rcbCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadNosTroAcc();
        }

        protected void rcbChargeCcy_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct();
        }

        protected void rcbChargeCcy2_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct2();
        }

        protected void rcbChargeCcy3_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct3();
        }
        protected void rcbChargeCcy4_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            LoadChargeAcct4();
        }
        protected void txtDraweeCusNo_OnTextChanged(object sender, EventArgs e)
        {
            CheckSwiftCodeExist();
        }
        protected void CheckSwiftCodeExist()
        {
            lblRemittingBankNoError.Text = "";
            txtDraweeCusName.Text = "";
            if (!string.IsNullOrEmpty(txtDraweeCusNo.Text.Trim()))
            {
                var dtBSWIFTCODE = bd.SQLData.B_BBANKSWIFTCODE_GetByCode(txtDraweeCusNo.Text.Trim());
                if (dtBSWIFTCODE.Rows.Count > 0)
                {
                    txtDraweeCusName.Text = dtBSWIFTCODE.Rows[0]["BankName"].ToString();
                    
                }
                else
                {
                    txtDraweeCusName.Text = string.Empty;
                    lblRemittingBankNoError.Text = "No found swiftcode";
                }
            }

        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Oct 07, 2015    Hien Nguyen       Fix bug 61 _ register and accept not use same VAT
         */
        private void showReport(int reportType)
        {
            string reportTemplate = "~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/";
            string saveName = "";
            DataSet reportData = null;
            Aspose.Words.SaveFormat saveFormat = Aspose.Words.SaveFormat.Doc;
            Aspose.Words.SaveType saveType = Aspose.Words.SaveType.OpenInApplication;
            try
            {
                switch (reportType)
                {
                    case 1://NhapNgoaiBang1
                        reportTemplate = Context.Server.MapPath(reportTemplate + (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG1.doc" : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG1.doc"));
                        saveName = (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG1_" : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG1_") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG1_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 2://NhapNgoaiBang2
                        reportTemplate = Context.Server.MapPath(reportTemplate + (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG2.doc" : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG2.doc"));
                        saveName = (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG2_" : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG2_") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG2_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 3://XuatNgoaiBang1
                        reportTemplate = Context.Server.MapPath(reportTemplate + (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUXUATNGOAIBANG1.doc" : "RegisterDocumentaryCollectionPHIEUXUATNGOAIBANG1.doc"));
                        saveName = (comboCollectionType.SelectedValue.Equals("CC") ? "RegisterDocumentaryCleanCollectionPHIEUXUATNGOAIBANG1_" : "RegisterDocumentaryCollectionPHIEUXUATNGOAIBANG1_") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUXUATNGOAIBANG1_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 4://XuatNgoaiBang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollection_Amend_PHIEUXUATNGOAIBANG.doc");
                        saveName = "RegisterDocumentaryCollection_Amend_PHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUXUATNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 5://NhapNgoaiBang
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollection_Amend_PHIEUNHAPNGOAIBANG.doc");
                        saveName = "RegisterDocumentaryCollection_Amend_PHIEUNHAPNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 6://CancelPHIEUXUATNGOAIBANG
                        reportTemplate = Context.Server.MapPath(reportTemplate + "DocumentaryCollectionCancelPHIEUXUATNGOAIBANG.doc");
                        saveName = "DocumentaryCollectionCancelPHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORTDOCUMETARYCOLLECTION_CANCEL_PHIEUXUATNGOAIBANG_Report(txtCode.Text, UserInfo.Username);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 7://VAT
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterDocumentaryCollectionVAT.doc");
                        saveName = "RegisterDocumentaryCollectionVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORT_DOCUMETARYCOLLECTION_VAT_Report(txtCode.Text, UserInfo.Username, comboCurrency.SelectedValue, TabId);
                        //reportData.Tables[0].TableName = "Table1";
                        break;
                    case 8://COVER
                        reportTemplate = Context.Server.MapPath(reportTemplate + "COVER NHO THU XK.doc");
                        saveName = "COVER NHO THU XK_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORT_DOCUMETARYCOLLECTION_COVER_Report(txtCode.Text, UserInfo.Username);
                        reportData.Tables[0].TableName = "Table1";
                        break;

                    //Fixed bug 61 start
                    case 9://VAT of "register" fucntion
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterVAT.doc");
                        saveName = "RegisterVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORT_DOCUMETARYCOLLECTION_VAT_Register_Report(txtCode.Text, UserInfo.Username, TabId);
                        break;

                    case 10://VAT of "Amend" fucntion
                        reportTemplate = Context.Server.MapPath(reportTemplate + "AmendVAT.doc");
                        saveName = "AmendVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORT_DOCUMETARYCOLLECTION_VAT_Amend_Report(txtCode.Text, UserInfo.Username, TabId);
                        break;

                    case 11://VAT of "Cancel" fucntion
                        reportTemplate = Context.Server.MapPath(reportTemplate + "RegisterVAT.doc");
                        saveName = "CancelVAT_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";
                        reportData = bd.SQLData.P_BEXPORT_DOCUMETARYCOLLECTION_VAT_Cancel_Report(txtCode.Text, UserInfo.Username, TabId);
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
        protected void btnRegisterNhapNgoaiBang1_Click(object sender, EventArgs e)
        {
            showReport(1);
        }
        protected void btnRegisterNhapNgoaiBang2_Click(object sender, EventArgs e)
        {
            showReport(2);
        }
        protected void btnRegisterXuatNgoaiBang1_Click(object sender, EventArgs e)
        {
            showReport(3);
        }
        protected void btnAmendXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport(4);
        }
        protected void btnAmendNhapNgoaiBang_Click(object sender, EventArgs e)
        {
            showReport(5);
        }
        protected void btnCancelPHIEUXUATNGOAIBANG_Click(object sender, EventArgs e)
        {
            showReport(6);
        }
        protected void btnVATReport_Click(object sender, EventArgs e)
        {
            showReport(7);
        }

        protected void btnCOVERReport_Click(object sender, EventArgs e)
        {
            showReport(8);
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            Oct 07, 2015    Hien Nguyen       Fix bug 61 _ register and accept not use same VAT
         */
        protected void btnRegisterVATReport_Click(object sender, EventArgs e)
        {
            showReport(9);
        }

        /*
          * Method Revision History:
          * Version        Date            Author            Comment
          * ----------------------------------------------------------
          * 0.1            Jan 15, 2015    Hien Nguyen       init code
          */
        protected void btnAmendVATReport_Click(object sender, EventArgs e)
        {
            showReport(10);
        }

        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            Jan 15, 2015    Hien Nguyen       init code
         */
        protected void btnCancelVATReport_Click(object sender, EventArgs e)
        {
            showReport(11);
        }
    }
}