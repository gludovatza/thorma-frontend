using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Feladatok
{
    public class _3anyagModel : PageModel
    {
        private readonly FeladatokApi _api;

        public List<string> Anyagok { get; private set; } = new();

        public _3anyagModel(FeladatokApi api) => _api = api;

        public async Task OnGetAsync()
        {
            Anyagok = await _api.Get3AnyagAsync();
        }
    }
}
