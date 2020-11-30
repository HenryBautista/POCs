using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeStore.Data.Repos;
using PokeStore.Entities;

namespace PokeStore.Web.ViewComponents
{
    public class PokemonListViewComponent : ViewComponent
    {
        private readonly IPokemonRepository _repository;

        public PokemonListViewComponent(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Pokemon> items = await _repository.GetAll();
            return View(items);
        }
    }
}