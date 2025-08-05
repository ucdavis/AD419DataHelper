using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class FundsViewModel : LaborTransactionsForMissingCodesModel
    {
        public List<Fund> Funds { get; set; }
    }
}