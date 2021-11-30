using System;
using System.Collections.Generic;
using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<Rating> UserRatings { get; set; }
    }
}
