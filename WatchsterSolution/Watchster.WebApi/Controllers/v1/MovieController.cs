using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Models;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> logger;

        public MovieController(
            IMediator mediator,
            ILogger<MovieController> logger) : base(mediator)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            logger.LogInformation("Handeling request on Movie/GetAll");
            var query = new GetAllMoviesQuery();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetMostPopular")]
        public async Task<IActionResult> GetMostPopular()
        {
            logger.LogInformation("Handeling request on Movie/GetMostPopular");
            var query = new GetMostPopularMoviesQuery();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetLatestReleased")]
        public async Task<IActionResult> GetLatestReleased()
        {
            logger.LogInformation("Handeling request on Movie/GetLatestReleased");
            var query = new GetLatestReleasedMoviesQuery();
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetRecommendations")]
        public async Task<IActionResult> GetRecommendationsAsync(int userId)
        {
            logger.LogInformation("Handeling request on Movie/GetRecommendations");
            GetRecommendationsQuery query = new GetRecommendationsQuery
            {
                UserId = userId
            };
            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetFromPage")]
        public async Task<IActionResult> GetFromPage([FromQuery] GetMoviesFromPageQuery command)
        {
            logger.LogInformation("Handeling request on Movie/GetFromPage/{Page}");
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetMovie")]
        public async Task<IActionResult> GetMovieAsync([FromQuery] int id)
        {
            logger.LogInformation("Handeling request on Movie/GetMovie/{id}");
            GetMovieByIdQuery query = new GetMovieByIdQuery
            {
                Id = id,
            };
            var response = await mediator.Send(query);
            if (response.ErrorMessage == Error.MovieNotFound)
            {
                return NotFound(new { Message = Error.MovieNotFound });
            }
            return Ok(response);
        }
        [HttpPost]
        [Route("AddRating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingCommand command)
        {
            logger.LogInformation("Handeling request on Movie/AddRating");
            var response = await mediator.Send(command);

            if (response.ErrorMessage == Error.MovieNotFound)
            {
                return NotFound(new { Message = Error.MovieNotFound });
            }

            if (response.ErrorMessage == Error.UserNotFound)
            {
                return NotFound(new { Message = Error.UserNotFound });
            }

            if (response.ErrorMessage == Error.MovieAlreadyRated)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { Message = Error.MovieAlreadyRated });
            }

            if (response.ErrorMessage == Error.RatingNotInRange)
            {
                return BadRequest(new { Message = Error.RatingNotInRange });
            }

            return StatusCode(StatusCodes.Status201Created, new { Message = "Rating added!" });
        }
    }
}
