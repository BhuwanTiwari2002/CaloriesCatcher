using Calories.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Calories.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Calories.API.Models.Calories> Calories { get; set; }
        public DbSet<AspNetUsersDetail> AspNetUsersDetails { get; set; }
        public DbSet<RecipeFood> RecipeFoods { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
