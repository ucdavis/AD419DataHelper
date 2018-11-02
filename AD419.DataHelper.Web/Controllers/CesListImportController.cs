using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.Services;
using ExcelDataReader;

namespace AD419.DataHelper.Web.Controllers
{
    public class CesListImportController : SuperController
    {
        private readonly CeSpecialistService _ceSpecialistService;

        public CesListImportController()
        {
            _ceSpecialistService = new CeSpecialistService(DbContext, FiscalYearService.FiscalEndDate);
        }

        // GET: CesListImport
        public ActionResult Index()
        {
            var imports = DbContext.CesListImports.ToList();
            return View(imports);
        }

        // GET: CesListImport/Details/5
        public ActionResult Details(int id)
        {
            var import = DbContext.CesListImports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            return View(import);
        }

        // GET: CesListImport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CesListImport/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PI,DeptLevelOrg,EmployeeId,ProjectAccessionNum,ProjectNumber,PercentCeEffort,FullAnnualPayRate,TitleCode,FTE,Chart,Account,SubAccount,EstimatedCeExpenses")] CesListImport cesListImport)
        {
            if (!ModelState.IsValid) return View(cesListImport);

            DbContext.CesListImports.Add(cesListImport);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CesListImport/Edit/5
        public ActionResult Edit(int id)
        {
            var import = DbContext.CesListImports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            return View(import);
        }

        // POST: CesListImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PI,DeptLevelOrg,EmployeeId,ProjectAccessionNum,ProjectNumber,PercentCeEffort,FullAnnualPayRate,TitleCode,FTE,Chart,Account,SubAccount,EstimatedCeExpenses")] CesListImport cesListImport)
        {
            if (!ModelState.IsValid) return View(cesListImport);

            DbContext.Entry(cesListImport).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CesListImport/Delete/5
        public ActionResult Delete(int id)
        {
            var import = DbContext.CesListImports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            return View(import);
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            //This works but doesn't reset the sequence number.
            //var all = from c in db.CesListImports select c;
            //db.CesListImports.RemoveRange(all);
            //db.SaveChanges();

            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE CesListImport");

            // 1. Use the hard-coded status Id to reset ProcessStatus.IsCompleted.
            DbContext.ClearStatusCompleted(ProcessStatuses.ImportCeSpecialists);

            // 2. Get it's categoryID and reset ProcessCategory.IsCompleted to false.
            DbContext.ClearCategoryCompleted(ProcessStatuses.ImportCeSpecialists);

            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: CesListImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var import = DbContext.CesListImports.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            DbContext.CesListImports.Remove(import);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            var myFile = Request.Files[0];
            if (myFile == null) return RedirectToAction("Index");

            var fileName = myFile.FileName;

            // setup reader:
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(myFile.InputStream);

            // read data:
            var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            excelReader.Close();

            // transform:
            var ceSpecialists = _ceSpecialistService.GetCesListImportFromRows(result.Tables[0].Rows).ToList();

            // validate:
            var errors = new List<ModelStateDictionary>();
            foreach (var ceSpecialist in ceSpecialists)
            {
                // clear and check
                ModelState.Clear();
                TryValidateModel(ceSpecialist);

                if (!ceSpecialist.IsCurrentAd419Project)
                {
                    ModelState.AddModelError("IsCurrentAd419Project", "This expense is not assigned to an active project.");
                }

                // copy out errors
                var state = new ModelStateDictionary();
                state.Merge(ModelState);
                errors.Add(state);
            }
            ViewBag.Errors = errors;
   
            //TempData.Add("Message", "Now viewing \"" + fileName + "\".");

            return PartialView("_UploadData", ceSpecialists);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<CesListImport> cesEntries)
        {
            if (cesEntries != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid && cesEntries.All(f => f.IsCurrentAd419Project))
                {
                    DbContext.CesListImports.AddRange(cesEntries);
                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportCeSpecialists);
                    DbContext.MarkCategoryCompleted(ProcessStatuses.ImportCeSpecialists);

                    // We also need to clear the next category and process so that they 
                    // can be re-inserted in the expenses table as the new values.
                    DbContext.ClearStatusCompletedForNextCategory(ProcessStatuses.ImportCeSpecialists);
                    DbContext.ClearCategoryCompletedForNextCategory(ProcessStatuses.ImportCeSpecialists);

                    DbContext.SaveChanges();
                }
                else
                {
                    ErrorMessage = string.Format("ERROR! Your import file could not be saved.  It contained {0} records with expired projects that could not be automatically remapped.  Please make corrections and try again.", cesEntries.Count(f => f.IsCurrentAd419Project == false));
                }
            }
            return RedirectToAction("Index");
        }
    }
}
