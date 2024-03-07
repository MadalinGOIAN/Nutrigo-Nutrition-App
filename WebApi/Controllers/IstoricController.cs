using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/istoric")]
public class IstoricController : ControllerBase
{
    private readonly ILogger<IstoricController> _logger;

    public IstoricController(ILogger<IstoricController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Istoric>> ObtineTotIstoricul()
    {
        return Ok(ListaIstoric.Istoric);
    }
    
    [HttpGet("{numeUtilizator}")]
    public ActionResult<IEnumerable<Istoric>> ObtineIstoricUtilizator(string numeUtilizator)
    {
        var istoricUtilizator = ListaIstoric.Istoric.FindAll(i => i.NumeUtilizator == numeUtilizator);
        return istoricUtilizator.Any() ? Ok(istoricUtilizator) : NotFound("Nu s-a gasit istoricul utilizatorului");
    }
    
    [HttpGet("{numeUtilizator}/{data}")]
    public ActionResult<IEnumerable<Istoric>> ObtineIstoricUtilizatorDupaData(string numeUtilizator, DateOnly data)
    {
        var istoricUtilizator = ListaIstoric.Istoric.FindAll(i => i.NumeUtilizator == numeUtilizator);

        if (!istoricUtilizator.Any())
        {
            return NotFound("Nu s-a gasit istoricul utilizatorului");
        }

        var istoricUtilizatorDupaData = istoricUtilizator.FindAll(i => i.Data == data);

        return istoricUtilizatorDupaData.Any()
            ? Ok(istoricUtilizatorDupaData)
            : NotFound("Nu s-a gasit istoricul utilizatorului la data cautata");
    }
}
