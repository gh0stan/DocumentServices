using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Data.Models;
using NotificationService.Dtos;
using NotificationService.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace NotificationService.Controllers
{
    [Route("api/n/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepo _notificationRepo;

        public NotificationController(IMapper mapper, INotificationRepo notificationRepo)
        {
            _mapper = mapper;
            _notificationRepo = notificationRepo;
        }

        /// <summary>
        /// Get all notifications for abonent.
        /// </summary>
        /// <returns>Returns a list of notifications <see cref="NotificationViewDto"/>.</returns>
        [HttpPost]
        public ActionResult<List<NotificationViewDto>> GetAbonentNotifications(int abonentId)
        {
            Console.WriteLine($"DEBUG. Getting all notifications for abonent Id = {abonentId}.");
            
            var notifications = _notificationRepo.GetAbonentNotifications(abonentId);

            return _mapper.Map<List<NotificationViewDto>>(notifications);
        }

        /// <summary>
        /// Mark notifications as viewed.
        /// </summary>
        [HttpPost]
        public ActionResult MarkNotificationsViewed(List<string> notificationIds)
        {
            Console.WriteLine($"DEBUG. Marking notifications as viewed.");

            _notificationRepo.NotificationsMarkViewed(notificationIds);

            return Ok();
        }

        /// <summary>
        /// Mark all notifications as viewed for abonent.
        /// </summary>
        [HttpPost]
        public ActionResult MarkAllNotificationsViewed(int abonentId)
        {
            Console.WriteLine($"DEBUG. Marking all notifications for abonent as viewed.");

            _notificationRepo.NotificationsMarkAllViewed(abonentId);

            return Ok();
        }
    }
}
