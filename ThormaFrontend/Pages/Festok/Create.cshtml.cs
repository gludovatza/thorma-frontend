using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThormaFrontend.Dtos;
using ThormaFrontend.Services;

namespace ThormaFrontend.Pages.Festok
{
    public class CreateModel : PageModel
    {
        private readonly FestokApi _festokApi;

        [BindProperty]
        public FestoDto Festo { get; set; } = new();

        public string? Error { get; private set; }

        public CreateModel(FestokApi festokApi)
        {
            _festokApi = festokApi;
        }

        public void OnGetAsync()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _festokApi.CreateAsync(Festo);
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
