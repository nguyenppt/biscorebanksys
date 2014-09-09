using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.Controls
{
    public partial class PaymentMethodControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadControls();
        }

        private void LoadControls()
        {
            VVComboBox vvcb1 = new VVComboBox();
            vvcb1.ID = "VVCB_"+ DateTime.Now.Millisecond;
            VVComboBox vvcb2 = new VVComboBox();
            vvcb2.ID = "VVCB_" + DateTime.Now.Millisecond;
            VVTextBox vvtb = new VVTextBox();
            vvtb.ID = "VVCB_" + DateTime.Now.Millisecond;

            ImageButton ibt = new ImageButton();
            //ibt.ID = "ibt" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
            ibt.ImageUrl = "~/Icons/Sigma/Add_16X16_Standard.png";
            ibt.Click += ibtVVT_Click;

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.CssClass = "MyContent";
            tc.Controls.Add(vvcb1);

            TableCell tc2 = new TableCell();
            tc2.CssClass = "MyContent";
            tc2.Controls.Add(vvtb);

            TableCell tc3 = new TableCell();
            tc2.CssClass = "MyContent";
            tc3.Controls.Add(vvcb2);


            TableCell tc4 = new TableCell();
            tc4.Controls.Add(ibt);

            tr.Cells.Add(tc);
            tr.Cells.Add(tc2);
            tr.Cells.Add(tc3);
            tr.Cells.Add(tc4);
            tblMain.Rows.Add(tr);
        }
        protected void ibtVVT_Click(object sender, ImageClickEventArgs e)
        {

            AddNewRow();
        }
        public void AddNewRow()
        {
            VVComboBox vvcb1 = new VVComboBox();
            vvcb1.ID = "VVCB_" + DateTime.Now.Millisecond;
            VVComboBox vvcb2 = new VVComboBox();
            vvcb2.ID = "VVCB_" + DateTime.Now.Millisecond;
            VVTextBox vvtb = new VVTextBox();
            vvtb.ID = "VVCB_" + DateTime.Now.Millisecond;

            ImageButton ibt = new ImageButton();
            //ibt.ID = "ibt" + ds.Tables[0].Rows[i]["DataControlID"].ToString();
            ibt.ImageUrl = "~/Icons/Sigma/Delete_16X16_Standard.png";
            ibt.Click += ibt_Click;

            TableRow tr = new TableRow();

            TableCell tc = new TableCell();
            tc.CssClass = "MyContent";
            tc.Controls.Add(vvcb1);

            TableCell tc2 = new TableCell();
            tc2.CssClass = "MyContent";
            tc2.Controls.Add(vvtb);

            TableCell tc3 = new TableCell();
            tc2.CssClass = "MyContent";
            tc3.Controls.Add(vvcb2);


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
            DataProvider.KhanhND.B_BDYNAMICCONTROLS_Del(name);
        }

    }
}