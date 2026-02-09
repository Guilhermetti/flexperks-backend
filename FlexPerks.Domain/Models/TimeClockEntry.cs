using FlexPerks.Domain.Enums;

namespace FlexPerks.Domain.Models
{
    public class TimeClockEntry : BaseModel
    {
        public int CompanyId { get; set; }
        public int EmployeeId { get; set; }

        public DateTime TimestampUtc { get; set; }

        public TimeClockEntryType Type { get; set; }
        public TimeClockEntrySource Source { get; set; }

        public string? Note { get; set; }

        public Company Company { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
