using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.Controls
{
    public partial class MultiTextBox : System.Web.UI.UserControl
    {
        protected int MultiTextBoxRow = 1;
        private string _Label = "";
        private int _LabelWidth = 0;
        private bool isSetText = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || isSetText) return;
            litMultiTextBox.Text = createTextBox();
            //Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), getJSFunction(), getJSFunction() + "();");
        }
        //
        public string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        public int LabelWidth
        {
            get { return _LabelWidth; }
            set { _LabelWidth = value; }
        }
        //
        public void setText(string text)
        {
            setText(text, false);
        }
        public void setText(string text, bool readOnly)
        {
            isSetText = true;
            litMultiTextBox.Text = "";
            txtMultiTextBoxString.Value = text.ToString();
            string[] Narratives = new string[] { "" };
            if (!string.IsNullOrEmpty(txtMultiTextBoxString.Value)) Narratives = txtMultiTextBoxString.Value.Split(new string[] { "\n" }, StringSplitOptions.None);
            foreach (string n in Narratives)
            {
                litMultiTextBox.Text += createTextBox(n, readOnly, MultiTextBoxRow);
                MultiTextBoxRow++;
            }
        }
        //
        private string createTextBox()
        {
            MultiTextBoxRow = 1;
            return createTextBox("", false, MultiTextBoxRow);
        }
        private string createTextBox(string text, bool readOnly, int order)
        {
            return "<tr>"
                + "<td class=\"MyLable\"" + (_LabelWidth > 0 ? " style=\"Width:" + _LabelWidth + "px\"" : "") + ">" + (order <= 1 ? _Label : "") + "</td>" //+ "." + order 
                        + "<td class=\"MyContent\" style=\"width:350px;\">" + (readOnly ? "<span class=\"riSingle RadInput RadInput_Default\">" : "") + "<input style=\"width:350px;\" type=\"text\" value=\"" + text.Replace("\"", "\"\"") + "\" " + (readOnly ? "readonly" : "") + (readOnly ? " class=\"riTextBox riDisabled\"" : "") + " />" + (readOnly ? "</span>" : "") + "</td>"
                        + "<td>" + (String.IsNullOrEmpty(text) ? "<a class=\"MultiTextBoxAddRow\"><img src=\"Icons/Sigma/Add_16X16_Standard.png\"></a>" : "") + "</td>"
                    + "</tr>";
        }
        //
        public string getText()
        {
            return txtMultiTextBoxString.Value;
        }
        //
        public string getJSFunction()
        {
            return divMultiTextBox.ClientID + "_submit";
        }
    }
}