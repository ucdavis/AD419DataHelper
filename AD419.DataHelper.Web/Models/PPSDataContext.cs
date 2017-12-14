namespace AD419.DataHelper.Web.Models
{
    using System.Data.Entity;

    public partial class PpsDataContext : DbContext
    {
        public PpsDataContext()
            : base("name=PpsDataContext")
        {
        }

        public virtual DbSet<Titles> Titles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Titles>()
                .Property(e => e.TitleCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.AbbreviatedName)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.PersonnelProgramCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.UnitCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.TitleGroup)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.OvertimeExemptionCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.CTOOccupationSubgroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.FederalOccupationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.FOCSubcategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.LinkTitleGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.EE06CategoryCode)
                .IsUnicode(false);

            modelBuilder.Entity<Titles>()
                .Property(e => e.StaffType)
                .IsFixedLength()
                .IsUnicode(false);
        }

        public virtual DbSet<SelfCertifyingTitleCode> SelfCertifyingTitleCodes { get; set; }
    }
}
