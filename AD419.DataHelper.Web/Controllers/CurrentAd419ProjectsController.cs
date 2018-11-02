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
    public class CurrentAd419ProjectsController : SuperController
    {
        private readonly ProjectImportService _projectImportService;

        public CurrentAd419ProjectsController()
        {
            _projectImportService = new ProjectImportService(DbContext, FiscalYearService.FiscalStartDate);
        }

        // GET: Projects
        public ActionResult Index()
        {
            var projects = DbContext.CurrentAd419Projects.ToList();
            return View(projects);
        }

        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.CurrentAd419Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,IsInterdepartmental,IsUCD,Is204,IsExpired")] CurrentAd419Project currentProject)
        {
            if (!ModelState.IsValid) return View(currentProject);

            DbContext.CurrentAd419Projects.Add(currentProject);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,IsInterdepartmental,IsUCD,Is204,IsExpired")] CurrentAd419Project currentProject)
        {
            if (!ModelState.IsValid) return View(currentProject);

            DbContext.Entry(currentProject).State = EntityState.Modified;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.Projects.Remove(project);
            DbContext.SaveChanges();

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
        public ActionResult Save(IEnumerable<CurrentAd419Project> projects)
        {
            if (projects == null)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data upload had errors";
                return RedirectToAction("Index");
            }

            DbContext.CurrentAd419Projects.AddRange(projects);
            DbContext.MarkStatusCompleted(ProcessStatuses.ImportProjects);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AccessionNumbers()
        {
            var accessions = DbContext.CurrentAd419Projects
                .Select(p => p.AccessionNumber)
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = accessions,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult FindAccessionNumbers(string query)
        {
            var accessions = DbContext.CurrentAd419Projects
                .Select(p => p.AccessionNumber)
                .Where(a => a.Contains(query))
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = accessions,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult ProjectNumbers()
        {
            var projects = DbContext.CurrentAd419Projects
                .Select(p => p.ProjectNumber)
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data =  projects,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult FindProjectNumbers(string query)
        {
            var projects = DbContext.CurrentAd419Projects
                .Select(p => p.ProjectNumber)
                .Where(p => p.Contains(query))
                .Distinct()
                .ToList();

            return new JsonResult()
            {
                Data = projects,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
