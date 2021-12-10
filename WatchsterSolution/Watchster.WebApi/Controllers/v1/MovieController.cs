using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> logger;
        public MovieController(IMediator mediator, ILogger<MovieController> logger) : base(mediator)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetRecommendations")]
        public IActionResult GetRecommendations(Guid userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("AddRating")]
        public IActionResult AddRating(Guid movieId, Guid userId, decimal rating)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetFromPage")]
        public async Task<IActionResult> GetFromPage([FromQuery] GetMoviesFromPageCommand command)
        {
            try
            {
                var response = await mediator.Send(command);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                logger.LogError("Unexpected Error: ", ex.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogError("Unexpected Error while processing the request: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
