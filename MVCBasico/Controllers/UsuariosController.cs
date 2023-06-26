using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public UsuariosController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Dni == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("Email, Password")] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {

                return View(loginRequest);
            }


            Usuario user = _context.Usuarios.Include("Cuentas").Where(usuario => usuario.Email == loginRequest.Email).Single();

            if (user is null) { return NotFound(); }

            if (user.Password != loginRequest.Password)
            {
                return BadRequest();
            }


            return View("Views/Home/Index.cshtml", user.Cuentas.First());
        }


        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dni,Email,Password,Telefono,FechaNacimiento,Nombre,Apellido")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                Cuenta cuenta = new Cuenta();
                cuenta.Saldo = 0;
                cuenta.Moneda = Moneda.PESOS;
                cuenta.Cbu = RandomNumberGenerator.GetInt32(999999);

                
                usuario.Rol = Rol.CLIENTE;
                usuario.Cuentas.Add(cuenta);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
 

                return RedirectToAction(nameof(Login));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Dni,Email,Password,Telefono,FechaNacimiento,Rol,Nombre,Apellido")] Usuario usuario)
        {
            if (id != usuario.Dni)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Dni))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Dni == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Dni == id);
        }
    }
}
