namespace FlexPerks.Domain.Models
{
    public class Employee : BaseModel
    {
        public int CompanyId { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string? Document { get; set; }
        public string? Registration { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee> DirectReports { get; set; } = new List<Employee>();

        public Company Company { get; set; } = null!;
    }
}
