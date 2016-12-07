using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class CeSpecialistService
    {
        private readonly AD419DataContext _dataContext;
        private static DateTime _fiscalEndDate;

        public CeSpecialistService(AD419DataContext dataContext, DateTime fiscalEndDate)
        {
            _dataContext = dataContext;
            _fiscalEndDate = fiscalEndDate;
        }

        public IEnumerable<CesListImport> GetCesListImportFromRows(DataRowCollection rows)
        {
            var allProjects = _dataContext.AllProjectsNew.Where(p => p.IsUcDavis).ToList();

            var ceSpecialists = rows.ToEnumerable().Select(row => new CesListImport(row)).ToList();

            var matchingProjects =
                from ceSpecialist in ceSpecialists
                join project in allProjects on ceSpecialist.ProjectAccessionNum equals project.AccessionNumber
                into j
                from match in j.DefaultIfEmpty()
                select new
                {
                    CesListImport = ceSpecialist,
                    Project = match
                };

            foreach (var match in matchingProjects)
            {
                ConfigureCesListImport(match.CesListImport, match.Project);
            }

            // Get a list of all current AD-419 projects:
            var currentProjects = _dataContext.CurrentAd419Projects.ToList();

            // Loop through the list of expenses that are not current linked to current projects:
            foreach (var ceSpecialist in ceSpecialists.Where(f => !f.IsCurrentAd419Project))
            {
                
                // Try updating the expense with a current project with the same project number:
                var originalAccessionNumber = ceSpecialist.ProjectAccessionNum;
                var projectNumber = ceSpecialist.ProjectNumber;
                var currentProject =
                    currentProjects.FirstOrDefault(p => p.ProjectNumber.Trim().Equals(projectNumber));
                
                // Update the accession number to the current project with a matching project number,
                // and update the message accordingly:
                if (currentProject != null)
                {
                    ceSpecialist.Message =
                        string.Format(
                            "Expired accession number {0} was remapped to active accession number {2} for project {1}.",
                            originalAccessionNumber, projectNumber, currentProject.AccessionNumber);
                    ceSpecialist.ProjectAccessionNum =
                        currentProjects.Where(p => p.ProjectNumber.Equals(projectNumber))
                            .Select(p => p.AccessionNumber)
                            .FirstOrDefault();
                    ceSpecialist.IsCurrentAd419Project = true;
                }
                else
                {
                    // Otherwise, set the message that we were unable to find a current project with a matching project number:
                    ceSpecialist.Message = string.Format("Unable to find active project for accession number {0}.", originalAccessionNumber);
                }
            }

            return ceSpecialists;
        }

        private void ConfigureCesListImport(CesListImport ceSpecialist, AllProjectsNew project)
        {
            if (project != null)
            {
                // Use the project number provided; otherwise update if blank:
                if (string.IsNullOrWhiteSpace(ceSpecialist.ProjectNumber))
                    ceSpecialist.ProjectNumber = project.ProjectNumber.Trim();

                // Determine if the expense belongs to a current project:
                ceSpecialist.IsCurrentAd419Project = IsCurrentAd419Project(project.ProjectStartDate,
                    project.IsExpired);
            }
        }

        /// <summary>
        /// Project is current if not expired and start date is not in the future.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="isExpired"></param>
        /// <returns></returns>
        private static bool IsCurrentAd419Project(DateTime? startDate, bool isExpired)
        {
            if (startDate == null)
                return false;

            return startDate < _fiscalEndDate && !isExpired;
        }
    }
}