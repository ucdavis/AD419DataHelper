using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class InterdepartmentalProjectService
    {
        private readonly AD419DataContext _dataContext;
        private static DateTime _fiscalEndDate;
       
        public InterdepartmentalProjectService(AD419DataContext dataContext, DateTime fiscalEndDate)
        {
            _dataContext = dataContext;
            _fiscalEndDate = fiscalEndDate;
        }

        public IEnumerable<Interdepartmental> GetInterdepartmentalProjectsFromRows(DataRowCollection rows)
        {
            var allInterdepartmentalProjects = _dataContext.AllProjectsNew.Where(p => p.IsUcDavis && p.IsInterdepartmental).ToList();
            var interdepartmentalProjects = rows.ToEnumerable().Select(GetInterdepartmentalProjectFromRow).ToList();

            var matchingProjects =
                from interdepartmentalProject in interdepartmentalProjects
                join project in allInterdepartmentalProjects on interdepartmentalProject.AccessionNumber equals project.AccessionNumber
                into j
                from match in j.DefaultIfEmpty()
                select new
                {
                    InterdepartmentalProject = interdepartmentalProject,
                    Project = match
                };

            foreach (var match in matchingProjects)
            {
                ConfigureInterdepartmentalProject(match.InterdepartmentalProject, match.Project);
            }

            // Get a list of all current AD-419 interdepartmental projects:
            var currentProjects = _dataContext.CurrentAd419Projects.Where(p => p.IsInterdepartmental).ToList();

            // Loop through the list of records that are not current linked to current projects:
            foreach (var interdepartmentalProject in interdepartmentalProjects.Where(f => !f.IsCurrentAd419Project))
            {
                // Try updating the expense with a current project with the same project number:
                var originalAccessionNumber = interdepartmentalProject.AccessionNumber;
                var projectNumber = interdepartmentalProject.ProjectNumber;
                var currentProject =
                    currentProjects.FirstOrDefault(p => p.ProjectNumber.Trim().Equals(projectNumber));
                
                // Update the accession number to the current project with a matching project number,
                // and update the message accordingly:
                if (currentProject != null)
                {
                    interdepartmentalProject.Message =
                        string.Format(
                            "Expired accession number {0} was remapped to active accession number {2} for project {1}.",
                            originalAccessionNumber, projectNumber, currentProject.AccessionNumber);
                    interdepartmentalProject.AccessionNumber =
                        currentProjects.Where(p => p.ProjectNumber.Equals(projectNumber))
                            .Select(p => p.AccessionNumber)
                            .FirstOrDefault();
                    interdepartmentalProject.IsCurrentAd419Project = true;
                }
                else
                {
                    // Otherwise, set the message that we were unable to find a current project with a matching project number:
                    interdepartmentalProject.IsCurrentAd419Project = false;
                    interdepartmentalProject.Message = string.Format("Unable to find an active interdepartmental project for accession number {0}.", originalAccessionNumber);
                }
            }

            // Check for interdepartmental projects that do not have an entry present in the import file:
            // We want to get a list of projects that are not present in the import file.
            var missingProjects =
                currentProjects.Where(
                    p => interdepartmentalProjects.All(i => !p.AccessionNumber.Equals(i.AccessionNumber)));

            foreach (var project in missingProjects)
            {
                interdepartmentalProjects.Add(new Interdepartmental()
                {
                    AccessionNumber = project.AccessionNumber,
                    ProjectNumber = project.ProjectNumber,
                    IsCurrentAd419Project = true,
                    OrgR = "AINT",
                    Year = _fiscalEndDate.Year,
                    Message = string.Format("Warning: There is no entry present for accession number {0}.  Please add an entry for interdepartmental project {1}.", 
                    project.AccessionNumber, project.ProjectNumber),
                    IsValidOrgR = true,
                    IsPresentInFile = false
                });
            }

            // Check that all interdepartmental projects have valid orgRs assigned:
            var activeOrgRs = _dataContext.ReportingOrganizations.Where(r => r.IsActive || r.Code.Equals("AINT")).ToList();

            var projectsWithInvalidOrgRs =
                interdepartmentalProjects.Where(i => activeOrgRs.
                    All(o => !i.OrgR.Equals(o.Code)));

            foreach (var project in projectsWithInvalidOrgRs)
            {
                project.IsValidOrgR = false;
                project.Message = string.Format("Warning: The OrgR {1} provided for accession number {0}, is not valid.  Please update the entry to contain a valid OrgR and try again.", 
                    project.AccessionNumber, project.OrgR);
            }

            return interdepartmentalProjects;
        }

        private void ConfigureInterdepartmentalProject(Interdepartmental interdepartmentalProject, AllProjectsNew project)
        {
            if (project != null)
            {
                // Use the project number provided; otherwise update if blank:
                if (string.IsNullOrWhiteSpace(interdepartmentalProject.ProjectNumber))
                    interdepartmentalProject.ProjectNumber = project.ProjectNumber.Trim();

                // Determine if the expense belongs to a current project:
                interdepartmentalProject.IsCurrentAd419Project = IsCurrentAd419Project(project.ProjectStartDate,
                    project.IsExpired);
            }
        }

        private Interdepartmental GetInterdepartmentalProjectFromRow(DataRow row)
        {
            var interdepartmentalProject = new Interdepartmental()
            {
                AccessionNumber = row["AccessionNumber"].ToString(),
                OrgR = row["OrgR"].ToString()
            };

            // fix accession number
            if (string.IsNullOrWhiteSpace(interdepartmentalProject.AccessionNumber))
            {
                interdepartmentalProject.AccessionNumber = "0000000";
            }
            interdepartmentalProject.AccessionNumber = interdepartmentalProject.AccessionNumber.PadLeft(7, '0');

            interdepartmentalProject.IsValidOrgR = true;
            interdepartmentalProject.IsPresentInFile = true;

            return interdepartmentalProject;
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