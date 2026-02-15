using ThormaFrontend.Dtos.Feladatok;

namespace ThormaFrontend.Services
{
    public class FeladatokApi
    {
        private readonly IHttpClientFactory _f;
        public FeladatokApi(IHttpClientFactory f) => _f = f;

        // 2haboru – 1914-ben élő festők nevei
        public async Task<List<string>> Get2HaboruAsync()
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<string>>("api/Feladatok/2haboru") ?? new();

        // 3anyag – olaj technikával készült képek anyagai (distinct)
        public async Task<List<string>> Get3AnyagAsync(CancellationToken ct = default)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<string>>("api/Feladatok/3anyag", ct)
               ?? new();

        // 4domb – cím, méretek, festő neve
        public async Task<List<DombKepDto>> Get4DombAsync(CancellationToken ct = default)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<DombKepDto>>("api/Feladatok/4domb", ct)
               ?? new();

        // 5magas – legmagasabb kép (lehet null, ha nincs adat)
        public async Task<LegmagasabbKepDto?> Get5MagasAsync(CancellationToken ct = default)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<LegmagasabbKepDto>("api/Feladatok/5magas", ct);

        // 6evek – év + darabszám
        public async Task<List<EvDarabDto>> Get6EvekAsync(CancellationToken ct = default)
            => await _f.CreateClient("ThormaApi")
                .GetFromJsonAsync<List<EvDarabDto>>("api/Feladatok/6evek", ct)
               ?? new();
    }
}
