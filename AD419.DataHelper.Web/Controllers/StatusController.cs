using System;
using System.Data.SqlClient;
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

        [HttpPost]
        public ActionResult RunSproc(int id)
        {
            var category = DbContext.ProcessCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            try
            {
                ExecuteSproc(category.StoredProcedureName);
            }
            catch (Exception)
            {
                Message = "Error Running it";
                return View(category);
            }
            

            category.IsCompleted = true;

            DbContext.SaveChanges();

            Message = "Done";
            return RedirectToAction("Index");

        }


        private void ExecuteSproc(string sprocName)
        {
            var year = FiscalYearService.FiscalYear;
            SqlParameter param1 = new SqlParameter("@FiscalYear", year);
            SqlParameter param2 = new SqlParameter("@IsDebug", 0);



            DbContext.Database.ExecuteSqlCommand(string.Format("{0} @FiscalYear @IsDebug", sprocName), param1, param2);

        }

    }
}