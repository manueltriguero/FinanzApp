using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using MVCBasico.Context;
using MVCBasico.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EscuelaDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, EscuelaDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int idCuenta)
        {
            Cuenta cuenta = _context.Cuentas.Find(idCuenta);
            return View(cuenta);
        }

        public IActionResult Alias(int idCuenta)
        {
            ViewBag.idCuentaOrigen = idCuenta;
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

                Cuenta cuenta = _context.Cuentas.Find(aliasRequest.IdCuentaOrigen);
                cuenta.Alias = aliasRequest.Alias;
                _context.Update(cuenta);
                await _context.SaveChangesAsync();
                return View(nameof(Index), cuenta);
            } else
            {
                return BadRequest();
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
