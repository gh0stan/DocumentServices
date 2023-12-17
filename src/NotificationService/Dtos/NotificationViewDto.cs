namespace NotificationService.Dtos
{
    public class NotificationViewDto
    {
        public string? Id { get; set; }

        public int ReceiverAbonentId { get; set; }

        public string Message { get; set; }

        public bool IsRead { get; set; }
    }
}
