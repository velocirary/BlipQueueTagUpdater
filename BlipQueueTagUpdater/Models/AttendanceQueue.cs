using System.Text.Json.Serialization;

public class AttendanceQueue
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("ownerIdentity")]
    public string OwnerIdentity { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }

    [JsonPropertyName("storageDate")]
    public DateTime StorageDate { get; set; }

    [JsonPropertyName("Priority")]
    public int Priority { get; set; }

    [JsonPropertyName("uniqueId")]
    public string UniqueId { get; set; }
}
