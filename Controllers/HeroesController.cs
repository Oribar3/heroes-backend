using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using heroesBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using heroesBackend.Repositories;
using heroesBackend.models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace heroesBackend.Controllers
{
    [Route("api/heroes")]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroesRepository _heroesRepository;

        public HeroesController(IHeroesRepository heroesRepository)
        {
            _heroesRepository = heroesRepository;
        }
 

        [HttpPost("")]
        public async Task<IActionResult> NewHero([FromBody] NewHeroModel newHeroModel )
        {
            var res = await _heroesRepository.NewHero(newHeroModel);
            if (res == Guid.Empty) return BadRequest();
            return Ok(res);
        }

        [HttpPost("add-to-trainer/{heroId:guid}")]
        [Authorize]

        public async Task <IActionResult> AddHeroToTrainer([FromRoute]Guid heroId)
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();
            var res = await _heroesRepository.AddHeroToTrainer(heroId, userName);
            if (res) return Ok();
            return BadRequest();
        }

        [HttpGet("mine")]
        [Authorize]
        public async Task<IActionResult> GetAllHeroes()
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();
            var res = await _heroesRepository.GetAllHeroesByUserName(userName);
            if (res == null || res.Count == 0)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost("train/{heroId:guid}")]
        [Authorize]
        public async Task <IActionResult> TrainHero([FromRoute] Guid heroId)
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();
            var res = await _heroesRepository.TrainHero(heroId, userName);
            if (res == null )
            {
                return BadRequest();
            }
            return Ok(res);
        }


    }
}