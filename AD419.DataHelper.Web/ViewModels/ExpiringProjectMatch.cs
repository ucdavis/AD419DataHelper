using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class ExpiringProjectMatch
    {
        public ExpiringProject ExpiringProject { get; set; }

        public ExpiredProjectCrossReference MatchedProject { get; set; }
    }
}