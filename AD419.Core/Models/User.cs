using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace AD419.Core.Models
{
    public class User : IUser<int>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Permissions = new HashSet<Permission>();
        }

        [Column("UserID")]
        public int Id { get; set; }

        [Column("LoginID")]
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(9)]
        public string EmployeeID { get; set; }

        [StringLength(9)]
        public string StudentID { get; set; }

        [StringLength(50)]
        public string UserImage { get; set; }

        [StringLength(50)]
        public string SID { get; set; }

        public bool Inactive { get; set; }

        public Guid UserKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
