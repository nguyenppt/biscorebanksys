using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankProject.Entity
{
    public class Category
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FormatedName
        {
            get
            {
                return string.Format("{0} - {1}", Code, Name);
            }
        }        
    }
}