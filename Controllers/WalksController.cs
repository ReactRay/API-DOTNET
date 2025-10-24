using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZwalksAPI.CustomActionFilter;
using NZwalksAPI.Models.Domain;
using NZwalksAPI.Models.DTO;
using NZwalksAPI.Repositories;

namespace NZwalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
      
       
                var WalkDomain = mapper.Map<Walk>(addWalkRequestDto);

                WalkDomain = await walkRepository.CreateWalkAsync(WalkDomain);

                return Ok(mapper.Map<WalkDto>(WalkDomain));
        


        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            var Walks = await walkRepository.GetAllWalksAsync(filterOn,filterQuery);


            return Ok(mapper.Map<List<WalkDto>>(Walks));

        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetWalkById([FromRoute]Guid id)
        {
            var Walk =  await walkRepository.GetWalkByIdAsync(id);

            if (Walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(Walk));

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]

        public async Task<IActionResult> updateWalk([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto newWalk)
        {
          
                Walk updatedWalk = mapper.Map<Walk>(newWalk);

                updatedWalk = await walkRepository.updateWalkAsync(id, updatedWalk);

                if (updatedWalk == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalkDto>(updatedWalk));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute]Guid id)
        {
            var walk = await walkRepository.DeleteWalkAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walk));
        }


    }
}
