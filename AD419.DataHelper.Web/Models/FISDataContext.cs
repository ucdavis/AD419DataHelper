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
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
                .Property(e => e.FunctionName)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
               .Property(e => e.CategoryCode)
               .IsUnicode(false);

            modelBuilder.Entity<ArcCode>()
               .Property(e => e.SubCategoryCode)
               .IsUnicode(false);
        }
    }
}
