using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebScraperLeet.ScrapingService;

var host = CreateHostBuilder(args).Build();
var scrapingService = host.Services.GetService<IScrapingService>();

await scrapingService!.InvokeScraper();

Console.ReadLine();

static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
                services.AddSingleton<IScrapingService, ScrapingService>();
            });
