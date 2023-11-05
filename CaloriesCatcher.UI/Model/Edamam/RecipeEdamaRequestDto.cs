namespace CaloriesCatcher.UI.Model.Edamam;

public class RecipeEdamaRequestDto
{
    public string ApplicationId { get; set; } = string.Empty;
    public string ApplicationKey { get; set; } = string.Empty;
    public string ReceipeName { get; set; } = string.Empty;
    public string Url { get; set; } 
    public RecipeEdamaRequestDto(string receipeName)
    {
        ReceipeName = receipeName;
        ApplicationId = "fcd09cde";
        ApplicationKey = "d3bbf2b058110a9f3c4ac7b0234590d0";
        Url = "https://api.edamam.com/api/recipes/v2?type=public";
    }
}