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
using AD419.DataHelper.Web.Services;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class InterdepartmentalsController : SuperController
    {
        private readonly InterdepartmentalProjectService _InterdepartmentalProjectService;

        public InterdepartmentalsController()
        {
            _InterdepartmentalProjectService = new InterdepartmentalProjectService(DbContext, FiscalYearService.FiscalEndDate);
        }

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
            var interdepartmentalProjects = _InterdepartmentalProjectService.GetInterdepartmentalProjectsFromRows(result.Tables[0].Rows).ToList();
            //var year = FiscalYearService.FiscalYear;
            //var data = result.Tables[0].Rows
            //    .ToEnumerable()
            //    .Select(r => new Interdepartmental()
            //    {
            //        Year            = year,
            //        OrgR            = r["OrgR"].ToString(),
            //        AccessionNumber = r["AccessionNumber"].ToString()
            //    });

            // validate:
            var errors = new List<ModelStateDictionary>();
            foreach (var interdepartmentProject in interdepartmentalProjects)
            {
                // clear and check
                ModelState.Clear();
                TryValidateModel(interdepartmentalProjects);

                if (!interdepartmentProject.IsCurrentAd419Project)
                {
                    ModelState.AddModelError("IsCurrentAd419Project", "This entry is not assigned to an active project.");
                }

                if (!interdepartmentProject.IsValidOrgR)
                {
                    ModelState.AddModelError("IsValidOrgR", "This entry is not assigned to an active OrgR.");
                }

                if (!interdepartmentProject.IsPresentInFile)
                {
                    ModelState.AddModelError("IsPresentInFile", "There is no entry present for this interdepartmental project.");
                }

                // copy out errors
                var state = new ModelStateDictionary();
                state.Merge(ModelState);
                errors.Add(state);
            }
            ViewBag.Errors = errors;

            //return PartialView("_uploadData", data.ToList());
            return PartialView("_uploadData", interdepartmentalProjects);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<Interdepartmental> list)
        {
            if (list == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                if (ModelState.IsValid && list.All(f => f.IsCurrentAd419Project && f.IsPresentInFile && f.IsValidOrgR))
                {
                    DbContext.Interdepartmentals.AddRange(list);
                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportInterdepartmentalProjects);
                    DbContext.SaveChanges();
                }
                else
                {
                    TempData.Add("ErrorMessage", string.Format("ERROR! Your import file could not be saved.  " +
                                                               "It contained {0} records with expired projects that could not be automatically remapped, " +
                                                               "{1} entries with invalid OrgRs, and " +
                                                               "{2} interdepartmental projects that were missing from the upload file.  " +
                                                               "Please make corrections and try again.", 
                                                               list.Count(i => i.IsCurrentAd419Project == false),
                                                               list.Count(i => i.IsValidOrgR == false),
                                                               list.Count(i => i.IsPresentInFile == false)));
                }
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
