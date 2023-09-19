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

        public RegionsController(NZWalkDBContex dBContex, IRegionRepository regionRepository)
        {
            this.dBContex = dBContex;
            this.regionRepository = regionRepository;
        }

        // GET all Regions
        // GET: https://localhost:PORT/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get data from database - Domain Model
            // Repository GetAllAsync
            var regionDomains = await regionRepository.GetAllAsync();

            //Map Domain Model to DTOs
            var regionDTO = new List<RegionDTO>();
            foreach (var regionDomain in regionDomains)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                });

            }

            // Return DTOs
            return Ok(regionDTO);
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
            RegionDTO regionDTO = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };



            return Ok(regionDTO);
        }

        // POST to create New Region
        // POST: https://localhost:PORT/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = new Region()
            {
                Name = addRegionRequestDTO.Name,
                Code = addRegionRequestDTO.Code,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            // Use Domain Model to create Region
            // Repository CreateAsync
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain model back to DTO

            var regionDTO = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.Id }, regionDTO);
        }


        // PUT to Update Region
        // PUT: https://localhost:PORT/api/regions/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            
            var regionDomainModal = new Region()
            {
                Code=updateRegionRequestDTO.Code,
                Name=updateRegionRequestDTO.Name,
                RegionImageUrl=updateRegionRequestDTO.RegionImageUrl
            };

            regionDomainModal = await regionRepository.UpdateAsync(id,regionDomainModal);

            if (regionDomainModal == null)
            {
                return NotFound();
            }

           
            // Convert Domain Model to DTO
            var regionDTO = new RegionDTO()
            {
                Id = regionDomainModal.Id,
                Name = regionDomainModal.Name,
                Code = regionDomainModal.Code,
                RegionImageUrl = regionDomainModal.RegionImageUrl
            };

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
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomainModel.Id,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImageUrl = regionDomainModel.RegionImageUrl
            //};

            return Ok();
        }

    }
}
