using AutoMapper;

namespace ImportLeague.Infrastructure.ExternalModels
{
    public class CompetitionProfile : Profile
    {
        public CompetitionProfile()
        {
            CreateMap<Competition, Core.Entities.Competition>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));
            CreateMap<Team, Core.Entities.Team>()
                .ForMember(dest => dest.AreaName, opt => opt.MapFrom(src => src.Area.Name));
            CreateMap<Player, Core.Entities.Player>();
        }
    }
}
