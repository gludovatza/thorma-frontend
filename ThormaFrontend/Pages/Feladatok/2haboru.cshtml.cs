using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Feladatok
{
    public class _2haboruModel : PageModel
    {
        private readonly FeladatokApi _api;
        public List<string> Nevek { get; private set; } = new();

        public _2haboruModel(FeladatokApi api) => _api = api;

        public async Task OnGetAsync()
            => Nevek = await _api.Get2HaboruAsync();
    }
}
