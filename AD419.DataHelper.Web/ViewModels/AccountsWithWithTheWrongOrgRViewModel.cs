using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class AccountsWithWithTheWrongOrgRViewModel
    {
        public AccountsWithWithTheWrongOrgRViewModel() { }

        public AccountsWithWithTheWrongOrgRViewModel(AccountWithTheWrongClosedOrg accountWithTheWrongClosedOrg,
            AD419Account ad419Account)
        {
            Ad419Account = ad419Account;

            AccountWithTheWrongClosedOrg = accountWithTheWrongClosedOrg;

            var selectedValue = accountWithTheWrongClosedOrg.CurrentOrgR;
            var selectedText = string.Format("{0} - {1}*", accountWithTheWrongClosedOrg.CurrentOrgR,
                accountWithTheWrongClosedOrg.CurrentOrg);

            var orgRSelectList = new SelectList(new List<SelectListItem>() {
                new SelectListItem() 
                {
                    Text = string.Format("{0} - {1}*", accountWithTheWrongClosedOrg.CurrentOrgR, accountWithTheWrongClosedOrg.CurrentOrg),
                    Value = accountWithTheWrongClosedOrg.CurrentOrgR,
                    Selected = true
                },
                new SelectListItem()
                {
                    Text = string.Format("{0} - {1}", accountWithTheWrongClosedOrg.LatestNonClosedOrgR,
                        accountWithTheWrongClosedOrg.LatestNonClosedOrg),
                    Value = accountWithTheWrongClosedOrg.LatestNonClosedOrgR
                }
            }, "Value", "Text", accountWithTheWrongClosedOrg.CurrentOrgR.ToString());

            OrgRSelectionList = orgRSelectList;
        }

        public AccountWithTheWrongClosedOrg AccountWithTheWrongClosedOrg { get; set; }

        public SelectList OrgRSelectionList { get; set; }

        public AD419Account Ad419Account { get; set; }

    }
}