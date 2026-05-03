public class LoadInput
{
    public double Load { get; set; }
    public Dictionary<string,double> Fuels { get; set; } = new Dictionary<string, double>();
    public List<Plant> Powerplants { get; set; } = new List<Plant>();
}