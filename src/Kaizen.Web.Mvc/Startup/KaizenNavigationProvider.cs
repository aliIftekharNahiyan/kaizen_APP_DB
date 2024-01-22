using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using Kaizen.Authorization;

namespace Kaizen.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class KaizenNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu

                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Customers,
                        L("Customers"),
                        url: "Customers",
                        icon: "fas fa-user-friends",
                        requiresAuthentication: true,
                        permissionDependency: new SimplePermissionDependency("Administration.UserManagement")
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("NonMedicalHelpers"),
                        url: "Users",
                        icon: "fas fa-hand-holding-heart",
                        permissionDependency: new SimplePermissionDependency("Administration.UserManagement")
                    )
                )
                 .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Marketing",
                        L("Marketing"),
                        icon: "fas fa-bullhorn",
                        permissionDependency: new SimplePermissionDependency("Administration")
                    ).AddItem(
                        new MenuItemDefinition(
                            "NRCPD",
                             L("NRCPD"),
                             url: "Marketing/NRCPD"
                        )
                    )
                )
                 .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Finance",
                        L("Finance"),
                        icon: "fas fa-file-invoice-dollar",
                        permissionDependency: new SimplePermissionDependency("Administration")
                    ).AddItem(
                        new MenuItemDefinition(
                            "Timesheet",
                             L("Timesheet"),
                             url: "Finance/Timesheet"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Invoice",
                             L("Invoice"),
                             url: "Finance/Invoice"
                        )
                    )
                )
                .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Checklists",
                        L("Checklists"),
                        icon: "fas fa-file-invoice-dollar",
                        permissionDependency: new SimplePermissionDependency("Administration")
                    ).AddItem(
                        new MenuItemDefinition(
                            "Checklists",
                             L("Checklists"),
                             url: "Checklists"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Checklist Items",
                             L("Checklist Items"),
                             url: "ChecklistItems"
                        )
                    )
                 )
                .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "Communications",
                        L("Communications"),
                        icon: "fas fa-file-invoice-dollar",
                        permissionDependency: new SimplePermissionDependency("Administration")
                    ).AddItem(
                        new MenuItemDefinition(
                            "Groups",
                             L("Groups"),
                             url: "Communications/Groups"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Templates",
                             L("Templates"),
                             url: "CommunicationTemplates"
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            "Send Communication",
                             L("Send Communication"),
                             url: "Communications/Create"
                        )
                    )
                 )
                .AddItem( // Menu items below is just for demonstration!
                new MenuItemDefinition(
                    "Settings",
                    L("Settings"),
                    icon: "fas fa-cogs",
                    permissionDependency: new SimplePermissionDependency("Administration")
                ).AddItem(
                    new MenuItemDefinition(
                        "Admins",
                        L("Admins"),
                        url: "/Settings/Admins"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "SupportTypes",
                        L("Support Types"),
                        url: "/SupportTypes"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        "Lookups",
                        L("Lookups"),
                        url: "/Lookups"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Universities",
                        L("Universities"),
                        url: "/Universitys"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Disabilities",
                        L("Disabilities"),
                        url: "/Disabilitys"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Contact Methods",
                        L("Contact Methods"),
                        url: "/ContactMethods"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Regions",
                        L("Regions"),
                        url: "/Regions"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Region Locations",
                        L("Region Locations"),
                        url: "/RegionLocations"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Customer Types",
                        L("Customer Types"),
                        url: "/CustomerTypes"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        "Payment Terms",
                        L("Payment Terms"),
                        url: "/PaymentTerms"
                    )
                )
                 .AddItem(
                    new MenuItemDefinition(
                        "Funding Bodies",
                        L("Funding Bodies"),
                        url: "/FundingBodys"
                    )
                )
                 .AddItem(
                    new MenuItemDefinition(
                        "Companies",
                        L("Companies"),
                        url: "/Companies"
                    )
                )
                 .AddItem(
                    new MenuItemDefinition(
                        "Session Types",
                        L("Session Types"),
                        url: "/SessionTypes"
                    )
                )
                   .AddItem(
                    new MenuItemDefinition(
                        "Academic Years",
                        L("Academic Years"),
                        url: "/AcademicYears"
                    )
                )
            )

            .AddItem( // Menu items below is just for demonstration!
                new MenuItemDefinition(
                    "Bulk Process",
                    L("Bulk Process"),
                    icon: "fas fa-cogs"
                ).AddItem(
                    new MenuItemDefinition(
                        "Session Groups",
                        L("SessionGroups"),
                        url: "/SessionGroups/Indexbp"
                    )
                )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, KaizenConsts.LocalizationSourceName);
        }
    }
}