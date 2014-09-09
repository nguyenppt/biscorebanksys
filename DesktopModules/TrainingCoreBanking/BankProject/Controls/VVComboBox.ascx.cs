using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Controls
{
    public partial class VVComboBox : System.Web.UI.UserControl
    {
        DataSet ds;
        DataSet _DataSource;
        string _VVTLabel, _VVTText, _VVCSelecttedValue, _SourceTable, _VVTDataKey, _LBwidth = "150";
        string _width="200";
        bool _Enabled = true;
        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        public string LBWidth
        {
            get { return _LBwidth; }
            set { _LBwidth = value; }
        }
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public string SourceTable
        {
            get { return _SourceTable; }
            set { _SourceTable = value; }
        }
        public string VVTLabel
        {
            get { return _VVTLabel; }
            set { _VVTLabel = value; }
        }
        public string VVTText
        {
            get { return _VVTText; }
            set { _VVTText = value; }
        }
        public string VVCSelecttedValue
        {
            get { return _VVCSelecttedValue; }
            set { _VVCSelecttedValue = value; }
        }
        public string VVTDataKey
        {
            get { return _VVTDataKey; }
            set { _VVTDataKey = value; }
        }
        public DataSet DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            
        }
        public void SetEnable(bool menable)
        {
            Enabled = menable;
            try
            {
                for (int i = 0; tblMain.Rows.Count > 0; i++)
                {
                    TableRow rw = tblMain.Rows[0];
                    tblMain.Rows.Remove(rw);
                }
            }
            catch { }
            LoadControls();
        }

        protected override void OnInit(EventArgs e)
        {
            
            base.OnInit(e);
            if (!IsPostBack)
            {
                if (Request.QueryString["CodeID"] != null)
                    Session["DataKey"] = Request.QueryString["CodeID"].ToString();
                else
                    Session["DataKey"] = "";
            }
            else
            {

                DataProvider.KhanhND.B_BDYNAMICCONTROLS_UpdateByTab(Request.QueryString["tabid"].ToString(), "", Session["DataKey"].ToString(), "");
                DataProvider.KhanhND.B_BDYNAMICCONTROLS_DelByTab(Request.QueryString["tabid"].ToString(), this.ID.ToString(), Session["DataKey"].ToString());
            }
            DataSource = DataProvider.KhanhND.B_BCUSTOMERS_GetAllForVVC(SourceTable);
            LoadControls();

        }

        private void LoadControls()
        {
            ds = BankProject.DataProvider.KhanhND.B_BDYNAMICCONTROLS_GetControls(Request.QueryString["tabid"].ToString(), this.ID.ToString(), Session["DataKey"].ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i > 0)
                {
                    Label lb = new Label();
                    lb.ID = "lbl" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    lb.Text = VVTLabel;


                    RadComboBox rcb = new RadComboBox();
                    rcb.ID = "rcb" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    rcb.AutoPostBack = true;
                    rcb.Items.Add(new RadComboBoxItem(""));
                    rcb.SelectedIndexChanged += rcb_SelectedIndexChanged;
                    rcb.AppendDataBoundItems = true;
                    rcb.Width = Unit.Parse(Width);
                    rcb.DataTextField = "TextField";
                    rcb.DataValueField = "ValueField";
                    if (DataSource != null)
                    {
                        rcb.DataSource = DataSource;
                        rcb.DataBind();
                        rcb.SelectedValue = ds.Tables[0].Rows[i]["Datavalue"].ToString();
                    }

                    ImageButton ibt = new ImageButton();
                    ibt.ID = "ibt" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    ibt.ImageUrl = "~/Icons/Sigma/Delete_16X16_Standard.png";
                    ibt.Click += ibt_Click;

                    TableRow tr = new TableRow();
                    tr.ID = ds.Tables[0].Rows[i]["DataControlID"].ToString();

                    TableCell tc = new TableCell();
                    tc.CssClass = "MyLable";
                    tc.Controls.Add(lb);
                    tc.Width = Unit.Parse(LBWidth);

                    TableCell tc2 = new TableCell();
                    tc2.CssClass = "MyContent";
                    tc2.Width = Unit.Parse(Width);
                    tc2.Controls.Add(rcb);

                    TableCell tc3 = new TableCell();
                    tc3.Controls.Add(ibt);

                    tr.Cells.Add(tc);
                    tr.Cells.Add(tc2);
                    tr.Cells.Add(tc3);
                    tblMain.Rows.Add(tr);
                }
                else
                {
                    Label lb = new Label();
                    lb.ID = "lbl" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    lb.Text = VVTLabel;


                    RadComboBox rcb = new RadComboBox();
                    rcb.ID = "rcb" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    rcb.AutoPostBack = true;
                    rcb.Items.Add(new RadComboBoxItem(""));
                    rcb.SelectedIndexChanged += rcb_SelectedIndexChanged;
                    rcb.AppendDataBoundItems = true;
                    rcb.Width = Unit.Parse(Width);
                    rcb.DataTextField = "TextField";
                    rcb.DataValueField = "ValueField";
                    if (DataSource != null)
                    {
                        rcb.DataSource = DataSource;
                        rcb.DataBind();
                        rcb.SelectedValue = ds.Tables[0].Rows[i]["Datavalue"].ToString();
                    }
                    rcb.Enabled = Enabled;



                    ImageButton ibt = new ImageButton();
                    ibt.ID = "ibt" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
                    ibt.ImageUrl = "~/Icons/Sigma/Add_16X16_Standard.png";
                    ibt.Click += ibtVVT_Click;
                    ibt.Enabled = Enabled;


                    TableRow tr = new TableRow();
                    tr.ID = ds.Tables[0].Rows[i]["DataControlID"].ToString();

                    TableCell tc = new TableCell();
                    tc.CssClass = "MyLable";
                    tc.Controls.Add(lb);
                    tc.Width = Unit.Parse(LBWidth);

                    TableCell tc2 = new TableCell();
                    tc2.CssClass = "MyContent";
                    tc2.Width = Unit.Parse(Width);
                    tc2.Controls.Add(rcb);

                    TableCell tc3 = new TableCell();
                    tc3.Controls.Add(ibt);

                    tr.Cells.Add(tc);
                    tr.Cells.Add(tc2);
                    tr.Cells.Add(tc3);
                    tblMain.Rows.Add(tr);
                }
            }


        }

        void rcb_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox tb = (RadComboBox)sender;
            DataSet dst = DataProvider.KhanhND.B_BDYNAMICCONTROLS_Update(tb.ID.ToString().Replace("rcb", ""), Request.QueryString["tabid"].ToString(), this.ID.ToString(), "", tb.SelectedValue, Session["DataKey"].ToString());
        }


        void ibt_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtXoa = (ImageButton)sender;
            string name = ibtXoa.ID.Replace("ibt", "");
            TableRow rw = (TableRow)tblMain.FindControl(name);
            tblMain.Rows.Remove(rw);
            DataProvider.KhanhND.B_BDYNAMICCONTROLS_Del(name);
        }

        protected void ibtVVT_Click(object sender, ImageClickEventArgs e)
        {

            DataSet dst = DataProvider.KhanhND.B_BDYNAMICCONTROLS_Update("0", Request.QueryString["tabid"].ToString(), this.ID.ToString(), "", "", Session["DataKey"].ToString());


            Label lb = new Label();
            lb.ID = "lbl" + dst.Tables[0].Rows[0]["DataControlID"].ToString();
            lb.Text = VVTLabel;

            RadComboBox rcb = new RadComboBox();
            rcb.ID = "rcb" + dst.Tables[0].Rows[0]["DataControlID"].ToString();
            rcb.AutoPostBack = true;
            rcb.Items.Add(new RadComboBoxItem(""));
            rcb.AppendDataBoundItems = true;
            rcb.Width = Unit.Parse(Width);
            rcb.DataTextField = "TextField";
            rcb.DataValueField = "ValueField";
            if (DataSource != null)
            {
                rcb.DataSource = DataSource;
                rcb.DataBind();
            }

            ImageButton ibt = new ImageButton();
            ibt.ID = "ibt" + dst.Tables[0].Rows[0]["DataControlID"].ToString();
            ibt.ImageUrl = "~/Icons/Sigma/Delete_16X16_Standard.png";
            ibt.Click += ibt_Click;

            TableRow tr = new TableRow();
            tr.ID = dst.Tables[0].Rows[0]["DataControlID"].ToString();

            TableCell tc = new TableCell();
            tc.CssClass = "MyLable";
            tc.Controls.Add(lb);
            tc.Width = Unit.Parse(LBWidth);

            TableCell tc2 = new TableCell();
            tc2.CssClass = "MyContent";
            tc2.Width = Unit.Parse(Width);
            tc2.Controls.Add(rcb);

            TableCell tc3 = new TableCell();
            tc3.Controls.Add(ibt);

            tr.Cells.Add(tc);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tblMain.Rows.Add(tr);


        }
    }
}