using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AD419.DataHelper.Web.Controllers.Fliters
{
    public class RoleNames
    {
        /// <summary>
        /// Allows access to all features, including User Admin.  Includes access to title and salary scale maintenance.
        /// Note: user must also have "ManageAll" role to actually be able to edit users within the user admin "plug-in".
        /// </summary>
        public static readonly string Admin = "Admin";

        /// <summary>
        /// Reserved for future use.
        /// </summary>
        public static readonly string ApplicationManager = "ApplicationManager";

        /// <summary>
        /// Allows access to all features except User Admin.  Includes access to title and salary scale maintenance.
        /// </summary>
        public static readonly string DOUser = "DOUser";

        /// <summary>
        /// Allows a user to emulate another user (for testing purposes, etc).
        /// </summary>
        public static readonly string EmulationUser = "EmulationUser";

        /// <summary>
        /// Allows a user to only view reports.
        /// </summary>
        public static readonly string ReportViewer = "ReportViewer";

        /// <summary>
        /// Allows user to "manage", i.e. edit Catbert users via the user admin plug-in.  Needed in addition to "Admin" role.
        /// </summary>
        public static readonly string ManageAll = "ManageAll";

        /// <summary>
        /// Allows user to "manage", i.e. edit Catbert users within a particular school.  Needed in addition to the SchoolAdmin role.
        /// </summary>
        public static readonly string ManageSchool = "ManageSchool";

        /// <summary>
        /// Allows access to "basic" features, i.e. Salary Scales, Employee Salary Comparison,
        /// and Employee Salary Review Analysis for all employees within their assigned college.
        /// </summary>
        public static readonly string Reviewer = "Reviewer";

        /// <summary>
        /// Allows access to all features, including User Admin, but for a particular college/school.
        /// Includes access to title and salary scale maintenance.
        /// Note: user must also have "ManageSchool" role to actually be able to edit users within the user admin "plug-in".
        /// </summary>
        public static readonly string SchoolAdmin = "SchoolAdmin";

        /// <summary>
        /// Departmental User: Same as rights as "Reviewer, except restricts users and departments displayed to only those they are authorized for,
        /// typically those within a user's department(s).  "Blanks" out employee specifics like name and department for those they are not
        /// (for purposes of salary comparison).
        /// </summary>
        public static readonly string User = "User";
    }
}