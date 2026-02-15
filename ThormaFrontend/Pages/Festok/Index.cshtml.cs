using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Festok
{
    public class IndexModel : PageModel
    {
        private readonly FestokApi _api;

        public List<FestoDto> Festok { get; private set; } = new();

        public IndexModel(FestokApi api) => _api = api;

        public async Task OnGetAsync()
        {
            Festok = await _api.GetAllAsync();
        }
    }
}
