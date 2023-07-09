using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCBasico.Context;
using MVCBasico.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MVCBasico.Controllers
{
    public class LoginController : Controller
    {

        private readonly EscuelaDatabaseContext _dbContext;

        public LoginController(EscuelaDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest) {

            if (ModelState.IsValid) {
                try
                {
                    Usuario usuario = _dbContext.Usuarios.Where(usuario => usuario.Dni == loginRequest.Documento && usuario.Email == loginRequest.Email).Single();

                    if (!(usuario.Password == loginRequest.Password)) {
                        ModelState.AddModelError(String.Empty, "Datos incorrectos");
                    } else
                    {
                        HttpContext.Session.SetInt32("Usuario", usuario.Id);
                        return RedirectToAction("Home", "Home");
                    }


                } catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, "No existe usuario con esos datos");
                }



            }

            return View(loginRequest); 
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = mapearUsuario(registerRequest);
                Cuenta cuenta = new Cuenta()
                {
                    Saldo = 0,
                    Alias = String.Format("{0}.{1}", registerRequest.Nombre.ToUpper(), registerRequest.Apellido.ToUpper()),
                    Moneda = Moneda.PESOS,
                    Cbu = RandomNumberGenerator.GetInt32(Int32.MaxValue)
                };
                usuario.Cuentas.Add(cuenta);
                _dbContext.Usuarios.Add(usuario);
                await _dbContext.SaveChangesAsync();
                return View(nameof(Login));
            }

            return View(registerRequest);
        }

        private Usuario mapearUsuario(RegisterRequest registerRequest)
        {
            return new Usuario
            {
                Nombre = registerRequest.Nombre,
                Apellido = registerRequest.Apellido,
                Rol = Rol.CLIENTE,
                Telefono = registerRequest.Telefono,
                FechaNacimiento = registerRequest.FechaNacimiento,
                Dni = registerRequest.Documento,
                Email = registerRequest.Email,
                Password = registerRequest.Password
            };
        }
    }

    
}
