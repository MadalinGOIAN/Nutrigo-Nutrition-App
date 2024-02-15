using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori")]
public class UtilizatorController : ControllerBase
{
    private readonly ILogger<UtilizatorController> _logger;

    public UtilizatorController(ILogger<UtilizatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Utilizator>> ObtineTotiUtilizatorii()
    {
        return Ok(ListaUtilizatori.Utilizatori);
    }

    [HttpGet("{numeUtilizator}")]
    public ActionResult<Utilizator> ObtineUtilizator(string numeUtilizator)
    {
        var utilizator = ListaUtilizatori.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == numeUtilizator);
        return utilizator != null ? Ok(utilizator) : NotFound("Utilizatorul cautat nu a fost gasit");
    }

    [HttpPost]
    public ActionResult<Utilizator> InregistreazaUtilizator([FromBody] Utilizator utilizator)
    {
        // Verificam ca numele de utilizator sa nu existe deja in sistem
        if (ListaUtilizatori.Utilizatori.Exists(u => u.NumeUtilizator == utilizator.NumeUtilizator))
            return BadRequest("Acest nume de utilizator exista deja");

        // Verificam ca datele introduse sa fie valide
        if (utilizator == null)
        {
            return BadRequest("Datele introduse sunt invalide");
        }

        ListaUtilizatori.Utilizatori.Add(utilizator);
        return CreatedAtAction(
            nameof(ObtineUtilizator),
            new { numeUtilizator = utilizator.NumeUtilizator },
            utilizator);
    }

    [HttpPut("{numeUtilizator}")]
    public ActionResult<Utilizator> ActualizeazaUtilizator(
        string numeUtilizator,
        [FromBody] Utilizator utilizatorActualizat)
    {
        var utilizatorExistent = ListaUtilizatori.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == numeUtilizator);

        // Verificam ca utilizatorul sa existe in sistem
        if (utilizatorExistent == null)
            return NotFound("Utilizatorul nu exista");

        // Verificam ca datele sa fie valide
        if (utilizatorActualizat == null)
        {
            return BadRequest("Datele introduse sunt invalide");
        }

        ActualizeazaUtilizatorExistent(utilizatorExistent, utilizatorActualizat);
        return Ok(utilizatorExistent);
    }

    private void ActualizeazaUtilizatorExistent(Utilizator utilizatorExistent, Utilizator utilizatorActualizat)
    {
        utilizatorExistent.NumeUtilizator = utilizatorActualizat.NumeUtilizator;
        utilizatorExistent.HashParola = utilizatorActualizat.HashParola;
        utilizatorExistent.Prenume = utilizatorActualizat.Prenume;
        utilizatorExistent.NumeFamilie = utilizatorActualizat.NumeFamilie;
        utilizatorExistent.Sex = utilizatorActualizat.Sex;
        utilizatorExistent.Varsta = utilizatorActualizat.Varsta;
        utilizatorExistent.Inaltime = utilizatorActualizat.Inaltime;
        utilizatorExistent.Greutate = utilizatorActualizat.Greutate;
        utilizatorExistent.NivelActivitateFizica = utilizatorActualizat.NivelActivitateFizica;
        utilizatorExistent.NecesarCaloric = utilizatorActualizat.NecesarCaloric;
    }

    [HttpDelete("{numeUtilizator}")]
    public ActionResult StergeUtilizator(string numeUtilizator)
    {
        var utilizator = ListaUtilizatori.Utilizatori.FirstOrDefault(u => u.NumeUtilizator == numeUtilizator);

        if (utilizator == null)
        {
            return NotFound("Utilizatorul nu a fost gasit");
        }

        ListaUtilizatori.Utilizatori.Remove(utilizator);
        return Ok("Utilizatorul a fost sters");
    }
}