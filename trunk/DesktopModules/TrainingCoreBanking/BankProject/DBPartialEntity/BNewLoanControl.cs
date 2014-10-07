using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.DBContext
{
    public partial class BNewLoanControl
    {
        public string Freq_display
        {
            get
            {
                string display = "";
                if (!String.IsNullOrEmpty(Freq))
                {
                    if (!Freq.Equals("E"))
                    {
                        int fre = 0;
                        if (Int32.TryParse(Freq, out fre))
                        {
                            display = fre == 1 ? "M" : Freq + "M";
                        }
                        else
                        {
                            display = Freq;
                        }
                        
                    }
                    else
                    {
                        display = Freq;
                    }
                }

                return display;
            }

        }
    }
}