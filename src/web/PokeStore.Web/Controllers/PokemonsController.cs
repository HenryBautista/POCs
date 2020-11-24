using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeStore.Data;
using PokeStore.Data.Repos;
using PokeStore.Entities;

namespace PokeStore.Web.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly IPokemonRepository _repository;
        public PokemonsController(IPokemonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            List<Pokemon> data = await _repository.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            Pokemon pokemon = await _repository.GetOne(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Pokemon pokemon = await _repository.GetOne(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPokemon(int id, Pokemon pokemon)
        {
            try
            {
                await _repository.Edit(id, pokemon);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(Pokemon pokemon)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    await _repository.Save(pokemon);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return View(pokemon);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            try
            {
                Pokemon pokemon = await _repository.GetOne(id);
                if (pokemon == null)
                {
                    return NotFound();
                }
                await _repository.Remove(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            Pokemon pokemon = await _repository.GetOne(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }
    }
}