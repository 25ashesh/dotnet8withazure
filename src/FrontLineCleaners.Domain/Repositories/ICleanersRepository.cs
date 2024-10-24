using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;

namespace FrontLineCleaners.Domain.Repositories;

public interface ICleanersRepository
{
    Task<IEnumerable<Cleaner>> GetAllAsync();
    Task<(IEnumerable<Cleaner>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<Cleaner?> GetByIdAsync(int id);
    Task<int> Create(Cleaner cleaner);
    Task Delete(Cleaner cleaner);
    Task SaveChanges();
}
