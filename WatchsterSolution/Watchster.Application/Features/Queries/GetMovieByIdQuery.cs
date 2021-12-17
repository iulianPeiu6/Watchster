using MediatR;
using System;
using Watchster.Application.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetMovieByIdQuery : IRequest<MovieResult>
    {
       public int Id { get; set; }
    }
 
}
