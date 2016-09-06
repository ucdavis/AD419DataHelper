using System;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BypassAntiForgeryTokenAttribute : ActionFilterAttribute { }
}