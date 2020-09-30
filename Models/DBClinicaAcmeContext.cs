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
        public virtual DbSet<Enfermedad> Enfermedad { get; set; }
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<Especialidad> Especialidad { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
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
            modelBuilder.Entity<Enfermedad>(entity =>
            {
                entity.Property(e => e.EnfermedadId)
                .HasColumnName("enfermedadId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

               
            });
            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.Property(e => e.EspecialidadId)
                .HasColumnName("EspecialidadID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.Property(e => e.MedicoId)
                    .HasColumnName("MedicoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

               entity.HasOne(d => d.Especialidad)
                .WithMany(p => p.Medico)
                .HasForeignKey(d => d.EspecialidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medico_Especialidad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoCelular)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoFijo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.Property(e => e.PacienteId)
                    .HasColumnName("PacienteId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Foto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoContacto)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.CitaId);

                entity.Property(e => e.CitaId).HasColumnName("CitaID");

                entity.Property(e => e.Diagnostico)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.EspecialidadId).HasColumnName("EspecialidadID");

                entity.Property(e => e.FechaCita)
                    .HasColumnName("Fecha_Cita")
                    .HasColumnType("datetime");

                entity.Property(e => e.MedicoId)
                    .HasColumnName("MedicoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PacienteId)
                   .HasColumnName("PacienteId")
                   .ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
