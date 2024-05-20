using HtmlAgilityPack;
using MediatR;
using WebScraperLeet.HttpService;

namespace WebScraperLeet.Features
{
    public class ScrapingRequestHandler : IRequestHandler<ScrapingRequest>
    {
        private readonly IHttpService _httpService;

        public ScrapingRequestHandler(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task Handle(ScrapingRequest request, CancellationToken cancellationToken)
        {
            var rootUrlsToScrape = request.RootUrlsToScrape;
            var localFilePath = request.LocalFilePath;

            await Task.WhenAll(rootUrlsToScrape.Select(async path =>
            {
                var content = await _httpService.FetchUrlContentAsync(path);
                await SavePageAsync(localFilePath, path, content);
            }));
        }

        private async Task SavePageAsync(string localFilePath, string fileName, string content)
        {
            var folderName = Path.Combine(localFilePath, fileName);

            try
            {
                if(!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var filePath = Path.Combine(folderName, "index.html");
                await File.WriteAllTextAsync(filePath, content);
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"An error occurred when saving the file: {ex.Message}");
            }
        }
    }
}
