using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Users;

namespace QFlow.Extensions
{
    public static class UserExtentions
    {
        public static async Task<string> GetFullUserNameAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
        {
            var appUser = await userManager.GetUserAsync(user);

            return appUser != null
                ? $"{appUser.FirstName} {appUser.LastName}"
                : "";
        }

        public static bool IsManager(this IPrincipal user)
        {
            return user != null && user.IsInRole(Roles.Manager.ToString());
        }

        public static bool IsSuperUser(this IPrincipal user, string superUserEmails)
        {
            if (string.IsNullOrEmpty(superUserEmails))
            {
                return false;
            }
            var emails = superUserEmails.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);
            return emails.Any(_ => user != null && _.Equals(user.Identity.Name));
        }
    }
}
