namespace InformalDocumentService.Dtos
{
    public class NotificationCreateDto
    {
        public string Event { get; set; }

        public int ReceiverAbonentId { get; set; }

        public string Message { get; set; }
    }
}
