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
                .Where(p => !p.EmployeeName.StartsWith("(CE PI)"))
                .OrderBy(p => p.EmployeeName)
                .ToList() // pull from db
                .Select(p => new SelectListItem()
                {
                    Text = string.Format("({0}) - {1}", p.EmployeeId, p.EmployeeName),
                    Value = p.EmployeeId
                })
                .ToList();

            // add empty
            names.Insert(0, new SelectListItem(){Text="No Employee Expense"});

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
                // Let's assume a null name means they want to prorate the match, instead of assigning a PI Name.
                //ErrorMessage = "Could not match";
                //return View(match);
                match.IsProrated = true;
                match.EmployeeId = null;  // This gets set to "Prorate" if I don't clear it out.
            }

            if (name != null)
            {
                match.MatchName = name.Name;
                match.IsProrated = false;
            }


            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
        }

        [HttpPost]
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

            DbContext.Entry(match).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("UnproratedMatches");
        }
    }
}
