namespace BlipQueueTagUpdater.Infrastructure;

public interface IBlipClient
{
    Task<List<AttendanceQueue>> GetAttendanceQueuesAsync();    
    Task<bool> SetTagsAsync(string queueId, List<string> tags);
}