using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace BankProject.Controls
{
    public class CustomDataTimePicker : RadDateTimePicker
    {
        public CustomDataTimePicker()
        {
            //DateInput-DateFormat="yyyy/MM/dd"
            this.DateInput.DateFormat = "yyyy/MM/dd";
            this.MinDate = DateTime.MinValue;
        }
    }
}