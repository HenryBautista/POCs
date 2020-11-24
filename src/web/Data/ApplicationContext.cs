using Microsoft.EntityFrameworkCore;
using PokeStore.Model;

namespace PokeStore.Web.Data
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