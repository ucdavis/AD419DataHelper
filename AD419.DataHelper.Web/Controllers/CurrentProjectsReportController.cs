using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AD419.DataHelper.Web.Models;
using Microsoft.Reporting.WebForms;

namespace AD419.DataHelper.Web.Controllers
{
    public class CurrentProjectsReportController : SuperController
    {
        // GET: CurrentProjectsReport
        public ActionResult Index()
        {
            var reportName = @"Current AD419 Projects";
            var model = new ReportViewerModel(reportName);

            return View(model);
        }
    }
}