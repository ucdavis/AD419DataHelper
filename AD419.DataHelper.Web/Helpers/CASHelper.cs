using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;

namespace AD419.DataHelper.Web.Helpers
{
    public static class CasHelper
    {
        private static readonly string CasBaseUrl = ConfigurationManager.AppSettings["Environment"] == "Production"
            ? "https://cas.ucdavis.edu/cas/"
            : "https://ssodev.ucdavis.edu/cas/";
        public static string Login()
        {
            // look for existing sign in data
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

            // build cas request
            var serviceParams = "";
            foreach (var key in current.Request.QueryString.AllKeys)
            {
                if (string.Compare(key, "ticket", StringComparison.OrdinalIgnoreCase) != 0)
                    serviceParams = serviceParams + "&" + key + "=" + current.Request.QueryString[key];
            }
            if (!string.IsNullOrEmpty(serviceParams))
                serviceParams = "?" + serviceParams.Substring(1);
            var serviceUrl = current.Server.UrlEncode(current.Request.Url.GetLeftPart(UriPartial.Path) + serviceParams);

            // check for ticket
            var ticket = current.Request.QueryString["ticket"];
            if (!string.IsNullOrEmpty(ticket))
            {
                // validate ticket
                var client = new WebClient();
                var stream = client.OpenRead(CasBaseUrl + "/validate?ticket=" + ticket + "&service=" + serviceUrl);
                if (stream != null)
                {
                    var streamReader = new StreamReader(stream);
                    if (streamReader.ReadLine() == "yes")
                    {
                        FormsAuthentication.SetAuthCookie(streamReader.ReadLine(), false);
                        return null;
                    }
                }
            }

            // return login redirect url
            return CasBaseUrl + "/login?service=" + serviceUrl;
        }
    }
}