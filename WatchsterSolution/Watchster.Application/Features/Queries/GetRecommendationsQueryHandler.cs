using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Watchster.Application.Interfaces;
using Watchster.Application.Models;
using Watchster.Application.Utils.ML.Models;

namespace Watchster.Application.Features.Queries
{
    public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, GetRecommendationsResponse>
    {
        private readonly IRatingRepository ratingRepository;
        private readonly IMovieRepository movieRepository;
        private readonly IMovieRecommender movieRecommender;

        public GetRecommendationsQueryHandler(IRatingRepository ratingRepository, IMovieRepository movieRepository, IMovieRecommender movieRecommender)
        {
            this.ratingRepository = ratingRepository;
            this.movieRepository = movieRepository;
            this.movieRecommender = movieRecommender;
        }

        public Task<GetRecommendationsResponse> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            var currentUserHasRatings = !ratingRepository
                    .Query(rating => rating.UserId == request.UserId)
                    .Any();

            if (currentUserHasRatings)
            {
                return Task.FromResult(new GetRecommendationsResponse
                {
                    Recommendations = new List<MovieRecommendation>()
                });
            }

            return GetRecommendations(request);
        }

        private async Task<GetRecommendationsResponse> GetRecommendations(GetRecommendationsQuery request)
        {
            var moviesIdsRatedByCurrentUser = ratingRepository.Query(rating => rating.UserId == request.UserId)
                .Select(rating => rating.MovieId)
                .ToList();

            var movieIds = ratingRepository.Query()
                .Select(rating => rating.MovieId)
                .Distinct()
                .Except(moviesIdsRatedByCurrentUser);

            var movieRatings = new List<MovieRating>();

            foreach (var id in movieIds)
            {
                var movieRating = new MovieRating
                {
                    UserId = request.UserId,
                    MovieId = id
                };
                movieRatings.Add(movieRating);
            }

            var recommendations = new List<MovieRecommendation>();

            foreach (MovieRating movieRating in movieRatings)
            {
                MovieRatingPrediction prediction = movieRecommender.PredictMovieRating(movieRating);
                if (!float.IsNaN(prediction.Score))
                {
                    var movie = await movieRepository.GetByIdAsync(movieRating.MovieId);
                    var recommendationDetails = new MovieRecommendation
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
                    recommendations.Add(recommendationDetails);
                }
            }

            recommendations = recommendations.OrderBy(recommendation => recommendation.Score).ToList();

            return new GetRecommendationsResponse
            {
                Recommendations = recommendations.Take(100).ToList()
            };
        }
    }
}
