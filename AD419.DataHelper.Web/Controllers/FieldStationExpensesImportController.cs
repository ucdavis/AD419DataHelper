using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class FieldStationExpensesImportController : SuperController
    {
        // GET: FieldStationExpensesImport
        public ActionResult Index()
        {
            return View(DbContext.FieldStationExpenseListImports.ToList());
        }

        // GET: FieldStationExpensesImport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStationExpenseListImport fieldStationExpenseListImport = DbContext.FieldStationExpenseListImports.Find(id);
            if (fieldStationExpenseListImport == null)
            {
                return HttpNotFound();
            }
            return View(fieldStationExpenseListImport);
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
            if (ModelState.IsValid)
            {
                DbContext.FieldStationExpenseListImports.Add(fieldStationExpenseListImport);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fieldStationExpenseListImport);
        }

        // GET: FieldStationExpensesImport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStationExpenseListImport fieldStationExpenseListImport = DbContext.FieldStationExpenseListImports.Find(id);
            if (fieldStationExpenseListImport == null)
            {
                return HttpNotFound();
            }
            return View(fieldStationExpenseListImport);
        }

        // POST: FieldStationExpensesImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectAccessionNum,FieldStationCharge,ProjectDirector")] FieldStationExpenseListImport fieldStationExpenseListImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(fieldStationExpenseListImport).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fieldStationExpenseListImport);
        }

        // GET: FieldStationExpensesImport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FieldStationExpenseListImport fieldStationExpenseListImport = DbContext.FieldStationExpenseListImports.Find(id);
            if (fieldStationExpenseListImport == null)
            {
                return HttpNotFound();
            }
            return View(fieldStationExpenseListImport);
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
            FieldStationExpenseListImport fieldStationExpenseListImport = DbContext.FieldStationExpenseListImports.Find(id);
            DbContext.FieldStationExpenseListImports.Remove(fieldStationExpenseListImport);
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

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            var myFile = Request.Files[0];

            var fieldStationExpenseEntries = new List<FieldStationExpenseListImport>();
            if (myFile != null)
            {
                var fileName = myFile.FileName;
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(myFile.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
                var result = excelReader.AsDataSet();

                TempData.Add("Message", "Now viewing \"" + fileName + "\".");
                excelReader.Close();
                var range = from DataRow row in result.Tables[0].Rows where !string.IsNullOrWhiteSpace(row["FieldStationCharge"].ToString()) select new FieldStationExpenseListImport(row);
                fieldStationExpenseEntries.AddRange(range);

                // This works.  Would now like the user to have a chance to review upload first.
                //if (ModelState.IsValid)
                //{
                //    db.FieldStationExpenseListImport.AddRange(fieldStationExpenseEntries);
                //    db.SaveChanges();
                //}

                return View("Display", fieldStationExpenseEntries);
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
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
                    DbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        [AcceptVerbs(HttpVerbs.Get)]
        public FileResult Download(string id)
        {
            const string filePathAndFilename = @"~\Sample_Forms\ModifiedFieldStationExpensesForImport.xlsx";
            const string contentType = "application/text";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filePathAndFilename, contentType, "SampleFieldStationExpenses.xlsx");
        }
    }
}
