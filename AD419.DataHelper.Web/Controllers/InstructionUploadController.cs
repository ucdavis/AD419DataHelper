﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Controllers
{
    public class InstructionUploadController : SuperController
    {
        readonly DataClasses _objData;
        private readonly string _instructionsDirectory = ConfigurationManager.AppSettings["InstructionsDirectory"]; 

        public InstructionUploadController()
        {
            _objData = new DataClasses(); 
        }

        // GET: InstructionUpload
        public ActionResult Index()
        {
            var files = _objData.GetFiles(_instructionsDirectory);
            return View(files);
        }

        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> file)
        {
            const string ad419InstructionsFileName = "AD419Instructions.pdf";

            string uploadPath = _instructionsDirectory + "\\";

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var fileName = Request.Files[i].FileName;
                
                Request.Files[0].SaveAs(uploadPath + ad419InstructionsFileName);


                TempData.Add("Message", "\"" + fileName + "\" has been uploaded as " + ad419InstructionsFileName + ".");
            }
            return RedirectToAction("Index", "InstructionUpload");
        }

        public ActionResult Delete(string id)
        {
            int fid = Convert.ToInt32(id);
            var files = _objData.GetFiles(_instructionsDirectory);
            string fullPath = (from f in files
                               where f.FileId == fid
                               select f.FilePath).First();

            var file = new FileInfo(fullPath);

            if (file.Exists)
            {
                file.Delete();
            }

            files = _objData.GetFiles(_instructionsDirectory);
            TempData["Message"] = "File \"" + file.Name + "\" has been deleted.";
            return RedirectToAction("Index", "InstructionUpload");
        }

        public FileResult Download(string id)
        {
            int fid = Convert.ToInt32(id);
            var files = _objData.GetFiles(_instructionsDirectory);
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