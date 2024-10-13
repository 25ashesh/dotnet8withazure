using FrontLineCleaners.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Domain.Repositories;

public interface IServicesRepository
{
    Task<int> Create(Service service);
    Task Delete(IEnumerable<Service> services);
}
