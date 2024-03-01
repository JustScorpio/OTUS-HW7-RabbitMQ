using MassTransit;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using SharedLibrary;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.Administration.WebHost.Consumers
{
    public class PromocodeCreatedConsumer : IConsumer<PromocodeCreated>
    {
        private readonly IRepository<Employee> _employeeRepository;

        public PromocodeCreatedConsumer(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Consume(ConsumeContext<PromocodeCreated> context)
        {
            var employee = await _employeeRepository.GetByIdAsync(context.Message.PartnerManagerId);

            if (employee == null)
                return;

            employee.AppliedPromocodesCount++;

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
