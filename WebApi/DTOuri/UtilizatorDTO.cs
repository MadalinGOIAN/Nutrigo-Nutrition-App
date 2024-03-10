using WebApi.Tipuri;

namespace WebApi.DTOuri;

public class UtilizatorDTO
{
    public string? NumeUtilizator { get; set; }
    public string? HashParola { get; set; }
    public string? Prenume { get; set; }
    public string? NumeFamilie { get; set; }
    public string? Sex { get; set; }
    public uint? Varsta { get; set; }
    public uint? Inaltime { get; set; }
    public uint? Greutate { get; set; }
    public NivelActivitateFizica? NivelActivitateFizica { get; set; }
    public uint? NecesarCaloric { get; set; }
}
