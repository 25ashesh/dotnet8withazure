using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using FrontLineCleaners.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontLineCleaners.Infrastructure.Repositories;
internal class ServicesRepository(FrontLineCleanersDbContext dbContext) : IServicesRepository
{
    public async Task<int> Create(Service service)
    {
        dbContext.Services.Add(service);
        await dbContext.SaveChangesAsync();
        return service.Id;
    }
    public async Task Delete(IEnumerable<Service> services) 
    {
        dbContext.Services.RemoveRange(services);
        await dbContext.SaveChangesAsync();
    }
}
