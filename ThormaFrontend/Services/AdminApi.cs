using ThormaFrontend.Dtos.Admin;

namespace ThormaFrontend.Services
{
    public class AdminApi
    {
        private readonly IHttpClientFactory _factory;

        public AdminApi(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<UserActivityDto>> GetUsersAsync(CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var response = await client.GetAsync("api/admin/users", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<UserActivityDto>>(cancellationToken: ct)
                ?? new List<UserActivityDto>();
        }

        public async Task<UserDetailsDto?> GetUserDetailsAsync(string userId, CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var response = await client.GetAsync($"api/admin/users/{userId}", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDetailsDto>(cancellationToken: ct);
        }

        public async Task<LogsPagedDto?> GetLogsAsync(
            string? userEmail = null,
            string? entityType = null,
            bool? isAuthFailure = null,
            int page = 1,
            int pageSize = 50,
            CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var query = $"api/admin/logs?page={page}&pageSize={pageSize}";

            if (!string.IsNullOrEmpty(userEmail))
                query += $"&userEmail={Uri.EscapeDataString(userEmail)}";

            if (!string.IsNullOrEmpty(entityType))
                query += $"&entityType={Uri.EscapeDataString(entityType)}";

            if (isAuthFailure.HasValue)
                query += $"&isAuthFailure={isAuthFailure.Value}";

            var response = await client.GetAsync(query, ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LogsPagedDto>(cancellationToken: ct);
        }

        public async Task<AdminStatsDto?> GetStatsAsync(CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var response = await client.GetAsync("api/admin/stats", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AdminStatsDto>(cancellationToken: ct);
        }

        public async Task<List<LogDto>> GetFailedLoginsAsync(int days = 7, CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var response = await client.GetAsync($"api/admin/logs/failed-logins?days={days}", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<LogDto>>(cancellationToken: ct)
                ?? new List<LogDto>();
        }

        public async Task<List<IpStatsDto>> GetIpStatsAsync(CancellationToken ct = default)
        {
            var client = _factory.CreateClient("ThormaApi");
            var response = await client.GetAsync("api/admin/logs/by-ip", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<IpStatsDto>>(cancellationToken: ct)
                ?? new List<IpStatsDto>();
        }
    }
}
