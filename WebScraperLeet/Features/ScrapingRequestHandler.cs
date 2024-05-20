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
                var parentFolderName = await _fileService.SavePageAsync(localFilePath, path, content);

                var innerPaths = ExtractLinks(content, request.PathToIgnore);
                await ProcessLinksAsync(innerPaths, localFilePath, parentFolderName);
            }));
        }

        private static List<string> ExtractLinks(string content, string pathToIgnore)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);

            var filteredHrefs = htmlDocument.DocumentNode
                .SelectNodes("//a[@href]")
                ?.Select(linkNode => linkNode.GetAttributeValue("href", ""))
                .Where(link => !link.StartsWith("https://") && !link.Contains("category") && link.Contains("index.html"))
                .Distinct()
                .ToList() ?? new List<string>();

            filteredHrefs.Remove(pathToIgnore);

            return filteredHrefs;
        }

        private async Task ProcessLinksAsync(List<string> links, string localFilePath, string? parentFolderName = null)
        {
            var linkTasks = links.Select(async link =>
            {
                var linkedPageContent = await _httpService.FetchUrlContentAsync(link);
                await _fileService.SavePageAsync(localFilePath, link, linkedPageContent, parentFolderName);
            }).ToList();

            await Task.WhenAll(linkTasks);
        }
    }
}
