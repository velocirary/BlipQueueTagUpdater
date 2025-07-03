using BlipQueueTagUpdater.Infrastructure;
using Microsoft.Extensions.Options;

namespace BlipQueueTagUpdater.Services;

public class QueueService : IQueueService
{
    private readonly IBlipClient _blipClient;
    private readonly List<string> _tags;

    public QueueService(IBlipClient blipClient, IOptions<BlipOptions> options)
    {
        _blipClient = blipClient;
        _tags = options.Value.Tags;
    }

    public async Task ExecuteAsync()
    {
        var queues = await _blipClient.GetAttendanceQueuesAsync();
        var tags = _tags;

        foreach (var queue in queues)
        {
            Console.WriteLine($"Atualizando fila {queue.Id}...");

            var success = await _blipClient.SetTagsAsync(queue.Id, tags);

            Console.WriteLine(success
                ? $"Tags atualizadas para fila '{queue.Id}'"
                : $"Falha ao atualizar fila '{queue.Id}'");
        }
    }
}
