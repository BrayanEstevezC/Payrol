﻿using NominaEngine.Models.Entities;
using NominaEngine.Models;
using Microsoft.EntityFrameworkCore;

namespace NominaEngine.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly Context _context;

        public UsuarioService(Context context)
        {
            _context = context;
        }

        public async Task<Usuarios> GetUsuarios(string correo, string clave)
        {
            Usuarios usuario_encontrado = await _context.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
               .FirstOrDefaultAsync();
            return usuario_encontrado;
        }

        public async Task<Usuarios> SaveUsuarios(Usuarios model)
        {
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
