using Azure.Core;
using FrontLineCleaners.Domain.Constants;
using FrontLineCleaners.Domain.Entities;
using FrontLineCleaners.Domain.Repositories;
using FrontLineCleaners.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FrontLineCleaners.Infrastructure.Repositories;

internal class CleanersRepository(FrontLineCleanersDbContext dbContext) : ICleanersRepository
{
    public async Task<IEnumerable<Cleaner>> GetAllAsync()
    {
        var cleaners = await dbContext.Cleaners.ToListAsync();
        return cleaners;
    }
    public async Task<(IEnumerable<Cleaner>, int)> GetAllMatchingAsync(string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Cleaners
            .Where(x => searchPhraseLower == null || (x.Name.ToLower().Contains(searchPhraseLower)
                                                    || x.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy != null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Cleaner, object>>>
            {
                { nameof(Cleaner.Name), r => r.Name},
                { nameof(Cleaner.Description), r => r.Description},
                { nameof(Cleaner.Category), r => r.Category},
            };

            var selectedColumn = columnSelector[sortBy];
            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var cleaners = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        //PageSize = 5, PageNumber = 3 : Skip => PageSize * (PageNumber - 1) => 5 * (3-1) => 10

        //1      {...}
        //2      {...}
        //3      {...}
        //4      {...}
        //5      {...}
        //6      {...}
        //7      {...}
        //8      {...}
        //9      {...}
        //10     {...}
        //11     {...}

        return (cleaners, totalCount);
    }
    public async Task<Cleaner?> GetByIdAsync(int id)
    {
        var cleaner = await dbContext.Cleaners.Include(r => r.Services).FirstOrDefaultAsync(x => x.Id == id);
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
