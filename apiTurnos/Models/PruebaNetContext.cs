using Microsoft.EntityFrameworkCore;

namespace apiTurnos.Models;

public partial class PruebaNetContext : DbContext
{
    public PruebaNetContext()
    {
    }

    public PruebaNetContext(DbContextOptions<PruebaNetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comercio> Comercios { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Comercio>(entity =>
        {
            entity.HasKey(e => e.IdComercio).HasName("PK__comercio__077F3D8592826AE9");

            entity.ToTable("comercios");

            entity.Property(e => e.IdComercio).HasColumnName("id_comercio");
            entity.Property(e => e.AforoMaximo).HasColumnName("aforo_maximo");
            entity.Property(e => e.NomComercio)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nom_comercio");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__servicio__6FD07FDC3B90299C");

            entity.ToTable("servicios");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Duracion).HasColumnName("duracion");
            entity.Property(e => e.HoraApertura).HasColumnName("hora_apertura");
            entity.Property(e => e.HoraCierre).HasColumnName("hora_cierre");
            entity.Property(e => e.IdComercio).HasColumnName("id_comercio");
            entity.Property(e => e.NomServicio)
                .IsUnicode(false)
                .HasColumnName("nom_servicio");

            entity.HasOne(d => d.objComercio).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdComercio)
                .HasConstraintName("FK__servicios__id_co__25869641");
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.IdTurno).HasName("PK__turnos__C68E73978F740577");

            entity.ToTable("turnos");

            entity.Property(e => e.IdTurno).HasColumnName("id_turno");
            entity.Property(e => e.Estado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaTurno)
                .HasColumnType("date")
                .HasColumnName("fecha_turno");
            entity.Property(e => e.HoraFin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

            entity.HasOne(d => d.objServicio).WithMany(p => p.objTurnos)
                .HasForeignKey(d => d.IdServicio)
                .HasConstraintName("FK__turnos__id_servi__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
