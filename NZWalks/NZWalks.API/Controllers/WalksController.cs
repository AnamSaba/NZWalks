using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(NZWalksDbContext dbContext, 
            IMapper mapper, IWalkRepository walkRepository, IRegionRepository regionRepository,
            IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
        }
		//GET : https://localhost:7046/api/Walks/filetrOn=Name&filterQuery=Track&sortBy=Name&isAscending=false&pageNumber=1&pageSize=10

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filetQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkDomain = await walkRepository.GetAllAsync(filterOn,filetQuery,sortBy,isAscending ?? true,
                pageNumber, pageSize);

            var walkDto = mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]
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
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkDto addWalkDto)
        {
            if (!(await ValidateAddWalk(addWalkDto)))
            {
                return BadRequest(ModelState);
            }

            var walkDomain = mapper.Map<Walk>(addWalkDto);

            walkDomain = await walkRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return CreatedAtAction(nameof(GetById), new {id =  walkDto.Id}, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            if (!(await ValidateUpdateWalk(updateWalkDto)))
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "Writer")]
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

        #region Private Methods

        private async Task<bool> ValidateAddWalk(AddWalkDto addWalkDto)
        {
            // Commented as we used Fluent Validation for below validations
            /* if (addWalkDto == null)
             {
                 ModelState.AddModelError(nameof(addWalkDto), $"{nameof(addWalkDto)} cannot be empty.");
                 return false;
             }

             if (string.IsNullOrWhiteSpace(addWalkDto.Name))
             {
                 ModelState.AddModelError(nameof(addWalkDto.Name), $"{nameof(addWalkDto.Name)} cannot be empty or white space.");
             }

             if (addWalkDto.Length < 0)
             {
                 ModelState.AddModelError(nameof(addWalkDto.Length), $"{nameof(addWalkDto.Length)} cannot be less than zero.");
             }*/

            var region = await regionRepository.GetByIdAsync(addWalkDto.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkDto.RegionId), $"{nameof(addWalkDto.RegionId)} is not valid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetByIdAsync(addWalkDto.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkDto.WalkDifficultyId), $"{nameof(addWalkDto.WalkDifficultyId)} is not valid.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }

        private async Task<bool> ValidateUpdateWalk(UpdateWalkDto updateWalkDto)
        {
            // Commented as we used Fluent Validation for below validations
            /* if (updateWalkDto == null)
             {
                 ModelState.AddModelError(nameof(updateWalkDto), $"{nameof(updateWalkDto)} cannot be empty.");
                 return false;
             }

             if (string.IsNullOrWhiteSpace(updateWalkDto.Name))
             {
                 ModelState.AddModelError(nameof(updateWalkDto.Name), $"{nameof(updateWalkDto.Name)} cannot be empty or white space.");
             }

             if (updateWalkDto.Length < 0)
             {
                 ModelState.AddModelError(nameof(updateWalkDto.Length), $"{nameof(updateWalkDto.Length)} cannot be less than zero.");
             }*/

            var region = await regionRepository.GetByIdAsync(updateWalkDto.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkDto.RegionId), $"{nameof(updateWalkDto.RegionId)} is not valid.");
            }

            var walkDifficulty = await walkDifficultyRepository.GetByIdAsync(updateWalkDto.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkDto.WalkDifficultyId), $"{nameof(updateWalkDto.WalkDifficultyId)} is not valid.");
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
