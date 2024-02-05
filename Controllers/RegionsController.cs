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

        public RegionsController(NZWalksDbContext nZWalksDbContext,IRepository repository)
        {
            this.nZWalksDbContext = nZWalksDbContext;
            this.repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsCollection = await repository.GetAllAsync();
          
            //Map Domain models to Dtos
            var regionsDto = new List<RegionDto>();
            foreach(var region in regionsCollection)
            {
                regionsDto.Add(new RegionDto() 
                { 
                    Id = region.Id,
                    Code=region.Code,
                    Name=region.Name,
                    RegionImageUrl=region.RegionImageUrl 
                });
            }          
            return  Ok(regionsDto);
        }


        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var region = await repository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            else
            {
                //Map  Domain Model to Dto
                var regionDtoObject=new RegionDto()
                { 
                    Id = region.Id, 
                    Code = region.Code, 
                    Name = region.Name, 
                    RegionImageUrl = region.RegionImageUrl 
                };
                return Ok(regionDtoObject);
            }            
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto regionDto)
        {
            if (regionDto == null) { return BadRequest(); }
            var regionDomainModel =new Regions()
            {
                Id = Guid.NewGuid(),
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl

            };

            //Add it to DBContext and SaveIt
                //await  nZWalksDbContext.Regions.AddAsync(regionDomainModel);
                //await nZWalksDbContext.SaveChangesAsync();
                await repository.CreateAsync(regionDomainModel);

            //Convert DomainModel to DTO to send 
            var regionDtotoSend = new RegionDto() {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            //Send 201 response 
            return CreatedAtAction(nameof(Get), new { Id = regionDtotoSend.Id }, regionDtotoSend);

        }

       
        [HttpPut]
        //[Route("{id:Guid}")]

        public async Task<IActionResult> updateRegioDto( Guid Id, [FromBody] UpdateRegionRto updateRegionDto)
        {
            var regionDomainModel = new Regions()
            {
                Id = Id,
                Code = updateRegionDto.Code,
                Name = updateRegionDto.Name,
                RegionImageUrl = updateRegionDto.RegionImageUrl
            };
             regionDomainModel = await repository.UpdateAsync(Id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDtotoSend = new RegionDto {

                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
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
            var regionDtotoSend = new RegionDto()
            {

                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDtotoSend);
        }
    }
}
