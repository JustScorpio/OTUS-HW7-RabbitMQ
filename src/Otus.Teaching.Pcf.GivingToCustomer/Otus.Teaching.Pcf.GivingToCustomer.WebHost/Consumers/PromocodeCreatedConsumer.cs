using MassTransit;
using SharedLibrary;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Consumers
{
    public class PromocodeCreatedConsumer : IConsumer<PromocodeCreated>
    {
        public Task Consume(ConsumeContext<PromocodeCreated> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
