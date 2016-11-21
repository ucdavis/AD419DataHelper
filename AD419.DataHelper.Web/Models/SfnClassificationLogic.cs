using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    public partial class SfnClassificationLogic
    {
        [NotMapped]
        public static string[] LogicalOperators = {"AND", "OR", "NULL"};

        [NotMapped]
        public static string[] ConditionalOperators = {"=", "BETWEEN", "IN", "LIKE"};

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Evaluation Order")]
        public int EvaluationOrder { get; set; }

        [Required]
        [Display(Name = "Parameter Order")]
        public int ParameterOrder { get; set; }


        [Display(Name = "Sub Parameter Order")]
        public int? SubParameterOrder { get; set; }

        [Required]
        [Display(Name = "Logical Operator")]
        public string LogicalOperator { get; set; }

        [Required]
        [Display(Name = "Column Name")]
        public string ColumnName { get; set; }

        [Display(Name = "Negate Condition?")]
        public bool? NegateCondition { get; set; }

        [Required]
        [Display(Name = "Conditional Operator")]
        public string ConditionalOperator { get; set; }

        [Required]
        [Display(Name = "Values")]
        public string Values { get; set; }

        [Required]
        [Display(Name = "SFN")]
        public string SFN { get; set; }
    }
}