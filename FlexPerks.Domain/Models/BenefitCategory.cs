namespace FlexPerks.Domain.Models
{
    public class BenefitCategory : BaseModel
    {
        public string Name { get; set; } = null!;

        public ICollection<PerksWallet> Wallets { get; set; } = new List<PerksWallet>();
    }
}
