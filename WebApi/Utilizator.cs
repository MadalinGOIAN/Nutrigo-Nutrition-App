namespace WebApi;

public class Utilizator
{
    public string? NumeUtilizator {  get; set; }
    public string? HashParola { get; set; }
    public string? Prenume { get; set; }
    public string? NumeFamilie { get; set; }
    public char? Sex { get; set; }
    public int? Varsta { get; set; }
    public int? Inaltime { get; set; }
    public int? Greutate { get; set; }
    public NivelActivitateFizica? NivelActivitateFizica { get; set; }
    public int? NecesarCaloric { get; set; }
}

public enum NivelActivitateFizica
{
    Sedentar,
    Mediu,
    Moderat,
    Intens,
    FoarteIntens
}
