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
    public class AmazonReviewParserTests
    {
        [TestMethod()]
        public void ParseLinesTest()
        {
            var amazonReviewParser = new AmazonReviewParser();

            var lines = new string[]
            {
                "{\"overall\": 5.0, \"verified\": true, \"reviewTime\": \"08 22, 2013\", \"reviewerID\": \"A34A1UP40713F8\", \"asin\": \"B00009W3I4\", \"style\": { \"Style:\": \" Dryer Vent\"}, \"reviewerName\": \"James. Backus\", \"reviewText\": \"I like this as a vent as well as something that will keep house warmer in winter.  I sanded it and then painted it the same color as the house.  Looks great.\", \"summary\": \"Great product\", \"unixReviewTime\": 1377129600}",
                "{\"overall\": 5.0, \"verified\": true, \"reviewTime\": \"02 8, 2016\", \"reviewerID\": \"A1AHW6I678O6F2\", \"asin\": \"B00009W3PA\", \"style\": {\"Size:\": \" 6-Foot\"}, \"reviewerName\": \"kevin.\", \"reviewText\": \"good item\", \"summary\": \"Five Stars\", \"unixReviewTime\": 1454889600}",
                "{\"overall\": 5.0, \"verified\": true, \"reviewTime\": \"08 5, 2015\", \"reviewerID\": \"A8R48NKTGCJDQ\", \"asin\": \"B00009W3PA\", \"style\": {\"Size:\": \" 6-Foot\"}, \"reviewerName\": \"CDBrannom\", \"reviewText\": \"Fit my new LG dryer perfectly.\", \"summary\": \"Five Stars\", \"unixReviewTime\": 1438732800}",
            };
            var results = amazonReviewParser.ParseLines(lines);
            Assert.AreEqual(results.Count, 3);
            Assert.AreEqual(results[0].reviewText, "I like this as a vent as well as something that will keep house warmer in winter.  I sanded it and then painted it the same color as the house.  Looks great.");
            Assert.AreEqual(results[0].summary, "Great product");
            Assert.AreEqual(results[0].reviewerName, "James. Backus");
            Assert.AreEqual(results[0].overall, 5);

            Assert.AreEqual(results[1].reviewText, "good item");
            Assert.AreEqual(results[1].summary, "Five Stars");
            Assert.AreEqual(results[1].reviewerName, "kevin.");
            Assert.AreEqual(results[1].overall, 5);

            Assert.AreEqual(results[2].reviewText, "Fit my new LG dryer perfectly.");
            Assert.AreEqual(results[2].summary, "Five Stars");
            Assert.AreEqual(results[2].reviewerName, "CDBrannom");
            Assert.AreEqual(results[2].overall, 5);
        }

        [TestMethod()]
        public void ProcessReviewsTest()
        {
            var amazonReviewParser = new AmazonReviewParser();

            var reviews = new List<ReviewModel>();

            reviews.Add(new ReviewModel 
            (
                overall: 1,
                reviewerName: "reviewerName one",
                summary: "summary one",
                reviewText: "reviewText one"
            ));
            reviews.Add(new ReviewModel
            (
                overall: 2,
                reviewerName: "reviewerName two",
                summary: "summary two",
                reviewText: "reviewText two"
            ));
            reviews.Add(new ReviewModel
            (
                overall: 3,
                reviewerName: "reviewerName3",
                summary: "",
                reviewText: null
            ));

            var results = new List<string>();

            amazonReviewParser.ProcessReviews(reviews, (sentence) =>
            {
                results.Add(sentence);
            });

            Assert.AreEqual(results.Count, 2);
            Assert.AreEqual(results[0], "reviewText one");
            Assert.AreEqual(results[1], "reviewText two");
        }
    }
}