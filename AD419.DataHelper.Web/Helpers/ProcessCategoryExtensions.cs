using System;
using System.Linq;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Helpers
{
    public static class ProcessCategoryExtensions
    {
        public static ProcessCategory GetProcessCategory(this AD419DataContext context, ProcessStatuses status)
        {
            var processStatus = ProcessStatusExtensions.GetProcessStatus(context, status);

            var processCategory =
                context.ProcessCategories.FirstOrDefault(c => c.Statuses.FirstOrDefault().Equals(processStatus));

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

            var nextProcessCategory = context.ProcessCategories.FirstOrDefault(c => c.SequenceOrder.Equals(processCategory.SequenceOrder + 1));

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