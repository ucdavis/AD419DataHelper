using System.Linq;
using System.Web.Mvc;
using System.Net;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class NaturalAccountsController : SuperController
    {
        // GET: NaturalAccounts
        public ActionResult Index()
        {
            var model = new NaturalAccountsViewModel
            {
                NaturalAccounts = DbContext.NaturalAccount
                    .OrderBy(a => a.IncludeInAD419.HasValue ? (a.IncludeInAD419.Value ? 0 : 1) : 2)  // Yes first, No second, NULL last
                    .ThenBy(a => a.Account_Parent_1)
                    .ThenBy(a => a.Account_Child_Posting_Level)
                    .ToList(),
                CodeTypeName = "Natural Accounts"
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var account = DbContext.NaturalAccount.Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account_Child_Posting_Level,IncludeInAD419")] NaturalAccount model)
        {
            if (!ModelState.IsValid) return View(model);

            var existing = DbContext.NaturalAccount.Find(model.Account_Child_Posting_Level);
            if (existing == null) return HttpNotFound();

            existing.IncludeInAD419 = model.IncludeInAD419;

            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}