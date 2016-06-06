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
   
        private int _fiscalYear;
        protected int FiscalYear {
            get
            {
                if (_fiscalYear == 0)
                {
                    var session = System.Web.HttpContext.Current.Session;
                    if (session["FiscalYear"] == null)
                    {
                        _fiscalYear = DateTime.Now.Year - 1;
                        session["FiscalYear"] = _fiscalYear;
                    }
                    else
                    {
                        _fiscalYear = Convert.ToInt32(session["FiscalYear"]);
                    }
                }
                return _fiscalYear;
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