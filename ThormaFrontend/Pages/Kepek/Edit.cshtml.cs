using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Kepek;

public class EditModel : PageModel
{
    private readonly KepekApi _kepekApi;
    private readonly FestokApi _festokApi;

    public List<SelectListItem> Festok { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string Leltar { get; set; } = "";

    [BindProperty]
    public KepDto Kep { get; set; } = new();

    public string? Error { get; private set; }

    public EditModel(KepekApi kepekApi, FestokApi festokApi)
    {
        _kepekApi = kepekApi;
        _festokApi = festokApi;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var kep = await _kepekApi.GetByLeltarAsync(Leltar);
        if (kep is null) return NotFound();

        await LoadFestokAsync();

        Kep = kep;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadFestokAsync();
            return Page();
        }

        try
        {
            await _kepekApi.UpdateAsync(Leltar, Kep);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            return Page();
        }
    }

    private async Task LoadFestokAsync()
    {
        var festok = await _festokApi.GetAllAsync();

        Festok = festok
            .OrderBy(f => f.Nev)
            .Select(f => new SelectListItem
            {
                Value = f.Azon.ToString(),
                Text = $"{f.Nev} ({f.Szuletett}-{f.Meghalt})"
            })
            .ToList();
    }
}
