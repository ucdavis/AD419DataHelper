using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class ArcCodesController : SuperController
    {
        // GET: ARC_Codes
        public ActionResult Index()
        {
            var codes = FisDbContext.ARC_Codes
                .OrderByDescending(a => a.IsAES)
                .ThenBy(a => a.Id)
                .ToList();

            return View(codes);
        }

        // GET: ARC_Codes/Details/5
        public ActionResult Details(string id)
        {
            var code = FisDbContext.ARC_Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // GET: ARC_Codes/Edit/5
        public ActionResult Edit(string id)
        {
            var code = FisDbContext.ARC_Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // POST: ARC_Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, FunctionName, UpdatedDate, IsAES, CategoryCode, SubCategoryCode")] ArcCode code)
        {
            var newCode = code;
            if (!ModelState.IsValid) return View(code);

            FisDbContext.Entry(code).State = EntityState.Modified;
            FisDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FisDbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
