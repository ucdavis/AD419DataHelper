using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class PrincipalsController : SuperController
    {
        public ActionResult UnproratedMatches()
        {
            var matches = DbContext.PrincipalInvestigatorMatches
                .Where(p => string.IsNullOrEmpty(p.MatchName))
                .Where(p => !p.IsProrated.HasValue || !p.IsProrated.Value)
                .ToList();

            return View(matches);
        }

        public ActionResult FindMatch(int id)
        {
            var match = DbContext.PrincipalInvestigatorMatches.Find(id);
            if (match == null) return HttpNotFound();

            return View(match);
        }

        [HttpPost]
        public ActionResult Match(PrincipalInvestigatorMatch match)
        {
            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
        }

        public ActionResult Find(string query)
        {
            var matches = DbContext.PrincipalInvestigatorMatches
                .Where(p => p.Name);
        }
    }
}
