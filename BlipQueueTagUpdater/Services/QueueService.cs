using BlipQueueTagUpdater.Infrastructure;
using Microsoft.Extensions.Options;
using System.Text.Json;

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

    public async Task ExecuteAllAsync()
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

    public async Task ExecuteAllowedOnlyAsync(string jsonPath = "queues.json")
    {
        var allowedQueues = LoadAllowedQueuesFromJson(jsonPath);
        var queues = await _blipClient.GetAttendanceQueuesAsync();

        var filteredQueues = queues.Where(q => allowedQueues.Contains(q.Name)).ToList();

        foreach (var queue in filteredQueues)
        {
            Console.WriteLine($"Atualizando fila {queue.Id}...");

            var success = await _blipClient.SetTagsAsync(queue.Id, _tags);

            Console.WriteLine(success
                ? $"Tags atualizadas para fila '{queue.Id}'"
                : $"Falha ao atualizar fila '{queue.Id}'");
        }
    }

    private List<string> LoadAllowedQueuesFromJson(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine($"Arquivo '{path}' não encontrado.");
            return new List<string>();
        }

        var json = File.ReadAllText(path);
        var config = JsonSerializer.Deserialize<QueueConfig>(json);
        return config?.AllowedQueues ?? new List<string>();
    }
}