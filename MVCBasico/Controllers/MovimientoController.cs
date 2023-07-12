using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;

namespace MVCBasico.Controllers
{
    [ValidarSesion]
    public class MovimientoController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public MovimientoController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Movimiento
        public async Task<IActionResult> Index(string? error)

        {
            if (!(error is null))
            {
                ModelState.AddModelError(string.Empty, error);
            }


            List<Cuenta> cuentas =  await _context.Cuentas.Include(cuenta => cuenta.Movimientos)
                .Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).ToListAsync();

            return View(cuentas[0].Movimientos.OrderByDescending(movimiento => movimiento.Fecha));
        }



        // GET: Movimiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // GET: Movimiento/Create
        public IActionResult Create()
        {
            
            return PartialView();
        }

        // POST: Movimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (MovimientoRequest movimientoRequest)
        {
            if (ModelState.IsValid)
            {
                Cuenta cuenta = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();

                if (cuenta.Saldo + movimientoRequest.Importe < 0)
                {
                    return RedirectToAction(nameof(Index), new { error = "No tiene suficiente dinero"});
                }

                cuenta.Saldo += movimientoRequest.Importe;
                Movimiento movimiento = new Movimiento()
                {
                    Fecha = movimientoRequest.Fecha,
                    Descripcion = movimientoRequest.Descripcion,
                    Importe = movimientoRequest.Importe
                };
                cuenta.Movimientos.Add(movimiento);
                _context.Update(cuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), new { error = "Hubo un error" });
        }

        // GET: Movimiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            return View(movimiento);
        }

        // POST: Movimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Descripcion,Importe,CuentaId")] Movimiento movimiento)
        {
            if (id != movimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoExists(movimiento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movimiento);
        }

        // GET: Movimiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimiento = await _context.Movimientos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            return View(movimiento);
        }

        // POST: Movimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.Id == id);
        }
    }
}
