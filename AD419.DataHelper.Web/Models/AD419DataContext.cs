using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AD419.DataHelper.Web.Models
{
    public class AD419DataContext : DbContext
    {
        public AD419DataContext()
            : base("name=AD419DataContext")
        {
        }

        public virtual DbSet<ArcCode> ARC_Codes { get; set; }

        public virtual DbSet<ArcCodeSelections> ArcCodes { get; set; }

        public virtual DbSet<AllProject> AllProjects { get; set; }

        public virtual DbSet<AllProjectsNew> AllProjectsNew { get; set; }

        public virtual DbSet<ArcCodeAccountExclusion> ArcCodeAccountExclusions { get; set; }

        public virtual DbSet<C204AcctXProj> C204AcctXProj { get; set; }

        public virtual DbSet<C204Exclusions> C204Exclusions { get; set; }

        public virtual DbSet<CesListImport> CesListImports { get; set; }

        public virtual DbSet<CfdaNumberImport> CfdaNumberImports { get; set; }

        public virtual DbSet<ConsolidationCodes> ConsolidationCodes { get; set; }

        public virtual DbSet<ConsolidationCodesForFTECalc> ConsolidationCodesForFTECalc { get; set; }

        public virtual DbSet<CurrentAd419Project> CurrentAd419Projects { get; set; }

        public virtual DbSet<DosCode> DosCodes { get; set; }

        public virtual DbSet<DosCodesForFTECalc> DosCodesForFTECalc { get; set; }

        public virtual DbSet<ExpiredProjectCrossReference> ExpiredProjectCrossReference { get; set; }

        public virtual DbSet<ExpenseOrgMapping> ExpenseOrgMappings { get; set; }

        public virtual DbSet<FieldStationExpenseListImport> FieldStationExpenseListImports { get; set; }

        public virtual DbSet<FfySfnEntry> FfySfnEntries { get; set; }

        public virtual DbSet<FfySfnEntryWithAccount> FfySfnEntriesWithAccounts { get; set; }

        public virtual DbSet<Interdepartmental> Interdepartmentals { get; set; }

        public virtual DbSet<TransDocType> TransDocTypes { get; set; }

        public virtual DbSet<TransDocTypesForFTECalc> TransDocTypesForFTECalc { get; set; }

        public virtual DbSet<ReportingOrganization> ReportingOrganizations { get; set; }

        public virtual DbSet<PrincipalInvestigator> PrincipalInvestigators { get; set; }

        public virtual DbSet<PrincipalInvestigatorMatch> PrincipalInvestigatorMatches { get; set; }

        public virtual DbSet<ProcessStatus> ProcessStatuses { get; set; }

        public virtual DbSet<ProcessCategory> ProcessCategories { get; set; }

        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }

        public virtual DbSet<LaborTransaction> LaborTransactions { get; set; }

        public virtual DbSet<SfnClassificationLogic> SfnClassificationLogic { get; set; }

        public virtual DbSet<StaffType> StaffTypes { get; set; } 

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

        public virtual DbRawSqlQuery<UnknownDepartment> GetUnknownDepartments()
        {
            return Database.SqlQuery<UnknownDepartment>(
                "SELECT * FROM [dbo].[udf_GetExpensesForNullOrUnknownDepartments] ()");
        }

        public virtual DbRawSqlQuery<UnknownDepartmentAccountDetail> GetAccountDetailsForNullOrUnknownDepartments()
        {
            return Database.SqlQuery<UnknownDepartmentAccountDetail>(
                "SELECT * FROM [dbo].[udf_GetAccountDetailsForNullOrUnknownDepartments] ()");
        }

        /// <summary>
        /// Gets a list of missing Labor Transactions for which we the corresponding type of code is
        /// not one we were aware of so it can be reviewed and added, included or excluded from the 
        /// appropriate list. 
        /// </summary>
        /// <param name="option">0: All, 1: ConsolidationCodes, 2: TransDocTypes, 3: DosCodes</param>
        /// <returns>A list of Labor Transactions for the option provided.</returns>
        public virtual DbRawSqlQuery<LaborTransaction> GetLaborTransactions(int option)
        {
            return Database.SqlQuery<LaborTransaction>(
                "SELECT * FROM [dbo].[udf_GetTransactionsForUnknownCodes] (@Option)",
                    new SqlParameter("@Option", SqlDbType.Int) { Value = option });
        }

        public virtual string GetSfnCaseStatement()
        {
            using (var conn = Database.Connection)
            {
                using (var command = conn.CreateCommand())
                {
                    conn.Open();

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[usp_GenerateNewAccountSfnCaseStatement]";

                    var parameter = command.CreateParameter();
                    parameter.DbType = DbType.String;
                    parameter.Size = int.MaxValue;
                    parameter.Direction = ParameterDirection.Output;
                    parameter.ParameterName = "@CaseStatement";
                    parameter.Value = string.Empty;
                    command.Parameters.Add(parameter);
                    
                    command.ExecuteNonQuery();
                    var caseStatement = command.Parameters["@CaseStatement"].Value as string;

                    return caseStatement;
                }
            }
        }

        public virtual DbRawSqlQuery<LaborTransactionsForTitlesWithMissingStaffTypes> GetTitlesWithMissingStaffTypes()
        {
            const string sql = @"SELECT
       [AnnualReportCode]
	  ,[ARC_Name]
	  ,[OrgR]
	  ,[TitleCd]
      ,[Name] TitleName
	  ,[EmployeeName]
	  ,[EmployeeID]
	  ,[Chart]
	  ,[Org]
      ,[Account]
      ,[SubAccount]
      ,[PrincipalInvestigator]
	  ,SUM([Amount]) Amount  
      ,SUM([FTE]) FTE
  FROM [dbo].[PPS_ExpensesForNon204Projects]
  INNER JOIN PPSDataMart.dbo.Titles ON TitleCd = TitleCode
  INNER JOIN FISDataMart.dbo.Arc_Codes ON AnnualReportCode = ARC_Cd
  WHERE StaffType IS NULL OR StaffType LIKE ''
  GROUP BY  
       [Chart]
      ,[Account]
      ,[SubAccount]
      ,[PrincipalInvestigator]
      ,[OrgR]
      ,[Org]
      ,[EmployeeID]
      ,[EmployeeName]
      ,[TitleCd]
	  ,[Name]
	  ,[AnnualReportCode]
	  ,[ARC_Name]
  HAVING SUM(FTE) <> 0

  UNION

  SELECT
       [AnnualReportCode]
	  ,[ARC_Name]
	  ,[OrgR]
	  ,[TitleCd]
      ,[Name] TitleName
	  ,[EmployeeName]
	  ,[EmployeeID]
	  ,[Chart]
	  ,[Org]
      ,[Account]
      ,[SubAccount]
      ,[PrincipalInvestigator]
	  ,SUM([Amount]) Amount  
      ,SUM([FTE]) FTE
  FROM [dbo].[PPS_ExpensesFor204Projects]
  INNER JOIN PPSDataMart.dbo.Titles ON TitleCd = TitleCode
  INNER JOIN FISDataMart.dbo.Arc_Codes ON AnnualReportCode = ARC_Cd
  WHERE StaffType IS NULL OR StaffType LIKE ''
  GROUP BY  
       [Chart]
      ,[Account]
      ,[SubAccount]
      ,[PrincipalInvestigator]
      ,[OrgR]
      ,[Org]
      ,[EmployeeID]
      ,[EmployeeName]
      ,[TitleCd]
	  ,[Name]
	  ,[AnnualReportCode]
	  ,[ARC_Name]
	HAVING SUM(FTE) <> 0
	ORDER BY 
	   [TitleCd]
	  ,[Name]
      ,[Chart]
      ,[Account]
      ,[SubAccount]
      ,[PrincipalInvestigator]
      ,[OrgR]
      ,[Org]
      ,[EmployeeID]
      ,[EmployeeName]
	  ,[AnnualReportCode]
	  ,[ARC_Name]";

            return Database.SqlQuery<LaborTransactionsForTitlesWithMissingStaffTypes>(sql);
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
