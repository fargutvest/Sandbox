using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ContosoPizza.Pages
{
    public partial class CpuMemoryModel : PageModel
    {

        public string Cpu { get; set; } = "no data";
        public string Ram { get; set; } = "no data";

        public CpuMemoryModel()
        {

        }

        public void OnGet()
        {
            //// troubleshoot: https://www.urtech.ca/2015/09/solved-cannot-load-counter-name-data-because-an-invalid-index-was-read-from-the-registry/
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var categories = PerformanceCounterCategory.GetCategories();

            cpuCounter.NextValue();
            Task.Delay(100).Wait();
            var cpu = cpuCounter.NextValue().ToString("0.##") + "%";

            Int64 phav = PerformanceInfo.GetPhysicalAvailableMemoryInMiB();
            Int64 tot = PerformanceInfo.GetTotalMemoryInMiB();

            Cpu = $"CPU:{cpu}";
            Ram = $"RAM [Mb]: {phav}/{tot}";
        }

    }
}
