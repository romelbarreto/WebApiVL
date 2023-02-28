namespace WebApiVL.Models
{
    public class UpdateUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Acceso { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public bool Activo { get; set; }
        public string NombreUsuario { get; set; } = null!;
    }
}
    
