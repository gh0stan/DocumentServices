using AbonentService;
using AutoMapper;
using Grpc.Net.Client;
using InformalDocumentService.Data.Models;

namespace InformalDocumentService.SyncDataServices.Grpc
{
    public class AbonentDataClient : IAbonentDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AbonentDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Abonent> GetAllAbonents()
        {
            Console.WriteLine($"DEBUG. Calling Grpc server {_configuration["GrpcAbonent"]}.");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcAbonent"]);
            var client = new GrpcAbonent.GrpcAbonentClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllAbonents(request);
                return _mapper.Map<IEnumerable<Abonent>>(reply.Abonent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR. Could not call Grpc server {ex.Message}");
                return null;
            }
        }
    }
}
