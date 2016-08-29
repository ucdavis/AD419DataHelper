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
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult RunSproc(int id)
        {
            var category = DbContext.ProcessCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            
            try
            {
                if(!category.IsCompleted) { ExecuteSproc(category.StoredProcedureName);}
            }
            catch (Exception)
            {
                return new JsonResult() {Data = new {Success = false, Message = "There was a problem."}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }            

            category.IsCompleted = true;
            DbContext.SaveChanges();
           
            return new JsonResult() { Data = new { Success = true, Message = "Done." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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