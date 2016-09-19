using System.Collections.Generic;

namespace AD419.DataHelper.Web.Models
{
    public class DosCodesViewModel : LaborTransactionsForMissingCodesModel
    {
        public List<DosCode> DosCodes { get; set; }
    }
}