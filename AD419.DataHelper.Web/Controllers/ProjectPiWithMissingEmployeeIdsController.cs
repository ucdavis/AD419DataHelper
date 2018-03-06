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
    public class ProjectPiWithMissingEmployeeIdsController : SuperController
    {
        // GET: ProjectPiWithMissingEmployeeIds
        public ActionResult Index()
        {
            return View(DbContext.GetProjectPisWithMissingEmployeeId().ToList());
        }

        // GET: ProjectPiWithMissingEmployeeIds/Edit/5
        public ActionResult Edit(string orgR, string inv1)
        {
            if (string.IsNullOrWhiteSpace(orgR) || string.IsNullOrWhiteSpace(inv1))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projectPiWithMissingEmployeeId = DbContext.ProjectPis.Find(orgR, inv1);
            if (projectPiWithMissingEmployeeId == null)
            {
                return HttpNotFound();
            }
            return View(projectPiWithMissingEmployeeId);
        }

        // POST: ProjectPiWithMissingEmployeeIds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrgR,Inv1,PI,LastName,FirstInitial,EmployeeId,IsExistingRecord,LastUpdateDate")] ProjectPi projectPiWithMissingEmployeeId)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(projectPiWithMissingEmployeeId).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectPiWithMissingEmployeeId);
        }
    }
}
