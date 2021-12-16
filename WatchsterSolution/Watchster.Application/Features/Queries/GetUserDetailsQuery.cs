using MediatR;
using System;
using System.Collections.Generic;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsResponse>
    {
        public Guid UserId { get; set; }
    }
}
