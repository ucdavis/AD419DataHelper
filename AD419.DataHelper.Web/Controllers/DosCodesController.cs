using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class DosCodesController : SuperController
    {
        // GET: DOS_Codes
        public ActionResult Index()
        {
            var model = new DosCodesViewModel
            {
                DosCodes = DbContext.DosCodes.ToList(),
                LaborTransactions = DbContext.GetLaborTransactions((int)LaborTransactionsOptions.DosCodes).ToList(), 
                CodeTypeName = "D.O.S. Codes" 
            };

            return View(model);
        }

        // GET: DOS_Codes/Details/5
        public ActionResult Details(string id)
        {
            var code = DbContext.DosCodes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // GET: DOS_Codes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DOS_Codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Code,Description,IncludeInAD419FTE")] DosCode dosCodes)
        {
            if (!ModelState.IsValid) return View(dosCodes);

            DbContext.DosCodes.Add(dosCodes);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: DOS_Codes/Edit/5
        public ActionResult Edit(string id)
        {
            var code = DbContext.DosCodes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // POST: DOS_Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Code,Description,IncludeInAD419FTE")] DosCode dosCodes)
        {
            if (!ModelState.IsValid) return View(dosCodes);

            DbContext.Entry(dosCodes).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: DOS_Codes/Delete/5
        public ActionResult Delete(string id)
        {
            var code = DbContext.DosCodes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            return View(code);
        }

        // POST: DOS_Codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var code = DbContext.DosCodes.Find(id);
            if (code == null)
            {
                return HttpNotFound();
            }

            DbContext.DosCodes.Remove(code);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
