using Microsoft.AspNetCore.Mvc;

namespace PokeStore.Controllers
{
    public class PokemonsController : Controller
    {
        public PokemonsController()
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