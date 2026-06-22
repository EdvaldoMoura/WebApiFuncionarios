using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sobrenome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Departamento)
                    .HasConversion<int>();

                entity.Property(e => e.Turno)
                    .HasConversion<int>();

                entity.Property(e => e.Status)
                    .HasDefaultValue(true);

               // entity.Property(e => e.DataCriacao)
               //     .HasDefaultValueSql("CURRENT_TIMESTAMP");

               // entity.Property(e => e.DataAlteracao)
               //     .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
