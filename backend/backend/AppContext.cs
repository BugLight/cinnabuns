using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using backend.Models;

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
    }
}
