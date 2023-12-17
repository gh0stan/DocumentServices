using AutoMapper;
using NotificationService.Data.Models;
using NotificationService.Dtos;

namespace NotificationService.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile() 
        {
            CreateMap<NotificationCreatedDto, Notification>();
            CreateMap<Notification, NotificationViewDto>();
        }
    }
}
