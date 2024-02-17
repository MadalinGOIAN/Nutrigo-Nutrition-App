using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/alimente")]
public class AlimentController : ControllerBase
{
    private readonly ILogger<AlimentController> _logger;

    public AlimentController(ILogger<AlimentController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Aliment>> ObtineToateAlimentele()
    {
        return Ok(ListaAlimente.Alimente);
    }

    [HttpGet("filtru/denumire/{denumire}")]
    public ActionResult<Aliment> ObtineAlimentDupaDenumire(string denumire)
    {
        var aliment = ListaAlimente.Alimente.FirstOrDefault(a => a.Denumire == denumire);
        return aliment != null ? Ok(aliment) : NotFound("Alimentul cautat nu a fost gasit");
    }
    
    [HttpGet("filtru/codbare/{codBare}")]
    public ActionResult<Aliment> ObtineAlimentDupaCodBare(string codBare)
    {
        var aliment = ListaAlimente.Alimente.FirstOrDefault(a => a.CodBare == codBare);
        return aliment != null ? Ok(aliment) : NotFound("Alimentul cautat nu a fost gasit");
    }

    [HttpPost]
    public ActionResult<Aliment> AdaugaAliment([FromBody] Aliment aliment)
    {
        // Verificam ca alimentul sa nu existe deja in sistem
        if (ListaAlimente.Alimente.Exists(a => a.Denumire == aliment.Denumire))
        {
            return BadRequest("Acest aliment exista deja");
        }

        // Verificam ca datele introduse sa fie valide
        if (aliment == null)
        {
            return BadRequest("Datele introduse sunt invalide");
        }

        ListaAlimente.Alimente.Add(aliment);

        if (aliment.CodBare != null)
            CreatedAtAction(nameof(ObtineAlimentDupaCodBare), new { codBare = aliment.CodBare }, aliment);

        return CreatedAtAction(nameof(ObtineAlimentDupaDenumire), new { denumire = aliment.Denumire }, aliment);
    }

    [HttpPut("{denumire}")]
    public ActionResult<Aliment> ActualizeazaAliment(string denumire, [FromBody] Aliment alimentActualizat)
    {
        var alimentExistent = ListaAlimente.Alimente.FirstOrDefault(a => a.Denumire == denumire);

        // Verificam ca alimentul sa existe in sistem
        if (alimentExistent == null)
            return NotFound("Alimentul nu exista");

        // Verificam ca datele sa fie valide
        if (alimentActualizat == null)
        {
            return BadRequest("Datele introduse sunt invalide");
        }

        ActualizeazaAlimentExistent(alimentExistent, alimentActualizat);
        return Ok(alimentExistent);
    }

    private void ActualizeazaAlimentExistent(Aliment alimentExistent, Aliment alimentActualizat) 
    {
        alimentExistent.Denumire = alimentActualizat.Denumire;
        alimentExistent.CodBare = alimentExistent.CodBare == null ? null : alimentActualizat.CodBare;
        alimentExistent.Calorii = alimentActualizat.Calorii;
        alimentExistent.Grasimi = alimentActualizat.Grasimi;
        alimentExistent.Glucide = alimentActualizat.Glucide;
        alimentExistent.Proteine = alimentActualizat.Proteine;
    }

    [HttpDelete("{denumire}")]
    public IActionResult StergeAliment(string denumire)
    {
        var aliment = ListaAlimente.Alimente.FirstOrDefault(a => a.Denumire == denumire);

        if (aliment == null)
        {
            return NotFound("Alimentul nu a fost gasit");
        }

        ListaAlimente.Alimente.Remove(aliment);
        return Ok("Alimentul a fost sters");
    }
}
