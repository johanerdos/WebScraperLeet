using MediatR;
using WebScraperLeet.Features;

namespace WebScraperLeet.ScrapingService
{
    public class ScrapingService : IScrapingService
    {
        private readonly ISender _mediator;
        private readonly string _pathToIgnore = "../index.html";

        public ScrapingService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task InvokeScraper(string localFilePath)
        {
            Console.WriteLine("Starting scraping service. Please wait...");

            var rootUrlsToScrape = Enumerable.Range(1, 50).Select(i => $"page-{i}.html");

            await _mediator.Send(new ScrapingRequest()
            {
                RootUrlsToScrape = rootUrlsToScrape,
                LocalFilePath = localFilePath,
                PathToIgnore = _pathToIgnore
            });

            Console.WriteLine("Scraping complete. Press any key to exit.");
        }
    }
}
