using RenderingCommon;
using System;
using System.ServiceModel;

namespace RenderApp
{
    public class ServiceConsumer
    {
        public ServiceConsumer()
        {
            var address = new Uri("http://localhost:4000/IContract");
            var binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = RenderPreferences.MaxBufferSize;
            binding.MaxBufferSize = RenderPreferences.MaxBufferSize;
            binding.MaxReceivedMessageSize = RenderPreferences.MaxBufferSize;
            var contract = typeof(IContract);
            var host = new ServiceHost(typeof(Service));
            host.AddServiceEndpoint(contract, binding, address);
            host.Open();
        }
    }
}
