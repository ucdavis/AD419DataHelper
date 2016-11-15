using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.Services;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class FieldStationExpensesImportController : SuperController
    {
        private readonly FieldStationExpenseService _fieldStationExpenseService;

        public FieldStationExpensesImportController()
        {
            _fieldStationExpenseService = new FieldStationExpenseService(DbContext, FiscalYearService.FiscalEndDate);
        }

        // GET: FieldStationExpensesImport
        public ActionResult Index()
        {
            var stations = DbContext.FieldStationExpenseListImports.ToList();
            return View(stations);
        }

        // GET: FieldStationExpensesImport/Details/5
        public ActionResult Details(int id)
        {
            var station = DbContext.FieldStationExpenseListImports.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }

            return View(station);
        }

        // GET: FieldStationExpensesImport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FieldStationExpensesImport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectAccessionNum,FieldStationCharge,ProjectDirector")] FieldStationExpenseListImport fieldStationExpenseListImport)
        {
            if (!ModelState.IsValid) return View(fieldStationExpenseListImport);

            DbContext.FieldStationExpenseListImports.Add(fieldStationExpenseListImport);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: FieldStationExpensesImport/Edit/5
        public ActionResult Edit(int id)
        {
            var station = DbContext.FieldStationExpenseListImports.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        // POST: FieldStationExpensesImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectAccessionNum,FieldStationCharge,ProjectDirector")] FieldStationExpenseListImport fieldStationExpenseListImport)
        {
            if (!ModelState.IsValid) return View(fieldStationExpenseListImport);

            DbContext.Entry(fieldStationExpenseListImport).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: FieldStationExpensesImport/Delete/5
        public ActionResult Delete(int id)
        {
            var station = DbContext.FieldStationExpenseListImports.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }

            return View(station);
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            // This works, but does not reset the sequence number back to the beginning.
            //var all = from c in db.FieldStationExpenseListImports select c;
            //db.FieldStationExpenseListImports.RemoveRange(all);
            //db.SaveChanges();

            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE FieldStationExpenseListImport");

            return RedirectToAction("Index");
        }

        // POST: FieldStationExpensesImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var station = DbContext.FieldStationExpenseListImports.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }

            DbContext.FieldStationExpenseListImports.Remove(station);
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
            excelReader.IsFirstRowAsColumnNames = true;

            // read data:
            var result = excelReader.AsDataSet();
            excelReader.Close();

            // transform:
            var fieldStationExpenses = _fieldStationExpenseService.GetFieldStationsExpensesFromRows(result.Tables[0].Rows).ToList();

            // validate:
            var errors = new List<ModelStateDictionary>();
            foreach (var fieldStationExpense in fieldStationExpenses)
            {
                // clear and check
                ModelState.Clear();
                TryValidateModel(fieldStationExpense);

                if ( !fieldStationExpense.IsCurrentAd419Project)
                {
                    ModelState.AddModelError("IsCurrentAd419Project", "This expense is not assigned to an active project!  Please select one that is not expired and is currently active.");
                }

                // copy out errors
                var state = new ModelStateDictionary();
                state.Merge(ModelState);
                errors.Add(state);
            }
            ViewBag.Errors = errors;

            //TempData.Add("Message", "Now viewing \"" + fileName + "\".");

            return PartialView("_UploadData", fieldStationExpenses);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<FieldStationExpenseListImport> fieldStationExpenseEntries)
        {
            if (fieldStationExpenseEntries != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid)
                {
                    DbContext.FieldStationExpenseListImports.AddRange(fieldStationExpenseEntries);
                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportFieldStationExpenses);
                    DbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
