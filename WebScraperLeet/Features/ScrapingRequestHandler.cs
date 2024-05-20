using HtmlAgilityPack;
using MediatR;
using WebScraperLeet.FileService;
using WebScraperLeet.HttpService;

namespace WebScraperLeet.Features
{
    public class ScrapingRequestHandler : IRequestHandler<ScrapingRequest>
    {
        private readonly IHttpService _httpService;
        private readonly IFileService _fileService;

        public ScrapingRequestHandler(IHttpService httpService, IFileService fileService)
        {
            _httpService = httpService;
            _fileService = fileService;
        }

        public async Task Handle(ScrapingRequest request, CancellationToken cancellationToken)
        {
            var rootUrlsToScrape = request.RootUrlsToScrape;
            var localFilePath = request.LocalFilePath;

            await Task.WhenAll(rootUrlsToScrape.Select(async path =>
            {
                var content = await _httpService.FetchUrlContentAsync(path);
                await _fileService.SavePageAsync(localFilePath, path, content);
            }));
        }
    }
}
