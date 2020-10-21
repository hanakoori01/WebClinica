using Clinica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebClinica.Models
{
    public partial class DBClinicaAcmeContext : DbContext
    {
        public DBClinicaAcmeContext()
        {
        }

        public DBClinicaAcmeContext(DbContextOptions<DBClinicaAcmeContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Boton> Boton { get; set; }
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Enfermedad> Enfermedad { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<TipoUsuarioPagina> TipoUsuarioPagina { get; set; }
        public virtual DbSet<TipoUsuarioPaginaBoton> TipoUsuarioPaginaBoton { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server= DESKTOP-L0CT9J8\\SQLEXPRESS;Database = DBClinicaAngel;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boton>(entity =>
            {
                entity.HasKey(e => e.BotonId);

                entity.Property(e => e.BotonId).HasColumnName("BotonId");

                entity.Property(e => e.Nombre)
                   .HasColumnName("Nombre")
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                  .HasColumnName("Descripcion")
                  .HasMaxLength(200)
                  .IsUnicode(false);

                entity.Property(e => e.BotonHabilitado).HasColumnName("BotonHabilitado");

            });

            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.CitaId);

                entity.Property(e => e.CitaId)
                .HasColumnName("CitaId");

                entity.Property(e => e.PacienteId)
                 .HasColumnName("PacienteId");

                entity.Property(e => e.MedicoId)
                   .HasColumnName("MedicoId");
                 
                entity.Property(e => e.FechaCita)
                    .HasColumnName("FechaCita")
                    .HasColumnType("datetime");

                entity.Property(e => e.Diagnostico)
                   .HasMaxLength(300)
                   .IsUnicode(false);

                entity.Property(e => e.EspecialidadId)
                .HasColumnName("EspecialidadId");
            });

            modelBuilder.Entity<Enfermedad>(entity =>
            {
                entity.HasKey(e => e.EnfermedadId);

                entity.Property(e => e.EnfermedadId)
                .HasColumnName("EnfermedadId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(400)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.EspecialidadId);

                entity.Property(e => e.EspecialidadId)
                .HasColumnName("EspecialidadId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                   .HasMaxLength(400)
                   .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.MedicoId);

                entity.Property(e => e.MedicoId)
                    .HasColumnName("MedicoId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoFijo)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.TelefonoCelular)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.EspecialidadId)
                .HasColumnName("EspecialidadId");


            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.PacienteId);

                entity.Property(e => e.PacienteId)
                    .HasColumnName("PacienteId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                 .HasMaxLength(50)
                 .IsUnicode(false);

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoContacto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                  .HasMaxLength(50)
                  .IsUnicode(false);
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.Property(e => e.PaginaId).HasColumnName("PaginaId");

                entity.Property(e => e.Mensaje)
                  .HasColumnName("Mensaje")
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.Accion)
                    .HasColumnName("Accion")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Controlador)
                    .HasColumnName("Controlador")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BotonHabilitado).HasColumnName("BotonHabilitado");

            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuarioPagina>(entity =>
            {
                entity.Property(e => e.TipoUsuarioPaginaId).HasColumnName("TipoUsuarioPaginaId");

                entity.Property(e => e.TipoUsuarioId).HasColumnName("TipoUsuarioId");

                entity.Property(e => e.PaginaId).HasColumnName("PaginaId");

                entity.Property(e => e.BotonHabilitado).HasColumnName("BotonHabilitado");
            });

            modelBuilder.Entity<TipoUsuarioPaginaBoton>(entity =>
            {
                entity.Property(e => e.TipoUsuarioPaginaBotonId).HasColumnName("TipoUsuarioPaginaBotonId");

                entity.Property(e => e.TipoUsuarioPaginaId).HasColumnName("TipoUsuarioPaginaId");

                entity.Property(e => e.BotonId).HasColumnName("BotonId");

                entity.Property(e => e.BotonHabilitado).HasColumnName("BotonHabilitado");

            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.Property(e => e.TipoUsuarioId)
             .HasColumnName("TipoUsuarioId");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
