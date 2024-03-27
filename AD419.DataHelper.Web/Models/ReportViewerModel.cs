using System;
using System.Configuration;
using System.Net;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace AD419.DataHelper.Web.Models
{
    public class ReportViewerModel
    {
        private static readonly string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];
        private static readonly string ReportServerDirectory = ConfigurationManager.AppSettings["ReportServerDirectory"];
        private static readonly string ReportViewerUserName = ConfigurationManager.AppSettings["ReportViewerUser"]; 
        private static readonly string ReportViewerPassword = ConfigurationManager.AppSettings["ReportViewerPassword"];
        private static readonly string ReportViewerDomainName = ConfigurationManager.AppSettings["ReportViewerDomain"];

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

           IReportServerCredentials myCredentials = new CustomReportCredentials(ReportViewerUserName, ReportViewerPassword, ReportViewerDomainName);
           ReportViewer.ServerReport.ReportServerCredentials = myCredentials;
           ReportViewer.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _userName;
            private string _passWord;
            private string _domainName;

            public CustomReportCredentials(string userName, string passWord, string domainName)
            {
                _userName = userName;
                _passWord = passWord;
                _domainName = domainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }

            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_userName, _passWord, _domainName); }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password,
                out string authority)
            {
                authCookie = null;
                user = password = authority = null;
                return false;
            }

        }
    }
}