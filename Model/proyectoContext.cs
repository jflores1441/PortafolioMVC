namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class proyectoContext : DbContext
    {
        public proyectoContext()
            : base("name=proyectoContext")
        {
        }

        //Hacen referencia a las tablas en la BD
        public virtual DbSet<Experiencia> Experiencia { get; set; }
        public virtual DbSet<Habilidad> Habilidad { get; set; }
        public virtual DbSet<TablaDato> TablaDato { get; set; }
        public virtual DbSet<Testimonio> Testimonio { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        //Hacen referencia a las relaciones EXISTENTES en la BD
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Experiencia)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Habilidad)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Testimonio)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.Usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
