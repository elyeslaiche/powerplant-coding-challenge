using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace loadCalculator.Controllers;

[ApiController]

[Route("[controller]")]
public class LoadController : ControllerBase
{
    private ILoadCalcService _calcService;

    public LoadController(ILoadCalcService calcService)
    {
        _calcService = calcService;

    }

    [HttpPost]
    public IActionResult ComputeLoad([FromBody] LoadInput input)
    {
        try
        {
            //valider avec un if l'input genre faire une méthode static qui check que tous les champs sont ok.
            return Ok(new
            {
                data = _calcService.CalculateLoad(input)
            });
        }catch(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return BadRequest(ex);
        }
    }
}