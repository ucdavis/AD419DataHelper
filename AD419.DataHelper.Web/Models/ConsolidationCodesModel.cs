﻿using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class ConsolidationCodesModel
    {
        public List<ConsolidationCodes> ConsolidationCodes { get; set; }

        public LaborTransactionsForMissingCodesModel LaborTransactionsForMissingCodesModel { get; set; }
    }
}