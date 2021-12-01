using MediatR;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetAppSettingsBySectionAndParameterQuery : IRequest<AppSettings>
    {
        public string Section { get; set; }

        public string Parameter { get; set; }
    }
}
