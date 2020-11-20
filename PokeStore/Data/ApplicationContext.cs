using Microsoft.EntityFrameworkCore;
using PokeStore.Models;

namespace PokeStore.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext (DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
    }
}