using backend_template.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend_template.Database
{
    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext(DbContextOptions options) : base(options)
        {
        }

        // Entities
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<FavouriteAdvertisement> FavouriteAdvertisements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Advertisement>()
                .Property(x => x.Title)
                .HasMaxLength(256)
                .IsRequired();

            modelBuilder.Entity<FavouriteAdvertisement>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<FavouriteAdvertisement>()
                .Property(x => x.AdvertisementId)
                .IsRequired();

            modelBuilder.Entity<FavouriteAdvertisement>()
                .Property(x => x.UserEmail)
                .IsRequired();
        }
    }
}


