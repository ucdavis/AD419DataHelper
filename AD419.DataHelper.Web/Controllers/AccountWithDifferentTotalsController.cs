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
    }
}
