using System.Numerics;

public class LoadCalcService : ILoadCalcService
{
    public List<LoadOutput> CalculateLoad(LoadInput input)
    {
        var fuelList = input.fuels;
        var pwrPlantList = input.powerplants;
        var loadToProduce = input.load;
        List<LoadOutput> outputList = new List<LoadOutput>();

        foreach (var plant in pwrPlantList)
        {
            plant.fuel = GetCostOfProd(plant, fuelList);
            if (plant.type == "windturbine")
            {
                plant.pmax *= fuelList["wind(%)"] / 100;
            }
            plant.TotalProductionCost = plant.fuel.Cost / plant.efficiency;
        }

        foreach (var plant in pwrPlantList.OrderBy(p => p.TotalProductionCost))
        {
            var output = CalculateLoadPerPlant(plant, loadToProduce);
            loadToProduce -= output.p;
            outputList.Add(output);
        }

        return outputList;

    }

    private LoadOutput CalculateLoadPerPlant(Plant plant, double load)
    {
        if (load > plant.pmax)
        {
            return new LoadOutput
            {
                name = plant.name,
                p = Math.Round(plant.pmax, 1)
            };
        }
        else
        {
            return new LoadOutput
            {
                name = plant.name,
                p = Math.Round(load, 1)
            };
        }
    }
    private Fuel GetCostOfProd(Plant plant, Dictionary<string, double> fuelList)
    {
        switch (plant.type)
        {
            case "gasfired":
                return new Fuel("gas(euro/MWh)", fuelList["gas(euro/MWh)"]);
            case "turbojet":
                return new Fuel("kerosine(euro/MWh)", fuelList["kerosine(euro/MWh)"]);
            default:
                return new Fuel("N/A", 0);
        }
    }
}