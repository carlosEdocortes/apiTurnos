using apiTurnos.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiTurnos.Controllers
{
    [EnableCors("reglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComercioController : ControllerBase
    {
        public readonly PruebaNetContext db_context;
        public ComercioController(PruebaNetContext context)
        {
            db_context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult listarComercio()
        {
            List<Comercio> listaComercios = new List<Comercio>();
            try
            {
                listaComercios = db_context.Comercios.Include(c => c.Servicios).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listaComercios });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al listar los comercios ", response = ex.Message });
            }

        }

        [HttpGet]
        [Route("obtener/{id_comercio:int}")]
        public IActionResult obternerId(int id_comercio)
        {
            Comercio? objComercio = db_context.Comercios.Find(id_comercio);

            if (objComercio == null)
            {
                return BadRequest("No se ha encontrado el comercio ");
            }

            try
            {
                objComercio = db_context.Comercios.Include(s => s.Servicios).Where(w => w.IdComercio.Equals(id_comercio)).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = objComercio });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al obtener el  comercio ", response = ex.Message });
            }

        }

        [HttpPost]
        [Route("crearComercio")]
        public IActionResult crearComercio([FromBody] Comercio objComercio)
        {

            try
            {
                db_context.Comercios.Add(objComercio);
                db_context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok " });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al obtener el  comercio ", response = ex.Message });

            }

        }

        [HttpPut]
        [Route("editarComercio")]

        public IActionResult editarComercio([FromBody] Comercio comercio)
        {
            Comercio? objComercio = db_context.Comercios.Find(comercio.IdComercio);

            if (objComercio == null)
            {
                return BadRequest("Error no se encuentra el comercio para editar ");
            }

            try
            {

                objComercio.NomComercio = comercio.NomComercio is null ? objComercio.NomComercio : comercio.NomComercio;
                objComercio.AforoMaximo = comercio.AforoMaximo == 0 ? objComercio.AforoMaximo : comercio.AforoMaximo;


                db_context.Comercios.Update(objComercio);
                db_context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok " });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al obtener el  comercio ", response = ex.Message });

            }
        }

        [HttpDelete]
        [Route("eliminarComercio/{id_comercio:int}")]

        public IActionResult eliminarComercio(int id_comercio)
        {
            Comercio? objComercio = db_context.Comercios.Find(id_comercio);

            if (objComercio == null)
            {
                BadRequest(" El comercio ingresado no existe valide de nuevo ");
            }

            try
            {
                objComercio = db_context.Comercios.Where(d => d.IdComercio == id_comercio).FirstOrDefault();
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                db_context.Comercios.Remove(objComercio);
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                db_context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Comercio eliminado de forma correcta " });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al eliminar el comercio ", response = ex.Message });
            }
        }

    }
}
