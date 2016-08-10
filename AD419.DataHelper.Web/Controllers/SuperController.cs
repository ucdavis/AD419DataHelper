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
        protected FiscalYearService FiscalYearService;

        public SuperController()
        {
            DbContext = new AD419DataContext();
            FisDbContext = new FISDataContext();
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
            }
            base.Dispose(disposing);
        }
    }
}