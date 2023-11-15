namespace apiTurnos.Models;

public partial class Turno
{
    public int IdTurno { get; set; }

    public int? IdServicio { get; set; }

    public DateTime? FechaTurno { get; set; }

    public string? HoraInicio { get; set; }

    public string? HoraFin { get; set; }

    public string? Estado { get; set; }

    public virtual Servicio? objServicio { get; set; }
}
