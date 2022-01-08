using System.Text;

namespace AmazonReviewGenerator.review
{
    public class SentenceProcessor
    {
        public List<string> GetWords(string sentence)
        {
            List<string> words = new List<string>();

            StringBuilder sb = new StringBuilder();

            foreach (var c in sentence.ToCharArray())
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

        public List<string> GetSentences(string reviewText)
        {
            List<string> sentences = new List<string>();

            StringBuilder sb = new StringBuilder();

            // TODO: what about "Ms. Jane" ?

            //var reviewTextL = reviewText.ToLower();
            foreach (var c in reviewText.ToCharArray())
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
                            sentences.Add(sb.ToString());
                            sb.Clear();
                        }
                    }
                }
            }

            return sentences;
        }
    }
}
