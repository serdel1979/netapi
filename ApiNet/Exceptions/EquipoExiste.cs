namespace ApiNet.Exceptions
{
    public class EquipoExiste : Exception
    {
        public EquipoExiste(string nombre)
        : base($"Ya existe un equipo con el nombre '{nombre}'.")
        {
        }
    }
}
