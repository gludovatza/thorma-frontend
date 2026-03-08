using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Admin;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Admin
{
    public class IpStatsModel : PageModel
    {
        private readonly AdminApi _adminApi;
        private readonly AuthSession _authSession;

        public IpStatsModel(AdminApi adminApi, AuthSession authSession)
        {
            _adminApi = adminApi;
            _authSession = authSession;
        }

        public List<IpStatsDto> IpStats { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authSession.IsInRole("Admin"))
            {
                return RedirectToPage("/Errors/Forbidden");
            }

            try
            {
                IpStats = await _adminApi.GetIpStatsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok betöltése közben: {ex.Message}";
            }

            return Page();
        }
    }
}
