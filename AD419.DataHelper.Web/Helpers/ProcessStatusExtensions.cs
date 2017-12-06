using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Helpers
{
    public static class ProcessStatusExtensions
    {
        // Mark process as completed
        public static void MarkStatusCompleted(this AD419DataContext context, ProcessStatuses status)
        {
            var processStatus = context.ProcessStatuses.Find((int)status);

            if (processStatus == null)
            {
                throw new ArgumentException(string.Format("Could not find process status for {0}", status));
            }

            processStatus.IsCompleted = true;
        }
    }
}