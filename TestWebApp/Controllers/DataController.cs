using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Newtonsoft.Json;
using TestWebApp.Data;
using TestWebApp.Models;
using TestWebApp.Services.Abstractions;

namespace TestWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IModelTrainService modelTrainService;
        private readonly PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool;
        public DataController(IModelTrainService modelTrainService, PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool)
        {
            this.modelTrainService = modelTrainService;
            this.predictionEnginePool = predictionEnginePool;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responseString = JsonConvert.SerializeObject(TrainDataContainer.Data, Formatting.Indented);

            return Ok(responseString);
        }

        [HttpGet("Add")]
        public async Task<IActionResult> Add(string sentimentText, bool sentiment)
        {
            var sentimentData = new SentimentData
            {
                SentimentText = sentimentText,
                Sentiment = sentiment
            };

            if(!sentimentData.IsValid())
            {
                return BadRequest("Invalid request");
            }

            TrainDataContainer.Data.Add(new SentimentData
            {
                SentimentText = sentimentText,
                Sentiment = sentiment
            });

            await modelTrainService.Train(TrainDataContainer.Data);

            return Ok();
        }

        [HttpGet("Check")]
        public async Task<IActionResult> Check(string sentimentText)
        {
            var prediction = predictionEnginePool.Predict(Constants.ModelName, new SentimentData { SentimentText = sentimentText });

            return Ok(prediction);
        }
    }
}
