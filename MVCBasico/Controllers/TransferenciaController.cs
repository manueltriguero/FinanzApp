using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;
using MVCBasico.Utils;

namespace MVCBasico.Controllers
{
    [ValidarSesion]
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
            return View();
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
        public async Task<IActionResult> Transferir(TransferenciaRequest transferencia)
        {
            if (ModelState.IsValid)
            {
                Cuenta cuenta = _context.Cuentas.Where(cuenta => cuenta.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();

                if (transferencia.Importe > cuenta.Saldo)
                {
                    ModelState.AddModelError("Importe", "El importe no puede ser menor al saldo en cuenta");
                    return View(nameof(Index), transferencia);
                }



                Cuenta cuentaDestino = null;

                if (!(transferencia.CBU is null))
                {
                    try
                    {
                        cuentaDestino = _context.Cuentas.Where(c => c.Cbu == transferencia.CBU).Single();
                    } catch (Exception ex)
                    {
                        ModelState.AddModelError("CBU", "CBU Inexistente");
                        return View(nameof(Index), transferencia);
                    }
                    
                } else if (!(transferencia.Alias is null))
                {
                    try
                    {
                        cuentaDestino = _context.Cuentas.Where(c => c.Alias == transferencia.Alias).Single();
                    } catch (Exception ex)
                    {
                        ModelState.AddModelError("Alias", "Alias inexistente");
                        return View(nameof(Index), transferencia);
                    }
                    
                } else
                {
                    ModelState.AddModelError(String.Empty, "Debe indicar CBU o Alias");
                    return View(nameof(Index), transferencia);

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
                agregarPuntos(transferencia.Importe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Home", "Home");
            }
            return View(nameof(Index), transferencia);
        }

        private void agregarPuntos(double importe)
        {
            Puntos puntos = _context.Puntos.Where(puntos => puntos.UsuarioId == HttpContext.Session.GetInt32("Usuario")).First();

            double saldo = puntos.SaldoRemanente + importe;
            puntos.CantPuntos += ((int)(saldo / Constants.PRECIO_PUNTO));
            puntos.SaldoRemanente = saldo % Constants.PRECIO_PUNTO;

            _context.Puntos.Update(puntos);
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
