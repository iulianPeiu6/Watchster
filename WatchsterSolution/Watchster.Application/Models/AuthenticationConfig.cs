namespace Watchster.Jwt
{
    public class AuthenticationConfig
    {
        public string Issuer { get; set; }

        public string Key { get; set; }

        public int MinutesExpiration { get; set; }
    }
}
