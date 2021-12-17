using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchster.Application.Models
{
    public class GetUserDetailsResponse
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool IsSubscribed { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int NumberOfTotalGivenRatings { get; set; }
    }
}
