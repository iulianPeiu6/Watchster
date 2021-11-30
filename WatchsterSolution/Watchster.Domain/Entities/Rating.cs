using System;
using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class Rating : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid MovieId { get; set; }

        public double RatingValue { get; set; }
    }
}
