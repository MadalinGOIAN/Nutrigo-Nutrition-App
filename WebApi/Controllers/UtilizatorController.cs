using Microsoft.AspNetCore.Mvc;
using WebApi.DTOuri;
using WebApi.Servicii;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori")]
public class UtilizatorController : ControllerBase
{
    private readonly ServiciuUtilizator serviciuUtilizator;
    private readonly ServiciuInregistrare serviciuInregistrare;

    public UtilizatorController(ServiciuUtilizator serviciuUtilizator, ServiciuInregistrare serviciuInregistrare)
    {
        this.serviciuUtilizator = serviciuUtilizator ?? throw new ArgumentNullException(nameof(serviciuUtilizator));
        this.serviciuInregistrare = serviciuInregistrare
            ?? throw new ArgumentNullException(nameof(serviciuInregistrare));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilizatorDTO>>> ObtineTotiUtilizatorii()
    {
        return await serviciuUtilizator.ObtineTotiUtilizatorii();
    }

    [HttpGet("{numeUtilizator}")]
    public async Task<ActionResult<UtilizatorDTO>> ObtineUtilizator(string numeUtilizator)
    {
        return await serviciuUtilizator.ObtineUtilizator(numeUtilizator);
    }

    [HttpPost]
    public async Task<ActionResult<UtilizatorDTO>> InregistreazaUtilizator([FromBody] UtilizatorDTO utilizatorDTO)
    {
        return await serviciuInregistrare.InregistreazaUtilizator(utilizatorDTO);
    }

    [HttpPut("{numeUtilizator}")]
    public async Task<ActionResult<UtilizatorDTO>> ActualizeazaUtilizator(
        string numeUtilizator,
        [FromBody] UtilizatorDTO utilizatorDTOActualizat)
    {
        return await serviciuUtilizator.ActualizeazaUtilizator(numeUtilizator, utilizatorDTOActualizat);
    }

    [HttpDelete("{numeUtilizator}")]
    public async Task<IActionResult> StergeUtilizator(string numeUtilizator)
    {
        return await serviciuUtilizator.StergeUtilizator(numeUtilizator);
    }
}