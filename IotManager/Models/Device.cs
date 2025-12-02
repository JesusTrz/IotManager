namespace IotManager.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; } = "";
        public string Status { get; set; } = "Offline";
        public string CurrentConfigJson { get; set; } = "{}";
    }
}
