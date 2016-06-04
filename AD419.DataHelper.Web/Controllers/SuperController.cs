using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    [Authorize]
    public class SuperController : Controller
    {
        protected AD419DataContext DbContext = new AD419DataContext();
        protected FISDataContext FisDbContext = new FISDataContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
                FisDbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}