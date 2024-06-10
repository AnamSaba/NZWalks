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
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository,
            NZWalksDbContext dbContext,
            IMapper mapper) 
        {
            this.regionRepository = regionRepository;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7046/api/Regions

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
       {
            var regionsDomain = await regionRepository.GetAllAsync();

            // Domain to DTO

            var regionDtos = mapper.Map<List<RegionDto>>(regionsDomain);

            
            return Ok(regionDtos);
       }

        // GET REGION By ID
        // GET: https://localhost:7046/api/Regions/{id}

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]

        public async Task<IActionResult> GetById(Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if(regionDomain == null) 
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        // POST REGION
        // POST: https://localhost:7046/api/Regions

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto)
        {

            // Commented as we used Fluent Validation for model
            /*if(!ValidateAddRegion(addRegionDto))
            {
                return BadRequest(ModelState);
            }*/

            var regionDomain = mapper.Map<Region>(addRegionDto);

            regionDomain = await regionRepository.AddAsync(regionDomain);

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);

        }

        // Delete REGION
        // DELETE: https://localhost:7046/api/Regions/{id}
        
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var regioDomain = await regionRepository.DeleteAsync(id);

            if (regioDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regioDomain);

            return Ok(regionDto);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            // Commented as we used Fluent Validation for model
            /* if (!ValidateUpdateRegion(updateRegionDto))
             {
                 return BadRequest(ModelState);
             }*/

            var regionDomain = mapper.Map<Region>(updateRegionDto);
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDomain);

        }

        #region Private Region

        private bool ValidateAddRegion(AddRegionDto addRegionDto)
        {
            if(addRegionDto == null)
            {
                ModelState.AddModelError(nameof(addRegionDto), $"{nameof(addRegionDto)} Add region data is required.");
                return false;
            }

            if(string.IsNullOrWhiteSpace(addRegionDto.Code))
            {
                ModelState.AddModelError(nameof(addRegionDto.Code), $"{nameof(addRegionDto.Code)} cannot be empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(addRegionDto.Name))
            {
                ModelState.AddModelError(nameof(addRegionDto.Name), $"{nameof(addRegionDto.Name)} cannot be empty or white space.");
            }
            if (addRegionDto.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionDto.Area), $"{nameof(addRegionDto.Area)} cannot be less than or equal to zero.");
            }

            if (addRegionDto.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionDto.Population), $"{nameof(addRegionDto.Long)} cannot be less than zero.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateRegion(UpdateRegionDto updateRegionDto)
        {
            if (updateRegionDto == null)
            {
                ModelState.AddModelError(nameof(updateRegionDto), $"{nameof(updateRegionDto)} Add region data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionDto.Code))
            {
                ModelState.AddModelError(nameof(updateRegionDto.Code), $"{nameof(updateRegionDto.Code)} cannot be empty or white space.");
            }
            if (string.IsNullOrWhiteSpace(updateRegionDto.Name))
            {
                ModelState.AddModelError(nameof(updateRegionDto.Name), $"{nameof(updateRegionDto.Name)} cannot be empty or white space.");
            }
            if (updateRegionDto.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionDto.Area), $"{nameof(updateRegionDto.Area)} cannot be less than or equal to zero.");
            }

            if (updateRegionDto.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionDto.Population), $"{nameof(updateRegionDto.Long)} cannot be less than zero.");
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
