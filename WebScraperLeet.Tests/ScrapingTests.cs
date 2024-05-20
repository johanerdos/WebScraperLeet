using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraperLeet.Features;
using WebScraperLeet.Tests.Utils;

namespace WebScraperLeet.Tests
{
    [TestFixture]
    public class ScrapingTests
    {
        private MockContainer? _mockContainer;
        private readonly MockManager _mockManager = new();
        private ScrapingRequestHandler _handler;


        private readonly List<string> _rootUrlsToScrape = new List<string>()
        {
            "/page-1.html",
            "/page-2.html",
            "/page-3.html"
        };

        [SetUp]
        public void SetUp()
        {
            _mockContainer = _mockManager.SetUpMocks();
            _handler = new(_mockContainer.HttpServiceMock.Object);
        }

        [Test]
        public async Task Should_Fetch_All_Pages()
        {
            _mockContainer?.HttpServiceMock?.Setup(x => x.FetchUrlContentAsync(It.IsAny<string>()))
                .Callback<string>((rootUrl) =>
                {
                    Assert.That(_rootUrlsToScrape, Does.Contain(rootUrl));
                });

            await _handler.Handle(new ScrapingRequest()
            {
                RootUrlsToScrape = _rootUrlsToScrape
            }, 
            CancellationToken.None);

            _mockContainer?.HttpServiceMock?.Verify(x => x.FetchUrlContentAsync(It.IsAny<string>()), Times.Exactly(_rootUrlsToScrape.Count));
        }

        [Test]
        public async Task Should_Fetch_All_Inner_Pages()
        {
            var expectedInnerPagesToScrape = new List<string>()
            {
                "/some_book.index.html",
                "/my_book.index.html"
            };

            await _handler.Handle(new ScrapingRequest()
            {
                RootUrlsToScrape = _rootUrlsToScrape
            }, 
            CancellationToken.None);


            

            Assert.That(_rootUrlsToScrape.Contains("someRootUrl") || expectedInnerPagesToScrape.Contains("someInnerUrl"), Is.True);
        }
    }
}
