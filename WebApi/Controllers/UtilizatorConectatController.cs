﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOuri;
using WebApi.Servicii;

namespace WebApi.Controllers;

[ApiController]
[Route("api/utilizatori/conectare")]
public class UtilizatorConectatController : ControllerBase
{
    private readonly ServiciuConectare serviciuConectare;

    public UtilizatorConectatController(ServiciuConectare serviciuConectare)
    {
        this.serviciuConectare = serviciuConectare ?? throw new ArgumentNullException(nameof(serviciuConectare));
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UtilizatorConectatDTO>>> ObtineTotiUtilizatoriiConectati()
    {
        return await serviciuConectare.ObtineTotiUtilizatoriiConectati();
    }

    [Authorize]
    [HttpGet("{numeUtilizatorConectat}")]
    public async Task<IActionResult> ObtineUtilizatorConectat(string numeUtilizatorConectat)
    {
        return await serviciuConectare.ObtineUtilizatorConectat(numeUtilizatorConectat);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> ConecteazaUtilizator([FromBody] UtilizatorConectatDTO utilizatorConectatDTO)
    {
        return await serviciuConectare.ConecteazaUtilizator(utilizatorConectatDTO);
    }

    [Authorize]
    [HttpDelete("{numeUtilizatorConectat}")]
    public async Task<IActionResult> DeconecteazaUtilizator(string numeUtilizatorConectat)
    {
        return await serviciuConectare.DeconecteazaUtilizator(numeUtilizatorConectat);
    }
}
