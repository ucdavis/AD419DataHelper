using System;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    /// <summary>
    /// Displays a list of adjustments that were made in order to make a side-by-side
    /// comparison between the amount displayed in the AD-419 UI and the FFY Expense
    /// by ARC totals.
    /// </summary>
    public class AdjustedTotalsController : SuperController
    {
        // GET: AdjustedTotals
        public ActionResult Index()
        {
            var model = new ExpenseAdjustments
            {
                FieldStationExpenses = GetFieldStationExpenses(),
                CeSpecialistExpenses = GetCeSpecialistExpenses(),
                Expired204Expenses = GetExpired204Expenses(),
                OutsideArcExpenses = GetOutsideArcExpenses(),
                TotalProjectLessThanCutoffAmount204Expenses = GetTotalProjectLessThanCutoffAmount204Expenses(),
                TotalFfyExpensesByArc = GetTotalFfyExpensesByArc(),
                TotalAllResearchFundsAd419 = GetTotalAllResearchFundsAd419(),
            };


            return View(model);
        }

        private decimal GetFieldStationExpenses()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(SUM(Expenses),0) FieldStationExpenses
	    FROM [dbo].[AllExpenses] 
	    WHERE [DataSource] = '22F'");

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetCeSpecialistExpenses()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(SUM(Expenses),0) CeSpecialistExpenses
	    FROM [dbo].[AllExpenses] 
	    WHERE [DataSource] = 'CES'");

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetOutsideArcExpenses()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(SUM(Expenses),0) '204ExpensesOutsideArcFromAllExpenses'
	    FROM AllExpenses
        WHERE Chart+Account IN (
            SELECT Chart+Account  
            FROM AllAccountsFor204Projects
            WHERE IsExpired = 0 AND ExcludedByARC = 1 AND ExcludedByAccount = 0)");

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetExpired204Expenses()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(sum(Expenses),0) 'Expired204Expenses'
        FROM AllAccountsFor204Projects
        WHERE IsExpired = 1 AND ExcludedByARC = 0 AND ExcludedByAccount = 0");

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetTotalProjectLessThanCutoffAmount204Expenses()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
                @"
        SELECT ISNULL(sum(Expenses),0) Excluded204Expenses 
        from AllAccountsFor204Projects 
        where AccessionNumber IN (
            select AccessionNumber 
            from allProjectsNew
            where  Is204 = 1 and IsExpired = 0 AND IsIgnored = 1)");

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetTotalFfyExpensesByArc()
        {
            var query = DbContext.Database.SqlQuery<decimal>(String.Format(
                @"
        SELECT ISNULL(sum(Total),0) TotalFfyExpensesByArc 
        from FFY_ExpensesByARC
        WHERE Chart+Account NOT IN (
            SELECT Chart+Account
            FROM ARCCodeAccountExclusions
            WHERE Year = {0})", FiscalYearService.FiscalYear));

            var retval = query.FirstOrDefault();

            return retval;
        }

        private decimal GetTotalAllResearchFundsAd419()
        {
            var query = DbContext.Database.SqlQuery<decimal>(
               @"
        SELECT ISNULL(sum(Expenses),0) TotalAd419Expenses 
        from Expenses");

            var retval = query.FirstOrDefault();

            return retval;
        }
    }
}