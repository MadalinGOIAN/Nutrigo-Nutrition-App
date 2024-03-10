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
    private readonly DbLicentaContext contextBd;

    public UtilizatorController(DbLicentaContext contextBd)
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

        if (utilizatori.Any())
            return Ok(utilizatori);

        return NotFound("Nu au fost gasiti utilizatori inregistrati");
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

        return utilizator != null ? Ok(utilizator) : NotFound("Utilizatorul cautat nu a fost gasit");
    }

    //[HttpPost]
    //public async Task<ActionResult<UtilizatorDTO>> InregistreazaUtilizator([FromBody] UtilizatorDTO utilizatorDTO)
    //{
    //    // Verificam ca numele de utilizator sa nu existe deja in sistem
    //    if (await contextBd.Utilizatori.ToListAsync().Result.Exists(u => u.NumeUtilizator.Equals(utilizatorDTO.NumeUtilizator)))
    //        return BadRequest("Acest nume de utilizator exista deja");

    //    // Verificam ca datele introduse sa fie valide
    //    if (utilizator == null)
    //    {
    //        return BadRequest("Datele introduse sunt invalide");
    //    }

    //    ListaUtilizatori.Utilizatori.Add(utilizator);
    //    return CreatedAtAction(
    //        nameof(ObtineUtilizator),
    //        new { numeUtilizator = utilizator.NumeUtilizator },
    //        utilizator);
    //}

    //[HttpPut("{numeUtilizator}")]
    //public ActionResult<Utilizator> ActualizeazaUtilizator(
    //    string numeUtilizator,
    //    [FromBody] Utilizator utilizatorActualizat)
    //{
    //    var utilizatorExistent = ListaUtilizatori.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == numeUtilizator);

    //    // Verificam ca utilizatorul sa existe in sistem
    //    if (utilizatorExistent == null)
    //        return NotFound("Utilizatorul nu exista");

    //    // Verificam ca datele sa fie valide
    //    if (utilizatorActualizat == null)
    //    {
    //        return BadRequest("Datele introduse sunt invalide");
    //    }

    //    ActualizeazaUtilizatorExistent(utilizatorExistent, utilizatorActualizat);
    //    return Ok(utilizatorExistent);
    //}

    //private void ActualizeazaUtilizatorExistent(Utilizator utilizatorExistent, Utilizator utilizatorActualizat)
    //{
    //    utilizatorExistent.NumeUtilizator = utilizatorActualizat.NumeUtilizator;
    //    utilizatorExistent.HashParola = utilizatorActualizat.HashParola;
    //    utilizatorExistent.Prenume = utilizatorActualizat.Prenume;
    //    utilizatorExistent.NumeFamilie = utilizatorActualizat.NumeFamilie;
    //    utilizatorExistent.Sex = utilizatorActualizat.Sex;
    //    utilizatorExistent.Varsta = utilizatorActualizat.Varsta;
    //    utilizatorExistent.Inaltime = utilizatorActualizat.Inaltime;
    //    utilizatorExistent.Greutate = utilizatorActualizat.Greutate;
    //    utilizatorExistent.NivelActivitateFizica = utilizatorActualizat.NivelActivitateFizica;
    //    utilizatorExistent.NecesarCaloric = utilizatorActualizat.NecesarCaloric;
    //}

    //[HttpDelete("{numeUtilizator}")]
    //public IActionResult StergeUtilizator(string numeUtilizator)
    //{
    //    var utilizator = ListaUtilizatori.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == numeUtilizator);

    //    if (utilizator == null)
    //    {
    //        return NotFound("Utilizatorul nu a fost gasit");
    //    }

    //    ListaUtilizatori.Utilizatori.Remove(utilizator);
    //    return Ok("Utilizatorul a fost sters");
    //}
}