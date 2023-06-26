﻿// <auto-generated />
using System;
using MVCBasico.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MVCBasico.Migrations
{
    [DbContext(typeof(EscuelaDatabaseContext))]
    [Migration("20230626191852_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MVCBasico.Models.Cuenta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Cbu")
                        .HasColumnType("bigint");

                    b.Property<int>("Moneda")
                        .HasColumnType("int");

                    b.Property<double>("Saldo")
                        .HasColumnType("float");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.Property<int?>("UsuarioId1")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("UsuarioId1");

                    b.ToTable("Cuentas");
                });

            modelBuilder.Entity("MVCBasico.Models.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DeporteFavorito")
                        .HasColumnType("int");

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInscripto")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("MVCBasico.Models.Movimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CuentaId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CuentaId");

                    b.ToTable("Movimientos");
                });

            modelBuilder.Entity("MVCBasico.Models.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<double>("Tasa")
                        .HasColumnType("float");

                    b.Property<int>("Vencimiento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("MVCBasico.Models.Tarjeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodSeguridad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<double>("Limite")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Numero")
                        .HasColumnType("bigint");

                    b.Property<string>("Proveedor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tarjeta");
                });

            modelBuilder.Entity("MVCBasico.Models.Transferencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CuentaDestinoid")
                        .HasColumnType("int");

                    b.Property<int?>("CuentaOrigenid")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<string>("Motivo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CuentaDestinoid");

                    b.HasIndex("CuentaOrigenid");

                    b.ToTable("Transferencias");
                });

            modelBuilder.Entity("MVCBasico.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Dni")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MVCBasico.Models.Cuenta", b =>
                {
                    b.HasOne("MVCBasico.Models.Usuario", null)
                        .WithMany("Cuentas")
                        .HasForeignKey("UsuarioId1");
                });

            modelBuilder.Entity("MVCBasico.Models.Movimiento", b =>
                {
                    b.HasOne("MVCBasico.Models.Cuenta", null)
                        .WithMany("Movimientos")
                        .HasForeignKey("CuentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MVCBasico.Models.Transferencia", b =>
                {
                    b.HasOne("MVCBasico.Models.Cuenta", "CuentaDestino")
                        .WithMany()
                        .HasForeignKey("CuentaDestinoid");

                    b.HasOne("MVCBasico.Models.Cuenta", "CuentaOrigen")
                        .WithMany()
                        .HasForeignKey("CuentaOrigenid");
                });
#pragma warning restore 612, 618
        }
    }
}
