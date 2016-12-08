using System.Collections.Generic;
using System.Linq;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class TitleCodeViewModel
    {
        public List<LaborTransactionsForTitlesWithMissingStaffTypes> LaborTransactionsForTitlesWithMissingStaffTypes { get; set; }

        public List<Titles> Titles { get; set; }

        public int TitlesWithoutStaffTypeCount
        {
            get { return TitlesWithoutStaffType.Count; }
        }

        public List<Titles> TitlesWithoutStaffType
        {
            get
            {
                return
                    LaborTransactionsForTitlesWithMissingStaffTypes.Select(t => new Titles {TitleCode = t.TitleCd, Name = t.TitleName})
                        .Distinct()
                        .ToList();
            }
        }
    }
} 