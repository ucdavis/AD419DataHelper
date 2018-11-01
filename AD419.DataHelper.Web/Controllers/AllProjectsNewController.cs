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
    public class AllProjectsNewController : SuperController
    {
        private readonly ProjectImportService _projectImportService;

        public AllProjectsNewController()
        {
            _projectImportService = new ProjectImportService(DbContext, FiscalYearService.FiscalStartDate);
        }

        // GET: AllProjectsNew
        public ActionResult Index()
        {
            var start = FiscalYearService.FiscalStartDate;  
            var end = FiscalYearService.FiscalEndDate;
            var sevenMonthsPriorToStart = start.AddMonths(-7);

            // Projects where project has started (project start date is < end date)  
            // and has not ended or whose end date is no earlier than 7 months prior to the start date (project end date >= 7 months to start date):
            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectEndDate >= sevenMonthsPriorToStart)
                .Where(p => p.ProjectStartDate <  end)
                
                .ToList();

            return View(projects);
        }

        public ActionResult FindByDirector(string director)
        {
            var start = FiscalYearService.FiscalStartDate;
            var end = FiscalYearService.FiscalEndDate;

            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectEndDate >= start) //project hasn't ended yet
                .Where(p => p.ProjectStartDate < end) //project has actually started
                .Where(p => p.ProjectDirector.Equals(director))
                .Where(p => !p.ProjectStatus.Equals("Unknown"));

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
                .Where(p => p.ProjectEndDate >= start) //project hasn't ended yet
                .Where(p => p.ProjectStartDate < end) //project has actually started
                .Where(p => p.OrgR.Equals(organization))
                .Where(p => !p.ProjectStatus.Equals("Unknown")); 

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

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[AllProjectsNew]");

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
        public ActionResult Save(IEnumerable<AllProjectsNew> projects)
        {
            if (projects == null)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Data upload had errors";
                return RedirectToAction("Index");
            }

            DbContext.AllProjectsNew.AddRange(projects);
            DbContext.MarkStatusCompleted(ProcessStatuses.ImportProjects);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
