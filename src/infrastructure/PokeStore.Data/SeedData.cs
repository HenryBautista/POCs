using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokeStore.Entities;
using PokeStore.Entities.Enums;

namespace PokeStore.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationContext>>()))
            {
                if (context.Pokemons.Any())
                {
                    return;
                } 
                
                context.Pokemons.AddRange(
                    new Pokemon {
                    Id= 1,
                    Name = "Pikachu",
                    Level = 90,
                    Type = PokemonType.Electric,
                    },
                    new Pokemon {
                        Id= 2,
                        Name = "Bulbasur",
                        Level = 91,
                        Type = PokemonType.Grass,
                    },
                    new Pokemon {
                        Id= 3,
                        Name = "Squirtel",
                        Level = 95,
                        Type = PokemonType.Water,
                    },
                    new Pokemon {
                        Id= 4,
                        Name = "Charmander",
                        Level = 100,
                        Type = PokemonType.Fire,
                    }
                );
                context.SaveChanges();
            };
        }
    }
}