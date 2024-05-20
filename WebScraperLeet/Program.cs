using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebScraperLeet.HttpService;
using WebScraperLeet.ScrapingService;

var host = CreateHostBuilder(args).Build();
var scrapingService = host.Services.GetService<IScrapingService>();

var localFilePath = "C://ScrapedFiles";

await scrapingService!.InvokeScraper(localFilePath);

Console.ReadLine();

static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
                services.AddSingleton<IScrapingService, ScrapingService>();
                services.AddHttpClient<IHttpService, HttpService>(client =>
                {
                    client.BaseAddress = new Uri("https://books.toscrape.com/catalogue");
                });
            });
