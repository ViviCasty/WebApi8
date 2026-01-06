using Microsoft.EntityFrameworkCore;
using WebApi8.Models;

namespace WebApi8.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // conexão banco de sados, cvonstrutor, faz serviço de meio de campo
        // tabela de livros e autores

        public DbSet<AutorModel> Autores { get; set; }
        public DbSet<LivroModel> Livros { get; set; }

    }
}
