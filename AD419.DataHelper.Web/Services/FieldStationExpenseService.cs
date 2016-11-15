﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class FieldStationExpenseService
    {
        private readonly AD419DataContext _dataContext;
        private static DateTime _fiscalEndDate;

        public FieldStationExpenseService(AD419DataContext dataContext, DateTime fiscalEndDate)
        {
            _dataContext = dataContext;
            _fiscalEndDate = fiscalEndDate;
        }

        public IEnumerable<FieldStationExpenseListImport> GetFieldStationsExpensesFromRows(DataRowCollection rows)
        {
            var allProjects = _dataContext.AllProjectsNew.Where(p => p.IsUcDavis).ToList();

            var fieldStationExpenses = rows.ToEnumerable().Select(GetFieldStationExpenseFromRow).ToList();

            var matchingProjects =
                from fieldStationExpense in fieldStationExpenses
                join project in allProjects on fieldStationExpense.ProjectAccessionNum equals project.AccessionNumber
                into j
                from match in j.DefaultIfEmpty()
                select new
                {
                    FieldStationExpense = fieldStationExpense,
                    Project = match
                };

            foreach (var match in matchingProjects)
            {
                ConfigureFieldStationExpense(match.FieldStationExpense, match.Project);
            }

            // Get a list of all current AD-419 projects:
            var currentProjects = _dataContext.CurrentAd419Projects.ToList();

            // Loop through the list of expenses that are not current linked to current projects:
            foreach (var fieldStationExpense in fieldStationExpenses.Where(f => !f.IsCurrentAd419Project))
            {
                
                // Try updating the expense with a current project with the same project number:
                var originalAccessionNumber = fieldStationExpense.ProjectAccessionNum;
                var projectNumber = fieldStationExpense.ProjectNumber;
                var currentProject =
                    currentProjects.FirstOrDefault(p => p.ProjectNumber.Trim().Equals(projectNumber));
                
                // Update the accession number to the current project with a matching project number,
                // and update the message accordingly:
                if (currentProject != null)
                {
                    fieldStationExpense.Message =
                        string.Format(
                            "Expired accession number {0} was remapped to active accession number {2} for project {1}.",
                            originalAccessionNumber, projectNumber, currentProject.AccessionNumber);
                    fieldStationExpense.ProjectAccessionNum =
                        currentProjects.Where(p => p.ProjectNumber.Equals(projectNumber))
                            .Select(p => p.AccessionNumber)
                            .FirstOrDefault();
                    fieldStationExpense.IsCurrentAd419Project = true;
                }
                else
                {
                    // Otherwise, set the message that we were unable to find a current project with a matching project number:
                    fieldStationExpense.Message = string.Format("Unable to find active project for accession number {0}.", originalAccessionNumber);
                }
            }

            //var correspondingProjects =
            //    fieldStationExpenses.Where(f => _dataContext.AllProjectsNew.Where(p => p.IsUcDavis).
            //        All(p => f.ProjectAccessionNum.Equals(p.AccessionNumber)));
         
            return fieldStationExpenses;
        }

        private void ConfigureFieldStationExpense(FieldStationExpenseListImport fieldStationExpense, AllProjectsNew project)
        {
            if (project != null)
            {
                // Use the project number provided; otherwise update if blank:
                if (string.IsNullOrWhiteSpace(fieldStationExpense.ProjectNumber))
                    fieldStationExpense.ProjectNumber = project.ProjectNumber.Trim();

                // Determine if the expense belongs to a current project:
                fieldStationExpense.IsCurrentAd419Project = IsCurrentAd419Project(project.ProjectStartDate,
                    project.IsExpired);
            }
        }

        private FieldStationExpenseListImport GetFieldStationExpenseFromRow(DataRow row)
        {
            var fieldStationExpense = new FieldStationExpenseListImport()
            {
                ProjectAccessionNum= row["ProjectAccessionNum"].ToString(),
                FieldStationCharge = string.IsNullOrWhiteSpace(row["FieldStationCharge"].ToString()) ? 0 : Convert.ToDecimal(row["FieldStationCharge"].ToString())
            };

            // fix accession number
            if (string.IsNullOrWhiteSpace(fieldStationExpense.ProjectAccessionNum))
            {
                fieldStationExpense.ProjectAccessionNum = "0000000";
            }
            fieldStationExpense.ProjectAccessionNum = fieldStationExpense.ProjectAccessionNum.PadLeft(7, '0');

            return fieldStationExpense;
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