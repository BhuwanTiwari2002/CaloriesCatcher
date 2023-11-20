using KitchenComfort.Web.Models.Utility;
using static KitchenComfort.Web.Models.Utility.StaticType;

namespace CaloriesCatcher.UI.Model
{
    public class EdamamRequestDto
    {
        public string app_id { get; set; }
        public string app_key { get; set; }
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ingredient { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public EdamamRequestDto(string ingredient)
        {
            app_id = "d40eca51";
            app_key = "192fe1bbaeed7914764ddd4a4348e10c";
            this.ingredient = ingredient;
            Url = "https://api.edamam.com/api/nutrition-data";
        }
        public EdamamRequestDto()
        {
            app_id = "d40eca51";
            app_key = "192fe1bbaeed7914764ddd4a4348e10c"; 
            Url = "https://api.edamam.com/api/nutrition-details";
        }
    }
}
