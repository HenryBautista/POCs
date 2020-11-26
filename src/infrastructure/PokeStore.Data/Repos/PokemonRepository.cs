using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokeStore.Entities;

namespace PokeStore.Data.Repos
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ApplicationContext _context;

        public PokemonRepository(ApplicationContext context)
        {
            _context = context;
        } 
        public async Task<List<Pokemon>> GetAll()
        {
            List<Pokemon> pokemons = await _context.Pokemons.ToListAsync();
            return pokemons;
        }

        public async Task<Pokemon> GetOne(int id)
        {
            Pokemon pokemon = await _context.Pokemons.FindAsync(id);
            return pokemon;
        }

        public async Task<Pokemon> Edit(int id, Pokemon data)
        {
            Pokemon pokemon = await _context.Pokemons.FindAsync(id);
            
            if(pokemon == null)
            {
                return null;
            }

            pokemon.Name = data.Name;
            pokemon.Level = data.Level;
            pokemon.Type = data.Type;
            pokemon.PhotoUrl = data.PhotoUrl;
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task Save(Pokemon data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            Pokemon pokemon = await GetOne(id);
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }
    }
}
