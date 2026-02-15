using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Festok
{
    public class EditModel : PageModel
    {
        private readonly FestokApi _festokApi;

        [BindProperty(SupportsGet = true)]
        public int Azon { get; set; }

        [BindProperty]
        public FestoDto Festo { get; set; } = new();

        public string? Error { get; private set; }

        public EditModel(FestokApi festokApi)
        {
            _festokApi = festokApi;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var festo = await _festokApi.GetByAzonAsync(Azon);
            if (festo is null) return NotFound();

            Festo = festo;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _festokApi.UpdateAsync(Azon, Festo);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return Page();
            }
        }
    }
}
