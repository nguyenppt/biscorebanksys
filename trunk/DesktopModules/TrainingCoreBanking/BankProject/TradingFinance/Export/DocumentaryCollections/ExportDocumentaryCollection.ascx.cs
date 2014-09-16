using System;
using System.Data;
using System.Web.UI;
using BankProject.DataProvider;
using DotNetNuke.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

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
        public double Amount = 0;
        public double AmountNew = 0;
        public double AmountOld = 0;
        public double AmountAut = 0;
        private DataRow _exportDoc;
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
                    case 362:
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

        private bool Disable
        {
            get { return Request.QueryString["disable"] == "1"; }
        }

        private string Refix_BMACODE()
        {
            return "TF";
        }

        private void LoadExportDoc()
        {
            var dsDoc = SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_GetByDocCollectCode(CodeId);
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
            LoadExportDoc();

            switch (ScreenType)
            {
                case ExportDocumentaryScreenType.RegisterCc:
                case ExportDocumentaryScreenType.Register:

                    InitToolBarForRegister();

                    lblAmount_New.Visible = false;
                  
                    break;
                case ExportDocumentaryScreenType.Amend:
                    InitToolBarForAmend();
                    //tabCharges.Visible = false;
                    //Charges.Visible = false;
                    break;
                case ExportDocumentaryScreenType.Cancel:
                    InitToolBarForCancel();

                    lblAmount_New.Visible = false;
                    divDocumentaryCollectionCancel.Visible = true;
                    break;
                case ExportDocumentaryScreenType.Acception:
                    InitToolBarForAccept();

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
                comboCollectionType.DataSource =
                    SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
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
                comboCollectionType.DataSource =
                    SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_CollectionType");
                comboCollectionType.DataBind();
                
            }
            lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

            // bind drawer
            DataView dv = new DataView(DataTam.B_BCUSTOMERS_GetAll().Tables[0]);

            dv.RowFilter = "CustomerID like '2%'"; 
            comboDrawerCusNo.Items.Clear();
            comboDrawerCusNo.Items.Add(new RadComboBoxItem(""));
            comboDrawerCusNo.DataValueField = "CustomerID";
            comboDrawerCusNo.DataTextField = "CustomerID";
            comboDrawerCusNo.DataSource = dv;
            comboDrawerCusNo.DataBind();

            // bind collecting bank no
            var dsSwiftCode = SQLData.B_BSWIFTCODE_GetAll();
            comboCollectingBankNo.Items.Clear();
            comboCollectingBankNo.Items.Add(new RadComboBoxItem(""));
            comboCollectingBankNo.DataValueField = "Code";
            comboCollectingBankNo.DataTextField = "Code";
            comboCollectingBankNo.DataSource = dsSwiftCode;
            comboCollectingBankNo.DataBind();

            // bind nostro cus no
            comboNostroCusNo.Items.Clear();
            comboNostroCusNo.Items.Add(new RadComboBoxItem(""));
            comboNostroCusNo.DataValueField = "AccountNo";
            comboNostroCusNo.DataTextField = "Code";
            comboNostroCusNo.DataSource = dsSwiftCode;
            comboNostroCusNo.DataBind();

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

            comboDocsCode3.Items.Clear();
            comboDocsCode3.Items.Add(new RadComboBoxItem(""));
            comboDocsCode3.DataValueField = "Id";
            comboDocsCode3.DataTextField = "Description";
            comboDocsCode3.DataSource = SQLData.CreateGenerateDatas("DocumetaryCollection_TabMain_DocsCode");
            comboDocsCode3.DataBind();

            comboCommodity.Items.Clear();
            comboCommodity.Items.Add(new RadComboBoxItem(""));
            comboCommodity.DataValueField = "ID";
            comboCommodity.DataTextField = "ID";
            comboCommodity.DataSource = DataTam.B_BCOMMODITY_GetAll();
            comboCommodity.DataBind();

            tbChargeCode.SelectedValue = "EC.RECEIVE";
            tbChargeCode.Enabled = false;
            tbChargeCode2.SelectedValue = "EC.COURIER";
            tbChargeCode2.Enabled = false;
            tbChargeCode3.SelectedValue = "EC.OTHER";
            tbChargeCode3.Enabled = false;


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
            var datasource = SQLData.B_BCHARGECODE_GetByViewType(226);

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
                    SaveData();
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
            if (_exportDoc != null)
            {
                if (Disable) // Authorizing
                {
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                    }
                    else if (_exportDoc["Amend_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                    }
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
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
                    if (_exportDoc["Status"].ToString() != "AUT") // Authorized
                    {
                        lblError.Text = "This Documentary was not authorized";
                        SetDisableByReview(false);
                    }
                    else if (_exportDoc["Cancel_Status"].ToString() == "AUT")
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Documentary was canceled";
                    }
                    else if (_exportDoc["Amend_Status"].ToString() == "AUT") // Authorized
                    {
                        RadToolBar1.FindItemByValue("btPrint").Enabled = true;
                        lblError.Text = "This Amend Documentary was authorized";
                        SetDisableByReview(false);
                    }
                    else // Not yet authorize
                    {
                        RadToolBar1.FindItemByValue("btSave").Enabled = true;
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
                    }
                    
                }
            }
            else // Creating
            {
                RadToolBar1.FindItemByValue("btSave").Enabled = true;
                txtCode.Text = SQLData.B_BMACODE_GetNewID("EXPORT_DOCUMETARYCOLLECTION", Refix_BMACODE());
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
                    }
                }

            }
            else
            {

            }
        }
        protected void SaveData()
        {
            SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_Insert(txtCode.Text.Trim()
                                                          , comboDrawerCusNo.SelectedValue
                                                          , txtDrawerCusName.Text.Trim()
                                                          , txtDrawerAddr1.Text.Trim()
                                                          , txtDrawerAddr2.Text.Trim()
                                                          , txtDrawerAddr3.Text.Trim()
                                                          , txtDrawerRefNo.Text.Trim()
                                                          , comboCollectingBankNo.SelectedValue
                                                          , txtCollectingBankName.Text.Trim()
                                                          , txtCollectingBankAddr1.Text.Trim(),
                                                          txtCollectingBankAddr2.Text.Trim()
                                                          , ""
                                                          , comboCollectingBankAcct.SelectedValue
                                                          , txtDraweeCusNo.Text
                                                          , txtDraweeCusName.Text.Trim()
                                                          , txtDraweeAddr1.Text.Trim()
                                                          , txtDraweeAddr2.Text.Trim()
                                                          , txtDraweeAddr3.Text.Trim()
                                                          , comboNostroCusNo.SelectedValue
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
                                                          , UserId.ToString()
                                                          , comboCollectionType.SelectedValue
                                                          , dteCancelDate.SelectedDate.ToString()
                                                          , dteContingentExpiryDate.SelectedDate.ToString()
                                                          , txtCancelRemark.Text
                                                          , dtAcceptDate.SelectedDate.ToString()
                                                          , txtAcceptREmark.Text
                                                          , ScreenType.ToString("G")
                );

            if (!string.IsNullOrWhiteSpace(tbChargeAmt.Text))
            {
                SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                    comboWaiveCharges.SelectedValue, tbChargeCode.SelectedValue, rcbChargeAcct.SelectedValue, ""
                    /*tbChargePeriod.Text*/,
                    rcbChargeCcy.SelectedValue, "0" /*tbExcheRate.Text*/, tbChargeAmt.Text,
                    rcbPartyCharged.SelectedValue, rcbOmortCharge.SelectedValue, "", "",
                    rcbChargeStatus.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode.Text, ""
                    /*lblTaxCcy.Text*/, lblTaxAmt.Text, "", "", "1");
            }
            if (!string.IsNullOrWhiteSpace(tbChargeAmt2.Text))
            {
                SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                    comboWaiveCharges.SelectedValue, tbChargeCode2.SelectedValue, rcbChargeAcct2.SelectedValue, ""
                    /*tbChargePeriod2.Text*/,
                    rcbChargeCcy2.SelectedValue, "0" /*tbExcheRate2.Text*/, tbChargeAmt2.Text,
                    rcbPartyCharged2.SelectedValue, rcbOmortCharge2.SelectedValue, "", "",
                    rcbChargeStatus2.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode2.Text, ""
                    /*lblTaxCcy2.Text*/, lblTaxAmt2.Text, "", "", "2");
            }
            if (!string.IsNullOrWhiteSpace(tbChargeAmt3.Text))
            {
                SQLData.B_BEXPORT_DOCUMETARYCOLLECTIONCHARGES_Insert(txtCode.Text.Trim(),
                    comboWaiveCharges.SelectedValue, tbChargeCode3.SelectedValue, rcbChargeAcct3.SelectedValue, ""
                    /*tbChargePeriod3.Text*/,
                    rcbChargeCcy3.SelectedValue, "0" /*tbExcheRate2.Text*/, tbChargeAmt3.Text,
                    rcbPartyCharged3.SelectedValue, rcbOmortCharge3.SelectedValue, "", "",
                    rcbChargeStatus3.SelectedValue, tbChargeRemarks.Text, tbVatNo.Text, lblTaxCode3.Text, ""
                    /*lblTaxCcy2.Text*/, lblTaxAmt3.Text, "", "", "3");
            }
        }

        protected void LoadData(DataSet dsDoc)
        {
          
            if (dsDoc.Tables[0].Rows.Count > 0)
            {

                

                RadToolBar1.FindItemByValue("btReview").Enabled = false;

                var drow = dsDoc.Tables[0].Rows[0];

                if (drow["Amend_Status"].ToString() == "AUT")
                {
                    Amount = double.Parse(drow["Amount"].ToString());
                    AmountOld = double.Parse(drow["AmountOld"].ToString());
                }
                else
                {
                    Amount = double.Parse(drow["AmountNew"].ToString());
                    AmountOld = double.Parse(drow["Amount"].ToString());
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

                if(string.IsNullOrEmpty(drow["Cancel_Status"].ToString()))
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
                    comboCollectionType.DataSource =
                        SQLData.CreateGenerateDatas("DocumetaryCleanCollection_TabMain_CollectionType");
                    comboCollectionType.DataBind();
                    divCollectionType.Visible = false;
                    divDocsCode.Visible = false;
                    divDocsCode2.Visible = false;
                    divDocsCode3.Visible = false;

                }
                // end cc
                comboCollectionType.SelectedValue = drow["CollectionType"].ToString();
                lblCollectionTypeName.Text = comboCollectionType.SelectedItem.Attributes["Description"];

                comboDrawerCusNo.SelectedValue = drow["DrawerCusNo"].ToString();
                txtDrawerCusName.Text = drow["DrawerCusName"].ToString();
                txtDrawerAddr1.Text = drow["DrawerAddr1"].ToString();
                txtDrawerAddr2.Text = drow["DrawerAddr2"].ToString();
                txtDrawerAddr3.Text = drow["DrawerAddr3"].ToString();
                txtDrawerRefNo.Text = drow["DrawerRefNo"].ToString();
                comboCollectingBankNo.SelectedValue = drow["CollectingBankNo"].ToString();
                txtCollectingBankName.Text = drow["CollectingBankName"].ToString();
                txtCollectingBankAddr1.Text = drow["CollectingBankAddr1"].ToString();
                txtCollectingBankAddr2.Text = drow["CollectingBankAddr2"].ToString();
                comboCollectingBankAcct.SelectedValue = drow["CollectingBankAcct"].ToString();
                txtDraweeCusNo.Text = drow["DraweeCusNo"].ToString();
                txtDraweeCusName.Text = drow["DraweeCusName"].ToString();
                txtDraweeAddr1.Text = drow["DraweeAddr1"].ToString();
                txtDraweeAddr2.Text = drow["DraweeAddr2"].ToString();
                txtDraweeAddr3.Text = drow["DraweeAddr3"].ToString();
                comboNostroCusNo.SelectedValue = drow["NostroCusNo"].ToString();
                lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
                comboCurrency.SelectedValue = drow["Currency"].ToString();
                numAmount.Value = Amount;
                lblAmount_New.Text = AmountOld.ToString("C");
                txtTenor.Text = drow["Tenor"].ToString();
                numReminderDays.Text = drow["ReminderDays"].ToString();

                comboCommodity.SelectedValue = drow["Commodity"].ToString();
                txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name2"];

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
                
                if (drow["DocsReceivedDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteDocsReceivedDate.SelectedDate = DateTime.Parse(drow["DocsReceivedDate"].ToString());
                }
                if (drow["MaturityDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteMaturityDate.SelectedDate = DateTime.Parse(drow["MaturityDate"].ToString());
                }
                if (drow["TracerDate"].ToString().IndexOf("1/1/1900") == -1)
                {
                    dteTracerDate.SelectedDate = DateTime.Parse(drow["TracerDate"].ToString());
                }
            }
            else
            {
                comboCollectionType.SelectedValue = string.Empty;
                lblCollectionTypeName.Text = string.Empty;

                
                comboNostroCusNo.SelectedValue = string.Empty;
                txtDrawerCusName.Text = string.Empty;
                txtDrawerAddr1.Text = string.Empty;
                txtDrawerAddr2.Text = string.Empty;
                txtDrawerAddr3.Text = string.Empty;
                txtDrawerRefNo.Text = string.Empty;
                comboCollectingBankNo.SelectedValue = string.Empty;
                txtCollectingBankName.Text = string.Empty;
                txtCollectingBankAddr1.Text = string.Empty;
                txtCollectingBankAddr2.Text = string.Empty;
                comboCollectingBankAcct.SelectedValue = string.Empty;
                txtDraweeCusName.Text = string.Empty;
                txtDraweeAddr1.Text = string.Empty;
                txtDraweeAddr2.Text = string.Empty;
                txtDraweeAddr3.Text = string.Empty;
                comboNostroCusNo.SelectedValue = string.Empty;
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
                lblPartyCharged.Text = drow1["PartyCharged"].ToString();
                rcbOmortCharge.SelectedValue = drow1["OmortCharges"].ToString();
                rcbChargeStatus.SelectedValue = drow1["ChargeStatus"].ToString();
                lblChargeStatus.Text = drow1["ChargeStatus"].ToString();

                tbChargeRemarks.Text = drow1["ChargeRemarks"].ToString();
                tbVatNo.Text = drow1["VATNo"].ToString();
                lblTaxCode.Text = drow1["TaxCode"].ToString();
                //lblTaxCcy.Text = drow1["TaxCcy"].ToString();
                lblTaxAmt.Text = drow1["TaxAmt"].ToString();

                tbChargeCode.SelectedValue = drow1["Chargecode"].ToString();
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
                lblPartyCharged.Text = string.Empty;
                rcbOmortCharge.SelectedValue = string.Empty;
                rcbChargeStatus.SelectedValue = string.Empty;
                lblChargeStatus.Text = string.Empty;

                tbChargeRemarks.Text = string.Empty;
                tbVatNo.Text = string.Empty;
                lblTaxCode.Text = string.Empty;
                //lblTaxCcy.Text = string.Empty;
                lblTaxAmt.Text = string.Empty;

                tbChargeCode.SelectedValue = string.Empty;

                //lblChargeAcct.Text = string.Empty;
                lblPartyCharged.Text = string.Empty;
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
                lblPartyCharged2.Text = drow2["PartyCharged"].ToString();
                rcbChargeStatus2.SelectedValue = drow2["ChargeStatus"].ToString();
                lblChargeStatus2.Text = drow2["ChargeStatus"].ToString();

                lblTaxCode2.Text = drow2["TaxCode"].ToString();
                lblTaxAmt2.Text = drow2["TaxAmt"].ToString();

                tbChargeCode2.SelectedValue = drow2["Chargecode"].ToString();
            }
            else
            {
                rcbChargeAcct2.SelectedValue = string.Empty;
                rcbChargeCcy2.SelectedValue = string.Empty;
                tbChargeAmt2.Text = string.Empty;
                rcbPartyCharged2.SelectedValue = string.Empty;
                lblPartyCharged2.Text = string.Empty;
                rcbChargeStatus2.SelectedValue = string.Empty;
                lblChargeStatus2.Text = string.Empty;

                lblTaxCode2.Text = string.Empty;
                lblTaxAmt2.Text = string.Empty;

                tbChargeCode2.SelectedValue = string.Empty;

                //lblChargeAcct2.Text = string.Empty;
                lblPartyCharged2.Text = string.Empty;
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
                lblPartyCharged3.Text = drow3["PartyCharged"].ToString();
                rcbChargeStatus3.SelectedValue = drow3["ChargeStatus"].ToString();
                //lblChargeStatus3.Text = drow3["ChargeStatus"].ToString();

                lblTaxCode3.Text = drow3["TaxCode"].ToString();
                lblTaxAmt3.Text = drow3["TaxAmt"].ToString();

                tbChargeCode3.SelectedValue = drow3["Chargecode"].ToString();
            }
            else
            {
                rcbChargeAcct3.SelectedValue = string.Empty;
                rcbChargeCcy3.SelectedValue = string.Empty;
                tbChargeAmt3.Text = string.Empty;
                rcbPartyCharged3.SelectedValue = string.Empty;
                lblPartyCharged3.Text = string.Empty;
                rcbChargeStatus3.SelectedValue = string.Empty;
                //lblChargeStatus3.Text = string.Empty;

                lblTaxCode3.Text = string.Empty;
                lblTaxAmt3.Text = string.Empty;

                tbChargeCode3.SelectedValue = string.Empty;

                //lblChargeAcct3.Text = string.Empty;
                lblPartyCharged3.Text = string.Empty;
                //lblChargeStatus3.Text = string.Empty;
            }
            #endregion

            SetVisibilityByStatus(dsDoc);
        }

        protected void Authorize()
        {
            SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), "AUT", UserId.ToString(),ScreenType.ToString("G"));

            Response.Redirect(Globals.NavigateURL(TabId));
        }

        protected void Revert()
        {
            SQLData.B_BEXPORT_DOCUMETARYCOLLECTION_UpdateStatus(txtCode.Text.Trim(), "REV", UserId.ToString(),ScreenType.ToString("G"));

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
        protected void comboCollectingBankNo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtCollectingBankName.Text = comboCollectingBankNo.SelectedItem.Attributes["Description"];
        }

        protected void commomSwiftCode_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            var row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["Code"] = row["Code"].ToString();
            e.Item.Attributes["Description"] = row["Description"].ToString();
        }

        protected void comboDrawerCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var drow = DataTam.B_BCUSTOMERS_GetbyID(comboDrawerCusNo.SelectedItem.Text).Tables[0].Rows[0];

            txtDrawerCusName.Text = drow["CustomerName"].ToString();
            txtDrawerAddr1.Text = drow["Address"].ToString();
            txtDrawerAddr2.Text = drow["City"].ToString();
            txtDrawerAddr3.Text = drow["Country"].ToString();

            LoadChargeAcct();
            LoadChargeAcct2();
            LoadChargeAcct3();
        }

        protected void comboNostroCusNo_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblNostroCusName.Text = comboNostroCusNo.SelectedItem.Attributes["Description"];
        }

        protected void comboCommodity_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            txtCommodityName.Text = comboCommodity.SelectedItem.Attributes["Name2"];
        }

        protected void comboCommodity_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            e.Item.Attributes["ID"] = row["ID"].ToString();
            e.Item.Attributes["Name2"] = row["Name2"].ToString();
        }

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



        protected void rcbPartyCharged_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //lblPartyCharged.Text = rcbPartyCharged.SelectedValue;
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];
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
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];
            CalcTax2();
        }
        protected void rcbPartyCharged3_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
            CalcTax3();
        }

        protected void rcbChargeStatus2_SelectIndexChange(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            lblChargeStatus2.Text = rcbChargeStatus2.SelectedValue.ToString();
        }

        protected void GenerateVATNo()
        {
            var vatno = Database.B_BMACODE_GetNewSoTT("VATNO");
            tbVatNo.Text = vatno.Tables[0].Rows[0]["SoTT"].ToString();
        }

        protected void GenerateFTCode()
        {
            if (TabId == 226)
            {
                txtCode.Text = SQLData.B_BMACODE_GetNewID("EXPORT_DOCUMETARYCOLLECTION", Refix_BMACODE());
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
        protected void LoadChargeAcct()
        {
            rcbChargeAcct.Items.Clear();
            rcbChargeAcct.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct.DataValueField = "Id";
            rcbChargeAcct.DataTextField = "Id";
            rcbChargeAcct.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy.SelectedValue);
            rcbChargeAcct.DataBind();
        }

        protected void LoadChargeAcct2()
        {
            rcbChargeAcct2.Items.Clear();
            rcbChargeAcct2.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct2.DataValueField = "Id";
            rcbChargeAcct2.DataTextField = "Id";
            rcbChargeAcct2.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy2.SelectedValue);
            rcbChargeAcct2.DataBind();
        }

        protected void LoadChargeAcct3()
        {
            rcbChargeAcct3.Items.Clear();
            rcbChargeAcct3.Items.Add(new RadComboBoxItem(""));
            rcbChargeAcct3.DataValueField = "Id";
            rcbChargeAcct3.DataTextField = "Id";
            rcbChargeAcct3.DataSource = SQLData.B_BDRFROMACCOUNT_GetByCurrency(comboDrawerCusNo.SelectedItem != null ? comboDrawerCusNo.SelectedItem.Attributes["CustomerName2"] : "XXXXX", rcbChargeCcy3.SelectedValue);
            rcbChargeAcct3.DataBind();
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
        protected void LoadDataSourceComboPartyCharged()
        {
            var dtSource = SQLData.CreateGenerateDatas("PartyCharged");

            rcbPartyCharged.Items.Clear();
            rcbPartyCharged.DataValueField = "Id";
            rcbPartyCharged.DataTextField = "Id";
            rcbPartyCharged.DataSource = dtSource;
            rcbPartyCharged.DataBind();
            lblPartyCharged.Text = rcbPartyCharged.SelectedItem.Attributes["Description"];

            rcbPartyCharged2.Items.Clear();
            rcbPartyCharged2.DataValueField = "Id";
            rcbPartyCharged2.DataTextField = "Id";
            rcbPartyCharged2.DataSource = dtSource;
            rcbPartyCharged2.DataBind();
            lblPartyCharged2.Text = rcbPartyCharged2.SelectedItem.Attributes["Description"];

            rcbPartyCharged3.Items.Clear();
            rcbPartyCharged3.DataValueField = "Id";
            rcbPartyCharged3.DataTextField = "Id";
            rcbPartyCharged3.DataSource = dtSource;
            rcbPartyCharged3.DataBind();
            lblPartyCharged3.Text = rcbPartyCharged3.SelectedItem.Attributes["Description"];
        }

        protected void LoadDataSourceComboChargeCcy()
        {
            var dtSource = SQLData.B_BCURRENCY_GetAll();

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
                var dtBSWIFTCODE = SQLData.B_BBANKSWIFTCODE_GetByCode(txtDraweeCusNo.Text.Trim());
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
        protected void btnRegisterNhapNgoaiBang1_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG1.doc");
            if (comboCollectionType.SelectedValue == "CC")
            {
                path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG1.doc");
            }
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG1_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            var filename = (comboCollectionType.SelectedValue == "CC"
                ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG1_"
                : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG1_") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                           ".doc";
            doc.Save(filename, Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnRegisterNhapNgoaiBang2_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG2.doc");
            if (comboCollectionType.SelectedValue == "CC")
            {
                path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG2.doc");
            }
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUNHAPNGOAIBANG2_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            var filename = (comboCollectionType.SelectedValue == "CC"
                ? "RegisterDocumentaryCleanCollectionPHIEUNHAPNGOAIBANG2_"
                : "RegisterDocumentaryCollectionPHIEUNHAPNGOAIBANG2_") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                           ".doc";
            doc.Save(filename, Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnRegisterXuatNgoaiBang1_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCollectionPHIEUXUATNGOAIBANG1.doc");
            if (comboCollectionType.SelectedValue == "CC")
            {
                path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCleanCollectionPHIEUXUATNGOAIBANG1.doc");
            }
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_PHIEUXUATNGOAIBANG1_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            var filename = (comboCollectionType.SelectedValue == "CC"
                ? "RegisterDocumentaryCleanCollectionPHIEUXUATNGOAIBANG1_"
                : "RegisterDocumentaryCollectionPHIEUXUATNGOAIBANG1_") + DateTime.Now.ToString("yyyyMMddHHmmss") +
                           ".doc";
            doc.Save(filename, Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }

        protected void btnAmendXuatNgoaiBang_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCollection_Amend_PHIEUXUATNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUXUATNGOAIBANG_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("RegisterDocumentaryCollectionPHIEUXUATNGOAIBANG1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnAmendNhapNgoaiBang_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/RegisterDocumentaryCollection_Amend_PHIEUNHAPNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_AMEND_PHIEUNHAPNGOAIBANG_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("AmendDocumentaryCollectionPHIEUNHAPNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
        protected void btnCancelPHIEUXUATNGOAIBANG_Click(object sender, EventArgs e)
        {
            Aspose.Words.License license = new Aspose.Words.License();
            license.SetLicense("Aspose.Words.lic");

            //Open template
            string path = Context.Server.MapPath("~/DesktopModules/TrainingCoreBanking/BankProject/Report/Template/DocumentaryCollection/Export/DocumentaryCollectionCancelPHIEUXUATNGOAIBANG.doc");
            //Open the template document
            Aspose.Words.Document doc = new Aspose.Words.Document(path);
            //Execute the mail merge.
            DataSet ds = new DataSet();
            ds = SQLData.P_BEXPORTDOCUMETARYCOLLECTION_CANCEL_PHIEUXUATNGOAIBANG_Report(txtCode.Text, UserInfo.Username);

            // Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(ds); //moas mat thoi jan voi cuc gach nay woa 
            // Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.
            doc.Save("ExportDocumentaryCollectionCancelPHIEUXUATNGOAIBANG_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc", Aspose.Words.SaveFormat.Doc, Aspose.Words.SaveType.OpenInBrowser, Response);
        }
    }
}