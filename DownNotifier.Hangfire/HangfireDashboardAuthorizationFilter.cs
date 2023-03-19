using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace DownNotifier.BackgroundJob
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {

            var httpContext = context.GetHttpContext();
            var data = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;


            return data != null;
        }
    }
}
