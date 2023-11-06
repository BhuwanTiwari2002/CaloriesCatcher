using System.Collections.Generic;
using Newtonsoft.Json;

public class RecipeModelEdamam
{
    public int From { get; set; }
    public int To { get; set; }
    public int Count { get; set; }
    public Links Links { get; set; }
    public List<Hit> Hits { get; set; }
}

public class Links
{
    public Next Next { get; set; }
}

public class Next
{
    public string Href { get; set; }
    public string Title { get; set; }
}

public class Hit
{
    public RecipeDetails Recipe { get; set; }
    public Link Self { get; set; }
}

public class RecipeDetails
{
    public string Image { get; set; }
    public double Calories { get; set; }
}

public class Link
{
    public string Title { get; set; }
    public string Href { get; set; }
}
