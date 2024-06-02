using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkDifficultiesController(NZWalksDbContext dbContext, 
            IMapper mapper, IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultyDto = mapper.Map<List<WalkDifficultyDto>>(walkDifficultyDomain);

            return Ok(walkDifficultyDto);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetByIdAsync(id);

            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficultyDomain);

            return Ok(walkDifficultyDto);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDifficultyDto addWalkDifficultyDto)
        {
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(addWalkDifficultyDto);

            walkDifficultyDomain = await walkDifficultyRepository.CreateAsync(walkDifficultyDomain);

            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetById), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkDifficultyDto updateWalkDifficultyDto)
        {
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(updateWalkDifficultyDto);

            walkDifficultyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficultyDomain);

            if(walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficultyDomain);

            return Ok(walkDifficultyDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficultyDomain);

            return Ok(walkDifficultyDto);

        }
    }
}
