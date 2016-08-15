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

            var names = DbContext.PrincipalInvestigators
                .Where(p => p.Organization == match.Organization)
                .OrderBy(p => p.EmployeeId)
                .ToList() // pull from db
                .Select(p => new SelectListItem()
                {
                    Text = string.Format("({0}) - {1}", p.EmployeeId,  p.EmployeeName.Replace("(CE PI)", "")),
                    Value = p.EmployeeId
                })
                .ToList();


            // add empty
            names.Insert(0, new SelectListItem());

            ViewBag.NamesInOrg = names;

            return View(match);
        }

        [HttpPost]
        public ActionResult FindMatch(PrincipalInvestigatorMatch match)
        {
            // check matched employee id, fetch and insert name
            var name = DbContext.PrincipalInvestigators
                .FirstOrDefault(p => p.EmployeeId == match.EmployeeId);
            if (name == null)
            {
                ErrorMessage = "Could not match";
                return View(match);
            }


            match.MatchName = name.Name;

            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
        }
    }
}
