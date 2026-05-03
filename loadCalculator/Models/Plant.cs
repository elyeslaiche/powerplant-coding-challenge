public class Plant
{
    public string Name { get; set; } = String.Empty;
    public string Type { get; set; } = String.Empty;

    public double Efficiency { get; set; }

    public double Pmin { get; set; }
    public double Pmax { get; set; }

    public double ProductionCost { get; set; } = 0;

}