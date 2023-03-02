namespace WebApiVL.Models
{
    public class UsuarioDTO
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

// Language: csharp
// Path: Controllers\UsuarioController.cs
// Compare this snippet from Controllers\UsuarioController.cs:
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebApiVL.Data;
// using WebApiVL.Models;
// 
// namespace WebApiVL.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class UsuarioController : ControllerBase
//     {
//         private readonly WebApiVLContext _context;
// 
//         public UsuarioController(WebApiVLContext context)
//         {
//             _context = context;
//         }
// 
//         // GET: api/Usuario
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
//         {
//             return await _context.Usuario.ToListAsync();
//         }
// 
//         // GET: api/Usuario/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Usuario>> GetUsuario(int id)
//         {
//             var usuario = await _context.Usuario.FindAsync(id);
// 
//             if (usuario == null)
//             {
//                 return NotFound();
//             }
// 
//             return usuario;
//         }
// 
//         // PUT: api/Usuario/5
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
//         {
//             if (id != usuario.Id)
//             {
//                 return BadRequest();
//             }
// 
//             _context.Entry(usuario).State = EntityState.Modified;
// 
//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//
   // }
//}
