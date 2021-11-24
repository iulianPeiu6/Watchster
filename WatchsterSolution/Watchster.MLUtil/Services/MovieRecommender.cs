using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System;
using System.IO;
using System.Linq;
using Watchster.MLUtil.Models;
using Watchster.MLUtil.Services.Interfaces;

namespace Watchster.MLUtil.Services
{
    public class MovieRecommender : IMovieRecommender
    {
        private readonly ILogger<MovieRecommender> logger;
        private readonly MLContext mlContext;
        private ITransformer model;
        private PredictionEngine<MovieRating, MovieRatingPrediction> predictionEngine;
        private const double TrainTestRatio = 0.2;
        private readonly string modelPath;

        public MovieRecommender(ILogger<MovieRecommender> logger)
        {
            this.logger = logger;
            mlContext = new MLContext();
            modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "MovieRecommenderModel.zip");

            if (File.Exists(modelPath))
            {
                logger.LogInformation("Model is already saved locally.");
                LoadModel();
            }
            else
            {
                ConstructMoviePredictModel();
            }
        }

        private void LoadModel()
        {
            logger.LogInformation($"Loading model from '{modelPath}'...");
            DataViewSchema modelSchema;
 
            model = mlContext.Model.Load(modelPath, out modelSchema);
            predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
        }

        private void ConstructMoviePredictModel()
        {
            (IDataView trainingDataView, IDataView testDataView) = LoadData();
            BuildAndTrainModel(trainingDataView);
            EvaluateModel(testDataView);
            predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
            SaveModel(trainingDataView.Schema);
        }

        private (IDataView training, IDataView test) LoadData()
        {
            

            var dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "rating.csv");

            logger.LogInformation($"Loading Dataset from '{dataPath}'...");

            IDataView dataView = mlContext.Data.LoadFromTextFile<MovieRating>(dataPath, hasHeader: true, separatorChar: ',');

            logger.LogInformation($"Dataset loaded. Spliting Dataset using {1 - TrainTestRatio}:{TrainTestRatio} Train:Test Ratio");

            var dataSplit = mlContext.Data
                .TrainTestSplit(dataView, testFraction: TrainTestRatio);

            return (dataSplit.TrainSet, dataSplit.TestSet);
        }

        public void BuildAndTrainModel(IDataView trainingDataView)
        {
            logger.LogInformation($"Training model ...");

            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion
                .MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "MovieIdEncoded", inputColumnName: "MovieId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded",
                MatrixRowIndexColumnName = "MovieIdEncoded",
                LabelColumnName = "Label",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            model = trainerEstimator.Fit(trainingDataView);

            logger.LogInformation($"Model trained");
           
        }

        public void EvaluateModel(IDataView testDataView)
        {
            var prediction = model.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
            logger.LogInformation($"Root Mean Squared Error : {metrics.RootMeanSquaredError}");
            logger.LogInformation($"RSquared: {metrics.RSquared}");
        }

        private void SaveModel(DataViewSchema schema)
        {
            logger.LogInformation($"Saving the model to file '{modelPath}'...");
            mlContext.Model.Save(model, schema, modelPath);
        }

        public MovieRatingPrediction PredictMovieRating(MovieRating movie)
        {
            logger.LogInformation($"Prediction Movie Rating for Movie: {movie.MovieId} ...");
            return predictionEngine.Predict(movie);
        }
    }
}
