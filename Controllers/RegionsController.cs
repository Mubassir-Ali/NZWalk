using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDBContex dBContex;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalkDBContex dBContex, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dBContex = dBContex;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET all Regions
        // GET: https://localhost:PORT/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get data from database - Domain Model
            // Repository GetAllAsync
            var regionDomains = await regionRepository.GetAllAsync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDTO>>(regionDomains));
        }


        // GET Region by Id
        // GET: https://localhost:PORT/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            //Get data from database - Domain Model
            //Repository GetByIdAsync;
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();

            }
            //Map Domain Model to DTOs

            var regionDTO = mapper.Map<RegionDTO>(regionDomain);


            return Ok(regionDTO);
        }

        // POST to create New Region
        // POST: https://localhost:PORT/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

            // Use Domain Model to create Region
            // Repository CreateAsync
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO       

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDTO.Id }, regionDTO);
        }


        // PUT to Update Region
        // PUT: https://localhost:PORT/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {

            var regionDomainModal = mapper.Map<Region>(updateRegionRequestDTO);

            regionDomainModal = await regionRepository.UpdateAsync(id, regionDomainModal);

            if (regionDomainModal == null)
            {
                return NotFound();
            }


            // Convert Domain Model to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModal);

            return Ok(regionDTO);

        }

        // DELETE the Region
        // PUT: https://localhost:PORT/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO (if you want to send delete region)
            //var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok();
        }

    }
}
