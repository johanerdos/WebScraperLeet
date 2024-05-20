using MediatR;

namespace WebScraperLeet.Features
{
    public class ScrapingRequest : IRequest
    {
        public List<string> RootUrlsToScrape { get; set; }
    }
}
