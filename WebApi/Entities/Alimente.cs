namespace WebApi.Entities;

public partial class Alimente
{
    public string Denumire { get; set; } = null!;

    public string? CodBare { get; set; }

    public int Calorii { get; set; }

    public float Grasimi { get; set; }

    public float Glucide { get; set; }

    public float Proteine { get; set; }

    public virtual ICollection<Istoric> Istoric { get; set; } = new List<Istoric>();
}
