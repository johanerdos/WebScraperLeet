using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperLeet.Tests
{
    [TestFixture]
    public class ScrapingTests
    {
        private readonly List<string> _rootUrlsToScrape = new List<string>()
        {
            "/page-1.html",
            "/page-2.html",
            "/page-3.html"
        };


        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public async Task Should_Fetch_All_Pages()
        {
            Assert.That(_rootUrlsToScrape, Does.Contain("someRootUrl"));
        }

        [Test]
        public async Task Should_Fetch_All_Inner_Pages()
        {
            var expectedInnerPagesToScrape = new List<string>()
            {
                "/some_book.index.html",
                "/my_book.index.html"
            };

            Assert.That(_rootUrlsToScrape.Contains("someRootUrl") || expectedInnerPagesToScrape.Contains("someInnerUrl"), Is.True);
        }
    }
}
