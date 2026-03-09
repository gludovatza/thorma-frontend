using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Admin;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Admin
{
    public class LogsModel : PageModel
    {
        private readonly AdminApi _adminApi;
        private readonly AuthSession _authSession;

        public LogsModel(AdminApi adminApi, AuthSession authSession)
        {
            _adminApi = adminApi;
            _authSession = authSession;
        }

        public LogsPagedDto? LogsData { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? UserEmail { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? EntityType { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsAuthFailure { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Page { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 50;

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authSession.IsInRole("Admin"))
            {
                return RedirectToPage("/Errors/Forbidden");
            }

            try
            {
                LogsData = await _adminApi.GetLogsAsync(
                    userEmail: UserEmail,
                    entityType: EntityType,
                    isAuthFailure: IsAuthFailure,
                    page: Page,
                    pageSize: PageSize
                );
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok betöltése közben: {ex.Message}";
            }

            return Page();
        }
    }
}
