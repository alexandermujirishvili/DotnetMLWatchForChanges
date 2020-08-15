using Microsoft.ML.Data;

namespace TestWebApp.Models
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string SentimentText;

        [LoadColumn(1)]
        public bool Sentiment;

        public override bool Equals(object obj)
        {
            if(!(obj is SentimentData sentimentData))
            {
                return false;
            }

            return string.Equals(sentimentData.SentimentText, SentimentText);
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(SentimentText);
        }

        public override int GetHashCode()
        {
            return SentimentText.GetHashCode();
        }
    }
}
