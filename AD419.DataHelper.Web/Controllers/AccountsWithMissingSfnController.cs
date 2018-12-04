using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class AccountsWithMissingSfnController : SuperController
    {
        // GET: AccountsWithMissingSfn
        public ActionResult Index()
        {
            return View(DbContext.GetAccountsWithMissingSfn().ToList());
        }

        public ActionResult Details(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                var accountDetails = DbContext.AccountsWithMissingSfns.Find(chart, account);

            return View(accountDetails);
        }

        // GET: AccountsWithMissingSfn/Edit/5
        public ActionResult Edit(string Chart, string Account)
        {
            if (string.IsNullOrWhiteSpace(Chart) || string.IsNullOrWhiteSpace(Account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountWithMissingSfn = DbContext.AccountsWithMissingSfns.Find(Chart, Account);
            if (accountWithMissingSfn == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AccountsWithMissingSfnEditViewModel(accountWithMissingSfn, DbContext.SfnListItems.ToList());
            return View(viewModel);
        }

        // POST: AccountsWithMissingSfn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Chart, Account, Sfn")] NewAccountSfn newAccountSfn)
        {
            if (ModelState.IsValid)
            {
                var currentRecord = DbContext.NewAccountSfns.Find(newAccountSfn.Chart, newAccountSfn.Account);
                if (currentRecord == null)
                {
                    return HttpNotFound();
                }
                currentRecord.SFN = newAccountSfn.SFN;

                DbContext.Entry(currentRecord).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newAccountSfn);
        }
    }
}
