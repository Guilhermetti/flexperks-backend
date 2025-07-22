namespace FlexPerks.Domain.Models
{
    public class User : BaseModel
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public Company Company { get; set; } = null!;
        public ICollection<PerksWallet> Wallets { get; set; } = new List<PerksWallet>();
    }
}
