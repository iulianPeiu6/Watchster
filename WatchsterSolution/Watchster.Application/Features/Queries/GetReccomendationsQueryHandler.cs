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
    public class GetReccomendationsQueryHandler : IRequestHandler<GetReccomendationsQuery, GetRecommendationsResponse>
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

        public async Task<GetRecommendationsResponse> Handle(GetReccomendationsQuery request, CancellationToken cancellationToken)
        { 
            if (await ratingRepository.GetByIdAsync(request.UserId) == null)
            {
                throw new ArgumentException("The specified user does not have any ratings in the database");
            }

            var movieIds = ratingRepository.Query()
                .Select(rating => rating.MovieId)
                .Distinct()
                .ToList();

            var movieRatings = new List<MovieRating>();

            foreach(var id in movieIds)
            {
                var movieRating = new MovieRating
                {
                    UserId = request.UserId,
                    MovieId = id
                };
                movieRatings.Add(movieRating);
            }

            var reccomendations = new List<ReccomendationDetails>();

            foreach(MovieRating movieRating in movieRatings)
            {
                MovieRatingPrediction prediction = movieRecommender.PredictMovieRating(movieRating);
                if(!float.IsNaN(prediction.Score))
                {
                    var movie = await movieRepository.GetByIdAsync(movieRating.MovieId);
                    var reccomendationDetails = new ReccomendationDetails
                    {
                        Id = movie.Id,
                        TMDbId = movie.TMDbId,
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        Genres = movie.Genres,
                        PosterUrl = movie.PosterUrl,
                        Popularity = movie.Popularity,
                        TMDbVoteAverage = movie.TMDbVoteAverage,
                        Overview = movie.Overview,
                        Score = prediction.Score
                    };
                    reccomendations.Add(reccomendationDetails);
                }
            }

            reccomendations.OrderBy(reccomendation => reccomendation.Score);

            return new GetRecommendationsResponse
            {
                Recommendations = reccomendations.Take(100).ToList()
            };
        }
    }
}
