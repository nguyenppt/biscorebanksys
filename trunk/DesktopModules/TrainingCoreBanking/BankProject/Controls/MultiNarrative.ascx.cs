using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankProject.Controls
{
    public partial class MultiNarrative : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "ConfirmSubmit", "NarrativeJoin();");
        }

        public void setText(string text)
        {
            setText(text, false);
        }
        public void setText(string text, bool readOnly)
        {
            litNarrative.Text = "";
            hiddenNarrative.Value = text.ToString();
            string[] Narratives = new string[] { "" };
            if (!string.IsNullOrEmpty(hiddenNarrative.Value)) Narratives = hiddenNarrative.Value.Split(new string[] { "\n" }, StringSplitOptions.None);
            foreach (string n in Narratives)
            {
                litNarrative.Text += "<tr>"
                                        + "<td class=\"MyLable\">Narrative</td>"
                                        + "<td class=\"MyContent\" style=\"width:350px;\"><span class=\"riSingle RadInput RadInput_Default\"><input name=\"txtNarrative\" style=\"width:350px;\" type=\"text\" value=\"" + n.Replace("\"", "\"\"") + "\" " + (readOnly ? "readonly" : "") + " class=\"riTextBox riDisabled\" /></span></td>"
                                        + "<td></td>"
                                    + "</tr>";
            }
        }

        public string getText()
        {
            return hiddenNarrative.Value;
        }
    }
}