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
    public partial class VVMutiControls : System.Web.UI.UserControl
    {
        DataSet ds;
        DataSet _DataSource;
        string _VVTLabel, _VVTText, _VVCSelecttedValue, _SourceTable, _VVTDataKey, _LBwidth = "150";
        string _width = "200";
        bool _Enabled = true;
        static int idx = 0;
        static int clicked = 0;
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
        protected override void OnInit(EventArgs e)
        {
            clicked++;
            base.OnInit(e);
            if (!IsPostBack)
            {
                
            }
            for (int i = 0; i < clicked; i++)
            {
                if(i==0)
                    LoadControls();
                else
                    AddRowDelete();
            }
            
           
        }

        private void LoadControls()
        {
            RadComboBox rcb = new RadComboBox();
            rcb.ID = "rcb" + idx++;
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
            }
            rcb.Enabled = Enabled;


            TextBox tb = new TextBox();
            tb.ID = "txtName" + idx++;
            tb.AutoPostBack = true;
            tb.Width = Unit.Parse(Width);

            RadComboBox rcb2 = new RadComboBox();
            rcb2.ID = "rcb2" + idx++;
            rcb2.AutoPostBack = true;
            rcb2.Items.Add(new RadComboBoxItem(""));
            rcb2.AppendDataBoundItems = true;
            rcb2.Width = Unit.Parse(Width);
            rcb2.DataTextField = "TextField";
            rcb2.DataValueField = "ValueField";
            if (DataSource != null)
            {
                rcb2.DataSource = DataSource;
                rcb2.DataBind();
            }
            rcb2.Enabled = Enabled;

            ImageButton ibt = new ImageButton();
            ibt.ID = "ibt" + idx++ ;
            ibt.ImageUrl = "~/Icons/Sigma/Add_16X16_Standard.png";
            ibt.Click += ibtVVT_Click;
            ibt.Enabled = Enabled;


            TableRow tr = new TableRow();
            tr.ID = idx.ToString();

            TableCell tc = new TableCell();
            tc.CssClass = "MyContent";
            tc.Controls.Add(rcb);
            tc.Width = Unit.Parse(LBWidth);

            TableCell tc2 = new TableCell();
            tc2.CssClass = "MyContent";
            tc2.Width = Unit.Parse(Width);
            tc2.Controls.Add(tb);

            TableCell tc3 = new TableCell();
            tc3.CssClass = "MyContent";
            tc3.Width = Unit.Parse(Width);
            tc3.Controls.Add(rcb2);

            TableCell tc4 = new TableCell();
            tc4.Controls.Add(ibt);

            tr.Cells.Add(tc);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tr.Cells.Add(tc4);
            tblMain.Rows.Add(tr);
        }
        void rcb_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox tb = (RadComboBox)sender;
        }
        protected void ibtVVT_Click(object sender, ImageClickEventArgs e)
        {

            AddRowDelete();


        }

        private void AddRowDelete()
        {
            RadComboBox rcb = new RadComboBox();
            rcb.ID = "rcb" + idx++;
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
            }
            rcb.Enabled = Enabled;


            TextBox tb = new TextBox();
            tb.ID = "txtName" + idx++;
            tb.AutoPostBack = true;
            tb.Width = Unit.Parse(Width);

            RadComboBox rcb2 = new RadComboBox();
            rcb2.ID = "rcb2" + idx++;
            rcb2.AutoPostBack = true;
            rcb2.Items.Add(new RadComboBoxItem(""));
            rcb2.AppendDataBoundItems = true;
            rcb2.Width = Unit.Parse(Width);
            rcb2.DataTextField = "TextField";
            rcb2.DataValueField = "ValueField";
            if (DataSource != null)
            {
                rcb2.DataSource = DataSource;
                rcb2.DataBind();
            }
            rcb2.Enabled = Enabled;

            ImageButton ibt = new ImageButton();
            ibt.ID = "ibt" + idx++;
            ibt.ImageUrl = "~/Icons/Sigma/Delete_16X16_Standard.png";
            ibt.Click += ibt_Click;
            ibt.Enabled = Enabled;


            TableRow tr = new TableRow();
            tr.ID = idx.ToString();

            TableCell tc = new TableCell();
            tc.CssClass = "MyContent";
            tc.Controls.Add(rcb);
            tc.Width = Unit.Parse(LBWidth);

            TableCell tc2 = new TableCell();
            tc2.CssClass = "MyContent";
            tc2.Width = Unit.Parse(Width);
            tc2.Controls.Add(tb);

            TableCell tc3 = new TableCell();
            tc3.CssClass = "MyContent";
            tc3.Width = Unit.Parse(Width);
            tc3.Controls.Add(rcb2);

            TableCell tc4 = new TableCell();
            tc4.Controls.Add(ibt);

            tr.Cells.Add(tc);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tr.Cells.Add(tc4);
            tblMain.Rows.Add(tr);
        }
        void ibt_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibtXoa = (ImageButton)sender;
            string name = ibtXoa.ID.Replace("ibt", "");
            TableRow rw = (TableRow)tblMain.FindControl(name);
            tblMain.Rows.Remove(rw);
        }
    }
}