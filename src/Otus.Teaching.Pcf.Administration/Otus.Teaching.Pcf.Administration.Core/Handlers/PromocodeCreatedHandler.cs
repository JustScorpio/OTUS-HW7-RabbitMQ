using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.Teaching.Pcf.Administration.Core.Handlers
{
    public class PromocodeCreatedHandler
    {
        private readonly IRepository<Employee> _employeeRepository;

        public PromocodeCreatedHandler(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Handle(Guid PartnerManagerId)
        {
            var employee = _employeeRepository.GetByIdAsync(PartnerManagerId).Result;

            if (employee == null)
                return;

            employee.AppliedPromocodesCount++;

            _employeeRepository.UpdateAsync(employee);
        }
    }
}
