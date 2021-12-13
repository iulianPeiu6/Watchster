using Microsoft.ML.Data;
using System;

namespace Watchster.Application.Utils.ML.Models
{
    public class MovieRating
    {
        [LoadColumn(0)]
        public Guid UserId { get; set; }

        [LoadColumn(1)]
        public Guid MovieId { get; set; }

        [LoadColumn(2)]
        public double Label { get; set; }
    }
}
