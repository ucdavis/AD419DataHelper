using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class AccountsWithMissingSfnEditViewModel
    {
        public AccountsWithMissingSfnEditViewModel() { }
        public AccountsWithMissingSfnEditViewModel(AccountsWithMissingSfn newAccountSfn, List<SfnListItem> sfnListItems)
        {
            NewAccountSfn = newAccountSfn;

            var sfnSelectlist = new List<SelectListItem>();

            foreach (var sfn in sfnListItems.OrderBy(s => s.Sfn))
            {
                sfnSelectlist.Add(new SelectListItem()
                {
                    Text = string.Format("{0} - {1}", sfn.Sfn, sfn.Description),
                    Value = sfn.Sfn,
                    Selected =
                    (!string.IsNullOrEmpty(NewAccountSfn.SFN) &&
                     NewAccountSfn.SFN.Equals(sfn.Sfn))
                });
            }
            SfnSelectList = sfnSelectlist;

        }

        public AccountsWithMissingSfn NewAccountSfn { get; set; }

        public List<SelectListItem> SfnSelectList { get; set; }

    }
}