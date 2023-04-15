using AutoMapper;
using DattingAppUpdate.Dtos;
using DattingAppUpdate.Entites;
using DattingAppUpdate.Extensions;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace DattingAppUpdate.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, LightUserToReturn>()
            .ForMember(dest => dest.PhotoUrl, opt =>
                 opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
            .ForMember(dest => dest.Age, opt =>
                 opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<User, UserToReturnDto>()
             .ForMember(dest => dest.PhotoUrl, opt =>
                 opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
             .ForMember(dest => dest.Age, opt =>
                 opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoToReturn>();

            CreateMap<UserToUpdateDto, User>();

            CreateMap<PhotoForCreationDto, Photo>();
            
            CreateMap<Photo, PhotoToReturnDto>();

            CreateMap<UserToRegisterDto, User>()
                .ForMember(dest => dest.Email, opt =>
                            opt.MapFrom(src => src.Username + "@gmail.com"));               
        }

    }
}
