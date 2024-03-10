namespace WebApi.Entities;

public partial class Utilizatori
{
    public string NumeUtilizator { get; set; } = null!;

    public string HashParola { get; set; } = null!;

    public string Prenume { get; set; } = null!;

    public string NumeFamilie { get; set; } = null!;

    public string Sex { get; set; } = null!;

    public uint Varsta { get; set; }

    public uint Inaltime { get; set; }

    public uint Greutate { get; set; }

    public uint NivelActivitateFizica { get; set; }

    public uint NecesarCaloric { get; set; }

    public virtual ICollection<Istoric> Istoric { get; set; } = new List<Istoric>();

    public virtual UtilizatoriConectati? UtilizatoriConectati { get; set; }
}
