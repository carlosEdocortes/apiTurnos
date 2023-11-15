namespace apiTurnos.Models;

public partial class Comercio
{
    public int IdComercio { get; set; }

    public string NomComercio { get; set; } = null!;

    public int AforoMaximo { get; set; }


    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
