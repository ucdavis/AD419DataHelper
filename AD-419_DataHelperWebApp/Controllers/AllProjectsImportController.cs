using AD_419_DataHelperWebApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AD_419_DataHelperWebApp.Controllers
{
    public class AllProjectsImportController : SuperController
    {
        // GET: AllProjectsImport
        public ActionResult Index()
        {
            return View(DbContext.AllProjectsImmport.ToList());
        }

        // GET: AllProjectsImport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsImport allProjectsImport = DbContext.AllProjectsImmport.Find(id);
            if (allProjectsImport == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsImport);
        }

        // GET: AllProjectsImport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllProjectsImport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus")] AllProjectsImport allProjectsImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.AllProjectsImmport.Add(allProjectsImport);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allProjectsImport);
        }

        // GET: AllProjectsImport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsImport allProjectsImport = DbContext.AllProjectsImmport.Find(id);
            if (allProjectsImport == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsImport);
        }

        // POST: AllProjectsImport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus")] AllProjectsImport allProjectsImport)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(allProjectsImport).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allProjectsImport);
        }

        // GET: AllProjectsImport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllProjectsImport allProjectsImport = DbContext.AllProjectsImmport.Find(id);
            if (allProjectsImport == null)
            {
                return HttpNotFound();
            }
            return View(allProjectsImport);
        }

        // POST: AllProjectsImport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllProjectsImport allProjectsImport = DbContext.AllProjectsImmport.Find(id);
            DbContext.AllProjectsImmport.Remove(allProjectsImport);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
