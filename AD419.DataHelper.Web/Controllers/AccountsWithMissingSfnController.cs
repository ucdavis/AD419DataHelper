using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class AccountsWithMissingSfnController : SuperController
    {
        // GET: AccountsWithMissingSfn
        public ActionResult Index()
        {
            return View(DbContext.GetAccountsWithMissingSfn().ToList());
        }

        // GET: AccountsWithMissingSfn/Edit/5
        public ActionResult Edit(string Chart, string Account)
        {
            if (string.IsNullOrWhiteSpace(Chart) || string.IsNullOrWhiteSpace(Account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountWithMissingSfn = DbContext.NewAccountSfns.Find(Chart, Account);
            if (accountWithMissingSfn == null)
            {
                return HttpNotFound();
            }
            return View(accountWithMissingSfn);
        }

        // POST: AccountsWithMissingSfn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sfn")] NewAccountSfn accountWithMissingSfn)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(accountWithMissingSfn).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountWithMissingSfn);
        }
    }
}
