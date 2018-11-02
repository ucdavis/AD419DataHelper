using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using ExcelDataReader;

namespace AD419.DataHelper.Web.Controllers
{
    public class CfdaNumberImportsController : SuperController
    {
        // GET: CFDANumsImport
        public ActionResult Index()
        {
            return View(DbContext.CfdaNumberImports.ToList());
        }

        // GET: CFDANumsImport/Details/5
        public ActionResult Details(int id)
        {
            var num = DbContext.CfdaNumberImports.Find(id);
            if (num == null)
            {
                return HttpNotFound();
            }

            return View(num);
        }

        // GET: CFDANumsImport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CFDANumsImport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number, ProgramTitle, AgencyOffice, Code")] CfdaNumberImport cfdaNumber)
        {
            if (!ModelState.IsValid) return View(cfdaNumber);

            DbContext.CfdaNumberImports.Add(cfdaNumber);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CFDANumsImport/Edit/5
        public ActionResult Edit(int id)
        {
            var code = DbContext.CfdaNumberImports.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // POST: CFDANumsImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number, ProgramTitle, AgencyOffice, Code")] CfdaNumberImport cFdaNumberImport)
        {
            if (!ModelState.IsValid) return View(cFdaNumberImport);

            DbContext.Entry(cFdaNumberImport).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CFDANumsImport/Delete/5
        public ActionResult Delete(int id)
        {
            var num = DbContext.CfdaNumberImports.Find(id);
            if (num == null)
            {
                return HttpNotFound();
            }

            return View(num);
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            //This works but doesn't reset the sequence number.
            //var all = from c in db.CFDANumImport select c;
            //db.CFDANumImport.RemoveRange(all);
            //db.SaveChanges();

            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [dbo].[CFDANumImport]");

            AddNifaDefaultCfdaNum();

            return RedirectToAction("Index");
        }

        // POST: CFDANumsImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var num = DbContext.CfdaNumberImports.Find(id); if (num == null)
            {
                return HttpNotFound();
            }

            DbContext.CfdaNumberImports.Remove(num);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            var myFile = Request.Files[0];
            if (myFile == null) return RedirectToAction("Index");

            var cfdaNums = new List<CfdaNumberImport>();
            var fileName = myFile.FileName;
            var excelReader = ExcelReaderFactory.CreateCsvReader(myFile.InputStream);

            var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });

            TempData.Add("Message", "Now viewing \"" + fileName + "\".");
            excelReader.Close();

            cfdaNums.AddRange(from DataRow row in result.Tables[0].Rows select new CfdaNumberImport(row));

            // This works.  Would now like the user to have a chance to review upload first.
            //if (ModelState.IsValid)
            //{
            //    db.CesListImports.AddRange(cesEntries);
            //    db.SaveChanges();
            //}

            return View("Display", cfdaNums);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<CfdaNumberImport> cfdaNums)
        {
            if (cfdaNums != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid)
                {
                    DbContext.CfdaNumberImports.AddRange(cfdaNums);
                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportCfdaNumbers);
                    DbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        private void AddNifaDefaultCfdaNum()
        {
            var defaultNifaCfdaNum = new CfdaNumberImport()
            {
                AgencyOffice = "NATIONAL INSTITUTE OF FOOD AND AGRICULTURE, AGRICULTURE, DEPARTMENT OF",
                Number = "10.000", Code = "NIFA", ProgramTitle = "MISC ASSIGNED IN SPONSORED PROGRAMS"
            };
            DbContext.CfdaNumberImports.Add(defaultNifaCfdaNum);
            DbContext.SaveChanges();

        }
    }
}
