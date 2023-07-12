using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;
using System;
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

            double credito = (double)puntos.CantPuntos / Constants.PUNTOS_POR_PESO;

            Movimiento movimiento = new Movimiento()
            {
                Fecha = DateTime.Now,
                Descripcion = String.Format("Canje de {0} puntos", puntos.CantPuntos),
                Importe = credito
            };

            
            cuenta.Saldo += credito;

            puntos.CantPuntos = 0;

            cuenta.Movimientos.Add( movimiento );

            _context.Update(puntos);
            _context.Update(cuenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
