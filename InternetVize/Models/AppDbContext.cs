using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetVize.Models
{
    public class AppDbContext: IdentityDbContext<User, Role, string>
    {
        public AppDbContext(DbContextOptions options): base(options) { }

        public DbSet<BuyerProfile> BuyerProfiles { get; set; }
        public DbSet<RentalProfile> RentalProfiles { get; set;}
        public DbSet<Rent> Rents { get; set;}
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Address> Addresses {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rent>()
                .HasOne(r => r.BuyerProfile)
                .WithOne(bp => bp.Rent)
                .HasForeignKey<Rent>(r => r.BuyerProfileId)
                .IsRequired(false);

            builder.Entity<BuyerProfile>()
                .HasMany(bp => bp.RentHistory)
                .WithOne(r => r.HistoryBuyerProfile)
                .HasForeignKey(e => e.HistoryBuyerProfileId)
                .IsRequired(false);

            builder.Entity<BuyerProfile>()
                    .HasOne(bp => bp.User)
                    .WithOne(user => user.BuyerProfile)
                    .HasForeignKey<BuyerProfile>(bp => bp.UserId)
                    .IsRequired(true);

            builder.Entity<RentalProfile>()
                    .HasOne(rp => rp.User)
                    .WithOne(user => user.RentalProfile)
                    .HasForeignKey<RentalProfile>(rp => rp.UserId)
                    .IsRequired(true);
        }
    }
}
