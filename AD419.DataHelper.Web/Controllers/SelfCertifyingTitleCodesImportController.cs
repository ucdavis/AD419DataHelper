using System.Collections.Generic;
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
    public class SelfCertifyingTitleCodesImportController : SuperController
    {
        private readonly SelfCertifyingTitleCodesService _selfCertifyingTitleCodesService;

        public SelfCertifyingTitleCodesImportController()
        {
            _selfCertifyingTitleCodesService = new SelfCertifyingTitleCodesService(PpsDbContext);
        }

        // GET: SelfCertifyingTitleCode
        public ActionResult Index()
        {
            var imports = PpsDbContext.SelfCertifyingTitleCodes.ToList();
            return View(imports);
        }

        // GET: CesListImport/Details/5
        public ActionResult Details(int id)
        {
            var import = PpsDbContext.SelfCertifyingTitleCodes.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            return View(import);
        }

        // GET: SelfCertifyingTitleCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SelfCertifyingTitleCode/Create
        // To protect from over-posting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TitleCode,TitleName,ClassTitleOutline")] SelfCertifyingTitleCode selfCertifyingTitleCode)
        {
            if (!ModelState.IsValid) return View(selfCertifyingTitleCode);

            PpsDbContext.SelfCertifyingTitleCodes.Add(selfCertifyingTitleCode);
            PpsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: SelfCertifyingTitleCode/Edit/5
        public ActionResult Edit(int id)
        {
            var import = PpsDbContext.SelfCertifyingTitleCodes.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            return View(import);
        }

        // POST: PpsDbContext/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TitleCode,TitleName,ClassTitleOutline")] SelfCertifyingTitleCode selfCertifyingTitleCode)
        {
            if (!ModelState.IsValid) return View(selfCertifyingTitleCode);

            PpsDbContext.Entry(selfCertifyingTitleCode).State = EntityState.Modified;
            PpsDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: SelfCertifyingTitleCode/Delete/5
        public ActionResult Delete(int id)
        {
            var import = PpsDbContext.SelfCertifyingTitleCodes.Find(id);
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
            //var all = from c in db.SelfCertifyingTitleCodes select c;
            //db.SelfCertifyingTitleCodes.RemoveRange(all);
            //db.SaveChanges();

            PpsDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE TitleCodesSelfCertify");

            return RedirectToAction("Index");
        }

        // POST: SelfCertifyingTitleCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var import = PpsDbContext.SelfCertifyingTitleCodes.Find(id);
            if (import == null)
            {
                return HttpNotFound();
            }

            PpsDbContext.SelfCertifyingTitleCodes.Remove(import);
            PpsDbContext.SaveChanges();

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
            var selfCertifyingTitleCodes = _selfCertifyingTitleCodesService.GetSelfCertifyingTitleCodesFromRows(result.Tables[0].Rows).ToList();

            // validate:
            var errors = new List<ModelStateDictionary>();
            foreach (var selfCertifyingTitleCode in selfCertifyingTitleCodes)
            {
                // clear and check
                ModelState.Clear();
                TryValidateModel(selfCertifyingTitleCode);

                // copy out errors
                var state = new ModelStateDictionary();
                state.Merge(ModelState);
                errors.Add(state);
            }
            ViewBag.Errors = errors;
   
            //TempData.Add("Message", "Now viewing \"" + fileName + "\".");

            return PartialView("_uploadData", selfCertifyingTitleCodes);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(List<SelfCertifyingTitleCode> selfCertifyingTitleCodes)
        {
            if (selfCertifyingTitleCodes != null)
            {
                // This works.  Would now like the user to have a chance to review upload first.
                if (ModelState.IsValid)
                {
                    PpsDbContext.SelfCertifyingTitleCodes.AddRange(selfCertifyingTitleCodes);
                    PpsDbContext.SaveChanges();

                    DbContext.MarkStatusCompleted(ProcessStatuses.ImportSelfCertifyingTitleCodes);
                    DbContext.SaveChanges();
                }
                else
                {
                    ErrorMessage = "ERROR! Your import file could not be saved.";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
