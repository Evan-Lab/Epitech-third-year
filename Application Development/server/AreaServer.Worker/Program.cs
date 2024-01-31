//using AreaServer.Worker;

//IHost host = Host.CreateDefaultBuilder(args)
//    .ConfigureServices(services =>
//    {
//        services.AddHostedService<Worker>();
//    })
//    .Build();

//await host.RunAsync();

using AreaServer.Infrastructure.Data;
using AreaServer.Worker;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(hostContext.Configuration.GetConnectionString("WebApiDatabase")));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

