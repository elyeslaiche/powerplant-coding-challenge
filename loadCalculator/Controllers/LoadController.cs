using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace loadCalculator.Controllers;
[ApiController]

[Route("[controller]")]
public class LoadController: ControllerBase
{
    private ILoadCalcService _calcService;

    public LoadController(ILoadCalcService calcService)
    {
        _calcService = calcService;

    }

    [HttpPost]
    public IActionResult ComputeLoad([FromBody] LoadInput input)
    {   
        return Ok(new {
            data = _calcService.CalculateLoad(input)
        });
    }
}