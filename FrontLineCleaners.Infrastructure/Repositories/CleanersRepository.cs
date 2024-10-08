using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using FrontLineCleaners.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FrontLineCleaners.Infrastructure.Repositories;

internal class CleanersRepository(FrontLineCleanersDbContext dbContext) : ICleanersRepository
{
    public async Task<IEnumerable<Cleaner>> GetAllAsync()
    {
        var cleaners = await dbContext.Cleaners.ToListAsync();
        return cleaners;
    }
    public async Task<Cleaner?> GetByIdAsync(int id)
    {
        var cleaner = await dbContext.Cleaners.Include(r=>r.Services).FirstOrDefaultAsync(x=>x.Id == id);
        return cleaner;
    }

    public async Task<int> Create(Cleaner cleaner)
    {
        dbContext.Cleaners.Add(cleaner);
        await dbContext.SaveChangesAsync();
        return cleaner.Id;
    }
    public async Task Delete(Cleaner cleaner) 
    {
        dbContext.Cleaners.Remove(cleaner);
        await dbContext.SaveChangesAsync();
    }
    public async Task SaveChanges()
    {
        await dbContext.SaveChangesAsync();
    }
}
