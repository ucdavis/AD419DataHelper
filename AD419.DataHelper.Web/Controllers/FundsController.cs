using System.Linq;
using System.Web.Mvc;
using System.Net;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class FundsController : SuperController
    {
        // GET: Funds
        public ActionResult Index()
        {
            var model = new FundsViewModel
            {
                Funds = DbContext.Fund
                    .OrderBy(f => string.IsNullOrEmpty(f.SFN))  // Nulls first
                    .ThenBy(f => f.SFN)                         // Then by SFN value
                    .ThenBy(f => f.FundCode)
                    .ToList(),
                LaborTransactions = DbContext.GetLaborTransactions((int)LaborTransactionsOptions.AEFunds, 2025).ToList(),
                CodeTypeName = "Funds"
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fund = DbContext.Fund.Find(id);

            if (fund == null)
            {
                return HttpNotFound();
            }

            return View(fund);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fund,SFN")] Fund model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = DbContext.Fund.Find(model.FundCode);
            if (existing == null) return HttpNotFound();

            existing.SFN = model.SFN;

            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}