using System.ComponentModel.DataAnnotations;

namespace ApiNet.DTOs
{
    public class EquipoRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
