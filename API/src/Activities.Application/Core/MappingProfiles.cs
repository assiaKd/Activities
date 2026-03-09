using Activities.Application.Activities.DTO;
using Activities.Domain;
using AutoMapper;


namespace Activities.Application.Core
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            string? currentUserId = null;
            CreateMap<Activity, Activity>();
            CreateMap<CreateActivityDto, Activity>();
            CreateMap<EditActivityDto, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostId, o => o.MapFrom(s =>
                    s.Attendees.FirstOrDefault(x => x.IsHost)!.User.Id))
                .ForMember(d => d.HostDisplayName, o => o.MapFrom(s =>
                    s.Attendees.FirstOrDefault(x => x.IsHost)!.User.DisplayName));
        }
    }
}
