using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace TDIHKCorporate.Helpers.Extensions
{
    public class UrlExtensions
    {
        public static string FriendlyUrlTurkish(UrlHelper helper, string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            url = url.ToLower();
            url = url.Trim();
            //if (url.Length > 100)
            //{
            //    url = url.Substring(0, 100);
            //}
            url = url.Replace("Ä", "A");
            url = url.Replace("ä", "a");
            url = url.Replace("ß", "b");
            url = url.Replace("İ", "I");
            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "o");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "u");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            url = r.Replace(url, "-");
            while (url.IndexOf("--") > -1)
                url = url.Replace("--", "-");
            return url;
        }

        public static string FriendlyUrlGerman(UrlHelper helper, string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            url = url.ToLower();
            url = url.Trim();
            //if (url.Length > 100)
            //{
            //    url = url.Substring(0, 100);
            //}

            url = url.Replace("Ä", "ae");
            url = url.Replace("ä", "ae");
            url = url.Replace("ß", "ss");
            url = url.Replace("İ", "I");
            url = url.Replace("ı", "i");
            url = url.Replace("ğ", "g");
            url = url.Replace("Ğ", "G");
            url = url.Replace("ç", "c");
            url = url.Replace("Ç", "C");
            url = url.Replace("ö", "oe");
            url = url.Replace("Ö", "O");
            url = url.Replace("ş", "s");
            url = url.Replace("Ş", "S");
            url = url.Replace("ü", "ue");
            url = url.Replace("Ü", "U");
            url = url.Replace("'", "");
            url = url.Replace("\"", "");
            char[] replacerList = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            for (int i = 0; i < replacerList.Length; i++)
            {
                string strChr = replacerList[i].ToString();
                if (url.Contains(strChr))
                {
                    url = url.Replace(strChr, string.Empty);
                }
            }
            Regex r = new Regex("[^a-zA-Z0-9_-]");
            url = r.Replace(url, "-");
            while (url.IndexOf("--") > -1)
                url = url.Replace("--", "-");
            return url;
        }
    }
}