using FluentValidation;
using PokeStore.Entities.Enums;

namespace PokeStore.Entities.Validators
{
    public class PokemonValidator : AbstractValidator<Pokemon>
    {
        public PokemonValidator()
        {
            RuleFor(pokemon =>  pokemon.Name)
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(50);
            RuleFor(pokemon => pokemon.Level)
                .NotNull();
            RuleFor(pokemon => pokemon.Type)
                .IsInEnum();

        }
        
    }
}