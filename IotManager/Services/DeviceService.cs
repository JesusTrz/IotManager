

using IotManager.Data;
using IotManager.Infraestructure;
using IotManager.Models;
using Microsoft.EntityFrameworkCore;

namespace IotManager.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly AppDbContext _context;
        public DeviceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices.ToListAsync();
        }
        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }
        public async Task<Device> AddAsync(Device device)
        {
            var existDevice = await _context.Devices.FirstOrDefaultAsync(m=>m.MacAddress == device.MacAddress);
            if (existDevice == null)
            {
                await _context.Devices.AddAsync(device);
                await _context.SaveChangesAsync();
                return device;
            }
            else
            {
                return existDevice;
            }
                
        }
        public async Task<Device?> UpdateAsync(Device device)
        {
            var deviceExist = await _context.Devices.FindAsync(device.Id);
            if (deviceExist == null)
                throw new Exception("El Dsipositivo no existe");

            deviceExist.Name = device.Name;
            deviceExist.Status = device.Status;
            deviceExist.CurrentConfigJson = device.CurrentConfigJson;

            await _context.SaveChangesAsync();
            return deviceExist;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device == null)
                throw new Exception("El Dispositivo no existe");

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
