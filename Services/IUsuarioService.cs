using WebApiVL.Models;

namespace WebApiVL.Services
{
    public interface IUsuarioService
    {
        Task<MainResponse> AddUsuario(UsuarioDTO usuarioDTO);
        Task<MainResponse> UpdateUsuario(UpdateUsuarioDTO updateUsuarioDTO);
        Task<MainResponse> DeleteUsuario(DeleteUsuarioDTO usuarioDTO);
        Task<MainResponse> GetAllUsuario();
        Task<MainResponse> GetUsuarioByUsuarioID(int usuarioID);
    }
}
