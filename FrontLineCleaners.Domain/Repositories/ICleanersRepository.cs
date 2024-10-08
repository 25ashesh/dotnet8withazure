using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Domain.Repositories;

public interface ICleanersRepository
{
    Task<IEnumerable<Cleaner>> GetAllAsync();
    Task<Cleaner?> GetByIdAsync(int id);
    Task<int> Create(Cleaner cleaner);
    Task Delete(Cleaner cleaner);
    Task SaveChanges();
}
