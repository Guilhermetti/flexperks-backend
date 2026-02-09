using FlexPerks.Application.Interfaces;
using FlexPerks.Application.Queries;
using FlexPerks.Domain.Enums;
using FlexPerks.Domain.Models;
using FlexPerks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPerks.Infrastructure.Repositories
{
    public class TimeClockEntryRepository : GenericRepository<TimeClockEntry>, ITimeClockEntryRepository
    {
        public TimeClockEntryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> ExistsSamePunch(int companyId, int employeeId, DateTime timestampUtc, TimeClockEntryType type)
        {
            return await _dbContext.TimeClockEntries
                .AnyAsync(TimeClockEntryQueries.SamePunch(companyId, employeeId, timestampUtc, type));
        }

        public async Task<List<TimeClockEntry>> ListByEmployeeAndPeriod(int companyId, int employeeId, DateTime fromUtc, DateTime toUtc)
        {
            return await _dbContext.TimeClockEntries
                .Where(TimeClockEntryQueries.ByEmployeeAndPeriod(companyId, employeeId, fromUtc, toUtc))
                .OrderBy(e => e.TimestampUtc)
                .ToListAsync();
        }
    }
}
