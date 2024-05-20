using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebScraperLeet.FileService;
using WebScraperLeet.HttpService;
using WebScraperLeet.ScrapingService;

var host = CreateHostBuilder(args).Build();
var scrapingService = host.Services.GetService<IScrapingService>();

var localFilePath = "C://ScrapedFiles";

await scrapingService!.InvokeScraper(localFilePath);

Console.ReadLine();

static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();
                logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
                services.AddSingleton<IScrapingService, ScrapingService>();
                services.AddSingleton<IFileService, FileService>();
                services.AddHttpClient<IHttpService, HttpService>(client =>
                {
                    client.BaseAddress = new Uri("https://books.toscrape.com/catalogue");
                });
            });
