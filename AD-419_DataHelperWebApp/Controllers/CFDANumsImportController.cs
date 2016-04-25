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
        public ActionResult Details(int id)
        {
            var num = DbContext.CFDANumImports.Find(id);
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
        public ActionResult Create([Bind(Include = "Id,CFDANum")] CFDANumImport cFDANumImport)
        {
            if (!ModelState.IsValid) return View(cFDANumImport);

            DbContext.CFDANumImports.Add(cFDANumImport);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CFDANumsImport/Edit/5
        public ActionResult Edit(int id)
        {
            var code = DbContext.CFDANumImports.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,CFDANum, ProgramTitle")] CFDANumImport cFDANumImport)
        {
            if (!ModelState.IsValid) return View(cFDANumImport);

            DbContext.Entry(cFDANumImport).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: CFDANumsImport/Delete/5
        public ActionResult Delete(int id)
        {
            var num = DbContext.CFDANumImports.Find(id);
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

            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE CFDANumImport");

            return RedirectToAction("Index");
        }

        // POST: CFDANumsImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var num = DbContext.CFDANumImports.Find(id); if (num == null)
            {
                return HttpNotFound();
            }

            DbContext.CFDANumImports.Remove(num);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            var myFile = Request.Files[0];
            if (myFile == null) return RedirectToAction("Index");

            var cfdaNums = new List<CFDANumImport>();
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
    }
}
