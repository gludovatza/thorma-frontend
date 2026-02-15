using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Kepek;

public class DeleteModel : PageModel
{
    private readonly KepekApi _api;

    [BindProperty(SupportsGet = true)]
    public string Leltar { get; set; } = "";

    public KepDto? Kep { get; private set; }
    public string? Error { get; private set; }

    public DeleteModel(KepekApi api) => _api = api;

    public async Task<IActionResult> OnGetAsync()
    {
        Kep = await _api.GetByLeltarAsync(Leltar);
        if (Kep is null) return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            await _api.DeleteAsync(Leltar);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            Kep = await _api.GetByLeltarAsync(Leltar);
            return Page();
        }
    }
}
