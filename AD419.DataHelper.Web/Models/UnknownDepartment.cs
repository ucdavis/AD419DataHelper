﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AD419.DataHelper.Web.Models
{
    public class UnknownDepartment
    {
        public string Chart { get; set; }
        public string OrgR { get; set; }
        public string Account { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,0.00}")]
        public decimal Expenses { get; set; }
    }
}
