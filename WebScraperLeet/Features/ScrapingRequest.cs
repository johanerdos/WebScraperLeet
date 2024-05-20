using MediatR;

namespace WebScraperLeet.Features
{
    public class ScrapingRequest : IRequest
    {
        public IEnumerable<string> RootUrlsToScrape { get; set; }
    }
}
