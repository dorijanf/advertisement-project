using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext(DbContextOptions options) : base(options)
        {
        }

        // Entities
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<FavoriteAdvertisement> FavoriteAdvertisements { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Advertisement>()
                .Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<FavoriteAdvertisement>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<FavoriteAdvertisement>()
                .Property(x => x.AdvertisementId)
                .IsRequired();

            modelBuilder.Entity<FavoriteAdvertisement>()
                .Property(x => x.UserEmail)
                .IsRequired();

            modelBuilder.Entity<EmailQueue>()
                .HasKey(x => x.Id);
        }
    }
}


