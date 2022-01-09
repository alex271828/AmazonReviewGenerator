using System.Text;

namespace AmazonReviewGenerator.review
{
    public class SentenceProcessor
    {
        /// <summary>
        /// Get separate words from text.
        /// </summary>
        /// <param name="reviewText"></param>
        /// <returns></returns>
        public List<string> GetWords(string text)
        {
            List<string> words = new List<string>();

            StringBuilder sb = new StringBuilder();

            foreach (var c in text.ToCharArray())
            {
                if (char.IsLetter(c))
                {
                    sb.Append(c);
                }
                else
                {
                    if (sb.Length > 0)
                    {
                        words.Add(sb.ToString());
                        sb.Clear();
                    }

                    //if (c == ',' || c == '.' || c == ';' || c == ':')
                    //{
                    //    words.Add(c.ToString());
                    //}
                }
            }

            return words;
        }

        /// <summary>
        /// Get separate sentences from text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> GetSentences(string text)
        {
            List<string> sentences = new List<string>();

            StringBuilder sb = new StringBuilder();

            // TODO: what about "Ms. Jane" ?

            //var reviewTextL = reviewText.ToLower();
            foreach (var c in text.ToCharArray())
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
                else
                {
                    if (c == '.' || c == ';' || c == ':')
                    {
                        if (sb.Length > 0)
                        {
                            sentences.Add(sb.ToString().Trim());
                            sb.Clear();
                        }
                    }
                }
            }

            if (sb.Length > 0)
            {
                sentences.Add(sb.ToString().Trim());
            }

            return sentences;
        }
    }
}
