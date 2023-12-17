namespace NotificationService.Interfaces
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}
