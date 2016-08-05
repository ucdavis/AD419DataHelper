using AD419.DataHelper.Web.Models;
using System;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    [Authorize]
    public class SuperController : Controller
    {
        protected AD419DataContext DbContext = new AD419DataContext();
        protected FISDataContext FisDbContext = new FISDataContext();
   
        protected int FiscalYear
        {
            get
            {
                var session = System.Web.HttpContext.Current.Session;
                if (session["FiscalYear"] == null)
                {
                    session["FiscalYear"] = DateTime.Now.Year - 1;
                }

                return Convert.ToInt32(session["FiscalYear"]);
            }
            set
            {
                var session = System.Web.HttpContext.Current.Session;
                session["FiscalYear"] = value;
            }
        }

        public DateTime FiscalStartDate
        {
            get
            {
                return new DateTime(FiscalYear - 1, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public DateTime FiscalEndDate
        {
            get
            {
                return new DateTime(FiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            }
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