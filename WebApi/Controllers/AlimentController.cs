using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOuri;
using WebApi.Servicii;

namespace WebApi.Controllers;

[ApiController]
[Route("api/alimente")]
public class AlimentController : ControllerBase
{
    private readonly ServiciuAliment serviciuAliment;

    public AlimentController(ServiciuAliment serviciuAliment)
    {
        this.serviciuAliment = serviciuAliment ?? throw new ArgumentNullException(nameof(serviciuAliment));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlimentDTO>>> ObtineToateAlimentele()
    {
        return await serviciuAliment.ObtineToateAlimentele();
    }

    [Authorize]
    [HttpGet("denumire/{denumire}")]
    public async Task<ActionResult<IEnumerable<AlimentDTO>>> ObtineAlimentDupaDenumire(string denumire)
    {
        return await serviciuAliment.ObtineAlimentDupaDenumire(denumire);
    }

    [Authorize]
    [HttpGet("codbare/{codBare}")]
    public async Task<ActionResult<AlimentDTO>> ObtineAlimentDupaCodBare(string codBare)
    {
        return await serviciuAliment.ObtineAlimentDupaCodBare(codBare);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AlimentDTO>> AdaugaAliment([FromBody] AlimentDTO alimentDTO)
    {
        return await serviciuAliment.AdaugaAliment(alimentDTO);
    }

    //[Authorize]
    //[HttpPut("{denumire}")]
    //public ActionResult<Aliment> ActualizeazaAliment(string denumire, [FromBody] Aliment alimentActualizat)
    //{
    //    return await serviciuAliment.ActualizeazaAliment(denumire, alimentActualizat);
    //}

    [Authorize]
    [HttpDelete("{denumire}")]
    public async Task<IActionResult> StergeAliment(string denumire)
    {
        return await serviciuAliment.StergeAliment(denumire);
    }
}
