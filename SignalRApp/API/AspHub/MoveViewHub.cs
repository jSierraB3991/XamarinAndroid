namespace API.AspHub
{
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class MoveViewHub: Hub
    {
        public async Task MoveViewFromServerAsync(float newX, float newY)
        {
            Console.WriteLine($"Recevied position from server app {newX}/{newY}");
            await Clients.Others.SendAsync("ReceiveNewPosition", newX, newY);
        } 
    }
}
