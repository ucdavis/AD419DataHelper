using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ReportingOrg")]
    public class ReportingOrganization
    {
        [Key]
        [Column("OrgR")]
        [MaxLength(4)]
        public string Code { get; set; }

        [Column("OrgName")]
        [MaxLength(40)]
        public string Name { get; set; }

        [DisplayName("Short Name")]
        [Column("OrgShortName")]
        [MaxLength(20)]
        public string ShortName { get; set; }

        [DisplayName("Short Code")]
        [Column("OrgCd3Char")]
        [MaxLength(3)]
        public string OrganizationShortCode { get; set; }

        [DisplayName("2nd & 3rd Account # Characters")]
        [Column("SecondAndThirdAcctNumCharacters")]
        [MaxLength(2)]
        public string AccountEndCharacters { get; set; }

        [DisplayName("Is Admin Cluster?")]
        [DefaultValue(false)]
        public bool? IsAdminCluster { get; set; }

        [DisplayName("Admin Cluster Organization")]
        [Column("AdminClusterOrgR")]
        [MaxLength(4)]
        public string AdminClusterOrganizationCode { get; set; }

        [DisplayName("Is Active?")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}