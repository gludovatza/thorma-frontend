namespace ThormaFrontend.Dtos.Admin
{
    public class UserActivityDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public DateTime? LastActivity { get; set; }
        public int TotalActions { get; set; }
        public int FailedLoginAttempts { get; set; }
    }
}
