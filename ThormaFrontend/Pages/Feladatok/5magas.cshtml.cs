using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Feladatok;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Feladatok
{
    public class _5magasModel : PageModel
    {
        private readonly FeladatokApi _api;

        public LegmagasabbKepDto? Eredmeny { get; private set; }

        public _5magasModel(FeladatokApi api) => _api = api;

        public async Task OnGetAsync()
        {
            Eredmeny = await _api.Get5MagasAsync();
        }
    }
}
