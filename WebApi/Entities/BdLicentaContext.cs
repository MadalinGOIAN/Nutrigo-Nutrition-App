using Microsoft.EntityFrameworkCore;

namespace WebApi.Entities;

public partial class BdLicentaContext : DbContext
{
    public BdLicentaContext()
    {
    }

    public BdLicentaContext(DbContextOptions<BdLicentaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alimente> Alimente { get; set; }

    public virtual DbSet<Istoric> Istoric { get; set; }

    public virtual DbSet<Utilizatori> Utilizatori { get; set; }

    public virtual DbSet<UtilizatoriConectati> UtilizatoriConectati { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alimente>(entity =>
        {
            entity.HasKey(e => e.Denumire).HasName("PRIMARY");

            entity.ToTable("alimente");

            entity.Property(e => e.Denumire)
                .HasMaxLength(30)
                .HasColumnName("denumire");
            entity.Property(e => e.Calorii).HasColumnName("calorii");
            entity.Property(e => e.CodBare)
                .HasMaxLength(13)
                .HasColumnName("cod_bare");
            entity.Property(e => e.Glucide).HasColumnName("glucide");
            entity.Property(e => e.Grasimi).HasColumnName("grasimi");
            entity.Property(e => e.Proteine).HasColumnName("proteine");
        });

        modelBuilder.Entity<Istoric>(entity =>
        {
            entity.HasKey(e => e.IstoricId).HasName("PRIMARY");

            entity.ToTable("istoric");

            entity.HasIndex(e => e.DenumireAliment, "fk_denumire_aliment_idx");

            entity.HasIndex(e => e.NumeUtilizator, "fk_nume_utilizator_idx");

            entity.Property(e => e.IstoricId).HasColumnName("istoric_id");
            entity.Property(e => e.CaloriiConsumate).HasColumnName("calorii_consumate");
            entity.Property(e => e.CantitateConsumata).HasColumnName("cantitate_consumata");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.DenumireAliment)
                .HasMaxLength(30)
                .HasColumnName("denumire_aliment");
            entity.Property(e => e.GlucideConsumate).HasColumnName("glucide_consumate");
            entity.Property(e => e.GrasimiConsumate).HasColumnName("grasimi_consumate");
            entity.Property(e => e.NumeUtilizator)
                .HasMaxLength(30)
                .HasColumnName("nume_utilizator");
            entity.Property(e => e.ProteineConsumate).HasColumnName("proteine_consumate");

            entity.HasOne(d => d.DenumireAlimentNavigation).WithMany(p => p.Istoric)
                .HasForeignKey(d => d.DenumireAliment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_denumire_aliment");

            entity.HasOne(d => d.NumeUtilizatorNavigation).WithMany(p => p.Istoric)
                .HasForeignKey(d => d.NumeUtilizator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_istoric_nume_utilizator");
        });

        modelBuilder.Entity<Utilizatori>(entity =>
        {
            entity.HasKey(e => e.NumeUtilizator).HasName("PRIMARY");

            entity.ToTable("utilizatori");

            entity.Property(e => e.NumeUtilizator)
                .HasMaxLength(30)
                .HasColumnName("nume_utilizator");
            entity.Property(e => e.Greutate).HasColumnName("greutate");
            entity.Property(e => e.HashParola)
                .HasMaxLength(45)
                .HasColumnName("hash_parola");
            entity.Property(e => e.Inaltime).HasColumnName("inaltime");
            entity.Property(e => e.NecesarCaloric).HasColumnName("necesar_caloric");
            entity.Property(e => e.NivelActivitateFizica).HasColumnName("nivel_activitate_fizica");
            entity.Property(e => e.NumeFamilie)
                .HasMaxLength(45)
                .HasColumnName("nume_familie");
            entity.Property(e => e.Prenume)
                .HasMaxLength(45)
                .HasColumnName("prenume");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sex");
            entity.Property(e => e.Varsta).HasColumnName("varsta");
        });

        modelBuilder.Entity<UtilizatoriConectati>(entity =>
        {
            entity.HasKey(e => e.NumeUtilizatorConectat).HasName("PRIMARY");

            entity.ToTable("utilizatori_conectati");

            entity.Property(e => e.NumeUtilizatorConectat)
                .HasMaxLength(30)
                .HasColumnName("nume_utilizator_conectat");
            entity.Property(e => e.HashParola)
                .HasMaxLength(45)
                .HasColumnName("hash_parola");

            entity.HasOne(d => d.NumeUtilizatorConectatNavigation).WithOne(p => p.UtilizatoriConectati)
                .HasForeignKey<UtilizatoriConectati>(d => d.NumeUtilizatorConectat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_nume_utilizator");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
