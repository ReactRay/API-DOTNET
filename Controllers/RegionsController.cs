using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalksAPI.CustomActionFilter;
using NZwalksAPI.Data;
using NZwalksAPI.Models.Domain;
using NZwalksAPI.Models.DTO;
using NZwalksAPI.Repositories;

namespace NZwalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController
        (NZwalksDbContext dbContext , IRegionRepository regionRepository ,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult>  GetAllRegions()
        {
            var regionsDomain= await regionRepository.GetAllRegionsAsync();

            var RegionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

            return Ok(RegionsDto);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async  Task<IActionResult> GetRegionById([FromRoute]Guid id)
        {
            var region= await regionRepository.GetRegionByIdAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            var RegionDto = mapper.Map<RegionDto>(region);

            return Ok(RegionDto);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult >Create([FromBody]AddRegionRequestDto region)
        {
            if (ModelState.IsValid)
            {
                var RegionDomain = mapper.Map<Region>(region);
                RegionDomain = await regionRepository.CreateRegionAsync(RegionDomain);
                var RegionDto = mapper.Map<RegionDto>(RegionDomain);
                return CreatedAtAction(nameof(GetRegionById), new { id = RegionDto.Id }, RegionDto);

            }
            return BadRequest(ModelState);
       
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute]Guid id,[FromBody] UpdateRegionRequestDto updatedRegion)
        {
     
                var domainRegion = mapper.Map<Region>(updatedRegion);

                domainRegion = await regionRepository.UpdateRegionAsync(id, domainRegion);


                if (domainRegion == null)
                {
                    return NotFound();
                }

                var RegionDto = mapper.Map<RegionDto>(domainRegion);
                return Ok(RegionDto);
  

        }

        [HttpDelete]
        [Route("{id}:guid")]
        public async Task<IActionResult> deleteRegion([FromRoute]Guid id)
        {

            var RegionDomainToDelete =await regionRepository.DeleteRegionAsync(id);

            if(RegionDomainToDelete == null)
            {
                return NotFound();
            }

            var RegionDto = mapper.Map<RegionDto>(RegionDomainToDelete);
            return Ok(RegionDto);

        }



    }
}
