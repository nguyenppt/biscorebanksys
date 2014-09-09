using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using BankProject.DataProvider;

namespace BankProject.Views.TellerApplication
{
    
    public partial class InputCollateralRight : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        public static int AutoID = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            FirstLoad();
            if (Request.QueryString["RightID"] != null)
            {
                LoadToolBar_AllFalse();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadInfo_forRightID(Request.QueryString["RightID"].ToString());
            }
        }
       
        protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            var toolBarButton = e.Item as RadToolBarButton;
            var commandName = toolBarButton.CommandName;
            if (commandName == "commit")
            {
                string CollID = tbCollateralRightID.Text.Trim();
                if (CollID.Length == 10) // kiem tra Id dc nhap
                {
                    string RightNo = CollID.Substring(8, 2);
                    string CustomerID = CollID.Substring(0, 7);
                    TriTT.B_CUSTOMER_RIGHT_Insert_Update(CollID, RightNo, CustomerID, lblCustomerName.Text, rcbLimitID.SelectedItem.Attributes["MainLimitID"]
                        , rcbLimitID.SelectedValue, rcbCollateralType.SelectedValue, rcbCollateralType.Text.Replace(rcbCollateralType.SelectedValue + " - ", "")
                        , rcbCollateralCode.SelectedValue, rcbCollateralCode.Text.Replace(rcbCollateralCode.SelectedValue + " - ", ""), RdpValidityDate.SelectedDate
                        , RdpExpiryDate.SelectedDate, tbNotes.Text, UserInfo.Username.ToString());
                    Response.Redirect("Default.aspx?tabid=193");
                }
                else { ShowMsgBox("This Collateral Right ID is Incorrect Format !"); return; }
            }
            if (commandName == "search")
            {
                LoadInfo_forRightID(tbCollateralRightID.Text.Trim());
            }
            if (commandName == "edit")
            {
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, true);
                RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
                RadToolBar1.FindItemByValue("btEdit").Enabled = false;
            }
        }
        protected void LoadInfo_forRightID(string collateralID)
        {
            BankProject.Controls.Commont.SetEmptyFormControls(this.Controls);
            FirstLoad(); // refresh form cu, de nhap du lieu moi
            tbCollateralRightID.Text = collateralID;
            var CustomerID = collateralID.Substring(0, 7);
            LoadSubLimitID(CustomerID); // do du lieu sublimitID vao combobox
            
            if (TriTT.B_CUSTOMER_LIMIT_LoadCustomerName(CustomerID) != null)
            {
                lblCheckCustomerName.Text = "";
                lblCustomerName.Text = TriTT.B_CUSTOMER_LIMIT_LoadCustomerName(CustomerID);
            }
            else
            {
                lblCheckCustomerName.Text = "Customer does not exist !"; return;
            }
            // kiem tra khach hang da dc cap dinh muc chua !
            if (rcbLimitID.Items.Count == 0) { ShowMsgBox("You  have not been delivered Limit ID, Please create Limit for your Account !"); return; }
            DataSet ds = TriTT.B_CUSTOMER_RIGHT_Load_RightID(collateralID);
            if ( ds.Tables != null && ds.Tables[0].Rows.Count >0 ) // kiem tra xem RIghtID da dc tao chua, co roi thi load len lai va` disable form
            {
                lblCustomerName.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                rcbLimitID.SelectedValue = ds.Tables[0].Rows[0]["SubLimitID"].ToString();
                LoadCollateralTyep(rcbLimitID.SelectedValue);
                rcbCollateralType.SelectedValue = ds.Tables[0].Rows[0]["CollateralTypeCode"].ToString();
                rcbCollateralCode.SelectedValue = ds.Tables[0].Rows[0]["CollateralCode"].ToString();
                if(ds.Tables[0].Rows[0]["ValidityDate"] != null )
                {
                    RdpValidityDate.SelectedDate = DateTime.Parse( ds.Tables[0].Rows[0]["ValidityDate"].ToString() );
                }
                if (ds.Tables[0].Rows[0]["ExpiryDate"].ToString() != "")
                {
                    RdpExpiryDate.SelectedDate = DateTime.Parse(ds.Tables[0].Rows[0]["ExpiryDate"].ToString());
                }
                tbNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                BankProject.Controls.Commont.SetTatusFormControls(this.Controls, false);
                LoadToolBar_AllFalse();
            }
        }
        protected void btSearch_Click1(object sender, EventArgs e)
        {
            if (tbCollateralRightID.Text !="") // kiem tra cho search khi ID khac null 
            {
                if (tbCollateralRightID.Text.Trim().Length ==10)
                {
                    LoadInfo_forRightID(tbCollateralRightID.Text.Trim());
                }
                else { 
                    ShowMsgBox("Collateral Right ID is incorrect, Please check again !");
                    FirstLoad();
                    return; }
            }
            else { ShowMsgBox("CustomerID must be not null !"); FirstLoad(); return; }
        }
        #region properties
        protected void FirstLoad()
        {
            LoadToolBar();
            RdpValidityDate.SelectedDate = DateTime.Now;
            tbNotes.Text = "NHAP MA LOAI HINH TAI SAN DAM BAO";
            rcbCollateralCode.Items.Clear(); rcbCollateralType.Items.Clear(); rcbLimitID.Items.Clear();
            rcbCollateralCode.Text = rcbCollateralType.Text =rcbLimitID.Text ="";
        }
        protected void LoadToolBar_AllFalse()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = true;
        }
        private void LoadToolBar()
        {
            RadToolBar1.FindItemByValue("btCommitData").Enabled = true;
            RadToolBar1.FindItemByValue("btPrint").Enabled = false;
            RadToolBar1.FindItemByValue("btPreview").Enabled = false;
            RadToolBar1.FindItemByValue("btAuthorize").Enabled = false;
            RadToolBar1.FindItemByValue("btReverse").Enabled = false;
            RadToolBar1.FindItemByValue("btSearch").Enabled = false;
            RadToolBar1.FindItemByValue("btEdit").Enabled = false;
        }
        protected void LoadSubLimitID(string CustomerID)
        {
            rcbLimitID.Items.Clear();
            DataSet ds = TriTT.B_CUSTOMER_RIGHT_Load_SubLimitID(CustomerID);
            if(ds.Tables != null && ds.Tables[0].Rows.Count>0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["SubLimitID"]="";
                ds.Tables[0].Rows.InsertAt(dr,0);
            }
            rcbLimitID.DataSource = ds;
            rcbLimitID.DataValueField = "SubLimitID";
            rcbLimitID.DataTextField = "SubLimitID";
            rcbLimitID.DataBind();
        }
        protected void rcbLimitID_OnItemDataBound(object sender, RadComboBoxItemEventArgs e)
        {
            DataRowView data = e.Item.DataItem as DataRowView;
            e.Item.Attributes["MainLimitID"] = data["MainLimitID"].ToString();
        }
        protected void LoadCollateralTyep(string CollID)
        {
            rcbCollateralType.Items.Clear();
            rcbCollateralType.Text = "";
            DataSet ds = TriTT.B_CUSTOMER_RIGHT_Load_CollateralTYpe(CollID);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CollateralTypeCode"] = "";
                dr["CollateralTypeName"] = "";
                dr["CollateralCode"] = "";
                dr["CollateralName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCollateralType.DataSource = ds;
            rcbCollateralType.DataValueField = "CollateralTypeCode";
            rcbCollateralType.DataTextField = "CollateralTypeName";
            rcbCollateralType.DataBind();

            rcbCollateralCode.Items.Clear();
            rcbCollateralCode.Text = "";
            rcbCollateralCode.DataSource = ds;
            rcbCollateralCode.DataValueField = "CollateralCode";
            rcbCollateralCode.DataTextField = "CollateralName";
            rcbCollateralCode.DataBind();
        }
        protected void rcbLimitID_LoadFor_COllateral(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            rcbCollateralType.Items.Clear();
            rcbCollateralType.Text = "";
            DataSet ds = TriTT.B_CUSTOMER_RIGHT_Load_CollateralTYpe(rcbLimitID.SelectedValue);
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr["CollateralTypeCode"] = "";
                dr["CollateralTypeName"] = "";
                dr["CollateralCode"] = "";
                dr["CollateralName"] = "";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            rcbCollateralType.DataSource = ds;
            rcbCollateralType.DataValueField = "CollateralTypeCode";
            rcbCollateralType.DataTextField = "CollateralTypeName";
            rcbCollateralType.DataBind();

            rcbCollateralCode.Items.Clear();
            rcbCollateralCode.Text = "";
            rcbCollateralCode.DataSource = ds;
            rcbCollateralCode.DataValueField = "CollateralCode";
            rcbCollateralCode.DataTextField = "CollateralName";
            rcbCollateralCode.DataBind();
        }
        protected void ShowMsgBox(string contents, int width = 420, int hiegth = 150)
        {
            string radalertscript =
                "<script language='javascript'>function f(){radalert('" + contents + "', " + width + ", '" + hiegth +
                "', 'Warning'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
        }
        #endregion
    }
}