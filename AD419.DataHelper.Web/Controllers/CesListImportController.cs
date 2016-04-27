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
    public class CesListImportController : SuperController
    {
        // GET: CesListImport
        public ActionResult Index()
        {
            return View(DbContext.CesListImports.ToList());
        }

        // GET: CesListImport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CesListImport cesListImport = DbContext.CesListImports.Find(id);
            if (cesListImport == null)
            {
                return HttpNotFound();
            }
            return View(cesListImport);
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
            if (ModelState.IsValid)
            {
                DbContext.CesListImports.Add(cesListImport);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cesListImport);
        }

        // GET: CesListImport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CesListImport cesListImport = DbContext.CesListImports.Find(id);
            if (cesListImport == null)
            {
                return HttpNotFound();
            }
            return View(cesListImport);
        }

        // POST: CesListImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PI,DeptLevelOrg,EmployeeId,ProjectAccessionNum,ProjectNumber,PercentCeEffort,FullAnnualPayRate,TitleCode,FTE,Chart,Account,SubAccount,EstimatedCeExpenses")] CesListImport cesListImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(cesListImport).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cesListImport);
        }

        // GET: CesListImport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CesListImport cesListImport = DbContext.CesListImports.Find(id);
            if (cesListImport == null)
            {
                return HttpNotFound();
            }
            return View(cesListImport);
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

            return RedirectToAction("Index");
        }

        // POST: CesListImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CesListImport cesListImport = DbContext.CesListImports.Find(id);
            DbContext.CesListImports.Remove(cesListImport);
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

            var cesEntries = new List<CesListImport>();
            if (myFile != null)
            {
                var fileName = myFile.FileName;
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(myFile.InputStream);
                excelReader.IsFirstRowAsColumnNames = true;
                var result = excelReader.AsDataSet();
               
                TempData.Add("Message", "Now viewing \"" + fileName + "\".");
                excelReader.Close();

                cesEntries.AddRange(from DataRow row in result.Tables[0].Rows select new CesListImport(row));

                // This works.  Would now like the user to have a chance to review upload first.
                //if (ModelState.IsValid)
                //{
                //    db.CesListImports.AddRange(cesEntries);
                //    db.SaveChanges();
                //}

                return View("Display", cesEntries);
            }
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<CesListImport> cesEntries)
        {
            if (cesEntries != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid)
                {
                    DbContext.CesListImports.AddRange(cesEntries);
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
            const string filePathAndFilename = @"~\Sample_Forms\ModifiedCEForImport.xlsx";
            const string contentType = "application/text";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filePathAndFilename, contentType, "SampleCesList.xlsx");
        }
    }
}
