using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.ViewModels;

namespace AD419.DataHelper.Web.Controllers
{
    public class SfnClassificationLogicController : SuperController
    {
        // GET: SfnClassificationLogic
        public ActionResult Index()
        {
            return View(DbContext.SfnClassificationLogic.
                OrderBy(o => o.EvaluationOrder).
                ThenBy(o => o.ParameterOrder).
                ThenBy(o => o.SubParameterOrder).ToList());
        }

        // GET: SfnClassificationLogic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SfnClassificationLogic sfnClassificationLogic = DbContext.SfnClassificationLogic.Find(id);
            if (sfnClassificationLogic == null)
            {
                return HttpNotFound();
            }
            return View(sfnClassificationLogic);
        }

        // GET: SfnClassificationLogic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SfnClassificationLogic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EvaluationOrder,ParameterOrder,SubParameterOrder,LogicalOperator,ColumnName,NegateCondition,ConditionalOperator,Values,SFN")] SfnClassificationLogic sfnClassificationLogic)
        {
            if (ModelState.IsValid)
            {
                DbContext.SfnClassificationLogic.Add(sfnClassificationLogic);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sfnClassificationLogic);
        }

        // GET: SfnClassificationLogic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SfnClassificationLogic sfnClassificationLogic = DbContext.SfnClassificationLogic.Find(id);
            if (sfnClassificationLogic == null)
            {
                return HttpNotFound();
            }
            //return View(sfnClassificationLogic);
            return View(new SfnClassificationLogicViewModel(sfnClassificationLogic));
        }

        // POST: SfnClassificationLogic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EvaluationOrder,ParameterOrder,SubParameterOrder,LogicalOperator,ColumnName,NegateCondition,ConditionalOperator,Values,SFN")] SfnClassificationLogic sfnClassificationLogic)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(sfnClassificationLogic).State = EntityState.Modified;
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(new SfnClassificationLogicViewModel(sfnClassificationLogic));
        }

        public ActionResult Display()
        {
            var caseStatement = DbContext.GetSfnCaseStatement();
            caseStatement = caseStatement.Replace("\r\n", "<br/>");
            caseStatement = caseStatement.Replace("\t", "&nbsp;&nbsp;");
            ViewBag.CaseStatement = caseStatement;
            return View();
        }

        // GET: SfnClassificationLogic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SfnClassificationLogic sfnClassificationLogic = DbContext.SfnClassificationLogic.Find(id);
            if (sfnClassificationLogic == null)
            {
                return HttpNotFound();
            }
            return View(sfnClassificationLogic);
        }

        // POST: SfnClassificationLogic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SfnClassificationLogic sfnClassificationLogic = DbContext.SfnClassificationLogic.Find(id);
            DbContext.SfnClassificationLogic.Remove(sfnClassificationLogic);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
