

using Microsoft.VisualBasic;

public class LoadCalcService : ILoadCalcService
{
    private Dictionary<FuelType, string> _fuelTypeParsingDictionary = new Dictionary<FuelType, string>
    {
        {FuelType.gasfired , "gas(euro/MWh)" },
        {FuelType.turbojet , "kerosine(euro/MWh)" },
        {FuelType.windturbine , "wind(%)" },
    };

    public LoadOutput CalculateLoad(LoadInput input)
    {
        var fuelList = input.Fuels;
        var pwrPlantList = input.Powerplants;
        var loadToProduce = input.Load;

        LoadOutput output = new LoadOutput();

        if (fuelList == null)
            throw new Exception("Could not parse Fuel prices");

        if (pwrPlantList == null)
            throw new Exception("Could not parse Powerplants");

        if (double.IsNaN(loadToProduce))
            throw new Exception("Load cannot be null");

        foreach (var plant in pwrPlantList)
        {
            GetCostOfProd(plant, fuelList);
        }

        foreach (var plant in pwrPlantList.OrderBy(p => p.ProductionCost))
        {
            (var calulatedLoad, loadToProduce) = CalculateLoadPerPlant(plant, loadToProduce);
            output.LoadList.Add(calulatedLoad);
        }

        return output;
    }

    private (LoadCalculated, double) CalculateLoadPerPlant(Plant plant, double load)
    {
        if (load > plant.Pmax)
        {
            return (new LoadCalculated
            {
                Name = plant.Name,
                P = Math.Round(plant.Pmax, 1)
            }, load - Math.Round(plant.Pmax, 1));
        }

        return (new LoadCalculated
        {
            Name = plant.Name,
            P = Math.Round(load, 1)
        }, 0);
    }
    private void GetCostOfProd(Plant plant, Dictionary<string, double> fuelList)
    {
        FuelType plantType;
        if (Enum.TryParse<FuelType>(plant.Type, out plantType))
        {
            var fuelTypeInInput = _fuelTypeParsingDictionary[plantType];

            if (plantType == FuelType.windturbine)
                plant.Pmax *= fuelList[fuelTypeInInput] / 100;
            else
            {
                plant.ProductionCost = fuelList[fuelTypeInInput] / plant.Efficiency;
                
                if (plantType == FuelType.gasfired)
                    plant.ProductionCost += 0.3 * fuelList["co2(euro/ton)"]; //Adding 0.3 Ton of CO2 per Mwh to the cost of prod

            }

        }

    }
}