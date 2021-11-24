using Microsoft.ML.Data;

namespace Watchster.MLUtil.Models
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
