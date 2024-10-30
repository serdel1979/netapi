using ApiNet.DTOs;
using ApiNet.Model;

namespace ApiNet.Services
{
    public interface IServiceEquipo
    {
        Task<EquipoRespuestaDTO> GetById(int Id);
        Task<EquipoRespuestaDTO> GetEquipoByName(string Name);
        Task<List<EquipoRespuestaDTO>> GetEquipoList();
        Task Update(EquipoNuevoDTO equipo, int Id);
        Task Delete(int Id);
        Task AgregaEquipo(EquipoNuevoDTO equipo);
    }
}
