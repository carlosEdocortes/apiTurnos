using apiTurnos.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace apiTurnos.Controllers
{
    [EnableCors("reglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        public readonly PruebaNetContext objContext;

        public TurnosController(PruebaNetContext context)
        {
            objContext = context;
        }
        [HttpGet]
        [Route("listarTurnos")]
        public IActionResult listarTurnos()
        {
            List<Turno> listaTurnos = new List<Turno>();

            try
            {
                listaTurnos = objContext.Turnos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listaTurnos });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Error al listar los turnos ", response = ex.Message });
            }
        }

        [HttpGet]
        [Route("/obtenerTurnoServicio/{id_servicio:int}")]

        public ActionResult buscarTurno(int servicio)
        {
            Turno? objTurno = objContext.Turnos.Find(servicio);

            if (objTurno == null)
            {
                BadRequest("no se encuenntran turnos para el servicio solicitado");
            }


            try
            {
                objTurno = objContext.Turnos.Where(t => t.IdServicio == servicio).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = objTurno });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ocurrió un error al buscar el id ", response = ex.Message });
            }
        }

        [HttpPost]
        [Route("/crearTurno")]

        public IActionResult crearTurno([FromBody] Response objResponse)
        {
            List<Turno> turnos = new List<Turno>();
            string? mensaje = "";
            try
            {
                SqlConnection objConexion = (SqlConnection)objContext.Database.GetDbConnection();
                SqlCommand comando = objConexion.CreateCommand();
                objConexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "generarTurnos";
                comando.Parameters.Add("@fecha_inicio", System.Data.SqlDbType.DateTime).Value = objResponse.fecha_inicio;
                comando.Parameters.Add("@fecha_final", System.Data.SqlDbType.DateTime).Value = objResponse.fecha_fin;
                comando.Parameters.Add("@id_servicio", System.Data.SqlDbType.Int).Value = objResponse.IdServicio;
                SqlDataReader reader = comando.ExecuteReader();
                
                    while (reader.Read())
                    {
                        Turno objTurno = new Turno();
                    //objTurno.IdTurno = (int)reader["id_turno"];
                    try
                    {
                        if (reader["mensaje"].ToString() != null)
                        {
                            mensaje = (string)reader["mensaje"].ToString();
                            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = mensaje });
                        }
                    }
                    catch (Exception)
                    {
                        objTurno.IdServicio = (int)reader["id_servicio"];
                        objTurno.FechaTurno = (DateTime)reader["fecha_turno"];
                        objTurno.HoraInicio = (string)reader["hora_inicio"];
                        objTurno.HoraFin = (string)reader["hora_fin"];
                        objTurno.Estado = (string)reader["estado"];
                        turnos.Add(objTurno);
                    }
                            
                    
                        
                        
                    }
                    objConexion.Close();

                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = turnos });

                
                
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "error ", response = ex.Message });
            }

        }

    }
}
