using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EscuelaDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, EscuelaDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Home()
        {
            List<Cuenta> cuentas = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).ToList();

            return View(cuentas[0]);
        }

        public IActionResult Index()
        {
            List<Cuenta> cuentas = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).ToList();
            
            return View(cuentas[0]);
        }

        public IActionResult Alias()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Alias(AliasRequest aliasRequest)
        {

            

            if (ModelState.IsValid)
            {

                if (_context.Cuentas.Where(c => c.Alias == aliasRequest.Alias).Any())
                {
                    ModelState.AddModelError("Alias", "Alias ya utilizado");
                    return View(aliasRequest);
                }

                Cuenta cuenta = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();
                cuenta.Alias = aliasRequest.Alias;
                _context.Update(cuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Home");
            } else
            {
                return View(aliasRequest);
            }

            
        }

        public IActionResult Movimientos(int idCuenta)
        {
            return RedirectToAction("Index", "Movimiento", new {id = idCuenta});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
