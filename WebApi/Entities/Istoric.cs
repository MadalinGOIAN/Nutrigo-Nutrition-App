namespace WebApi.Entities;

public partial class Istoric
{
    public int IstoricId { get; set; }

    public string NumeUtilizator { get; set; } = null!;

    public string DenumireAliment { get; set; } = null!;

    public DateTime Data { get; set; }

    public float CantitateConsumata { get; set; }

    public int CaloriiConsumate { get; set; }

    public float GrasimiConsumate { get; set; }

    public float GlucideConsumate { get; set; }

    public float ProteineConsumate { get; set; }

    public virtual Alimente DenumireAlimentNavigation { get; set; } = null!;

    public virtual Utilizatori NumeUtilizatorNavigation { get; set; } = null!;
}
