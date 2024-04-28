using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD4.services;
using APBD4.models;

namespace APBD4.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : Controller
    {
        private readonly IDbService _animalDbService;
        public AnimalController(IDbService dbService)
        {
            _animalDbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimal([FromQuery] string orderBy)
        {
            return Ok(_animalDbService.GetAnimal(orderBy));
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> PutAnimalById([FromRoute]string idAnimal, [FromBody]Animal animal)
        {
            _animalDbService.UpdateAnimal(idAnimal, animal);
            return Ok();
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute]string idAnimal)
        {
            _animalDbService.DeleteAnimal(idAnimal);
            return Ok();
        }

        [HttpPost]
        public async Task<IAsyncResult> UpdateAnimal([FromBody]Animal animal)
        {
            _animalDbService.AddAnimal(animal);
            return null;
        }
    }
}
