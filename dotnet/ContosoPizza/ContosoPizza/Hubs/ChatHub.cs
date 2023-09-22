using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using static ContosoPizza.Pages.CpuMemoryModel;

namespace ContosoPizza.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage()
        {
            //// troubleshoot: https://www.urtech.ca/2015/09/solved-cannot-load-counter-name-data-because-an-invalid-index-was-read-from-the-registry/
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
         
            cpuCounter.NextValue();
            Task.Delay(100).Wait();
            var cpu = cpuCounter.NextValue().ToString("0.##");

            Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();

            cpu = $"CPU:{cpu} %";
            var ram = $"RAM: {tot - phav}/{tot} Mb";

            await Clients.All.SendAsync("ReceiveMessage", cpu, ram);
        }
    }
}
