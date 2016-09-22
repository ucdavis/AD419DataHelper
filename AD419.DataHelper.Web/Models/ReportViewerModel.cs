using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;

namespace AD419.DataHelper.Web.Models
{
    public class ReportViewerModel
    {
        private static readonly string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];
        private static readonly string ReportServerDirectory = ConfigurationManager.AppSettings["ReportServerDirectory"];
        public string ReportPath { get; set; }

        public ReportViewer ReportViewer { get; set; }

        public string ReportName { get; set; }

        public string ReportTitle { get; set; }

        public ReportViewerModel(string reportName) : this()
        {
            ReportName = reportName;
            ReportPath = ReportServerDirectory+ReportName;
            ReportViewer.ServerReport.ReportPath = ReportPath;
        }

        public ReportViewerModel()
        {
            ReportViewer = new ReportViewer
            {
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100),
                ProcessingMode = ProcessingMode.Remote
            };

            ReportViewer.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
        }
    }
}