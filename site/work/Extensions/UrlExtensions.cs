using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QFlow.Controllers;

namespace QFlow.Extensions
{
    public static class UrlExtensions
    {
        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ContentUrl(this HttpRequest request)
        {
            var contentUrl = request.Path.Value;
            var status = request.Query["status"];
            if (!string.IsNullOrEmpty(status))
            {
                contentUrl += "?status=" + status;
            }

            return contentUrl;
        }
    }
}
