using System;
using System.Collections.Generic;
using System.Text;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using System.Linq;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Handlers
{
    public class PromocodeCreatedHandler
    {
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;
        private readonly IRepository<PromoCode> _promoCodesRepository;

        public PromocodeCreatedHandler(IRepository<Preference> preferencesRepository,
                                       IRepository<Customer> customersRepository,
                                       IRepository<PromoCode> promoCodesRepository)
        {
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
            _promoCodesRepository = promoCodesRepository;
        }

        public void Handle(string ServiceInfo, 
                                 Guid PartnerId, 
                                 Guid PromoCodeId, 
                                 string PromoCode, 
                                 Guid PreferenceId, 
                                 string BeginDate, 
                                 string EndDate)
        {
            //Получаем предпочтение по имени
            var preference = _preferencesRepository.GetByIdAsync(PreferenceId).Result;

            if (preference == null)
                return;

            //  Получаем клиентов с этим предпочтением:
            var customers = _customersRepository.GetWhere(d => d.Preferences.Any(x => x.Preference.Id == preference.Id)).Result;

            var promocode = new PromoCode();
            promocode.Id = PromoCodeId;

            promocode.PartnerId = PartnerId;
            promocode.Code = PromoCode;
            promocode.ServiceInfo = ServiceInfo;

            promocode.BeginDate = DateTime.Parse(BeginDate);
            promocode.EndDate = DateTime.Parse(EndDate);

            promocode.Preference = preference;
            promocode.PreferenceId = preference.Id;

            promocode.Customers = new List<PromoCodeCustomer>();

            foreach (var item in customers)
            {
                promocode.Customers.Add(new PromoCodeCustomer()
                {

                    CustomerId = item.Id,
                    Customer = item,
                    PromoCodeId = promocode.Id,
                    PromoCode = promocode
                });
            };

            _promoCodesRepository.AddAsync(promocode);
        }
    }
}
