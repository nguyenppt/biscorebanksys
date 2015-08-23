using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public partial class BCOUNTRY
    {
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.MaQuocGia))
            {
                return string.Empty;
            }

            return string.Format("{0}-{1}", this.MaQuocGia, this.TenTA);
        }
    }
}