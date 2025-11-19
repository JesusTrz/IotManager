using IotManager.Infraestructure;
using IotManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IotManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IDeviceConfigHistoryService _historyService;

        public DeviceController(IDeviceService deviceService, IDeviceConfigHistoryService historyService)
        {
            _deviceService = deviceService;
            _historyService = historyService;
        }

        #region Device EndPoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devices = await _deviceService.GetAllAsync();
            return Ok(devices);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
                return NotFound();
            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Device device)
        {
            if (device == null)
                return BadRequest("El Dispositivo no puede contener datos nulos.");
            var newDevice = await _deviceService.AddAsync(device);
            return CreatedAtAction(nameof(GetById), new { id = newDevice.Id }, newDevice);
            //Usar CreatedAtAction() es mejor que solo Ok(),
            //porque cumple con el estándar REST (indica que se creó algo nuevo y dónde se puede consultar).
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Device device)
        {
            if (id != device.Id)
                return BadRequest("El ID del dispositivo no coincide.");

            var updateDevice = await _deviceService.UpdateAsync(device);
            if (updateDevice == null)
                return NotFound("El dispositivo no existe.");
            return Ok(updateDevice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _deviceService.DeleteAsync(id);
            if (!deleted)
                return NotFound("El Dispositivo no existe.");
            return NoContent(); //204: Eliminado correctamente
        }
        #endregion

        [HttpPost("{id}/snapshot")]
        public async Task<IActionResult> SaveSnapshot(int id)
        {
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
                return NotFound("El dispositivo no existe.");

            await _historyService.SaveSnapshotAsync(device);
            return Ok($"La configuracion del dispositivo {id} fue guardada exitosamente!");
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(int id)
        {
            var device = await _deviceService.GetByIdAsync(id);
            if (device == null)
                return NotFound($"El Dispositivo con el id {id} no existe.");

            var history = await _historyService.GetHistoryByDeviceIdAsync(id);

            if (!history.Any())
                return NotFound($"Este dispositivo: {id}, no tiene historial de configuraciones.");

            return Ok(history);
        }

        [HttpPost("{deviceId}/restore/{snapshotId}")]
        public async Task<IActionResult> RestoreSnapshot(int deviceId, int snapshotId)
        {
            var device = await _deviceService.GetByIdAsync(deviceId);
            if (device == null)
                return NotFound("El dispositivo no existe.");

            var snapshot = await _historyService.GetHistoryByDeviceIdAsync(deviceId);
            var targetSnapshot = snapshot.FirstOrDefault(h => h.Id == snapshotId);

            if (targetSnapshot == null)
                return NotFound("La configuracion especificada no existe.");

            // Guarda el estado actual antes de restaurar el estado anterior
            await _historyService.SaveSnapshotAsync(device);

            // Restaura la configuración guardada
            device.CurrentConfigJson = targetSnapshot.ConfigSnapshotJson;
            await _deviceService.UpdateAsync(device);

            return Ok($"Se restauro la configuracion #{snapshotId} para el dispositivo #{deviceId}.");
        }
    }
}
