using MediatR;

namespace WebScraperLeet.Features
{
    public class ScrapingRequest : IRequest
    {
        public IEnumerable<string> RootUrlsToScrape { get; set; }
        public string LocalFilePath { get; set; }
        public string PathToIgnore { get; set; }
    }
}
