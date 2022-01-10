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
                if (char.IsLetter(c)) // for now we only care about letters
                {
                    sb.Append(c);
                }
                else
                {
                    // not a letter, see if we have a word
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

            // add remaining text
            if (sb.Length > 0)
            {
                words.Add(sb.ToString().Trim());
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

            // TODO: what about "Ms. Jane" and such ?

            //var reviewTextL = reviewText.ToLower();
            foreach (var c in text.ToCharArray())
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
                else
                {
                    // not sure about which characters can separate sentences
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

            // add remaining text
            if (sb.Length > 0)
            {
                sentences.Add(sb.ToString().Trim());
            }

            return sentences;
        }
    }
}
