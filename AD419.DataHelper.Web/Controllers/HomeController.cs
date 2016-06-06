using System;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class HomeController : SuperController
    {
        public HomeController()
        {
            var session = System.Web.HttpContext.Current.Session;
            var fiscalYear = session["FiscalYear"];
            if (fiscalYear == null)
            {
                fiscalYear = DateTime.Now.Year - 1;
                session["FiscalYear"] = fiscalYear;
            }
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}