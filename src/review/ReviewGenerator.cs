using MarkovNextGen;

namespace AmazonReviewGenerator.review
{
    public class ReviewGenerator
    {
        /// <summary>
        /// Initialize our AI.
        /// </summary>
        /// <param name="folder"></param>
        public void Init(string folder)
        {
            // check for exiting pdo files
            var pdoFiles = System.IO.Directory.GetFiles(folder, "*.pdo", SearchOption.AllDirectories);
            if (pdoFiles.Length > 0)
            {
                // we can handle only one file. For now.
                _markov = new Markov(pdoFiles[0]);
            }
            else
            {
                // check to see if data folder contains json file with training data
                var jsonFiles = System.IO.Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories);

                if (jsonFiles.Length > 0)
                {
                    // create new pdo file
                    _markov = new Markov(Path.Combine(folder, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdo"));

                    var amazonReviewParser = new AmazonReviewParser();

                    foreach (var jsonFile in jsonFiles)
                    {
                        // get reviews from file
                        var reviews = amazonReviewParser.Parse(jsonFile);

                        // teach our AI using review objects
                        amazonReviewParser.ProcessReviews(reviews, (sentence) =>
                        {
                            _markov.AddToChain(sentence);
                        });
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
                   "reviewerName", // TODO: get randon name from the list of names
                   _markov.Generate(Random.Shared.Next(10, 100)),
                   _markov.Generate(Random.Shared.Next(3, 20))
               );
        }
    }
}
