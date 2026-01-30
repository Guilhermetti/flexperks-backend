using FlexPerks.Domain.Models;

namespace FlexPerks.Infrastructure.Data
{
    public static class DemoSeed
    {
        public static void Run(ApplicationDbContext db)
        {
            if (db.Users.Any()) return;

            var comp = new Company { Name = "Demo Co", TaxId = "00.000.000/0001-00" };
            var user = new User { Name = "Admin", Email = "admin@demo.co", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Company = comp };
            var cat1 = new BenefitCategory { Name = "Alimentação" };
            var cat2 = new BenefitCategory { Name = "Transporte" };
            var w1 = new PerksWallet { User = user, Category = cat1, Balance = 200 };
            var w2 = new PerksWallet { User = user, Category = cat2, Balance = 100 };
            db.AddRange(comp, user, cat1, cat2, w1, w2);
            db.SaveChanges();
        }
    }
}
