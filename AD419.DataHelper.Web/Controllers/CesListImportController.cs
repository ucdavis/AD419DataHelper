using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class CesListImportController : SuperController
    {
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

            var cesEntries = new List<CesListImport>();
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
                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportCeSpecialists);
                    DbContext.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
