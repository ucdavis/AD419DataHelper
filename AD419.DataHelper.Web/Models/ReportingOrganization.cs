using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD419.DataHelper.Web.Models
{
    [Table("ReportingOrg")]
    public class ReportingOrganization
    {
        [Key]
        [Column("OrgR")]
        public string OrganizationCode { get; set; }

        [Column("OrgName")]
        public string OrganizationName { get; set; }

        [Column("OrgShortName")]
        public string OrganizationShortName { get; set; }

        [Column("CRISDeptCd")]
        public string CrisDepartmentCode { get; set; }

        [Column("OrgCd3Char")]
        public string OrganizationShortCode { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("SecondAndThirdAcctNumCharacters")]
        public string AccountEndCharacters { get; set; }

        [Column("IsAdminCluster")]
        public bool? IsAdminCluster { get; set; }

        [Column("AdminClusterOrgR")]
        public string AdminClusterOrganizationCode { get; set; }
    }
}