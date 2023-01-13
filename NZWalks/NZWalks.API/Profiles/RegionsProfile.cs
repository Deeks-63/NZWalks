using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile 
    {
        public RegionsProfile()
        {
            //Create maps for models
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();

            //If we point without automapper
            //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));

        }
    }
}
