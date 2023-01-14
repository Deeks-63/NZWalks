using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
       public async Task<IActionResult> GetAllWalksAsync() 
       {
            //Fetch data from database - Domain walks
            var walksDomain = await walkRepository.GetAllAsync();
            //Convert domain walks to DTO walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);
            //return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid Id)
        {
            //Get walk Domain object from database
            var walkDomain = await walkRepository.GetAsync(Id);
            //Convert from domain to DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            //Return
            return Ok(walkDTO);
        }

        [HttpPost]
        //[Route("{id:guid}")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //Convert DTO to Domain
            var walkDomain = new Models.Domain.Walk 
            {
                Length= addWalkRequest.Length,
                Name=addWalkRequest.Name,
                RegionId=addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId
            };
            //Pass domain object to repository to persist this
            walkDomain = await walkRepository.AddAsync(walkDomain);
            //Convert Domain back to DTO
            var walkDTO = new Models.DTO.Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            //Send DTO response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }
    }
}
