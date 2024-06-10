using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            var walkDifficultyDomain = await walkDifficultyRepository.GetAllAsync();

            var walkDifficultyDto = mapper.Map<List<WalkDifficultyDto>>(walkDifficultyDomain);

            return Ok(walkDifficultyDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]
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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkDifficultyDto addWalkDifficultyDto)
        {
            // Commented as we used Fluent Validation for model
            /*if (!(ValidateAddWalkDifficulty(addWalkDifficultyDto)))
            {
                return BadRequest(ModelState);
            }*/
            var walkDifficultyDomain = mapper.Map<WalkDifficulty>(addWalkDifficultyDto);

            walkDifficultyDomain = await walkDifficultyRepository.CreateAsync(walkDifficultyDomain);

            var walkDifficultyDto = mapper.Map<WalkDifficultyDto>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetById), new { id = walkDifficultyDto.Id }, walkDifficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkDifficultyDto updateWalkDifficultyDto)
        {
            // Commented as we used Fluent Validation for model
            /* if (!(ValidateUpdateWalkDifficulty(updateWalkDifficultyDto)))
             {
                 return BadRequest(ModelState);
             }*/
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
        [Authorize(Roles = "Writer")]
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

        #region Private Methods

        private bool ValidateAddWalkDifficulty(AddWalkDifficultyDto addWalkDifficultyDto)
        {
            if (addWalkDifficultyDto == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyDto), $"{nameof(addWalkDifficultyDto)} cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyDto.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyDto.Code), $"{nameof(addWalkDifficultyDto.Code)} cannot be empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficulty(UpdateWalkDifficultyDto updateWalkDifficultyDto)
        {
            if (updateWalkDifficultyDto == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyDto), $"{nameof(updateWalkDifficultyDto)} cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyDto.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyDto.Code), $"{nameof(updateWalkDifficultyDto.Code)} cannot be empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
