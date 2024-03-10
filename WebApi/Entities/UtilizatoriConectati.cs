namespace WebApi.Entities;

public partial class UtilizatoriConectati
{
    public string NumeUtilizatorConectat { get; set; } = null!;

    public string HashParola { get; set; } = null!;

    public virtual Utilizatori NumeUtilizatorConectatNavigation { get; set; } = null!;
}
