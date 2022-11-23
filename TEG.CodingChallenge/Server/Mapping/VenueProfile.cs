using AutoMapper;
using TEG.CodingChallenge.Domain.Models;
using TEG.CodingChallenge.Shared.Dtos;

namespace TEG.CodingChallenge.Server.Mapping
{
    public class VenueProfile : Profile
    {
        public VenueProfile()
        {
            CreateMap<Venue, VenueSummaryDto>();

        }
    }
}
