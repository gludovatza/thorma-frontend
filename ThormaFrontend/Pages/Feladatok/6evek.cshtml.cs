using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Feladatok;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Feladatok
{
    public class _6evekModel : PageModel
    {
        private readonly FeladatokApi _api;

        public List<EvDarabDto> Evek { get; private set; } = new();

        public _6evekModel(FeladatokApi api) => _api = api;

        public async Task OnGetAsync()
        {
            Evek = await _api.Get6EvekAsync();
        }
    }
}
