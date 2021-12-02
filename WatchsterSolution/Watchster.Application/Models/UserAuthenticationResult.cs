using Watchster.Domain.Entities;

namespace Watchster.Application.Authentication.Models
{
    public class UserAuthenticationResult
    {
        public User User { get; set; }

        public string JwtToken { get; set; }

        public string ErrorMessage { get; set; }
    }
}
