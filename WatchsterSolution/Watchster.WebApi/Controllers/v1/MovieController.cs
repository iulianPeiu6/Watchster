using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MovieController : BaseController
    {
        public MovieController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetRecommendations(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult AddRating(Guid movieId, Guid userId, decimal rating)
        {
            throw new NotImplementedException();
        }
    }
}
