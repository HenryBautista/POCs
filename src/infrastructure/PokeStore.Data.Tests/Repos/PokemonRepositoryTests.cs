using PokeStore.Entities;
using PokeStore.Data;
using PokeStore.Data.Repos;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokeStore.Data.Tests.Repos
{
    public class PokemonRepositoryTests
    {
        public DbContextOptions<ApplicationContext> _options;
        public PokemonRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                    .UseInMemoryDatabase(databaseName: "Pokemons")
                    .Options;
        }

        [Fact]
        public async Task It_Should_Return_All_Pokemons()
        {
            //Arrange
            using (var context = new ApplicationContext(_options))
            {
                context.Pokemons.Add(new Pokemon {Name = "Pikachu", Level = 80});
                context.Pokemons.Add(new Pokemon {Name = "Charmander", Level = 60});
                context.Pokemons.Add(new Pokemon {Name = "Bulbasaur", Level = 50});    
                context.SaveChanges();
            }
            int expectedQuantity = 4;
            //Act   
            using (var context = new ApplicationContext(_options))
            {
                PokemonRepository pokemonRepository = new PokemonRepository(context);
                List<Pokemon> pokemons = await pokemonRepository.GetAll();
                
                //Assert
                Assert.Equal(expectedQuantity, pokemons.Count);
            }
        }     

        [Fact]
        public async Task It_Should_Return_An_Pokemon_By_Id()
        {
            int PokemonId = 2;
            Pokemon expectedPokemon = new Pokemon 
            {   
                Id = 2, 
                Name = "Charmander", 
                Level = 60,
            };

            using (var context = new ApplicationContext(_options))
            {
                PokemonRepository pokemonRepository = new PokemonRepository(context);
                Pokemon pokemon = await pokemonRepository.GetOne(PokemonId);
                
                //Assert
                Assert.NotStrictEqual(expectedPokemon, pokemon);
            }
        }


        [Fact]
        public async Task It_Should_Create_A_New_Pokemon()
        {
            int expectedQuantity = 1;
            Pokemon newPokemon = new Pokemon 
            {   
                Name = "Eevee", 
                Level = 100,
            };

            using (var context = new ApplicationContext(_options))
            {
                PokemonRepository pokemonRepository = new PokemonRepository(context);
                await pokemonRepository.Save(newPokemon);
                
                int currentQuantity = (await pokemonRepository.GetAll()).Count;

                //Assert    
                Assert.Equal(expectedQuantity, currentQuantity);
            }
        }  
    }
}