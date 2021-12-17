using MediatR;
using System;
using System.Collections.Generic;
using Watchster.Application.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetUserDetailsQuery : IRequest<GetUserDetailsResponse>
    {
        public int UserId { get; set; }
    }
}
