namespace FlexPerks.Domain.Models
{
    public class PerkTransaction : BaseModel
    {
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = null!;

        public PerksWallet Wallet { get; set; } = null!;
    }
}
