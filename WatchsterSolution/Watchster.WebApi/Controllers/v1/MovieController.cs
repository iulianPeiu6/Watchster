using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class MovieController : BaseController
    {
        private readonly ILogger<MovieController> logger;
        private readonly IMovieRecommender movieRecommender;

        public MovieController(
            IMediator mediator, 
            ILogger<MovieController> logger, 
            IMovieRecommender movieRecommender): base(mediator)
        {
            this.logger = logger;
            this.movieRecommender = movieRecommender;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var query = new GetAllMoviesQuery();
                var response = await mediator.Send(query);
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

        [HttpGet]
        [Route("GetRecommendations")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
            try
            {
                GetReccomendationsQuery query = new GetReccomendationsQuery
                {
                    UserId = userId
                };
                var response = await mediator.Send(query);
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

        //Used for testing
        [HttpGet]
        [Route("GetPrediction")]
        public IActionResult GetRecommendations(Guid userId, Guid movieId)
        {
            try
            {
                var movie = new MovieRating
                {
                    UserId = userId.ToString(),
                    MovieId = movieId.ToString()
                };

                return Ok(movieRecommender.PredictMovieRating(movie));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddRating")]
        public IActionResult AddRating(Guid movieId, Guid userId, decimal rating)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetFromPage")]
        public async Task<IActionResult> GetFromPage([FromQuery] GetMoviesFromPageQuery command)
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
