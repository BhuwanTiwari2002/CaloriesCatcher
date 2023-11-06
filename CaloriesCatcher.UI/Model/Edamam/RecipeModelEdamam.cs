using System.Collections.Generic;
using Newtonsoft.Json;

public class RecipeModelEdamam
{
    public int From { get; set; }
    public int To { get; set; }
    public int Count { get; set; }
    public Links Links { get; set; }
    public List<Hit> Hits { get; set; }
    public bool isSuccess { get; set; }
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
    public string Label { get; set; }
    public string Image { get; set; }
    public double Calories { get; set; }
    public List<Ingredients> ingredients { get; set; }
}

public class Ingredients
{
    public string text { get; set; }

    public double quantity { get; set; }

    public string measure { get; set; }

    public string food { get; set; }

    public double weight { get; set; }

    public string foodCategory { get; set; }

    public string foodId { get; set; }

    public string image { get; set; }   
}

public class Link
{
    public string Title { get; set; }
    public string Href { get; set; }
}
