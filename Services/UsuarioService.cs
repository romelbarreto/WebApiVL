using Microsoft.EntityFrameworkCore;
using WebApiVL.Context;
using WebApiVL.Models;

namespace WebApiVL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly VelocidadLecturaContext _context;
        public UsuarioService(VelocidadLecturaContext context)
        {
            _context = context;
        }
        public async Task<MainResponse> AddUsuario(UsuarioDTO usuarioDTO)
        {
            var response = new MainResponse();
            try
            {
                if (_context.Usuarios.Any(f => f.Correo.ToLower() == usuarioDTO.Correo.ToLower()))
                {
                    response.ErrorMessage = "Ya Existe un Usuario con este Correo";
                    response.IsSuccess = false;
                }
                else
                {
                    await _context.AddAsync(new Usuario
                    {
                        Nombre = usuarioDTO.Nombre,
                        Correo = usuarioDTO.Correo,
                        Acceso = usuarioDTO.Acceso,
                        Clave = usuarioDTO.Clave,
                        Activo = usuarioDTO.Activo,
                        NombreUsuario = usuarioDTO.NombreUsuario
                    });
                    await _context.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Usuario Agregado";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;

        }

        public async Task<MainResponse> DeleteUsuario(DeleteUsuarioDTO usuarioDTO)
        {
            var response = new MainResponse();
            try
            {
                if (usuarioDTO.Correo == "")
                {
                    response.ErrorMessage = "Por Favor Indique el correo con el que esta Registrado";
                    return response;
                }
                var existeusuario=_context.Usuarios.Where(f => f.Correo == usuarioDTO.Correo).FirstOrDefault();
                
                if (existeusuario!=null)
                {
                    _context.Remove(existeusuario);
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Usuario Eliminado";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "No existe Usuario con el correo indicado";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;

        }

        

        public async Task<MainResponse> GetAllUsuario()
        {
            var response = new MainResponse();
            try
            {
                response.Content = await _context.Usuarios.ToListAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> GetUsuarioByUsuarioID(int usuarioID)
        {
            var response = new MainResponse();
            try
            {
                response.Content =
                    await _context.Usuarios.Where(f => f.Id == usuarioID).FirstOrDefaultAsync();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<MainResponse> UpdateUsuario(UpdateUsuarioDTO usuarioDTO)
        {
            var response = new MainResponse();
            try
            {
                if (usuarioDTO.Id < 0)
                {
                    response.ErrorMessage = "Por Favor Indique el Id del Usuario";
                    return response;
                }

                var usuarioexiste = _context.Usuarios.Where(f => f.Id == usuarioDTO.Id).FirstOrDefault();

                if (usuarioexiste != null)
                {
                    usuarioexiste.Nombre = usuarioDTO.Nombre;
                    usuarioexiste.Correo = usuarioDTO.Correo;
                    usuarioexiste.NombreUsuario = usuarioDTO.NombreUsuario;
                    usuarioexiste.Clave = usuarioDTO.Clave;
                    usuarioexiste.Acceso = usuarioDTO.Acceso;
                    usuarioexiste.Activo = usuarioDTO.Activo;
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Datos Actualizados";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Content = "No Hay usuarios Registrados con ese Id";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
