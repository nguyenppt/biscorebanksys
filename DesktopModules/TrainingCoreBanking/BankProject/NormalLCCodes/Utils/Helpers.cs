using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BankProject
{
    /// <summary>
    /// Contain general helper functions
    /// </summary>
    public static class Helpers
    {
        #region Strings
        public static bool IsEmpty(this string s)
        {
            return s == null || s.Length == 0;
        }
        public static IEnumerable<int> SplitToInts(this string s, params char[] separator)
        {
            if (!s.IsEmpty())
            {
                var ss = s.Split(separator);
                if (ss != null && ss.Length > 0)
                {
                    return ss.Select((s1) => { return Convert.ToInt32(s1); });
                }
            }
            return Enumerable.Empty<int>();
        }
        #endregion

        /// <summary>
        /// Get state value base on given key. If the value of the key is not exist, return default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="state"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetState<T>(this StateBag state, string key, T defaultValue = default(T))
        {
            var val = state[key];
            if (val == null || !(val is T))
                return defaultValue;
            return (T)val;
        }

        public static void SetState<T>(this StateBag state, string key, T value)
        {
            state[key] = value;
        }
    }
}
