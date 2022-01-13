using System;

namespace Watchster.MovieRecommenderNotifier.Settings
{
    public class QuartzSettings
    {
        public bool StartNow { get; set; }
        public DateTime? StartAt { get; set; }
        public bool RerunUnitOfMeasureIsInDays { get; set; }
        public int RerunInHours { get; set; }
        public int RerunInDays { get; set; }
    }
}
