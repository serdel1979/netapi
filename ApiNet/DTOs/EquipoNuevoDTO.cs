using System.ComponentModel.DataAnnotations;

namespace ApiNet.DTOs
{
    public class EquipoNuevoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
