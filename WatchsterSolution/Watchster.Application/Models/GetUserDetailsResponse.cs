using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchster.Application.Models
{
    class GetUserDetailsResponse : UserDetails
    {
        public int NumberOfTotalRatingsGiven { get; set; }
    }
}
