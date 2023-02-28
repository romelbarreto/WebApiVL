using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiVL.Models;

namespace WebApiVL.Context;

public partial class VelocidadLecturaContext : DbContext
{
    public VelocidadLecturaContext()
    {
    }

    public VelocidadLecturaContext(DbContextOptions<VelocidadLecturaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estadistica> Estadisticas { get; set; }

    public virtual DbSet<Nivel> Nivels { get; set; }

    public virtual DbSet<Tarjeta> Tarjetas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
     //   => optionsBuilder.UseSqlServer("");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estadistica>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNivelNavigation).WithMany()
                .HasForeignKey(d => d.IdNivel)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estadisticas_Nivel");

            entity.HasOne(d => d.IdTarjetaNavigation).WithMany()
                .HasForeignKey(d => d.IdTarjeta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estadisticas_Tarjetas");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Estadisticas_Usuarios");
        });

        modelBuilder.Entity<Nivel>(entity =>
        {
            entity.ToTable("Nivel");

            entity.Property(e => e.Nivel1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nivel");
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.Property(e => e.Cuerpo).IsUnicode(false);
            entity.Property(e => e.Dificultad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Enlace)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Op1P1).IsUnicode(false);
            entity.Property(e => e.Op1P2).IsUnicode(false);
            entity.Property(e => e.Op1P3).IsUnicode(false);
            entity.Property(e => e.Op2P1).IsUnicode(false);
            entity.Property(e => e.Op2P2).IsUnicode(false);
            entity.Property(e => e.Op2P3).IsUnicode(false);
            entity.Property(e => e.Op3P1).IsUnicode(false);
            entity.Property(e => e.Op3P2).IsUnicode(false);
            entity.Property(e => e.Op3P3).IsUnicode(false);
            entity.Property(e => e.Op4P1).IsUnicode(false);
            entity.Property(e => e.Op4P2).IsUnicode(false);
            entity.Property(e => e.Op4P3).IsUnicode(false);
            entity.Property(e => e.Op5P1).IsUnicode(false);
            entity.Property(e => e.Op5P2).IsUnicode(false);
            entity.Property(e => e.Op5P3).IsUnicode(false);
            entity.Property(e => e.Pregunta1).IsUnicode(false);
            entity.Property(e => e.Pregunta2).IsUnicode(false);
            entity.Property(e => e.Pregunta3).IsUnicode(false);
            entity.Property(e => e.Resp1).IsUnicode(false);
            entity.Property(e => e.Resp2).IsUnicode(false);
            entity.Property(e => e.Resp3).IsUnicode(false);
            entity.Property(e => e.Titulo).IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Acceso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
