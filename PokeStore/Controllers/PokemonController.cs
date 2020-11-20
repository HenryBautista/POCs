using Microsoft.AspNetCore.Mvc;

namespace PokeStore.Controllers
{
    public class PokemonController : Controller
    {
        public PokemonController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int Id)
        {
            return View();
        }
    }
}