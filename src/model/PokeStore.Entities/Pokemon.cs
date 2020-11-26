using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PokeStore.Entities.Enums;

namespace PokeStore.Entities
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonType Type { get; set; }
        public int Level { get; set; }
        public string PhotoUrl { get; set; }
        
    }
}