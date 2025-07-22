namespace FlexPerks.Domain.Models
{
    public class PerksWallet : BaseModel
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Balance { get; set; }

        public User User { get; set; } = null!;
        public BenefitCategory Category { get; set; } = null!;
        public ICollection<PerkTransaction> Transactions { get; set; } = new List<PerkTransaction>();
    }
}
