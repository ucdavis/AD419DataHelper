using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class AD419AccountController : SuperController
    {
        // GET: AD419Account
        public ActionResult Index()
        {
            return View(DbContext.Ad419Accounts.ToList());
        }

        // GET: AD419Account/Details/5
        public ActionResult Details(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AD419Account aD419Account = DbContext.Ad419Accounts.Find(chart, account);
            if (aD419Account == null)
            {
                return HttpNotFound();
            }
            return View(aD419Account);
        }

        // GET: AD419Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AD419Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Chart,Account,Org,OrgR,Expenses,SFN,LatestNonClosedYearPeriod,IsExpired,HaveOrgBeenAdjusted")] AD419Account aD419Account)
        {
            if (ModelState.IsValid)
            {
                DbContext.Ad419Accounts.Add(aD419Account);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aD419Account);
        }

        // GET: AD419Account/Edit/5
        public ActionResult Edit(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AD419Account aD419Account = DbContext.Ad419Accounts.Find(chart, account);
            if (aD419Account == null)
            {
                return HttpNotFound();
            }
            return View(aD419Account);
        }

        // POST: AD419Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Chart,Account,Org,OrgR,Expenses,SFN,LatestNonClosedYearPeriod,IsExpired,HaveOrgBeenAdjusted")] AD419Account aD419Account)
        {
            var myAd419Account = DbContext.Ad419Accounts.Find(aD419Account.Chart, aD419Account.Account);
            var accountWithTheWrongClosedOrg = DbContext.AccountWithTheWrongClosedOrg.Find(aD419Account.Chart, aD419Account.Account);
            if (myAd419Account == null || accountWithTheWrongClosedOrg == null)
            {
                return HttpNotFound();
            }

            if (aD419Account.OrgR.Equals(accountWithTheWrongClosedOrg.CurrentOrgR))
            {
                // Then just set the flag to indicate that the account has been reviewed
                myAd419Account.HaveOrgsBeenAdjusted = false;
            }
            else
            {
                // AD-419 Account's Org/OrgR needs to be updated:

                myAd419Account.HaveOrgsBeenAdjusted = true;
                myAd419Account.OrgR = aD419Account.OrgR;

                myAd419Account.Org = aD419Account.OrgR.Equals(accountWithTheWrongClosedOrg.CurrentOrgR)
                    ? accountWithTheWrongClosedOrg.CurrentOrg
                    : accountWithTheWrongClosedOrg.LatestNonClosedOrg;
            }

            if (ModelState.IsValid)
            {
                DbContext.Entry(myAd419Account).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index","AccountWithTheWrongClosedOrg");
            }
            return View(aD419Account);
        }

        // GET: AD419Account/Delete/5
        public ActionResult Delete(string chart, string account)
        {
            if (string.IsNullOrWhiteSpace(chart) || string.IsNullOrWhiteSpace(account))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AD419Account aD419Account = DbContext.Ad419Accounts.Find(chart, account);
            if (aD419Account == null)
            {
                return HttpNotFound();
            }
            return View(aD419Account);
        }

        // POST: AD419Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string chart, string account)
        {
            AD419Account aD419Account = DbContext.Ad419Accounts.Find(chart, account);
            DbContext.Ad419Accounts.Remove(aD419Account);
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
