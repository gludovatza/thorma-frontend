using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Kepek;

public class IndexModel : PageModel
{
    private readonly KepekApi _api;

    public List<KepDto> Kepek { get; private set; } = new();

    public IndexModel(KepekApi api) => _api = api;

    public async Task OnGetAsync()
    {
        Kepek = await _api.GetAllAsync();
    }
}
