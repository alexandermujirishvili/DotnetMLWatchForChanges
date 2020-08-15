using Microsoft.ML;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Models;
using TestWebApp.Services.Abstractions;

namespace TestWebApp.Services
{
    public class ModelTrainService : IModelTrainService
    {
        public async Task Train(IEnumerable<SentimentData> data)
        {
            Trace.WriteLine("Training has started");

            var context = new MLContext(seed: 1);

            var trainingDataView = context.Data.LoadFromEnumerable(data);

            var pipeline = ProcessData(context);

            ITransformer model = BuildAndTrainModel(context, pipeline, trainingDataView);

            Trace.WriteLine("Training has finished, saving model...");

            context.Model.Save(model, trainingDataView.Schema, Constants.ModelFileName);

            Trace.WriteLine("Model saved...");
        }

        private ITransformer BuildAndTrainModel(MLContext context, IEstimator<ITransformer> pipeline, IDataView trainingDataView)
        {
            var trainingPipeline = pipeline.Append(context.BinaryClassification.Trainers.SdcaLogisticRegression(
                labelColumnName: nameof(SentimentData.Sentiment),
                featureColumnName: "SentimentTextFeaturized"));

            var trainedModel = trainingPipeline.Fit(trainingDataView);

            return trainedModel;
        }

        private static IEstimator<ITransformer> ProcessData(MLContext context)
        {
            var pipeline = context.Transforms.Text.FeaturizeText(inputColumnName: nameof(SentimentData.SentimentText), outputColumnName: "SentimentTextFeaturized");

            return pipeline;
        }
    }
}
