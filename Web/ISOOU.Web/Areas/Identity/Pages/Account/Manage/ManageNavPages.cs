namespace ISOOU.Web.Areas.Identity.Pages.Account.Manage
{
    using System;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string ChildInput => "ChildInput";

        public static string ParentInput => "ParentInput";

        public static string ChangePassword => "ChangePassword";

        public static string PersonalData => "PersonalData";

        public static string Criterias => "Criterias";

        public static string Applications => "Applications";

        public static string AddApplications => "AddApplications";

        public static string DistrictSelected => "DistrictSelected";

        public static string SchoolsSelected => "SchoolsSelected";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChildInputNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChildInput);

        public static string ParentInputNavClass(ViewContext viewContext) => PageNavClass(viewContext, ParentInput);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string CriteriasNavClass(ViewContext viewContext) => PageNavClass(viewContext, Criterias);

        public static string ApplicationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Applications);

        public static string AddApplicationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddApplications);

        public static string DistrictSelectedNavClass(ViewContext viewContext) => PageNavClass(viewContext, DistrictSelected);

        public static string SchoolsSelectedNavClass(ViewContext viewContext) => PageNavClass(viewContext, SchoolsSelected);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
