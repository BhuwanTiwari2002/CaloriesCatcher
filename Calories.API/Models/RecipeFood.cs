using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class RecipeFood
    {
        [Key]
        public int RecipeFoodId { get; set; }
        [ForeignKey("FoodId")]
        public virtual int FoodId { get; set; }
        [ForeignKey("RecipeId")]
        public virtual int RecipeId { get; set; }
        [ForeignKey("UserId")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(450)]
        public virtual string UserId { get; set; } = string.Empty;
    }
}
