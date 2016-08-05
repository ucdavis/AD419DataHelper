using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AD419.DataHelper.Web.Models
{
    public partial class AD419DataContext : DbContext
    {
        public AD419DataContext()
            : base("name=AD419DataContext")
        {
        }

        public virtual DbSet<AllProject> AllProjects { get; set; }

        public virtual DbSet<AllProjectsNew> AllProjectsNew { get; set; }

        public virtual DbSet<ArcCodeAccountExclusion> ArcCodeAccountExclusions { get; set; }

        public virtual DbSet<C204AcctXProj> C204AcctXProj { get; set; }

        public virtual DbSet<C204Exclusions> C204Exclusions { get; set; }

        public virtual DbSet<CesListImport> CesListImports { get; set; }

        public virtual DbSet<CfdaNumberImport> CfdaNumberImports { get; set; }

        public virtual DbSet<ConsolidationCodes> ConsolidationCodes { get; set; }

        public virtual DbSet<ConsolidationCodesForFTECalc> ConsolidationCodesForFTECalc { get; set; }

        public virtual DbSet<DosCode> DosCodes { get; set; }

        public virtual DbSet<DosCodesForFTECalc> DosCodesForFTECalc { get; set; }

        public virtual DbSet<ExpiredProjectCrossReference> ExpiredProjectCrossReference { get; set; }

        public virtual DbSet<ExpenseOrgMapping> ExpenseOrgMappings { get; set; }

        public virtual DbSet<FieldStationExpenseListImport> FieldStationExpenseListImports { get; set; }

        public virtual DbSet<Interdepartmental> Interdepartmentals { get; set; }

        public virtual DbSet<TransDocTypes> TransDocTypes { get; set; }

        public virtual DbSet<TransDocTypesForFTECalc> TransDocTypesForFTECalc { get; set; }

        public virtual DbSet<ReportingOrganization> ReportingOrganizations { get; set; }

        public virtual DbRawSqlQuery<AllProjectsNew> GetNewProjects(int fiscalYear)
        {
            return Database.SqlQuery<AllProjectsNew>(
                "SELECT * FROM [dbo].[udf_AllProjectsNewForFiscalYear] (@FiscalYear)",
                    new SqlParameter("@FiscalYear", SqlDbType.Int) { Value = fiscalYear }
                );
        }

        public virtual async Task<List<ExpiringProject>> GetExpired20XProjects(int fiscalYear)
        {
            return await Database.SqlQuery<ExpiringProject>(
                "SELECT * FROM [dbo].[udf_GetExpired20xProjects] (@FiscalYear)",
                    new SqlParameter("@FiscalYear", SqlDbType.Int) { Value = fiscalYear }
                ).ToListAsync();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            CreateAllProject(modelBuilder);

            CreateAllProjectsNew(modelBuilder);

            CreateArcCodeAccountExclusion(modelBuilder);

            CreateC204AcctXProj(modelBuilder);

            CreateC204Exclusions(modelBuilder);

            CreateCesListImport(modelBuilder);

            CreateCfdaNumberImport(modelBuilder);

            CreateExpiredProjectCrossReference(modelBuilder);

            CreateFieldStationExpenseListImport(modelBuilder);
        }

        private static void CreateAllProjectsNew(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllProjectsNew>()
                .Property(e => e.OrgR)
                .IsUnicode(false);

            modelBuilder.Entity<AllProjectsNew>()
                .Property(e => e.CoProjectDirectors)
                .IsUnicode(false);
        }

        private static void CreateCfdaNumberImport(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CfdaNumberImport>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<CfdaNumberImport>()
                .Property(e => e.ProgramTitle)
                .IsUnicode(false);
        }

        private static void CreateFieldStationExpenseListImport(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FieldStationExpenseListImport>()
                .Property(e => e.ProjectAccessionNum)
                .IsUnicode(false);

            modelBuilder.Entity<FieldStationExpenseListImport>()
                .Property(e => e.FieldStationCharge)
                .HasPrecision(19, 4);
        }

        private static void CreateExpiredProjectCrossReference(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpiredProjectCrossReference>()
                .Property(e => e.FromAccession)
                .IsUnicode(false);

            modelBuilder.Entity<ExpiredProjectCrossReference>()
                .Property(e => e.ToAccession)
                .IsUnicode(false);
        }

        private static void CreateCesListImport(DbModelBuilder modelBuilder)
        {
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
        }

        private static void CreateArcCodeAccountExclusion(DbModelBuilder modelBuilder)
        {
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
        }

        private static void CreateAllProject(DbModelBuilder modelBuilder)
        {
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
        }

        private static void CreateC204Exclusions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<C204Exclusions>()
                .Property(e => e.AwardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<C204Exclusions>()
                .Property(e => e.Comments)
                .IsUnicode(false);
        }

        private static void CreateC204AcctXProj(DbModelBuilder modelBuilder)
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
        }
    }
}
