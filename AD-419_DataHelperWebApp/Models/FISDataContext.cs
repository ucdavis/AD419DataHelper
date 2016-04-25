namespace AD_419_DataHelperWebApp.Models
{
    using System.Data.Entity;

    public partial class FISDataContext : DbContext
    {
        public FISDataContext()
            : base("name=FISDataContext")
        {
        }

        public virtual DbSet<ArcCode> ARC_Codes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArcCode>()
                .Property(e => e.ARC_Cd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
                .Property(e => e.ARC_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
                .Property(e => e.OP_Func_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
               .Property(e => e.ARC_Category_Cd)
               .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
               .Property(e => e.ARC_Sub_Category_Cd)
               .IsUnicode(false);
        }
    }
}
