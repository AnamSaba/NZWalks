using AutoMapper;
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
      
        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto)
        {
            var regionDomain = mapper.Map<Region>(addRegionDto);

            regionDomain = await regionRepository.AddAsync(regionDomain);

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);

        }

        // Delete REGION
        // DELETE: https://localhost:7046/api/Regions/{id}
        
        [HttpDelete]
        [Route("{id:guid}")]
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

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomain = mapper.Map<Region>(updateRegionDto);
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDomain);

        }


    }
}
