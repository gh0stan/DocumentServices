using AbonentService.Dtos;
using AbonentService.Data.Models;
using AbonentService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AbonentService.SyncDataServices.Http;
using AbonentService.AsyncDataServices;

namespace AbonentService.Controllers
{
    [Route("api/a/[controller]/[action]")]
    [ApiController]
    public class AbonentsController : ControllerBase
    {
        private readonly IAbonentRepo _repository;
        private readonly IMapper _mapper;
        private readonly IInformalDocDataClient _informalDocDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public AbonentsController(
            IAbonentRepo abonentRepo, 
            IMapper mapper,
            IInformalDocDataClient informalDocDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = abonentRepo;
            _mapper = mapper;
            _informalDocDataClient = informalDocDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet("{id}", Name = "GetAbonentById")]
        public ActionResult<AbonentReadDto> GetAbonentById(int id)
        {
            Console.WriteLine("DEBUG. Getting abonent by id.");

            var abonent = _repository.GetAbonentById(id);
            if (abonent != null)
            {
                return Ok(_mapper.Map<AbonentReadDto>(abonent));
            }

            return NotFound();
        }

        [HttpGet(Name = "GetAllAbonents")]
        public ActionResult<AbonentReadDto> GetAllAbonents()
        {
            Console.WriteLine("DEBUG. Getting all abonents.");

            var abonents = _repository.GetAllAbonents();
            
            return Ok(_mapper.Map<IEnumerable<AbonentReadDto>>(abonents));
        }

        /// <summary>
        /// Creating an abonent.
        /// </summary>
        /// <param name="createDto">Dto with required information for creating an abonent.</param>
        /// <returns>Returns created abonent <see cref="AbonentReadDto"/>.</returns>
        [HttpPost]
        public ActionResult<AbonentReadDto> CreateAbonent(AbonentCreateDto createDto)
        {
            Console.WriteLine("DEBUG. Creating abonent.");

            var abonentModel = _mapper.Map<Abonent>(createDto);
            _repository.CreateAbonent(abonentModel);
            _repository.SaveChanges();

            var abonentReadDto = _mapper.Map<AbonentReadDto>(abonentModel);

            // Sending sync message
            //try
            //{
            //    await _informalDocDataClient.SendAbonentToInformalDocService(abonentReadDto);
            //}
            //catch (Exception ex) 
            //{ 
            //    Console.WriteLine($"ERROR. Could not send synchronously. {ex.Message}.");
            //}

            // Sending async message
            try
            {
                var abonentCreatedDto = _mapper.Map<AbonentCreatedDto>(abonentReadDto);
                abonentCreatedDto.Event = "Abonent_Created";
                _messageBusClient.CreateNewAbonent(abonentCreatedDto);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"ERROR. Could not send asynchronously. {ex.Message}.");
            }

            return CreatedAtRoute(nameof(GetAbonentById), new {abonentReadDto.Id}, abonentReadDto);
        }
    }
}
