using Microsoft.AspNetCore.Components.Web.Virtualization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calories.API.Models
{
    public class RecipeIngredient
    {
        [Key]
        public int RecipeIngredientId { get; set; }
        [ForeignKey("RecipeId")]
        public virtual int RecipeId { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(150)]
        public string IngredientName { get; set; }
        [Column(TypeName = "DECIMAL")]
        public double Quantity { get; set; }

    }
}
