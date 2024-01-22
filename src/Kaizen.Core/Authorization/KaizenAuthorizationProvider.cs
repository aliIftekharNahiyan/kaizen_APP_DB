using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Kaizen.Authorization
{
    public class KaizenAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var administration = context.CreatePermission("Administration", L("Administration"));

            var userManagement = administration.CreateChildPermission("Administration.UserManagement", L("User Management"));
            administration.CreateChildPermission("Administration.UserManagement.ViewUser", L("View User"));
            administration.CreateChildPermission("Administration.UserManagement.CreateUser", L("Create User"));

            var user = context.CreatePermission("User", L("User Options"));
            var sessionManagement = user.CreateChildPermission("User.SessionManagement", L("Session Management"));
            sessionManagement.CreateChildPermission("User.SessionManagement.ViewHostedSessions", L("View Hosted Sessions"));
            sessionManagement.CreateChildPermission("User.SessionManagement.CreateSessionOverview", L("Create Session Overviews"));
            sessionManagement.CreateChildPermission("User.SessionManagement.ViewCalendar", L("View Session Calendar"));

            var notes = context.CreatePermission("Notes", L("Notes"));
            notes.CreateChildPermission("Notes.CreateInternal", L("Create Internal Notes"));
            notes.CreateChildPermission("Notes.ViewInternal", L("View Internal Notes"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, KaizenConsts.LocalizationSourceName);
        }
    }
}
