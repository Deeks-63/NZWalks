using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers

{
    [ApiController]
    [Route("Regions")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
       public IActionResult GetAllRegions()
       {
            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington",
            //        Code = "WLG",
            //        Area = 223344,
            //        Lat = 2.42113,
            //        Long = 5.23457,
            //        Population= 1000000
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Auckland",
            //        Code = "AUCK",
            //        Area = 267864,
            //        Lat = 4.42786,
            //        Long = 1.56157,
            //        Population= 178656
            //    }
            //};   

           var regions =  regionRepository.GetAll();

            //Return DTO region
            var regionsDTO = new List<Models.DTO.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTO.Region()
                {
                    Id= region.Id,
                    Code = region.Code,
                    Name= region.Name,
                    Area= region.Area,
                    Lat=region.Lat,
                    Long=region.Long,   
                    Population=region.Population,   
                };
                regionsDTO.Add(regionDTO);
            });

            return Ok(regions);
        }
    }
}
