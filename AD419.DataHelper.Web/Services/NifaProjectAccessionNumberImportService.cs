using System.Collections.Generic;
using System.Data;
using System.Linq;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class NifaProjectAccessionNumberImportService
    {
        private readonly AD419DataContext _dataContext;
 
        public NifaProjectAccessionNumberImportService(AD419DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<NifaProjectAccessionNumberImport> GetProjectsFromRows(DataRowCollection rows)
        {
            var projects = rows.ToEnumerable().Select(GetProjectFromRow).ToList();
          
            return projects;
        }

        private NifaProjectAccessionNumberImport GetProjectFromRow(DataRow row)
        {
            var project = new NifaProjectAccessionNumberImport()
            {
                ProjectNumber = row[0].ToString(),
                AccessionNumber    = row[1].ToString(),
                Notes = row[2].ToString()
            };

            // fix accession number
            if (string.IsNullOrWhiteSpace(project.AccessionNumber))
            {
                project.AccessionNumber = "0000000";
            }
            project.AccessionNumber = project.AccessionNumber.PadLeft(7, '0');

            return project;
        }
    }
}