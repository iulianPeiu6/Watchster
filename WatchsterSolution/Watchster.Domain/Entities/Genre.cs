using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class Genre : BaseEntity
    {
        public int TMDbId { get; set; }

        public string Name { get; set; }
    }
}
