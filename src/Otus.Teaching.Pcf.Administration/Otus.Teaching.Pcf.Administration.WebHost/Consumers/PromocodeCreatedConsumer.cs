using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.Core.Handlers;
using SharedLibrary;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Consumers
{
    public class PromocodeCreatedConsumer : IConsumer<PromocodeCreated>
    {
        private readonly PromocodeCreatedHandler _promocodeCreatedHandler;

        public PromocodeCreatedConsumer(PromocodeCreatedHandler promocodeCreatedHandler)
        {
            _promocodeCreatedHandler = promocodeCreatedHandler;
        }

        public async Task Consume(ConsumeContext<PromocodeCreated> context)
        {
            _promocodeCreatedHandler.Handle(context.Message.PartnerManagerId);
        }
    }
}
