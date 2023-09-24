using Calories.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Calories.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Calories.API.Models.Calories> Calories { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<RecipeFood> RecipeFoods { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserDetail>().HasData(new UserDetail
            {
                Id = 1,
                UserId = "Test",
                Height = 100,
                Weight = 100,
                DailyCalories = 1,
                BirthDate= DateTime.Now,
                Gender = "Male",
                CalorieId= 1,
                RecipeFoodId= 1,
            });
        }

    }
}
