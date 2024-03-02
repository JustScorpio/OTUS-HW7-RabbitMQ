using MassTransit;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using SharedLibrary;
using System.Threading.Tasks;
using System.Linq;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Handlers;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Consumers
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
            _promocodeCreatedHandler.Handle(context.Message.ServiceInfo,
                                            context.Message.PartnerId,
                                            context.Message.PromoCodeId,
                                            context.Message.PromoCode,
                                            context.Message.PreferenceId,
                                            context.Message.BeginDate,
                                            context.Message.EndDate);
        }
    }
}
