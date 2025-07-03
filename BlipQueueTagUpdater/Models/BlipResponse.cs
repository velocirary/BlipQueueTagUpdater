using System.Text.Json.Serialization;

namespace BlipQueueTagUpdater.Models;

public class BlipResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("resource")]
    public ResourceData Resource { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("to")]
    public string To { get; set; }

    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; set; }
}

public class ResourceData
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("itemType")]
    public string ItemType { get; set; }

    [JsonPropertyName("items")]
    public List<AttendanceQueue> Items { get; set; }
}

public class Metadata
{
    [JsonPropertyName("traceparent")]
    public string TraceParent { get; set; }

    [JsonPropertyName("#command.uri")]
    public string CommandUri { get; set; }

    [JsonPropertyName("#metrics.custom.label")]
    public string MetricsCustomLabel { get; set; }
}
