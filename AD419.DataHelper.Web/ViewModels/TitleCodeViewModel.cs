using System.Collections.Generic;
using System.Linq;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class TitleCodeViewModel
    {
        public List<LaborTransactionsForTitlesWithMissingStaffTypes> LaborTransactionsForTitlesWithMissingStaffTypes { get; set; }

        public List<Titles> Titles { get; set; }

        public List<Titles> TitlesWithoutStaffType
        {
            get
            {
                var titles = LaborTransactionsForTitlesWithMissingStaffTypes.
                    GroupBy(l => new {l.TitleCd, l.TitleName}).
                    Select(g => g.First()).
                    Select(t => new Titles{TitleCode = t.TitleCd, Name = t.TitleName});

                return titles.ToList();
            }
        }
    }
} 