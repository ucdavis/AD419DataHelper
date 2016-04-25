using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD_419_DataHelperWebApp.Models;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class ArcCodesController : SuperController
    {
        private readonly FISDataContext _fisContext = new FISDataContext();

        // GET: ARC_Codes
        public ActionResult Index()
        {
            var codes = _fisContext.ARC_Codes
                .OrderByDescending(a => a.isAES)
                .ThenBy(a => a.ARC_Cd)
                .ToList();

            return View(codes);
        }

        // GET: ARC_Codes/Details/5
        public ActionResult Details(string id)
        {
            var code = _fisContext.ARC_Codes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // GET: ARC_Codes/Edit/5
        public ActionResult Edit(string id)
        {
            var code = _fisContext.ARC_Codes.Find(id);
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
        public ActionResult Edit([Bind(Include = "ARC_Cd,ARC_Name,OP_Func_Name,ARC_Category_Cd, ARC_Sub_Category_Cd, DS_Last_Update_Date,isAES")] ARC_Codes code)
        {
            if (!ModelState.IsValid) return View(code);

            _fisContext.Entry(code).State = EntityState.Modified;
            _fisContext.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _fisContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
