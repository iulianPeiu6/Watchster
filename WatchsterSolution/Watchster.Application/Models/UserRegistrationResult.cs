using Watchster.Domain.Entities;

namespace Watchster.Application.Models
{
    public class UserRegistrationResult
    {
        public User User { get; set; }

        public string ErrorMessage { get; set; }
    }
}
