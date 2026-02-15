using ThormaFrontend.Dtos;

namespace ThormaFrontend.Services
{
    public class FestokApi
    {
        private readonly IHttpClientFactory _f;
        public FestokApi(IHttpClientFactory f) => _f = f;

        public async Task<List<FestoDto>> GetAllAsync()
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<FestoDto>>("api/Festok") ?? new();

        public async Task<FestoDto?> GetByAzonAsync(int azon)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<FestoDto>($"api/Festok/{azon}");

        public async Task CreateAsync(FestoDto dto)
        {
            var r = await _f.CreateClient("ThormaApi").PostAsJsonAsync("api/Festok", dto);
            r.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int azon, FestoDto dto)
        {
            var r = await _f.CreateClient("ThormaApi")
                .PutAsJsonAsync($"api/Festok/{azon}", dto);
            r.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int azon)
        {
            var r = await _f.CreateClient("ThormaApi")
                .DeleteAsync($"api/Festok/{azon}");
            r.EnsureSuccessStatusCode();
        }
    }
}
