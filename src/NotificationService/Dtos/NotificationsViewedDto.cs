namespace NotificationService.Dtos
{
    public class NotificationsViewedDto
    {
        public string Event { get; set; }
        public List<string> NotificationIds { get; set; }
    }
}
