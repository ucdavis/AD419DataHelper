using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("Natural_Acct")]
    public class NaturalAccount
    {
        public string Account_Parent_5 { get; set; }
        public string Account_Parent_5_Description { get; set; }

        public string Account_Parent_4 { get; set; }
        public string Account_Parent_4_Description { get; set; }

        public string Account_Parent_3 { get; set; }
        public string Account_Parent_3_Description { get; set; }

        public string Account_Parent_2 { get; set; }
        public string Account_Parent_2_Description { get; set; }

        public string Account_Parent_1 { get; set; }
        public string Account_Parent_1_Description { get; set; }

        [Key]
        public string Account_Child_Posting_Level { get; set; }
        public string Account_Child_Posting_Description { get; set; }

        [Column("Include_in_AD-419?")]
        public bool? IncludeInAD419 { get; set; }

        [Column("Present_in_Financial_Data?")]
        public bool? PresentInFinancialData { get; set; }
    }

    public class NaturalAccountsViewModel
    {
        public List<NaturalAccount> NaturalAccounts { get; set; }
        public string CodeTypeName { get; set; }
    }
}