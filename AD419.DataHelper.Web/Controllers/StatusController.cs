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
            var status = DbContext.ProcessStatuses.ToList();
            status[0].IsCompleted = true;

            return View(status);
        }
    }
}