using MediatR;

namespace WebScraperLeet.Features
{
    public class ScrapingRequestHandler : IRequestHandler<ScrapingRequest>
    {
        public Task Handle(ScrapingRequest request, CancellationToken cancellationToken)
        {
            var rootUrlsToScrape = request.RootUrlsToScrape;


            throw new NotImplementedException();
        }
    }
}
