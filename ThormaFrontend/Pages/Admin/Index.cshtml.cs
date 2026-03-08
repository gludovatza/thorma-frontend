using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Admin;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly AdminApi _adminApi;
        private readonly AuthSession _authSession;

        public IndexModel(AdminApi adminApi, AuthSession authSession)
        {
            _adminApi = adminApi;
            _authSession = authSession;
        }

        public AdminStatsDto? Stats { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authSession.IsInRole("Admin"))
            {
                return RedirectToPage("/Errors/Forbidden");
            }

            try
            {
                Stats = await _adminApi.GetStatsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok betöltése közben: {ex.Message}";
            }

            return Page();
        }
    }
}
