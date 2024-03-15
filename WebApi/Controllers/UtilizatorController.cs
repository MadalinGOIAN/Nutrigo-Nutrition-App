using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOuri;
using WebApi.Entities;
using WebApi.Tipuri;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori")]
public class UtilizatorController : ControllerBase
{
    private readonly BdLicentaContext contextBd;

    public UtilizatorController(BdLicentaContext contextBd)
    {
        this.contextBd = contextBd ?? throw new ArgumentNullException(nameof(contextBd));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilizatorDTO>>> ObtineTotiUtilizatorii()
    {
        var utilizatori = await contextBd.Utilizatori.Select(
            u => new UtilizatorDTO
            {
                NumeUtilizator = u.NumeUtilizator,
                HashParola = u.HashParola,
                Prenume = u.Prenume,
                NumeFamilie = u.NumeFamilie,
                Sex = u.Sex,
                Varsta = u.Varsta,
                Inaltime = u.Inaltime,
                Greutate = u.Greutate,
                NivelActivitateFizica = (NivelActivitateFizica)u.NivelActivitateFizica,
                NecesarCaloric = u.NecesarCaloric
            })
            .ToListAsync();

        return utilizatori.Any()
            ? Ok(utilizatori)
            : NotFound("Nu au fost gasiti utilizatori inregistrati");
    }

    [HttpGet("{numeUtilizator}")]
    public async Task<ActionResult<UtilizatorDTO>> ObtineUtilizator(string numeUtilizator)
    {
        var utilizator = await contextBd.Utilizatori.Select(
            u => new UtilizatorDTO
            {
                NumeUtilizator = u.NumeUtilizator,
                HashParola = u.HashParola,
                Prenume = u.Prenume,
                NumeFamilie = u.NumeFamilie,
                Sex = u.Sex,
                Varsta = u.Varsta,
                Inaltime = u.Inaltime,
                Greutate = u.Greutate,
                NivelActivitateFizica = (NivelActivitateFizica)u.NivelActivitateFizica,
                NecesarCaloric = u.NecesarCaloric
            })
            .FirstOrDefaultAsync(u => u.NumeUtilizator.Equals(numeUtilizator));

        return utilizator != null
            ? Ok(utilizator)
            : NotFound("Utilizatorul cautat nu a fost gasit");
    }

    [HttpPost]
    public async Task<ActionResult<UtilizatorDTO>> InregistreazaUtilizator([FromBody] UtilizatorDTO utilizatorDTO)
    {
        var utilizatoriExistenti = await contextBd.Utilizatori.ToListAsync();

        // Verificam ca numele de utilizator sa nu existe deja in sistem
        if (utilizatoriExistenti.Exists(u => u.NumeUtilizator.Equals(utilizatorDTO.NumeUtilizator)))
            return BadRequest("Acest nume de utilizator exista deja");

        // Verificam ca datele introduse sa fie valide
        if (utilizatorDTO == null)
            return BadRequest("Datele introduse sunt invalide");

        var utilizatorEntitate = new Utilizatori
        {
            NumeUtilizator = utilizatorDTO.NumeUtilizator,
            HashParola = utilizatorDTO.HashParola,
            Prenume = utilizatorDTO.Prenume,
            NumeFamilie = utilizatorDTO.NumeFamilie,
            Sex = utilizatorDTO.Sex,
            Varsta = utilizatorDTO.Varsta,
            Inaltime = utilizatorDTO.Inaltime,
            Greutate = utilizatorDTO.Greutate,
            NivelActivitateFizica = Convert.ToUInt32(utilizatorDTO.NivelActivitateFizica),
            NecesarCaloric = utilizatorDTO.NecesarCaloric
        };

        contextBd.Utilizatori.Add(utilizatorEntitate);
        await contextBd.SaveChangesAsync();
        
        return CreatedAtAction(
            nameof(ObtineUtilizator),
            new { numeUtilizator = utilizatorDTO.NumeUtilizator },
            utilizatorDTO);
    }

    [HttpPut("{numeUtilizator}")]
    public async Task<ActionResult<UtilizatorDTO>> ActualizeazaUtilizator(
        string numeUtilizator,
        [FromBody] UtilizatorDTO utilizatorDTOActualizat)
    {
        var utilizatorExistent = await contextBd.Utilizatori.FirstOrDefaultAsync(
            u => u.NumeUtilizator.Equals(numeUtilizator));

        // Verificam ca utilizatorul sa existe in sistem
        if (utilizatorExistent == null)
            return NotFound("Utilizatorul nu exista");

        // Verificam ca datele sa fie valide
        if (utilizatorDTOActualizat == null)
            return BadRequest("Datele introduse sunt invalide");

        ActualizeazaUtilizatorExistent(utilizatorExistent, utilizatorDTOActualizat);
        await contextBd.SaveChangesAsync();

        return Ok(utilizatorDTOActualizat);
    }

    private void ActualizeazaUtilizatorExistent(
        Utilizatori utilizatorEntitate,
        UtilizatorDTO utilizatorDTOActualizat)
    {
        utilizatorEntitate.NumeUtilizator = utilizatorDTOActualizat.NumeUtilizator;
        utilizatorEntitate.HashParola = utilizatorDTOActualizat.HashParola;
        utilizatorEntitate.Prenume = utilizatorDTOActualizat.Prenume;
        utilizatorEntitate.NumeFamilie = utilizatorDTOActualizat.NumeFamilie;
        utilizatorEntitate.Sex = utilizatorDTOActualizat.Sex;
        utilizatorEntitate.Varsta = utilizatorDTOActualizat.Varsta;
        utilizatorEntitate.Inaltime = utilizatorDTOActualizat.Inaltime;
        utilizatorEntitate.Greutate = utilizatorDTOActualizat.Greutate;
        utilizatorEntitate.NivelActivitateFizica = Convert.ToUInt32(utilizatorDTOActualizat.NivelActivitateFizica);
        utilizatorEntitate.NecesarCaloric = utilizatorDTOActualizat.NecesarCaloric;
    }

    [HttpDelete("{numeUtilizator}")]
    public async Task<IActionResult> StergeUtilizator(string numeUtilizator)
    {
        var utilizator = await contextBd.Utilizatori.FirstOrDefaultAsync(u => u.NumeUtilizator.Equals(numeUtilizator));

        if (utilizator == null)
            return NotFound("Utilizatorul nu a fost gasit");

        contextBd.Utilizatori.Attach(utilizator);
        contextBd.Utilizatori.Remove(utilizator);
        await contextBd.SaveChangesAsync();

        return Ok("Utilizatorul a fost sters");
    }
}