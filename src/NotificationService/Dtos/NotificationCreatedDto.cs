namespace NotificationService.Dtos
{
    public class NotificationCreatedDto
    {
        public string Event { get; set; }

        public int ReceiverAbonentId { get; set; }

        public string Message { get; set; }
    }
}
