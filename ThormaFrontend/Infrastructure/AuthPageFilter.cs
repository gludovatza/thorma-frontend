using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ThormaFrontend.Services;

namespace ThormaFrontend.Infrastructure
{
    public class AuthPageFilter : IAsyncPageFilter
    {
        private readonly AuthSession _auth;

        public AuthPageFilter(AuthSession auth) => _auth = auth;

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
            => Task.CompletedTask;

        public Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            var path = context.HttpContext.Request.Path.Value ?? "/";

            // Bizonyos oldalakat megvédünk
            if (
                path.StartsWith("/Kepek", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/Festok", StringComparison.OrdinalIgnoreCase))
            {
                if (!_auth.IsSignedIn)
                {
                    context.Result = new RedirectToPageResult("/Account/Login",
                        new { returnUrl = path + context.HttpContext.Request.QueryString });
                    return Task.CompletedTask;
                }
            }

            return next();
        }
    }
}
