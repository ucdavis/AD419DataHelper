using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.Services;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class AllProjectsNewController : SuperController
    {
        private readonly ProjectImportService _projectImportService;

        public AllProjectsNewController()
        {
            _projectImportService = new ProjectImportService(DbContext);
        }

        // GET: AllProjectsNew
        public ActionResult Index()
        {
            var start = FiscalYearService.FiscalStartDate;
            var end = FiscalYearService.FiscalEndDate;

            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate >= start)
                .Where(p => p.ProjectEndDate >= end)
                .ToList();

            return View(projects);
        }

        public ActionResult FindByDirector(string director)
        {
            var start = FiscalYearService.FiscalStartDate;
            var end = FiscalYearService.FiscalEndDate;

            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate <= end) //project has actually started
                .Where(p => p.ProjectEndDate >= start) //project hasn't ended yet
                .Where(p => p.ProjectDirector.Equals(director));

            var results = new
            {
                data = projects.ToList().Select(p => new 
                {
                    p.ProjectDirector,
                    p.Title,
                    ProjectEndDate = p.ProjectEndDate.HasValue ? p.ProjectEndDate.Value.ToShortDateString() : "",
                    p.ProjectNumber,
                    p.AccessionNumber
                })
            };

            return new JsonResult() { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult FindByOrganization(string organization)
        {
            var start = FiscalYearService.FiscalStartDate;
            var end = FiscalYearService.FiscalEndDate;

            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate <= end) //project has actually started
                .Where(p => p.ProjectEndDate >= start) //project hasn't ended yet
                .Where(p => p.OrgR.Equals(organization));

            var results = new
            {
                data = projects.ToList().Select(p => new
                {
                    p.ProjectDirector,
                    p.Title,
                    ProjectEndDate = p.ProjectEndDate.HasValue ? p.ProjectEndDate.Value.ToShortDateString() : "",
                    p.ProjectNumber,
                    p.AccessionNumber
                })
            };

            return new JsonResult() { Data = results, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: AllProjectsNew/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: AllProjectsNew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllProjectsNew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.AllProjectsNew.Add(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.AllProjectsNew.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Upload()
        {
            var errors = ImportErrors;
            if (errors != null)
            {
                ErrorMessage = "Your upload file has errors.";
                ModelState.Merge(ImportErrors);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Save(IEnumerable<AllProjectsNew> projects)
        {
            if (projects == null)
                return RedirectToAction("Upload");

            if (!ModelState.IsValid)
            {
                ImportErrors = ModelState;
                return RedirectToAction("Upload");
            }

            DbContext.AllProjectsNew.AddRange(projects);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private ModelStateDictionary ImportErrors
        {
            get
            {
                return TempData["ImportErrors"] as ModelStateDictionary;
            }
            set
            {
                TempData["ImportErrors"] = value;
            }
        }
    }
}
