using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class TitlesController : SuperController
    {
        // GET: Titles
        public ActionResult Index()
        {
            var model = new TitleCodeViewModel
            {
                Titles = PpsDbContext.Titles.ToList(),
                LaborTransactionsForTitlesWithMissingStaffTypes = DbContext.GetTitlesWithMissingStaffTypes().ToList()
            };

            return View(model);
        }

        // GET: Titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var title = PpsDbContext.Titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            var staffType = DbContext.StaffTypes.Find(title.StaffType);
            
            return View(new TitleCodeDetailViewModel(title, staffType));
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

            var title = PpsDbContext.Titles.Find(id);
            if (title == null)
            {
                Message = string.Format("Error: Unable to find title code {0} in the database.", id);
                RedirectToAction("Index");
            }

            var titleCodeViewModel = new TitleCodeEditViewModel(title, DbContext.StaffTypes.ToList());

            return View(titleCodeViewModel);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitleCode,Name,AbbreviatedName,PersonnelProgramCode,UnitCode,TitleGroup,OvertimeExemptionCode,CTOOccupationSubgroupCode,FederalOccupationCode,FOCSubcategoryCode,LinkTitleGroupCode,EE06CategoryCode,StaffType,EffectiveDate,UpdateTimestamp")] Titles title)
        {
            if (ModelState.IsValid)
            {
                PpsDbContext.Entry(title).State = EntityState.Modified;
                PpsDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new TitleCodeEditViewModel(title, DbContext.StaffTypes.ToList()));
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
