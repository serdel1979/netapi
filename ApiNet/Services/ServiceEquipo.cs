using ApiNet.DTOs;
using ApiNet.Exceptions;
using ApiNet.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiNet.Services
{
    public class ServiceEquipo : IServiceEquipo
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ServiceEquipo(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task AgregaEquipo(EquipoNuevoDTO equipo)
        {
            var buscado = await _context.Equipos.FirstOrDefaultAsync(eq => eq.Nombre.Equals(equipo.Nombre));
            if (buscado != null)
            {
                throw new EquipoExiste(buscado.Nombre);
            }
            _context.Equipos.Add(_mapper.Map<Equipo>(equipo));
            await _context.SaveChangesAsync(); // Usar SaveChangesAsync
        }


        public async Task Delete(int Id)
        {
            var buscado = await _context.Equipos.FirstOrDefaultAsync(eq => eq.Id == Id);
            if (buscado != null)
            {
                throw new EquipoInexistente(Id);
            }
            _context.Equipos.Remove(buscado);
            _context.SaveChanges();
        }

        public async Task<EquipoRespuestaDTO> GetById(int Id)
        {
            var equipoEntity = await _context.Equipos.FirstOrDefaultAsync(eq => eq.Id == Id);

            if (equipoEntity == null)
            {
                throw new EquipoInexistente(Id);
            }

            EquipoRespuestaDTO equipo = _mapper.Map<EquipoRespuestaDTO>(equipoEntity);
            return equipo;
        }


        public async Task<EquipoRespuestaDTO> GetEquipoByName(string Name)
        {
            var equipoEntity = await _context.Equipos.FirstOrDefaultAsync(eq => eq.Nombre.Equals(Name));

            if (equipoEntity == null)
            {
                throw new NombreEquipoInexistente(Name);
            }

            EquipoRespuestaDTO equipo = _mapper.Map<EquipoRespuestaDTO>(equipoEntity);
            return equipo;
        }

        public async Task<List<EquipoRespuestaDTO>> GetEquipoList()
        {
            return _mapper.Map<List<EquipoRespuestaDTO>>(await _context.Equipos.OrderBy(eq=>eq.Id).ToListAsync());
        }

        public async Task Update(EquipoNuevoDTO equipo, int Id)
        {
            var existe = await _context.Equipos.FirstOrDefaultAsync(eq=>eq.Id == Id);
            if (existe == null)
            {
                throw new EquipoInexistente(Id);
            }
            existe.Nombre = equipo.Nombre;
            existe.Descripcion = equipo.Descripcion;
            _context.Equipos.Update(existe);
            _context.SaveChanges();
        }
    }
}
