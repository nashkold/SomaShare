using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SomaShare.Models;

namespace SomaShare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<WantedAd> WantedAds { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision
            modelBuilder.Entity<Offer>()
                .Property(o => o.OfferAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Textbook>()
                .Property(t => t.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Advert>()
                .Property(a => a.MaxPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<WantedAd>()
                .Property(w => w.MaxPrice)
                .HasPrecision(18, 2);

            // Reviews
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewee)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Offers
            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Textbook)
                .WithMany(t => t.Offers)
                .HasForeignKey(o => o.TextbookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Buyer)
                .WithMany()
                .HasForeignKey(o => o.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Favourites 
            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.Textbook)
                .WithMany()
                .HasForeignKey(f => f.TextbookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favourite>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prevent a user favouriting the same textbook twice
            modelBuilder.Entity<Favourite>()
                .HasIndex(f => new { f.UserId, f.TextbookId })
                .IsUnique();
        }
    }
}