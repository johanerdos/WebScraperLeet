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
            var rootUrlsToScrape = new List<string>();

            for(int i = 0; i < 50; i++)
            {
                rootUrlsToScrape.Add($"/catalogue/page-{i}.html");
            }

            await _mediator.Send(new ScrapingRequest()
            {
                RootUrlsToScrape = rootUrlsToScrape
            });
        }
    }
}
