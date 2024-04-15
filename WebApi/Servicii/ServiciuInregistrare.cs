using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DTOuri;
using WebApi.Entities;
using WebApi.Utilitati;

namespace WebApi.Servicii;

public class ServiciuInregistrare : ControllerBase
{
    private readonly BdLicentaContext contextBd;

    public ServiciuInregistrare(BdLicentaContext contextBd)
    {
        this.contextBd = contextBd ?? throw new ArgumentNullException(nameof(contextBd));
    }

    public async Task<ActionResult<UtilizatorDTO>> InregistreazaUtilizator(UtilizatorDTO utilizatorDTO)
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
            HashParola = EncriptorParola.CripteazaParola(utilizatorDTO.Parola),
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

        return Created($"api/utilizatori/{utilizatorDTO.NumeUtilizator}", utilizatorDTO);
    }
}
