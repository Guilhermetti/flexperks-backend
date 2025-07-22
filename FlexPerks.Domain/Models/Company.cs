namespace FlexPerks.Domain.Models
{
    public class Company : BaseModel
    {
        public string Name { get; set; } = null!;
        public string TaxId { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
