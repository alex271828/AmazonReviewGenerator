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
            {
                var results = sentenceProcessor.GetSentences("aaa bbb. ccc dddd. gggg");
                Assert.AreEqual(results.Count, 3);
                Assert.AreEqual(results[0], "aaa bbb");
                Assert.AreEqual(results[1], "ccc dddd");
                Assert.AreEqual(results[2], "gggg");
            }
            {
                var results = sentenceProcessor.GetSentences("aaa bbb; ccc dddd. gggg");
                Assert.AreEqual(results.Count, 3);
                Assert.AreEqual(results[0], "aaa bbb");
                Assert.AreEqual(results[1], "ccc dddd");
                Assert.AreEqual(results[2], "gggg");
            }
            {
                var results = sentenceProcessor.GetSentences("aaa bbb. ccc dddd: gggg");
                Assert.AreEqual(results.Count, 3);
                Assert.AreEqual(results[0], "aaa bbb");
                Assert.AreEqual(results[1], "ccc dddd");
                Assert.AreEqual(results[2], "gggg");
            }
        }

        [TestMethod()]
        public void GetWordsTest()
        {
            var sentenceProcessor = new SentenceProcessor();
            {
                var results = sentenceProcessor.GetWords("aaa bbb. ccc dddd. gggg");
                Assert.AreEqual(results.Count, 5);
                Assert.AreEqual(results[0], "aaa");
                Assert.AreEqual(results[1], "bbb");
                Assert.AreEqual(results[2], "ccc");
                Assert.AreEqual(results[3], "dddd");
                Assert.AreEqual(results[4], "gggg");
            }
            {
                var results = sentenceProcessor.GetWords("aaa bbb; ccc dddd: gggg");
                Assert.AreEqual(results.Count, 5);
                Assert.AreEqual(results[0], "aaa");
                Assert.AreEqual(results[1], "bbb");
                Assert.AreEqual(results[2], "ccc");
                Assert.AreEqual(results[3], "dddd");
                Assert.AreEqual(results[4], "gggg");
            }
            {
                var results = sentenceProcessor.GetWords("aaa bbb, ccc dddd. gggg");
                Assert.AreEqual(results.Count, 5);
                Assert.AreEqual(results[0], "aaa");
                Assert.AreEqual(results[1], "bbb");
                Assert.AreEqual(results[2], "ccc");
                Assert.AreEqual(results[3], "dddd");
                Assert.AreEqual(results[4], "gggg");
            }
        }
    }
}