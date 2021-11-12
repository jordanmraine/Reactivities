using System.Linq;
using Application.Activities;
using Application.Comments;
using Application.Profiles;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;

            CreateMap<Activity, Activity>();

            CreateMap<Activity, ActivityDto>()
                .ForMember(a => a.HostUsername, o =>
                    o.MapFrom(s => s.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));

            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(a => a.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(a => a.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(a => a.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(a => a.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(a => a.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(a => a.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(a => a.Following, o => o.MapFrom(s => s.AppUser.Followers.Any(
                    x => x.Observer.UserName == currentUsername)));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(a => a.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(a => a.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(a => a.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(a => a.Following, o => o.MapFrom(s => s.Followers.Any(
                    x => x.Observer.UserName == currentUsername)));

            CreateMap<Comment, CommentDto>()
                .ForMember(a => a.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(a => a.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(a => a.Image, o => o.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<ActivityAttendee, UserActivityDto>()
                .ForMember(d => d.ID, o => o.MapFrom(s => s.Activity.Id))
                .ForMember(d => d.Date, o => o.MapFrom(s => s.Activity.Date))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Activity.Title))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Activity.Category))
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => 
                    s.Activity.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));
        }
    }
}