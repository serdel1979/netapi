using ApiNet.DTOs;
using ApiNet.Exceptions;
using ApiNet.Model;
using ApiNet.Services;
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
        private readonly IServiceEquipo _serviceEquipo;

        public ApiController(IServiceEquipo serviceEquipo)
        {
            this._serviceEquipo = serviceEquipo;
        }



        [HttpPost("Nuevo")]
        public async Task<ActionResult> Nuevo(EquipoNuevoDTO equipo)
        {
            try
            {
                await _serviceEquipo.AgregaEquipo(equipo); // Agrega await aquí
                return Ok();
            }
            catch (EquipoExiste ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("Listar")]
        public async Task<ActionResult<List<EquipoRespuestaDTO>>> GetAll()
        {
            try
            {
                var equipos = await _serviceEquipo.GetEquipoList();
                return Ok(equipos); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult<EquipoRespuestaDTO>> UpdateEquipo(int id, [FromBody] EquipoNuevoDTO equipoDto)
        {
            try
            {
                await _serviceEquipo.Update(equipoDto, id);
               

                return Ok($"Equipo con id {id} actualizado"); 
            }
            catch (EquipoInexistente ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EquipoExiste ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la consulta");
            }
        }

        [HttpDelete("Borra/{Id}")]
        public async Task<ActionResult> Borra(int Id)
        {
            try
            {
                await _serviceEquipo.Delete(Id);
                return Ok($"Equipo con id {Id} eliminado!!!");

            }
            catch (EquipoInexistente ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la consulta");
            }
        }

        [HttpGet("GetById/{Id}")]
        public async Task<ActionResult<EquipoRespuestaDTO>> GetById(int Id)
        {
            try
            {
                var equipo = await _serviceEquipo.GetById(Id);
                return Ok(equipo); 
            }
            catch (EquipoInexistente ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la consulta");
            }
        }


        [HttpGet("GetByName/{Name}")]
        public async Task<ActionResult<EquipoRespuestaDTO>> GetGByName(string Name)
        {
            try
            {
                var equipo = await _serviceEquipo.GetEquipoByName(Name);
                return Ok(equipo);
            }
            catch (NombreEquipoInexistente ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Error en la consulta");
            }
        }

    }
}



