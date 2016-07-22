using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;
using AD419.DataHelper.Web.Services;
using Excel;

namespace AD419.DataHelper.Web.Controllers
{
    public class AllProjectsNewController : SuperController
    {
        private readonly ProjectImportService _projectImportService;

        public AllProjectsNewController()
        {
            _projectImportService = new ProjectImportService(DbContext);
        }

        // GET: AllProjectsNew
        public ActionResult Index()
        {
            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate >= FiscalStartDate)
                .Where(p => p.ProjectEndDate >= FiscalEndDate)
                .ToList();

            return View(projects);
        }

        public ActionResult FindByDirector(string director)
        {
            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate <= FiscalEndDate) //project has actually started
                .Where(p => p.ProjectEndDate >= FiscalStartDate) //project hasn't ended yet
                .Where(p => p.ProjectDirector.Equals(director));

            return new JsonResult() { Data = projects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult FindByOrganization(string organization)
        {
            var projects = DbContext.AllProjectsNew
                .Where(p => p.ProjectStartDate <= FiscalEndDate) //project has actually started
                .Where(p => p.ProjectEndDate >= FiscalStartDate) //project hasn't ended yet
                .Where(p => p.OrgR.Equals(organization));

            return new JsonResult() { Data = projects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: AllProjectsNew/Details/5
        public ActionResult Details(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // GET: AllProjectsNew/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllProjectsNew/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,isInterdepartmental,IsActive")] AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.AllProjectsNew.Add(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Edit/5
        public ActionResult Edit(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProject,AccessionNumber,ProjectNumber,ProposalNumber,AwardNumber,Title,OrganizationName,OrgR,Department,ProjectDirector,CoProjectDirectors,FundingSource,ProjectStartDate,ProjectEndDate,ProjectStatus,IsInterdepartmental,IsActive")] AllProjectsNew project)
        {
            if (!ModelState.IsValid) return View(project);

            DbContext.Entry(project).State = EntityState.Modified;
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: AllProjectsNew/Delete/5
        public ActionResult Delete(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // POST: AllProjectsNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var project = DbContext.AllProjectsNew.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            DbContext.AllProjectsNew.Remove(project);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            var myFile = Request.Files[0];
            if (myFile == null) return RedirectToAction("Index");

            var fileName = myFile.FileName;
            TempData.Add("Message", "Now viewing \"" + fileName + "\".");

            // setup reader
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(myFile.InputStream);
            excelReader.IsFirstRowAsColumnNames = true;

            // read data
            var result = excelReader.AsDataSet();
            excelReader.Close();

            // transform
            var projects = from DataRow row in result.Tables[0].Rows
                           select _projectImportService.GetProjectFromRow(row);

            return View(projects.ToList());
        }

        public ActionResult Save(IEnumerable<AllProjectsNew> projects)
        {
            if (projects == null)
                return RedirectToAction("Index");

            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            DbContext.AllProjectsNew.AddRange(projects);
            DbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
