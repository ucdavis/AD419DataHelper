using System;
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
                    fieldStationExpense.ProjectNumber = project.ProjectNumber;

                // Determine if the expense belongs to a current project:
                fieldStationExpense.IsCurrentAd419Project = IsCurrentAd419Project(project.ProjectStartDate, project.IsExpired);
            }
        }

        private FieldStationExpenseListImport GetFieldStationExpenseFromRow(DataRow row)
        {
            var fieldStationExpense = new FieldStationExpenseListImport()
            {
                ProjectAccessionNum= row["ProjectAccessionNum"].ToString(),
                ProjectNumber      = row["Project"].ToString(),
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