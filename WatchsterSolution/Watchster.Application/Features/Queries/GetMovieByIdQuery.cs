using MediatR;
using System;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {
       public Guid guid { get; set; }
    }
 
}
