using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BankProject.Helper
{
    public class CurrencyList
    {
        [XmlElement("CURRENCY")]
        public List<Currency> Items;
    }
    public class Currency
    {
        public string CURRENCY_CODE { get; set; }
        public string CURRENCY_NAME { get; set; }
        public string CURRENCY_DESCRIPTION { get; set; }
        public string UNIT { get; set; }
        public int NUM_OF_PERCENT { get; set; }
        public string READ_PERCENT_1 { get; set; }
        public string READ_PERCENT_2 { get; set; }
        public string READ_PERCENT_3 { get; set; }
        public string VN_UNIT { get; set; }
        public string VN_READ_PERCENT_1 { get; set; }
        public string VN_READ_PERCENT_2 { get; set; }
        public string VN_READ_PERCENT_3 { get; set; }
    }

}