namespace FlexPerks.Application.Options
{
    public class JwtOptions
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 60;
    }
}
