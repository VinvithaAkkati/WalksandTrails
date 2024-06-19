using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models.Domain;
using Project1.Models.DTO;
using Project1.Repositories;

namespace Project1.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext nZWalksDbContext;
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext nZWalksDbContext,IRepository repository,IMapper mapper)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            this.repository = repository;
            this.mapper = mapper;
        }

        //Get Method to Get All Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsCollection = await repository.GetAllAsync();        
            var regionsDto= mapper.Map <List<RegionDto>> (regionsCollection);
            return  Ok(regionsDto);
        }
        //Get
        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var region = await repository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDtoObject=mapper.Map<RegionDto> (region);
            return Ok(regionDtoObject);
                        
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto regionDto)
        {
            if (regionDto == null) { return BadRequest(); }
            var regionDomainModel = mapper.Map<Regions>(regionDto);
            await repository.CreateAsync(regionDomainModel);

            //Convert DomainModel to DTO to send 
            var regionDtotoSend = mapper.Map<RegionDto>(regionDomainModel);
            //Send 201 response 
            return CreatedAtAction(nameof(Get), new { Id = regionDtotoSend.Id }, regionDtotoSend);

        }

       
        [HttpPut]
        //[Route("{id:Guid}")]

        public async Task<IActionResult> updateRegioDto( Guid Id, [FromBody] UpdateRegionRto updateRegionDto)
        {
            
            var regionDomainModel=mapper.Map<Regions> (updateRegionDto);
             regionDomainModel = await repository.UpdateAsync(Id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDtotoSend=mapper.Map<RegionDto>(regionDomainModel);   
            return Ok(regionDtotoSend);
        }

        
        [HttpDelete]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> Delete( Guid Id)
        {
            var regionDomainModel = await repository.DeleteAsync(Id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDtotoSend = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDtotoSend);
        }
    }
}
