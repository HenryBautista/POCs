using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokeStore.Entities;

namespace PokeStore.Web.ViewComponents
{
    public class PokemonFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string action)
        {
            ViewBag.Action = action;

            return View();
        }
    }
}