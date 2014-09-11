using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Common
{
    public class Util
    {
        public static string getIDDate()
        {
            DateTime today = DateTime.Today;
            return today.ToString("yy") + today.DayOfYear.ToString().PadLeft(3, '0');

        }
    }
}