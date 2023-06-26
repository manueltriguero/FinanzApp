using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class TransferenciaController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public TransferenciaController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Transferencia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transferencias.ToListAsync());
        }

        // GET: Transferencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // GET: Transferencia/Create
        public IActionResult Create(int idCuenta)
        {
            ViewBag.idCuentaOrigen = idCuenta;
            return View();
        }

        // POST: Transferencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Importe,Motivo, CBU, Alias, IdCuentaOrigen")] TransferenciaRequest transferencia)
        {
            if (ModelState.IsValid)
            {
                Cuenta cuenta = _context.Cuentas.Find(transferencia.IdCuentaOrigen);

                if (transferencia.Importe > cuenta.Saldo)
                {
                    return BadRequest();
                }



                Cuenta cuentaDestino = null;

                if (!(transferencia.CBU == 0))
                {
                    cuentaDestino = _context.Cuentas.Where(c => c.Cbu == transferencia.CBU).Single();
                } else if (!(transferencia.Alias is null))
                {
                    cuentaDestino = _context.Cuentas.Where(c => c.Alias == transferencia.Alias).Single();
                } else
                {
                    return BadRequest();
                }

                Movimiento salida = new Movimiento()
                {
                    Fecha = DateTime.Now,
                    Descripcion = "Transferencia Saliente Hacia " + cuentaDestino.Cbu,
                    Importe = transferencia.Importe * -1
                };

                Movimiento entrada = new Movimiento()
                {
                    Fecha = DateTime.Now,
                    Descripcion = "Transferencia Entrante Desde " + cuenta.id + " - " + transferencia.Motivo,
                    Importe = transferencia.Importe
                };

                cuenta.Saldo -= transferencia.Importe;
                cuenta.Movimientos.Add(salida);

                cuentaDestino.Saldo += transferencia.Importe;
                cuentaDestino.Movimientos.Add(entrada);

                Transferencia transferenciaE = new Transferencia()
                {
                    CuentaOrigen = cuenta,
                    CuentaDestino = cuentaDestino,
                    Fecha = DateTime.Now,
                    Importe = transferencia.Importe,
                    Motivo = transferencia.Motivo
                };

                _context.Update(cuenta);
                _context.Update(cuentaDestino);
                _context.Add(transferenciaE);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home", new { idCuenta = cuenta.id});
            }
            return View(transferencia);
        }

        // GET: Transferencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                return NotFound();
            }
            return View(transferencia);
        }

        // POST: Transferencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Importe,Motivo")] Transferencia transferencia)
        {
            if (id != transferencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransferenciaExists(transferencia.Id))
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
            return View(transferencia);
        }

        // GET: Transferencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transferencia = await _context.Transferencias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transferencia == null)
            {
                return NotFound();
            }

            return View(transferencia);
        }

        // POST: Transferencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transferencia = await _context.Transferencias.FindAsync(id);
            _context.Transferencias.Remove(transferencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransferenciaExists(int id)
        {
            return _context.Transferencias.Any(e => e.Id == id);
        }
    }
}
