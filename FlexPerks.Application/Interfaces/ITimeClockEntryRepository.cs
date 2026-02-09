using FlexPerks.Domain.Enums;
using FlexPerks.Domain.Models;

namespace FlexPerks.Application.Interfaces
{
    public interface ITimeClockEntryRepository : IAsyncRepository<TimeClockEntry>
    {
        Task<bool> ExistsSamePunch(int companyId, int employeeId, DateTime timestampUtc, TimeClockEntryType type);
        Task<List<TimeClockEntry>> ListByEmployeeAndPeriod(int companyId, int employeeId, DateTime fromUtc, DateTime toUtc);
    }
}
