using System;
using System.Linq;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Controllers
{
    public class StatusController : SuperController
    {
        // GET: Status
        public ActionResult Index()
        {
            var categories = DbContext.ProcessCategories.Include("Statuses").ToList();

            return View(categories);
        }

        [HttpPost]
        public ActionResult Completed(int id)
        {
            var category = DbContext.ProcessCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            category.IsCompleted = true;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}