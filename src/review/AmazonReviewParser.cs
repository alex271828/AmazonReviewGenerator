namespace AmazonReviewGenerator.review
{
    /// <summary>
    /// Handle amazon review files and teach our AI.
    /// </summary>
    public class AmazonReviewParser
    {
        /// <summary>
        /// Load file data and convert json lines to review objects.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public List<ReviewModel> Parse(string filename)
        {
            // files contain lines, each containing one json object
            var jsonLines = System.IO.File.ReadAllLines(filename);

            return ParseLines(jsonLines);
        }

        /// <summary>
        /// Convert json lines to review objects.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Teach our AI using review objects.
        /// </summary>
        /// <param name="reviews"></param>
        /// <param name="processSentence"></param>
        public void ProcessReviews(List<ReviewModel> reviews, Action<string> processSentence)
        {
            var sentenceProcessor = new SentenceProcessor();

            foreach (var review in reviews)
            {
                // skip invalid reviews, we can't learn anything here
                if (string.IsNullOrWhiteSpace(review.reviewText)) continue;

                // get sentences from review text
                var sentences = sentenceProcessor.GetSentences(review.reviewText);

                // TODO: what about summary and reviewerName

                foreach (var sentence in sentences)
                {
                    // teach our AI
                    processSentence(sentence);
                }
            }
        }
    }
}
