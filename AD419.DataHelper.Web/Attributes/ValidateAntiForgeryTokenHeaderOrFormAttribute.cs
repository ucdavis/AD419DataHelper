using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateAntiForgeryTokenHeaderOrFormAttribute : FilterAttribute, IAuthorizationFilter
    {
        private const string TokenHeaderName = "X-XSRF-TOKEN"; //Angular token, but could be anything

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var token = filterContext.HttpContext.Request.Headers[TokenHeaderName];

            if (token != null)
            {
                //validate the token stored in header, so check it against the validation cookie
                if (filterContext.HttpContext.Request == null) { throw new ArgumentNullException("filterContext"); }

                var cookieToken = filterContext.HttpContext.Request.Cookies[AntiForgeryConfig.CookieName];
                if (cookieToken == null) { throw new HttpAntiForgeryException("Required verification token not found"); }
                AntiForgery.Validate(cookieToken.Value, token);
            }
            else
            {
                //validate the token stored in form. Same as ValidateAntiForgeryTokenAttribute
                AntiForgery.Validate();
            }
        }
    }
}