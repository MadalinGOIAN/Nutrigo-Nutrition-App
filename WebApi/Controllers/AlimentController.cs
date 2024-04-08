using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOuri;
using WebApi.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("api/alimente")]
public class AlimentController : ControllerBase
{
    private readonly BdLicentaContext contextBd;

    public AlimentController(BdLicentaContext contextBd)
    {
        this.contextBd = contextBd ?? throw new ArgumentNullException(nameof(contextBd));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlimentDTO>>> ObtineToateAlimentele()
    {
        var alimente = await contextBd.Alimente.Select(
            a => new AlimentDTO
            {
                Denumire = a.Denumire,
                CodBare = a.CodBare,
                Calorii = a.Calorii,
                Grasimi = a.Grasimi,
                Glucide = a.Glucide,
                Proteine = a.Proteine
            })
            .ToListAsync();

        return alimente.Any()
            ? Ok(alimente)
            : NotFound("Nu au fost gasite alimente inregistrate");
    }

    [HttpGet("denumire/{denumire}")]
    public async Task<ActionResult<IEnumerable<AlimentDTO>>> ObtineAlimentDupaDenumire(string denumire)
    {
        var alimente = await contextBd.Alimente.Select(
            a => new AlimentDTO
            {
                Denumire = a.Denumire,
                CodBare = a.CodBare,
                Calorii = a.Calorii,
                Grasimi = a.Grasimi,
                Glucide = a.Glucide,
                Proteine = a.Proteine
            })
            .ToListAsync();

        var alimenteCautate = alimente.FindAll(a => a.Denumire.ToLower().Contains(denumire.ToLower()));

        return alimenteCautate.Any()
            ? Ok(alimenteCautate)
            : NotFound("Alimentele cautate nu a fost gasite");
    }

    [HttpGet("codbare/{codBare}")]
    public async Task<ActionResult<AlimentDTO>> ObtineAlimentDupaCodBare(string codBare)
    {
        var aliment = await contextBd.Alimente.Select(
            a => new AlimentDTO
            {
                Denumire = a.Denumire,
                CodBare = a.CodBare,
                Calorii = a.Calorii,
                Grasimi = a.Grasimi,
                Glucide = a.Glucide,
                Proteine = a.Proteine
            })
            .FirstOrDefaultAsync(a => a.CodBare.Equals(codBare));

        return aliment != null 
            ? Ok(aliment)
            : NotFound("Alimentul cautat nu a fost gasit");
    }

    [HttpPost]
    public async Task<ActionResult<AlimentDTO>> AdaugaAliment([FromBody] AlimentDTO alimentDTO)
    {
        var alimenteExistente = await contextBd.Alimente.ToListAsync();

        // Verificam ca alimentul sa nu existe deja in sistem
        if (alimenteExistente.Exists(a => a.Denumire.Equals(alimentDTO.Denumire)))
            return BadRequest("Acest aliment exista deja");

        // Verificam ca datele introduse sa fie valide
        if (alimentDTO == null)
            return BadRequest("Datele introduse sunt invalide");

        var alimentEntitate = new Alimente
        {
            Denumire = alimentDTO.Denumire,
            CodBare = alimentDTO.CodBare,
            Calorii = alimentDTO.Calorii,
            Grasimi = alimentDTO.Grasimi,
            Glucide = alimentDTO.Glucide,
            Proteine = alimentDTO.Proteine
        };

        contextBd.Alimente.Add(alimentEntitate);
        await contextBd.SaveChangesAsync();

        if (alimentDTO.CodBare != null)
            CreatedAtAction(
                nameof(ObtineAlimentDupaCodBare),
                new { codBare = alimentDTO.CodBare },
                alimentDTO);

        return CreatedAtAction(
            nameof(ObtineAlimentDupaDenumire),
            new { denumire = alimentDTO.Denumire },
            alimentDTO);
    }

    //[HttpPut("{denumire}")]
    //public ActionResult<Aliment> ActualizeazaAliment(string denumire, [FromBody] Aliment alimentActualizat)
    //{
    //    var alimentExistent = ListaAlimente.Alimente.FirstOrDefault(a => a.Denumire == denumire);

    //    // Verificam ca alimentul sa existe in sistem
    //    if (alimentExistent == null)
    //        return NotFound("Alimentul nu exista");

    //    // Verificam ca datele sa fie valide
    //    if (alimentActualizat == null)
    //    {
    //        return BadRequest("Datele introduse sunt invalide");
    //    }

    //    ActualizeazaAlimentExistent(alimentExistent, alimentActualizat);
    //    return Ok(alimentExistent);
    //}

    //private void ActualizeazaAlimentExistent(Aliment alimentExistent, Aliment alimentActualizat) 
    //{
    //    alimentExistent.Denumire = alimentActualizat.Denumire;
    //    alimentExistent.CodBare = alimentExistent.CodBare == null ? null : alimentActualizat.CodBare;
    //    alimentExistent.Calorii = alimentActualizat.Calorii;
    //    alimentExistent.Grasimi = alimentActualizat.Grasimi;
    //    alimentExistent.Glucide = alimentActualizat.Glucide;
    //    alimentExistent.Proteine = alimentActualizat.Proteine;
    //}

    [HttpDelete("{denumire}")]
    public async Task<IActionResult> StergeAliment(string denumire)
    {
        var aliment = await contextBd.Alimente.FirstOrDefaultAsync(a => a.Denumire.Equals(denumire));

        if (aliment == null)
            return NotFound("Alimentul nu a fost gasit");

        contextBd.Alimente.Attach(aliment);
        contextBd.Alimente.Remove(aliment);
        await contextBd.SaveChangesAsync();

        return Ok("Alimentul a fost sters");
    }
}
