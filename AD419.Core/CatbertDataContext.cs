using System;
using System.Data.Entity;
using AD419.Core.Models;

namespace AD419.Core
{
    public class CatbertDataContext : DbContext
    {
        public CatbertDataContext()
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public static CatbertDataContext Create()
        {
            return new CatbertDataContext();
        }
    }
}
