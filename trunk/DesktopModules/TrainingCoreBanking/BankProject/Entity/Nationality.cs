using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class Nationality
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string TenTA { get; set; }

        public string TenTV { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Code))
            {
                return string.Empty;
            }

            return string.Format("{0}-{1}", this.Code, this.TenTA);
        }
    }
}