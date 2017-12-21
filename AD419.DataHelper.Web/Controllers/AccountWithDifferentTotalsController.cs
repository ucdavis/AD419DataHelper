using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class AccountWithDifferentTotalsController : SuperController
    {
        // GET: AccountWithDifferentTotals
        public ActionResult Index()
        {
            var year = FiscalYearService.FiscalYear;
            var accounts = DbContext.GetAccountsWithDifferentTotals(year);
            return View(accounts.ToList());
        }

        public ActionResult Details(string id)
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(SUM(Expenses),0) FieldStationExpenses
	    FROM [dbo].[AllExpenses] 
	    WHERE [DataSource] = '22F'");

            var retval = query.FirstOrDefault();

            return retval;
            var exclusion = DbContext.C204Exclusions.Find(id);
            if (exclusion == null)
            {
                return HttpNotFound();
            }

            return View(exclusion);
        }
    }
}
