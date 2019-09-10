using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AD419.DataHelper.Web.Models
{
    public partial class OrgRListItem : SelectListItem
    {
        
        [Display(Name = "Org R")]
        public string OrgR { get; set; }

        public string Description { get; set; }
    }
}