using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD_419_DataHelperWebApp.Models;
using Excel;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class CFDANumsImportController : SuperController
    {
        // GET: CFDANumsImport
        public ActionResult Index()
        {
            return View(DbContext.CFDANumImports.ToList());
        }

        // GET: CFDANumsImport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CFDANumImport cFDANumImport = DbContext.CFDANumImports.Find(id);
            if (cFDANumImport == null)
            {
                return HttpNotFound();
            }
            return View(cFDANumImport);
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
        public ActionResult Create([Bind(Include = "Id,CFDANum")] CFDANumImport cFDANumImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.CFDANumImports.Add(cFDANumImport);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cFDANumImport);
        }

        // GET: CFDANumsImport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CFDANumImport cFDANumImport = DbContext.CFDANumImports.Find(id);
            if (cFDANumImport == null)
            {
                return HttpNotFound();
            }
            return View(cFDANumImport);
        }

        // POST: CFDANumsImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CFDANum, ProgramTitle")] CFDANumImport cFDANumImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(cFDANumImport).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cFDANumImport);
        }

        // GET: CFDANumsImport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CFDANumImport cFDANumImport = DbContext.CFDANumImports.Find(id);
            if (cFDANumImport == null)
            {
                return HttpNotFound();
            }
            return View(cFDANumImport);
        }

        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAll()
        {
            //This works but doesn't reset the sequence number.
            //var all = from c in db.CFDANumImport select c;
            //db.CFDANumImport.RemoveRange(all);
            //db.SaveChanges();

            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE CFDANumImport");

            return RedirectToAction("Index");
        }

        // POST: CFDANumsImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CFDANumImport cFDANumImport = DbContext.CFDANumImports.Find(id);
            DbContext.CFDANumImports.Remove(cFDANumImport);
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

            var cfdaNums = new List<CFDANumImport>();
            if (myFile != null)
            {
                var fileName = myFile.FileName;
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(myFile.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
                var result = excelReader.AsDataSet();
               
                TempData.Add("Message", "Now viewing \"" + fileName + "\".");
                excelReader.Close();

                cfdaNums.AddRange(from DataRow row in result.Tables[0].Rows select new CFDANumImport(row));

                // This works.  Would now like the user to have a chance to review upload first.
                //if (ModelState.IsValid)
                //{
                //    db.CesListImports.AddRange(cesEntries);
                //    db.SaveChanges();
                //}

                return View("Display", cfdaNums);
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<AD_419_DataHelperWebApp.Models.CFDANumImport> cfdaNums)
        {
            if (cfdaNums != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid)
                {
                    DbContext.CFDANumImports.AddRange(cfdaNums);
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
            const string filePathAndFilename = @"~\Sample_Forms\CfdaNumbersForImport.xlsx";
            const string contentType = "application/text";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filePathAndFilename, contentType, "SampleCfdaNumbers.xlsx");
        }
    }
}
