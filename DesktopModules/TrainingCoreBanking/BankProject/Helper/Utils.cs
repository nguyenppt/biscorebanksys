using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;

namespace BankProject.Helper
{
    public class Utils
    {
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        private static T XmlDeserialize<T>(String xml)
        {
            if (!xml.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
            {
                const string template = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><{0} xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">{1}</{2}>";
                xml = String.Format(template, typeof(T).Name, xml, typeof(T).Name);
            }
            var ser = new XmlSerializer(typeof(T));
            using (TextReader rd = new StringReader(xml))
            {
                return (T)ser.Deserialize(rd);
            }
        }

        private static CurrencyList GetCurrencyList()
        {
            var xmlPath = HttpContext.Current.Server.MapPath("./DesktopModules/TrainingCoreBanking/BankProject/") + "Helper//CURRENCY_LIST.xml";
            var strlstC = File.ReadAllText(xmlPath);
            var lstC = XmlDeserialize<CurrencyList>(strlstC);
            return lstC;
        }
        public static string SubStr(string vl, int idex, int? len)
        {
            string strR = "";
            var nlen = len ?? 0;
            var vlen = vl.Length;
            if (idex > vlen) return strR;
            strR = idex + nlen > vlen ? vl.Substring(idex, vlen - idex) : vl.Substring(idex, nlen);

            return strR;
        }
        private static string ReadThreeUni(string nThree, int idS, int readH)
        {

            var rThree = ReverseString(nThree);
            var iNumDv = rThree.Length > 0 ? Int32.Parse(rThree.Substring(0, 1)) : 0;
            var iNumCh = rThree.Length > 1 ? Int32.Parse(rThree.Substring(1, 1)) : 0;
            var iNumTr = rThree.Length > 2 ? Int32.Parse(rThree.Substring(2, 1)) : 0;
            const string charRead = "****một*hai*ba**bốn*năm*sáu*bảy*tám*chín";

            var rCharRead = "";
            var vdvt = "";
            try
            {
                switch (idS)
                {
                    case 1: vdvt = ""; break;
                    case 2: vdvt = "ngàn"; break;
                    case 3: vdvt = "triệu"; break;
                    case 4: vdvt = "tỷ"; break;
                    case 5: vdvt = "ngàn"; break;
                    case 6: vdvt = "triệu"; break;
                    case 7: vdvt = "tỷ"; break;
                }

                if (Int32.Parse(nThree) == 0)
                {
                    rCharRead = idS == 4 ? " tỷ" : "";
                    return rCharRead;
                }
                string strTr = iNumTr == 0 ? "" : charRead.Substring(iNumTr * 4, 4).Replace("*", "") + " trăm";
                string strDv;
                string strCh;
                if (iNumCh == 0)
                {
                    strCh = "";
                    strDv = iNumDv == 0
                                ? ""
                                : (nThree.Length > 1
                                       ? "lẻ " + charRead.Substring(iNumDv * 4, 4).Replace("*", "")
                                       : charRead.Substring(iNumDv * 4, 4).Replace("*", ""));
                }
                else
                {
                    strCh = iNumCh == 1 ? "mười" : charRead.Substring(iNumCh * 4, 4).Replace("*", "") + " mươi";
                    strDv = iNumDv == 0
                                ? ""
                                : (iNumDv == 1
                                       ? (iNumCh == 1 ? "một" : "mốt")
                                       : (iNumDv == 5 ? "lăm" : charRead.Substring(iNumDv * 4, 4).Replace("*", "")));
                }

                var vdvtr = "";
                if (readH == 1)
                {
                    vdvtr = "không trăm";
                }
                string vdvtr1 = strTr == "" ? vdvtr : strTr;
                rCharRead = vdvtr1.Trim() + " " + strCh.Trim() + " " + strDv.Trim() + " " + vdvt.Trim();

            }
            catch { }

            return rCharRead.Trim();

        }
        
        /*
         * Method Revision History:
         * Version        Date            Author            Comment
         * ----------------------------------------------------------
         * 0.1            NA
         * 0.2            Sep 12, 2015    Hien Nguyen       Fix bug convert sai so le cua USD
         */
        public static string ReadNumber(string mant, double number)
        {
            const string charRead = "khôngmột**hai**ba***bốn**năm**sáu**bảy**tám**chín*";
            var numberR = Math.Abs(number);

            string strdoc = "";
            string strDocle = "";
            string donvi = "";
            int soleTp = 0;
            string le1 = "";
            string le2 = "";
            string le3 = "";
            try
            {
                if (mant != "")
                {

                    try
                    {
                        var cList = GetCurrencyList();
                        var q = from a in cList.Items
                                where a.CURRENCY_NAME == mant
                                select a;

                        foreach (var c in q)
                        {
                            donvi = c.VN_UNIT;
                            soleTp = c.NUM_OF_PERCENT;
                            le1 = c.VN_READ_PERCENT_1;
                            le2 = c.VN_READ_PERCENT_2;
                            le3 = c.VN_READ_PERCENT_3;
                            break;
                        }

                    }
                    catch(Exception ex) {}
                }


                if (numberR <= 0)
                {

                    strdoc = "không";
                    strDocle = "";
                }
                else
                {
                    var soNguyen = (long)Math.Floor(numberR);
                    var strNguyen = soNguyen.ToString(CultureInfo.InvariantCulture);
                    var soLe = Math.Round(numberR - soNguyen, 4);

                    //Fix bug "DIEN GIAI PHIEU VAT TRONG MA HINH THU PHI COLLECT CHARGE BI SAI"
                    /*
                    *Doi voi USD, so le phai duoc nhan 100 truoc khi
                    *dua vao cong thuc chuyen sang so doc boi vi 1USD = 100cent
                    *vi du: 0.5 USD thi so le la 50 cent
                    */
                    if (mant == "USD")
                    {
                        soLe = soLe * 100;
                    }

                    var strLe = soLe.ToString(CultureInfo.InvariantCulture).Replace("0.", "");

                    var I = 0;
                    var idS = 0;
                    var lenn = strNguyen.Length;
                    strDocle = "";
                    strdoc = "";

                    while (I <= lenn)
                    {

                        string three = ReverseString(SubStr(ReverseString(strNguyen), I, 3));
                        idS = idS + 1;
                        I = I + 3;
                        int rh = 0;
                        if (lenn >= 3)
                        {
                            if (idS == 1)
                            {
                                rh = 1;
                            }
                        }

                        strdoc = ReadThreeUni(three, idS, rh) + " " + strdoc;


                    }

                    if (soLe > 0)
                    {
                        strLe = SubStr(strLe, 0, soleTp);
                        if (mant != "VND") //Khong phai VND
                        {
                            if (mant == "XAU") //Vang (4 So Le )
                            {
                                var phan = Int32.Parse(SubStr(strLe, 0, 1));
                                var li = Int32.Parse(SubStr(strLe, 1, 1));
                                var zem = Int32.Parse(SubStr(strLe, 2, 2));
                                if (phan > 0)
                                {
                                    strDocle = charRead.Substring(phan * 5, 5).Replace("*", "") + " " + le1;
                                }
                                if (li > 0)
                                {
                                    strDocle = strDocle + " " + charRead.Substring(li * 5, 5).Replace("*", "") + " " + le2;
                                }
                                if (zem > 0)
                                {
                                    strLe = zem.ToString(CultureInfo.InvariantCulture);
                                    strDocle = strDocle + " " + ReadThreeUni(strLe, 1, 0) + " " + le3;
                                }
                            }
                            else
                            {
                                strDocle = ReadThreeUni(strLe, 1, 0) + " " + le1;
                            }
                        }
                    }
                }
            }
            catch { }
            strdoc = strdoc.Trim().Replace("  ", " ");
            if ((strdoc.Trim() == "") && (strDocle.Trim() != ""))
            {
                strdoc = "Không";
            }

            if (strdoc.Trim().Length > 1)
            {
                strdoc = strdoc.Substring(0, 1).ToUpper() + strdoc.Substring(1).ToLower();
            }

            var strR = strdoc.Trim() + " " + donvi.Trim() + " " + strDocle.Trim();
            
            return strR;
        }
        public static string CurrencyFormat(object ob, string currency, string format = null)
        {
            try
            {
                if (ob == null) return "0";
                if (String.IsNullOrWhiteSpace(format))
                {
                    format = _GetCurrencyFormat(currency);
                }
                if (ob.GetType() == typeof(string))
                {
                    decimal d = decimal.Parse(ob.ToString());
                    return String.Format(format, ob.ToString());
                }
                return String.Format(format, ob);
            }
            catch (Exception)
            {
                return ob.ToString();
            }
        }
        private static string _GetCurrencyFormat(string currency)
        {
            int dec = 2; //default
            foreach (var it in NumberDecimal)
            {
                if (it.Key.Equals(currency))
                {
                    dec = it.Value;
                    break;
                }
            }

            string format = "{0:#,##0";
            if (dec > 0)
            {
                format += "." + new string('#', dec);
            }
            return format + "}";
        }
        public static readonly Dictionary<string, int> NumberDecimal = new Dictionary<string, int>();
        static Utils()
        {
            NumberDecimal.Add("VND", 0);
            NumberDecimal.Add("JPY", 0);
            NumberDecimal.Add("XAU", 4);
            NumberDecimal.Add("VNL", 4);
            NumberDecimal.Add("VTK", 4);
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable datatable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                datatable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
    }
}