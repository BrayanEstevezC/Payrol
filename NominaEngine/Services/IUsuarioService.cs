using NominaEngine.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace NominaEngine.Services
{
    public interface IUsuarioService
    {
        Task<Usuarios> GetUsuarios(string correo, string clave);
        Task<Usuarios> SaveUsuarios(Usuarios model);
    }
}
