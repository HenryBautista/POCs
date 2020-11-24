using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokeStore.Web.Data;
using PokeStore.Web.Models;

namespace PokeStore.Web.Controllers
{
    public class PokemonsController : Controller
    {   
        private readonly ApplicationContext _context; 
        public PokemonsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Pokemon> data = await _context.Pokemons.ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            Pokemon pokemon = await _context.Pokemons.FindAsync(id);
            if(pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Pokemon pokemon = await _context.Pokemons.FindAsync(id);
            if(pokemon == null)
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
                Pokemon pokemonData = await _context.Pokemons.FindAsync(id);
                if(pokemon == null)
                {
                    return NotFound();
                }

                pokemonData.Name = pokemon.Name;
                pokemonData.Level = pokemon.Level;
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");                
            }
            catch(Exception ex)
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
            if(!ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();    
                    return RedirectToAction("Index");
                } 
                catch(Exception ex)
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
                Pokemon pokemon = await _context.Pokemons.FindAsync(id);
                if(pokemon == null)
                {
                    return NotFound();
                }
                _context.Pokemons.Remove(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");                
            }
            catch(Exception ex)
            {

            }
            return View();
        }


        public async Task<IActionResult> Delete(int id)
        {
            Pokemon pokemon = await _context.Pokemons.FindAsync(id);
            if(pokemon ==null)
            {
                return NotFound();
            }
            return View(pokemon);
        }
    }
}