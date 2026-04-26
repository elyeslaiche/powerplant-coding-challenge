public class Fuel
{
    public string Name { get; set; } = String.Empty;
    public double Cost { get; set; } = 0;

    public Fuel(string name, double cost)
    {
        this.Name = name;
        this.Cost = cost;
    }
}
