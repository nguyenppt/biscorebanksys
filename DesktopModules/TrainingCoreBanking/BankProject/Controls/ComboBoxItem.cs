using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Controls
{
    public class ComboBoxItem
    {
        public string Value;
        public string Text;
        //
        public ComboBoxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}