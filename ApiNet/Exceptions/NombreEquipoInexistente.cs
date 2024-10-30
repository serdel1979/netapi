namespace ApiNet.Exceptions
{
    public class NombreEquipoInexistente : Exception
    {
        public NombreEquipoInexistente(string name) : base($"El nombre de equipo {name} no existe")
        {
        }
    }
}
