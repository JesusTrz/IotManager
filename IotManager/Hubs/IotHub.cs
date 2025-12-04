using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IotManager.Hubs
{
    public class IotHub : Hub
    {
        // Metodo que se ejecuta cuando alguien se conecta a la Mac del Dispositivo Iot
        public override async Task OnConnectedAsync()
        {
            // Se recupera la MAC simulada
            var httpContext = Context.GetHttpContext();
            var macAddress = httpContext.Request.Query["macAddress"];

            // Si el dispositivo tiene Mac, se mete como ConnectionId
            if(!string.IsNullOrEmpty(macAddress))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, macAddress);
                Console.WriteLine($"[HUB] Cliente conectado al grupo: {macAddress}");
            }
            await base.OnConnectedAsync();
        }
        public async Task ReportarEstado(object datos)
        {
            // Por ahora, solo retransmitimos el mensaje a todos los clientes (ej. al Dashboard en React)
            // para ver que los datos fluyen.
            await Clients.All.SendAsync("NuevoDatoIot", datos);

            // Opcional: Imprimir en la consola del servidor para depurar
            Console.WriteLine($"[HUB] Dato recibido: {datos}");
        }
    }
}
