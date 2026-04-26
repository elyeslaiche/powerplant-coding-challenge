public class Plant
{
    public string name { get; set; }
    public string type { get; set; }

    public double efficiency { get; set; }

    public double pmin { get; set; }
    public double pmax { get; set; }

    public double TotalProductionCost { get; set; }
    public Fuel? fuel { get; set; }


}