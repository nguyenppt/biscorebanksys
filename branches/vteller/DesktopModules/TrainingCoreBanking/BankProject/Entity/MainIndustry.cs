using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class MainIndustry
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string VNDescription { get; set; }

        public string GBDescription { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                return string.Empty;
            }

            return string.Format("{0}-{1}-{2}", this.Code, this.VNDescription, this.GBDescription);
        }
    }
}