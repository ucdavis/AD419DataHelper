using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.Services;
using ExcelDataReader;

namespace AD419.DataHelper.Web.Controllers
{
    public class NifaProjectAccessionNumberImportsController : SuperController
    {
        private readonly NifaProjectAccessionNumberImportService _projectImportService;
        public NifaProjectAccessionNumberImportsController()
        {
            _projectImportService = new NifaProjectAccessionNumberImportService(DbContext);
        }

        // GET: NifaProjectAccessionNumberImports
        public ActionResult Index()
        {
            return View(DbContext.NifaProjectAccessionNumberImports.ToList());
        }

        // GET: NifaProjectAccessionNumberImports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport = DbContext.NifaProjectAccessionNumberImports.Find(id);
            if (nifaProjectAccessionNumberImport == null)
            {
                return HttpNotFound();
            }
            return View(nifaProjectAccessionNumberImport);
        }

        // GET: NifaProjectAccessionNumberImports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NifaProjectAccessionNumberImports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectNumber,AccessionNumber,UcpEmployeeId,Notes")] NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.NifaProjectAccessionNumberImports.Add(nifaProjectAccessionNumberImport);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nifaProjectAccessionNumberImport);
        }

        // GET: NifaProjectAccessionNumberImports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport = DbContext.NifaProjectAccessionNumberImports.Find(id);
            if (nifaProjectAccessionNumberImport == null)
            {
                return HttpNotFound();
            }
            return View(nifaProjectAccessionNumberImport);
        }

        // POST: NifaProjectAccessionNumberImports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectNumber,AccessionNumber,UcpEmployeeId,Notes")] NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(nifaProjectAccessionNumberImport).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nifaProjectAccessionNumberImport);
        }

        // GET: NifaProjectAccessionNumberImports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport = DbContext.NifaProjectAccessionNumberImports.Find(id);
            if (nifaProjectAccessionNumberImport == null)
            {
                return HttpNotFound();
            }
            return View(nifaProjectAccessionNumberImport);
        }

        // POST: NifaProjectAccessionNumberImports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NifaProjectAccessionNumberImport nifaProjectAccessionNumberImport = DbContext.NifaProjectAccessionNumberImports.Find(id);
            DbContext.NifaProjectAccessionNumberImports.Remove(nifaProjectAccessionNumberImport);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE NifaProjectAccessionNumberImport");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // setup reader
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);

            // read data
            var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            excelReader.Close();

            // transform
            var projects = _projectImportService.GetProjectsFromRows(result.Tables[0].Rows).ToList();

            // validate
            var errors = new List<ModelStateDictionary>();
            foreach (var project in projects)
            {
                // clear and check
                ModelState.Clear();
                TryValidateModel(project);

                // copy out errors
                var state = new ModelStateDictionary();
                state.Merge(ModelState);
                errors.Add(state);
            }
            ViewBag.Errors = errors;

            return PartialView("_uploadData", projects);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(IEnumerable<NifaProjectAccessionNumberImport> projects)
        {
            if (projects == null)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data upload had errors";
                return RedirectToAction("Index");
            }

            DbContext.NifaProjectAccessionNumberImports.AddRange(projects);
            DbContext.MarkStatusCompleted(ProcessStatuses.ImportCurrentAd419Projects);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
