using ApiNet.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private static List<Equipo> equipos = new List<Equipo>();

        [HttpGet("test")]
        public ActionResult GetTest()
        {
            return Ok("Get Ok!!!");
        }

        [HttpPost]
        public ActionResult PostTest(Equipo equipo)
        {
            if (equipo == null || string.IsNullOrWhiteSpace(equipo.Descripción) || string.IsNullOrWhiteSpace(equipo.Nombre))
            {
                return BadRequest("El equipo, su Descripción, y su Nombre son obligatorios.");
            }
            equipos.Add(equipo);
            return Ok("Equipo agregado");
        }

        [HttpGet("todos")]
        public ActionResult GetAll()
        {
            return Ok(equipos);
        }

        [HttpGet("GetByName/{Name}")]
        public ActionResult GetByName(string Name)
        {
            var equipo = equipos.FirstOrDefault(e => string.Equals(e.Nombre, Name, StringComparison.OrdinalIgnoreCase));
            if (equipo != null)
            {
                return Ok(equipo);
            }
            return NotFound("Equipo no encontrado.");
        }
    }
}



