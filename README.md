# DotnetMLWatchForChanges

### Issue: Model is not reloaded after it is retrained (only if prediction was made before)

Sample web api has 3 endpoints:

1) https://localhost:44390/api/data - returns training data
2) https://localhost:44390/api/data/add?sentimentText=ItsBrilliant&sentiment=true - adds record to training data
3) https://localhost:44390/api/data/check?sentimentText=ItsBrilliant - checks sentiment

At first, this is training data:

```json
[
  {
    "SentimentText": "ItsGood",
    "Sentiment": true
  },
  {
    "SentimentText": "ItsBad",
    "Sentiment": false
  }
]
```

When I call https://localhost:44390/api/data/check?sentimentText=ItsBrilliant, response looks like this:

```json
{
    "prediction": false,
    "probability": 0.23420961,
    "score": -1.184692
}
```

After I call https://localhost:44390/api/data/add?sentimentText=ItsBrilliant&sentiment=true, training data is modified:

```json
[
  {
    "SentimentText": "ItsGood",
    "Sentiment": true
  },
  {
    "SentimentText": "ItsBad",
    "Sentiment": false
  },
  {
    "SentimentText": "ItsBrilliant",
    "Sentiment": true
  }
]
```

But https://localhost:44390/api/data/check?sentimentText=ItsBrilliant returns same - prediction does not change.
