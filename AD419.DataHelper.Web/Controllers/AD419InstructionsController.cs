using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AD419.DataHelper.Web.Controllers
{
    public class Ad419InstructionsController : SuperController
    {
        private readonly string _ad419InstructionsFileName = ConfigurationManager.AppSettings["AD419InstructionsFileName"];
        private readonly string _instructionsDirectory = ConfigurationManager.AppSettings["InstructionsDirectory"];

        // GET: AD419Instructions
        public ActionResult Index()
        {
            return File(HostingEnvironment.MapPath(_instructionsDirectory + "\\" + _ad419InstructionsFileName), "application/pdf");
        }
    }
}