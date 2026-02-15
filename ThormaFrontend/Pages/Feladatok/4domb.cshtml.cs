using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos.Feladatok;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Feladatok
{
    public class _4dombModel : PageModel
    {
        private readonly FeladatokApi _api;

        public List<DombKepDto> Kepek { get; private set; } = new();

        public _4dombModel(FeladatokApi api) => _api = api;

        public async Task OnGetAsync()
        {
            Kepek = await _api.Get4DombAsync();
        }
    }
}
