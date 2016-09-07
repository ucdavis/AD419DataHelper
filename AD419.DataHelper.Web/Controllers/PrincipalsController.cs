using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        public ActionResult ProrateMatch(int id)
        {
            var match = DbContext.PrincipalInvestigatorMatches.Find(id);
            if (match == null)
                return HttpNotFound();

            if (!string.IsNullOrEmpty(match.MatchName))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (match.IsProrated.HasValue && match.IsProrated.Value)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            match.IsProrated = true;

            // Adding these lines here allows the user to simply click on the Prorate link to prorate the entry
            // without the need to navigate to another page.
            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
            // end added lines

            //return View(match);
        }

        [HttpPost]
        public ActionResult ProrateMatch(PrincipalInvestigatorMatch match)
        {
            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
        }
    }
}
