using FlexPerks.Domain.Enums;
using FlexPerks.Domain.Models;
using System.Linq.Expressions;

namespace FlexPerks.Application.Queries
{
    public static class TimeClockEntryQueries
    {
        public static Expression<Func<TimeClockEntry, bool>> SamePunch(int companyId, int employeeId, DateTime tsUtc, TimeClockEntryType type)
            => e => e.CompanyId == companyId && e.EmployeeId == employeeId && e.TimestampUtc == tsUtc && e.Type == type;

        public static Expression<Func<TimeClockEntry, bool>> ByEmployeeAndPeriod(int companyId, int employeeId, DateTime fromUtc, DateTime toUtc)
            => e => e.CompanyId == companyId
                 && e.EmployeeId == employeeId
                 && e.TimestampUtc >= fromUtc
                 && e.TimestampUtc < toUtc;
    }
}
