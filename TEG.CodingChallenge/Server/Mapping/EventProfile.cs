using AutoMapper;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Shared.Dtos;

namespace TEG.CodingChallenge.Server.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventSummaryDto>();
            CreateMap<Event, EventDetailsDto>()
                .ForMember(dest => dest.VenueName, cfg => cfg.MapFrom(src =>src.Venue.Name))
                .ForMember(dest => dest.Capacity, cfg => cfg.MapFrom(src =>src.Venue.Capacity))
                .ForMember(dest => dest.Location, cfg => cfg.MapFrom(src =>src.Venue.Location));
        }
    }
}
