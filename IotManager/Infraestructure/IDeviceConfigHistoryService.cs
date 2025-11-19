using IotManager.Models;

namespace IotManager.Infraestructure
{
    public interface IDeviceConfigHistoryService
    {
        Task SaveSnapshotAsync(Device device); //Funciona como el "Originator"
        Task<IEnumerable<DeviceConfigHistory>> GetHistoryByDeviceIdAsync(int deviceId);
        Task<DeviceConfigHistory?> GetLastSnapshotAsync(int deviceId);
    }
}
