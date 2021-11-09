using System.Linq;
using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();

            CreateMap<Activity, ActivityDto>()
                .ForMember(a => a.HostUsername, o =>
                    o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));

            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(a => a.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(a => a.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(a => a.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(a => a.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(a => a.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<Comment, CommentDto>()
                .ForMember(a => a.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(a => a.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(a => a.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}