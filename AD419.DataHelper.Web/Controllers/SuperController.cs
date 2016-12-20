using System.Configuration;
using AD419.DataHelper.Web.Models;
using System.Web.Mvc;
using AD419.DataHelper.Web.Services;

namespace AD419.DataHelper.Web.Controllers
{
    [Authorize]
    public class SuperController : Controller
    {
        protected AD419DataContext DbContext;
        protected FISDataContext FisDbContext;
        protected PPSDataContext PpsDbContext;
        protected FiscalYearService FiscalYearService;
        protected static readonly string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];

        public SuperController()
        {
            DbContext = new AD419DataContext();
            FisDbContext = new FISDataContext();
            PpsDbContext = new PPSDataContext();
            FiscalYearService = new FiscalYearService(DbContext);
        }

        public string Message
        {
            get
            {
                return TempData["Message"] as string;
            }
            set
            {
                TempData["Message"] = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return TempData["ErrorMessage"] as string;
            }
            set
            {
                TempData["ErrorMessage"] = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
                FisDbContext.Dispose();
                PpsDbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}