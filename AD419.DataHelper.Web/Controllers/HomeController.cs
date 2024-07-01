using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using AD419.Core.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class HomeController : SuperController
    {
        public HomeController()
        {
            ViewBag.FiscalYear = FiscalYearService.FiscalYear;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("ReportViewer"))
                {
                    // return View("Lockout");
                    return RedirectToAction("AeComparisonReport", "Report");
                }

                return View();
            }

            return View("Error");

        }

        [HttpPost]
        public ActionResult SetFiscalYear(int fiscalYear)
        {
            FiscalYearService.FiscalYear = fiscalYear;
            return RedirectToAction("Index");
        }
    }
}