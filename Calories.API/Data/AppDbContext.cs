using Calories.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Calories.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Calories.API.Models.Calories> Calories { get; set; }
        public DbSet<AspNetUsersDetail> AspNetUsersDetails { get; set; }
        public DbSet<RecipeIngredient>  ReceipeIngredients { get; set; }
        public DbSet<Calories.API.Models.Recipe> Recipes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
