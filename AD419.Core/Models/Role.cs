using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace AD419.Core.Models
{
    public partial class Role : IRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            Permissions = new HashSet<Permission>();
        }

        public int RoleID { get; set; }

        [Column("Role")]
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        public bool Inactive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permission> Permissions { get; set; }

        #region IRole Properties
        [NotMapped]
        public string Id
        {
            get { return RoleID.ToString(); }
        }

        [NotMapped]
        public string Name
        {
            get { return RoleName; }
            set { RoleName = value; }
        }
        #endregion 
    }
}
