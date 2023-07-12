using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Controllers
{
    [ValidarSesion]
    public class PuntosController : Controller
    {

        private readonly EscuelaDatabaseContext _context;

        public PuntosController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            Puntos puntos = _context.Puntos.Where(puntos => puntos.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();


            return View(new PuntosVista()
            {
                Puntos = puntos.CantPuntos
            });
        }

        public async Task<IActionResult> Canjear() {

            Cuenta cuenta = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();

            Puntos puntos = _context.Puntos.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();

            cuenta.Saldo += puntos.CantPuntos / Constants.PUNTOS_POR_PESO;

            puntos.CantPuntos = 0;

            _context.Update(puntos);
            _context.Update(cuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
