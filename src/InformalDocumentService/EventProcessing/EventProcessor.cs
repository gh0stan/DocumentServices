using AutoMapper;
using System.Text.Json;
using InformalDocumentService.Data.Models;
using InformalDocumentService.Data.Repositories;
using InformalDocumentService.Dtos;

namespace InformalDocumentService.EventProcessing
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
                case EventType.AbonentCreated:
                    AddAbonent(message);
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
                case "Abonent_Created":
                    Console.WriteLine("DEBUG. Abonent Created event detected.");
                    return EventType.AbonentCreated;
                default:
                    Console.WriteLine("DEBUG. Unknown event type.");
                    return EventType.Undetermined;
            }
        }

        private void AddAbonent(string abonentCreatedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IDocumentRepo>();

                var abonentCreatedDto = JsonSerializer.Deserialize<AbonentCreatedDto>(abonentCreatedMessage);

                try
                {
                    var abonent = _mapper.Map<Abonent>(abonentCreatedDto);
                    if (!repo.AbonentExists(abonent.ExternalId))
                    {
                        repo.CreateAbonent(abonent);
                        repo.SaveChanges();

                        Console.WriteLine("DEBUG. Abonent created.");
                    }
                    else
                    {
                        Console.WriteLine("DEBUG. Abonent already exists...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR. Could not add Abonent to DB. {ex.Message}.");
                }
            }
        }
    }

    enum EventType
    {
        AbonentCreated,
        Undetermined
    }
}
