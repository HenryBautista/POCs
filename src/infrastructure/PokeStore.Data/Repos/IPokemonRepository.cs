using System.Collections.Generic;
using System.Threading.Tasks;
using PokeStore.Entities;

namespace PokeStore.Data.Repos
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetAll();

        Task<Pokemon> GetOne(int id);

        Task<Pokemon> Edit(int id, Pokemon data);

        Task Save(Pokemon data);

        Task Remove(int id);
    }
}