using System;

namespace Watchster.MovieImporter.Settings
{
    public class QuartzSettings
    {
        public bool StartNow { get; set; }
        public DateTime? StartAt { get; set; }
        public bool RerunUnitOfMeasureIsInHours { get; set; }
        public int RerunInHours { get; set; }
        public int RerunInMinutest { get; set; }
    }
}
