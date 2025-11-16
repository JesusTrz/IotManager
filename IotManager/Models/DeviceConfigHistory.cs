namespace IotManager.Models
{
    public class DeviceConfigHistory
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public string ConfigSnapshotJson { get; set; } = "{}";
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
