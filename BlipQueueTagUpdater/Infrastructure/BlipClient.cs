using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlipQueueTagUpdater.Models;
using Microsoft.Extensions.Options;

namespace BlipQueueTagUpdater.Infrastructure;

public class BlipClient : IBlipClient 
{
    private readonly HttpClient _httpClient;
    private readonly BlipOptions _options;

    public BlipClient(HttpClient httpClient, IOptions<BlipOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<List<AttendanceQueue>> GetAttendanceQueuesAsync()
    {
        var requestBody = new
        {
            id = Guid.NewGuid().ToString(),
            to = _options.To,
            method = "get",
            uri = "/attendance-queues?$skip=0&$take=2080"
        };

        var json = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, _options.BaseUrl)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Key", _options.BotKey);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new List<AttendanceQueue>();        

        var responseContent = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<BlipResponse>(responseContent);

        return data?.Resource?.Items ?? new List<AttendanceQueue>();
    }

    public async Task<bool> SetTagsAsync(string queueId, List<string> tags)
    {
        var requestBody = new
        {
            id = Guid.NewGuid().ToString(),
            to = _options.To,
            method = "set",
            uri = $"/attendance-queues/{queueId}/tags",
            type = "application/vnd.lime.collection+json",
            resource = new
            {
                itemType = "application/vnd.iris.desk.attendancequeuetag+json",
                items = tags.Select(tag => new { tag }).ToList()
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var request = new HttpRequestMessage(HttpMethod.Post, _options.BaseUrl)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Key", _options.BotKey);

        var response = await _httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}
