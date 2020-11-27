using Xunit;
using Moq;
using PokeStore.Data.Repos;
using System.Collections.Generic;
using PokeStore.Entities;
using PokeStore.Entities.Enums;
using PokeStore.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;

namespace PokeStore.Web.Tests.Controllers
{
    public class PokemonsControllerTests
    {

        [Fact]
        public async Task It_Should_Return_A_View_With_All_Pokemons()
        {
            //Arrange
            var mockRepo = new Mock<IPokemonRepository>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetDummyData());
            PokemonsController controller = new PokemonsController(mockRepo.Object);
            //Act
            var result = await controller.Index();
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<List<Pokemon>>(viewResult.ViewData.Model);
            Assert.Equal(2, modelResult.Count);
        }

        [Fact]
        public async Task It_Should_Return_A_Details_View_And_Pokemon_Data_By_Id()
        {
            //Given
            var mockRepo = new Mock<IPokemonRepository>();
            mockRepo.Setup(repo => repo.GetOne(1)).ReturnsAsync(GetDummyData().FirstOrDefault());
            PokemonsController controller = new PokemonsController(mockRepo.Object);
            Pokemon dummyExpected = new Pokemon { Id = 1, Name = "DummyPokemon", Level = 0, Type = PokemonType.Bug, };
            var expected = JsonConvert.SerializeObject(dummyExpected);
            //When
            var result = await controller.Details(1);
            //Then
            var viewResult = Assert.IsType<ViewResult>(result);
            var modelResult = Assert.IsAssignableFrom<Pokemon>(viewResult.ViewData.Model);
            var objectResult = JsonConvert.SerializeObject(modelResult);
            Assert.Equal(expected, objectResult);
        }

        [Fact]
        public async Task It_Should_Modify_Pokemon_And_Return_To_Index()
        {
            //Given
            var mockRepo = new Mock<IPokemonRepository>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(GetDummyData());
            PokemonsController controller = new PokemonsController(mockRepo.Object);
            Pokemon dummy = new Pokemon { Id = 1, Name = "DummyPokemon", Level = 0, Type = PokemonType.Bug, };
            mockRepo.Setup(repo => repo.Edit(1, dummy)).ReturnsAsync(dummy);
            //When
            var result = await controller.EditPokemon(1, dummy);
            //Then
            var viewResult = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        [Fact]
        public async Task It_should_Return_Not_Found_If_Object_Doesnt_Exits()
        {
        //Given
            var mockRepo = new Mock<IPokemonRepository>();
            mockRepo.Setup(repo => repo.GetOne(1)).ReturnsAsync((Pokemon)null);
            PokemonsController controller = new PokemonsController(mockRepo.Object);
        //When
            var result = await controller.Edit(1);        
        //Then
            Assert.IsType<NotFoundResult>(result);
        }

        public List<Pokemon> GetDummyData()
        {
            List<Pokemon> dummyData = new List<Pokemon>
            {
                new Pokemon { Id = 1, Name = "DummyPokemon", Level = 0, Type = PokemonType.Bug, },
                new Pokemon { Id = 2, Name = "DummyPokemon2", Level = 2, Type = PokemonType.Bug, },
            };

            return dummyData;
        }
    }
}