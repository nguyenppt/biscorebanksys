using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.DBContext
{
    public partial class BEXPORT_LC_DOCS_PROCESSING_Report_Cover_Result
    {
        public string ReferLC
        {
            get
            {
                int i = DocCode.IndexOf(".");
                if (i > 0)
                    return DocCode.Substring(0, i);
                else
                    return DocCode;
            }

        }
        public string MaturityDateString
        {
            get
            {
                if (MaturityDate != null)
                {
                    return ((DateTime)MaturityDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    return "";
                }
            }
        }
    }
}