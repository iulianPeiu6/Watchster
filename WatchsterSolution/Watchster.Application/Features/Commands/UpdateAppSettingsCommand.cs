using MediatR;
using System;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Commands
{
    public class UpdateAppSettingsCommand : IRequest<AppSettings>
    {
        public int Id { get; set; }
        public string Section { get; set; }

        public string Parameter { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}
