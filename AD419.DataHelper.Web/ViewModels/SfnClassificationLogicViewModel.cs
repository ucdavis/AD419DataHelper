using System.Collections.Generic;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class SfnClassificationLogicViewModel
    {
        public SfnClassificationLogicViewModel(SfnClassificationLogic model)
        {
            SfnClassificationLogic = model;

            // Initialize logical operators select list:
            var logicalOperators = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "",
                    Value = null,
                    Selected = true
                }
            };

            foreach (var logicalOperator in SfnClassificationLogic.LogicalOperators)
            {
                logicalOperators.Add(new SelectListItem()
                {
                    Text = logicalOperator,
                    Value = logicalOperator,
                    Selected =
                        (!string.IsNullOrEmpty(SfnClassificationLogic.LogicalOperator) && SfnClassificationLogic.LogicalOperator.Equals(logicalOperator))
                });
            }
            LogicalOperatorsSelectList = logicalOperators;


            // Initialize conditional operators select list:
            var conditionalOperators = new List<SelectListItem>();

            foreach (var conditionalOperator in SfnClassificationLogic.ConditionalOperators)
            {
                conditionalOperators.Add(new SelectListItem()
                {
                    Text = conditionalOperator,
                    Value = conditionalOperator,
                    Selected = SfnClassificationLogic.ConditionalOperator.Equals(conditionalOperator)
                });
            }

            ConditionalOperatorsSelectList = conditionalOperators;
        }

        public List<SelectListItem> ConditionalOperatorsSelectList { get; set; }
        public List<SelectListItem> LogicalOperatorsSelectList { get; set; }

        public SfnClassificationLogic SfnClassificationLogic { get; set; }  
    }
}