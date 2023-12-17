using AutoMapper;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NotificationService.Data.Models;
using NotificationService.Dtos;
using NotificationService.Interfaces;
using System.Text.Json;

namespace NotificationService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.CreateNotification:
                    AddNotification(message);
                    break;
                case EventType.ViewNotifications:
                    ReadNotifications(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("DEBUG. Determining Event.");

            var eventType = JsonSerializer.Deserialize<EventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Create_Notification":
                    Console.WriteLine("DEBUG. Notification created event detected.");
                    return EventType.CreateNotification;
                case "View_Notifications":
                    Console.WriteLine("DEBUG. Notification read event detected.");
                    return EventType.ViewNotifications;
                default:
                    Console.WriteLine("DEBUG. Unknown event type.");
                    return EventType.Undetermined;
            }
        }

        private void AddNotification(string createNotificationMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepo>();

                var notificationCreateDto = JsonSerializer.Deserialize<NotificationCreatedDto>(createNotificationMessage);

                try
                {
                    var notification = _mapper.Map<Notification>(notificationCreateDto);
                    if (!repo.NotificationExists(notification.Id.ToString()))
                    {
                        repo.Create(notification);
                        Console.WriteLine("DEBUG. Notification created.");
                    }
                    else
                    {
                        Console.WriteLine("DEBUG. Notification already exists...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR. Could not add Notification to DB. {ex.Message}.");
                }
            }
        }

        private void ReadNotifications(string viewNotificationsMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<INotificationRepo>();

                var notificationsViewedDto = JsonSerializer.Deserialize<NotificationsViewedDto>(viewNotificationsMessage);

                try
                {
                    repo.NotificationsMarkViewed(notificationsViewedDto.NotificationIds);

                    Console.WriteLine("DEBUG. Notifications marked as viewed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR. Could not mark Notifications as viewed. {ex.Message}.");
                }
            }
        }
    }

    enum EventType
    {
        CreateNotification,
        ViewNotifications,
        Undetermined
    }
}
