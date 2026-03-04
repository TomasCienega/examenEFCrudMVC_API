namespace examenEFCrudMVC_API.DTOS
{
    public class EmpleadoDTO
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public int IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; } = string.Empty;
        public bool? Activo { get; set; }
    }
}
