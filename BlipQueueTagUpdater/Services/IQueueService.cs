namespace BlipQueueTagUpdater.Services;

public interface IQueueService
{
    Task ExecuteAllAsync();
    Task ExecuteAllowedOnlyAsync(string jsonPath = "queues.json");
}