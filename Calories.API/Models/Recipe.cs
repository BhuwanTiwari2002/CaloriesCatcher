using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string RecipeName { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual string UserId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Calories { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [MaxLength]
        public string ImagePath { get; set; }
    }
}
