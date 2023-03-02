namespace WebApiVL.Models
{
    public class RegistrarUsuarioDTO
    {
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Acceso { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public string NombreUsuario { get; set; }
    }
}
// 
// Language: csharp
// Path: Models\Usuario.cs
// Compare this snippet from Services\UsuarioService.cs:
// using WebApiVL.Context;
// using WebApiVL.Models;
// 
// namespace WebApiVL.Services
// {
//     public class UsuarioService : IUsuarioService
//     {
//         private readonly VelocidadLecturaContext _context;
//         public UsuarioService(VelocidadLecturaContext context)
//         {
//             _context = context;
//         }
//         public async Task<MainResponse> AddUsuario(UsuarioDTO usuarioDTO)
//         {
//             var response = new MainResponse();
//             try
//             {
//                 if (_context.Usuarios.Any(f => f.Correo.ToLower() == usuarioDTO.Correo.ToLower()))
//                 {
//                     response.ErrorMessage = "Ya Existe un Usuario con este Correo";
//                     response.IsSuccess = false;
//                 }
//                 else
//                 {
//                     await _context.AddAsync(new Usuario
//                     {
//                         Nombre = usuarioDTO.Nombre,
//                         Correo = usuarioDTO.Correo,
//                         Acceso = usuarioDTO.Acceso,
//                         Clave = usuarioDTO.Clave,
//                         Activo = usuarioDTO.Activo,
//                         NombreUsuario = usuarioDTO.NombreUsuario
//                     });
//                     await _context.SaveChangesAsync();
//                     response.IsSuccess = true;
//                     response.Content = "Usuario Agregado";
//                 }
//             }
//             catch (Exception ex)
//             {
//                 response.ErrorMessage = ex.Message;
//                 response.IsSuccess = false;
//             }
// 
//             return response;
// 
//         }
// 
//         public async Task<MainResponse> DeleteUsuario(DeleteUsuarioDTO usuarioDTO)
//         {
//             var response = new MainResponse();
//             try
//             {
//                 if (usuarioDTO.Correo == "")
//                 {
//                     response.ErrorMessage = "Por Favor Indique el correo con el que esta Registrado";
//                     return response;
//                 }
//                 var existeusuario
    