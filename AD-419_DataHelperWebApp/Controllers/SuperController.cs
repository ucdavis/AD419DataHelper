using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD_419_DataHelperWebApp.Models;

namespace AD_419_DataHelperWebApp.Controllers
{
    [Authorize]
    public class SuperController : Controller
    {
        protected AD419DataContext DbContext = new AD419DataContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}