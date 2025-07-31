using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Attributes;


namespace AD419.DataHelper.Web.Controllers
{
    [UseAntiForgeryTokenOnPostByDefault]
    public class StatusController : SuperController
    {
        private readonly int _sqlCommandTimeout;
        private const int DefaultSqlCommandTimeout = 15;

        public StatusController()
        {
            try
            {
                _sqlCommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SqlCommandTimeout"));
            }
            catch (System.FormatException)
            {
                _sqlCommandTimeout = DefaultSqlCommandTimeout;
            }
            catch (System.OverflowException)
            {
                _sqlCommandTimeout = DefaultSqlCommandTimeout;
            }
        }

        // GET: Status
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var categories = DbContext.ProcessCategories.Include("Statuses").ToList();

                return View(categories);
            }
            return RedirectToAction("Index", "Home");
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

            var statuses = DbContext.ProcessStatuses.Where(s => s.CategoryId == id);
            foreach (var status in statuses)
            {
                status.IsCompleted = true;
            }

            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Completes a step
        /// </summary>
        [HttpPost]
        public ActionResult StatusCompleted(int id)
        {
            var status = DbContext.ProcessStatuses.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }

            status.IsCompleted = !status.IsCompleted;  // toggle from true to false, false to true, etc.

            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Reset()
        {
            try
            {
                ExecuteSproc("usp_ClearProcessStatusAndProcessCategory"); 
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = new { Success = false, Message = "There was a problem.", ErrorMessage = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ClearAssociations()
        {
            try
            {
                ExecuteSproc("[usp_RollbackAndReloadExpenses]");
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = new { Success = false, Message = "There was a problem.", ErrorMessage = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Resets status back to post processing review,
        /// Re-hides any 204 projects which have less than $100 of expenses, and
        /// Truncates and reloads the project table so those projects are no longer visible.
        /// </summary>
        [HttpPost]
        public ActionResult HideProjectsAndResetStatusBackToPostProcessingReview()
        {
            try
            {
                ExecuteSproc("[usp_HideProjectsAndResetStatusBackToPostProcessingReview]");
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = new { Success = false, Message = "There was a problem.", ErrorMessage = ex.Message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

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
                if(!category.IsCompleted) { ExecuteSproc(category.StoredProcedureName);}
            }
            catch (Exception ex)
            {
                return new JsonResult() {Data = new {Success = false, Message = "There was a problem.", ErrorMessage = ex.Message}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

            category.IsCompleted = true;

            var statuses = DbContext.ProcessStatuses.Where(s => s.CategoryId == id);
            foreach (var status in statuses)
            {
                status.IsCompleted = true;
            }

            DbContext.SaveChanges();
           
            return new JsonResult() { Data = new { Success = true, Message = "Done." }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        private void ExecuteSproc(string sprocName)
        {
            var year = FiscalYearService.FiscalYear;
            SqlParameter param1 = new SqlParameter("@FiscalYear", year);
            SqlParameter param2 = new SqlParameter("@IsDebug", false);

            DbContext.Database.CommandTimeout = 60 * _sqlCommandTimeout; // 15 default minute timeout, as reports take about 12 minutes to run,
                                                                         // but usp_BeginProcessForNewReportingYear takes over 21 minutes to run;
                                                                         // therefore, this value can now be set from the Web.config.
            DbContext.Database.ExecuteSqlCommand(string.Format("{0} @FiscalYear, @IsDebug", sprocName), param1, param2);
            DbContext.Database.CommandTimeout = null;
        }        
    }
}