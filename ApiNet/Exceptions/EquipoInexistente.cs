using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiNet.Exceptions
{
    public class EquipoInexistente : Exception
    {
        public EquipoInexistente(int Id) : base($"No existe un equipo con Id '{Id}'.")
        {
        }
    }
}
