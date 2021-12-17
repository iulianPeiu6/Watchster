using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.Utils.ML.Models;
using Watchster.Domain.Entities;

namespace Watchster.Application.Features.Queries
{
    public class GetReccomendationsQueryHandler : IRequestHandler<GetReccomendationsQuery, GetReccomendationsResponse>
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IMovieRecommender movieRecommender;

        public GetReccomendationsQueryHandler(IRatingRepository ratingRepository, IMovieRepository movieRepository, IMovieRecommender movieReccomender)
        {
            this.ratingRepository = ratingRepository;
            this.movieRepository = movieRepository;
            this.movieRecommender = movieReccomender;
        }

        public async Task<GetReccomendationsResponse> Handle(GetReccomendationsQuery request, CancellationToken cancellationToken)
        { 
            if (await ratingRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new ArgumentException("The specified user does not have any ratings in the database");
            }
            List<Guid> movieIds = ratingRepository.Query().Select(rating => rating.MovieId).Distinct().ToList();
            List<MovieRating> movieRatings = new List<MovieRating>();
            foreach(Guid id in movieIds)
            {
                MovieRating movieRating = new MovieRating
                {
                    UserId = request.UserId.ToString(),
                    MovieId = id.ToString()
                };
                movieRatings.Add(movieRating);
            }
            List<ReccomendationDetails> reccomendations = new List<ReccomendationDetails>();
            foreach(MovieRating movieRating in movieRatings)
            {
                MovieRatingPrediction prediction = movieRecommender.PredictMovieRating(movieRating);
                if(!float.IsNaN(prediction.Score))
                {
                    Movie movie = await movieRepository.GetByIdAsync(Guid.Parse(movieRating.MovieId));
                    ReccomendationDetails reccomendationDetails = new ReccomendationDetails
                    {
                        Id = movie.Id,
                        TMDbId = movie.TMDbId,
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        Genres = movie.Genres,
                        Overview = movie.Overview,
                        Score = prediction.Score
                    };
                    reccomendations.Add(reccomendationDetails);
                }
            }

            reccomendations.OrderBy(reccomendation => reccomendation.Score);

            if(reccomendations.Count > 100)
            {
                return new GetReccomendationsResponse
                {
                    reccomendations = reccomendations.Take(100).ToList()
                };
            }
            else
            {
                return new GetReccomendationsResponse
                {
                    reccomendations = reccomendations
                };
            }
        }
    }
}
