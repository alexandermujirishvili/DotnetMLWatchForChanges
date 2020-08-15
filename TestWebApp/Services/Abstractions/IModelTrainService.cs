using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApp.Models;

namespace TestWebApp.Services.Abstractions
{
    public interface IModelTrainService
    {
        Task Train(IEnumerable<SentimentData> data);
    }
}
