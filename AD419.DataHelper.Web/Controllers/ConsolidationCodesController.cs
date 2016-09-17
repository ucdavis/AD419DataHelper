using AD419.DataHelper.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class ConsolidationCodesController : SuperController
    {
        // GET: ConsolidationCodes
        public ActionResult Index()
        {
            var model = new ConsolidationCodesModel
            {
                ConsolidationCodes = DbContext.ConsolidationCodes.ToList(),
                LaborTransactions =
                    DbContext.GetLaborTransactions((int) LaborTransactionsOptions.ConsolidationCodes).ToList()
            };

            return View(model);
        }

        // GET: ConsolidationCodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = DbContext.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConsolidationCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjectConsolidationNumber,ObjectConsolidationName,IncludeInFTECalc,IncludeInLaborTransactions")] ConsolidationCodes consolidationCodes)
        {
            if (ModelState.IsValid)
            {
                DbContext.ConsolidationCodes.Add(consolidationCodes);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = DbContext.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // POST: ConsolidationCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjectConsolidationNumber,ObjectConsolidationName,IncludeInFTECalc,IncludeInLaborTransactions")] ConsolidationCodes consolidationCodes)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(consolidationCodes).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consolidationCodes);
        }

        // GET: ConsolidationCodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsolidationCodes consolidationCodes = DbContext.ConsolidationCodes.Find(id);
            if (consolidationCodes == null)
            {
                return HttpNotFound();
            }
            return View(consolidationCodes);
        }

        // POST: ConsolidationCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ConsolidationCodes consolidationCodes = DbContext.ConsolidationCodes.Find(id);
            DbContext.ConsolidationCodes.Remove(consolidationCodes);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
