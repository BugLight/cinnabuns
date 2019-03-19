using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace backend
{
    public class AppContext : DbContext
    {
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<MealCategory> MealCategories { get; set; }
        public DbSet<Meal> Meals { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Canteen>()
                .Property(c => c.AcceptCards)
                .HasConversion(new BoolToZeroOneConverter<Int16>());
        }
    }
}
