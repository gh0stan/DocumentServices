using AbonentService.Interfaces;
using AutoMapper;
using Grpc.Core;

namespace AbonentService.SyncDataServices.Grpc
{
    public class GrpcAbonentService : GrpcAbonent.GrpcAbonentBase
    {
        private readonly IAbonentRepo _repository;
        private readonly IMapper _mapper;

        public GrpcAbonentService(IAbonentRepo repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<AbonentResponse> GetAllAbonents(GetAllRequest request, ServerCallContext context)
        {
            var response = new AbonentResponse();
            var abonents = _repository.GetAllAbonents();

            foreach (var abonent in abonents)
            {
                response.Abonent.Add(_mapper.Map<GrpcAbonentModel>(abonent));
            }

            return Task.FromResult(response);
        }
    }
}
