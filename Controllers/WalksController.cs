using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //  /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        // GET All Walks
        // GET: /api/walks
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walkDomainModel=await walkRepository.GetAllWalksAsync();
            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));

        }

        // GET Walk by id
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var walkDomainModel=await walkRepository.GetWalkByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        // CREATE Walk
        // POST: /api/walks

        [HttpPost]
        public  async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map DTO to Domain Model
            var walkDomainModel=  mapper.Map<Walk>(addWalkRequestDTO);
            //walkDomainModel= await walkRepository.CreateWalkAsync(walkDomainModel);
            walkDomainModel=await walkRepository.CreateWalkAsync(walkDomainModel);


            return Ok(mapper.Map<WalkDTO>(walkDomainModel));

        }

        // UPDATE Walk
        // PUT: /api/walks/{id}

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            var walkDomailModel = mapper.Map<Walk>(updateWalkRequestDTO);
            walkDomailModel = await walkRepository.UpdateWalkAsync(id, walkDomailModel);
            
            if (walkDomailModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDTO>(walkDomailModel));
        }

        // DELETE
        // DELETE: /api/walks/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]  Guid id)
        {
            var regionDomainModel=await walkRepository.DeleteWalkAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
