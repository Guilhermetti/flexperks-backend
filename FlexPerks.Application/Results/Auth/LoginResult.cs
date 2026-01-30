namespace FlexPerks.Application.Results.Auth
{
    public class LoginResult
    {
        public required string AccessToken { get; init; }
        public string TokenType { get; init; } = "Bearer";
        public required DateTime ExpiresAt { get; init; }
        public int UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
    }
}
