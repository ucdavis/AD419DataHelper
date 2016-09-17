using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class LaborTransactionsForMissingCodesModel
    {
        public List<LaborTransaction> LaborTransactions { get; set; }
        public string CodeTypeName { get; set; }
    }
}