namespace AD419.Core.Models
{
    public partial class Permission
    {
        public int PermissionID { get; set; }

        public int UserID { get; set; }

        public int? ApplicationID { get; set; }

        public int? RoleID { get; set; }

        public bool Inactive { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
