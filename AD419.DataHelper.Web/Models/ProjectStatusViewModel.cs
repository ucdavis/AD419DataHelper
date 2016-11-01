
using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class ProjectStatusViewModel
    {
        public List<CurrentAd419Project> CurrentAd419Projects { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }
    }
}