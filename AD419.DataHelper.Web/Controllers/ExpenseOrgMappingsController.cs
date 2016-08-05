﻿using System;
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
    public class ExpenseOrgMappingsController : SuperController
    {
        // GET: ExpenseOrgMappings
        public ActionResult Index()
        {
            return View(DbContext.ExpenseOrgMappings.ToList());
        }

        // GET: ExpenseOrgMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }
            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExpenseOrgMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Chart,ExpenseOrgR,AD419OrgR")] ExpenseOrgMapping expenseOrgMapping)
        {
            if (ModelState.IsValid)
            {
                DbContext.ExpenseOrgMappings.Add(expenseOrgMapping);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }
            return View(expenseOrgMapping);
        }

        // POST: ExpenseOrgMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Chart,ExpenseOrgR,AD419OrgR")] ExpenseOrgMapping expenseOrgMapping)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(expenseOrgMapping).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseOrgMapping);
        }

        // GET: ExpenseOrgMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            if (expenseOrgMapping == null)
            {
                return HttpNotFound();
            }
            return View(expenseOrgMapping);
        }

        // POST: ExpenseOrgMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpenseOrgMapping expenseOrgMapping = DbContext.ExpenseOrgMappings.Find(id);
            DbContext.ExpenseOrgMappings.Remove(expenseOrgMapping);
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
