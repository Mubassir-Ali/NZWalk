using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalkDBContex dBContex;

        public RegionsController(NZWalkDBContex dBContex)
        {
            this.dBContex = dBContex;
        }

        // GET all Regions
        // GET: https://localhost:PORT/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get data from database - Domain Model
            var regionDomains = await dBContex.Region.ToListAsync();

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
            //var regionDomain = dBContex.Region.Find(id);
            var regionDomain = await dBContex.Region.FirstOrDefaultAsync(x => x.Id == id);

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
            await dBContex.Region.AddAsync(regionDomainModel);
            await dBContex.SaveChangesAsync();

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
            // check if region exist
            var regionDomainModal = await dBContex.Region.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModal == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModal.Name = updateRegionRequestDTO.Name;
            regionDomainModal.Code = updateRegionRequestDTO.Code;
            regionDomainModal.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            await dBContex.SaveChangesAsync();

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
            var regionDomainModel = await dBContex.Region.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete Region
            dBContex.Remove(regionDomainModel);
            await dBContex.SaveChangesAsync();

            return Ok();
        }

    }
}
