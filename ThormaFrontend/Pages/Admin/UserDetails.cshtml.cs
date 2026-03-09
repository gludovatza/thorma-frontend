using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Admin;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Admin
{
    public class UserDetailsModel : PageModel
    {
        private readonly AdminApi _adminApi;
        private readonly AuthSession _authSession;

        public UserDetailsModel(AdminApi adminApi, AuthSession authSession)
        {
            _adminApi = adminApi;
            _authSession = authSession;
        }

        public UserDetailsDto? UserDetails { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!_authSession.IsInRole("Admin"))
            {
                return RedirectToPage("/Errors/Forbidden");
            }

            if (string.IsNullOrEmpty(id))
            {
                return RedirectToPage("/Admin/Users");
            }

            try
            {
                UserDetails = await _adminApi.GetUserDetailsAsync(id);
                
                if (UserDetails == null)
                {
                    ErrorMessage = "A felhasználó nem található.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Hiba történt az adatok betöltése közben: {ex.Message}";
            }

            return Page();
        }
    }
}
