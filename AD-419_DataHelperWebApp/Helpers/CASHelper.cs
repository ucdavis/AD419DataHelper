using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;

namespace AD_419_DataHelperWebApp.Helpers
{
    public static class CasHelper
    {
        private const string StrCasUrl = "https://cas.ucdavis.edu/cas/";
        private const string StrTicket = "ticket";
        private const string StrReturnUrl = "ReturnURL";

        public static string GetReturnUrl()
        {
            return HttpContext.Current.Request.QueryString["ReturnURL"];
        }

        public static void LoginAndRedirect()
        {
            var url = Login();
            if (url == null)
                return;
            HttpContext.Current.Response.Redirect(url);
        }

        public static string Login()
        {
            var current = HttpContext.Current;
            var httpCookie = current.Request.Cookies[FormsAuthentication.FormsCookieName];
            var authenticationTicket = (FormsAuthenticationTicket)null;
            if (httpCookie != null)
            {
                try
                {
                    authenticationTicket = FormsAuthentication.Decrypt(httpCookie.Value);
                }
                catch
                {
                    authenticationTicket = null;
                }
            }
            if (authenticationTicket != null && !authenticationTicket.Expired) return null;

            var str1 = "";
            foreach (var strA in current.Request.QueryString.AllKeys)
            {
                if (string.Compare(strA, "ticket", StringComparison.OrdinalIgnoreCase) != 0)
                    str1 = str1 + "&" + strA + "=" + current.Request.QueryString[strA];
            }
            if (!string.IsNullOrEmpty(str1))
                str1 = "?" + str1.Substring(1);
            var str2 = current.Request.QueryString["ticket"];
            var str3 = current.Server.UrlEncode(current.Request.Url.GetLeftPart(UriPartial.Path) + str1);
            if (!string.IsNullOrEmpty(str2))
            {
                var stream = (new WebClient()).OpenRead("https://cas.ucdavis.edu/cas/validate?ticket=" + str2 + "&service=" + str3);
                var streamReader = new StreamReader(stream);
                if (streamReader.ReadLine() == "yes")
                {
                    FormsAuthentication.SetAuthCookie(streamReader.ReadLine(), false);
                    var returnUrl = CasHelper.GetReturnUrl();
                    if (string.IsNullOrEmpty(returnUrl))
                        return FormsAuthentication.DefaultUrl;
                    return returnUrl;
                }
            }
            current.Response.Redirect("https://cas.ucdavis.edu/cas/login?service=" + str3);
            return null;
        }
    }
}