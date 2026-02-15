using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Festok
{
    public class DeleteModel : PageModel
    {
        private readonly FestokApi _festokApi;

        [BindProperty(SupportsGet = true)]
        public int Azon { get; set; }

        public FestoDto? Festo { get; private set; }

        public string? Error { get; private set; }

        public DeleteModel(FestokApi festokApi)
        {
            _festokApi = festokApi;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Festo = await _festokApi.GetByAzonAsync(Azon);
            if (Festo is null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _festokApi.DeleteAsync(Azon);
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Festo = await _festokApi.GetByAzonAsync(Azon);
                return Page();
            }
        }
    }
}
