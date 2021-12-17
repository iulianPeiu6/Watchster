using Microsoft.ML.Data;
using System;

namespace Watchster.Application.Utils.ML.Models
{
    public class MovieRating
    {
        [LoadColumn(0)]
        public int UserId { get; set; }

        [LoadColumn(1)]
        public int MovieId { get; set; }

        [LoadColumn(2)]
        public float Label { get; set; }
    }
}
