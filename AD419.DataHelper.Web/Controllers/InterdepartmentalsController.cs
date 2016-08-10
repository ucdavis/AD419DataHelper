using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class InterdepartmentalsController : SuperController
    {
        // GET: Interdepartmentals
        public ActionResult Index()
        {
            var year = FiscalYearService.FiscalYear;

            var model = DbContext.Interdepartmentals
                .Where(i => i.Year == year)
                .ToList();

            return View(model);
        }

        // GET: Interdepartmentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var interdepartmental = DbContext.Interdepartmentals.Find(id);
            if (interdepartmental == null)
                return HttpNotFound();

            return View(interdepartmental);
        }

        // GET: Interdepartmentals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Interdepartmentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessionNumber,OrgR,Year")] Interdepartmental interdepartmental)
        {
            if (!ModelState.IsValid)
                return View(interdepartmental);

            DbContext.Interdepartmentals.Add(interdepartmental);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Interdepartmentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var interdepartmental = DbContext.Interdepartmentals.Find(id);
            if (interdepartmental == null)
                return HttpNotFound();

            return View(interdepartmental);
        }

        // POST: Interdepartmentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "AccessionNumber,OrgR,Year")] Interdepartmental interdepartmental)
        {
            if (!ModelState.IsValid)
                return View(interdepartmental);

            DbContext.Entry(interdepartmental).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Interdepartmentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var interdepartmental = DbContext.Interdepartmentals.Find(id);
            if (interdepartmental == null)
                return HttpNotFound();

            return View(interdepartmental);
        }

        // POST: Interdepartmentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var interdepartmental = DbContext.Interdepartmentals.Find(id);

            DbContext.Interdepartmentals.Remove(interdepartmental);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            var year = FiscalYearService.FiscalYear;

            var target = DbContext.Interdepartmentals
                .Where(i => i.Year == year);

            DbContext.Interdepartmentals.RemoveRange(target);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // setup reader
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
            excelReader.IsFirstRowAsColumnNames = true;

            // read data
            var result = excelReader.AsDataSet();
            excelReader.Close();

            // transform
            var year = FiscalYearService.FiscalYear;
            var data = result.Tables[0].Rows
                .ToEnumerable()
                .Select(r => new Interdepartmental()
                {
                    Year            = year,
                    OrgR            = r["OrgR"].ToString(),
                    AccessionNumber = r["AccessionNumber"].ToString()
                });

            return PartialView("_uploadData", data.ToList());
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<Interdepartmental> list)
        {
            if (list == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                DbContext.Interdepartmentals.AddRange(list);
                DbContext.SaveChanges();
            }
            catch (DbUpdateException dbEx)
            {
                // dig into the exception
                if (dbEx.InnerException == null || dbEx.InnerException.InnerException == null) throw;

                // look for the unique constraint error code
                var sqlEx = dbEx.InnerException.InnerException as SqlException;
                if (sqlEx == null || sqlEx.Number != 2627) throw;

                ErrorMessage = "You data contains duplicates with the database. Please delete all records and re-check your document for duplicates.";
            }

            return RedirectToAction("Index");
        }
    }
}
