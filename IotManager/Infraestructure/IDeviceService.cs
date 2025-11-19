using IotManager.Models;

namespace IotManager.Infraestructure
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(int id);
        Task<Device> AddAsync(Device device);
        Task<Device?> UpdateAsync(Device device);
        Task<bool> DeleteAsync(int id);
    }
}
