using ThormaFrontend.Dtos;

namespace ThormaFrontend.Services
{
    public class KepekApi
    {
        private readonly IHttpClientFactory _f;
        public KepekApi(IHttpClientFactory f) => _f = f;

        public async Task<List<KepDto>> GetAllAsync()
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<KepDto>>("api/Kepek") ?? new();

        public async Task<KepDto?> GetByLeltarAsync(string leltar)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<KepDto>($"api/Kepek/{Uri.EscapeDataString(leltar)}");

        public async Task CreateAsync(KepDto dto)
        {
            var r = await _f.CreateClient("ThormaApi").PostAsJsonAsync("api/Kepek", dto);
            r.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(string leltar, KepDto dto)
        {
            var r = await _f.CreateClient("ThormaApi")
                .PutAsJsonAsync($"api/Kepek/{Uri.EscapeDataString(leltar)}", dto);
            r.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string leltar)
        {
            var r = await _f.CreateClient("ThormaApi")
                .DeleteAsync($"api/Kepek/{Uri.EscapeDataString(leltar)}");
            r.EnsureSuccessStatusCode();
        }
    }
}
