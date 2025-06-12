using BusinessLogic.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BusinessLogic.Models
{
    [DebuggerDisplay("{Id}, {Name}, {Difficulty}, {Cuisine}, {CaloriesPerServing}, {Rating}, {Tags}")]
    public class Recipe
    {
        [Key, Column("RecipeId")]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; }

        public string[] Ingredients { get; set; }

        public string[] Instructions { get; set; }

        public int PrepTimeMinutes { get; set; }

        public int CookTimeMinutes { get; set; }

        public int Servings { get; set; }

        public Difficulty Difficulty { get; set; }

        public string Cuisine { get; set; }

        public string[] MealType { get; set; }

        public float Rating { get; set; }

        public int CaloriesPerServing { get; set; }

        public int UserId { get; set; }

        public string[] Tags { get; set; }
    }
}
