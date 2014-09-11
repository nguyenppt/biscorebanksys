using BankProject.Repository;
using DotNetNuke.Entities.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BankProject.Views.TellerApplication
{
    public partial class CalculatedInterestedAmountManual : PortalModuleBase
    {
        private static Object _calculateInterestConsoleLock = new Object();
        private static bool _calculatingInterestConsole = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack) return;
            dtpDate.SelectedDate = DateTime.Today;
        }

        protected void btCalcu_Click(object sender, EventArgs e)
        {
            if (rcbAccountType.SelectedValue == "1")
            {
                BankProject.DataProvider.Database.BOPENACCOUNT_CalculatorInterestAmount(dtpDate.SelectedDate);
            }

            if (rcbAccountType.SelectedValue == "2")
            {
                if (!_calculatingInterestConsole)
                {
                    lock (_calculateInterestConsoleLock)
                    {                        
                        try
                        {
                            _calculatingInterestConsole = true;                            
                            string appPath = HttpContext.Current.Request.ApplicationPath;
                            string physicalPath = HttpContext.Current.Request.MapPath(appPath);
                            Process process = new Process();
                            process.StartInfo.FileName = physicalPath + "\\Bin\\CalculateInterestConsole.exe";
                            process.StartInfo.RedirectStandardOutput = true;
                            process.StartInfo.UseShellExecute = false;
                            process.StartInfo.CreateNoWindow = true;
                            //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            process.StartInfo.Arguments = string.Format("input {0} {1} {2}", dtpDate.SelectedDate.Value.Year, dtpDate.SelectedDate.Value.Month, dtpDate.SelectedDate.Value.Day);
                            process.Start();
                        }
                        finally
                        {
                            _calculatingInterestConsole = false;
                        }                        
                    }                    
                }                
            }
        }
    }
}