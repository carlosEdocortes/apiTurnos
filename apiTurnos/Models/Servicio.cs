using System.Text.Json.Serialization;

namespace apiTurnos.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public int? IdComercio { get; set; }

    public string? NomServicio { get; set; }

    public int? HoraApertura { get; set; }

    public int? HoraCierre { get; set; }

    public int? Duracion { get; set; }


    public virtual Comercio? objComercio { get; set; }
    [JsonIgnore]
    public virtual ICollection<Turno> objTurnos { get; set; } = new List<Turno>();
}
