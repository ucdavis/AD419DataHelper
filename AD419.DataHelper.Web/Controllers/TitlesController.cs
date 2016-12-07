using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class TitlesController : SuperController
    {
        // GET: Titles
        public ActionResult Index()
        {
            return View(PpsDbContext.Titles.ToList());
        }

        // GET: Titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = PpsDbContext.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitleCode,Name,AbbreviatedName,PersonnelProgramCode,UnitCode,TitleGroup,OvertimeExemptionCode,CTOOccupationSubgroupCode,FederalOccupationCode,FOCSubcategoryCode,LinkTitleGroupCode,EE06CategoryCode,StaffType,EffectiveDate,UpdateTimestamp")] Titles titles)
        {
            if (ModelState.IsValid)
            {
                PpsDbContext.Titles.Add(titles);
                PpsDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(titles);
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = PpsDbContext.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitleCode,Name,AbbreviatedName,PersonnelProgramCode,UnitCode,TitleGroup,OvertimeExemptionCode,CTOOccupationSubgroupCode,FederalOccupationCode,FOCSubcategoryCode,LinkTitleGroupCode,EE06CategoryCode,StaffType,EffectiveDate,UpdateTimestamp")] Titles titles)
        {
            if (ModelState.IsValid)
            {
                PpsDbContext.Entry(titles).State = EntityState.Modified;
                PpsDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(titles);
        }

        // GET: Titles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Titles titles = PpsDbContext.Titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Titles titles = PpsDbContext.Titles.Find(id);
            PpsDbContext.Titles.Remove(titles);
            PpsDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
