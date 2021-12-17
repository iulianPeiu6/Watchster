using System;

namespace Watchster.Application.Models
{
    public class UserDetails
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool IsSubscribed { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
