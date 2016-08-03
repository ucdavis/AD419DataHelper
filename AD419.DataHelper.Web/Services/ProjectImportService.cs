using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class ProjectImportService
    {
        private readonly AD419DataContext _dataContext;


        public ProjectImportService(AD419DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<AllProjectsNew> GetProjectsFromRows(DataRowCollection rows)
        {
            var projects = rows.ToEnumerable().Select(GetProjectFromRow).ToList();
            
            // setup organiztion references
            var reportingOrganizations = _dataContext.ReportingOrganizations
                .Where(r => r.IsActive).ToList();

            var matches = 
                from project in projects
                join organization in reportingOrganizations on project.ShortCode equals organization.OrganizationShortCode into j
                from match in j.DefaultIfEmpty()
                select new { Project = project, Organization = match };

            foreach (var match in matches)
            {
                ConfigureReportingOrg(match.Project, match.Organization);
            }

            return projects;
        }

        private AllProjectsNew GetProjectFromRow(DataRow row)
        {
            var project = new AllProjectsNew()
            {
                AccessionNumber    = row["Accession Number"].ToString(),
                ProjectNumber      = row["Project Number"].ToString(),
                ProposalNumber     = row["Proposal Number"].ToString(),
                AwardNumber        = row["Award Number"].ToString(),
                Title              = row["Title"].ToString(),
                OrganizationName   = row["Organization Name"].ToString(),
                Department         = row["Department"].ToString(),
                ProjectDirector    = row["Project Director"].ToString(),
                CoProjectDirectors = row["Co-Project Directors"].ToString(),
                FundingSource      = row["Funding Source"].ToString(),
                ProjectStatus      = row["Project Status"].ToString(),
            };

            // parse dates
            DateTime startDate;
            if (DateTime.TryParse(row["Project Start Date"].ToString(), out startDate))
            {
                project.ProjectStartDate = startDate;
            }

            DateTime endDate;
            if (DateTime.TryParse(row["Project End Date"].ToString(), out endDate))
            {
                project.ProjectEndDate = endDate;
            }

            // parse other values!
            project.Is204               = Is204Project(project.ProjectNumber);
            project.IsUcDavis           = IsUCDavisProject(project.OrganizationName);
            project.IsInterdepartmental = IsInterdepartmentalProject(project.ProjectNumber);

            return project;
        }

        private void ConfigureReportingOrg(AllProjectsNew project, ReportingOrganization organization)
        {
            if (organization != null)
            {
                project.OrgR = organization.OrganizationCode;
            }
            else if (!string.IsNullOrEmpty(project.ShortCode) && project.ShortCode.Equals("XXX", StringComparison.OrdinalIgnoreCase))
            {
                project.OrgR = "XXXX";
                project.IsInterdepartmental = true;
            }
            else if (!string.IsNullOrEmpty(project.ShortCode) && project.ShortCode.Equals("IPO", StringComparison.OrdinalIgnoreCase))
            {
                project.OrgR = "AIND";
                project.IsInterdepartmental = true;
            }
            else if (project.IsUcDavis)
            {
                project.OrgR = GetDepartmentCode(project.Department);
            }
        }

        private static readonly List<Tuple<Regex, string>> Searches = new List<Tuple<Regex, string>>
        {
            Tuple.Create(new Regex(@"Ag.*Resource.*Econ.*"            , RegexOptions.IgnoreCase), "AARE"),
            Tuple.Create(new Regex(@"Animal.*Sci.*"                   , RegexOptions.IgnoreCase), "AANS"),
            Tuple.Create(new Regex(@"Bio.*Ag.*En.*"                   , RegexOptions.IgnoreCase), "ABAE"),
            Tuple.Create(new Regex(@"Land.*Air.*Water.*Resour.*"      , RegexOptions.IgnoreCase), "ALAW"),
            Tuple.Create(new Regex(@"Entomology.*Nematology.*"        , RegexOptions.IgnoreCase), "AENM"),
            Tuple.Create(new Regex(@"Env.*Science.*Policy.*"          , RegexOptions.IgnoreCase), "ADES"),
            Tuple.Create(new Regex(@"Env.*Tox.*"                      , RegexOptions.IgnoreCase), "AETX"),
            Tuple.Create(new Regex(@"Evolution.*Ecology.*"            , RegexOptions.IgnoreCase), "BEVE"),
            Tuple.Create(new Regex(@"Food.*Science.*Tech.*"           , RegexOptions.IgnoreCase), "AFST"),
            Tuple.Create(new Regex(@"Human.*Ecology.*"                , RegexOptions.IgnoreCase), "AHCE"),
            Tuple.Create(new Regex(@"Independent.*"                   , RegexOptions.IgnoreCase), "AIND"),
            Tuple.Create(new Regex(@"Interdepartmental.*"             , RegexOptions.IgnoreCase), "XXXX"),
            Tuple.Create(new Regex(@"Micro.*Molecular.*Gen.*"         , RegexOptions.IgnoreCase), "BMIC"),
            Tuple.Create(new Regex(@"Molecular.*Cell.*Bio.*"          , RegexOptions.IgnoreCase), "BMCB"),
            Tuple.Create(new Regex(@"Neurobio.*Physiology.*Behavior.*", RegexOptions.IgnoreCase), "BNPB"),
            Tuple.Create(new Regex(@"Nutrition.*"                     , RegexOptions.IgnoreCase), "ANUT"),
            Tuple.Create(new Regex(@"Plant.*Bio.*"                    , RegexOptions.IgnoreCase), "BPLB"),
            Tuple.Create(new Regex(@"Plant.*Path.*"                   , RegexOptions.IgnoreCase), "APPA"),
            Tuple.Create(new Regex(@"Plant.*Sci.*"                    , RegexOptions.IgnoreCase), "APLS"),
            Tuple.Create(new Regex(@"Textiles.*Clothing.*"            , RegexOptions.IgnoreCase), "ATXC"),
            Tuple.Create(new Regex(@"Vit.*Enology.*"                  , RegexOptions.IgnoreCase), "AVIT"),
            Tuple.Create(new Regex(@"Wildlife.*Fish.*Cons.*Bio.*"     , RegexOptions.IgnoreCase), "AWFC")
        };

        public static string GetDepartmentCode(string department)
        {
            foreach (var search in Searches)
            {
                if (search.Item1.IsMatch(department))
                    return search.Item2;
            }

            return null;
        }

        public static bool IsUCDavisProject(string organization)
        {
            return organization.StartsWith("SAES - UNIVERSITY OF CALIFORNIA AT DAVIS",
                StringComparison.OrdinalIgnoreCase);
        }

        public static bool Is204Project(string projectNumber)
        {
            projectNumber = projectNumber.Trim();

            if (projectNumber.EndsWith("CG", StringComparison.OrdinalIgnoreCase))
                return true;

            if (projectNumber.EndsWith("OG", StringComparison.OrdinalIgnoreCase))
                return true;

            if (projectNumber.EndsWith("SG", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        public static bool IsInterdepartmentalProject(string projectNumber)
        {
            return !string.IsNullOrWhiteSpace(projectNumber) && projectNumber.Contains("XXX");
        }
    }
}