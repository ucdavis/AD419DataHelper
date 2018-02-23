using System.Linq;
using System.Web.Mvc;

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

        public ActionResult Details(string chart, string account)
        {
            var year = FiscalYearService.FiscalYear;
            var accountDetails = DbContext.GetAccountWithDifferentTotalDetails(year, chart, account).FirstOrDefault();

            return View(accountDetails);
        }

    }
}
