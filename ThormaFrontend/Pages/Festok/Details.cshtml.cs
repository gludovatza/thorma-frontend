using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Festok
{
    public class DetailsModel : PageModel
    {
        private readonly FestokApi _api;
        private readonly AuthSession _auth;

        public DetailsModel(FestokApi api, AuthSession auth)
        {
            _api = api;
            _auth = auth;
        }

        public FestoDto? Festo { get; private set; }

        public async Task<IActionResult> OnGetAsync(int azon)
        {
            if (azon <= 0)
                return NotFound();

            Festo = await _api.GetByAzonAsync(azon);

            if (Festo is null)
                return NotFound();

            return Page();
        }
    }
}
