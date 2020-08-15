using System.Collections.Generic;
using TestWebApp.Models;

namespace TestWebApp.Data
{
    public class TrainDataContainer
    {
        static TrainDataContainer()
        {
            Data = new HashSet<SentimentData>();

            Data.Add(new SentimentData
            {
                SentimentText = "ItsGood",
                Sentiment = true,
            });

            Data.Add(new SentimentData
            {
                SentimentText = "ItsBad",
                Sentiment = false,
            });
        }

        public static ISet<SentimentData> Data;
    }
}
