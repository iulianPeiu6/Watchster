using System;
using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public int UserId { get; set; }

        public int MovieId { get; set; }

        public double RatingValue { get; set; }
    }
}
