using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public MovimientoController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Movimiento
        public async Task<IActionResult> Index(int id)

        {
            ViewBag.IdCuenta = id;
            return View(await _context.Movimientos.Where(m => m.CuentaId == id).ToListAsync());
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
        public IActionResult Create(int idCuenta)
        {
            Movimiento movimiento = new Movimiento();
            movimiento.CuentaId = idCuenta;
            return View(movimiento);
        }

        // POST: Movimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Descripcion,Importe, CuentaId")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                Cuenta cuenta = _context.Cuentas.Find(movimiento.CuentaId);
                cuenta.Saldo += movimiento.Importe;
                cuenta.Movimientos.Add(movimiento);
                _context.Update(cuenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {id = movimiento.CuentaId});
            }
            return View(movimiento);
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
