using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Watchster.Application.Features.Commands;
using Watchster.Application.Features.Queries;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
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
        public IActionResult GetRecommendations(Guid userId)
        {
            throw new NotImplementedException();
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

        [HttpGet]
        [Route("GetMovie")]
        public async Task<IActionResult> GetMovie([FromQuery] Guid id)
        {
            try
            {
                GetMovieByIdQuery query = new GetMovieByIdQuery
                {
                    guid = id,
                };
                var response = await mediator.Send(query);
                if(response.ErrorMessage == Error.MovieNotFound)
                {
                    return NotFound(new { Message = Error.MovieNotFound });
                }
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
        [HttpPost]
        [Route("AddRating")]
        public async Task<IActionResult> AddRating([FromBody] AddRatingCommand command)
        {
            try
            {
                var response = await mediator.Send(command);

                if (response.ErrorMessage == Error.MovieNotFound)
                {
                    return NotFound(new { Message = Error.MovieNotFound });
                }

                if(response.ErrorMessage == Error.UserNotFound)
                {
                    return NotFound(new { Message = Error.UserNotFound });
                }

                if (response.ErrorMessage == Error.MovieAlreadyRated)
                {
                  return StatusCode(StatusCodes.Status406NotAcceptable, new { Message = Error.MovieAlreadyRated });
                }

                if(response.ErrorMessage == Error.RatingNotInRange)
                {
                    return BadRequest(new { Message = Error.RatingNotInRange });
                }

                return StatusCode(StatusCodes.Status201Created, new { Message = "Rating added!" });
            }
            catch (Exception ex)
            {
                logger.LogError("Unexpected Error while processing the request: ", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
