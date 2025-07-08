using BlipQueueTagUpdater.Infrastructure;
using BlipQueueTagUpdater.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<BlipOptions>(context.Configuration.GetSection("Blip"));
        services.AddHttpClient<IBlipClient, BlipClient>();
        services.AddScoped<IQueueService, QueueService>();
    })
    .Build();

var svc = host.Services.GetRequiredService<IQueueService>();

// Executa a atualização apenas nas filas que estão listadas no arquivo queues.json.
await svc.ExecuteAllowedOnlyAsync();

// Executa a atualização em todas as filas retornadas pela API da Blip, sem filtro.
await svc.ExecuteAllAsync();
