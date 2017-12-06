using System;
using System.Linq;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Helpers
{
    public static class ProcessStatusExtensions
    {
        public static AD419.DataHelper.Web.Models.ProcessStatus GetProcessStatus(AD419DataContext context,
            ProcessStatuses status)
        {
            var processStatus = context.ProcessStatuses.Find((int) status);

            if (processStatus == null)
            {
                throw new ArgumentException(string.Format("Could not find process status for {0}", status));
            }

            return processStatus;
        }

        // Mark process as completed
        public static void MarkStatusCompleted(this AD419DataContext context, ProcessStatuses status)
        {
            var processStatus = GetProcessStatus(context, status);

            processStatus.IsCompleted = true;
        }

        public static void ClearStatusCompleted(this AD419DataContext context, ProcessStatuses status)
        {
            var processStatus = GetProcessStatus(context, status);

            processStatus.IsCompleted = false;
        }

        public static void ClearStatusCompletedForNextCategory(this AD419DataContext context, ProcessStatuses status)
        {
            var nextProcessCategory = context.GetNextProcessCategory(status);

            var nextProcessStatuses = nextProcessCategory.Statuses;

            if (nextProcessStatuses == null)
            {
                throw new ArgumentException(string.Format("Could not find next process status(es) for {0}", status));
            }

            // Because there may be more than just a single process status to clear.
            //nextProcessStatuses.ForEach(n => n.IsCompleted = false);
            //nextProcessStatuses.All(c => { c.IsCompleted = false; return true; });
            nextProcessStatuses.Select(c => { c.IsCompleted = false; return c; }).ToList();
        }
    }
}