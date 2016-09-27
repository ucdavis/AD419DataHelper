using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class Ad419InstructionsController : SuperController
    {
        readonly DataClasses _objData;
        private readonly string _instructionsDirectory = ConfigurationManager.AppSettings["InstructionsDirectory"];
        private readonly string _ad419InstructionsFileName = ConfigurationManager.AppSettings["AD419InstructionsFileName"];

        public Ad419InstructionsController()
        {
            _objData = new DataClasses(); 
        }

        public ActionResult Index()
        {
            return File(HostingEnvironment.MapPath(_instructionsDirectory + "\\" + _ad419InstructionsFileName), "application/pdf");
        }

        // GET: InstructionUpload
        public ActionResult InstructionUpload()
        {
            var files = _objData.GetFiles(HostingEnvironment.MapPath(_instructionsDirectory));
            return View(files);
        }

        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            string uploadPath = HostingEnvironment.MapPath(_instructionsDirectory) + "\\";

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var fileName = Request.Files[i].FileName;

                Request.Files[0].SaveAs(uploadPath + _ad419InstructionsFileName);


                TempData.Add("Message", "\"" + fileName + "\" has been uploaded as " + _ad419InstructionsFileName + ".");
            }
            return RedirectToAction("InstructionUpload");
        }

        public ActionResult Delete(string id)
        {
            int fid = Convert.ToInt32(id);
            var files = _objData.GetFiles(HostingEnvironment.MapPath(_instructionsDirectory));
            string fullPath = (from f in files
                               where f.FileId == fid
                               select f.FilePath).First();

            var file = new FileInfo(fullPath);

            if (file.Exists)
            {
                file.Delete();
            }

            files = _objData.GetFiles(HostingEnvironment.MapPath(_instructionsDirectory));
            TempData["Message"] = "File \"" + file.Name + "\" has been deleted.";
            return RedirectToAction("InstructionUpload");
        }

        public FileResult Download(string id)
        {
            int fid = Convert.ToInt32(id);
            var files = _objData.GetFiles(HostingEnvironment.MapPath(_instructionsDirectory));
            string filename = (from f in files
                               where f.FileId == fid
                               select f.FileName).First();

            string filePathAndFilename = (from f in files
                                          where f.FileId == fid
                                          select f.FilePath).First();
            const string contentType = "application/text";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filePathAndFilename, contentType, filename);
        }
    }
}