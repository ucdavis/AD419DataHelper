using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ActivityValues_New")]
    public class ActivityModel
    {
        [Key]
        public int Id { get; set; }

        public string Activity { get; set; }

        public string Activity_Description { get; set; }

        public bool? IsExcluded { get; set; }
    }

    public class ActivitiesViewModel
    {
        public List<ActivityModel> Activities { get; set; }
        public string CodeTypeName { get; set; }
    }
}