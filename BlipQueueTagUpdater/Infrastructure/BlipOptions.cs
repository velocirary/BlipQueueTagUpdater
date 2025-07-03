namespace BlipQueueTagUpdater.Infrastructure;

public class BlipOptions
{
    public string BaseUrl { get; set; } = "";
    public string To { get; set; } = "";
    public string BotKey { get; set; } = "";
    public List<string> Tags { get; set; } = new();
}
