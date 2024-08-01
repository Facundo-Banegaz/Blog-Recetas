using Blog_Recetas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog_Recetas.Data
{
    public class BlogContext : IdentityDbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Instruccion> Instrucciones { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la relación entre Publicacion e Ingrediente con eliminación en cascada
            modelBuilder.Entity<Publicacion>()
                .HasMany(p => p.Ingredientes)
                .WithOne(i => i.Publicacion)
                .HasForeignKey(i => i.PublicacionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configura la relación entre Publicacion e Instruccion con eliminación en cascada
            modelBuilder.Entity<Publicacion>()
                .HasMany(p => p.Instrucciones)
                .WithOne(i => i.Publicacion)
                .HasForeignKey(i => i.PublicacionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configura la relación entre Autor y Publicacion con eliminación en cascada
            modelBuilder.Entity<Autor>()
                .HasMany(a => a.Publicaciones)
                .WithOne(p => p.Autor)
                .HasForeignKey(p => p.AutorId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configura la relación entre Categoria y Publicacion con eliminación en cascada
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Publicaciones)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
