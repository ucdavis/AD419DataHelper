using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class AccountWithTheWrongClosedOrgController : SuperController { 
    
        // GET: AccountWithTheWrongClosedOrg
        public ActionResult Index()
        {
            return View(DbContext.AccountWithTheWrongClosedOrg.ToList());
        }

        // GET: AccountWithTheWrongClosedOrg/Details/5
        public ActionResult Details(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg = DbContext.GetAccountWithTheWrongClosedOrg(chart, account).FirstOrDefault();
            if (accountWithTheWrongClosedOrg == null)
            {
                return HttpNotFound();
            }
            return View(accountWithTheWrongClosedOrg);
        }

        // GET: AccountWithTheWrongClosedOrg/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountWithTheWrongClosedOrg/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Chart,Account,CurrentOrg,LatestNonClosedOrg,CurrentOrgR,LatestNonClosedOrgR,LatestNonClosedYearPeriod,AccountName,AccountPurpose,CurrentOrgName,LatestNonClosedOrgName,LatestNonClosedHomeDepartment,Expenses")] AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg)
        {
            if (ModelState.IsValid)
            {
                DbContext.AccountWithTheWrongClosedOrg.Add(accountWithTheWrongClosedOrg);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountWithTheWrongClosedOrg);
        }

        // GET: AccountWithTheWrongClosedOrg/Edit/5
        public ActionResult Edit(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AD419Account aD419Account = DbContext.Ad419Accounts.Find(chart, account);
            AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg = DbContext.GetAccountWithTheWrongClosedOrg(chart, account).FirstOrDefault();
            if (accountWithTheWrongClosedOrg == null)
            {
                return HttpNotFound();
            }
            var viewModel = new AccountsWithWithTheWrongOrgRViewModel(accountWithTheWrongClosedOrg, aD419Account);
            return View(viewModel);
        }

        // POST: AccountWithTheWrongClosedOrg/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Chart,Account,CurrentOrg,LatestNonClosedOrg,CurrentOrgR,LatestNonClosedOrgR,LatestNonClosedYearPeriod,AccountName,AccountPurpose,CurrentOrgName,LatestNonClosedOrgName,LatestNonClosedHomeDepartment,Expenses")] AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(accountWithTheWrongClosedOrg).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountWithTheWrongClosedOrg);
        }

        // GET: AccountWithTheWrongClosedOrg/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg = DbContext.AccountWithTheWrongClosedOrg.Find(id);
            if (accountWithTheWrongClosedOrg == null)
            {
                return HttpNotFound();
            }
            return View(accountWithTheWrongClosedOrg);
        }

        // POST: AccountWithTheWrongClosedOrg/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrgDetails = DbContext.AccountWithTheWrongClosedOrg.Find(id);
            DbContext.AccountWithTheWrongClosedOrg.Remove(accountWithTheWrongClosedOrgDetails);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
