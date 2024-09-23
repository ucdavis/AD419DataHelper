using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Controllers.Fliters;

namespace AD419.DataHelper.Web.Controllers
{
    [AdminOnly]
    public class UserAdministrationController : SuperController
    {
        // GET: UserAdministration
        public ActionResult Index()
        {
            return View();
        }
    }
}