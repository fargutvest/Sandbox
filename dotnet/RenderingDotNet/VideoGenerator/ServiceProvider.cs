using RenderingCommon;
using System;
using System.ServiceModel;

namespace VideoGenerator
{
    public class ServiceProvider
    {
        public IContract Channel { get; }

        public ServiceProvider()
        {
            var address = new Uri("http://localhost:4000/IContract");
            var binding = new BasicHttpBinding();
            binding.MaxBufferPoolSize = RenderPreferences.MaxBufferSize;
            binding.MaxBufferSize = RenderPreferences.MaxBufferSize;
            binding.MaxReceivedMessageSize = RenderPreferences.MaxBufferSize;
            var endpoint = new EndpointAddress(address);
            var factory = new ChannelFactory<IContract>(binding, endpoint);
            Channel = factory.CreateChannel();
        }
    }
}
