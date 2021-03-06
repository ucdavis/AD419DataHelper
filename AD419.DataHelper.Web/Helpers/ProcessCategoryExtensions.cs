﻿using System;
using System.Data.Entity;
using System.Linq;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Helpers
{
    public static class ProcessCategoryExtensions
    {
        public static ProcessCategory GetProcessCategory(this AD419DataContext context, ProcessStatuses status)
        {
            // returns the process status for the given ID
            var processStatus = ProcessStatusExtensions.GetProcessStatus(context, status);

            var processCategory = context.ProcessCategories.FirstOrDefault(c => c.Id == processStatus.CategoryId);

            if (processCategory == null)
            {
                throw new ArgumentException(string.Format("Could not find process category for process status {0}",
                    status));
            }

            return processCategory;
        }

        public static ProcessCategory GetNextProcessCategory(this AD419DataContext context, ProcessStatuses status)
        {
            var processCategory = GetProcessCategory(context, status);

            var nextProcessCategory = context.ProcessCategories.Where(c => c.SequenceOrder == processCategory.SequenceOrder + 1).
                Include(c => c.Statuses).FirstOrDefault();

            if (nextProcessCategory == null)
            {
                throw new ArgumentException(string.Format("Could not find next process category for process status {0}", status));
            }

            return nextProcessCategory;
        }

        // Mark category as completed
        public static void MarkCategoryCompleted(this AD419DataContext context, ProcessStatuses status)
        {
            var processCategory = GetProcessCategory(context, status);

            processCategory.IsCompleted = true;
        }

        public static void ClearCategoryCompleted(this AD419DataContext context, ProcessStatuses status)
        {
            var processCategory = GetProcessCategory(context, status);

            processCategory.IsCompleted = false;
        }

        public static void ClearCategoryCompletedForNextCategory(this AD419DataContext context, ProcessStatuses status)
        {
            var nextProcessCategory = GetNextProcessCategory(context, status);

            nextProcessCategory.IsCompleted = false;
        }
    }
}