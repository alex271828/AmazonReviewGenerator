using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmazonReviewGenerator.review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonReviewGenerator.review.Tests
{
    [TestClass()]
    public class SentenceProcessorTests
    {
        [TestMethod()]
        public void GetSentencesTest()
        {
            var sentenceProcessor = new SentenceProcessor();

            var results = sentenceProcessor.GetSentences("aaa bbb. ccc dddd. gggg.");

            Assert.AreEqual(results.Count, 3);
        }
    }
}