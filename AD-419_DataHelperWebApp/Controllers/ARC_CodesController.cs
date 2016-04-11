using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class ARC_CodesController : Controller
    {
        private FISDataContext db = new FISDataContext();

        // GET: ARC_Codes
        public ActionResult Index()
        {
            return View(db.ARC_Codes.ToList().OrderByDescending(a => a.isAES).ThenBy(a => a.ARC_Cd));
        }

        // GET: ARC_Codes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARC_Codes arcCodes = db.ARC_Codes.Find(id);
            if (arcCodes == null)
            {
                return HttpNotFound();
            }
            return View(arcCodes);
        }

        // GET: ARC_Codes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARC_Codes arcCodes = db.ARC_Codes.Find(id);
            if (arcCodes == null)
            {
                return HttpNotFound();
            }
            return View(arcCodes);
        }

        // POST: ARC_Codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ARC_Cd,ARC_Name,OP_Func_Name,ARC_Category_Cd, ARC_Sub_Category_Cd, DS_Last_Update_Date,isAES")] ARC_Codes arcCodes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arcCodes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arcCodes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
