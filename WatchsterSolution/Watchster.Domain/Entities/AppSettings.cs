using Watchster.Domain.Common;

namespace Watchster.Domain.Entities
{
    public class AppSettings : BaseEntity
    {
        public string Section { get; set; }

        public string Parameter { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}
