using MassTransit;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using SharedLibrary;
using System.Threading.Tasks;
using System.Linq;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Consumers
{
    public class PromocodeCreatedConsumer : IConsumer<PromocodeCreated>
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromocodeCreatedConsumer(IRepository<PromoCode> promoCodesRepository,
                                        IRepository<Preference> preferencesRepository,
                                        IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;

        }

        public async Task Consume(ConsumeContext<PromocodeCreated> context)
        {
            var request = new GivePromoCodeRequest()
            {
                PromoCodeId = context.Message.PromoCodeId,
                PartnerId = context.Message.PartnerId,
                BeginDate = context.Message.BeginDate,
                EndDate = context.Message.EndDate,
                PreferenceId = context.Message.PreferenceId,
                PromoCode = context.Message.PromoCode,
                ServiceInfo = context.Message.ServiceInfo,
            };

            //Получаем предпочтение по имени
            var preference = await _preferencesRepository.GetByIdAsync(request.PreferenceId);

            if (preference == null)
                return;

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository.GetWhere(d => d.Preferences.Any(x => x.Preference.Id == preference.Id));

            PromoCode promoCode = PromoCodeMapper.MapFromModel(request, preference, customers);

            await _promoCodesRepository.AddAsync(promoCode);
        }
    }
}
