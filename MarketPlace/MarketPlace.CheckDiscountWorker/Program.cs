using MarketPlace.CheckDiscountWorker;
using MarketPlace.CheckDiscountWorker.Models;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<DiscountWorker>();
        services.Configure<UrlAddress>(configuration.GetSection(nameof(UrlAddress)));
        services.AddSingleton<DiscountCheckClient>();
    })
    .Build();

host.Run();
