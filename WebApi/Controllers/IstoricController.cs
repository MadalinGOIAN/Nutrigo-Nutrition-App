using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOuri;
using WebApi.Entities;

namespace WebApi.Controllers;

[ApiController]
[Route("api/istoric")]
public class IstoricController : ControllerBase
{
    private readonly BdLicentaContext contextBd;

    public IstoricController(BdLicentaContext contextBd)
    {
        this.contextBd = contextBd ?? throw new ArgumentNullException(nameof(contextBd));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IstoricDTO>>> ObtineTotIstoricul()
    {
        var istoric = await contextBd.Istoric.Select(
            i => new IstoricDTO
            {
                NumeUtilizator = i.NumeUtilizator,
                DenumireAliment = i.DenumireAliment,
                Data = i.Data,
                CantitateConsumata = i.CantitateConsumata,
                CaloriiConsumate = i.CaloriiConsumate,
                GrasimiConsumate = i.GrasimiConsumate,
                GlucideConsumate = i.GlucideConsumate,
                ProteineConsumate = i.ProteineConsumate
            })
            .ToListAsync();

        return istoric.Any()
            ? Ok(istoric)
            : NotFound("Nu exista inregistrari in istoric");
    }

    [HttpGet("{numeUtilizator}")]
    public async Task<ActionResult<IEnumerable<IstoricDTO>>> ObtineIstoricUtilizator(string numeUtilizator)
    {
        var istoric = await contextBd.Istoric.Select(
            i => new IstoricDTO
            {
                NumeUtilizator = i.NumeUtilizator,
                DenumireAliment = i.DenumireAliment,
                Data = i.Data,
                CantitateConsumata = i.CantitateConsumata,
                CaloriiConsumate = i.CaloriiConsumate,
                GrasimiConsumate = i.GrasimiConsumate,
                GlucideConsumate = i.GlucideConsumate,
                ProteineConsumate = i.ProteineConsumate
            })
            .ToListAsync();

        var istoricUtilizator = istoric.FindAll(i => i.NumeUtilizator.Equals(numeUtilizator));

        return istoricUtilizator.Any()
            ? Ok(istoricUtilizator)
            : NotFound("Nu exista inregistrari ale utilizatorului cautat");
    }

    [HttpGet("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<ActionResult<IstoricDTO>> ObtineInregistrare(
        string numeUtilizator,
        DateTime data,
        string denumireAliment)
    {
        var istoric = await contextBd.Istoric.Select(
            i => new IstoricDTO
            {
                NumeUtilizator = i.NumeUtilizator,
                DenumireAliment = i.DenumireAliment,
                Data = i.Data,
                CantitateConsumata = i.CantitateConsumata,
                CaloriiConsumate = i.CaloriiConsumate,
                GrasimiConsumate = i.GrasimiConsumate,
                GlucideConsumate = i.GlucideConsumate,
                ProteineConsumate = i.ProteineConsumate
            })
            .ToListAsync();

        var istoricUtilizator = istoric.FindAll(i => i.NumeUtilizator.Equals(numeUtilizator));
        var istoricUtilizatorDupaData = istoricUtilizator.FindAll(i => i.Data.Equals(data));
        var inregistrare = istoricUtilizatorDupaData.FirstOrDefault(i => i.DenumireAliment.Equals(denumireAliment));

        return inregistrare != null
            ? Ok(inregistrare)
            : NotFound("Inregistrarea nu a fost gasita");
    }

    [HttpPost]
    public async Task<ActionResult<IstoricDTO>> AdaugaInregistrare([FromBody] IstoricDTO istoricDTO)
    {
        // Verificam ca datele introduse sa fie valide
        if (istoricDTO == null)
            return BadRequest("Datele introduse sunt invalide");

        var istoricEntitate = new Istoric
        {
            NumeUtilizator = istoricDTO.NumeUtilizator,
            DenumireAliment = istoricDTO.DenumireAliment,
            Data = istoricDTO.Data,
            CantitateConsumata = istoricDTO.CantitateConsumata,
            CaloriiConsumate = istoricDTO.CaloriiConsumate,
            GrasimiConsumate = istoricDTO.GrasimiConsumate,
            GlucideConsumate = istoricDTO.GlucideConsumate,
            ProteineConsumate = istoricDTO.ProteineConsumate
        };

        contextBd.Istoric.Add(istoricEntitate);
        await contextBd.SaveChangesAsync();

        return Ok();
    }

    
    [HttpPut("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<ActionResult<IstoricDTO>> ActualizeazaInregistrare(
        string numeUtilizator,
        DateTime data,
        string denumireAliment,
        [FromBody] IstoricDTO istoricDTOActualizat)
    {
        var istoric = await contextBd.Istoric.ToListAsync();
        var inregistrareExistenta = istoric.FindAll(i => i.NumeUtilizator.Equals(numeUtilizator))
                                  .FindAll(i => i.Data.Equals(data))
                                  .FirstOrDefault(i => i.DenumireAliment.Equals(denumireAliment));

        ActualizeazaInregistrareExistenta(inregistrareExistenta, istoricDTOActualizat);
        await contextBd.SaveChangesAsync();

        return Ok(istoricDTOActualizat);
    }

    private void ActualizeazaInregistrareExistenta(Istoric inregistrareEntitate, IstoricDTO inregistrareDTOActualizata)
    {
        inregistrareEntitate.NumeUtilizator = inregistrareDTOActualizata.NumeUtilizator;
        inregistrareEntitate.DenumireAliment = inregistrareDTOActualizata.DenumireAliment;
        inregistrareEntitate.Data = inregistrareDTOActualizata.Data;
        inregistrareEntitate.CantitateConsumata = inregistrareDTOActualizata.CantitateConsumata;
        inregistrareEntitate.CaloriiConsumate = inregistrareDTOActualizata.CaloriiConsumate;
        inregistrareEntitate.GrasimiConsumate = inregistrareDTOActualizata.GrasimiConsumate;
        inregistrareEntitate.GlucideConsumate = inregistrareDTOActualizata.GlucideConsumate;
        inregistrareEntitate.ProteineConsumate = inregistrareDTOActualizata.ProteineConsumate;
    }

    [HttpDelete("{numeUtilizator}")]
    public async Task<IActionResult> StergeIstoricUtilizator(string numeUtilizator)
    {
        var istoric = await contextBd.Istoric.ToListAsync();
        var istoricUtilizator = istoric.FindAll(i => i.NumeUtilizator.Equals(numeUtilizator));

        if (!istoricUtilizator.Any())
            return NotFound("Nu s-au gasit inregistrari pentru acest utilizator");

        contextBd.Istoric.AttachRange(istoricUtilizator);
        contextBd.Istoric.RemoveRange(istoricUtilizator);
        await contextBd.SaveChangesAsync();

        return Ok("Istoricul utilizatorului a fost sters");
    }

    [HttpDelete("{numeUtilizator}/{data}/{denumireAliment}")]
    public async Task<IActionResult> StergeInregistrare(string numeUtilizator, DateTime data, string denumireAliment)
    {
        var istoric = await contextBd.Istoric.ToListAsync();
        var inregistrare = istoric.FindAll(i => i.NumeUtilizator.Equals(numeUtilizator))
                                  .FindAll(i => i.Data.Equals(data))
                                  .FirstOrDefault(i => i.DenumireAliment.Equals(denumireAliment));

        if (inregistrare == null)
            return NotFound("Inregistrarea nu a fost gasita");

        contextBd.Istoric.Attach(inregistrare);
        contextBd.Istoric.Remove(inregistrare);
        await contextBd.SaveChangesAsync();

        return Ok("Inregistrarea a fost stearsa");
    }
}
