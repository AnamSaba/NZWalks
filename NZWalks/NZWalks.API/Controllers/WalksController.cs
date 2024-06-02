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
    public class WalksController : Controller
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(NZWalksDbContext dbContext, 
            IMapper mapper, IWalkRepository walkRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var walkDomain = await walkRepository.GetAllAsync();

            var walkDto = mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomain);
            return Ok(walkDto);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWalkDto addWalkDto)
        {
            var walkDomain = mapper.Map<Walk>(addWalkDto);

            walkDomain = await walkRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return CreatedAtAction(nameof(GetById), new {id =  walkDto.Id}, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walkDomain = mapper.Map<Walk>(updateWalkDto);

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

    }
}
