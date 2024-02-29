using MassTransit;
using SharedLibrary;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Consumers
{
    public class PromocodeCreatedConsumer : IConsumer<PromocodeCreated>
    {
        public Task Consume(ConsumeContext<PromocodeCreated> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
