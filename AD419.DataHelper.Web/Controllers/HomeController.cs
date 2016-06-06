using System;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class HomeController : SuperController
    {
        public HomeController()
        {
            ViewBag.FiscalYear = FiscalYear;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetFiscalYear(int fiscalYear)
        {
            FiscalYear = fiscalYear;
            return RedirectToAction("Index");
        }
    }
}