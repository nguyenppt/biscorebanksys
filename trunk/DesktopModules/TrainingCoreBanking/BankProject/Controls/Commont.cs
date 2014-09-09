using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Controls
{
    public class Commont
    {
        public static void SetTatusFormControls(ControlCollection ChildCtrls,bool enabel)
        {
            foreach (Control Ctrl in ChildCtrls)
            {
                if (Ctrl is TextBox)
                    ((TextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadComboBox)
                    ((RadComboBox)Ctrl).Enabled = enabel;
                else if(Ctrl is RadMaskedTextBox)
                    ((RadMaskedTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is Label)
                    ((Label)Ctrl).Enabled = enabel;
                else if (Ctrl is RadNumericTextBox)
                    ((RadNumericTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadTextBox)
                    ((RadTextBox)Ctrl).Enabled = enabel;
                else if (Ctrl is RadDatePicker)
                    ((RadDatePicker)Ctrl).Enabled = enabel;
                else if (Ctrl is VVTextBox)
                    ((VVTextBox)Ctrl).SetEnable(enabel);
                else if (Ctrl is VVNumberBox)
                    ((VVNumberBox)Ctrl).SetEnable(enabel);
                else if (Ctrl is VVDatePicker)
                    ((VVDatePicker)Ctrl).SetEnable(enabel);
                else
                    SetTatusFormControls(Ctrl.Controls, enabel);
            }
        }

        public static void SetEmptyFormControls(ControlCollection ChildCtrls)
        {
            foreach (Control Ctrl in ChildCtrls)
            {
                if (Ctrl is TextBox)
                    ((TextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadComboBox)
                    ((RadComboBox)Ctrl).SelectedValue = string.Empty;
                else if (Ctrl is Label)
                    ((Label)Ctrl).Text = string.Empty;
                else if (Ctrl is RadNumericTextBox)
                    ((RadNumericTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadMaskedTextBox)
                    ((RadMaskedTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadTextBox)
                    ((RadTextBox)Ctrl).Text = string.Empty;
                else if (Ctrl is RadDatePicker)
                    ((RadDatePicker)Ctrl).SelectedDate = null;
                else if (Ctrl is VVTextBox)
                    ((VVTextBox)Ctrl).SetTextDefault("");
                else if (Ctrl is VVNumberBox)
                    ((VVNumberBox)Ctrl).SetTextDefault("");
                else if (Ctrl is VVDatePicker)
                    ((VVDatePicker)Ctrl).SetTextDefault("");
                else
                    SetEmptyFormControls(Ctrl.Controls);
            }
        }
    }
}