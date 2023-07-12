using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCBasico.Models;
using Microsoft.Extensions.Hosting;

namespace MVCBasico.Context
{
    public class EscuelaDatabaseContext : DbContext
    {
        public EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cuenta> Cuentas { get; set;}

        public DbSet<Movimiento> Movimientos { get; set;}

        public DbSet<Tarjeta> Tarjetas { get; set;}

        public DbSet<Tarjeta> Pagos { get; set; }

        public DbSet<Transferencia> Transferencias { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; }

        public DbSet<Puntos> Puntos { get; set; }

    }
}
