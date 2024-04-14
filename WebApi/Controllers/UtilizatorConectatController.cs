using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOuri;
using WebApi.Entities;
using WebApi.Utilitati;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori/conectare")]
public class UtilizatorConectatController : ControllerBase
{
    private readonly BdLicentaContext contextBd;

    public UtilizatorConectatController(BdLicentaContext contextBd)
    {
        this.contextBd = contextBd ?? throw new ArgumentNullException(nameof(contextBd));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilizatorConectatDTO>>> ObtineTotiUtilizatoriiConectati()
    {
        var utilizatoriConectati = await contextBd.UtilizatoriConectati.Select(
            u => new UtilizatorConectatDTO
            {
                NumeUtilizatorConectat = u.NumeUtilizatorConectat,
                Parola = u.HashParola
            })
            .ToListAsync();

        return utilizatoriConectati.Any()
            ? Ok(utilizatoriConectati)
            : NotFound("Nu exista utilizatori conectati");
    }

    [HttpGet("{numeUtilizatorConectat}")]
    public async Task<IActionResult> ObtineUtilizatorConectat(string numeUtilizatorConectat)
    {
        var utilizatorConectat = await contextBd.UtilizatoriConectati.Select(
            u => new UtilizatorConectatDTO
            {
                NumeUtilizatorConectat = u.NumeUtilizatorConectat,
                Parola = u.HashParola
            })
            .FirstOrDefaultAsync(u => u.NumeUtilizatorConectat.Equals(numeUtilizatorConectat));

        return utilizatorConectat != null
            ? Ok($"{utilizatorConectat.NumeUtilizatorConectat}: Conectat")
            : NotFound("Utilizatorul cautat nu este conectat sau nu exista");
    }

    [HttpPost]
    public async Task<IActionResult> ConecteazaUtilizator([FromBody] UtilizatorConectatDTO utilizatorConectatDTO)
    {
        var utilizatorExistent = await contextBd.Utilizatori.Select(
            u => new UtilizatorDTO
            {
                NumeUtilizator = u.NumeUtilizator,
                Parola = u.HashParola
            })
            .FirstOrDefaultAsync(u => u.NumeUtilizator.Equals(utilizatorConectatDTO.NumeUtilizatorConectat));

        // Verificam ca numele de utilizator sa fie introdus corect
        if (utilizatorExistent == null)
            return BadRequest("Numele de utilizator introdus nu este corect");

        var utilizatoriConectati = await contextBd.UtilizatoriConectati.Select(
            u => new UtilizatorConectatDTO
            {
                NumeUtilizatorConectat = u.NumeUtilizatorConectat,
                Parola = u.HashParola
            })
            .ToListAsync();

        // Verificam ca utilizatorul sa nu fie conectat deja
        if (utilizatoriConectati.Exists(u => u.NumeUtilizatorConectat == utilizatorExistent.NumeUtilizator))
            return BadRequest("Utilizatorul este deja conectat");

        // Verificam ca parola sa fie introdusa corect
        if (!EncriptorParola.VerificaParola(utilizatorConectatDTO.Parola, utilizatorExistent.Parola))
            return BadRequest("Parola introdusa nu este corecta");

        var utilizatorConectatEntitate = new UtilizatoriConectati
        {
            NumeUtilizatorConectat = utilizatorConectatDTO.NumeUtilizatorConectat,
            HashParola = utilizatorExistent.Parola
        };

        contextBd.UtilizatoriConectati.Add(utilizatorConectatEntitate);
        await contextBd.SaveChangesAsync();

        CreatedAtAction(
            nameof(ObtineUtilizatorConectat),
            new { numeUtilizatorConectat = utilizatorConectatDTO.NumeUtilizatorConectat },
            utilizatorConectatDTO);

        return Ok($"Utilizatorul {utilizatorConectatDTO.NumeUtilizatorConectat} s-a conectat cu succes");
    }

    [HttpDelete("{numeUtilizatorConectat}")]
    public async Task<IActionResult> DeconecteazaUtilizator(string numeUtilizatorConectat)
    {
        var utilizatorConectat = await contextBd.UtilizatoriConectati.FirstOrDefaultAsync(
            u => u.NumeUtilizatorConectat.Equals(numeUtilizatorConectat));

        if (utilizatorConectat == null)
            return NotFound("Utilizatorul cautat nu este conectat sau nu exista");

        contextBd.UtilizatoriConectati.Attach(utilizatorConectat);
        contextBd.UtilizatoriConectati.Remove(utilizatorConectat);
        await contextBd.SaveChangesAsync();

        return Ok($"Utilizatorul {utilizatorConectat.NumeUtilizatorConectat} s-a deconectat cu succes");
    }
}
