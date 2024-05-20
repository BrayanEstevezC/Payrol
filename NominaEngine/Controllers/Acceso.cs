using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NominaEngine.Models.Entities;
using NominaEngine.Services;
using NominaEngine.Services.Class;
using System.Security.Claims;


namespace NominaEngine.Controllers
{
    public class Acceso : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public Acceso (IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult>Login(string Correo, string Clave)
        {
            Usuarios usuarioEncontrado = await _usuarioService.GetUsuarios(Correo, Utilidades.Encriptar(Clave));

            if(usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.Nombre)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuarios model)
        {
            model.Clave = Utilidades.Encriptar(model.Clave);

            Usuarios UsuarioCreado = await _usuarioService.SaveUsuarios(model);

            if (UsuarioCreado.Id > 0)
            {
                return RedirectToAction("Login", "Acceso");
            }

            ViewData["Mensaje"] = "no se pudo crear el usuario";
            return View();
        }
    }
}
