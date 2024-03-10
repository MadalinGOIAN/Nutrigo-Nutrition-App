using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori/conectare")]
public class UtilizatorConectatController : ControllerBase
{
    private readonly ILogger<UtilizatorConectatController> _logger;

    public UtilizatorConectatController(ILogger<UtilizatorConectatController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UtilizatorConectat>> ObtineTotiUtilizatoriiConectati()
    {
        return ListaUtilizatoriConectati.UtilizatoriConectati.Any() ?
            Ok(ListaUtilizatoriConectati.UtilizatoriConectati) :
            NotFound("Nu exista utilizatori conectati");
    }

    [HttpGet("{numeUtilizatorConectat}")]
    public ActionResult<UtilizatorConectat> ObtineUtilizatorConectat(string numeUtilizatorConectat)
    {
        var utilizatorConectat = ListaUtilizatoriConectati.UtilizatoriConectati.FirstOrDefault(
            u => u.NumeUtilizatorConectat == numeUtilizatorConectat);

        return utilizatorConectat != null ?
            Ok($"{utilizatorConectat.NumeUtilizatorConectat}: Conectat") :
            NotFound("Utilizatorul cautat nu este conectat sau nu exista");
    }

    //[HttpPost]
    //public IActionResult ConecteazaUtilizator([FromBody] UtilizatorConectat utilizatorConectat)
    //{
    //    var utilizatorExistent = ListaUtilizatori.Utilizatori.FirstOrDefault(
    //        u => u.NumeUtilizator == utilizatorConectat.NumeUtilizatorConectat);

    //    // Verificam ca numele de utilizator sa fie introdus corect
    //    if (utilizatorExistent == null)
    //    {
    //        return BadRequest("Numele de utilizator introdus nu este corect");
    //    }

    //    // Verificam ca utilizatorul sa nu fie conectat deja
    //    if (ListaUtilizatoriConectati.UtilizatoriConectati.Exists(
    //        u => u.NumeUtilizatorConectat == utilizatorExistent.NumeUtilizator))
    //    {
    //        return BadRequest("Utilizatorul este deja conectat");
    //    }

    //    // Verificam ca parola sa fie introdusa corect
    //    if (!utilizatorExistent.HashParola.Equals(utilizatorConectat.HashParola))
    //    {
    //        return BadRequest("Parola introdusa nu este corecta");
    //    }
            
    //    ListaUtilizatoriConectati.UtilizatoriConectati.Add(utilizatorConectat);
    //    return Ok($"Utilizatorul {utilizatorConectat.NumeUtilizatorConectat} s-a conectat cu succes");
    //}

    [HttpDelete("{numeUtilizatorConectat}")]
    public IActionResult DeconecteazaUtilizator(string numeUtilizatorConectat)
    {
        var utilizatorConectat = ListaUtilizatoriConectati.UtilizatoriConectati.FirstOrDefault(
            u => u.NumeUtilizatorConectat == numeUtilizatorConectat);

        if (utilizatorConectat == null)
        {
            return NotFound("Utilizatorul cautat nu este conectat sau nu exista");
        }

        ListaUtilizatoriConectati.UtilizatoriConectati.Remove(utilizatorConectat);
        return Ok($"Utilizatorul {utilizatorConectat.NumeUtilizatorConectat} s-a deconectat cu succes");
    }
}
