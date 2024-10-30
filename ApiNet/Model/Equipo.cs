using System.ComponentModel.DataAnnotations;

namespace ApiNet.Model
{
    public class Equipo
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripción { get; set; }
    }
}
