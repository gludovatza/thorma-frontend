using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Admin;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly AdminApi _adminApi;
        private readonly AuthSession _authSession;

        public UsersModel(AdminApi adminApi, AuthSession authSession)
        {
            _adminApi = adminApi;
            _authSession = authSession;
        }

        public List<UserActivityDto> Users { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authSession.IsInRole("Admin"))
            {
                return RedirectToPage("/Errors/Forbidden");
            }

            try
            {
                Users = await _adminApi.GetUsersAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok betöltése közben: {ex.Message}";
            }

            return Page();
        }
    }
}
