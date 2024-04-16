using Microsoft.AspNetCore.Mvc;
using WebApi.DTOuri;
using WebApi.Servicii;

namespace WebApi.Controllers;

[ApiController]
[Route("api/istoric")]
public class IstoricController : ControllerBase
{
    private readonly ServiciuIstoric serviciuIstoric;

    public IstoricController(ServiciuIstoric serviciuIstoric)
    {
        this.serviciuIstoric = serviciuIstoric ?? throw new ArgumentNullException(nameof(serviciuIstoric));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IstoricDTO>>> ObtineTotIstoricul()
    {
        return await serviciuIstoric.ObtineTotIstoricul();
    }

    [HttpGet("{numeUtilizator}")]
    public async Task<ActionResult<IEnumerable<IstoricDTO>>> ObtineIstoricUtilizator(string numeUtilizator)
    {
        return await serviciuIstoric.ObtineIstoricUtilizator(numeUtilizator);
    }

    [HttpGet("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<ActionResult<IstoricDTO>> ObtineInregistrare(
        string numeUtilizator,
        DateTime data,
        string denumireAliment)
    {
        return await serviciuIstoric.ObtineInregistrare(numeUtilizator, data, denumireAliment);
    }

    [HttpPost]
    public async Task<ActionResult<IstoricDTO>> AdaugaInregistrare([FromBody] IstoricDTO istoricDTO)
    {
        return await serviciuIstoric.AdaugaInregistrare(istoricDTO);
    }

    
    [HttpPut("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<ActionResult<IstoricDTO>> ActualizeazaInregistrare(
        string numeUtilizator,
        DateTime data,
        string denumireAliment,
        [FromBody] IstoricDTO istoricDTOActualizat)
    {
        return await serviciuIstoric.ActualizeazaInregistrare(
            numeUtilizator, data, denumireAliment, istoricDTOActualizat);
    }

    [HttpDelete("{numeUtilizator}")]
    public async Task<IActionResult> StergeIstoricUtilizator(string numeUtilizator)
    {
        return await serviciuIstoric.StergeIstoricUtilizator(numeUtilizator);
    }

    [HttpDelete("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<IActionResult> StergeInregistrare(string numeUtilizator, DateTime data, string denumireAliment)
    {
        return await serviciuIstoric.StergeInregistrare(numeUtilizator, data, denumireAliment);
    }
}
