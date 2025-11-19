using IotManager.Data;
using IotManager.Infraestructure;
using IotManager.Models;
using Microsoft.EntityFrameworkCore;

namespace IotManager.Services
{
    public class DeviceConfigHistoryService : IDeviceConfigHistoryService
    {
        private readonly AppDbContext _context;

        public DeviceConfigHistoryService(AppDbContext context)
        {
            _context = context;
        }
        // "Device" funciona como el "Originator" y SaveSnapshotAsync "crea la foto, el "Memento""
        // Crea un nuevo registro con la configuración actual del dispositivo y el DeviceConfigHistory funciona como el "Memento"
        public async Task SaveSnapshotAsync(Device device)
        {
            var snapshot = new DeviceConfigHistory
            {
                DeviceId = device.Id,
                ConfigSnapshotJson = device.CurrentConfigJson,
                Timestamp = DateTime.Now
            };

            await _context.DeviceConfigHistory.AddAsync(snapshot);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<DeviceConfigHistory>> GetHistoryByDeviceIdAsync(int deviceId)
        {
            return await _context.DeviceConfigHistory
                .Where(h => h.DeviceId == deviceId)
                .OrderByDescending(h => h.Timestamp)
                .ToListAsync();
        }
        public async Task<DeviceConfigHistory?> GetLastSnapshotAsync(int deviceId)
        {
            return await _context.DeviceConfigHistory
                .Where(h => h.DeviceId == deviceId)
                .OrderByDescending(h => h.Timestamp)
                .FirstOrDefaultAsync();
        }
    }
}
