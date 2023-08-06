namespace Catalog.Dtos;
public class Calories
{
    public double value { get; set; }
    public string unit { get; set; }
    public ConfidenceRange95Percent confidenceRange95Percent { get; set; }
    public double standardDeviation { get; set; }
}

public class Carbs
{
    public double value { get; set; }
    public string unit { get; set; }
    public ConfidenceRange95Percent confidenceRange95Percent { get; set; }
    public double standardDeviation { get; set; }
}

public class ConfidenceRange95Percent
{
    public double min { get; set; }
    public double max { get; set; }
}

public class Fat
{
    public double value { get; set; }
    public string unit { get; set; }
    public ConfidenceRange95Percent confidenceRange95Percent { get; set; }
    public double standardDeviation { get; set; }
}

public class Protein
{
    public double value { get; set; }
    public string unit { get; set; }
    public ConfidenceRange95Percent confidenceRange95Percent { get; set; }
    public double standardDeviation { get; set; }
}

public class NutValues
{
    public int recipesUsed { get; set; }
    public Calories calories { get; set; }
    public Fat fat { get; set; }
    public Protein protein { get; set; }
    public Carbs carbs { get; set; }
}



