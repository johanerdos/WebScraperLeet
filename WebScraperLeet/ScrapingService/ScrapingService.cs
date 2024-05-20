using MediatR;
using WebScraperLeet.Features;

namespace WebScraperLeet.ScrapingService
{
    public class ScrapingService : IScrapingService
    {
        private readonly ISender _mediator;

        public ScrapingService(ISender mediator)
        {
            _mediator = mediator;
        }

        public async Task InvokeScraper()
        {
            var rootUrlsToScrape = Enumerable.Range(0, 50).Select(i => $"/catalogue/page-{i}.html");

            await _mediator.Send(new ScrapingRequest()
            {
                RootUrlsToScrape = rootUrlsToScrape
            });
        }
    }
}
