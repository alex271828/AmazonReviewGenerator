using MarkovNextGen;

namespace AmazonReviewGenerator.review
{
    public class ReviewGenerator
    {
        public void Init(string folder)
        {
            // check for exiting pdo files
            var pdoFiles = System.IO.Directory.GetFiles(folder, "*.pdo", SearchOption.AllDirectories);
            if (pdoFiles.Length > 0)
            {
                _markov = new Markov(pdoFiles[0]);
            }
            else
            {
                // check to see if data folder contains json file with training data
                var jsonFiles = System.IO.Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories);

                if (jsonFiles.Length > 0)
                {
                    _markov = new Markov(Path.Combine(folder, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdo"));

                    var amazonReviewParser = new AmazonReviewParser();
                    var sentenceProcessor = new SentenceProcessor();

                    foreach (var jsonFile in jsonFiles)
                    {
                        var reviews = amazonReviewParser.Parse(jsonFile);

                        foreach (var review in reviews)
                        {
                            if (string.IsNullOrWhiteSpace(review.reviewText)) continue;

                            var sentences = sentenceProcessor.GetSentences(review.reviewText);

                            foreach (var sentence in sentences)
                            {
                                _markov.AddToChain(sentence);
                            }
                        }
                    }
                }
            }
        }

        Markov _markov = null;

        public ReviewModel GenerateReview()
        {
            if (_markov == null)
            {
                // failed to initialize
                return new AmazonReviewGenerator.review.ReviewModel(Random.Shared.Next(1, 6), "reviewerName", "reviewText", "summary");
            }

            return new AmazonReviewGenerator.review.ReviewModel
               (
                   Random.Shared.Next(1, 6),
                   "reviewerName",
                   _markov.Generate(Random.Shared.Next(10, 100)),
                   _markov.Generate(Random.Shared.Next(3, 20))
               );
        }
    }
}
