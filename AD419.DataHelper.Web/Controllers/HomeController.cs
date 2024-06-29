﻿using System.Web.Mvc;

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
            if (User.IsInRole("ReportViewer"))
            {
                // return View("Lockout");
                return RedirectToAction("AeComparisonReport", "Report");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SetFiscalYear(int fiscalYear)
        {
            FiscalYearService.FiscalYear = fiscalYear;
            return RedirectToAction("Index");
        }
    }
}