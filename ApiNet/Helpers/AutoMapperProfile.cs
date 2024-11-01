using ApiNet.DTOs;
using ApiNet.Model;
using AutoMapper;

namespace ApiNet.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Equipo, EquipoNuevoDTO>().ReverseMap();
            CreateMap<Equipo, EquipoRespuestaDTO>().ReverseMap();
           // CreateMap<Equipo, EquipoRespuestaDTO>()
           //.ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Descripcion)); // Asegúrate de que esta línea esté presente

        }
    }
}
