using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Kepek
{
    public class DetailsModel : PageModel
    {
        private readonly KepekApi _api;
        private readonly AuthSession _auth;

        public DetailsModel(KepekApi api, AuthSession auth)
        {
            _api = api;
            _auth = auth;
        }

        public KepDto? Kep { get; private set; }

        public async Task<IActionResult> OnGetAsync(string leltar)
        {
            if (string.IsNullOrEmpty(leltar))
                return NotFound();

            Kep = await _api.GetByLeltarAsync(leltar);

            if (Kep is null)
                return NotFound();

            return Page();
        }
    }
}
