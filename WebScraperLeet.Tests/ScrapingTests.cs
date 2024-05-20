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


        private readonly List<string> _rootPagesToScrape = new()
        {
            "/page-1.html",
            "/page-2.html"
        };

        private readonly List<string> _expectedInnerPages = new()
        {
            "/some_book_inner.index.html",
            "/my_book_inner.index.html",
            "/some_other_book_inner.index.html",
            "/my_other_book_inner.index.html"
        };

        private readonly string _mockedHtml = "@\"\r\n<html>\r\n<body>\r\n<a href='/some_book_inner.index.html'>Page 1</a>\r\n<a href='/my_book_inner.index.html'>Page 2</a>\r\n<a href='/some_other_book_inner.index.html'>Page 3</a>\r\n<a href='/my_other_book_inner.index.html'>Page 4</a>\r\n</body>\r\n</html>\"";

        [SetUp]
        public void SetUp()
        {
            _mockContainer = _mockManager.SetUpMocks();
            _handler = new(_mockContainer?.HttpServiceMock?.Object, _mockContainer?.FileServiceMock?.Object);
        }

        [Test]
        public async Task Should_Fetch_All_Pages()
        {

            _mockContainer?.HttpServiceMock?.Setup(x => x.FetchUrlContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_mockedHtml))
                .Callback<string>((rootUrl) =>
                {
                    Assert.That(_rootPagesToScrape.Contains(rootUrl) || _expectedInnerPages.Contains(rootUrl), Is.True);
                });

            await _handler.Handle(new ScrapingRequest()
            {
                RootUrlsToScrape = _rootPagesToScrape
            }, 
            CancellationToken.None);
        }

        [Test]
        public async Task Should_Save_File()
        {
            var myLocalFilePath = "myLocalFilePath";

            _mockContainer?.HttpServiceMock?.Setup(x => x.FetchUrlContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_mockedHtml));

            _mockContainer?.FileServiceMock?.Setup(x => x.SavePageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string, string, string>((localFilePathParam, fileNameParam, contentParam, parentFolderName) => {
                    Assert.Multiple(() =>
                    {
                        Assert.That(myLocalFilePath, Is.EqualTo(localFilePathParam));
                        Assert.That(_rootPagesToScrape.Contains(fileNameParam) || _expectedInnerPages.Contains(fileNameParam), Is.True);
                    });
                });

            await _handler.Handle(new ScrapingRequest()
            {
                RootUrlsToScrape = _rootPagesToScrape,
                LocalFilePath = myLocalFilePath
            },
            CancellationToken.None);
        }

        [Test]
        public async Task Should_Fetch_All_Inner_Pages()
        {
            

            _mockContainer?.HttpServiceMock?.Setup(x => x.FetchUrlContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_mockedHtml))
                .Callback<string>((path) =>
                {
                    Assert.That(_rootPagesToScrape.Contains(path) || _expectedInnerPages.Contains(path), Is.True);
                });

            await _handler.Handle(new ScrapingRequest()
            {
                RootUrlsToScrape = _rootPagesToScrape
            }, 
            CancellationToken.None);

            var expectedNumberOfInvocations = _rootPagesToScrape.Count * _expectedInnerPages.Count + _rootPagesToScrape.Count;
            _mockContainer?.HttpServiceMock?.Verify(x => x.FetchUrlContentAsync(It.IsAny<string>()), Times.Exactly(expectedNumberOfInvocations));
        }
    }
}
