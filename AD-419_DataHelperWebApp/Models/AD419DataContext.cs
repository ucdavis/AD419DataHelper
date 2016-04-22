namespace AD_419_DataHelperWebApp.Models
{
    using System.Data.Entity;

    public partial class AD419DataContext : DbContext
    {
        public AD419DataContext()
            : base("name=AD419DataContext")
        {
        }

        public virtual DbSet<C204AcctXProj> C204AcctXProj { get; set; }
        public virtual DbSet<C204Exclusions> C204Exclusions { get; set; }
        public virtual DbSet<AllProject> AllProjects { get; set; }
        public virtual DbSet<ArcCodeAccountExclusion> ArcCodeAccountExclusions { get; set; }
        public virtual DbSet<CesListImport> CesListImports { get; set; }
        public virtual DbSet<ExpiredProjectCrossReference> ExpiredProjectCrossReference { get; set; }

        public virtual DbSet<FieldStationExpenseListImport> FieldStationExpenseListImports { get; set; }

        public virtual DbSet<CFDANumImport> CFDANumImports { get; set; }

        public virtual DbSet<AllProjectsNew> AllProjectsNew { get; set; }

        public virtual DbSet<AllProjectsImport> AllProjectsImmport { get; set; }

        public virtual DbSet<DOS_Codes> DOS_Codes { get; set; }

        //public static void Clear<T>(this DbSet<T> dbSet) where T : class
        //{
        //    dbSet.RemoveRange(dbSet);
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.AccountID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.Accession)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.Chart)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.CSREES_ContractNo)
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.AwardNum)
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.Org)
                .IsUnicode(false);

            modelBuilder.Entity<C204AcctXProj>()
                .Property(e => e.OrgR)
                .IsUnicode(false);

            modelBuilder.Entity<C204Exclusions>()
                .Property(e => e.AwardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<C204Exclusions>()
               .Property(e => e.Comments)
               .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.Accession)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.Project)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.ProjTypeCd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.RegionalProjNum)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.CRIS_DeptID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.CoopDepts)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.CSREES_ContractNo)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.StatusCd)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv1)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv2)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv3)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv4)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv5)
                .IsUnicode(false);

            modelBuilder.Entity<AllProject>()
                .Property(e => e.inv6)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCodeAccountExclusion>()
                .Property(e => e.Chart)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCodeAccountExclusion>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<ArcCodeAccountExclusion>()
                .Property(e => e.AnnualReportCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ArcCodeAccountExclusion>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.PI)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.DeptLevelOrg)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.EmployeeId)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.ProjectAccessionNum)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.ProjectNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.PercentCeEffort)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.TitleCode)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.FTE)
                .HasPrecision(10, 2);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.Chart)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<CesListImport>()
                .Property(e => e.SubAccount)
                .IsUnicode(false);

            modelBuilder.Entity<ExpiredProjectCrossReference>()
                .Property(e => e.FromAccession)
                .IsUnicode(false);

            modelBuilder.Entity<ExpiredProjectCrossReference>()
                .Property(e => e.ToAccession)
                .IsUnicode(false);

            modelBuilder.Entity<FieldStationExpenseListImport>()
                .Property(e => e.ProjectAccessionNum)
                .IsUnicode(false);

            modelBuilder.Entity<FieldStationExpenseListImport>()
                .Property(e => e.FieldStationCharge)
                .HasPrecision(19, 4);

            modelBuilder.Entity<FieldStationExpenseListImport>()
                .Property(e => e.ProjectDirector)
                .IsUnicode(false);

            modelBuilder.Entity<CFDANumImport>()
                .Property(e => e.CFDANum)
                .IsUnicode(false);

            modelBuilder.Entity<CFDANumImport>()
               .Property(e => e.ProgramTitle)
               .IsUnicode(false);

            modelBuilder.Entity<AllProjectsNew>()
               .Property(e => e.OrgR)
               .IsUnicode(false);

            modelBuilder.Entity<AllProjectsNew>()
                .Property(e => e.CoProjectDirectors)
                .IsUnicode(false);
        }
    }
}
