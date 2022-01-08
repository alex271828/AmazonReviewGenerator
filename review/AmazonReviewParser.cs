namespace AmazonReviewGenerator.review
{
    public class AmazonReviewParser
    {
        public List<ReviewModel> Parse(string filename)
        {
            var jsonLines = System.IO.File.ReadAllLines(filename);

            return ParseLines(jsonLines);
        }

        public List<ReviewModel> ParseLines(string[] lines)
        {
            var amazonReviews = new List<ReviewModel>();

            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Populate
            };

            foreach (var jsonText in lines)
            {
                try
                {
                    var review = Newtonsoft.Json.JsonConvert.DeserializeObject<ReviewModel>(jsonText, settings);

                    amazonReviews.Add(review);
                }
                catch
                {
                    // TODO: log the error
                    continue;
                }
            }

            return amazonReviews;
        }
    }
}
